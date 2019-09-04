using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using com.usi.shd1_tools.TestcasePackage;
using com.usi.shd1_tools._8960Library;
using System.Collections;
using System.Reflection;
using System.Xml.Linq;
using dev.jerry_h.pc_tools.CommonLibrary;
using dev.jerry_h.pc_tools.AndroidLibrary;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    public class ProcedureProcessor_8960 : IDisposable
    {
        public readonly static String pathConfiguration = System.AppDomain.CurrentDomain.BaseDirectory + "TestCaseSettings_8960.xml";
        public readonly static String pathScreenCaptureDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\ScreenCapture\\";
        public readonly static String pathMTKLogDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\MtkLogs\\";
        public enum RunningState
        {
            None = 0,
            Stopped = 1,
            Running = 2,
            Pausing = 3,
            Paused = 4,
            Stopping = 5,
            Finished=6
        }
        private frmMain mainForm;
        private clsDevice dutDevice;
        private StationEmulator_8960 se8960;
        private RunningState _doNotUseCurrentRunningState = RunningState.None;
        public RunningState CurrentRunningState
        {
            get
            {
                return _doNotUseCurrentRunningState;
            }
            set
            {
                if(!_doNotUseCurrentRunningState.Equals(value))
                {
                    _doNotUseCurrentRunningState = value;
                    if (RunningStateChangedEventHandler != null)
                    {
                        RunningStateChangedEventHandler.Invoke(this, new ProcedureProcessorRunningStateChangedEventArgs(_doNotUseCurrentRunningState));
                    }
                }
            }
        }
        public static int defaultDialTimeout = 20000;
        public static int cellModifyDelay = 60000;
        public static double defaultCellPower = -50;
        private Thread tdTcLister = null;
        private Thread tdTcRunner = null;
        private bool _runFlag = false;
        private bool _pauseFlag = false;
        private const int loopDelay = 5000;
        private const int tcDelay = 10000;
        private const int actionDelay = 1000;
        private const int flightMode_Delay = 3000;
        private JHWaitoneEventHandle jhWaitOne = new JHWaitoneEventHandle(false);
        public EventHandler<CallStateChangedEventArgs> CallStateChangedEventHandler;
        public EventHandler<DutSignalStrengthChangedEventArgs> DutSignalStengthChangedEventHandler;
        public EventHandler<ProcedureProgressChangedEventArgs> ProcedureProgressChangedEventHandler;
        public EventHandler<TestResultUpdateEventArgs> TestResultUpdateEventHandler;
        public EventHandler<ProcedureProcessorRunningStateChangedEventArgs> RunningStateChangedEventHandler;
        private int iterationTimes = 1;
        private int iterationCounter = 0;
        private int callDuration_inSeconds = 0;
        private int checkStatusInterval_inMilliseconds = 0;
        private DateTime callConnectedTime = DateTime.MinValue;
        private int _doNotUseTotalCheckPointPassCount = 0;
        private int _doNotUseTotalCheckPointFailCount = 0;
        private int totalPassCheckPoint
        {
            get
            {
                return _doNotUseTotalCheckPointPassCount;
            }
            set
            {
                _doNotUseTotalCheckPointPassCount = value;
                if (value > 0)
                {
                    tcPassCheckPoint++;
                }
                else
                {
                    tcPassCheckPoint = 0;
                }
                if (TestResultUpdateEventHandler != null)
                {
                    TestResultUpdateEventHandler.Invoke(this, new TestResultUpdateEventArgs(_doNotUseTotalCheckPointPassCount, _doNotUseTotalCheckPointFailCount,totalPassedTestCase,totalFailedTestCase));
                }
            }
        }
        private int totalFailCheckPoint
        {
            get
            {
                return _doNotUseTotalCheckPointFailCount;
            }
            set
            {
                _doNotUseTotalCheckPointFailCount = value;
                if (value > 0)
                {
                    tcFailCheckPoint++;
                }
                else
                {
                    tcFailCheckPoint = 0;
                }
                if (TestResultUpdateEventHandler != null)
                {
                    TestResultUpdateEventHandler.Invoke(this, new TestResultUpdateEventArgs(_doNotUseTotalCheckPointPassCount, _doNotUseTotalCheckPointFailCount,totalPassedTestCase,totalFailedTestCase));
                }
            }
        }
        private int tcPassCheckPoint = 0;
        private int tcFailCheckPoint = 0;
        private int _doNotUseTotalPassedTestCase = 0;
        private int _doNotUseTotalFailedTestCase = 0;
        private int totalPassedTestCase
        {
            get 
            {
                return _doNotUseTotalPassedTestCase;
            }
            set
            {
                _doNotUseTotalPassedTestCase = value;
                if (TestResultUpdateEventHandler != null)
                {
                    TestResultUpdateEventHandler.Invoke(this, new TestResultUpdateEventArgs(_doNotUseTotalCheckPointPassCount, _doNotUseTotalCheckPointFailCount, _doNotUseTotalPassedTestCase, _doNotUseTotalFailedTestCase));
                }
            }
        }
        private int totalFailedTestCase
        {
            get
            {
                return _doNotUseTotalFailedTestCase;
            }
            set
            {
                _doNotUseTotalFailedTestCase = value;
                if (TestResultUpdateEventHandler != null)
                {
                    TestResultUpdateEventHandler.Invoke(this, new TestResultUpdateEventArgs(_doNotUseTotalCheckPointPassCount, _doNotUseTotalCheckPointFailCount, _doNotUseTotalPassedTestCase, _doNotUseTotalFailedTestCase));
                }
            }
        }
        private int totalLoopTimes = 0;
        public int totalLoopCount
        {
            get
            {
                return _doNotUseTotalLoopCount;
            }
            set
            {
                _doNotUseTotalLoopCount = value;
                if (ProcedureProgressChangedEventHandler != null)
                {
                    ProcedureProgressChangedEventHandler.Invoke(this, new ProcedureProgressChangedEventArgs(totalLoopTimes, _doNotUseTotalLoopCount));
                }
            }
        }
        private int _doNotUseTotalLoopCount = 0;
        private int loopTimes = 0;
        private int loopCount = 0;
        private bool isDebugMode = false;
        private bool isInfinityMode = false;
        private bool isEndCalling = false;
        private int tcIndex = 0;
        private int captureScreenIndex = 1;
        private int retry_count = 0;
        private const int retry_limit_for_call = 3;
        private int[] callDurationOfTCs = new int[1024];
        private int _highestChannelNumber
        {
            get
            {
                int result = -1;
                switch (currentTestcase.CurrentBand)
                {
                    case Wwan_TestCaseInfo.Band.GSM_EGSM:
                    case Wwan_TestCaseInfo.Band.GSM_900 :
                        result = 124;
                        break;
                    case Wwan_TestCaseInfo.Band.DCS_1800:
                        result = 885;
                        break;
                    case Wwan_TestCaseInfo.Band.UMTS_900:
                        result = 3088;
                        break;
                    case Wwan_TestCaseInfo.Band.UMTS_2100:
                        result = 10838;
                        break; 
                    case Wwan_TestCaseInfo.Band.TD_SCDMA_34:
                        result = 10075;
                        break;
                    case Wwan_TestCaseInfo.Band.TD_SCDMA_39:
                        result = 9596;
                        break;
                }
                return result;
            }
        }
        private int _lowestChannelNumber
        {
            get
            {
                int result = -1;
                switch (currentTestcase.CurrentBand)
                {
                    case Wwan_TestCaseInfo.Band.GSM_EGSM:
                        result = 975;
                        break;
                    case Wwan_TestCaseInfo.Band.GSM_900:
                        result = 1;
                        break;
                    case Wwan_TestCaseInfo.Band.DCS_1800:
                        result = 512;
                        break;
                    case Wwan_TestCaseInfo.Band.UMTS_900:
                        result = 2937;
                        break;
                    case Wwan_TestCaseInfo.Band.UMTS_2100:
                        result = 10562;
                        break;
                    case Wwan_TestCaseInfo.Band.TD_SCDMA_34:
                        result = 10054;
                        break;
                    case Wwan_TestCaseInfo.Band.TD_SCDMA_39:
                        result = 9404;
                        break;
                }
                return result;
            }
        }
        private int _middleChannelNumber
        {
            get
            {
                int result = -1;
                switch (currentTestcase.CurrentBand)
                {
                    case Wwan_TestCaseInfo.Band.GSM_EGSM:
                        result = 38;
                        break;
                    case Wwan_TestCaseInfo.Band.GSM_900:
                        result = 63;
                        break;
                    case Wwan_TestCaseInfo.Band.DCS_1800:
                        result = 698;
                        break;
                    case Wwan_TestCaseInfo.Band.UMTS_900:
                        result = 3012;
                        break;
                    case Wwan_TestCaseInfo.Band.UMTS_2100:
                        result = 10700;
                        break;
                    case Wwan_TestCaseInfo.Band.TD_SCDMA_34:
                        result = 10064;
                        break;
                    case Wwan_TestCaseInfo.Band.TD_SCDMA_39:
                        result = 9500;
                        break;
                }
                return result;
            }
        }
        private bool isDutPhoneConnected
        {
            get
            {
                return dutDevice.Telephony.CallState.Equals(clsTelephony.CallStates.OFFHOOK);
            }
        }
        private bool isStationEmulatorConnected
        {
            get
            {
                bool result = false; 
                try
                {
                    result = se8960.CallState.Equals(StationEmulator_8960.CallStates.Connected);
                }
                catch
                {
                    result = false;
                }
                return result;
            }
        }
        private bool waitForPhoneConnected
        {
            get
            {
                int timeout = 5000;
                DateTime dtStartTime = DateTime.Now;
                bool stationCon = false;
                bool phoneConn = false;
                phoneConn = waitForDutPhoneState(clsTelephony.CallStates.OFFHOOK, timeout);
                if (timeout > 0)
                {
                    timeout = (int)DateTime.Now.Subtract(dtStartTime).TotalMilliseconds;
                    stationCon = waitForStationEmulatorPhoneState(StationEmulator_8960.CallStates.Connected, timeout);
                }
                return phoneConn & stationCon;
            }
        }
        private bool checkWanConnectionFlag
        {
            get
            {
                bool dutConn = isDutPhoneConnected;
                bool stationConn = isStationEmulatorConnected;
                return dutConn & stationConn;
            }
        }
        private clsTelephony.CallStates currentCallState = clsTelephony.CallStates.IDLE;
        private Wwan_TestCaseInfo currentTestcase = null;
        #region for MTK Log
        private bool bCatchMtkLog_Flag = false;
        private int iCatchMtkLog_Interval_InMinutes = 150;
        private int iCatchMtkLogIndex = 1;
        private String strCatchMtkLogSubDirectory = "";
        private System.Threading.Timer tmrMtkLogCatcher;
        private void saveMtkLog(object obj)
        {
            String catchLogDir = pathMTKLogDirectory + strCatchMtkLogSubDirectory + (iCatchMtkLogIndex++) + "\\";
            System.IO.Directory.CreateDirectory(catchLogDir);
            System.Diagnostics.Process psCatchMtkLog = new System.Diagnostics.Process();
            ADB_Process.RunAdbCommand("-s " + dutDevice.ID + " pull /sdcard/mtklog/ " + catchLogDir);
            Logger.WriteLog(Logger.LogLevels.Verbose, "MtkLog", "Save MTK logs to " + catchLogDir);
        }
        private void startCatchMtkLog()
        {
            strCatchMtkLogSubDirectory = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "\\";
            stopCatchMtkLog();
            tmrMtkLogCatcher = new Timer(new TimerCallback(saveMtkLog), null, 5000, (iCatchMtkLog_Interval_InMinutes * 60* 1000));
            Logger.WriteLog(Logger.LogLevels.Verbose, "MtkLog", "Start MTK log catcher.");
        }             
        private void startCatchMtkLog(int catchIntervalInMinutes)
        {
            iCatchMtkLog_Interval_InMinutes = catchIntervalInMinutes;
            startCatchMtkLog();
        }
        private void stopCatchMtkLog()
        {
            //bCatchMtkLog_Flag = false;
            //if (tdCatchMtkLog != null)
            //{
            //    tdCatchMtkLog.Join(60 * 1000);
            //    tdCatchMtkLog.Abort();
            //    tdCatchMtkLog = null;
            //    CommonLibrary.Logger.WriteLog(Logger.LogLevels.Verbose, "MtkLog", "Stop MTK log catcher.");
            //}
            bCatchMtkLog_Flag = false;
            if (tmrMtkLogCatcher != null)
            {
                tmrMtkLogCatcher.Dispose();
                tmrMtkLogCatcher = null;
                Logger.WriteLog(Logger.LogLevels.Verbose, "MtkLog", "Stop MTK log catcher.");
            }
        }
        #endregion For MTK Log
        #region for Handover Test
        public static int adjustSignalStrength_ModifyStrengthDelay = 6000;
        public static int adjustSignalStrength_TimeoutInSeconds = 100;
        public static int adjustSignalStrength_CheckSignalStrengthInterval = 1200;
        public static int adjustSignalStrength_HitTargetPassingCriteria = 1; //取樣有n次singnal strength誤差在容許範圍內，才算pass；
        public static int adjustSignalStrength_RetryLimit = 2;
        public static int adjustSignalStrength_Inaccuracy = 4;
        private int[] channelTable_EGSM = new int[]
        {
            975,985,995,1005,1015,1023,1,11,21,31,41,51,61,71,81,91,101,111,121,124
        };
        private int[] channelTable_DCS = new int[]
        {
            512,532,552,572,592,612,632,652,672,692,712,732,752,772,792,812,832,852,872,885
        };
        #endregion for Handover Test
        #region for debug testcases
        private System.Threading.Timer tmrBER;
        private void startBERCatcher(long interval)
        {
            if (tmrBER != null)
            {
                stopBERCatcher();
            }
            tmrBER = new System.Threading.Timer(new TimerCallback(callBack_RecordBER), null, 0, interval);
        }
        private void stopBERCatcher()
        {
            if (tmrBER != null)
            {
                tmrBER.Dispose();
                tmrBER = null;
            }
        }
        private void callBack_RecordBER(object obj)
        {
            Logger.WriteLog("BER", "Lookback Bit Error Ratio = " + se8960.LookbackBER);
        }
        #endregion for debug testcases
        public ProcedureProcessor_8960(frmMain main,clsDevice device, StationEmulator_8960 se)
        {
            mainForm = main;
            dutDevice = device;
            se8960 = se;
        }
        public void Dispose()
        {
            Stop();
            stopCatchMtkLog();
            stopBERCatcher();
            CallStateChangedEventHandler = null;
            RunningStateChangedEventHandler = null;
            ProcedureProgressChangedEventHandler = null;
            TestResultUpdateEventHandler = null;
            DutSignalStengthChangedEventHandler = null;
            se8960.StationEmulatorStateChangedEventHandler = null;
        }
        public void Start(List<Wwan_TestCaseInfo> testcaseList, bool debugMode, bool infinityMode, bool catchMtkLog)
        {
            bCatchMtkLog_Flag = catchMtkLog;
            isDebugMode = debugMode;
            isInfinityMode = infinityMode;
            Array.Clear(callDurationOfTCs, 0, callDurationOfTCs.Length);
            #region Set call duration
            #region debug mode
            if (isDebugMode)
            {
                int currentValue = 10;
                int start = 0, end = 15;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 50; end = 65;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 100; end = 103;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 150; end = 153;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                currentValue = 60;
                start = 16; end = 21;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 66; end = 71;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 104; end = 107;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 154; end = 157;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 200; end = 202;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                #region For Debug testcases
                callDurationOfTCs[1001] = currentValue;  // In/Out coverage
                #endregion For Debug testcases
            }
            #endregion debug mode
            #region Normal mode
            else
            {
                int currentValue = 20;
                int start = 0, end = 15;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 50; end = 65;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 100; end = 103;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 150; end = 153;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 200; end = 202;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                callDurationOfTCs[1001] = currentValue;  // In/Out coverage
                currentValue = 3600;
                start = 16; end = 21;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 66; end = 71;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 104; end = 107;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                start = 154; end = 157;
                for (int index = start; index <= end; index++)
                {
                    callDurationOfTCs[index] = currentValue;
                }
                #region For Debug testcases
                #endregion For Debug testcases
            }
            #endregion Normal mode
            #endregion Set call duration
            ReadTestCaseSettings();
            iterationCounter = 0;
            List<ThreadStartWithTcInfo> tsList = new List<ThreadStartWithTcInfo>();
            Logger.Initialize(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\" + dutDevice.ID);
            Logger.WriteLog(Logger.LogLevels.Super, "Application Name", mainForm.Title,false);
            Logger.WriteLog(Logger.LogLevels.Super, "Version", mainForm.Version.ToString(4), false);
            ParameterizedThreadStart ts = null;
            totalPassCheckPoint = 0;
            totalFailCheckPoint = 0;
            totalLoopTimes = 0;
            totalLoopCount = 0;
            captureScreenIndex = 1;
            ADB_Process.SetWiFiState(dutDevice.ID, false, 5000);  //Disable WiFi
            foreach (Wwan_TestCaseInfo tc in testcaseList)
            {
                if (tc.IsSelected)
                {
                    switch (tc.TCID)
                    {
                        case "WA0001":
                        case "WA0051":
                            ts = new ParameterizedThreadStart(WA0001_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0002":
                        case "WA0052":
                            ts = new ParameterizedThreadStart(WA0002_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0003":
                        case "WA0053":
                            ts = new ParameterizedThreadStart(WA0003_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0004":
                        case "WA0054":
                            ts = new ParameterizedThreadStart(WA0004_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0005":
                        case "WA0055":
                            ts = new ParameterizedThreadStart(WA0005_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0006":
                        case "WA0056":
                            ts = new ParameterizedThreadStart(WA0006_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0007":
                        case "WA0057":
                            ts = new ParameterizedThreadStart(WA0007_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0008":
                        case "WA0058":
                            ts = new ParameterizedThreadStart(WA0008_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0009":
                        case "WA0059":
                            ts = new ParameterizedThreadStart(WA0009_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0010":
                        case "WA0060":
                            ts = new ParameterizedThreadStart(WA0010_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0011":
                        case "WA0061":
                            ts = new ParameterizedThreadStart(WA0011_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0012":
                        case "WA0062":
                            ts = new ParameterizedThreadStart(WA0012_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0013":
                        case "WA0063":
                            ts = new ParameterizedThreadStart(WA0013_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0014":
                        case "WA0064":
                            ts = new ParameterizedThreadStart(WA0014_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0015":
                        case "WA0065":
                            ts = new ParameterizedThreadStart(WA0015_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0016":
                        case "WA0066":
                            ts = new ParameterizedThreadStart(WA0016_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0017":
                        case "WA0067":
                            ts = new ParameterizedThreadStart(WA0017_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0018":
                        case "WA0068":
                            ts = new ParameterizedThreadStart(WA0018_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0019":
                        case "WA0069":
                            ts = new ParameterizedThreadStart(WA0019_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0020":
                        case "WA0070":
                            ts = new ParameterizedThreadStart(WA0020_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0021":
                        case "WA0071":
                            ts = new ParameterizedThreadStart(WA0021_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0022":
                        case "WA0072":
                            ts = new ParameterizedThreadStart(WA0022_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0101":
                        case "WA0151":
                            ts = new ParameterizedThreadStart(WA0101_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0102":
                        case "WA0152":
                            ts = new ParameterizedThreadStart(WA0102_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0103":
                        case "WA0153":
                            ts = new ParameterizedThreadStart(WA0103_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0104":
                        case "WA0154":
                            ts = new ParameterizedThreadStart(WA0104_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0105":
                        case "WA0155":
                            ts = new ParameterizedThreadStart(WA0105_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0106":
                        case "WA0156":
                            ts = new ParameterizedThreadStart(WA0106_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0107":
                        case "WA0157":
                            ts = new ParameterizedThreadStart(WA0107_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0108":
                        case "WA0158":
                            ts = new ParameterizedThreadStart(WA0108_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0201":
                        case "WA0251":
                            ts = new ParameterizedThreadStart(WA0201_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0202":
                        case "WA0252":
                            ts = new ParameterizedThreadStart(WA0202_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "WA0203":
                        case "WA0253":
                            ts = new ParameterizedThreadStart(WA0203_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "DBG0001":
                            ts = new ParameterizedThreadStart(DBG0001_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "DBG0002":
                            ts = new ParameterizedThreadStart(DBG0002_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        case "DBG0003":
                            ts = new ParameterizedThreadStart(DBG0003_runnable);
                            totalLoopTimes += tc.Loop;
                            break;
                        default:
                            break;
                    }
                    if (ts != null)
                    {
                        tsList.Add(new ThreadStartWithTcInfo(tc, ts));
                    }
                }
            }
            if (ProcedureProgressChangedEventHandler != null)
            {
                ProcedureProgressChangedEventHandler.Invoke(this, new ProcedureProgressChangedEventArgs(totalLoopTimes, _doNotUseTotalLoopCount));
            }
            if (tdTcLister != null)
            {
                tdTcLister.Interrupt();
                tdTcLister = null;
            }
            tdTcLister = new Thread(new ParameterizedThreadStart(startTcList));
            _runFlag = true;
            CurrentRunningState = RunningState.Running;
            tdTcLister.Start(tsList);
        }
        public void Start(List<Wwan_TestCaseInfo> testcaseList, bool debugMode, bool infinityMode)
        {
            Start(testcaseList,debugMode,infinityMode,false);
        }
        public void Start(List<Wwan_TestCaseInfo> testcaseList)
        {
            Start(testcaseList,false,false,false);
        }
        private void startTcList(object list)
        {
            List<ThreadStartWithTcInfo> tcRuns = list as List<ThreadStartWithTcInfo>;
            if (tcRuns.Count > 0)
            {
                try
                {
                    tcIndex = 0;
                    try
                    {
                        dutDevice.Auxiliary.KillLogcatprocess();
                        dutDevice.Auxiliary.StartLogcatProcess("/sdcard/usi/shd1_tools/Wan_Automation/Log/");
                    }
                    catch { }
                    if (bCatchMtkLog_Flag)
                    {
                        startCatchMtkLog();
                    }
                    Logger.WriteLog("START", "************************ Test start ************************");
                    while ((iterationCounter < iterationTimes || isInfinityMode) && _runFlag)
                    {
                        if (tcIndex == 0)
                        {
                            Logger.WriteLog("Iteration", "####### Iteration " + (iterationCounter + 1) + " start #######");
                        }
                        Logger.WriteLog("TC", "============= Test case " + tcRuns[tcIndex].CurrentTC.TCID + " start =============");
                        currentTestcase = tcRuns[tcIndex].CurrentTC;
                        startTcRunner(tcRuns[tcIndex]);
                        jhWaitOne.WaitOne();
                        Logger.WriteLog("TC", "============= Test case " + tcRuns[tcIndex].CurrentTC.TCID + " end =============", true);
                        Thread.Sleep(tcDelay);
                        tcIndex++;
                        if (tcIndex >= tcRuns.Count)
                        {
                            tcIndex = 0;
                            iterationCounter++;
                            Logger.WriteLog("Iteration", "####### Iteration " + iterationCounter + " end #######");
                        }
                    }
                    if (_runFlag)
                    {
                        if (bCatchMtkLog_Flag)
                        {
                            saveMtkLog("");
                        }
                        CurrentRunningState = RunningState.Finished;
                        _runFlag = false;
                        se8960.SetCellOff();
                    }
                }
                catch (Exception exx)
                {
                    Logger.WriteLog(Logger.LogLevels.Warning, "STOP", "************************Test interrupted  ************************", true);
                }
                finally
                {
                    _runFlag = false;
                    stopCatchMtkLog();
                    Logger.WriteLog("END", "************************ Test end ************************", true);
                    Logger.Cancel();
                    dutDevice.Auxiliary.KillLogcatprocess();
                }
            }
        }

        private void startTcRunner( ThreadStartWithTcInfo tsInfo)
        {
            if (tdTcRunner != null)
            {
                tdTcRunner.Interrupt();
                tdTcRunner = null;
            }
            tdTcRunner = new Thread(tsInfo.TcRunnable);
            tdTcRunner.Start(tsInfo.CurrentTC);
        }

        public void Stop()
        {
            if (_runFlag)
            {
                Logger.WriteLog("STOP", "ProcedureProcessor.Stop() is called manually", true);
                _runFlag = false;
                CurrentRunningState = RunningState.Stopping;
                if (tdTcLister != null)
                {
                    tdTcLister.Interrupt();
                }
                tdTcLister = null;
                if (tdTcRunner != null)
                {
                    tdTcRunner.Interrupt();
                }
                stopCatchMtkLog();
                tdTcRunner = null;
            }
        }

        public void Pause(bool pauseFlag)
        {
            if (pauseFlag)
            {
                CurrentRunningState = RunningState.Pausing;
                _pauseFlag = true;
            }
            else
            {
                _pauseFlag = false;
            }
        }

        #region Test cases' runnable
        private void WA0001_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.GSM_EGSM_FULL);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));           
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[0];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetTrafficChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetTrafficChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetTrafficChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channelto the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                        }
                        else{
                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }
                    Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if( tcFailCheckPoint == 0 )
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint); 
            jhWaitOne.Set();
        }

        private void WA0002_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.GSM_EGSM_FULL);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[1];
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {            
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetTrafficChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetTrafficChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetTrafficChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                        }
                        else //Establish call timeout
                        {
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (writeLog_CheckPointResult(!checkWanConnectionFlag, "Station Emulator hangs up"))
                        {
                            dutHangsUp();
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }
                    Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint); 
            jhWaitOne.Set();
        }

        private void WA0003_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.GSM_EGSM_FULL);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[1];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetTrafficChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetTrafficChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetTrafficChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorCallDut(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                        }
                        else //Establish call timeout
                        {
                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }
                    Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0004_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.GSM_EGSM_FULL);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[3];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorCallDut(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                        }
                        else //Establish call timeout
                        {

                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (writeLog_CheckPointResult(!checkWanConnectionFlag, "Station Emulator hangs up"))
                        {
                            dutHangsUp();
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }
                    Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0005_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            setCellBand(ACTIONS_SetCellBand.DCS_1800_FULL);
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[4];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetTrafficChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetTrafficChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetTrafficChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                        }
                        else //Establish call timeout
                        {

                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }
                    Thread.Sleep(loopDelay);
                } 
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint); 
            jhWaitOne.Set();
        }

        private void WA0006_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.DCS_1800_FULL);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[5];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetTrafficChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetTrafficChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetTrafficChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                            #region Check call connection
                            //Thread.Sleep(actionDelay);
                            //while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            //{
                            //    Thread.Sleep(checkStatusInterval_inMilliseconds);
                            //    if (IsPhoneConnected)
                            //    {
                            //        writeLog_CheckPointResult(true,"Check the phone connection");
                            //    }
                            //    else
                            //    {
                            //        writeLog_CheckPointResult(false,"Check the phone connection");
                            //        break;
                            //    }
                            //}
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {
                            // writeLog_CheckPointResult(false,"DUT call station emulator unsuccessfully");
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (writeLog_CheckPointResult(!checkWanConnectionFlag, "Station Emulator hangs up"))
                        {
                            dutHangsUp();
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }
                    Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }

            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint); 
            jhWaitOne.Set();
        }

        private void WA0007_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.DCS_1800_FULL);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[6];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetTrafficChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetTrafficChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetTrafficChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorCallDut(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                            #region Check call connection
                            //Thread.Sleep(actionDelay);
                            //while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            //{
                            //    Thread.Sleep(checkStatusInterval_inMilliseconds);
                            //    if (!writeLog_CheckPointResult(waitForPhoneConnected, "Check the phone connection"))
                            //    {
                            //        break;
                            //    }
                            //}
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {
                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }
                    Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint); 
            jhWaitOne.Set();
        }

        private void WA0008_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.DCS_1800_FULL);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[7];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetTrafficChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetTrafficChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetTrafficChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set Traffic Channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorCallDut(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");

                        }
                        else //Establish call timeout
                        {

                        }
                        stationEmulatorHangsUp();

                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }
                    Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint); 
            jhWaitOne.Set();
        }

        private void WA0009_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.UMTS_900);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[8];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                            #region Check call connection
                            //Thread.Sleep(actionDelay);
                            //while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            //{
                            //    Thread.Sleep(checkStatusInterval_inMilliseconds);
                            //    if (IsPhoneConnected)
                            //    {
                            //        writeLog_CheckPointResult(true, "Check the phone connection");
                            //    }
                            //    else
                            //    {
                            //        writeLog_CheckPointResult(false, "Check the phone connection");
                            //        break;
                            //    }
                            //}
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {
                            writeLog_CheckPointResult(false, "DUT call station emulator unsuccessfully");
                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                            Thread.Sleep(actionDelay);
                        }                     
                        #endregion Check call connection after hang up
                       
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }
                    Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0010_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.UMTS_900);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[9];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                            #region Check call connection
                            //Thread.Sleep(actionDelay);
                            //while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            //{
                            //    Thread.Sleep(checkStatusInterval_inMilliseconds);
                            //    if (IsPhoneConnected)
                            //    {
                            //        writeLog_CheckPointResult(true, "Check the phone connection");
                            //    }
                            //    else
                            //    {
                            //        writeLog_CheckPointResult(false, "Check the phone connection");
                            //        break;
                            //    }
                            //}
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    } Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }

            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }
        
        private void WA0011_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.UMTS_2100);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[10];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                            #region Check call connection
                            //Thread.Sleep(actionDelay);
                            //while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            //{
                            //    Thread.Sleep(checkStatusInterval_inMilliseconds);
                            //    if (IsPhoneConnected)
                            //    {
                            //        writeLog_CheckPointResult(true, "Check the phone connection");
                            //    }
                            //    else
                            //    {
                            //        writeLog_CheckPointResult(false, "Check the phone connection");
                            //        break;
                            //    }
                            //}
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {
                            //writeLog_CheckPointResult(false, "DUT call station emulator unsuccessfully");
                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up

                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    } Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0012_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.UMTS_2100);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[11];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                            #region Check call connection
                            //Thread.Sleep(actionDelay);
                            //while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            //{
                            //    Thread.Sleep(checkStatusInterval_inMilliseconds);
                            //    if (IsPhoneConnected)
                            //    {
                            //        writeLog_CheckPointResult(true, "Check the phone connection");
                            //    }
                            //    else
                            //    {
                            //        writeLog_CheckPointResult(false, "Check the phone connection");
                            //        break;
                            //    }
                            //}
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {

                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }

            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0013_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.TD_SCDMA_34);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[12];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                            #region Check call connection
                            // Thread.Sleep(actionDelay);
                            //while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            //{
                            //    Thread.Sleep(checkStatusInterval_inMilliseconds);
                            //    if (IsPhoneConnected)
                            //    {
                            //        writeLog_CheckPointResult(true, "Check the phone connection");
                            //    }
                            //    else
                            //    {
                            //        writeLog_CheckPointResult(false, "Check the phone connection");
                            //        break;
                            //    }
                            //}
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {

                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                            Thread.Sleep(actionDelay);
                        }                      
                        #endregion Check call connection after hang up
                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0014_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.TD_SCDMA_34);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[13];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isStationEmulatorConnected && isDutPhoneConnected, "Check the phone connection while call finished.");
                            #region Check call connection
                            //Thread.Sleep(actionDelay);
                            //while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            //{
                            //    Thread.Sleep(checkStatusInterval_inMilliseconds);
                            //    if (IsPhoneConnected)
                            //    {
                            //        writeLog_CheckPointResult(true, "Check the phone connection");
                            //    }
                            //    else
                            //    {
                            //        writeLog_CheckPointResult(false, "Check the phone connection");
                            //        break;
                            //    }
                            //}
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {

                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }

            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0015_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.TD_SCDMA_39);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[14];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                            #region Check call connection
                            //Thread.Sleep(actionDelay);
                            //while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            //{
                            //    Thread.Sleep(checkStatusInterval_inMilliseconds);
                            //    if (IsPhoneConnected)
                            //    {
                            //        writeLog_CheckPointResult(true, "Check the phone connection");
                            //    }
                            //    else
                            //    {
                            //        writeLog_CheckPointResult(false, "Check the phone connection");
                            //        break;
                            //    }
                            //}
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {
                            //writeLog_CheckPointResult(false, "DUT call station emulator unsuccessfully");
                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0016_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.TD_SCDMA_39);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[15];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            //writeLog_CheckPointResult(true, "StationEmulator call DUT successfully");
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isDutPhoneConnected && isStationEmulatorConnected, "Check the phone connection while call finished.");
                            #region Check call connection
                            //Thread.Sleep(actionDelay);
                            //while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            //{
                            //    Thread.Sleep(checkStatusInterval_inMilliseconds);
                            //    if (IsPhoneConnected)
                            //    {
                            //        writeLog_CheckPointResult(true, "Check the phone connection");
                            //    }
                            //    else
                            //    {
                            //        writeLog_CheckPointResult(false, "Check the phone connection");
                            //        break;
                            //    }
                            //}
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {
                            //writeLog_CheckPointResult(false, "DUT call station emulator unsuccessfully");
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }

            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0017_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.GSM_EGSM_FULL);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[16];
            checkStatusInterval_inMilliseconds = 30000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            #region switch the signal channel
            se8960.SetTrafficChannel(_middleChannelNumber);
            Logger.WriteLog("Action", " Set Traffic Channel to the middle one : " + _middleChannelNumber);
            #endregion switch the signal channel
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            //writeLog_CheckPointResult(true, "DUT call station emulator successfully");
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(10000);
                            #region Check call connection
                            while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            {
                                Thread.Sleep(checkStatusInterval_inMilliseconds);
                                if(!writeLog_CheckPointResult(waitForPhoneConnected,"Check the phone connection"))
                                {
                                    break;
                                }
                            }
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {
                            //writeLog_CheckPointResult(false,"DUT call station emulator unsuccessfully");
                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0018_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.DCS_1800_FULL);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[17];
            checkStatusInterval_inMilliseconds = 30000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            #region switch the signal channel
            se8960.SetTrafficChannel(_middleChannelNumber);
            Logger.WriteLog("Action", " Set Traffic Channel to the middle one : " + _middleChannelNumber);
            #endregion switch the signal channel
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(10000);
                            #region Check call connection
                            while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            {
                                Thread.Sleep(checkStatusInterval_inMilliseconds);
                                if (!writeLog_CheckPointResult(waitForPhoneConnected, "Check the phone connection"))
                                {
                                    break;
                                }
                            }
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {

                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0019_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.UMTS_900);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[18];
            checkStatusInterval_inMilliseconds = 30000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            #region switch the signal channel
            se8960.SetCellChannel(_middleChannelNumber);
            Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
            #endregion switch the signal channel
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(10000);
                            #region Check call connection
                            while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            {
                                Thread.Sleep(checkStatusInterval_inMilliseconds);
                                if (!writeLog_CheckPointResult(waitForPhoneConnected, "Check the phone connection"))
                                {
                                    break;
                                }
                            }
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {

                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                            Thread.Sleep(actionDelay);
                        }                       
                        #endregion Check call connection after hang up
                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0020_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.UMTS_2100);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[19];
            checkStatusInterval_inMilliseconds = 30000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            #region switch the signal channel
            se8960.SetCellChannel(_middleChannelNumber);
            Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
            #endregion switch the signal channel
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(10000);
                            #region Check call connection
                            while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            {
                                Thread.Sleep(checkStatusInterval_inMilliseconds);
                                if (!writeLog_CheckPointResult(waitForPhoneConnected, "Check the phone connection"))
                                {
                                    break;
                                }
                            }
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {

                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                            Thread.Sleep(actionDelay);
                        }                     
                        #endregion Check call connection after hang up
                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0021_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.TD_SCDMA_34);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[20];
            checkStatusInterval_inMilliseconds = 30000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            #region switch the signal channel
            se8960.SetCellChannel(_middleChannelNumber);
            Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
            #endregion switch the signal channel
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(10000);
                            #region Check call connection
                            while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            {
                                Thread.Sleep(checkStatusInterval_inMilliseconds);
                                if (!writeLog_CheckPointResult(waitForPhoneConnected, "Check the phone connection"))
                                {
                                    break;
                                }
                            }
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {

                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                            Thread.Sleep(actionDelay);
                        }                       
                        #endregion Check call connection after hang up                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0022_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.TD_SCDMA_39);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[21];
            checkStatusInterval_inMilliseconds = 30000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            #region switch the signal channel
            se8960.SetCellChannel(_middleChannelNumber);
            Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
            #endregion switch the signal channel
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (dutCallStationEmulator(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(10000);
                            #region Check call connection
                            while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            {
                                Thread.Sleep(checkStatusInterval_inMilliseconds);
                                if (!writeLog_CheckPointResult(waitForPhoneConnected, "Check the phone connection"))
                                {
                                    break;
                                }
                            }
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {

                        }
                        dutHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        if (!writeLog_CheckPointResult(!checkWanConnectionFlag, "DUT hangs up"))
                        {
                            stationEmulatorHangsUp();
                            Thread.Sleep(actionDelay);
                        }                      
                        #endregion Check call connection after hang up
                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0101_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.UMTS_900);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[100];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorConnectToDut(defaultDialTimeout))
                        {
                            //writeLog_CheckPointResult(true, "StationEmulator call DUT successfully");
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isStationEmulatorConnected, "Check the data connection while call finished.");
                        }
                        else //Establish call timeout
                        {
                            //writeLog_CheckPointResult(false, "DUT call station emulator unsuccessfully");
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        bool isConnected = isStationEmulatorConnected;
                        if (!writeLog_CheckPointResult(!isConnected, "DUT hangs up"))
                        {
                            dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                       
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    } Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0102_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.UMTS_2100);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[101];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorConnectToDut(defaultDialTimeout))
                        {
                            //writeLog_CheckPointResult(true, "StationEmulator call DUT successfully");
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isStationEmulatorConnected, "Check the data connection while call finished.");
                        }
                        else //Establish call timeout
                        {
                            //writeLog_CheckPointResult(false, "DUT call station emulator unsuccessfully");
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        bool isConnected = isStationEmulatorConnected || isDutPhoneConnected;
                        if (!writeLog_CheckPointResult(!isConnected, "DUT hangs up"))
                        {
                            dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0103_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.TD_SCDMA_34);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[102];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorConnectToDut(defaultDialTimeout))
                        {
                            //writeLog_CheckPointResult(true, "StationEmulator call DUT successfully");
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isStationEmulatorConnected, "Check the data connection while call finished.");
                        }
                        else //Establish call timeout
                        {
                            //writeLog_CheckPointResult(false, "DUT call station emulator unsuccessfully");
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        bool isConnected = isStationEmulatorConnected || isDutPhoneConnected;
                        if (!writeLog_CheckPointResult(!isConnected, "DUT hangs up"))
                        {
                            dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0104_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.TD_SCDMA_39);            
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[103];
            checkStatusInterval_inMilliseconds = 5000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        #region switch the signal channel
                        if (loopCount % 50 == 0)
                        {
                            switch (loopCount / 50)
                            {
                                case 0:
                                    se8960.SetCellChannel(_middleChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
                                    break;
                                case 1:
                                    se8960.SetCellChannel(_highestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the highest one : " + _highestChannelNumber);
                                    break;
                                case 2:
                                    se8960.SetCellChannel(_lowestChannelNumber);
                                    Logger.WriteLog("Action", " Set signal (downlink) channel to the lowest one : " + _lowestChannelNumber);
                                    break;
                            }
                            Thread.Sleep(cellModifyDelay);
                        }
                        #endregion switch the signal channel
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorConnectToDut(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(callDuration_inSeconds * 1000);
                            writeLog_CheckPointResult(isStationEmulatorConnected, "Check the data connection while call finished.");

                        }
                        else //Establish call timeout
                        {
                            //writeLog_CheckPointResult(false, "DUT call station emulator unsuccessfully");
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        bool isConnected = isStationEmulatorConnected || isDutPhoneConnected;
                        if (!writeLog_CheckPointResult(!isConnected, "DUT hangs up"))
                        {
                            dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0105_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.UMTS_900);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[104];
            checkStatusInterval_inMilliseconds = 30000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            #region switch the signal channel
            se8960.SetCellChannel(_middleChannelNumber);
            Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
            #endregion switch the signal channel
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorConnectToDut(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(10000);
                            #region Check call connection
                            while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            {
                                Thread.Sleep(checkStatusInterval_inMilliseconds);
                                if (!writeLog_CheckPointResult(isStationEmulatorConnected, "Check the data connection"))
                                {
                                    break;
                                }
                            }
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {
                            //writeLog_CheckPointResult(false,"DUT call station emulator unsuccessfully");
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        bool isConnected = isStationEmulatorConnected || isDutPhoneConnected;
                        if (!writeLog_CheckPointResult(!isConnected, "Station Emulator hangs up"))
                        {
                            //dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        Thread.Sleep(loopDelay);
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0106_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.UMTS_2100);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[105];
            checkStatusInterval_inMilliseconds = 30000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            #region switch the signal channel
            se8960.SetCellChannel(_middleChannelNumber);
            Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
            #endregion switch the signal channel
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorConnectToDut(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(10000);
                            #region Check call connection
                            while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            {
                                Thread.Sleep(checkStatusInterval_inMilliseconds);
                                if (!writeLog_CheckPointResult(isStationEmulatorConnected, "Check the data connection"))
                                {
                                    break;
                                }
                            }
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {
                            //writeLog_CheckPointResult(false,"DUT call station emulator unsuccessfully");
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        bool isConnected = isStationEmulatorConnected || isDutPhoneConnected;
                        if (!writeLog_CheckPointResult(!isConnected, "Station Emulator hangs up"))
                        {
                            //dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0107_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.TD_SCDMA_34);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[106];
            checkStatusInterval_inMilliseconds = 30000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            #region switch the signal channel
            se8960.SetCellChannel(_middleChannelNumber);
            Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
            #endregion switch the signal channel
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorConnectToDut(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(10000);
                            #region Check call connection
                            while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            {
                                Thread.Sleep(checkStatusInterval_inMilliseconds);
                                bool isConnected = isStationEmulatorConnected || isDutPhoneConnected;
                                if (!writeLog_CheckPointResult(isConnected, "Check the data connection"))
                                {
                                    break;
                                }
                            }
                            #endregion Check call connection
                        }
                        else //Establish call timeout
                        {
                            //writeLog_CheckPointResult(false,"DUT call station emulator unsuccessfully");
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        bool isConnected1 = isStationEmulatorConnected || isDutPhoneConnected;
                        if (!writeLog_CheckPointResult(!isConnected1, "Station Emulator hangs up"))
                        {
                            //dutHangsUp();
                            //Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0108_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start=============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            setCellBand(ACTIONS_SetCellBand.TD_SCDMA_39);
            ADB_Process.SetSimCardsEnable("",
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM1),
                                          testcase.CurrentSimSlot.Equals(Wwan_TestCaseInfo.SimSlot.SIM2));
            Thread.Sleep(actionDelay);
            setCellPower(defaultCellPower);
            Thread.Sleep(actionDelay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[107];
            checkStatusInterval_inMilliseconds = 30000;
            callConnectedTime = DateTime.MinValue;
            loopCount = 0;
            Logger.WriteLog("TcInfo", "Loop times = " + loopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " secs");
            #region switch the signal channel
            se8960.SetCellChannel(_middleChannelNumber);
            Logger.WriteLog("Action", " Set signal (downlink) channel to the middle one : " + _middleChannelNumber);
            #endregion switch the signal channel
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end=============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    if (_pauseFlag)
                    {
                        CurrentRunningState = RunningState.Paused;
                    }
                    else
                    {
                        CurrentRunningState = RunningState.Running;
                        Logger.WriteLog("Loop", "--- Loop " + (loopCount + 1) + " start ---");
                        if (stationEmulatorConnectToDut(defaultDialTimeout))
                        {
                            callConnectedTime = DateTime.Now;
                            Thread.Sleep(10000);
                            #region Check call connection
                            while (DateTime.Now.Subtract(callConnectedTime).TotalSeconds < callDuration_inSeconds && _runFlag)
                            {
                                Thread.Sleep(checkStatusInterval_inMilliseconds);
                                if (!writeLog_CheckPointResult(isStationEmulatorConnected, "Check the data connection"))
                                {
                                    break;
                                }
                            }
                            #endregion Check call connection
                        }
                        stationEmulatorHangsUp();
                        Thread.Sleep(actionDelay);
                        #region Check call connection after hang up
                        bool isConnected = isStationEmulatorConnected || isDutPhoneConnected;
                        if (!writeLog_CheckPointResult(!isConnected, "Station Emulator hangs up"))
                        {
                            //dutHangsUp();
                            Thread.Sleep(actionDelay);
                        }
                        #endregion Check call connection after hang up
                        loopCount++;
                        totalLoopCount++;
                        Logger.WriteLog("Loop", "--- Loop " + loopCount + " end ---");
                    }
                    Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        int index_channel_GSM = 0;
        int index_channel_DCS = 0;
        bool isPhoneCallInitial = false;
        private void WA0201_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            Logger.WriteLog("TcInfo", "GSM 900 handover test start");
            #region Initial Settings
            Logger.WriteLog("Prerequisite", "=============  Initialization start =============");
            //if (!ADB_Process.IsReceivePhoneStateService_MC36_Running())
            //{
            //    ADB_Process.StartPhoneStateReceiverService_MC36(dutDevice.ID);
            //}
            //dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            //dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            int minStrength = -102;
            int maxStrength = -82;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[200];
            checkStatusInterval_inMilliseconds = 5000;
            int signalStrength = maxStrength;
            int retryCount = 0;
            setCellBand(ACTIONS_SetCellBand.GSM_EGSM_FULL);
            loopCount = 0;
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end =============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    index_channel_GSM = 0;
                    isPhoneCallInitial = true;
                    while (_runFlag && index_channel_GSM < channelTable_EGSM.Length)
                    {
                        retryCount = 0;
                        signalStrength = maxStrength;
                        setTrafficChannel(channelTable_EGSM[index_channel_GSM]);
                        if (index_channel_GSM == 0)
                        {
                            setCellPower(defaultCellPower);
                        }
                        bool bSetSignalStrengthSuccessfully = setDutSignalStrength(testcase.CurrentSimSlot, signalStrength, adjustSignalStrength_TimeoutInSeconds, adjustSignalStrength_Inaccuracy);
                        if (writeLog_CheckPointResult(bSetSignalStrengthSuccessfully,"set channel[" + channelTable_EGSM[index_channel_GSM] + "] to " + signalStrength + " db"))
                        {
                            #region Check phone connection, if disconnected,retry to get reconnection
                            bool bIsPhoneConnected = checkWanConnectionFlag;
                            if (isPhoneCallInitial)
                            {
                                isPhoneCallInitial = false;
                            }
                            else
                            {
                                writeLog_CheckPointResult(bIsPhoneConnected, "Check phone connection @channel[" + channelTable_EGSM[index_channel_GSM] + "]");
                            }
                            if (!bIsPhoneConnected)
                            {
                                while (_runFlag && retryCount <= 3)
                                {
                                    retryCount++;
                                    Logger.WriteLog(Logger.LogLevels.Warning, "Connect", "Try to establish the phone connection for the " + retryCount + " time(s).");
                                    if (dutCallStationEmulator(defaultDialTimeout, true))
                                    {
                                        Logger.WriteLog(Logger.LogLevels.Information, "Connect", "Try to establish the phone connection successfully.", true);
                                        break;
                                    }
                                    else
                                    {
                                        dutHangsUp();
                                        se8960.EndCall();
                                    }
                                }
                                //still could not establish call, ignore and try next one.
                                if (!checkWanConnectionFlag)
                                {
                                    index_channel_GSM++;
                                    continue;
                                }
                            }
                            #endregion Retry to get reconnection
                            do
                            {
                                Thread.Sleep(callDuration_inSeconds);
                                if (writeLog_CheckPointResult(waitForPhoneConnected, "Check the phone state @channel[" + channelTable_EGSM[index_channel_GSM] + "],signal strength = " + signalStrength))
                                {
                                    signalStrength -= 10;
                                    bSetSignalStrengthSuccessfully = setDutSignalStrength(testcase.CurrentSimSlot, signalStrength, adjustSignalStrength_TimeoutInSeconds, adjustSignalStrength_Inaccuracy);
                                    if (!writeLog_CheckPointResult(bSetSignalStrengthSuccessfully,"set channel[" + channelTable_EGSM[index_channel_GSM] + "] to " + signalStrength + " db"))
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            } while (_runFlag & signalStrength > minStrength);
                        }
                        index_channel_GSM++;
                    }
                    dutHangsUp();
                    se8960.EndCall();                    
                    loopCount++;
                    totalLoopCount++;
                    Thread.Sleep(loopDelay);                
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }            
            //ADB_Process.StopPhoneStateReceiverService_MC36(dutDevice.ID);
            dutHangsUp();
            se8960.EndCall();
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void WA0202_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("TcInfo", "DCS 1800 handover test start");
            Logger.WriteLog("Prerequisite", "=============  Initialization start =============");
            //if (!ADB_Process.IsReceivePhoneStateService_MC36_Running())
            //{
            //    ADB_Process.StartPhoneStateReceiverService_MC36(dutDevice.ID);
            //}
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            int minStrength = -102;
            int maxStrength = -82;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[201];
            checkStatusInterval_inMilliseconds = 5000;
            int signalStrength = maxStrength;
            int retryCount = 0;
            setCellBand(ACTIONS_SetCellBand.DCS_1800_FULL);
            loopCount = 0;
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end =============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    index_channel_DCS = 0;
                    isPhoneCallInitial = true;
                    while (_runFlag && index_channel_DCS < channelTable_DCS.Length)
                    {
                        retryCount = 0;
                        signalStrength = maxStrength;
                        setTrafficChannel(channelTable_DCS[index_channel_DCS]);
                        if (index_channel_DCS == 0)
                        {
                            setCellPower(defaultCellPower);
                        }
                        bool bSetSignalStrengthSuccessfully = setDutSignalStrength(testcase.CurrentSimSlot, signalStrength, adjustSignalStrength_TimeoutInSeconds, adjustSignalStrength_Inaccuracy);
                        if (writeLog_CheckPointResult(bSetSignalStrengthSuccessfully, "set channel[" + channelTable_DCS[index_channel_DCS] + "] to " + signalStrength + " db"))
                        {
                            #region Check phone connection, if disconnected,retry to get reconnection
                            bool bIsPhoneConnected = checkWanConnectionFlag;
                            if (isPhoneCallInitial)
                            {
                                isPhoneCallInitial = false;
                            }
                            else
                            {
                                writeLog_CheckPointResult(bIsPhoneConnected, "Check phone connection @channel[" + channelTable_DCS[index_channel_DCS] + "]");
                            }
                            if (!bIsPhoneConnected)
                            {
                                while (_runFlag && retryCount <= 3)
                                {
                                    retryCount++;
                                    Logger.WriteLog(Logger.LogLevels.Warning, "Connect", "Try to establish the phone connection for the " + retryCount + " time(s).");
                                    if (dutCallStationEmulator(defaultDialTimeout, true))
                                    {
                                        Logger.WriteLog(Logger.LogLevels.Information, "Connect", "Try to establish the phone connection successfully.", true);
                                        break;
                                    }
                                    else
                                    {
                                        dutHangsUp();
                                        se8960.EndCall();
                                    }
                                }
                                //still could not establish call, ignore and try next one.
                                if (!checkWanConnectionFlag)
                                {
                                    index_channel_DCS++;
                                    continue;
                                }
                            }
                            #endregion Retry to get reconnection
                            do
                            {
                                Thread.Sleep(20000);
                                if (writeLog_CheckPointResult(waitForPhoneConnected, "Check the phone state @channel[" + channelTable_DCS[index_channel_DCS] + "],signal strength = " + signalStrength))
                                {
                                    signalStrength -= 10;
                                    bSetSignalStrengthSuccessfully = setDutSignalStrength(testcase.CurrentSimSlot, signalStrength, adjustSignalStrength_TimeoutInSeconds, adjustSignalStrength_Inaccuracy);
                                    if (!writeLog_CheckPointResult(bSetSignalStrengthSuccessfully, "set channel[" + channelTable_DCS[index_channel_DCS] + "] to " + signalStrength + " db"))
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            } while (_runFlag & signalStrength > minStrength);
                        }
                        index_channel_DCS++;
                    }
                    dutHangsUp();
                    se8960.EndCall();
                    loopCount++;
                    totalLoopCount++;
                    Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            //ADB_Process.StopPhoneStateReceiverService_MC36(dutDevice.ID);
            dutHangsUp();
            se8960.EndCall();
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }
        
        private void WA0203_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("TcInfo", "DCS/GSM handover test start");
            Logger.WriteLog("Prerequisite", "=============  Initialization start =============");
            //if (!ADB_Process.IsReceivePhoneStateService_MC36_Running())
            //{
            //    ADB_Process.StartPhoneStateReceiverService_MC36(dutDevice.ID);
            //}
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            bool isGsmMode = true;  //switch GSM or DCS mode 
            int minStrength = -102;
            int maxStrength = -82;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[202];
            checkStatusInterval_inMilliseconds = 5000;
            int channel = 0;
            //int signalStrength_GSM = maxStrength;
            //int signalStrength_DCS = maxStrength;
            int signalStrength = maxStrength;
            int retryCount = 0;
            setCellBand(ACTIONS_SetCellBand.GSM_EGSM_FULL);
            loopCount = 0;            
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end =============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    index_channel_DCS = 0;
                    index_channel_GSM = 0;
                    isPhoneCallInitial = true;
                    isGsmMode = true;
                    while (_runFlag && ((isGsmMode && index_channel_GSM < channelTable_EGSM.Length) || (!isGsmMode && index_channel_DCS < channelTable_DCS.Length)))
                    {
                        retryCount = 0;
                        if (isGsmMode)
                        {
                            setCellBand(ACTIONS_SetCellBand.GSM_EGSM_BAND);
                            Thread.Sleep(actionDelay);
                            channel = channelTable_EGSM[index_channel_GSM];
                        }
                        else
                        {
                            setCellBand(ACTIONS_SetCellBand.DCS_1800_BAND);
                            Thread.Sleep(actionDelay);
                            channel = channelTable_DCS[index_channel_DCS];
                        }
                        setTrafficChannel(channel);
                        if (index_channel_GSM == 0)
                        {
                            setCellPower(defaultCellPower);
                        }
                        Thread.Sleep(adjustSignalStrength_ModifyStrengthDelay);
                        signalStrength = maxStrength;

                        bool bSetSignalStrengthSuccessfully = setDutSignalStrength(testcase.CurrentSimSlot, signalStrength, adjustSignalStrength_TimeoutInSeconds, adjustSignalStrength_Inaccuracy);
                         if (writeLog_CheckPointResult(bSetSignalStrengthSuccessfully, "set channel[" + channel + "] to " + signalStrength + " db"))
                         {
                             #region Check phone connection, if disconnected,retry 3 times to get reconnection
                             bool bIsPhoneConnected = checkWanConnectionFlag;
                             if (isPhoneCallInitial)
                             {
                                 isPhoneCallInitial = false;
                             }
                             else
                             {
                                 writeLog_CheckPointResult(bIsPhoneConnected, "Check phone connection @channel[" + channel + "]");
                             }
                             if (!bIsPhoneConnected)
                             {
                                while (_runFlag && retryCount <= 3)
                                {
                                    retryCount++;
                                    Logger.WriteLog(Logger.LogLevels.Warning, "Connect", "Try to establish the phone connection for the " + retryCount + " time(s).");
                                    if (dutCallStationEmulator(defaultDialTimeout,true))
                                    {
                                        Logger.WriteLog(Logger.LogLevels.Information, "Connect", "Try to establish the phone connection successfully.", true);
                                        break;
                                    }
                                    else
                                    {
                                        dutHangsUp();
                                        se8960.EndCall();
                                    }
                                }
                                if (!checkWanConnectionFlag)
                                {
                                    if (isGsmMode)
                                    {
                                        index_channel_GSM++;
                                    }
                                    else
                                    {
                                        index_channel_DCS++;
                                    }
                                    if ((index_channel_GSM < channelTable_EGSM.Length) && (index_channel_DCS < channelTable_DCS.Length))  //任何一個channel 到達maxChannel後，就不再換mode
                                    {
                                        isGsmMode = !isGsmMode;
                                    }
                                    continue;
                                }
                            }
                            #endregion Retry 3 times to get reconnection
                            do
                            {
                                Thread.Sleep(20000);
                                if (writeLog_CheckPointResult(waitForPhoneConnected, "Check the phone state @channel[" + channelTable_DCS[index_channel_DCS] + "],signal strength = " + signalStrength))
                                {
                                    signalStrength -= 10;
                                    bSetSignalStrengthSuccessfully = setDutSignalStrength(testcase.CurrentSimSlot, signalStrength, adjustSignalStrength_TimeoutInSeconds, adjustSignalStrength_Inaccuracy);
                                    if (!writeLog_CheckPointResult(bSetSignalStrengthSuccessfully, "set channel[" + channel + "] to " + signalStrength + " db"))
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            } while (_runFlag & signalStrength > minStrength);
                        }

                        if (isGsmMode)
                        {
                            index_channel_GSM++;
                        }
                        else
                        {
                            index_channel_DCS++;
                        }

                        if (index_channel_GSM >= channelTable_EGSM.Length)
                        {
                            isGsmMode = false;
                        }
                        else if (index_channel_DCS >= channelTable_DCS.Length)
                        {
                            isGsmMode = true;
                        }
                        else
                        {
                            isGsmMode = !isGsmMode;
                        }                      
                    }
                    dutHangsUp();
                    se8960.EndCall();
                    loopCount++;
                    totalLoopCount++;
                    Thread.Sleep(loopDelay);
                }
            }
            catch (Exception exx)
            {
            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }            
            //ADB_Process.StopPhoneStateReceiverService_MC36(dutDevice.ID);
            dutHangsUp();
            se8960.EndCall();
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }
        
        private void DBG0001_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("TcInfo", testcase.Name +" start");
            Logger.WriteLog("Prerequisite", "=============  Initialization start =============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            callDuration_inSeconds = callDurationOfTCs[1001];
            checkStatusInterval_inMilliseconds = 5000;
            #region GetProperties
            bool bG1_Enable = (bool)testcase.GetProperty("G1_Enable");
            int iG1_CellPower1 = (int)testcase.GetProperty("G1_CellPower1");
            int iG1_CellPower2 = (int)testcase.GetProperty("G1_CellPower2");
            int iG1_CellPower3 = (int)testcase.GetProperty("G1_CellPower3");
            int iG1_Delay1 = (int)testcase.GetProperty("G1_Delay1");
            int iG1_Delay2 = (int)testcase.GetProperty("G1_Delay2");
            int iG1_Delay3 = (int)testcase.GetProperty("G1_Delay3");

            bool bG2_Enable = (bool)testcase.GetProperty("G2_Enable");
            int iG2_CellPower1 = (int)testcase.GetProperty("G2_CellPower1");
            int iG2_CellPower2 = (int)testcase.GetProperty("G2_CellPower2");
            int iG2_CellPower3 = (int)testcase.GetProperty("G2_CellPower3");
            int iG2_Delay1 = (int)testcase.GetProperty("G2_Delay1");
            int iG2_Delay2 = (int)testcase.GetProperty("G2_Delay2");
            int iG2_Delay3 = (int)testcase.GetProperty("G2_Delay3");

            bool bG3_Enable = (bool)testcase.GetProperty("G3_Enable");
            int iG3_CellPower1 = (int)testcase.GetProperty("G3_CellPower1");
            int iG3_CellPower2 = (int)testcase.GetProperty("G3_CellPower2");
            int iG3_CellPower3 = (int)testcase.GetProperty("G3_CellPower3");
            int iG3_Delay1 = (int)testcase.GetProperty("G3_Delay1");
            int iG3_Delay2 = (int)testcase.GetProperty("G3_Delay2");
            int iG3_Delay3 = (int)testcase.GetProperty("G3_Delay3");

            bool bG4_Enable = (bool)testcase.GetProperty("G4_Enable");
            int iG4_CellPower1 = (int)testcase.GetProperty("G4_CellPower1");
            int iG4_CellPower2 = (int)testcase.GetProperty("G4_CellPower2");
            int iG4_CellPower3 = (int)testcase.GetProperty("G4_CellPower3");
            int iG4_CellPower4 = (int)testcase.GetProperty("G4_CellPower4");
            int iG4_Delay1 = (int)testcase.GetProperty("G4_Delay1");
            int iG4_Delay2 = (int)testcase.GetProperty("G4_Delay2");
            int iG4_Delay3 = (int)testcase.GetProperty("G4_Delay3");
            int iG4_Delay4 = (int)testcase.GetProperty("G4_Delay4");
            #endregion GetProperties
            #region Set band
            ACTIONS_SetCellBand action = ACTIONS_SetCellBand.UMTS_2100;
            switch (testcase.CurrentBand)
            {
                case Wwan_TestCaseInfo.Band.DCS_1800:
                    action = ACTIONS_SetCellBand.DCS_1800_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_850:
                    action = ACTIONS_SetCellBand.EGPRS_850;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_DCS:
                    action = ACTIONS_SetCellBand.EGPRS_DCS;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_EGSM:
                    action = ACTIONS_SetCellBand.EGPRS_EGSM;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_PCS:
                    action = ACTIONS_SetCellBand.EGPRS_PCS;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_850:
                    action = ACTIONS_SetCellBand.GPRS_850;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_DCS:
                    action = ACTIONS_SetCellBand.GPRS_DCS;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_EGSM:
                    action = ACTIONS_SetCellBand.GPRS_EGSM;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_PCS:
                    action = ACTIONS_SetCellBand.GPRS_PCS;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_850:
                    action = ACTIONS_SetCellBand.GSM_850_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_900:
                    action = ACTIONS_SetCellBand.GSM_900_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_EGSM:
                    action = ACTIONS_SetCellBand.GSM_EGSM_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_PCS:
                    action = ACTIONS_SetCellBand.GSM_PCS_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.TD_SCDMA_34:
                    action = ACTIONS_SetCellBand.TD_SCDMA_34;
                    break;
                case Wwan_TestCaseInfo.Band.TD_SCDMA_39:
                    action = ACTIONS_SetCellBand.TD_SCDMA_39;
                    break;
                case Wwan_TestCaseInfo.Band.UMTS_2100:
                    action = ACTIONS_SetCellBand.UMTS_2100;
                    break;
                case Wwan_TestCaseInfo.Band.UMTS_900:
                    action = ACTIONS_SetCellBand.UMTS_900;
                    break;
                default:
                    break;
            }
            setCellBand(action);
            #endregion Set band
            se8960.SetCellChannel(testcase.CellChannel);
            loopCount = 0;
            const int iGroupLoopDelay = 5000;
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end =============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    loopCount++;
                    totalLoopCount++;
                    #region Group 1
                    if (_runFlag && bG1_Enable)
                    {
                        se8960.SetCellPower(iG1_CellPower1);
                        Thread.Sleep(iG1_Delay1 * 1000);
                        //setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2, iG1_CellPower2, 60);
                        se8960.SetCellPower(iG1_CellPower2);
                        Thread.Sleep(iG1_Delay2 * 1000);
                        //setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2, iG1_CellPower3, 60);
                        se8960.SetCellPower(iG1_CellPower3);
                        Thread.Sleep(iG1_Delay3 * 1000);
                        dutCallStationEmulator(defaultDialTimeout);
                        Thread.Sleep(5);
                        writeLog_CheckPointResult(waitForPhoneConnected, "Check MO call state [" + loopCount + "-1]");
                        se8960.EndCall();
                        Thread.Sleep(iGroupLoopDelay);
                    }
                    #endregion Group 1

                    #region Group 2
                    if (_runFlag && bG2_Enable)
                    {
                        se8960.SetCellPower(iG2_CellPower1);
                        Thread.Sleep(iG2_Delay1 * 1000);
                        se8960.SetCellPower(iG2_CellPower2);
                        //setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2, iG2_CellPower2, 60);
                        Thread.Sleep(iG2_Delay2 * 1000);
                        //setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2, iG2_CellPower3, 60);
                        se8960.SetCellPower(iG2_CellPower3);
                        Thread.Sleep(iG2_Delay3 * 1000);
                        dutCallStationEmulator(defaultDialTimeout);
                        Thread.Sleep(5);
                        writeLog_CheckPointResult(waitForPhoneConnected, "Check MO call state [" + loopCount + "-2]");
                        se8960.EndCall();
                        Thread.Sleep(iGroupLoopDelay);
                    }
                    #endregion Group 2

                    #region Group 3
                    if (_runFlag && bG3_Enable)
                    {
                        se8960.SetCellPower(iG3_CellPower1);
                        Thread.Sleep(iG3_Delay1 * 1000);
                        //setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2, iG3_CellPower2, 60);
                        se8960.SetCellPower(iG3_CellPower2);
                        Thread.Sleep(iG3_Delay2 * 1000);
                        //setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2, iG3_CellPower3, 60);
                        se8960.SetCellPower(iG3_CellPower3);
                        Thread.Sleep(iG3_Delay3 * 1000);
                        dutCallStationEmulator(defaultDialTimeout);
                        Thread.Sleep(5);
                        writeLog_CheckPointResult(waitForPhoneConnected, "Check MO call state [" + loopCount + "-3]");
                        se8960.EndCall();
                        Thread.Sleep(iGroupLoopDelay);
                    }
                    #endregion Group 3

                    #region Group 4
                    if (_runFlag && bG4_Enable)
                    {
                        se8960.SetCellPower(iG4_CellPower1);
                        Thread.Sleep(iG4_Delay1 * 1000);
                        //setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2, iG4_CellPower2, 60);
                        se8960.SetCellPower(iG4_CellPower2);
                        Thread.Sleep(iG4_Delay2 * 1000);
                        dutCallStationEmulator(defaultDialTimeout);
                        Thread.Sleep(5);
                        writeLog_CheckPointResult(waitForPhoneConnected, "Check MO call state [" + loopCount + "-4-1]");
                        se8960.EndCall();
                        Thread.Sleep(5);
                        //setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2, iG4_CellPower3, 60);
                        se8960.SetCellPower(iG4_CellPower3);
                        Thread.Sleep(iG4_Delay3 * 1000);
                        setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2, iG4_CellPower4, 60);
                        //se8960.SetCellPower(iG4_CellPower4);
                        Thread.Sleep(iG4_Delay4 * 1000);
                        dutCallStationEmulator(defaultDialTimeout);
                        Thread.Sleep(5);
                        writeLog_CheckPointResult(waitForPhoneConnected, "Check MO call state [" + loopCount + "-4-2]");
                        se8960.EndCall();
                        Thread.Sleep(iGroupLoopDelay);
                    }
                    #endregion Group 4

                    if (_runFlag)
                    {
                        Thread.Sleep(loopDelay);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }  
            dutHangsUp();
            se8960.EndCall();
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void DBG0002_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("TcInfo", testcase.Name + " start");
            Logger.WriteLog("Prerequisite", "=============  Initialization start =============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            //callDuration_inSeconds = callDurationOfTCs[1001];
            checkStatusInterval_inMilliseconds = 5000;
            #region GetProperties
            int ber_interval = (int)testcase.GetProperty("BER_Interval");
            int channel_duration = (int)testcase.GetProperty("Channel_Duration");
            List<String> channels = (List<String>)testcase.GetProperty("Channels");
            int rscp = (int)testcase.GetProperty("RSCP");
            int rscp_Inaccuracy = (int)testcase.GetProperty("RSCP_Inaccuracy");
            #endregion GetProperties
            #region Set band
            ACTIONS_SetCellBand action = ACTIONS_SetCellBand.UMTS_2100;
            switch (testcase.CurrentBand)
            {
                case Wwan_TestCaseInfo.Band.DCS_1800:
                    action = ACTIONS_SetCellBand.DCS_1800_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_850:
                    action = ACTIONS_SetCellBand.EGPRS_850;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_DCS:
                    action = ACTIONS_SetCellBand.EGPRS_DCS;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_EGSM:
                    action = ACTIONS_SetCellBand.EGPRS_EGSM;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_PCS:
                    action = ACTIONS_SetCellBand.EGPRS_PCS;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_850:
                    action = ACTIONS_SetCellBand.GPRS_850;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_DCS:
                    action = ACTIONS_SetCellBand.GPRS_DCS;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_EGSM:
                    action = ACTIONS_SetCellBand.GPRS_EGSM;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_PCS:
                    action = ACTIONS_SetCellBand.GPRS_PCS;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_850:
                    action = ACTIONS_SetCellBand.GSM_850_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_900:
                    action = ACTIONS_SetCellBand.GSM_900_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_EGSM:
                    action = ACTIONS_SetCellBand.GSM_EGSM_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_PCS:
                    action = ACTIONS_SetCellBand.GSM_PCS_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.TD_SCDMA_34:
                    action = ACTIONS_SetCellBand.TD_SCDMA_34;
                    break;
                case Wwan_TestCaseInfo.Band.TD_SCDMA_39:
                    action = ACTIONS_SetCellBand.TD_SCDMA_39;
                    break;
                case Wwan_TestCaseInfo.Band.UMTS_2100:
                    action = ACTIONS_SetCellBand.UMTS_2100;
                    break;
                case Wwan_TestCaseInfo.Band.UMTS_900:
                    action = ACTIONS_SetCellBand.UMTS_900;
                    break;
                default:
                    break;
            }
            setCellBand(action);
            #endregion Set band
            se8960.SetCellPower(rscp);
            loopCount = 0;
            isPhoneCallInitial = true;
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end =============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    loopCount++;
                    totalLoopCount++;
                    DateTime channelStartTime = DateTime.Now;
                    foreach (String channel in channels)
                    {
                        se8960.SetCellChannel(Convert.ToInt32(channel));
                        Thread.Sleep(cellModifyDelay);
                        channelStartTime = DateTime.Now;
                        if (writeLog_CheckPointResult(stationEmulatorCallDut(defaultDialTimeout), "Establish the phone connection."))
                        {
                            if (writeLog_CheckPointResult(setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2, rscp, 60, rscp_Inaccuracy), "Adjust RSSI of DUT"))
                            {
                                se8960.Init_BER(10240, true, -1);
                                startBERCatcher(ber_interval);
                                while (_runFlag && DateTime.Now.Subtract(channelStartTime).TotalMinutes < channel_duration)
                                {
                                    //Logger.WriteLog("BER", "Lookback Bit Error Ratio = " + se8960.LookbackBER);
                                    Thread.Sleep(10000);
                                }
                                stopBERCatcher();
                            }
                        }
                        stationEmulatorHangsUp();
                        if (_runFlag)
                        {
                            Thread.Sleep(loopDelay);
                        }
                        else
                        {
                            break;
                        }
                    }
                    stationEmulatorHangsUp();
                }
            }
            catch (Exception ex)
            {

            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            dutHangsUp();
            se8960.EndCall();
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        private void DBG0003_runnable(object objTc)
        {
            Wwan_TestCaseInfo testcase = objTc as Wwan_TestCaseInfo;
            #region Initial Settings
            Logger.WriteLog("TcInfo", testcase.Name + " start");
            Logger.WriteLog("Prerequisite", "=============  Initialization start =============");
            dutDevice.AirplaneMode.Enable = true;
            Thread.Sleep(flightMode_Delay);
            dutDevice.AirplaneMode.Enable = false;
            Thread.Sleep(flightMode_Delay);
            tcPassCheckPoint = 0;
            tcFailCheckPoint = 0;
            loopTimes = isDebugMode ? 1 : testcase.Loop;
            //callDuration_inSeconds = callDurationOfTCs[1001];
            checkStatusInterval_inMilliseconds = 5000;
            #region GetProperties
            int ber_interval = (int)testcase.GetProperty("BER_Interval");
            int channel_cycles = (int)testcase.GetProperty("Channel_Cycles");
            int channel_duration = (int)testcase.GetProperty("Channel_Duration");
            List<String> channels = (List<String>)testcase.GetProperty("Channels");
            int rscp_init = (int)testcase.GetProperty("RSCP_Init");
            int rscp_inaccuracy_init = (int)testcase.GetProperty("RSCP_Inaccuracy_Init");
            int rscp_high = (int)testcase.GetProperty("RSCP_High");
            int rscp_inaccuracy_high = (int)testcase.GetProperty("RSCP_Inaccuracy_High");
            int rscp_low = (int)testcase.GetProperty("RSCP_Low");
            int cyclesPerChannel = (int)testcase.GetProperty("CyclesPerChannel");
            #endregion GetProperties
            #region Set band
            ACTIONS_SetCellBand action = ACTIONS_SetCellBand.UMTS_2100;
            switch (testcase.CurrentBand)
            {
                case Wwan_TestCaseInfo.Band.DCS_1800:
                    action = ACTIONS_SetCellBand.DCS_1800_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_850:
                    action = ACTIONS_SetCellBand.EGPRS_850;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_DCS:
                    action = ACTIONS_SetCellBand.EGPRS_DCS;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_EGSM:
                    action = ACTIONS_SetCellBand.EGPRS_EGSM;
                    break;
                case Wwan_TestCaseInfo.Band.EGPRS_PCS:
                    action = ACTIONS_SetCellBand.EGPRS_PCS;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_850:
                    action = ACTIONS_SetCellBand.GPRS_850;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_DCS:
                    action = ACTIONS_SetCellBand.GPRS_DCS;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_EGSM:
                    action = ACTIONS_SetCellBand.GPRS_EGSM;
                    break;
                case Wwan_TestCaseInfo.Band.GPRS_PCS:
                    action = ACTIONS_SetCellBand.GPRS_PCS;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_850:
                    action = ACTIONS_SetCellBand.GSM_850_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_900:
                    action = ACTIONS_SetCellBand.GSM_900_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_EGSM:
                    action = ACTIONS_SetCellBand.GSM_EGSM_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.GSM_PCS:
                    action = ACTIONS_SetCellBand.GSM_PCS_FULL;
                    break;
                case Wwan_TestCaseInfo.Band.TD_SCDMA_34:
                    action = ACTIONS_SetCellBand.TD_SCDMA_34;
                    break;
                case Wwan_TestCaseInfo.Band.TD_SCDMA_39:
                    action = ACTIONS_SetCellBand.TD_SCDMA_39;
                    break;
                case Wwan_TestCaseInfo.Band.UMTS_2100:
                    action = ACTIONS_SetCellBand.UMTS_2100;
                    break;
                case Wwan_TestCaseInfo.Band.UMTS_900:
                    action = ACTIONS_SetCellBand.UMTS_900;
                    break;
                default:
                    break;
            }
            setCellBand(action);
            #endregion Set band
            se8960.SetCellPower(rscp_init);
            loopCount = 0;
            isPhoneCallInitial = true;
            Thread.Sleep(cellModifyDelay);
            Logger.WriteLog("Prerequisite", "============= Initialization end =============");
            #endregion Initial Settings
            try
            {
                while (_runFlag && loopCount < loopTimes)
                {
                    loopCount++;
                    totalLoopCount++;
                    foreach (String channel in channels)
                    {
                        startBERCatcher(ber_interval);
                        se8960.SetCellChannel(Convert.ToInt32(channel));
                        Thread.Sleep(cellModifyDelay);
                        setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2,rscp_init,40);
                        stationEmulatorCallDut(5000);
                        Thread.Sleep(10000);
                        se8960.SetCellPower(rscp_low);
                        Thread.Sleep(10000);
                        if (se8960.CallState != StationEmulator_8960.CallStates.Connected)
                        {
                            stationEmulatorCallDut(5000);
                        }
                        setDutSignalStrength(Wwan_TestCaseInfo.SimSlot.SIM2,rscp_high, 60);
                        Thread.Sleep(47000);
                        stopBERCatcher();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            if (tcFailCheckPoint == 0)
            {
                totalPassedTestCase++;
            }
            else
            {
                totalFailedTestCase++;
            }
            if (CurrentRunningState.Equals(RunningState.Stopping))
            {
                CurrentRunningState = RunningState.Stopped;
            }
            dutHangsUp();
            se8960.EndCall();
            Logger.WriteLog(Logger.LogTags.Summary.ToString(), "Summary of current TC, Pass = " + tcPassCheckPoint + ", Fail = " + tcFailCheckPoint);
            jhWaitOne.Set();
        }

        #endregion Test cases' runnable

        #region Basic actions

        public enum ACTIONS_SetCellBand
        {
            GSM_850_FULL,
            GSM_900_FULL,
            GSM_PCS_FULL,
            DCS_1800_FULL,
            GSM_EGSM_FULL,  
            UMTS_900,
            UMTS_2100,
            TD_SCDMA_34,
            TD_SCDMA_39,          
            GPRS_850,
            GPR_SPCS,
            GPRS_PCS,
            GPRS_DCS,
            GPRS_EGSM,
            EGPRS_850,
            EGPRS_PCS,
            EGPRS_DCS,
            EGPRS_EGSM,
            GSM_850_BAND,
            GSM_900_BAND,
            GSM_EGSM_BAND,
            GSM_PCS_BAND,
            DCS_1800_BAND
        };

        private void setCellBand(ACTIONS_SetCellBand actions)
        {
            try
            {
                String currentCellBand = actions.ToString().Replace("_FULL", "").Replace("_BAND", "");
                switch (actions)
                {
                    case ACTIONS_SetCellBand.GSM_850_FULL:
                        ADB_Process.SetMobileDataStatus(dutDevice.ID, false, 5000); // 2G only, disable DUT's mobile data
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.GSM_850;
                        se8960.Set_GSM_850();
                        break;
                    case ACTIONS_SetCellBand.GSM_850_BAND:
                        se8960.SetTrafficBand("GSM850");
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.GSM_850;
                        break;
                    case ACTIONS_SetCellBand.GSM_900_FULL:
                        ADB_Process.SetMobileDataStatus(dutDevice.ID, false, 5000);// 2G only, disable DUT's mobile data
                        se8960.Set_GSM_900();
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.GSM_900;
                        break;
                    case ACTIONS_SetCellBand.GSM_900_BAND:
                        se8960.SetTrafficBand("PGSM");
                        break;
                    case ACTIONS_SetCellBand.GSM_PCS_FULL:
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.GSM_900;
                        ADB_Process.SetMobileDataStatus(dutDevice.ID, false, 5000); // 2G only, disable DUT's mobile data
                        se8960.Set_GSM_PCS();
                        break;
                    case ACTIONS_SetCellBand.GSM_PCS_BAND:
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.GSM_PCS;
                        se8960.SetTrafficBand("PCS");
                        break;
                    case ACTIONS_SetCellBand.DCS_1800_FULL:
                        ADB_Process.SetMobileDataStatus(dutDevice.ID, false, 5000); // 2G only, disable DUT's mobile data
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.DCS_1800;
                        se8960.Set_GSM_DCS();
                        break;
                    case ACTIONS_SetCellBand.DCS_1800_BAND:
                        se8960.SetTrafficBand("DCS");
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.DCS_1800;
                        break;                        
                    case ACTIONS_SetCellBand.UMTS_900:
                        ADB_Process.SetMobileDataStatus(dutDevice.ID, true, 5000); // 3G , enable DUT's mobile data
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.UMTS_900;
                        se8960.Set_UMTS_900();
                        break;
                    case ACTIONS_SetCellBand.UMTS_2100:
                        ADB_Process.SetMobileDataStatus(dutDevice.ID, true, 5000); // 3G , enable DUT's mobile data
                        se8960.Set_UMTS_2100();
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.UMTS_2100;
                        break;
                    case ACTIONS_SetCellBand.TD_SCDMA_34:
                        ADB_Process.SetMobileDataStatus(dutDevice.ID, true, 5000); // 3G , enable DUT's mobile data
                        se8960.Set_TD_SCDMA_B34();
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.TD_SCDMA_34;
                        break;
                    case ACTIONS_SetCellBand.TD_SCDMA_39:
                        ADB_Process.SetMobileDataStatus(dutDevice.ID, true, 5000); // 3G , enable DUT's mobile data
                        se8960.Set_TD_SCDMA_B39();
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.TD_SCDMA_39;
                        break;
                    case ACTIONS_SetCellBand.GSM_EGSM_BAND:
                        se8960.SetTrafficBand("EGSM");
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.GSM_EGSM;
                        break;
                    case ACTIONS_SetCellBand.GSM_EGSM_FULL:
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.GSM_EGSM;
                        ADB_Process.SetMobileDataStatus(dutDevice.ID, false, 5000); // 2G only, disable DUT's mobile data
                        se8960.Set_GSM_EGSM();
                        break;
                    case ACTIONS_SetCellBand.GPRS_850:
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.GPRS_850;
                        se8960.Set_GPRS_850();
                        break;
                    case ACTIONS_SetCellBand.GPRS_PCS:
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.GPRS_PCS;
                        se8960.Set_GPRS_PCS();
                        break;
                    case ACTIONS_SetCellBand.GPRS_DCS:
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.GPRS_DCS;
                        se8960.Set_GPRS_DCS();
                        break;
                    case ACTIONS_SetCellBand.GPRS_EGSM:
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.GPRS_EGSM;
                        se8960.Set_GPRS_EGSM();
                        break;
                    case ACTIONS_SetCellBand.EGPRS_850:
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.EGPRS_850;
                        se8960.Set_EGPRS_850();
                        break;
                    case ACTIONS_SetCellBand.EGPRS_PCS:
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.EGPRS_PCS;
                        se8960.Set_EGPRS_PCS();
                        break;
                    case ACTIONS_SetCellBand.EGPRS_DCS:
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.EGPRS_DCS;
                        se8960.Set_EGPRS_DCS();
                        break;
                    case ACTIONS_SetCellBand.EGPRS_EGSM:
                        currentTestcase.CurrentBand = Wwan_TestCaseInfo.Band.EGPRS_EGSM;
                        se8960.Set_EGPRS_EGSM();
                        break;
                    default:
                        currentCellBand = "Unknow";
                        break;
                }
                Logger.WriteLog(Logger.LogLevels.Information, Logger.LogTags.Action.ToString(), "Set cell band = " + currentCellBand,false);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogLevels.Error,Logger.LogTags.ToolInfo.ToString(), "Set cell band exception : " + ex.Message, true);
            }
            Thread.Sleep(100);
        }

        private void setCellPower(double powerValue)
        {            
            try
            {
                se8960.SetCellPower(powerValue);
                //currentCellPower = powerValue;
                Logger.WriteLog(Logger.LogLevels.Information,Logger.LogTags.Action.ToString(), "Set cell power = " + powerValue.ToString("0.00")+" db", false);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogLevels.Error, Logger.LogTags.ToolInfo.ToString(), "Set cell power exception : " + ex.Message, true);
            }
            Thread.Sleep(100);          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns>Establish the call connection successfully or not</returns>
        /// 
        private bool dutCallStationEmulator(int timeout)
        {
            return dutCallStationEmulator(timeout, false);
        }

        private bool dutCallStationEmulator(int timeout,bool isScrennCapture)
        {
            bool result = false;
            Logger.WriteLog(Logger.LogLevels.Debug, Logger.LogTags.Action.ToString(), "DUT call station emulator, timeout = " + timeout + " ms");
            dutDevice.Telephony.Dial_InsLib("000");
            Thread.Sleep(3000);            
            if (isScrennCapture)
            {
                Thread.Sleep(400);
                CaptureScreen("Dialing");
            }
            DateTime dialTime = DateTime.Now;
            bool timeout_flag = false;
            try
            {
                do
                {
                    timeout_flag = DateTime.Now.Subtract(dialTime).TotalMilliseconds > timeout;
                    try
                    {
                        StationEmulator_8960.CallStates state = se8960.CallState;
                        if(isDutPhoneConnected && state.Equals(StationEmulator_8960.CallStates.Connected))
                        {
                            result = true;
                            break;
                        }
                        else if(state.Equals(StationEmulator_8960.CallStates.Idle)){
                            Thread.Sleep(1000);
                            dutHangsUp();
                            Thread.Sleep(1000);
                            dutDevice.Telephony.Dial_InsLib("000");
                            Thread.Sleep(3000);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(Logger.LogLevels.Error, "Error", "Dut call stationEmulator exception, message = " + ex.Message, true);
                    }
                    Thread.Sleep(1500);
                } while (!timeout_flag && _runFlag);
                writeLog_CheckPointResult(result, "DUT call station emulator");
            }
            catch (Exception exxx)
            {

            }
            return result;
        }

        private bool stationEmulatorCallDut(int timeout)
        {
            bool result = false, isTimeout = false;
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Station emulator call DUT , timeout = " + timeout + " ms");
            DateTime dialTime = DateTime.Now;
            try
            {
                se8960.Dial();
                Thread.Sleep(actionDelay);
                Logger.WriteLog(Logger.LogLevels.Debug,Logger.LogTags.Detail.ToString(), "Dailing, station emulator state = " + se8960.CallState.ToString());
                Logger.WriteLog(Logger.LogLevels.Debug,Logger.LogTags.Detail.ToString(), "Dailing, DUT phone state = " + getCallState());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogLevels.Error, Logger.LogTags.ToolInfo.ToString(), "StationEmulator call the DUT exception : " + ex.Message, true);
            }
            Thread.Sleep(3000);
            result = dutDevice.Telephony.AnswerCall_InsLib(timeout);// pick up phone call
            if (result)
            {
                do
                {
                    try
                    {
                        result = isDutPhoneConnected && se8960.CallState.Equals(StationEmulator_8960.CallStates.Connected);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(Logger.LogLevels.Error, Logger.LogTags.ToolInfo.ToString(), "Station emulator call the DUT, check connection exception : " + ex.Message, true);
                    }
                    isTimeout = DateTime.Now.Subtract(dialTime).TotalMilliseconds > timeout;
                    Thread.Sleep(1500);
                } while ((!(result || isTimeout)) && _runFlag);
            }
            writeLog_CheckPointResult(result, "Station emulator call the DUT");
            return result;
        }

        private bool stationEmulatorConnectToDut(int timeout)
        {
            bool result = false, isTimeout = false;
            Logger.WriteLog(Logger.LogLevels.Information, Logger.LogTags.Action.ToString(), "Station emulator connect to the DUT, timeout = " + timeout + " ms");
            DateTime dialTime = DateTime.Now;
            try
            {
                se8960.Dial();
                Thread.Sleep(actionDelay);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogLevels.Error, Logger.LogTags.ToolInfo.ToString(), "StationEmulator connect to the DUT exception : " + ex.Message, true);
            }
            Thread.Sleep(3000);
            do
            {
                try
                {
                    result = se8960.CallState.Equals(StationEmulator_8960.CallStates.Connected);
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(Logger.LogLevels.Error, Logger.LogTags.ToolInfo.ToString(), "Station emulator connect to the DUT, check connection exception : " + ex.Message, true);
                }
                isTimeout = DateTime.Now.Subtract(dialTime).TotalMilliseconds > timeout;
                Thread.Sleep(1500);
            } while ((!(result || isTimeout)) && _runFlag);
            writeLog_CheckPointResult(result, "Station emulator establish a data connection to DUT");
            return result;
        }

        private clsTelephony.CallStates getCallState()
        {
            clsTelephony.CallStates newState = dutDevice.Telephony.CallState;
            if (!currentCallState.Equals(newState))
            {
                currentCallState = newState;
            }
            if (CallStateChangedEventHandler != null)
            {
                CallStateChangedEventHandler.Invoke(this, new CallStateChangedEventArgs(currentCallState));
            }
            Logger.WriteLog(Logger.LogLevels.Debug,"DUT","Get dut phone state = "+newState.ToString());
            return newState;
        }

        private void stationEmulatorHangsUp()
        {
            try
            {
                isEndCalling = true;
                se8960.EndCall();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogLevels.Error, Logger.LogTags.ToolInfo.ToString(), "StationEmulator hangs Up exception, message = " + ex.Message, true);
            }
            finally
            {
                isEndCalling = false;
            }
        }

        private void dutHangsUp()
        {
            try
            {
                isEndCalling = true;
                dutDevice.Telephony.EndCall_InsLib();
                getCallState();
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogLevels.Error, Logger.LogTags.ToolInfo.ToString(), "DUT hangs Up exception, message = " + ex.Message, true);
            }
            finally
            {
                isEndCalling = false;
            }
        }

        private void setTrafficChannel(int channel)
        {
            Logger.WriteLog(Logger.LogLevels.Debug,Logger.LogTags.Action.ToString(), "Set traffic channel to " + channel);
            se8960.SetTrafficChannel(channel);
        }
        
        private bool setDutSignalStrength(Wwan_TestCaseInfo.SimSlot sim, int strengthInDb, int timeoutInSeconds)
        {
            return setDutSignalStrength(sim,strengthInDb,timeoutInSeconds,2);
        }

        private bool setDutSignalStrength(Wwan_TestCaseInfo.SimSlot sim, int strengthInDb, int timeoutInSeconds, int inaccuracy)
        {
            bool isAutoAdjusted = false; //if have not auto-adjusted, never need re-check for hitting the target strength
            int retryLimit = 1;
            bool result = false;
            bool isTimeout = false;
            int signalStrength = -999;
            int cellPowerInStationEmulator = -999;
            int diff = 999;          
            int passTimesCount = 0;
            int retryCount = 0;
            timeoutInSeconds = timeoutInSeconds >= 60 ? timeoutInSeconds : 60;
            DateTime startTime = DateTime.Now;
            inaccuracy = Math.Abs(inaccuracy);
            Logger.WriteLog(Logger.LogLevels.Debug,Logger.LogTags.Action.ToString(), "Try to set DUT signal strength to " + strengthInDb + " db");
            do
            {
                #region Check if current signal strength meet to the target strength
                retryCount = 0;
                diff = 999;
                isAutoAdjusted = false;
                do
                {
                    retryCount++;
                    signalStrength = getSignalStrength(sim); //Get Signal Strength
                    if (signalStrength <= -999)
                    {
                        result = false;
                        isTimeout = true;
                        break;
                    }
                    else
                    {
                        retryLimit = isAutoAdjusted ? adjustSignalStrength_RetryLimit : 1;
                        diff = strengthInDb - signalStrength;
                        Logger.WriteLog(Logger.LogLevels.Debug,Logger.LogTags.Detail.ToString(), "Difference between current signal strength and target = " + diff + " db");
                        if (Math.Abs(diff) <= inaccuracy)
                        {
                            passTimesCount++;
                            if (!isAutoAdjusted) // Hit the target before auto-adjusting, result = PASS, no need to retry.
                            {
                                result = true;
                            }
                        }
                        if (passTimesCount >= adjustSignalStrength_HitTargetPassingCriteria)
                        {
                            result = true;
                        }
                        else
                        {
                            isTimeout = DateTime.Now.Subtract(startTime).TotalSeconds > timeoutInSeconds;
                            Thread.Sleep(adjustSignalStrength_CheckSignalStrengthInterval);
                        }
                    }
                } while (!(result || isTimeout) && (retryCount < retryLimit));
                #endregion Check if current signal strength meet to the target strength
                if (result || isTimeout)
                {
                    break;
                }
                else
                {
                    cellPowerInStationEmulator = se8960.CellPower;
                    int newCellPower = cellPowerInStationEmulator + diff;
                    isAutoAdjusted = true;
                    if (newCellPower <= 0 && newCellPower >= -150)
                    {
                        setCellPower(newCellPower);
                        Thread.Sleep(adjustSignalStrength_ModifyStrengthDelay);
                    }
                    else
                    {
                        isTimeout = true; // new signal strength is out of safe range, give up to adjust.
                    }
                }              
            } while (!(result || isTimeout)); 
            Logger.WriteLog(Logger.LogLevels.Debug,Logger.LogTags.Action.ToString(), "Set DUT signal strength result = " + (result?"Pass":"Fail")+", timeout = "+isTimeout);
            return result;
        }

        private int getSignalStrength(Wwan_TestCaseInfo.SimSlot sim)
        {
            int signalStrength = -999;
            bool isTimeout = false;
            if (sim.Equals(Wwan_TestCaseInfo.SimSlot.SIM2))
            {
                dutDevice.SIM2.RefreshState();
                signalStrength = dutDevice.SIM2.SignalStrength;
            }
            else
            {
                dutDevice.SIM1.RefreshState();
                signalStrength = dutDevice.SIM1.SignalStrength;
            }
            Logger.WriteLog(Logger.LogLevels.Verbose, "Action", "Get SIM" + (sim.Equals(Wwan_TestCaseInfo.SimSlot.SIM2) ? "2" : "1") + " strength = " + signalStrength + " db", false);
            if (DutSignalStengthChangedEventHandler != null)
            {
                DutSignalStengthChangedEventHandler.Invoke(this, new DutSignalStrengthChangedEventArgs(sim, signalStrength));
            }
            return signalStrength;
        }

        private bool waitForDutPhoneState(clsTelephony.CallStates expectedState, int timeout)
        {
            bool result = false;
            Logger.WriteLog(Logger.LogLevels.Verbose, "DutSta", "Wait for DUT phone state = \"" + expectedState.ToString() + "\"");
            DateTime dtStartTime = DateTime.Now;
            while (!result && DateTime.Now.Subtract(dtStartTime).TotalMilliseconds < timeout)
            {
                currentCallState = getCallState();
                result = currentCallState.Equals(expectedState);
                if (!result)
                {
                    Thread.Sleep(500);
                }
            }
            return result;
        }

        private bool waitForStationEmulatorPhoneState(StationEmulator_8960.CallStates expectedState,int timeout)//StationEmulatorState expectedState, int timeout)
        {
            bool result = false;
            Logger.WriteLog(Logger.LogLevels.Verbose, "StaEmu", "Wait station emulator cell state = \"" + expectedState.ToString() + "\"");
            DateTime dtStartTime = DateTime.Now;
            while (!result && DateTime.Now.Subtract(dtStartTime).TotalMilliseconds < timeout)
            {
                result = se8960.CallState.Equals(expectedState);
                if (!result)
                {
                    Thread.Sleep(500);
                }
            }
            return result;
        }

        #endregion Basic actions

        private bool writeLog_CheckPointResult(bool result, String message)
        {
            if (result)
            {
                totalPassCheckPoint++;
            }
            else
            {
                CaptureScreen("CheckPoint");
                totalFailCheckPoint++;               
            }
            String strPassFail = result ? "\"Pass\"" : "\"Fail\"";
            Logger.WriteLog(Logger.LogTags.CheckPoint.ToString(), message + ", result = " + strPassFail + " (" + tcPassCheckPoint + "/" + (tcFailCheckPoint + tcPassCheckPoint) + ")");
            return result;
        }
    
        public static void ReadTestCaseSettings()
        {
            XElement xeConfig = null;
            try
            {
                xeConfig = XElement.Load(pathConfiguration);
                if(xeConfig!=null)
                {
                    try{
                        defaultCellPower = Convert.ToDouble(xeConfig.Element("DefaultCellPower").Value);
                    }
                    catch{}
                    try{
                        cellModifyDelay = Convert.ToInt32(xeConfig.Element("CellModifyDelay").Value);
                    }
                    catch { }
                    try{
                        defaultDialTimeout = Convert.ToInt32(xeConfig.Element("DefaultDialTimeout").Value);
                    }
                    catch { }
                    try
                    {
                        XElement xeAdjustParams = xeConfig.Element("AdjustDutSignalStrengthParams");
                        try
                        {
                            adjustSignalStrength_CheckSignalStrengthInterval = Convert.ToInt32(xeAdjustParams.Element("CheckInterval").Value);
                        }
                        catch
                        {

                        }
                        try
                        {
                            adjustSignalStrength_Inaccuracy = Convert.ToInt32(xeAdjustParams.Element("Inaccuracy").Value);
                        }
                        catch
                        {

                        }
                        try
                        {
                            adjustSignalStrength_HitTargetPassingCriteria = Convert.ToInt32(xeAdjustParams.Element("HitTargetPassingCriteria").Value);
                        }
                        catch
                        {

                        }
                        try
                        {
                            adjustSignalStrength_RetryLimit = Convert.ToInt32(xeAdjustParams.Element("RetryLimit").Value);
                        }
                        catch
                        {

                        }
                        try
                        {
                            adjustSignalStrength_TimeoutInSeconds = Convert.ToInt32(xeAdjustParams.Element("TimeoutInSeconds").Value);
                        }
                        catch
                        {

                        }
                        try
                        {
                            adjustSignalStrength_ModifyStrengthDelay = Convert.ToInt32(xeAdjustParams.Element("ModifyStrengthDelay").Value);
                        }
                        catch
                        {

                        }
                    }
                    catch { }
                }
            }
            catch
            { }
        }
        
        public void SaveTestCaseSettings(double cellPower,int cellModifyDelay,int dialTimeout)
        {
            XElement xeConfig = new XElement("config");
            XElement xeCellPower = new XElement("DefaultCellPower");
            xeCellPower.Value = cellPower.ToString("0.00");
            XElement xeCellModifyDelay = new XElement("CellModifyDelay");
            xeCellModifyDelay.Value = cellModifyDelay.ToString();
            XElement xeDefaultDialTimeout = new XElement("DefaultDialTimeout");
            xeDefaultDialTimeout.Value = dialTimeout.ToString();
            xeConfig.Add(xeCellPower);
            xeConfig.Add(xeCellModifyDelay);
            xeConfig.Add(xeDefaultDialTimeout);
            xeConfig.Save(pathConfiguration);
        }

        String captureScreenSubFolderName = "";
        private void CaptureScreen(String identifyName)
        {            
            if (captureScreenIndex == 1)
            {
                captureScreenSubFolderName = DateTime.Now.ToString("yyyyMMdd_HHmmss")+"\\";
            }
            String fileName = pathScreenCaptureDirectory + captureScreenSubFolderName +"[" + (captureScreenIndex++).ToString("0000") + "]"+ identifyName + ".png";
            dutDevice.Auxiliary.CaptureScreen_InsLib(fileName);
            Logger.WriteLog("ScnCap", "Capture screen and save to " + fileName);            
        }
    }
        
    public class ProcedureProgressChangedEventArgs : EventArgs
    {
        public readonly int TotalTimes=0;
        public readonly int Counter = 0;
        public ProcedureProgressChangedEventArgs(int totalTimes,int counter)
        {
            TotalTimes = totalTimes;
            Counter = counter;
        }
    }

    public class TestResultUpdateEventArgs:EventArgs
    {
        public readonly int PassedCheckPoint = 0;
        public readonly int FailedCheckPoint = 0;
        public readonly int PassedTestCase = 0;
        public readonly int FailedTestCase = 0;
        public TestResultUpdateEventArgs(int passedCheckPoint, int failedCheckPoint, int passedTestCase, int failedTestCase)
        {
            PassedCheckPoint = passedCheckPoint;
            FailedCheckPoint = failedCheckPoint;
            PassedTestCase = passedTestCase;
            FailedTestCase = failedTestCase;
        }
    }

    public class ProcedureProcessorRunningStateChangedEventArgs : EventArgs
    {
        public readonly ProcedureProcessor_8960.RunningState State;
        public ProcedureProcessorRunningStateChangedEventArgs(ProcedureProcessor_8960.RunningState state)
        {
            State = state;
        }
    }
    
    public class ThreadStartWithTcInfo
    {
        public readonly Wwan_TestCaseInfo CurrentTC;
        public readonly ParameterizedThreadStart TcRunnable;
        public ThreadStartWithTcInfo(Wwan_TestCaseInfo tc, ParameterizedThreadStart tsr)
        {
            CurrentTC = tc;
            TcRunnable = tsr;
        }
    }

    public class DutSignalStrengthChangedEventArgs : EventArgs
    {
        public Wwan_TestCaseInfo.SimSlot Slot;
        public readonly int SignalStrength;
        public DutSignalStrengthChangedEventArgs(com.usi.shd1_tools.TestcasePackage.Wwan_TestCaseInfo.SimSlot slot,int strength)
        {
            Slot = slot;
            SignalStrength = strength;
        }
    }

    public class CallStateChangedEventArgs : EventArgs
    {
        public clsTelephony.CallStates State;
        public CallStateChangedEventArgs(clsTelephony.CallStates state)
        {
            State = state;
        }
    }
}
