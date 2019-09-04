using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using System.IO;
using OpenNETCF.Desktop.Communication;
using System.Text.RegularExpressions;
using System.Diagnostics;
using ExtensionMethods;

namespace com.usi.shd1_tools.TestGuide
{
    public partial class FormMain : Form
    {
        public string Title
        {
            get
            {
                object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title.Length > 0) return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        public Version Version 
        {
            get { return Assembly.GetCallingAssembly().GetName().Version; } 
        }
        private List<TestCase> lstTestCase_All = new List<TestCase>();
        public EventHandler SelectedPlatformChangedEventHandler = null;
        public EventHandler ConnectedDevicesChangedEventHandler = null;
        public EventHandler ToolSettingsChangedEventHandler = null;

        internal String Platform
        {
            get
            {
                return platform;
            }
        }
        private String platform = "";
        internal String DefaultSettingsPath = System.AppDomain.CurrentDomain.BaseDirectory + "DefaultSettings.xml";
        private Thread tdDeviceCheck = null;
        public List<String> ConnectedDevices
        {
            get
            {
                List<String> rtnValue = new List<String>();
                foreach (Device_Infomation dev in deviceList)
                {
                    if (!dev.ConnectingStatus.ToLower().Equals("offline"))
                    {
                        rtnValue.Add(dev.ID);
                    }
                }
                return rtnValue;
            }
        }

        public bool IsDeviceOffline
        {
            get
            {
                return (deviceList.Count == 1 && deviceList[0].ConnectingStatus.ToLower().Equals("offline"));                
            }
        }        
        private List<Device_Infomation> deviceList = new List<Device_Infomation>();
        private int connetedDevicesCount = 0;
        private bool deviceCheck_Flag = true;
        private String device_IPAddress = "";
        FormInstall frmInstall = null;
        FormTestResult frmTestResult = null;
        FormInitial frmInitial = null;
        FormDataMaintaince frmDataMaintainance = null;
        
        public FormMain()
        {
            InitializeComponent();
            this.Text = Title + " V" + Version;
            ADB_Process.startADB();
            loadDefaultSettings();
            startDeivceCheck();
            menuActionSelected(this.menuTRSReader, new EventArgs());  //Select Install Page  by default;
        }

        private void startDeivceCheck()
        {
            if (tdDeviceCheck != null)
            {
                tdDeviceCheck.Abort(1000);
                tdDeviceCheck = null;
            }
            tdDeviceCheck = new Thread(checkConnectedDevice_Runnable);
            tdDeviceCheck.Start();
        }

        private void checkConnectedDevice_Runnable()
        {
            while (deviceCheck_Flag)
            {
                switch (platform.ToLower())
                {
                    case "ce":
                        if (CE_Process.Connected)
                        {
                            deviceList = new List<Device_Infomation>();
                            deviceList.Add(new Device_Infomation("WinCE device", "Connected"));
                        }
                        else
                        {
                            deviceList.Clear();
                            if (ConnectedDevicesChangedEventHandler != null)
                            {
                                ConnectedDevicesChangedEventHandler(this, new EventArgs());
                            }
                        }
                        break;
                    case "android":
                        {
                            deviceList = ADB_Process.GetDeivcesList();
                        }
                        break;
                }
                if (connetedDevicesCount != deviceList.Count)
                {
                    connetedDevicesCount = deviceList.Count;
                    if (ConnectedDevicesChangedEventHandler != null)
                    {
                        ConnectedDevicesChangedEventHandler(this, new EventArgs());
                    }
                }
                if (connetedDevicesCount == 1 && platform.ToLower().Equals("android"))
                {
                    String rtnString = "", reguexNetCfg = @"wlan\d+\s*\S*\s*(?<IP>\s*\d+\s*.\s*\d+\s*.\s*\d+\s*.\s*\d+\s*)\s*\S*\s*\S*\s*(?<MAC>\s*[a-fA-f0-9]+\s*:\s*[a-fA-f0-9]+\s*:\s*[a-fA-f0-9]+\s*:\s*[a-fA-f0-9]+\s*:\s*[a-fA-f0-9]+\s*:\s*[a-fA-f0-9]+\s*)";
                    ADB_Process.RunAdbCommand("shell netcfg", ref rtnString);
                    Regex regNetcfg = new Regex(reguexNetCfg);
                    Match m = regNetcfg.Match(rtnString);
                    if (m.Success)
                    {
                        device_IPAddress = m.Groups["IP"].Value.Equals("0.0.0.0") ? "" :m.Groups["IP"].Value;
                    }
                    else
                    {
                        device_IPAddress = "";
                    }
                }
                else
                {
                    device_IPAddress = "";
                }
                updateDeviceConnection();
                Thread.Sleep(1000);
            }
        }

        delegate void delVoidNoParam();
        private void updateDeviceConnection()
        {
            if (this.InvokeRequired)
            {
                delVoidNoParam del = new delVoidNoParam(updateDeviceConnection);
                this.Invoke(del);
            }
            else
            {
                if (deviceList.Count == 0)
                {
                    this.txtDeviceConnetion.Text = "Disconnected";
                    this.txtDeviceConnetion.BackColor = Color.Crimson;
                }
                else if (IsDeviceOffline)
                {
                    this.txtDeviceConnetion.Text = "Offline";
                    this.txtDeviceConnetion.BackColor = Color.Gray;
                }
                else if (deviceList.Count == 1 && !IsDeviceOffline)
                {
                    this.txtDeviceConnetion.Text = "Conncted";
                    this.txtDeviceConnetion.BackColor = Color.Green;
                }
                else
                {
                    this.txtDeviceConnetion.Text = "More than 1 devices";
                    this.txtDeviceConnetion.BackColor = Color.Orange;
                }
                txtIPAddress.Visible = device_IPAddress.Length > 0;
                if (txtIPAddress.Visible)
                {
                    txtIPAddress.Text = device_IPAddress;
                }
            }
        }

        private void saveDefaultPlatform()
        {
            XElement xeDefaultSettings = null, xeCommon = null, xeDefaultPlatform = null;
            if (File.Exists(DefaultSettingsPath))
            {
                try
                {
                    xeDefaultSettings = XElement.Load(DefaultSettingsPath);
                }
                catch
                {
                }
            }
            if (xeDefaultSettings == null)
            {
                xeDefaultSettings = new XElement("DefaultSettings");
            }
            try
            {
                xeCommon = xeDefaultSettings.Element("Common");
            }
            catch { }
            if (xeCommon == null)
            {
                xeCommon = new XElement("Common");
                xeDefaultSettings.Add(xeCommon);
            }
            try
            {
                xeDefaultPlatform = xeCommon.Element("DefaultPlatform");
            }
            catch { }
            if (xeDefaultPlatform == null)
            {
                xeDefaultPlatform = new XElement("DefaultPlatform");
                xeCommon.Add(xeDefaultPlatform);
            }
            xeDefaultPlatform.Value = platform;
            xeDefaultSettings.Save(DefaultSettingsPath);
        }

        private void saveDefaultSettings()
        {
            XElement xeDefaultSettings = null, xeInstall = null, xeInitial = null, xeResult = null,
                             xePlatform = null, xeFilterKeyword = null, xeTestResultFilter = null, xeConfigSourceFolder = null,
                             xeProfilePath=null,xeAttachmentSourceFolder=null,xeAttachmentDestinationFolder=null,
                             xeInitial_ScriptFolder = null, xeLogFolder=null,xeMaxLogInterval = null;
            #region XeDefaultSettings
            if (File.Exists(DefaultSettingsPath))
            {
                try
                {
                    xeDefaultSettings = XElement.Load(DefaultSettingsPath);
                }
                catch
                {
                }
            }
            if (xeDefaultSettings == null)
            {
                xeDefaultSettings = new XElement("DefaultSettings");
            }
            #endregion XeDefaultSettings
            #region xeInstall
            try
            {
                xeInstall = xeDefaultSettings.Element("Install");
            }
            catch { }
            if (xeInstall == null)
            {
                xeInstall = new XElement("Install");
                xeDefaultSettings.Add(xeInstall);
            }
            try
            {
                xeInstall = xeDefaultSettings.Element("Install");
            }
            catch { }
            if (xeInstall == null)
            {
                xeInstall = new XElement("Install");
                xeDefaultSettings.Add(xeInstall);
            }
            #region xeInstall.xePlatform
            try
            {
                 xePlatform = xeInstall.ElementByAttribute("Platform", "name", platform);
                 if (xePlatform == null)
                {
                     xePlatform=new XElement("Platform");
                     xeInstall.Add(xePlatform);
                 }
                 xePlatform.SetAttributeValue("name", platform);
            }
            catch { }
            #region xeInstall.xePlatform.xeProfilePath
            try
            {
                xeProfilePath = xePlatform.Element("ProfilePath");
                if (xeProfilePath == null)
                {
                    xeProfilePath = new XElement("ProfilePath");
                    xePlatform.Add(xeProfilePath);
                }
                xeProfilePath.Value = FormToolSettings.ProfileApplication_Path;
            }
            catch { }
            #endregion xeInstall.xePlatform.xeProfilePath
            #region xeInstall.xePlatform.xeAttachmentSource
            try
            {
                xeAttachmentSourceFolder = xePlatform.Element("AttachmentSource");
                if (xeAttachmentSourceFolder == null)
                {
                    xeAttachmentSourceFolder = new XElement("AttachmentSource");
                    xePlatform.Add(xeAttachmentSourceFolder);
                }
                xeAttachmentSourceFolder.Value = FormToolSettings.TRS_AttachmentSourceFolder;
            }
            catch { }
            #endregion xeInstall.xePlatform.xeAttachmentSource
            #region xeInstall.xePlatform.xeAttachmentDestination
            try
            {
                xeAttachmentDestinationFolder = xePlatform.Element("AttachmentDestination");
                if (xeAttachmentDestinationFolder == null)
                {
                    xeAttachmentDestinationFolder = new XElement("AttachmentDestination");
                    xePlatform.Add(xeAttachmentDestinationFolder);
                }
                xeAttachmentDestinationFolder.Value = FormToolSettings.TRS_AttachmentDestinationFolder;
            }
            catch { }
            #endregion xeInstall.xePlatform.xeAttachmentDestination
            #region xeInstall.xePlatform.xeConfigSourceFolder
            try
            {
                xeConfigSourceFolder = xePlatform.Element("ConfigSourceFolder");
                if (xeConfigSourceFolder == null)
                {
                    xeConfigSourceFolder = new XElement("ConfigSourceFolder");
                    xePlatform.Add(xeConfigSourceFolder);
                }
                xeConfigSourceFolder.Value = FormToolSettings.TRS_ConfigSourceFolder;
            }
            catch { }
            #endregion xeInstall.xePlatform.xeConfigSourceFolder
            #region xeInstall.xePlatform.xeFilterKeyword
            try
            {
                xeFilterKeyword = xePlatform.Element("FilterKeyword");
                if (xeFilterKeyword == null)
                {
                    xeFilterKeyword = new XElement("FilterKeyword");
                    xePlatform.Add(xeFilterKeyword);
                }
                xeFilterKeyword.SetAttributeValue("enable", FormToolSettings.KeywordFilterEnable.ToString());
                xeFilterKeyword.Value = FormToolSettings.KeywordFilter_Text;
            }
            catch { }
            #endregion xeInstall.xePlatform.xeFilterKeyword
            #region xeInstall.xePlatform.xeTestResultFilter
            try
            {
                xeTestResultFilter = xePlatform.Element("TestResultFilter");
                if (xeTestResultFilter == null)
                {
                    xeTestResultFilter = new XElement("TestResultFilter");
                    xePlatform.Add(xeTestResultFilter);
                }
                xeTestResultFilter.Value = FormToolSettings.TestResultFilter.ToString();
            }
            catch { }
            #endregion xeInstall.xePlatform.xeTestResultFilter
            #endregion xeInstall.xePlatform
            #endregion xeInstall
            #region xeInitial
            try
            {
                xeInitial = xeDefaultSettings.Element("Initial");
            }
            catch { }
            if (xeInitial == null)
            {
                xeInitial = new XElement("Initial");
                xeDefaultSettings.Add(xeInitial);
            }
            #region xeInitial.ScriptFolder
            try
            {
                xeInitial_ScriptFolder = xeInitial.Element("ScriptFolder");
            }
            catch { }
            if (xeInitial_ScriptFolder == null)
            {
                xeInitial_ScriptFolder = new XElement("ScriptFolder");
                xeInitial.Add(xeInitial_ScriptFolder);
            }
            xeInitial_ScriptFolder.Value = FormToolSettings.Pre_ConditionScriptFolder;
            #endregion xeInitial.ScriptFolder
            #endregion xeInitial
            #region xeResult
            try
            {
                xeResult = xeDefaultSettings.Element("Result");
            }
            catch { }
            if (xeResult == null)
            {
                xeResult = new XElement("Result");
                xeDefaultSettings.Add(xeResult);
            }
            #region xeResult.xeLogFolder
            try
            {
                xeLogFolder = xeResult.Element("LogFolder");
            }
            catch { }
            if (xeLogFolder == null)
            {
                xeLogFolder = new XElement("LogFolder");
                xeResult.Add(xeLogFolder);
            }
            xeLogFolder.Value = FormToolSettings.LogFolder;
            #endregion xeResult.xeLogFolder
            #region xeResult.xeMaxLogInterval
            try
            {
                xeMaxLogInterval = xeResult.Element("MaxLogInterval");
            }
            catch { }
            if (xeMaxLogInterval == null)
            {
                xeMaxLogInterval = new XElement("MaxLogInterval");
                xeResult.Add(xeMaxLogInterval);
            }
            xeMaxLogInterval.Value = FormToolSettings.MaxLogInterval.ToString();
            #endregion xeResult.xeMaxLogInterval
            #endregion xeResult
            xeDefaultSettings.Save(DefaultSettingsPath);
        }

        private void loadDefaultSettings()
        {
            XElement xeDefaultSettings = null;
            try
            {
                xeDefaultSettings = XElement.Load(DefaultSettingsPath);
                #region DefaultPlatform
                XElement xeDefaultPlatform = xeDefaultSettings.Element("Common").Element("DefaultPlatform");
                if (xeDefaultPlatform != null && xeDefaultPlatform.Value != null)
                {
                    platform = xeDefaultPlatform.Value;
                    switch (xeDefaultPlatform.Value.ToLower())
                    {
                        case "android":
                            cmbPlatform.SelectedIndex = 1;
                            break;
                        case "ce":
                            cmbPlatform.SelectedIndex = 0;
                            break;
                    }
                }
                #endregion DefaultPlatform

                #region TRS reader
                XElement xeInstall = xeDefaultSettings.Element("Install");
                XElement xePlatform = xeInstall.ElementByAttribute("Platform", "name", platform);
                XElement xeAttachmentSourceFolder = null, xeAttachmentDestinationFolder = null;
                if (xePlatform != null)
                {
                    //TestPlan default folder
                    String testPlanDefaultFolder = xePlatform.Element("TestPlanFolder").Value;
                    #region ConfigSourceFolder
                    try
                    {
                        XElement xeConfigSourceFolder = xePlatform.Element("ConfigSourceFolder");
                        FormToolSettings.TRS_ConfigSourceFolder = xeConfigSourceFolder.Value;
                    }
                    catch
                    {
                        FormToolSettings.TRS_ConfigSourceFolder = "";
                        //Fail to load settings item ,just ignore and read next one. 
                    }
                    #endregion ConfigSourceFolder
                    #region ProfileApplication
                    try
                    {
                        XElement xeProfilePath = xePlatform.Element("ProfilePath");
                        FormToolSettings.ProfileApplication_Path = xeProfilePath.Value;
                    }
                    catch
                    {
                        FormToolSettings.ProfileApplication_Path = "";
                        //Fail to load settings item ,just ignore and read next one. 
                    }
                    #endregion ProfileApplication
                    #region Filter keyword
                    try
                    {
                        XElement xeFilterKeyword = xePlatform.Element("FilterKeyword");
                        FormToolSettings.KeywordFilterEnable = xeFilterKeyword.Attribute("enable").Value.ToLower().Equals("true");
                        FormToolSettings.KeywordFilter_Text = xeFilterKeyword.Value;
                    }
                    catch
                    {
                        FormToolSettings.KeywordFilter_Text = "";
                        //Fail to load settings item ,just ignore and read next one. 
                    }
                    #endregion Filter keyword
                    #region TestResultFilter
                    try
                    {
                        XElement xeTestResultFilter = xePlatform.Element("TestResultFilter");
                        int filter = Convert.ToInt32(xeTestResultFilter.Value);
                        FormToolSettings.TestResultFilter = filter;
                    }
                    catch
                    {
                        FormToolSettings.TestResultFilter = 0x11111;
                        //Fail to load settings item ,just ignore and read next one. 
                    }
                    #endregion TestResultFilter
                    #region AttachmentSourceFolder
                    try
                    {
                        xeAttachmentSourceFolder = xePlatform.Element("AttachmentSource");
                        FormToolSettings.TRS_AttachmentSourceFolder = xeAttachmentSourceFolder.Value;
                    }
                    catch
                    {
                        FormToolSettings.TRS_AttachmentSourceFolder = "";
                        //Fail to load settings item ,just ignore and read next one. 
                    }
                    #endregion AttachmentSourceFolder
                    #region AttachmentDestinationFolder
                    try
                    {
                        xeAttachmentDestinationFolder = xePlatform.Element("AttachmentDestination");
                        FormToolSettings.TRS_AttachmentDestinationFolder = xeAttachmentDestinationFolder.Value;
                    }
                    catch
                    {
                        FormToolSettings.TRS_AttachmentDestinationFolder = "";
                        //Fail to load settings item ,just ignore and read next one. 
                    }
                    #endregion AttachmentDestinationFolder
                    #region QuickEditor Items
                    FormConfigEditor.ClearItems();
                    XElement xeQuickEditor = xePlatform.Element("QuickEditor");
                    foreach (XElement xeItem in xeQuickEditor.Elements())
                    {
                        try
                        {
                            String key = xeItem.Attribute("keyword").Value;
                            String value = xeItem.Value;
                            FormConfigEditor.AddItem(key, value);
                        }
                        catch
                        {
                            //Fail to load settings item ,just ignore and read next one. 
                        }
                    }
                    #endregion QuickEditor Items                }
                #endregion TRS reader

                    #region Pre-condition Setup
                    #region Pre_ConditioScriptFolder
                    try
                    {
                        XElement xeScriptFolder = xeDefaultSettings.Element("Initial").Element("ScriptFolder");
                        if (xeScriptFolder != null && xeScriptFolder.Value != null)
                        {
                            FormToolSettings.Pre_ConditionScriptFolder = xeScriptFolder.Value;
                        }
                    }
                    catch
                    {
                        //Fail to load settings item ,just ignore and read next one. 
                    }
                    #endregion Pre_ConditioScriptFolder
                    #endregion Pre-condition Setup

                    #region Test Result
                    try
                    {
                        XElement xeTestResult = xeDefaultSettings.Element("Result");
                        #region LogFolder
                        XElement xeLogFolder = xeTestResult.Element("LogFolder");
                        if (xeLogFolder.Value != null)
                        {
                            FormToolSettings.LogFolder = xeLogFolder.Value;
                        }
                        #endregion LogFolder
                        #region Max Log Interval
                        XElement xeMaxLogInterval = xeTestResult.Element("MaxLogInterval");
                        if (xeMaxLogInterval.Value != null)
                        {
                            FormToolSettings.MaxLogInterval = Convert.ToInt32(xeMaxLogInterval.Value);
                        }
                        #endregion Max Log Interval
                    }
                    catch
                    {
                        //Fail to load settings item ,just ignore and read next one. 
                    }
                    #endregion Test Result
                }
            }
            catch
            {
                //Fail to load settings item ,just ignore. 
            }
        }

        #region UI control
        private void cmbPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            String currentSelectPlatform = "";
            if (cmbPlatform.Text == "Win CE")
            {
                currentSelectPlatform = "CE";
            }
            else if (cmbPlatform.Text == "Android")
            {
                currentSelectPlatform = "Android";
            }
            if (currentSelectPlatform != platform) //Check the last platform to avoid  redundancy operation.
            {
                platform = currentSelectPlatform;
                saveDefaultPlatform();
                loadDefaultSettings();
                if (SelectedPlatformChangedEventHandler != null)
                {
                    SelectedPlatformChangedEventHandler(this, new EventArgs());
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String helperChmPath = System.AppDomain.CurrentDomain.BaseDirectory + "StressTestGuide_Helper.chm";

            try
            {
                Help.ShowHelp(this, "file://"+helperChmPath);
            }
            catch (Exception ex)
            { 
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(FormToolSettings.Show(platform).Equals(DialogResult.OK))
            {
                saveDefaultSettings();
                if (ToolSettingsChangedEventHandler != null)
                {
                    ToolSettingsChangedEventHandler(this, new EventArgs());
                }
            }
        }

        private void menuActionSelected(object sender, EventArgs e)
        {
            UserControl usrctl = null;
            pnlMain.Controls.Clear();
            menuInitial.Checked = false;
            menuTRSReader.Checked = false;
            menuTestResult.Checked = false;
            menuDataMaintainance.Checked = false;
            ToolStripMenuItem mi = sender as ToolStripMenuItem;
            if (mi.Equals(menuTRSReader))
            {
                if (frmInstall == null)
                {
                    frmInstall = new FormInstall(this);
                }
                usrctl = frmInstall;
                menuTRSReader.Checked = true;
            }
            else if (mi.Equals(menuInitial))
            {
                if (platform.ToLower().Equals("android"))
                {
                    if (frmInitial == null)
                    {
                        frmInitial = new FormInitial(this);
                    }
                    usrctl = frmInitial;
                }
                else
                {
                    MessageBox.Show("Only Android devices are supported now.");
                }
                menuInitial.Checked = true;
            }
            else if (mi.Equals(menuTestResult))
            {
                if (frmTestResult == null)
                {
                    frmTestResult = new FormTestResult(this);
                }
                usrctl = frmTestResult;
                menuTestResult.Checked = true;
            }
            else if (mi.Equals(menuDataMaintainance))
            {
                if (frmDataMaintainance == null)
                {
                    frmDataMaintainance = new FormDataMaintaince(this);
                }
                usrctl = frmDataMaintainance;
                menuDataMaintainance.Checked = true;
            }
            if (usrctl != null)
            {
                pnlMain.Controls.Add(usrctl);
                usrctl.Dock = DockStyle.Fill;
                usrctl.Show();
            }
        }

        #endregion UI control        



    }
}
