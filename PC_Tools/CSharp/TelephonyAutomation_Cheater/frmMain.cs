using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using com.usi.shd1_tools.TestcasePackage;
using System.Threading;
using dev.jerry_h.pc_tools.CommonLibrary;
using dev.jerry_h.pc_tools.AndroidLibrary;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    public partial class frmMain : Form
    {
        public string Title
        {
            get
            {
                object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title.Length > 0) 
                        return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        public Version Version
        {
            get { return Assembly.GetCallingAssembly().GetName().Version; }
        }
        private String apiFunctionApkPath = System.AppDomain.CurrentDomain.BaseDirectory + "apk\\APIFunction.apk";
        uc8960TelephonyAutomation uc8960 = null;
        ToolStripMenuItem currentTestModeItem =null;
        private List<AdbDeviceInfomation> deviceList;
        public EventHandler<DeviceListChangedEventArgs> deviceListChangedEventHandler;
        private bool devicesMonitorFlag = false;
        private Thread tdDeviceConnectionMonitor;
        private const int deviceConnectingMonitorInterval = 5000;

        public frmMain()
        {
            InitializeComponent();
            this.Text = Title + " - V" + Version.ToString(3);
            currentTestModeItem = dUT1ToDUT2ToolStripMenuItem;
            deviceList = new List<AdbDeviceInfomation>();
            Logger.LiveLogEventHandler += new EventHandler<LoggerLiveMessageEventArgs>(liveLogMessageEventHandler);
            Logger.LogLevel = Logger.LogLevels.Verbose;
            startDevicesMonitor();
            //if (ucDUT == null)
            //{
            //    ucDUT = new ucDutTelephonyAutomation(this);
            //}
            if (uc8960 == null)
            {
                uc8960 = new uc8960TelephonyAutomation(this);
            }
            menuTestMode_Clicked(_8960toDUTToolStripMenuItem, new EventArgs());
        }

        public void ClearLiveLogMessage()
        {
            lsvLiveLog.Items.Clear();
        }

        public void clearStatusBarMsg()
        {
            lblTcResult.Text = "";
            lblChekPointResult.Text = ""; 
        }

        delegate void delVoidObject(object param);

        private void btnCleanLiveLog_Click(object sender, EventArgs e)
        {
            ClearLiveLogMessage();
        }

        private void lsvLiveLog_Resize(object sender, EventArgs e)
        {
            chMessage.Width = lsvLiveLog.Width - chTime.Width - chLogLevel.Width - chTag.Width - 5;
        }

        private bool checkAtstPackageReady(String deviceID)
        {
            bool apkReady = false, pathReady = false;
            String adbArgument = "", adbReturn = "";
            #region check atst apk
            List<String> list = ADB_Process.GetPackagesList(deviceID, "com.asus.at");
            if (list.Count > 0)
            {
                apkReady = true;
            #endregion check atst apk
                #region check file path
                if (deviceID.Length > 0)
                {
                    adbArgument += "-s " + deviceID + " ";
                }
                adbArgument += "shell ls /sdcard/ATST/ToolInfo/";
                ADB_Process.RunAdbCommand(adbArgument, ref adbReturn);
                pathReady = !adbReturn.ToLower().Contains("no such file or directory");
                #endregion check file path
            }
            return apkReady & pathReady;
        }

        private void startDevicesMonitor()
        {
            stopDevicesMonitor();
            devicesMonitorFlag = true;
            tdDeviceConnectionMonitor = new Thread(startDevicesMonitor_Runnable);
            tdDeviceConnectionMonitor.Start();
        }

        private void stopDevicesMonitor()
        {
            devicesMonitorFlag = false;
            if (tdDeviceConnectionMonitor != null)
            {
                tdDeviceConnectionMonitor.Interrupt();
                tdDeviceConnectionMonitor = null;
            }
        }

        private void startDevicesMonitor_Runnable()
        {
            try
            {
                while (devicesMonitorFlag)
                {
                    List<AdbDeviceInfomation> list = ADB_Process.GetDeivcesList();
                    {
                        deviceList = list;
                        foreach (AdbDeviceInfomation dev in list)
                        {
                            if (dev.ConnectingStatus == "Connected")
                            {
                                if (checkAtstPackageReady(dev.ID))
                                {
                                    dev.ConnectingStatus = "Ready";
                                }
                                else 
                                {
                                    initializeAtstPackage(dev.ID);
                                    dev.ConnectingStatus = "Initializing";
                                }
                                if (deviceListChangedEventHandler != null)
                                {
                                    deviceListChangedEventHandler.Invoke(this,new DeviceListChangedEventArgs( deviceList));
                                }
                            }
                            else if (dev.ConnectingStatus.Equals("Initializing"))
                            {
                                if (checkAtstPackageReady(dev.ID))
                                {
                                    dev.ConnectingStatus = "Ready";
                                    if (deviceListChangedEventHandler != null)
                                    {
                                        deviceListChangedEventHandler.Invoke(this, new DeviceListChangedEventArgs(deviceList));
                                    }
                                }
                            }
                        }
                        if (deviceListChangedEventHandler != null)
                        {
                            deviceListChangedEventHandler.Invoke(this, new DeviceListChangedEventArgs(deviceList));
                        }
                    }
                    Thread.Sleep(deviceConnectingMonitorInterval);
                }
            }
            catch (ThreadInterruptedException tie)
            {
            }
        }

        private void initializeAtstPackage(String DeviceID)
        {
            String deviceHeader = "", adbArgument = "", adbReturn = "";
            if (DeviceID.Length > 0)
            {
                deviceHeader = "-s " + DeviceID + " ";
            }            
            #region delete old files
            adbArgument = deviceHeader + "shell rm -r /sdcard/ATST";
            ADB_Process.RunAdbCommand(adbArgument);
            #endregion delete old files
            #region create file path
            adbArgument = deviceHeader + "shell mkdir -p /sdcard/ATST/ToolInfo";
            ADB_Process.RunAdbCommand(adbArgument);
            //adbArgument = deviceHeader + "shell mkdir -p /sdcard/ATST/Core";
            //ADB_Process.RunAdbCommand(adbArgument);
            adbArgument = deviceHeader + "shell mkdir -p /sdcard/ATST/Logs";
            ADB_Process.RunAdbCommand(adbArgument);
            #endregion create file path
            #region install apk
            adbArgument = deviceHeader + "install -r " + "\"" + apiFunctionApkPath + "\"";
            String rtn = "", error = "";
            ADB_Process.RunAdbCommand(adbArgument, ref rtn, ref error, true);
            #endregion install apk   
        }

        #region Progress update

        public void UpdateProgress(object procedureProgressChangedEventArgs)
        {
            if (this.InvokeRequired)
            {
                delVoidObject del = new delVoidObject(UpdateProgress);
                this.Invoke(del, procedureProgressChangedEventArgs);
            }
            else
            {
                ProcedureProgressChangedEventArgs param = procedureProgressChangedEventArgs as ProcedureProgressChangedEventArgs;
                if (param.TotalTimes > 0 && param.TotalTimes > param.Counter)
                {
                    progressMain.Maximum = param.TotalTimes;
                    progressMain.Value = param.Counter;
                    lblProgress.Text = progressMain.Value + " / " + progressMain.Maximum;
                    progressMain.Visible = true;
                    lblProgress.Visible = true;
                }
                else
                {
                    if (param.TotalTimes == param.Counter)
                    {
                        lblProgress.Text = "Completed!!";
                    }
                    else
                    {
                        lblProgress.Text = "";
                        lblProgress.Visible = false;
                    }
                    progressMain.Visible = false;
                }

            }
        }

        #endregion Progress update

        #region Live log UI refresh

        private void liveLogMessageEventHandler(object sender, LoggerLiveMessageEventArgs ea)
        {
            showLiveLog(ea.LiveLogMessage);
        }
        private delegate void delVoid_String(String msg);
        private void showLiveLog(String msg)
        {
            if (this.InvokeRequired)
            {
                delVoid_String del = new delVoid_String(showLiveLog);
                try
                {
                    this.Invoke(del, msg);
                }
                catch (InvalidOperationException iex)
                {
                }
            }
            else
            {
                String[] subMsg = msg.Split('\t');
                if (subMsg.Length >= 4)
                {
                    ListViewItem li = new ListViewItem(subMsg[0]);
                    li.SubItems.AddRange(new String[] { subMsg[1], subMsg[2], subMsg[3] });
                    if (subMsg[2].Equals(Logger.LogTags.Summary) || subMsg[2].Equals(Logger.LogTags.Conclusion.ToString()))
                    {
                        li.ForeColor = SystemColors.HotTrack;
                    }
                    else if ((subMsg[3].ToUpper().Contains("FAIL") && subMsg[2].Equals(Logger.LogTags.CheckPoint.ToString())) || subMsg[1].ToUpper().Equals("E"))
                    {
                        li.ForeColor = Color.Red;
                    }
                    else if (subMsg[3].ToUpper().Contains("PASS") && subMsg[2].Equals(Logger.LogTags.CheckPoint.ToString()))
                    {
                        li.ForeColor = Color.Green;
                    }
                    lsvLiveLog.Items.Insert(0, li);
                }
                int itmesCount = lsvLiveLog.Items.Count;
                if (itmesCount > 400)
                {
                    try
                    {
                        lsvLiveLog.Items.RemoveAt(itmesCount - 1);
                    }
                    catch { }
                }

            }
        }

        #endregion Live log UI refresh

        #region Test result update

        public void UpdateTestResult(object testResultUpdateEventArgs)
        {
            if (this.InvokeRequired)
            {
                delVoidObject del = new delVoidObject(UpdateTestResult);
                this.Invoke(del, testResultUpdateEventArgs);
            }
            else
            {
                TestResultUpdateEventArgs param = testResultUpdateEventArgs as TestResultUpdateEventArgs;
                if (param.PassedCheckPoint > 0 || param.FailedCheckPoint > 0)
                {
                    lblChekPointResult.Visible = true;
                    lblChekPointResult.Text = "Check points : Pass = " + param.PassedCheckPoint + " ,Fail = " + param.FailedCheckPoint;
                }
                else
                {
                    lblChekPointResult.Visible = false;
                    lblChekPointResult.Text = "";
                }
                if (param.PassedTestCase > 0 || param.FailedTestCase > 0)
                {
                    lblTcResult.Visible = true;
                    lblTcResult.Text = "Test cases : Pass = " + param.PassedTestCase + " ,Fail = " + param.FailedTestCase;
                }
                else
                {
                    lblTcResult.Text = "";
                    lblTcResult.Visible = false;
                }
            }
        }

        #endregion  Test result update

        private void menuTestMode_Clicked(object sender, EventArgs e)
        {            
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            UserControl usrctl = null;
            currentTestModeItem = item;
            foreach (ToolStripMenuItem tsmi in testModeToolStripMenuItem.DropDownItems)
            {
                tsmi.Checked = false;
            }
            item.Checked = true;
            switch (item.Name)
            {
                case "dUT1ToDUT2ToolStripMenuItem":
                    //if (ucDUT == null)
                    //{
                    //    ucDUT = new ucDutTelephonyAutomation(this);
                    //}
                    //usrctl = ucDUT;
                    break;
                case "_8960toDUTToolStripMenuItem":
                    if (uc8960 == null)
                    {
                        uc8960 = new uc8960TelephonyAutomation(this);
                    }
                    usrctl = uc8960;
                    break;
                default:
                    break;
            }
            if (usrctl != null)
            {
                pnlMain.Controls.Clear();
                pnlMain.Controls.Add(usrctl);
                usrctl.Dock = DockStyle.Fill;
                usrctl.Show();
            }
        }
    }

    public class DeviceListChangedEventArgs : EventArgs
    {
        public List<AdbDeviceInfomation> DeviceList;
        public DeviceListChangedEventArgs(List<AdbDeviceInfomation> list)
        {
            DeviceList = list;
        }
    }
}
