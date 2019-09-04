using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;
using dev.jerry_h.pc_tools.AndroidLibrary;
using System.Threading;
using System.IO;
using SharpSvn;

namespace com.usi.shd1_tools.RobotframeworkTestGuide
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
        internal static String currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
        internal static String configPath = currentDir + "settings.xml";
        private String svn_address = "192.168.2.101";  //Default value, could be changed via config.xml
        //private String svn_address = "220.134.174.191";
        private const String svn_user = "SHD1_User";
        private const String svn_password = "usi#1234";
        private static String strEmdk_folderPath = "C:\\EMDKScanner\\";
        private static String strEmdk_RfsFolderPath = strEmdk_folderPath + "RFS";
        public const String EMDKScanner_VariableName = "VariableFiles\\Android\\EMDKScanner\\Scanner_EMDK_Var.yaml";
        public const String RobotListenerConfigName = "ExternalFiles\\Common\\RobotListener\\Config_RobotListener.yaml";
        private String svn_testPackagesUrl
        {
            get
            {
                String fullAddress = "";
                fullAddress += "https://";
                fullAddress += svn_address;
                fullAddress += "/svn/com.usi.shd1_tools/AutomationTestPackages";
                return fullAddress;
            }
        }
        private String scriptFolder = "";
        private String testResultFolder = "";
        private String pybotPath = "";
        private bool isInitialized = false;
        private bool runFlag = false;
        private Thread tdDeviceMonitor = null;
        private int iDeviceMonitorInterval = 6000;
        private List<AdbDeviceInfomation> lstConnectedDevs = new List<AdbDeviceInfomation>();
        private Dictionary<String, int> dicSelectedDevs = new Dictionary<string, int>();
        private SvnClient svnc;
        private bool __DONOTUSE_isDownloading__ = false;
        private bool isDownloading
        {
            get
            {
                return __DONOTUSE_isDownloading__;
            }
            set
            {
                __DONOTUSE_isDownloading__ = value;
                __DONOTUSE_showDownloadingMessage__();
            }

        }
        private void __DONOTUSE_showDownloadingMessage__()
        {
            if (this.InvokeRequired)
            {
                delRefreshUI del = new delRefreshUI(__DONOTUSE_showDownloadingMessage__);
                this.Invoke(del);
            }
            else
            {
                if (__DONOTUSE_isDownloading__)
                {
                    tslDownloading.Visible = true;
                    clearNotification1();
                }
                else
                {
                    tslDownloading.Visible = false;
                }
            }
        }
        private runningModes runningMode = runningModes.EMDKScanner_Mode;
        private enum runningModes
        {
            Robotframework_Mode = 0,
            EMDKScanner_Mode = 1
        };
        private String chmFileName = "USI_RobotframeworkTestGuide_Helper.chm";
        List<String> lstEmdkVariableLines = new List<string>();
        List<String> lstRobotListenerConfigLines = new List<string>();
        private String emdkScannerType
        {
            get
            {
                return cbbScannerType.Text;
            }
            set
            {
                cbbScannerType.Text = value;
            }
        }
        private String emdkLegacyID
        {
            get
            {
                return txtLegacyId.Text;
            }
            set
            {
                txtLegacyId.Text = value;
            }
        }
        private String emdkPlatform
        {
            get
            {
                return cbbTestPlatform.Text;
            }
            set
            {
                cbbTestPlatform.Text = value;
            }
        }
        private String emdkDisplayType
        {
            get
            {
                return cbbDisplayType.Text;
            }
            set
            {
                cbbDisplayType.Text = value;
            }
        }
        
        public FormMain()
        {
            InitializeComponent();
            this.Text = Title + " - V" + Version.ToString(3);
            loadConfig();
            initializeNotification1Timer();
            startDeviceMonitor();
            initialSvnClient();
            refreshRunningModeUI();
        }

        private void startDeviceMonitor()
        {
            stopDeviceMonitor();
            runFlag = true;
            tdDeviceMonitor = new Thread(deviceMonitor_Runnable);
            tdDeviceMonitor.Start();
        }

        private void stopDeviceMonitor()
        {
            runFlag = false;
            if (tdDeviceMonitor != null)
            {
                tdDeviceMonitor.Join(iDeviceMonitorInterval);
                tdDeviceMonitor.Abort();
                tdDeviceMonitor = null;
            }
        }

        private void deviceMonitor_Runnable()
        {
            while (runFlag)
            {
                lstConnectedDevs.Clear();
                dicSelectedDevs.Clear();
                dicSelectedDevs.Add("", -1); // -1:Nonselected
                lstConnectedDevs = ADB_Process.GetDeivcesList();
                foreach (AdbDeviceInfomation adi in lstConnectedDevs)
                {
                    if (adi.ConnectingStatus == "Connected")
                    {
                        dicSelectedDevs.Add(adi.ID, -1);
                    }
                }
                refreshAvailableDevices();
                Thread.Sleep(iDeviceMonitorInterval);
            }
        }

        delegate void delRefreshUI();
        private void refreshAvailableDevices()
        {
            if (this.InvokeRequired)
            {
                delRefreshUI del = new delRefreshUI(refreshAvailableDevices);
                this.Invoke(del);
            }
            else
            {
                if (cbbPrimaryDevice.Text.Length > 0 && dicSelectedDevs.ContainsKey(cbbPrimaryDevice.Text))
                {
                    dicSelectedDevs[cbbPrimaryDevice.Text] = 0; //0:Primary device
                }
                if (cbbSecondaryDevice1.Text.Length > 0 && dicSelectedDevs.ContainsKey(cbbSecondaryDevice1.Text))
                {
                    dicSelectedDevs[cbbSecondaryDevice1.Text] = 1; //1:Secondary device #1
                }
                if (cbbSecondaryDevice2.Text.Length > 0 && dicSelectedDevs.ContainsKey(cbbSecondaryDevice2.Text))
                {
                    dicSelectedDevs[cbbSecondaryDevice2.Text] = 2; //2:Secondary device #2
                }
                if (cbbSecondaryDevice3.Text.Length > 0 && dicSelectedDevs.ContainsKey(cbbSecondaryDevice3.Text))
                {
                    dicSelectedDevs[cbbSecondaryDevice3.Text] = 3; //3:Secondary device #3
                }
                cbbPrimaryDevice.Items.Clear();
                cbbSecondaryDevice1.Items.Clear();
                cbbSecondaryDevice2.Items.Clear();
                cbbSecondaryDevice3.Items.Clear();
                foreach (KeyValuePair<String, int> kvp in dicSelectedDevs)
                {
                    switch (kvp.Value)
                    {
                        case -1:
                            cbbPrimaryDevice.Items.Add(kvp.Key);
                            cbbSecondaryDevice1.Items.Add(kvp.Key);
                            cbbSecondaryDevice2.Items.Add(kvp.Key);
                            cbbSecondaryDevice3.Items.Add(kvp.Key);
                            break;
                        case 0:
                            cbbPrimaryDevice.Items.Add(kvp.Key);
                            cbbPrimaryDevice.SelectedItem = kvp.Key;
                            break;
                        case 1:
                            cbbSecondaryDevice1.Items.Add(kvp.Key);
                            cbbSecondaryDevice1.SelectedItem = kvp.Key;
                            break;
                        case 2:
                            cbbSecondaryDevice2.Items.Add(kvp.Key);
                            cbbSecondaryDevice2.SelectedItem = kvp.Key;
                            break;
                        case 3:
                            cbbSecondaryDevice3.Items.Add(kvp.Key);
                            cbbSecondaryDevice3.SelectedItem = kvp.Key;
                            break;
                    }
                }
                if (cbbPrimaryDevice.Items.Count == 2 && cbbPrimaryDevice.SelectedIndex <0)
                {
                    cbbPrimaryDevice.SelectedIndex = 1;
                }
            }
        }

        #region process config.xml
        public void loadConfig()
        {
            if (!File.Exists(configPath))
            {
                initializeConfig();
            }
            try
            {
                XElement xeRoot = XElement.Load(configPath);
                try
                {
                    XElement xeRF = xeRoot.Element("Robotframework");
                    try
                    {
                        XElement xePybot = xeRF.Element("pybot");
                        pybotPath = xePybot.Value;
                    }
                    catch { }
                    try
                    {
                        XElement xeScriptsFolder = xeRF.Element("scripts_folder");
                        if (xeScriptsFolder == null)
                        {
                            xeScriptsFolder = new XElement("scripts_folder");
                        }
                        else
                        {
                            scriptFolder = transToAbsolutelyPath(xeScriptsFolder.Value);
                        }
                    }
                    catch { }
                    try
                    {
                        XElement xeTestResultFolder = xeRF.Element("test_result_folder");
                        if (xeTestResultFolder == null)
                        {
                            xeTestResultFolder = new XElement("test_result_folder");
                        }
                        else
                        {

                            testResultFolder = transToAbsolutelyPath(xeTestResultFolder.Value);
                        }
                    }
                    catch { }
                    try
                    {
                        XElement xeemdk_folder = xeRF.Element("emdk_folder");
                        strEmdk_folderPath = xeemdk_folder.Value;
                    }
                    catch { }
                }
                catch { }
                try
                {
                    XElement xeTool = xeRoot.Element("Tool_Settings");
                    try
                    {
                        XElement xeSvnAddress = xeTool.Element("svn_address");
                        svn_address = xeSvnAddress.Value;
                    }
                    catch { }
                    try
                    {
                        XElement xeMonitorInterval = xeTool.Element("monitor_interval");
                        iDeviceMonitorInterval = Convert.ToInt32(xeMonitorInterval.Value);
                    }
                    catch { }
                    try
                    {
                        XElement xeIsInitialized = xeTool.Element("is_initialized");
                        isInitialized = Convert.ToBoolean(xeIsInitialized.Value);
                    }
                    catch { }
                }
                catch { }
            }
            catch { }
        }

        private void initializeConfig()
        {
            XElement xeRoot = new XElement("root");
            XElement xeRF = getSubElement(ref xeRoot, "Robotframework");
            XElement xePybot = getSubElement(ref xeRF, "pybot");
            XElement xeScriptsFolder = getSubElement(ref xeRF, "scripts_folder");
            XElement xeTestResultFolder = getSubElement(ref xeRF, "test_result_folder");
            XElement xeEmdkFolder = getSubElement(ref xeRF, "emdk_folder");
            xePybot.Value = "C:\\Python27\\Scripts\\pybot.bat";
            xeScriptsFolder.Value = ".\\AutomationTestPackages\\";            
            xeTestResultFolder.Value = ".\\TestReult\\";
            xeEmdkFolder.Value = strEmdk_folderPath;

            XElement xeTool = getSubElement(ref xeRoot, "Tool_Settings");
            XElement xeMonitorInterval = getSubElement(ref xeTool, "monitor_interval");
            XElement xeSvnAddress = getSubElement(ref xeTool, "svn_address");
            XElement xeIsInitialized = getSubElement(ref xeTool, "is_initialized");
            xeMonitorInterval.Value = iDeviceMonitorInterval.ToString();
            xeIsInitialized.Value = false.ToString();
            xeSvnAddress.Value = svn_address;
            xeRoot.Save(FormMain.configPath);
        }

        private void setConfig_IsInitialized()
        {
            XElement xeRoot = XElement.Load(configPath);
            XElement xeTool = getSubElement(ref xeRoot, "Tool_Settings");
            XElement xeIsInitialized = getSubElement(ref xeTool, "is_initialized");
            xeIsInitialized.Value = true.ToString();
            isInitialized = true;
            xeRoot.Save(configPath);
        }
        #endregion process config.xml
                
        private void initialSvnClient()
        {
            svnc = new SvnClient();
            svnc.Authentication.Clear(); // prevents checking cached credentials
            svnc.Authentication.DefaultCredentials = new System.Net.NetworkCredential(svn_user, svn_password);
            svnc.Authentication.SslServerTrustHandlers += delegate(object sender, SharpSvn.Security.SvnSslServerTrustEventArgs sstea)
            {
                sstea.AcceptedFailures = sstea.Failures;
                sstea.Save = true; // Save acceptance to authentication store
            };
            svnc.Notify += svnc_Notify;
            svnc.SvnError += svnc_SvnError;
        }        

        private XElement getSubElement(ref XElement xeParent, String childElementName)
        {
            XElement xeChild = xeParent.Element(childElementName);
            if (xeChild == null)
            {
                xeChild = new XElement(childElementName);
                xeParent.Add(xeChild);
            }
            return xeChild;
        }

        private Thread tdSvn;
        private void start_SyncToSvn()
        {
            if (isInitialized)
            {
                isDownloading = true;
                if (tdSvn != null)
                {
                    tdSvn.Join(1000);
                    tdSvn = null;
                }
                tdSvn = new Thread(update_package_Runnable);
                tdSvn.Start();
            }
            else
            {
                DialogResult dr = MessageBox.Show("It's the first time to synchronize to server, it may spend you a few minutes.", "Synchronization", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dr.Equals(DialogResult.OK))
                {
                    isDownloading = true;
                    if (tdSvn != null)
                    {
                        tdSvn.Join(1000);
                        tdSvn = null;
                    }
                    tdSvn = new Thread(checkout_packages_Runnable);
                    tdSvn.Start();
                }
            }
        }

        private void stop_SyncToSvn()
        {
            if (tdSvn != null)
            {
                isDownloading = false;
                tdSvn.Abort();
                tdSvn = null;
            }
        }

        private void checkout_packages_Runnable()
        {
            svnc.CheckOut(new Uri(svn_testPackagesUrl), scriptFolder);
        }

        private void update_package_Runnable()
        {
            svnc.Update(scriptFolder);
        }

        delegate void delEventHandler(object sender, EventArgs ea);
        private void svnc_SvnError(object sender, EventArgs ea)
        {
            if (this.InvokeRequired)
            {
                delEventHandler del = new delEventHandler(svnc_SvnError);
                this.Invoke(del, sender, ea);
            }
            else
            {
                SvnErrorEventArgs seea = ea as SvnErrorEventArgs;
                isDownloading = false;
                clearNotification1();
                MessageBox.Show("SVN Error:\r\n" + seea.Exception.Message);
            }
        }

        private void svnc_Notify(object sender, EventArgs ea)
        {
            if (this.InvokeRequired)
            {
                delEventHandler del = new delEventHandler(svnc_Notify);
                this.Invoke(del, sender, ea);
            }
            else
            {
                SvnNotifyEventArgs snea = ea as SvnNotifyEventArgs;
                if (snea.Action == SvnNotifyAction.UpdateCompleted)
                {
                    isDownloading = false;
                    setConfig_IsInitialized();
                    MessageBox.Show("Done!");
                }
            }
        }
        
        private String transToAbsolutelyPath(String path)
        {
            String absPath = path;
            if (path.StartsWith("./") || path.StartsWith(".\\"))
            {
                absPath = currentDir + path.Substring(2);
            }
            return absPath;            
        }
                
        private void start_RobotframeworkMode(String priDev, String secDev1,
                                String secDev2, String secDev3,
                                String hostFolder, String testplanpath,
                                String rerunFailed, String workingFolder,
                                String scriptPath)
        {
            Process ps = new Process();
            ps.StartInfo = new ProcessStartInfo(pybotPath);
            ps.StartInfo.WorkingDirectory = workingFolder;
            ps.StartInfo.CreateNoWindow = true;
            ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            ps.StartInfo.RedirectStandardOutput = false;
            ps.StartInfo.UseShellExecute = true;

            String argString = "";            
            if (priDev != null && priDev.Length > 0)
            {
                argString += "-v PRIMARYDEVICE:" + priDev + " ";
            }
            if (secDev1 != null && secDev1.Length > 0)
            {
                argString += "-v SECONDARYDEVICE1:" + secDev1 + " ";
            }
            if (secDev2 != null && secDev2.Length > 0)
            {
                argString += "-v SECONDARYDEVICE2:" + secDev2 + " ";
            }
            if (secDev3 != null && secDev3.Length > 0)
            {
                argString += "-v SECONDARYDEVICE3:" + secDev3 + " ";
            }
            argString += "-v HOSTPATH:" + hostFolder + " ";
            if (testplanpath != null && testplanpath.Length > 0)
            {
                argString += "-v TESTPLANPATH:" + testplanpath + " ";
            }
            if (rerunFailed != null && rerunFailed.Length > 0)
            {
                argString += "--rerunfailed " + rerunFailed + " ";
            }
            argString += scriptPath;
            ps.StartInfo.Arguments = argString;
            //MessageBox.Show(argString);
            ps.Start();
        }

        private void start_EmdkScannerMode(bool isRerun)
        {
            Process ps = new Process();
            //ps.StartInfo = new ProcessStartInfo(strEmdk_folderPath +"RobotTestRunner.bat");
            //ps.StartInfo.WorkingDirectory = strEmdk_folderPath;
            String pythonScriptsFolder = Directory.GetParent(pybotPath).ToString();
            ps.StartInfo = new ProcessStartInfo(pythonScriptsFolder +"\\scannerassist.bat");
            ps.StartInfo.WorkingDirectory = strEmdk_RfsFolderPath;
            ps.StartInfo.CreateNoWindow = true;
            ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            ps.StartInfo.RedirectStandardOutput = false;
            ps.StartInfo.UseShellExecute = true;
            String argString = "--run "+strEmdk_RfsFolderPath;
            if(isRerun)
            {
                argString += " -r";
            }
            else
            {
                argString += " -f";
            }
            ps.StartInfo.Arguments = argString;
            ps.Start();            
        }

        private void cleanup_Scripts()
        {
            if (MessageBox.Show("The scripts folder:[" + scriptFolder + "] will be removed,\r\n do you really want to do this?",
                           "Deleting warning...",
                           MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Warning).Equals(DialogResult.OK))
            {
                try
                {
                    Directory.Delete(scriptFolder, true);
                    MessageBox.Show("Done!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    XElement xeRoot = XElement.Load(configPath);
                    XElement xeTool = xeRoot.Element("Tool_Settings");
                    XElement xeIsInitialized = xeTool.Element("is_initialized");
                    xeIsInitialized.Value = false.ToString();
                    xeRoot.Save(configPath);
                }
                catch { }
                isInitialized = false;
            }      
        }

        private void mergeTestResult(String resultFolder)
        {
            List<String> lstOutputs = getOutputResultList(folderBrowserDialog1.SelectedPath+"\\");
            String rebotPath = Path.GetDirectoryName(pybotPath) + "\\rebot.bat";
            if (File.Exists(rebotPath))
            {
                if (lstOutputs.Count > 0)
                {
                    Process ps = new Process();
                    ps.StartInfo = new ProcessStartInfo(rebotPath);
                    ps.StartInfo.WorkingDirectory = resultFolder;
                    ps.StartInfo.CreateNoWindow = true;
                    ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    ps.StartInfo.RedirectStandardOutput = false;
                    ps.StartInfo.UseShellExecute = true;
                    String strArgs = "--merge ";
                    foreach (string output in lstOutputs)
                    {
                        strArgs += output + " ";
                    }
                    ps.StartInfo.Arguments = strArgs;
                    ps.Start();
                }
                else
                {
                    MessageBox.Show("No output.xml file found!!");
                }
            }
            else
            {
                MessageBox.Show("[rebot.bat] does not exist:\r\nPlease check your installation of Robotframework,\r\n and try again!");
            }
        }

        private List<String> getOutputResultList(String folder)
        {
            List<String> lstResults = new List<string>();
            foreach (String file in Directory.GetFiles(folder, "output.xml"))
            {
                lstResults.Add(file);
            }
            foreach (String subFolder in Directory.GetDirectories(folder))
            {
                lstResults.AddRange(getOutputResultList(subFolder).ToArray());
            }
            return lstResults;
        }

        private void readEmdkConfigurations()
        {
            StreamReader sr = null;
            #region EmdkVariables
            try
            {
                sr = new StreamReader(strEmdk_RfsFolderPath + "\\" + EMDKScanner_VariableName);
                lstEmdkVariableLines.Clear();
                String line = "";
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    lstEmdkVariableLines.Add(line);
                    if (line.StartsWith("LEGACY_ID:"))
                    {
                        emdkLegacyID = line.Replace("LEGACY_ID:", "").Trim();
                    }
                    else if (line.StartsWith("SCANNER_TYPE"))
                    {
                        emdkScannerType = line.Replace("SCANNER_TYPE:", "").Trim();
                    }
                    else if (line.StartsWith("TEST_PLATFORM:"))
                    {
                        emdkPlatform = line.Replace("TEST_PLATFORM:", "").Trim();
                    }
                    else if (line.StartsWith("BARCODE_DISPLAY_TYPE:"))
                    {
                        emdkDisplayType = line.Replace("BARCODE_DISPLAY_TYPE:", "").Trim();
                    }
                }
            }
            catch
            {

            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            #endregion EmdkVariables
            #region RobotListenerConfig
            sr = null;
            try
            {
                sr = new StreamReader(strEmdk_RfsFolderPath + "\\" + RobotListenerConfigName);
                lstRobotListenerConfigLines.Clear();
                String strLine = "";
                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    if(strLine.StartsWith("#Result_To_File"))
                    {
                        strLine = "Result_To_File: True"; //Set Result_To_File falg always on
                    }
                    else if (strLine.StartsWith("#Update_Results_In_TRS"))
                    {
                        strLine = strLine.TrimStart('#');//Set Update_Results_In_TRS falg always on
                    }
                    lstRobotListenerConfigLines.Add(strLine);
                }
            }
            catch
            {

            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
            #endregion RobotListenerConfig
        }

        private void saveEmdkConfigurations()
        {
            # region update EmdkVariables
            for (int i = 0; i < lstEmdkVariableLines.Count; i++)
            {
                if (lstEmdkVariableLines[i].StartsWith("DUT1")||
                    lstEmdkVariableLines[i].StartsWith("SCANNER_DUT_ID"))
                {
                    lstEmdkVariableLines[i] = "DUT1: " + cbbPrimaryDevice.Text;
                }
                else if (lstEmdkVariableLines[i].StartsWith("LEGACY_ID"))
                {
                    lstEmdkVariableLines[i] = "LEGACY_ID: " + emdkLegacyID;
                }
                else if (lstEmdkVariableLines[i].StartsWith("SCANNER_TYPE"))
                {
                    lstEmdkVariableLines[i] = "SCANNER_TYPE: " + emdkScannerType;
                }
                else if (lstEmdkVariableLines[i].StartsWith("TEST_PLATFORM"))
                {
                    lstEmdkVariableLines[i] = "TEST_PLATFORM: " + emdkPlatform;
                }
                else if (lstEmdkVariableLines[i].StartsWith("BARCODE_DISPLAY_TYPE"))
                {
                    lstEmdkVariableLines[i] = "BARCODE_DISPLAY_TYPE: " + emdkDisplayType;
                }
            }
            # endregion update EmdkVariables
            #region EmdkVariables
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(strEmdk_RfsFolderPath + "\\" + EMDKScanner_VariableName, false);
                foreach (String line in lstEmdkVariableLines)
                {
                    sw.WriteLine(line);
                }
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            #endregion write emdk file
            # region update Robot Listener Config
            for (int i = 0; i < lstRobotListenerConfigLines.Count; i++)
            {
                if (lstRobotListenerConfigLines[i].StartsWith("DUT_ID"))
                {
                    lstRobotListenerConfigLines[i] = "DUT_ID: " + cbbPrimaryDevice.Text;
                }
                else if (lstRobotListenerConfigLines[i].StartsWith("Update_Results_In_TRS"))
                {
                    lstRobotListenerConfigLines[i] = "Update_Results_In_TRS: " + txtEs_TestPlanPath.Text;
                }                
            }
            # endregion update Robot Listener Config
            #region write robotlistener config
            sw = null;
            try
            {
                sw = new StreamWriter(strEmdk_RfsFolderPath + "\\" + RobotListenerConfigName, false);
                foreach (String line in lstRobotListenerConfigLines)
                {
                    sw.WriteLine(line);
                }
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            #endregion write robotlistener config
        }
        
        #region UI Controller

        private void ckbSecondaryDevice1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbb = sender as CheckBox;
            cbbSecondaryDevice1.Enabled = cbb.Checked;
            if (!cbb.Checked)
            {
                cbbSecondaryDevice1.Items.Clear();
            }
        }

        private void ckbSecondaryDevice2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbb = sender as CheckBox;
            cbbSecondaryDevice2.Enabled = cbb.Checked;
            if (!cbb.Checked)
            {
                cbbSecondaryDevice2.Items.Clear();
            }
        }

        private void ckbSecondaryDevice3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbb = sender as CheckBox;
            cbbSecondaryDevice3.Enabled = cbb.Checked;
            if (!cbb.Checked)
            {
                cbbSecondaryDevice3.Items.Clear();
            }
        }

        private void ckbRerunFailed_CheckedChanged(object sender, EventArgs e)
        {
            txtRf_RerunFailed.Visible = ckbRf_RerunFailed.Checked;
            if (ckbRf_RerunFailed.Checked)
            {
                openFileDialog1.Filter = "Output File|*.xml";
                if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
                {
                    txtRf_RerunFailed.Text = openFileDialog1.FileName;
                }
                else
                {
                    ckbRf_RerunFailed.Checked = false;
                }
            }
        }

        private void ckbTestPlanPath_CheckedChanged(object sender, EventArgs e)
        {
            txtRf_TestPlanPath.Visible = ckbRf_TestPlanPath.Checked;
            if (ckbRf_TestPlanPath.Checked)
            {
                openFileDialog1.Filter = "Excel File|*.xlsx";
                if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
                {
                    txtRf_TestPlanPath.Text = openFileDialog1.FileName;
                }
                else
                {
                    ckbRf_TestPlanPath.Checked = false;
                }
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            cbbPrimaryDevice.Width = gpbDevices.Width - cbbPrimaryDevice.Location.X - 2 * this.DefaultMargin.Right;
            cbbSecondaryDevice1.Width = gpbDevices.Width - cbbSecondaryDevice1.Location.X - 2 * this.DefaultMargin.Right;
            cbbSecondaryDevice2.Width = gpbDevices.Width - cbbSecondaryDevice2.Location.X - 2 * this.DefaultMargin.Right;
            cbbSecondaryDevice3.Width = gpbDevices.Width - cbbSecondaryDevice3.Location.X - 2 * this.DefaultMargin.Right;
            txtRf_ScrpitPath.Width = btnScriptBrowser.Location.X - txtRf_ScrpitPath.Location.X - 2 * this.DefaultMargin.Right;
            txtRf_RerunFailed.Width = btnRf_Run.Location.X - txtRf_RerunFailed.Location.X - 2 * this.DefaultMargin.Right;
            txtRf_TestPlanPath.Width = btnRf_Run.Location.X - txtRf_TestPlanPath.Location.X - 2 * this.DefaultMargin.Right;
            txtRf_OtherArguments.Width = btnRf_Run.Location.X - txtRf_OtherArguments.Location.X - 2 * this.DefaultMargin.Right;            
        }

        private void btnScriptBrowser_Click(object sender, EventArgs e)
        {
            String strFolder = "";
            if(runningMode == runningModes.EMDKScanner_Mode)
            {
                strFolder = "C:\\EMDKScanner";
            }
            else if(runningMode == runningModes.Robotframework_Mode)
            {
                strFolder = scriptFolder;
            }
            if (Directory.Exists(strFolder))
            {
                if (FormScriptTree.Display(strFolder).Equals(DialogResult.OK))
                {
                    txtRf_ScrpitPath.Text = FormScriptTree.SelectedScript;
                }
            }
            else
            {
                MessageBox.Show("Could not find executable scripts, please sync scripts folder first!");
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            stopDeviceMonitor();
            stop_SyncToSvn();
            this.Cursor = Cursors.Default;
        }

        private void toolSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormSettings.Display().Equals(DialogResult.OK))
            {
                loadConfig();
            }
        }

        private void ckbRerunFailed_CheckedChanged_1(object sender, EventArgs e)
        {
            if (runningMode == runningModes.Robotframework_Mode)
            {
                txtRf_RerunFailed.Visible = ckbRf_RerunFailed.Checked;
                if (ckbRf_RerunFailed.Checked)
                {
                    openFileDialog1.Filter = "Output XML File|*.xml";
                    if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
                    {
                        txtRf_RerunFailed.Text = openFileDialog1.FileName;
                    }
                    else
                    {
                        ckbRf_RerunFailed.Checked = false;
                    }
                }
            }
            else if (runningMode == runningModes.EMDKScanner_Mode)
            {
                txtRf_RerunFailed.Visible = false;
            }
        }

        private void ckbOtherArguments_CheckedChanged(object sender, EventArgs e)
        {
            txtRf_OtherArguments.Visible = ckbRf_OtherArguments.Checked;
        }

        private void syncScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                start_SyncToSvn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cleanUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cleanup_Scripts();
        }

        #region Notification1
        private System.Windows.Forms.Timer tmrNotificationBlink1;
        private System.Windows.Forms.Timer tmrNotificationTimer1;
        private void initializeNotification1Timer()
        {
            tmrNotificationBlink1 = new System.Windows.Forms.Timer();
            tmrNotificationBlink1.Interval = 1000;
            tmrNotificationBlink1.Tick += tmrNotificationBlink1_Tick;
            tmrNotificationTimer1 = new System.Windows.Forms.Timer();
            tmrNotificationTimer1.Interval = 10000;
            tmrNotificationTimer1.Tick += tmrNotificationTimer1_Tick;
        }

        private delegate void delNotification(String message, int duration_inSeconds, Color foreColor, bool blink);
        private void showNotification1(String message, int duration_inSeconds, Color foreColor, bool blink)
        {
            if (this.InvokeRequired)
            {
                delNotification del = new delNotification(showNotification1);
                this.Invoke(del, message, duration_inSeconds, foreColor, blink);
            }
            else
            {
                tslNotification1.Visible = true;
                tslNotification1.ForeColor = foreColor;
                tslNotification1.Text = message;
                if (blink)
                {
                    tmrNotificationBlink1.Start();
                }
                if (duration_inSeconds > 0)
                {
                    tmrNotificationTimer1.Interval = duration_inSeconds * 1000;
                    tmrNotificationTimer1.Start();
                }
            }
        }

        private void showNotification1(String message, int duration_inSeconds)
        {
            showNotification1(message, duration_inSeconds, Color.Black, false);
        }

        private void tmrNotificationTimer1_Tick(Object sender, EventArgs ea)
        {
            tslNotification1.Text = "";
            if (tmrNotificationBlink1 != null && !tmrNotificationBlink1.Enabled)
            {
                tmrNotificationBlink1.Stop();
            }
            if (tmrNotificationTimer1 != null && tmrNotificationTimer1.Enabled)
            {
                tmrNotificationTimer1.Stop();
            }
        }

        private void tmrNotificationBlink1_Tick(Object sender, EventArgs ea)
        {
            tslNotification1.Visible = !tslNotification1.Visible;
        }

        private void clearNotification1()
        {
            tslNotification1.Text = "";
            if (tmrNotificationBlink1 != null && !tmrNotificationBlink1.Enabled)
            {
                tmrNotificationBlink1.Stop();
            }
            if (tmrNotificationTimer1 != null && tmrNotificationTimer1.Enabled)
            {
                tmrNotificationTimer1.Stop();
            }
        }

        #endregion Notification1

        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                mergeTestResult(folderBrowserDialog1.SelectedPath+"\\");
            }
        }

        private void btnRfRun_Click(object sender, EventArgs e)
        {
            String strErrorMsg = "";
            String strScriptPath = txtRf_ScrpitPath.Text;
            if (strScriptPath.Length == 0)
            {
                strErrorMsg += "* " + "Select a script to run.";
            }
            if (runningMode == runningModes.Robotframework_Mode)
            {
                #region Working space & Rerun failed
                String strTimeStamp = DateTime.Now.ToString("yyyyMMdd_HHmm");
                String strWorkingSpace = "";
                String strRefunFailed = "";
                if (ckbRf_RerunFailed.Checked)
                {
                    strRefunFailed = txtRf_RerunFailed.Text;
                    strWorkingSpace = Path.GetDirectoryName(strRefunFailed) + "\\";
                }
                else
                {
                    strWorkingSpace = testResultFolder;
                }
                strWorkingSpace += strTimeStamp + "\\";
                Directory.CreateDirectory(strWorkingSpace);
                #endregion Working space & Rerun failed
                #region Devices
                String strPrimaryDevice = cbbPrimaryDevice.Text;
                String strSecondaryDevice1 = "";
                String strSecondaryDevice2 = "";
                String strSecondaryDevice3 = "";
                if (strPrimaryDevice.Length == 0)
                {
                    if (ckbSecondaryDevice1.Checked |
                       ckbSecondaryDevice2.Checked |
                       ckbSecondaryDevice3.Checked)
                    {
                        strErrorMsg += "* " + "Please select the Primary Device" + "\r\n";
                    }
                }
                if (ckbSecondaryDevice1.Checked)
                {
                    strSecondaryDevice1 = cbbSecondaryDevice1.Text;
                    if (strSecondaryDevice1.Length == 0)
                    {
                        strErrorMsg += "* " + "Please select the Secondary Device1" + "\r\n";
                    }
                }
                if (ckbSecondaryDevice2.Checked)
                {
                    strSecondaryDevice2 = cbbSecondaryDevice2.Text;
                    if (strSecondaryDevice2.Length == 0)
                    {
                        strErrorMsg += "* " + "Please select the Secondary Device2" + "\r\n";
                    }
                }
                if (ckbSecondaryDevice3.Checked)
                {
                    strSecondaryDevice3 = cbbSecondaryDevice3.Text;
                    if (strSecondaryDevice3.Length == 0)
                    {
                        strErrorMsg += "* " + "Please select the Secondary Device3" + "\r\n";
                    }
                }
                #endregion Devices
                #region Host Path
                string strHostFolder = scriptFolder + "AutomationDependency\\";
                if (!Directory.Exists(strHostFolder))
                {

                    strErrorMsg += "* " + "DependencyFolder doesn't exist, please sync scripts folder and try again." + "\r\n";
                }
                #endregion Host Path
                #region Test Plan
                String strTestPlanPath = "";
                if (ckbRf_TestPlanPath.Checked)
                {
                    strTestPlanPath = txtRf_TestPlanPath.Text;
                }
                #endregion Test Plan             
                if (strErrorMsg.Length == 0)
                {
                    start_RobotframeworkMode(strPrimaryDevice, strSecondaryDevice1,
                               strSecondaryDevice2, strSecondaryDevice3,
                               strHostFolder, strTestPlanPath,
                               strRefunFailed, strWorkingSpace,
                               strScriptPath);
                }               
            }

            if (strErrorMsg.Length>0)
            {
                MessageBox.Show(strErrorMsg, "Incorrect operations", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }        
        }
        
        private void generateScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String scannerassistPath = Path.GetDirectoryName(pybotPath) + "\\scannerassist.bat";
            FormEmdkScriptGenerater.Display(scannerassistPath, strEmdk_RfsFolderPath + "\\" + EMDKScanner_VariableName, strEmdk_RfsFolderPath + "\\" + RobotListenerConfigName, cbbPrimaryDevice.Text, txtRf_TestPlanPath.Text);
        }

        private void txtTestPlanPath_DoubleClick(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel File|*.xlsx";
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtRf_TestPlanPath.Text = openFileDialog1.FileName;
            }
        }
        
        private void runningModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (runningMode == runningModes.EMDKScanner_Mode)
            {
                runningMode = runningModes.Robotframework_Mode;
            }
            else if (runningMode == runningModes.Robotframework_Mode)
            {
                runningMode = runningModes.EMDKScanner_Mode;
            }
            refreshRunningModeUI();
        }

        private void refreshRunningModeUI()
        {
            runningModeToolStripMenuItem.Text = runningMode.ToString();
            clearNotification1();
            if (runningMode == runningModes.EMDKScanner_Mode)
            {                
                runningModeToolStripMenuItem.BackColor = Color.LightGreen;
                readEmdkConfigurations();
                splitContainer1.Panel1.Enabled = true;
                splitContainer1.Panel2.Enabled = false;
                splitContainer1.Panel2Collapsed = true;
                ckbSecondaryDevice1.Enabled = false;
                ckbSecondaryDevice2.Enabled = false;
                ckbSecondaryDevice3.Enabled = false;
                ckbRf_RerunFailed.Checked = false;
                cbbSecondaryDevice1.Text = "";
                cbbSecondaryDevice2.Text = "";
                cbbSecondaryDevice3.Text = "";
                txtRf_ScrpitPath.Text = "";
                txtRf_ScrpitPath.Enabled = false;
                btnScriptBrowser.Enabled = false;
                txtRf_RerunFailed.Text = "";
                generateScriptToolStripMenuItem.Enabled = true;
                mergeToolStripMenuItem.Enabled = false;
            }
            else if(runningMode == runningModes.Robotframework_Mode)
            {
                runningModeToolStripMenuItem.BackColor = Color.LightPink;
                splitContainer1.Panel1.Enabled = false;
                splitContainer1.Panel2.Enabled = true;
                splitContainer1.Panel1Collapsed = true;
                txtRf_ScrpitPath.Text = "";
                txtRf_RerunFailed.Text = "";
                txtRf_ScrpitPath.Enabled = true;
                btnScriptBrowser.Enabled = true;
                ckbSecondaryDevice1.Enabled = true;
                ckbSecondaryDevice2.Enabled = true;
                ckbSecondaryDevice3.Enabled = true;
                ckbRf_RerunFailed.Checked = false;
                generateScriptToolStripMenuItem.Enabled = false;
                mergeToolStripMenuItem.Enabled = true;
                if (!isInitialized)
                {
                    showNotification1("Not initialized, click [Funcions]==>[Sycn Scripts] first!!!", 30, Color.Red, true);
                }
            }
        }

        private void txtRerunFailed_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            openFileDialog1.Filter = "Output XML File|*.xml";
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtRf_RerunFailed.Text = openFileDialog1.FileName;
            }
        }
        
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file://"+currentDir+chmFileName);            
        }

        private void btnEs_TrsPathBrowser_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel File |*.xlsx";
            if(openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtEs_TestPlanPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnEs_Run_Click(object sender, EventArgs e)
        {
            String strErrorMsg = "";
            if (cbbPrimaryDevice.Text.Length == 0)
            {
                strErrorMsg += "There are no devices connected!";
            }
            if(txtLegacyId.Text.Length==0)
            {
                strErrorMsg += "* " + "Lagacy ID can NOT be empty.\r\n";
            }
            if(cbbScannerType.Text.Length==0)
            {
                strErrorMsg += "* " + "Scanner type can NOT be empty.\r\n";
            }
            if (cbbTestPlatform.Text.Length == 0)
            {
                strErrorMsg += "* " + "Platform can NOT be empty.\r\n";
            }
            if (cbbDisplayType.Text.Length == 0)
            {
                strErrorMsg += "* " + "Display type can NOT be empty.\r\n";
            }
            if (strErrorMsg.Length == 0)
            {
                saveEmdkConfigurations();
                bool isRerun = ckbEs_RefunFailed.Checked;
                start_EmdkScannerMode(isRerun);
            }
        }

        #endregion UI Controller
    }
}
    