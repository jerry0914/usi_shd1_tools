using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace com.usi.shd1_tools.TestGuide
{
    public partial class FormInitial : UserControl
    {
        public enum RunningStatus { Stop = 0, Run = 1, Pause = 2 };
        public RunningStatus Status = RunningStatus.Stop;
        private FormMain mainForm = null;
        private Thread tdRun = null;
        private Thread tdDevCheckRunning = null;
        private System.Windows.Forms.Timer tmrStatus;
        private bool checkDev_Flag = true;
        private bool processOnStarting = false;
        private bool toolInstalling = false;
        private String android_APIFunction_Path = System.AppDomain.CurrentDomain.BaseDirectory + @"ATST\Files\APKs\APIFunction.apk";
        private String android_PackageInstall_Path = System.AppDomain.CurrentDomain.BaseDirectory + @"ATST\BatchCommand\Install.bat";
        private String android_TestCaseList_Path = System.AppDomain.CurrentDomain.BaseDirectory + @"ATST\Scripts\TestCaseList.txt";
        private String selectedScriptPath = "";
        private String lastPreconditionScriptFolder = ""; //Record the last PreconditionScriptFolder to avoid the non-necessary filtering operation
        public FormInitial(FormMain MainForm)
        {
            InitializeComponent();
            lblStatus.Text = "";
            mainForm = MainForm;
            mainForm.ToolSettingsChangedEventHandler += new EventHandler(ToolSettings_Changed);
            tmrStatus = new System.Windows.Forms.Timer();
            tmrStatus.Tick += new EventHandler(tmrStatus_Tick);
            tmrStatus.Interval = 10000;
            loadScriptList(FormToolSettings.Pre_ConditionScriptFolder);
            checkDevProcessRunning();
        }

        public bool APIFunctions_Ready
        {
            get
            {
                bool rtnValue = false;
                if (mainForm.Platform.ToLower().Equals("android"))
                {
                    String standardOutput = "";
                    String argument = "shell pm list packages";
                    ADB_Process.RunAdbCommand(argument, ref standardOutput, true);
                    rtnValue = standardOutput.Contains("com.asus.at");
                }
                else
                {
                    rtnValue = false;
                }
                return rtnValue;
            }
        }

        public bool ToolPackage_Ready
        {
            get
            {
                bool rtnValue = false;
                if (mainForm.Platform.ToLower().Equals("android"))
                {
                    String standardOutput = "";
                    String argument = "shell ls -l /sdcard/ATST/Core/ATST.Run";
                    ADB_Process.RunAdbCommand(argument, ref standardOutput);
                    rtnValue = !standardOutput.ToLower().Contains("no such file or directory");
                }
                else
                {
                    rtnValue = false;
                }
                return rtnValue;
            }
        }

        private void shellScript_Runnable(object arg)
        {
            #region Trim argument
            String argument = arg.ToString().Trim();
            if (!argument.StartsWith("shell"))
            {
                argument = "shell \"" + argument;
            }
            if (argument.EndsWith("&\""))
            {
            }
            else if (argument.EndsWith("&"))
            {
                argument = argument + "\"";
            }
            else
            {
                argument = argument + " &\"";
            }
            #endregion Trim argument
            int counter = 0;
            processOnStarting = false;
            Process psADB = new Process();
            try
            {
                psADB.StartInfo = new ProcessStartInfo(ADB_Process.adbPath);
                psADB.StartInfo.WorkingDirectory = ADB_Process.workingDirectory;
                psADB.StartInfo.Arguments = argument;
                psADB.StartInfo.CreateNoWindow = true;
                psADB.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                psADB.StartInfo.RedirectStandardOutput = true;
                psADB.StartInfo.RedirectStandardError = true;
                psADB.StartInfo.UseShellExecute = false;
                psADB.Start();
                //Start to run & keep retry 200 times
                processOnStarting = true;
                while (psADB.StandardOutput.EndOfStream && counter < 200)
                {
                    counter++;
                    updateStatus("Precondition process is starting , please wait...", Color.Crimson);
                    psADB.Start();
                    Thread.Sleep(100);
                }
                if (counter <= 200)
                {
                    updateStatus("Precondition process starts successfully", Color.Black);
                }
                else
                {
                    updateStatus("Fail to start the test process, please retry again later." + counter.ToString(), Color.Crimson);
                }
                processOnStarting = false;
                //while (!psADB.StandardOutput.EndOfStream || !psADB.StandardError.EndOfStream)
                //{
                //    //show the executing message
                //    if (!psADB.StandardOutput.EndOfStream)
                //    {
                //        appendOutputMessage(psADB.StandardOutput.ReadLine(),Color.Black);
                //    }
                //    if (!psADB.StandardError.EndOfStream)
                //    {
                //        appendOutputMessage(psADB.StandardError.ReadLine(), Color.Crimson);
                //    }
                //}
            }
            catch
            {
            }
            finally
            {
                if (psADB != null)
                {
                    psADB.Close();
                }
            }
        }

        private void checkDevProcessRunning()
        {
            if (tdDevCheckRunning != null)
            {
                tdDevCheckRunning.Abort(1000);
                tdDevCheckRunning = null;
            }

            tdDevCheckRunning = new Thread(checkDevProcessRunning_Runnable);
            tdDevCheckRunning.Start();
        }

        bool isDevChecking = false;
        private void checkDevProcessRunning_Runnable()
        {
            while (checkDev_Flag)
            {
                if (mainForm.ConnectedDevices.Count > 0)
                {
                    if (!isDevChecking)
                    {
                        isDevChecking = true;
                        String myPID = "", standardOutput = "";
                        try
                        {
                            #region GetMyPID
                            ADB_Process.RunAdbCommand("shell cat /sdcard/ATST/ToolInfo/MyPID", ref standardOutput);
                            myPID = standardOutput.Trim(new char[] { '\n', '\r' });
                            #endregion GetMyPID
                            try
                            {
                                Convert.ToInt32(myPID);  //myPID若存在必定為Integer型態，若沒有發生Exception代表myPID取得成功
                                #region 確認ATSP Process是否存在
                                ADB_Process.RunAdbCommand("shell ps " + myPID, ref standardOutput);
                                String strReturnValue = standardOutput.Trim(new char[] { '\n', '\r' });
                                String[] myProcessStatus = strReturnValue.Split(new char[] { '\n', '\r' });
                                if (myProcessStatus.Count() > 1)
                                {
                                    #region Check pause or running
                                    try
                                    {
                                        ADB_Process.RunAdbCommand("shell cat /sdcard/ATST/ToolInfo/Pause.txt", ref standardOutput, false);
                                        String pauseInfo = standardOutput.Trim(new char[] { '\n', '\r' }).Trim();
                                        if (pauseInfo.Equals("1"))
                                        {
                                            Status = RunningStatus.Pause;
                                        }
                                        else
                                        {
                                            Status = RunningStatus.Run;
                                        }
                                    }
                                    catch
                                    {
                                        //If no pause.txt here, means it's running.
                                        Status = RunningStatus.Run;
                                    }
                                    #endregion Check pause or running
                                }
                                else
                                {
                                    Status = RunningStatus.Stop;
                                }
                                #endregion 確認ATSP Process是否存在
                            }
                            catch
                            {
                                Status = RunningStatus.Stop;
                                //It's stoped when fail to get myPID.
                            }
                            if (tdRun == null)
                            {
                                if (Status.Equals(RunningStatus.Run))
                                {
                                    updateStatus("Tool is still  running in the background now...", Color.Black);
                                }
                                else if (Status.Equals(RunningStatus.Pause))
                                {
                                    updateStatus("Tool is paused", Color.Black);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            updateStatus("Exception occur : " + ex.Message, Color.Crimson);
                            Status = RunningStatus.Stop;
                        }
                        finally
                        {
                            isDevChecking = false;
                        }
                    }
                }
                refreshButtonEnable();
                Thread.Sleep(1000);
            }
        }

        private void install_ToolPackage()
        {
            Process p = new Process();
            try
            {
                p.StartInfo = new ProcessStartInfo(android_PackageInstall_Path);
                p.StartInfo.WorkingDirectory = System.AppDomain.CurrentDomain.BaseDirectory + @"ATST\BatchCommand";
                p.StartInfo.Arguments = "";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.UseShellExecute = false;
                p.Start();
                p.WaitForExit();
            }
            catch
            {

            }
            finally
            {
                if (p != null)
                {
                    p.Close();
                }
            }
        }

        private void install_APIFunctions()
        {
            ADB_Process.RunAdbCommand("install -r \"" + android_APIFunction_Path + "\"", true);
        }

        private void install_Script()
        {
            if (File.Exists(selectedScriptPath))
            {
                toolInstalling = true;
                saveToTestCaseList();
                //push TestCaseList.txt
                ADB_Process.RunAdbCommand("push \"" + android_TestCaseList_Path + "\" /sdcard/ATST/Scripts/", true);
                //push Script
                ADB_Process.RunAdbCommand("push \"" + selectedScriptPath + "\" /sdcard/ATST/Scripts/");
                String scriptSourceFolder = Path.GetDirectoryName(selectedScriptPath);
                try
                {
                    String tcFunctionsPath = scriptSourceFolder + "\\TC.Functions";
                    if (File.Exists(tcFunctionsPath))
                    {
                        ADB_Process.RunAdbCommand("push \"" + tcFunctionsPath + "\" /sdcard/ATST/Scripts/");
                    }
                }
                catch { }

                try
                {
                    String variableSettingsPath = scriptSourceFolder + "\\Variable.Settings";
                    if (File.Exists(variableSettingsPath))
                    {
                        ADB_Process.RunAdbCommand("push \"" + variableSettingsPath + "\" /sdcard/ATST/Scripts/");
                    }
                }
                catch
                {
                }
                toolInstalling = false;
            }
        }

        private void runScript()
        {
            if (tdRun != null)
            {
                tdRun.Abort(1000);
                tdRun = null;
            }
            tdRun = new Thread(new ParameterizedThreadStart(shellScript_Runnable));
            tdRun.Start("sh /sdcard/ATST/Core/ATST.Run");
        }

        private void loadScriptList(String sourceDirectory)
        {
            tvScriptList.Nodes.Clear();
            if (Directory.Exists(sourceDirectory))
            {
                tvScriptList.Nodes.Add(loadScriptList_Run(sourceDirectory));
                tvScriptList.Nodes[0].Expand();
            }
        }

        private TreeNode loadScriptList_Run(String directory)
        {
            String name = Path.GetFileNameWithoutExtension(directory);
            TreeNode tnCurrent = new TreeNode(name);
            tnCurrent.ImageIndex = 0;
            tnCurrent.SelectedImageIndex = 0;
            tnCurrent.Tag = directory;
            foreach(String subDir in Directory.GetDirectories(directory))
            {
                tnCurrent.Nodes.Add(loadScriptList_Run(subDir));
            }
            foreach (String file in Directory.GetFiles(directory))
            {
                String fileExtension = Path.GetExtension(file);
                if(fileExtension==null || fileExtension.Length==0)
                {
                    TreeNode tnFile = new TreeNode(Path.GetFileName(file));
                    tnFile.Tag = file;
                    tnFile.ImageIndex = 1;
                    tnFile.SelectedImageIndex = 1;
                    tnCurrent.Nodes.Add(tnFile);
                }
            }
            return tnCurrent;
        }

        private void showScriptContent(String filePath)
        {
            lsvScript.Items.Clear();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(filePath);
                while(!sr.EndOfStream)
                {
                    ListViewItem li = new ListViewItem(sr.ReadLine());
                    lsvScript.Items.Add(li);
                }
            }
            catch
            {
            }
            finally
            {
                if(sr!=null)
                {
                    sr.Close();
                }
            }
        }

        private void saveToTestCaseList()
        {
            this.Cursor = Cursors.WaitCursor;
            #region delete old file
            try
            {
                if (File.Exists(android_TestCaseList_Path))
                {
                    File.Delete(android_TestCaseList_Path);
                }
            }
            catch
            {
            }
            #endregion delete old file
            StreamWriter sw = null;
            try
            {
                String localTestCaseListDirectory = Path.GetDirectoryName(android_TestCaseList_Path);
                if (!Directory.Exists(localTestCaseListDirectory))
                {
                    Directory.CreateDirectory(localTestCaseListDirectory);
                }
                sw = new StreamWriter(android_TestCaseList_Path, true, Encoding.UTF8);
                sw.Write(Path.GetFileName(selectedScriptPath) + "\n");
                sw.Close();
                //Remove BOM ; Special header of Microsoft, it will be an error for unix
                var bytes = System.IO.File.ReadAllBytes(android_TestCaseList_Path);
                if (bytes.Length > 2 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
                {
                    System.IO.File.WriteAllBytes(android_TestCaseList_Path, bytes.Skip(3).ToArray());
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            this.Cursor = Cursors.Default;
        }


        private void ToolSettings_Changed(object sender, EventArgs ea)
        {
            if (lastPreconditionScriptFolder != FormToolSettings.Pre_ConditionScriptFolder)
            {
                lastPreconditionScriptFolder = FormToolSettings.Pre_ConditionScriptFolder;
                loadScriptList(lastPreconditionScriptFolder);
            }
        }

        #region UI Control
        delegate void delVoidNoParam();
        private void refreshButtonEnable()
        {
            if (this.InvokeRequired)
            {
                delVoidNoParam del = new delVoidNoParam(refreshButtonEnable);
                this.Invoke(del);
            }
            else
            {
                if (this.Status == RunningStatus.Stop)
                {
                    btnRun.Enabled = mainForm.ConnectedDevices.Count > 0 && !processOnStarting && !toolInstalling;
                    btnStop.Enabled = false;
                }
                else
                {
                    btnRun.Enabled = false;
                    btnStop.Enabled = mainForm.ConnectedDevices.Count > 0 && !toolInstalling;
                }
            }
        }

        delegate void delVoidStringColor(String str, Color color);
        private void appendOutputMessage(String msg, Color foreColor)
        {
            if (this.InvokeRequired)
            {
                delVoidStringColor del = new delVoidStringColor(appendOutputMessage);
                this.Invoke(del, msg, foreColor);
            }
            else
            {
                ListViewItem li = new ListViewItem(msg);
                li.ForeColor = foreColor;
                lsvScript.Items.Add(li);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            switch(mainForm.Platform.ToLower())
            {
                case "android":
                    if (!APIFunctions_Ready)
                    {
                        updateStatus("APIFunction.apk is installing...", Color.Black);
                        Application.DoEvents();
                        install_APIFunctions();
                    }
                    if (!ToolPackage_Ready)
                    {
                        updateStatus("Tool package is installing...", Color.Black);
                        Application.DoEvents();
                        install_ToolPackage();
                    }
                    install_Script();
                    updateStatus("Installation completed!", Color.Black);
                    Application.DoEvents();
                    runScript();
                    break;
                case "ce":
                    break;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ADB_Process.RunAdbCommand("shell sh /sdcard/ATST/Core/ATST.Forcestop", false);
            updateStatus("Precondition process stoped.", Color.Black);
        }

        private void updateStatus(String msg, Color foreColor)
        {
            if (this.InvokeRequired)
            {
                delVoidStringColor del = new delVoidStringColor(updateStatus);
                this.Invoke(del, msg, foreColor);
            }
            else
            {
                tmrStatus.Start();
                lblStatus.Text = msg;
                lblStatus.ForeColor = foreColor;
            }
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            tmrStatus.Stop();
        }

        private void lsvScript_SizeChanged(object sender, EventArgs e)
        {
            int width = lsvScript.Width - 6;
            if (width > 0)
            {
                lsvScript.Columns[0].Width = width;
            }
        }

        private void RecoverNodeForeColor(TreeNode tnCurrent)
        {
            tnCurrent.ForeColor = Color.Black;
            foreach (TreeNode tnSub in tnCurrent.Nodes)
            {
                RecoverNodeForeColor(tnSub);
            }
        }

        private void tvScriptList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode tn in tvScriptList.Nodes)
            {
                RecoverNodeForeColor(tn);
            }
            if (e.Node.ImageIndex == 1)
            {
                e.Node.ForeColor = Color.Crimson;
                selectedScriptPath = e.Node.Tag.ToString();
                showScriptContent(selectedScriptPath);
            }
            else
            {
                lsvScript.Items.Clear();
            }
        }
        #endregion UI Control


    }
}
