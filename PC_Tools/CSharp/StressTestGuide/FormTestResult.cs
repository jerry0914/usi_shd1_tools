using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Threading;
using System.Text.RegularExpressions;

namespace com.usi.shd1_tools.TestGuide
{
    public partial class FormTestResult : UserControl
    {
        private FormMain frmMain = null;
        private List<String> logList = new List<string>();
        private String localLogFolder = "";
        private String deviceID = "";
        private String currentLogPath = "";
        private Thread tdLogParse = null;
        private bool parseLog_Flag = false;
        private System.Windows.Forms.Timer tmrCleanMsg;
        private ProfilerLog profilerLog = null;
        private ProfilerLog_TestCase selectedTestCase = null;
        private int logMsgSpliteSize = 2000;
        private int currentTCIndex = -1;
        private int currentPageIndex = -1;
        private int currentLine = -1;
        private int lastLogInterval = -1;//Record the last log interval to avoid the non-necessary filtering operation

        public FormTestResult(FormMain mainform)
        {
            tmrCleanMsg = new System.Windows.Forms.Timer();
            tmrCleanMsg.Interval = 10000;
            tmrCleanMsg.Tick+=new EventHandler(tmrCleanMsg_Tick);
            frmMain = mainform;
            frmMain.ConnectedDevicesChangedEventHandler += new EventHandler(connectedDeviceChanged);
            frmMain.ToolSettingsChangedEventHandler += new EventHandler(ToolSettings_Changed);
            InitializeComponent();
            lblStaus.Text = "";
            showConnectDevices();
            listLocalLogs();
        }       

        private void showConnectDevices()
        {
            if (frmMain != null)
            {
                cmbDeviceList.DataSource = frmMain.ConnectedDevices;
            }
        }

