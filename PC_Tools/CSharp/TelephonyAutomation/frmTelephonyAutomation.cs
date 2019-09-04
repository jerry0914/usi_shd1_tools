using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.usi.shd1_tools.CommonLibrary;
using com.usi.shd1_tools.TelephonyLibrary;
using System.Threading;
using System.Reflection;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    public partial class frmTelephonyAutomation : Form
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
        #region Testcases' description
        private List<String> testcaseDescription = new List<String>(new String[]{
                                                                                                                                                    @"Terminal calls Test set and terminal hangs up. 
Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).  
1. Terminal Calls StationEmulator
2. Check Agilent call status
3. Terminal hangs up.
4. Check Agilent call status",
                                                                                                                                                    @"Terminal calls Test set and Test set hangs up .
Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).
1. Terminal Calls StationEmulator
2. Check Agilent call status
3. StationEmulator hangs up.
4. Check Terminal call status",
                                                                                                                                                    @"StationEmulator calls terminal and terminal hangs up
Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).
1. StationEmulator Calls Terminal
2. Check Agilent call status
3. Terminal hangs up.
4. Check Agilent call status",
                                                                                                                                                    @"StationEmulator calls terminal and StationEmulator hangs up.
Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).
1. StationEmulator Calls Terminal
2. Check Agilent call status
3. StationEmulator hangs up.
4. Check Agilent call status",
                                                                                                                                                    @"Terminal calls Test set and terminal hangs up. 
Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).  
1. Terminal Calls StationEmulator
2. Check Agilent call status
3. Terminal hangs up.
4. Check Agilent call status",
                                                                                                                                                    @"Terminal calls Test set and terminal hangs up. 
Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).  
1. Terminal Calls StationEmulator
2. Check Agilent call status
3. Terminal hangs up.
4. Check Agilent call status",
                                                                                                                                                    @"Terminal calls Test set and terminal hangs up. 
Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).  
1. Terminal Calls StationEmulator
2. Check Agilent call status
3. Terminal hangs up.
4. Check Agilent call status",
                                                                                                                                                    @"Terminal calls Test set and terminal hangs up. 
Each call lasts 20 seconds with sufficient power level (RSSI -50dbm).  
1. Terminal Calls StationEmulator
2. Check Agilent call status
3. Terminal hangs up.
4. Check Agilent call status",
                                                                                                                                                    @"Terminal calls StationEmulator and terminal hangs up the call.
The call should last at least 30 minutes with sufficient power level (RSSI -50dbm).
1. Terminal Calls StationEmulator
2. Check Agilent Active Cell status
3. Time lapse of 10 sec
4. Check Terminal Call status every 30 seconds
5. Check Agilent Active Cell status every 30 seconds 
6. StationEmulator hangs up.
7. Check Agilent call status",
                                                                                                                                                    @"StationEmulator calls terminal and terminal hangs up the call.
The call should last at least 60 minutes with with sufficient power level (RSSI -50dbm).
1. Terminal Calls StationEmulator
2. Check Agilent Active Cell status
3. Time lapse of 10 sec
4. Check Terminal Call status every 30 seconds
5. Check Agilent Active Cell status every 30 seconds 
6. StationEmulator hangs up.
7. Check Agilent call status",
                                                                                                                                                    @"Terminal calls Test set and Test set hangs up the call.
The call should last at least 90 minutes with minimum support power level. (RSSI -92dbm).
1. Terminal Calls StationEmulator
2. Check Agilent Active Cell status
3. Time lapse of 10 sec
4. Check Terminal Call status every 30 seconds
5. Check Agilent Active Cell status every 30 seconds 
6. StationEmulator hangs up.
7. Check Agilent call status",
                                                                                                                                                    @"StationEmulator calls terminal and StationEmulator hangs up the call. 