        private delegate void delEventHandler(object sender, EventArgs ea);
        private void connectedDeviceChanged(object sender, EventArgs ea)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delEventHandler(connectedDeviceChanged), sender, ea);
            }
            else
            {
                showConnectDevices();
                checkGetLogReady();
            }
        }

        private void checkGetLogReady()
        {
            bool logFolderExist = Directory.Exists(FormToolSettings.LogFolder);
            btnGetLog.Enabled = frmMain.ConnectedDevices.Count > 0 && logFolderExist;
        }
        
        private void downloadLogFiles()
        {
            logList.Clear();
            String remoteLogFolder = "";
            switch (frmMain.Platform.ToLower())
            {

                case "ce":
                    {
                        remoteLogFolder = @"\Application\";
                        String searchingCriterion = @"Log-*.txt";
                        logList = CE_Process.GetFileList(remoteLogFolder, searchingCriterion);
                        foreach (String log in logList)
                        {
                            CE_Process.GetFileFormDevice(log,FormToolSettings.LogFolder+"\\");
                        }
                    }
                    break;
                case "android":

                    remoteLogFolder = "/sdcard/";
                    String outputString = "";
                    String argGetLogFiles = "shell ls \"" + remoteLogFolder + "\"Log-*.txt";
                    #region Get log file list
                    if (deviceID.Length > 0)
                    {
                        argGetLogFiles = "-s " + deviceID + " " + argGetLogFiles;
                    }
                    ADB_Process.RunAdbCommand(argGetLogFiles, ref outputString);
                    if (outputString.ToLower().Contains("No such file or directory".ToLower()))
                    {
                        //Log file does not exist.
                    }
                    else
                    {
                        outputString = outputString.Replace("\r", "").TrimEnd('\n').Trim();
                        logList.AddRange(outputString.Split('\n'));
                    }
                    #endregion Get log file list
                    #region pull the log file(s) to PC
                    String argPullLog = "";
                    foreach (String log in logList)
                    {
                        argPullLog = "pull \"" + log + "\" \"" + FormToolSettings.LogFolder + "\"";
                        if (deviceID.Length > 0)
                        {
                            argPullLog = "-s " + deviceID + " " + argPullLog;
                        }
                        ADB_Process.RunAdbCommand(argPullLog);
                    }
                    #endregion pull the log file(s) to PC
                    break;
                default:
                    break;
            }
        }

        private void listLocalLogs()
        {
            cmbLogFileList.Items.Clear();
            cmbLogFileList.Items.Add("");
            //String[] localLogs = Directory.GetFiles(directory, "Log-*.txt");
            List<String> localLogs = listLocalLogs(FormToolSettings.LogFolder);
            cmbLogFileList.Items.AddRange(localLogs.ToArray<String>());
            #region By default, auto-select the first log pulled from DUT .
            int indexToAutoSelect = 0;
            if (logList.Count > 0)
            {               
                for (indexToAutoSelect=0;indexToAutoSelect<localLogs.Count;indexToAutoSelect++)
                {
                    String targetname = Path.GetFileName(logList[0]);
                    if (localLogs[indexToAutoSelect].Contains(targetname))
                    {
                        break;
                    }
                }
            }
            if (indexToAutoSelect < localLogs.Count && logList.Count > 0)
            {
                cmbLogFileList.SelectedIndex = indexToAutoSelect + 1;
            }
            else  //Target log does not found, select empty name by default;
            {
                cmbLogFileList.SelectedIndex = 0;
            }
            #endregion By default, auto-select the first log pulled from DUT .
        }

        private List<String> listLocalLogs(String directory)
        {
            List<String> logs = new List<string>();
            try
            {
                logs.AddRange(Directory.GetFiles(directory, "Log-*.txt"));
                foreach (String dir in Directory.GetDirectories(directory))
                {
                    logs.AddRange(listLocalLogs(dir));
                }
            }
            catch
            {
            }
            return logs;
        }

        private void startLogParse(String logPath)
        {
            profilerLog = new ProfilerLog(logPath);
            profilerLog.Parse();
            foreach (ProfilerLog_TestCase tc in profilerLog.TestCaseList)
            { 
                ddlistTestCases.DropDownItems.Add(tc.Title + "( " + tc.StartTime.ToString("HH:mm:ss") + "~" + tc.EndTime.ToString("HH:mm:ss") + " )");
            }
        }

        private void showSummaries()
        {
            txtStartTime.Text = profilerLog.StartTime.ToString("yyyy/MM/dd HH:mm:ss");
            txtEndTime.Text = profilerLog.EndTime.ToString("yyyy/MM/dd HH:mm:ss");
            TimeSpan ts = profilerLog.EndTime.Subtract(profilerLog.StartTime);
            txtElapsedTime.Text = ((int)ts.TotalHours).ToString() + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
            txtTestCaseNo.Text = profilerLog.TestCaseList.Count.ToString();
            txtTotalMsgNo.Text = profilerLog.MessageCount.ToString();
            txtErrorMsgNo.Text = profilerLog.ErrorCount.ToString();
        }

        /*
        private void readAndParseLog()
        {
            int passCount = 0;
            int failCount = 0;
            if (File.Exists(currentLogPath))
            {

               StreamReader sr = null;
               Regex rgxPass = new Regex(@"\|(\s*)(?i:info)(\s*)\|");
               Regex rgxFail = new Regex(@"\|(\s*)(?i:error)(\s*)\|");
               Match m;
               try
               {
                   sr = new StreamReader(currentLogPath);
                   while (!sr.EndOfStream && parseLog_Flag)
                   {
                       String line = sr.ReadLine();
                       m = rgxPass.Match(line);
                       if (m.Success)
                       {
                           passCount++;
                           rtxtLogAppendNewText(line);
                       }
                       m = rgxFail.Match(line);
                       if (m.Success)
                       {
                           failCount++;
                           rtxtLogAppendNewText(line, Color.Crimson);
                       }
                       updateStatusLabel("Pass = " + passCount + "    ;    Fail = " + failCount, Color.Black);
                   }          
                   updateStatusLabel("Pass = " + passCount + "    ;    Fail = " + failCount + (parseLog_Flag ? ", finished!" : ", interuputed!"), (parseLog_Flag?Color.Green:Color.Red));
               }
               catch
               {
               }
               if (sr != null)
               {
                   sr.Close();
               }
            }
            parseLog_Flag = false;
            parseLogStausChanged();
        }
        */
        private void showLogMsgOfTestCase(ProfilerLog_TestCase tc)
        {
            showLogMsgOfTestCase(tc, 0, logMsgSpliteSize);
            currentPageIndex = 0;
        }

        private void showLogMsgOfTestCase(ProfilerLog_TestCase tc, int startIndex, int length)
        {
            lstResultList.Items.Clear();
            this.Cursor = Cursors.WaitCursor;
            for (int counter = 0; counter < length; counter++)
            {
                try
                {
                    if (tc.LogMessages.Count > startIndex + counter)
                    {
                        ProfilerLogMsg msg = tc.LogMessages[startIndex + counter];
                        ListViewItem li = new ListViewItem((startIndex + counter + 1).ToString("0000"));
                        ListViewItem.ListViewSubItem lsi = new ListViewItem.ListViewSubItem(li, msg.ToString());
                        li.SubItems.Add(lsi);
                        if ((int)msg.Level == (int)ProfilerLogMsg.LogLevels.Debug)
                        {
                            li.BackColor = Color.Yellow;
                        }
                        else if ((int)msg.Level == (int)ProfilerLogMsg.LogLevels.Error)
                        {
                            li.BackColor = Color.Crimson;
                        }
                        else
                        {
                            li.BackColor = Color.White;
                        }
                        if (msg.Timeout)
                        {
                            li.ForeColor = Color.Gray;
                        }
                        lstResultList.Items.Add(li);
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {

                }
            }
            //heighMsgText();
            this.Cursor = Cursors.Default;
        }

        //private void heighMsgText()
        //{
        //    Regex rgxDebug = new Regex(@"\|(\s*)(?i:debug)(\s*)\|");
        //    Regex rgxError = new Regex(@"\|(\s*)(?i:error)(\s*)\|");
        //    MatchCollection mcDebug = rgxDebug.Matches(rtxtResult.Text);
        //    MatchCollection mcError = rgxError.Matches(rtxtResult.Text);
        //    foreach (Match m in mcDebug)
        //    {
        //        rtxtResult.Select(m.Index, m.Length);
        //        rtxtResult.SelectionBackColor = Color.Yellow;
        //    }
        //    foreach (Match m in mcError)
        //    {
        //        rtxtResult.Select(m.Index, m.Length);
        //        rtxtResult.SelectionBackColor = Color.Crimson;
        //    }
        //}

        private delegate void delVoidStringColor(String msg, Color color);
        //private void rtxtLogAppendNewText(String msg, Color backColor)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.Invoke(new delVoidStringColor(rtxtLogAppendNewText),msg,backColor);
        //    }
        //    else
        //    {
        //        rtxtResult.SelectionBackColor = backColor;
        //        rtxtResult.AppendText(msg + "\n");
        //    }
        //}
        //private void rtxtLogAppendNewText(String msg)
        //{
        //    rtxtLogAppendNewText(msg,Color.White);
        //}

        private void updateStatusLabel(String msg, Color fontColor)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delVoidStringColor(updateStatusLabel),msg,fontColor);
            }
            else
            {
                tmrCleanMsg.Start();
                lblStaus.Text = msg;
                lblStaus.ForeColor = fontColor;
            }
        }

        private bool findNextErrorMsg()
        {
            int visibleCount, currentIndex, nextIndex, nextPageIndex = 0;
            bool found = false;
            GetIndexes(lstResultList, out currentLine, out visibleCount);
            currentLine = currentLine + visibleCount >= lstResultList.Items.Count ? lstResultList.Items.Count -1: currentLine + visibleCount;
            currentIndex = currentPageIndex * logMsgSpliteSize + currentLine;

            if (selectedTestCase != null && selectedTestCase.ErrorIndexList.Count > 0)
            {
                for (int i = 0; i < selectedTestCase.ErrorIndexList.Count; i++)
                {
                    if (selectedTestCase.ErrorIndexList[i] > currentIndex)
                    {
                        nextIndex = selectedTestCase.ErrorIndexList[i];
                        nextPageIndex = nextIndex / logMsgSpliteSize;
                        //Auto jump to the current page of log msg
                        if (nextPageIndex != currentPageIndex)
                        {
                            ddlistSplitedLogPage_DropDownItemClicked(this.ddlistSplitedLogPage, new ToolStripItemClickedEventArgs(this.ddlistSplitedLogPage.DropDownItems[nextPageIndex]));
                        }
                        currentLine = nextIndex % logMsgSpliteSize + visibleCount;
                        if (currentLine >= lstResultList.Items.Count)
                        {
                            currentLine = lstResultList.Items.Count - 1;
                        }
                        lstResultList.Items[currentLine].EnsureVisible();
                        found = true;
                        break;
                    }
                }
            }
            return found;
        }

        private bool findPreviousErrorMsg()
        {
            int visibleCount, currentIndex, nextIndex, nextPageIndex = 0;
            bool found = false;
            GetIndexes(lstResultList, out currentLine, out visibleCount);
            currentIndex = currentPageIndex * logMsgSpliteSize + currentLine;            
            if (selectedTestCase != null && selectedTestCase.ErrorIndexList.Count > 0)
            {
                for (int i = selectedTestCase.ErrorIndexList.Count-1; i >=0 ; i--)
                {
                    if (selectedTestCase.ErrorIndexList[i] < currentIndex)
                    {
                        nextIndex = selectedTestCase.ErrorIndexList[i];
                        nextPageIndex = nextIndex / logMsgSpliteSize;
                        //Auto jump to the current page of log msg
                        if (nextPageIndex != currentPageIndex)
                        {
                            ddlistSplitedLogPage_DropDownItemClicked(this.ddlistSplitedLogPage, new ToolStripItemClickedEventArgs(this.ddlistSplitedLogPage.DropDownItems[nextPageIndex]));
                        }
                        currentLine = nextIndex % logMsgSpliteSize;
                        lstResultList.Items[currentLine].EnsureVisible();
                        found = true;
                        break;
                    }
                }
            }
            return found;
        }

        private void GetIndexes(ListView vv, out int startidx, out int count)
        {
            ListViewItem lvi1 = vv.GetItemAt(vv.ClientRectangle.X + 6, vv.ClientRectangle.Y + 6);
            ListViewItem lvi2 = vv.GetItemAt(vv.ClientRectangle.X + 6, vv.ClientRectangle.Bottom - 10);
            startidx = vv.Items.IndexOf(lvi1);
            int endidx = vv.Items.IndexOf(lvi2);
            if (endidx == -1) endidx = vv.Items.Count;
            count = endidx - startidx;
        }

        private void ToolSettings_Changed(object sender, EventArgs ea)
        {
            if (lastLogInterval != FormToolSettings.MaxLogInterval)
            {
                lastLogInterval = FormToolSettings.MaxLogInterval;
                profilerLog.RefreshIsMsgTimeout(FormToolSettings.MaxLogInterval);
                checkGetLogReady();
                listLocalLogs();
            }
        }
        #region UI event

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            int width = 0;
            width = btnGetLog.Location.X - cmbDeviceList.Location.X - 10;
            if(width>0)
            {
                cmbDeviceList.Width = width;
            }
            
            width = btnGetLog.Location.X - cmbLogFileList.Location.X - 10;
            if (width > 0)
            {
                cmbLogFileList.Width = width;
            }
        }

        private void btnGetLog_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            downloadLogFiles();
            listLocalLogs();
            this.Cursor = Cursors.Default;
        }

        private void cmbDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            deviceID = cmbDeviceList.Text;
        }

        private void cmbLogFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstResultList.Items.Clear();
            btnNext.Visible = false;
            btnPrevious.Visible = false;
            #region erase summaries
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            txtElapsedTime.Text = "";
            txtTestCaseNo.Text = "";
            txtTotalMsgNo.Text = "";
            txtErrorMsgNo.Text = "";
            #endregion erase summaries
            try
            {
                if (cmbLogFileList.Text.Trim().Length > 0)
                {
                    currentLogPath = cmbLogFileList.Text.Trim();
                }
                else
                {
                    currentLogPath = "";
                    updateStatusLabel("", Color.Black);
                }
            }
            catch
            {
                currentLogPath = "";
            }
            btnParseSwitch.Enabled = currentLogPath.Length > 0;
        }

        private void btnParseSwitch_Click(object sender, EventArgs e)
        {
            profilerLog = null;
            selectedTestCase = null;
            ddlistTestCases.DropDownItems.Clear();
            lstResultList.Items.Clear();
            updateStatusLabel("Start to parse log date, please wait...... ", Color.Black);
            btnParseSwitch.Text = "   Stop parse";
            btnParseSwitch.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.stop;
            cmbLogFileList.Enabled = false;
            ddlistTestCases.Visible = false;
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            parseLog_Flag = !parseLog_Flag;
            startLogParse(cmbLogFileList.Text);
            profilerLog.RefreshIsMsgTimeout(FormToolSettings.MaxLogInterval);
            showSummaries();
            currentTCIndex = -1;
            currentPageIndex = -1;
            currentLine = -1;
            btnNext.Visible = true;
            btnPrevious.Visible = true;
            this.Cursor = Cursors.Default;
            ddlistTestCases.Visible = ddlistTestCases.DropDownItems.Count > 0;
            btnParseSwitch.Text = "   Start parse";
            btnParseSwitch.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.Start;
            cmbLogFileList.Enabled = true;
            updateStatusLabel("All messages = " + profilerLog.MessageCount + "    ;    Fail = " + profilerLog.ErrorCount, Color.Black);
            #region auto-select the first testcase result to show as default
            if (ddlistTestCases.DropDownItems.Count > 0)
            {
                ddlistTestCases_DropDownItemClicked(ddlistTestCases,new ToolStripItemClickedEventArgs(ddlistTestCases.DropDownItems[0]));
            }
            #endregion auto-select the first testcase result to show as default
        }
        /*
        private delegate void delVoidNoParam();
        private void parseLogStausChanged()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delVoidNoParam(parseLogStausChanged));
            }
            else
            {
                if (parseLog_Flag)
                {
                    startLogParse();
                    btnParseSwitch.Text = "   Stop parse";
                    btnParseSwitch.Image = global::StressTestGuide.Properties.Resources.stop;
                }
                else
                {
                    btnParseSwitch.Text = "   Start parse";
                    btnParseSwitch.Image = global::StressTestGuide.Properties.Resources.Start;
                }
                cmbLogFileList.Enabled = !parseLog_Flag;
            }
        }*/

        private void tmrCleanMsg_Tick(object sender, EventArgs ea)
        {
            lblStaus.Text = "";
            tmrCleanMsg.Stop();
        }

        private void ddlistTestCases_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int selectedIndex = ddlistTestCases.DropDownItems.IndexOf(e.ClickedItem);
            if(profilerLog!=null && selectedIndex!=currentTCIndex)
            {
                currentPageIndex = -1;
                currentTCIndex = selectedIndex;
                selectedTestCase = profilerLog.TestCaseList[selectedIndex];
                ddlistSplitedLogPage.DropDownItems.Clear();
                for (int i = 1; i < selectedTestCase.LogMessages.Count; i += logMsgSpliteSize)
                {
                    ddlistSplitedLogPage.DropDownItems.Add(i.ToString() + " ~");
                }
                ddlistSplitedLogPage.Text = "1 ~";
                showLogMsgOfTestCase(selectedTestCase);
                ddlistSplitedLogPage.Visible = selectedTestCase != null;
                lblLines.Visible = selectedTestCase != null;
                ddlistTestCases.Text = e.ClickedItem.Text;
            }
        }

        private void ddlistSplitedLogPage_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int selectIndex = ddlistSplitedLogPage.DropDownItems.IndexOf(e.ClickedItem);
            if (selectIndex != currentPageIndex && selectIndex>=0)
            {
                currentPageIndex = selectIndex;
                showLogMsgOfTestCase(selectedTestCase, logMsgSpliteSize * selectIndex, logMsgSpliteSize);
                ddlistSplitedLogPage.Text = e.ClickedItem.Text;
            }
        }

        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            lstResultList.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            chText.Width = lstResultList.Width - chLineNo.Width;
        }

        private void btnNext_ButtonClick(object sender, EventArgs e)
        {
            if (!findNextErrorMsg())
            {
                if (currentTCIndex == profilerLog.TestCaseList.Count - 1)
                {
                    updateStatusLabel("It's the last error message already.", Color.Navy);
                }
                // Jump into the next test case to find the next erorr msg
                else
                {
                    bool nextsTestCaseFound = false;
                    for (int i = currentTCIndex +1 ; i < profilerLog.TestCaseList.Count; i++)
                    {
                        //Jump to the previous TestCase
                        if (profilerLog.TestCaseList[i].ErrorIndexList.Count > 0)
                        {
                            ddlistTestCases_DropDownItemClicked(ddlistTestCases, new ToolStripItemClickedEventArgs(ddlistTestCases.DropDownItems[i]));
                            ddlistSplitedLogPage_DropDownItemClicked(ddlistSplitedLogPage, new ToolStripItemClickedEventArgs(ddlistSplitedLogPage.DropDownItems[0]));
                            //lstResultList.Items[lstResultList.Items.Count - 1].EnsureVisible();
                            nextsTestCaseFound = true;
                            break;
                        }
                    }
                    if (nextsTestCaseFound)
                    {
                        btnNext_ButtonClick(sender, e);
                    }
                    else
                    {
                        updateStatusLabel("It's the last error message already.", Color.Navy);
                    }
                }
            }
        }

        private void btnPrevious_ButtonClick(object sender, EventArgs e)
        {
            if (!findPreviousErrorMsg())
            {
                if (currentTCIndex == 0)
                {
                    updateStatusLabel("It's the first error message already.", Color.Navy);
                }
                // Jump into the previous test case to find the previous erorr msg
                else
                {
                    bool previousTestCaseFound = false;
                    for (int i = currentTCIndex - 1; i >= 0; i--)
                    {
                        //Jump to the previous TestCase
                        if (profilerLog.TestCaseList[i].ErrorIndexList.Count > 0)
                        {
                            ddlistTestCases_DropDownItemClicked(ddlistTestCases, new ToolStripItemClickedEventArgs(ddlistTestCases.DropDownItems[i]));
                            ddlistSplitedLogPage_DropDownItemClicked(ddlistSplitedLogPage, new ToolStripItemClickedEventArgs(ddlistSplitedLogPage.DropDownItems[ddlistSplitedLogPage.DropDownItems.Count - 1]));
                            lstResultList.Items[lstResultList.Items.Count - 1].EnsureVisible();
                            previousTestCaseFound = true;
                            break;
                        }
                    }
                    if (previousTestCaseFound)
                    {
                        btnPrevious_ButtonClick(sender, e);
                    }
                    else
                    {
                        updateStatusLabel("It's the first error message already.", Color.Navy);
                    }
                }
            }
        }

        private void btnSummariesVisiable_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = !groupBox1.Visible;
            if (groupBox1.Visible)
            {
                btnSummariesVisiable.Image = Properties.Resources.up_1;
            }
            else
            {
                btnSummariesVisiable.Image = Properties.Resources.down_1;
            }
        }

        private void btnBatteryUsage_Click(object sender, EventArgs e)
        {
            StreamWriter sw = null;
            String fileName = "C:\\BatteryInfo_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            try
            {
                sw = new StreamWriter(fileName, true);
                foreach(ProfilerLog_TestCase tc in profilerLog.TestCaseList)
                {
                    foreach (BatteryInfo batteryInfo in tc.BatteryInfoList)
                    {
                        sw.WriteLine(batteryInfo.Time.ToString("MM/dd_HH:mm:ss") + "\t\t" +
                                               batteryInfo.Percentage.ToString("0.0") + "%\t" +
                                               batteryInfo.AC_Status + "\t" +
                                               batteryInfo.Voltage.ToString() + " mV\t" +
                                               batteryInfo.Temperature.ToString() + " °c\t" +
                                               batteryInfo.AvgCurrent.ToString() + " mA\t"+
                                               tc.Title);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if(sw!=null)
                {
                    sw.Close();
                    sw = null;
                }
            }
            MessageBox.Show("Battery infomation save to : " + fileName);
        }

        #endregion UI event
    }
}