The call should last at least 120 minutes with minimum support power level. (RSSI -92dbm).
1. Terminal Calls StationEmulator
2. Check Agilent Active Cell status
3. Time lapse of 10 sec
4. Check Terminal Call status every 30 seconds
5. Check Agilent Active Cell status every 30 seconds 
6. StationEmulator hangs up.
7. Check Agilent call status"
        });
        #endregion Testcases' description

        private IStationEmulatorConnector connector = null;
        private DeviceInfomation currentDevice = null;
        private Thread tdDutStatus = null;
        private const int dutMonitorInterval = 4000;
        private bool dutStatusMonitor_Flag = false;
        private List<DeviceInfomation> devList;
        private bool dutReady = false, atstReady = false;
        private String apiFunctionApkPath = System.AppDomain.CurrentDomain.BaseDirectory + "apk\\APIFunction.apk";
        private dutController.DutPhoneState dut1State = dutController.DutPhoneState.Unknow;
        private ProcedureProcessor_8960.StationEmulatorState stationEmulatorState = ProcedureProcessor_8960.StationEmulatorState.Unknow;
        private ProcedureProcessor_8960 procedureProcessor = null;
        private dutController.DutPhoneState previousDutPhoneState = dutController.DutPhoneState.Unknow;

        public frmTelephonyAutomation()
        {
            InitializeComponent();
            this.Text = Title + " v" + Version.ToString(3);
            startDutMonitor();
            Logger.LiveLogEventHandler += new EventHandler<LoggerLiveMessageEventArgs>(liveLogMessageEventHandler);
        }

        private void startDutMonitor()
        {
            dutStatusMonitor_Flag = true;
            if (tdDutStatus != null)
            {
                stopDutMonitor();
            }
            tdDutStatus = new Thread(dutStatusMonitor_Runnable);
            tdDutStatus.Start();
        }

        private void stopDutMonitor()
        {
            dutStatusMonitor_Flag = false;
            if (tdDutStatus != null)
            {
                tdDutStatus.Interrupt();
                tdDutStatus = null;
            }
        }

        private void dutStatusMonitor_Runnable()
        {
            try
            {
                while (dutStatusMonitor_Flag)
                {
                    refreshDutStatus();
                    Thread.Sleep(dutMonitorInterval);
                }
            }
            catch (ThreadInterruptedException tiex)
            {
            }
        }

        private void testcasesRun(bool debugMode,bool infinityMode)
        {
            //lsvLiveLog.Items.Clear();
            //lblTcResult.Text = "";
            //lblChekPointResult.Text = ""; 
            //if (procedureProcessor != null)
            //{
            //    procedureProcessor.Dispose();
            //}
            //procedureProcessor = new ProcedureProcessor_8960(currentDevice.ID, connector);
            //procedureProcessor.DutPhoneStateChangedEventHandler += new EventHandler<DutPhoneStateChangedEventArgs>(DutPhoneStateChangedEventHandler);
            //procedureProcessor.StationEmulatorStateChangedEventHandler += new EventHandler<StationEmulatorStateChangedEventArgs>(StationEmulatorStateChangedEventHandler);
            //procedureProcessor.ProcedureProgressChangedEventHandler += new EventHandler<ProcedureProgressChangedEventArgs>(ProcedureProgressChangedEventHandler);
            //procedureProcessor.TestResultUpdateEventHandler += new EventHandler<TestResultUpdateEventArgs>(TestResultUpdateEventHandler);
            //procedureProcessor.RunningStateChangedEventHandler += new EventHandler<ProcedureProcessorRunningStateChangedEventArgs>(ProcedureProcessorRunningStateChagedEventHandler);
            //List<String> selectedTCs = new List<String>();
            //foreach (ListViewItem li in lsvTestcases.CheckedItems)
            //{
            //    selectedTCs.Add(li.Index);
            //}
            //procedureProcessor.Start(selectedTCs.ToArray(), debugMode, infinityMode);
        }

        #region Custom Event Handler

        #region DUT connecting state UI refresh
        private delegate void delVoidNoparam();
        private bool dutAutoSelectFlag = false;
        private void refreshDutStatus()
        {
            if (this.InvokeRequired)
            {
                delVoidNoparam del = new delVoidNoparam(refreshDutStatus);
                try
                {
                    this.Invoke(del);
                }
                catch (InvalidOperationException iex)
                {
                }
            }
            else
            {
                devList = ADB_Process.GetDeivcesList();
                dutAutoSelectFlag = true;
                cmbDeviceList.Items.Clear();
                foreach (DeviceInfomation dev in devList)
                {
                    cmbDeviceList.Items.Add(dev.ID);
                }
                if (devList.Count == 0)
                {
                    currentDevice = null;
                    btnDutStatus.Text = "No device";
                    btnDutStatus.BackColor = System.Drawing.Color.Crimson;
                    btnDutStatus.ForeColor = System.Drawing.Color.White;
                    dutReady = false;
                    atstReady = false;
                }
                else
                {
                    if (devList.Count == 1)
                    {
                        cmbDeviceList.SelectedIndex = 0;
                        currentDevice = devList[0];
                    }
                    else
                    {
                        if (currentDevice != null && cmbDeviceList.Items.Contains(currentDevice.ID))
                        {
                            cmbDeviceList.SelectedItem = currentDevice.ID;
                        }
                        else
                        {
                            currentDevice = null;
                            btnDutStatus.Text = "Select a DUT";
                            btnDutStatus.BackColor = System.Drawing.Color.Orange;
                            btnDutStatus.ForeColor = System.Drawing.Color.White;
                        }
                    }
                }
                if (currentDevice != null)
                {
                    switch (currentDevice.ConnectingStatus)
                    {
                        case "Offline":
                            btnDutStatus.Text = "Offline";
                            btnDutStatus.BackColor = System.Drawing.Color.Gray;
                            btnDutStatus.ForeColor = System.Drawing.Color.White;
                            dutReady = false;
                            atstReady = false;
                            break;
                        case "Connected":
                            dutReady = true;
                            if (!atstReady)
                            {
                                atstReady = checkAtstPackageReady();
                            }
                            if (atstReady)
                            {
                                btnDutStatus.Text = "Ready";
                                btnDutStatus.BackColor = System.Drawing.Color.Green;
                                btnDutStatus.ForeColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                btnDutStatus.Text = "Need initialize";
                                btnDutStatus.BackColor = System.Drawing.Color.YellowGreen;
                                btnDutStatus.ForeColor = System.Drawing.Color.White;
                                btnDutStatus_Click(btnDutStatus, new EventArgs());
                            }
                            break;
                        default:
                            dutReady = false;
                            atstReady = false;
                            btnDutStatus.Text = currentDevice.ConnectingStatus;
                            btnDutStatus.BackColor = System.Drawing.SystemColors.Control;
                            btnDutStatus.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
                dutAutoSelectFlag = false;
            }
        }
        #endregion DUT connecting state UI refresh

        #region DUT state UI refresh
        private void DutPhoneStateChangedEventHandler(object sender, DutPhoneStateChangedEventArgs ea)
        {
            refreshDutPhoneState(ea.State);
        }
        private delegate void delRefreshDutPhoneState(dutController.DutPhoneState state);
        private void refreshDutPhoneState(dutController.DutPhoneState state)
        {
            if (this.InvokeRequired)
            {
                delRefreshDutPhoneState del = new delRefreshDutPhoneState(refreshDutPhoneState);
                try
                {
                    this.Invoke(del, state);
                }
                catch (InvalidOperationException iex)
                {
                }
            }
            else
            {
                dut1State = state;
                switch (state)
                {
                    case dutController.DutPhoneState.Ringing:
                        txtDutPhoneState.BackColor = Color.Orange;
                    break;
                    case dutController.DutPhoneState.Offhook:
                    if (previousDutPhoneState.Equals(dutController.DutPhoneState.Ringing))
                    {
                        dut1State = dutController.DutPhoneState.Answered;
                    }
                    else if (previousDutPhoneState.Equals(dutController.DutPhoneState.Answered))
                    {
                        dut1State = dutController.DutPhoneState.Answered;
                    }
                    else if (previousDutPhoneState.Equals(dutController.DutPhoneState.Dialing))
                    {
                        if (procedureProcessor != null && procedureProcessor.CurrentStationEmulatorPhoneState.Equals(ProcedureProcessor_8960.StationEmulatorState.Connected))
                        {
                            dut1State = dutController.DutPhoneState.Connected;
                        }
                    }
                    else if (previousDutPhoneState.Equals(dutController.DutPhoneState.Connected))
                    {
                        dut1State = dutController.DutPhoneState.Connected;
                    }
                    else
                    {
                        dut1State = dutController.DutPhoneState.Dialing;
                    }
                    txtDutPhoneState.BackColor = Color.Crimson;
                    break;
                    case dutController.DutPhoneState.Idle:
                    if (previousDutPhoneState.Equals(dutController.DutPhoneState.Offhook) ||
                        previousDutPhoneState.Equals(dutController.DutPhoneState.Answered) ||
                        previousDutPhoneState.Equals(dutController.DutPhoneState.Connected) ||
                        previousDutPhoneState.Equals(dutController.DutPhoneState.Dialing))
                    {
                        dut1State = dutController.DutPhoneState.EndCall;
                        txtDutPhoneState.BackColor = Color.Green;
                    }
                    else if (previousDutPhoneState.Equals(dutController.DutPhoneState.Ringing))
                    {
                        dut1State = dutController.DutPhoneState.Rejected;
                        txtDutPhoneState.BackColor = Color.Purple;
                    }
                    else
                    {
                        dut1State = dutController.DutPhoneState.Idle;
                        txtDutPhoneState.BackColor = Color.YellowGreen;
                    }
                    break;
                default:
                    txtDutPhoneState.BackColor = Color.Gray;
                    dut1State = dutController.DutPhoneState.Unknow;
                    break;
                }
                previousDutPhoneState = dut1State;
                String DutPhoneStateText = dut1State.ToString();
                DutPhoneStateText =  DutPhoneStateText.Contains("_")?DutPhoneStateText.Substring(DutPhoneStateText.IndexOf("_")+1):DutPhoneStateText;
                txtDutPhoneState.Text = DutPhoneStateText;
            }
        }
        #endregion DUT state UI refresh

        #region StationEmulator state UI refresh
        private void StationEmulatorStateChangedEventHandler(object sender, StationEmulatorStateChangedEventArgs ea)
        {
            refreshStationEmulatorState(ea.Band, ea.CellPower, ea.State);
        }
        private delegate void delStationEmulatorDutPhoneState(String band, double cellPower, ProcedureProcessor_8960.StationEmulatorState state);
        private void refreshStationEmulatorState(String band, double cellPower, ProcedureProcessor_8960.StationEmulatorState state)
        {
            if (this.InvokeRequired)
            {
                delStationEmulatorDutPhoneState del = new delStationEmulatorDutPhoneState(refreshStationEmulatorState);
                try
                {
                    this.Invoke(del, band, cellPower, state);
                }
                catch (InvalidOperationException iex)
                {
                }
            }
            else
            {
                txtCellPower.Text = cellPower.ToString() + " db";
                txtBand.Text = band;
                switch (state)
                {
                    case ProcedureProcessor_8960.StationEmulatorState.Idle:
                        txtStationEmulatorState.BackColor = Color.YellowGreen;
                        break;
                    case ProcedureProcessor_8960.StationEmulatorState.Alerting:
                        txtStationEmulatorState.BackColor = Color.Orange;
                        break;
                    case ProcedureProcessor_8960.StationEmulatorState.Connected:
                        txtStationEmulatorState.BackColor = Color.Crimson;
                        break;
                    case ProcedureProcessor_8960.StationEmulatorState.Disconnecting:
                        txtStationEmulatorState.BackColor = Color.Green;
                        break;
                    case ProcedureProcessor_8960.StationEmulatorState.SetupRequest:
                        txtStationEmulatorState.BackColor = Color.OrangeRed;
                        break;
                    case ProcedureProcessor_8960.StationEmulatorState.Proceeding:
                        txtStationEmulatorState.BackColor = Color.Purple;
                        break;
                    case ProcedureProcessor_8960.StationEmulatorState.Unknow:
                    default:
                        txtStationEmulatorState.BackColor = Color.Gray;
                        break;
                }
                txtStationEmulatorState.Text = state.ToString();
            }
        }
        #endregion StationEmulator state UI refresh

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
                    if (subMsg[3].ToUpper().Contains("FAIL") || subMsg[1].ToUpper().Equals("E"))
                    {
                        li.ForeColor = Color.Red;
                    }
                    else if (subMsg[3].ToUpper().Contains("PASS"))
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

        #region Progress update
        private void ProcedureProgressChangedEventHandler(object sender, ProcedureProgressChangedEventArgs ea)
        {
            updateProgress(ea);
        }
        delegate void delVoidObject(object param);
        private void updateProgress(object procedureProgressChangedEventArgs)
        {
            if (this.InvokeRequired)
            {
                delVoidObject del = new delVoidObject(updateProgress);
                this.Invoke(del,procedureProgressChangedEventArgs);
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

        #region Test result update
        private void TestResultUpdateEventHandler(object sender, TestResultUpdateEventArgs ea)
        {
            updateTestResult(ea);
        }
        private void updateTestResult(object testResultUpdateEventArgs)
        {
            if (this.InvokeRequired)
            {
                delVoidObject del = new delVoidObject(updateTestResult);
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

        #region Procedure running state refresh
        private void ProcedureProcessorRunningStateChagedEventHandler(object sender, ProcedureProcessorRunningStateChangedEventArgs ea)
        {
            procedureProcessorRunningStateRefresh(ea);
        }
        private void procedureProcessorRunningStateRefresh(object procedureProcessorRunningStateChangedEventArgs)
        {
            if (this.InvokeRequired)
            {
                delVoidObject del = new delVoidObject(procedureProcessorRunningStateRefresh);
                this.Invoke(del, procedureProcessorRunningStateChangedEventArgs);
            }
            else
            {
                ProcedureProcessorRunningStateChangedEventArgs param = procedureProcessorRunningStateChangedEventArgs as ProcedureProcessorRunningStateChangedEventArgs;
                switch (param.State)
                {
                    case ProcedureProcessor_8960.RunningState.Running:
                        btnRun.BackColor = Color.Green;
                        btnRun.ForeColor = Color.White;
                        btnRun.Text = param.State.ToString();
                        break;
                    case ProcedureProcessor_8960.RunningState.Pausing:
                        btnRun.BackColor = Color.Orange;
                        btnRun.ForeColor = Color.White;
                        btnRun.Text = param.State.ToString();
                        break;
                    case ProcedureProcessor_8960.RunningState.Paused:
                        btnRun.BackColor = Color.OrangeRed;
                        btnRun.ForeColor = Color.White;
                        btnRun.Text = param.State.ToString();
                        break;
                    case ProcedureProcessor_8960.RunningState.Stopped:
                        btnRun.BackColor = Color.Crimson;
                        btnRun.ForeColor = Color.White;
                        btnRun.Text = param.State.ToString();
                        break;
                    case ProcedureProcessor_8960.RunningState.Stopping:
                        btnRun.BackColor = Color.PaleVioletRed;
                        btnRun.ForeColor = Color.White;
                        btnRun.Text = param.State.ToString();
                        break;
                    default:
                        btnRun.BackColor = SystemColors.Control;
                        btnRun.ForeColor = SystemColors.MenuHighlight;
                        btnRun.Text = "Run";
                        break;
                }
            }
        }
        #endregion Procedure running state refresh

        #endregion Custom Event Handler

        #region UI events
        private void lsvTestcases_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem li = lsvTestcases.GetItemAt(e.X, e.Y);
            if (li != null)
            {
                txtTcDescripition.Text = testcaseDescription[li.Index];
            }
            else
            {
                txtTcDescripition.Text = "";
            }
        }

        private void rdbGpib_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb.Name.Equals("rdbGpib"))
            {
                gbGpib.Visible = rdb.Checked;
                //checkGpibConnected();
            }
            else if (rdb.Name.Equals("rdbVisa"))
            {
                gbVisa.Visible = rdb.Checked;
                //checkVisaConnected();             
            }
        }

        //private void checkGpibConnected()
        //{
        //    IStationEmulatorConnector testconnector = null;
        //    bool isStationEmulatorReady = false;
        //    try
        //    {
        //        testconnector = new GPIB_Connector(Convert.ToInt16(numBoard.Value), Convert.ToByte(numGPIB1.Value), Convert.ToByte(numGPIB2.Value));
        //        testconnector.Connect();
        //        isStationEmulatorReady = true;
        //        connector = testconnector;
        //    }
        //    catch
        //    {

        //    }
        //    if (isStationEmulatorReady)
        //    {
        //        btnGpibConnect.Text = "Connected";
        //        btnGpibConnect.BackColor = Color.Green;
        //        btnGpibConnect.ForeColor = Color.White;
        //    }
        //    else
        //    {
        //        btnGpibConnect.Text = "Click to connect";
        //        btnGpibConnect.BackColor = SystemColors.Control;
        //        btnGpibConnect.ForeColor = Color.LimeGreen;
        //    }
        //}

        //private void checkVisaConnected()
        //{
        //    IStationEmulatorConnector testconnector = null;
        //    bool isStationEmulatorReady = false;
        //    if (txtVisaName.Text.Length > 0)
        //    {
        //        try
        //        {
        //            testconnector = new VISA_Connector(txtVisaName.Text);
        //            testconnector.Connect();
        //            isStationEmulatorReady = true;
        //            connector = testconnector;
        //        }
        //        catch
        //        {

        //        }
        //    }
        //    if (isStationEmulatorReady)
        //    {
        //        btnVisaConnect.Text = "Connected";
        //        btnVisaConnect.BackColor = Color.Green;
        //        btnVisaConnect.ForeColor = Color.White;
        //    }
        //    else
        //    {
        //        btnVisaConnect.Text = "Click to connect";
        //        btnVisaConnect.BackColor = SystemColors.Control;
        //        btnVisaConnect.ForeColor = Color.LimeGreen;
        //    }
        //}

        private void btnGpibConnect_Click(object sender, EventArgs e)
        {
            try
            {
                connector = new _8960_GPIB_Connector(Convert.ToInt16(numBoard.Value), Convert.ToByte(numGPIB1.Value), Convert.ToByte(numGPIB2.Value));
                connector.Connect();
                btnGpibConnect.Text = "Connected";
                btnGpibConnect.BackColor = Color.Green;
                btnGpibConnect.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnGpibConnect.Text = "Disconnected";
                btnGpibConnect.BackColor = Color.Crimson;
                btnGpibConnect.ForeColor = Color.White;
            }
        }

        private void btnVisaConnect_Click(object sender, EventArgs e)
        {
            try
            {
                connector = new VISA_Connector(txtVisaName.Text);
                connector.Connect();
                btnVisaConnect.Text = "Connected";
                btnVisaConnect.BackColor = Color.Green;
                btnVisaConnect.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnVisaConnect.Text = "Disconnected";
                btnVisaConnect.BackColor = Color.Crimson;
                btnVisaConnect.ForeColor = Color.White;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (connector == null)
            {
                MessageBox.Show("Establish a connection first.");
            }
            else
            {
                frmStationEmulatorFunctionTest frm = new frmStationEmulatorFunctionTest(connector);
                frm.ShowDialog();
            }
        }

        private void cmbDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dutAutoSelectFlag)
            {
            }
            else
            {
                currentDevice = devList[cmbDeviceList.SelectedIndex];
            }
        }

        private void btnDutTest_Click(object sender, EventArgs e)
        {
            new frmDutFunctionTest().ShowDialog();
        }

        private bool checkAtstPackageReady()
        {
            bool apkReady = false, pathReady = false;
            String adbArgument = "", adbReturn = "";
            #region check atst apk
            List<String> list = ADB_Process.GetPackagesList(currentDevice.ID, "com.asus.at");
            if (list.Count > 0)
            {
                apkReady = true;
            #endregion check atst apk
                #region check file path
                if (currentDevice != null && currentDevice.ID.Length > 0)
                {
                    adbArgument += "-s " + currentDevice.ID + " ";
                }
                adbArgument += "shell ls /sdcard/ATST/ToolInfo/";
                ADB_Process.RunAdbCommand(adbArgument, ref adbReturn);
                pathReady = !adbReturn.ToLower().Contains("no such file or directory");
                #endregion check file path
            }
            return apkReady & pathReady;
        }

        private void initializeAtstPackage()
        {
            String deviceHeader = "", adbArgument = "", adbReturn = "";
            if (currentDevice != null && currentDevice.ID.Length > 0)
            {
                deviceHeader = "-s " + currentDevice.ID + " ";
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

        private void btnDutStatus_Click(object sender, EventArgs e)
        {
            if (dutReady && !atstReady)
            {
                this.Cursor = Cursors.WaitCursor;
                initializeAtstPackage();
                atstReady = checkAtstPackageReady();
                this.Cursor = Cursors.Default;
            }
        }

        String pw = "";
        private void btnRun_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && !(e.KeyCode.Equals(Keys.RButton | Keys.ShiftKey)))
            {
                pw += e.KeyCode.ToString();
            }
            else
            {
                pw = "";
            }
            if (pw.Length > 3)
            {
                pw = "";
            }
            if (pw.ToLower().Equals("usi"))
            {
                btnDutDebug.Visible = true;
                btnStationEmulatorDebug.Visible = true;
                btnTcDebug.Visible = true;
                chkInfinityMode.Visible = true;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (procedureProcessor != null && procedureProcessor.CurrentRunningState.Equals(ProcedureProcessor_8960.RunningState.Running))
            {
                procedureProcessor.Stop();
                //btnRun.Text = "Stopped";
                //btnRun.BackColor = Color.Crimson;
                //btnRun.ForeColor = Color.White;
            }
            else
            {
                if (lsvTestcases.CheckedItems.Count > 0)
                {
                    lsvLiveLog.Items.Clear();
                    if (connector != null && currentDevice != null)
                    {
                        testcasesRun(false,chkInfinityMode.Checked);
                        //btnRun.Text = "Running";
                        //btnRun.BackColor = Color.Green;
                        //btnRun.ForeColor = Color.White;
                    }
                    else
                    {
                        MessageBox.Show("Establish the connections with station emulator & DUT  first, please.");
                    }
                }
                else
                {
                    MessageBox.Show("Choose the test case(s) to run.");
                }
            }
        }

        private void btnTcDebug_Click(object sender, EventArgs e)
        {
            chkInfinityMode.Checked = true;
            btnGpibConnect_Click(btnGpibConnect, new EventArgs());
            if (procedureProcessor != null && procedureProcessor.CurrentRunningState.Equals(ProcedureProcessor_8960.RunningState.Running))
            {
                procedureProcessor.Stop();
                btnRun.Text = "Stopped";
                btnRun.BackColor = Color.Crimson;
                btnRun.ForeColor = Color.White;
            }
            else
            {
                testcasesRun(true,chkInfinityMode.Checked);
                btnRun.Text = "Running";
                btnRun.BackColor = Color.Green;
                btnRun.ForeColor = Color.White;
            }
        }      

        private void btnCleanLiveLog_Click(object sender, EventArgs e)
        {
            lsvLiveLog.Items.Clear();
        }

        private void lsvLiveLog_Resize(object sender, EventArgs e)
        {
            chMessage.Width = lsvLiveLog.Width - chTime.Width - chLogLevel.Width - chTag.Width;
        }

        #endregion UI events
    }
}
