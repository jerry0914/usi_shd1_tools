using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using com.usi.shd1_tools.TestcasePackage;
using System.Windows.Forms;
using dev.jerry_h.pc_tools.CommonLibrary;
using dev.jerry_h.pc_tools.AndroidLibrary;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    class ProcedureProcessor_DUT
    {
        private dutController _dut1;
        private dutController _dut2;
        private frmMain mainForm;
        private Thread tdRun;
        private int currentTestingIndex = -1;
        private const int testCaseInterval = 5000;
        private Thread tdTestCase;
        public bool isRunning
        {
            get
            {
                return runFlag;
            }
        }
        private bool runFlag = false;
        private bool infinityModeFlag = false;
        private bool debugModeFlag = false;
        private static JHWaitoneEventHandle waitoneEventHandle = new JHWaitoneEventHandle(false);
        private List<ThreadStart> lstTestCases = new List<ThreadStart>();
        private int tcLoopTimes = 0;
        private int tcLoopCounter = 0;
        private const int loopDelay = 3000;
        private const int tcDelay = 5000;
        private const int actionDelay = 1000;
        private int callDuration_inSeconds = 20;
        private const int cellModifyDelay = 20000;
        private bool isDialOK = false;
        private bool isPickupOK = false;
        private bool isCallDurationCheckOK = false;
        private bool isEndCallOK = false;
        private bool isProcessTimeout = false;
        private double processTimeout = 5000;
        private DateTime processStartTime = DateTime.MaxValue;
        public ProcedureProcessor_DUT(frmMain main)
        {
            mainForm = main;
        }

        public void Run(ListView.CheckedIndexCollection selectedTCs, dutController dut1, dutController dut2)
        {
            Run(false, false, selectedTCs, dut1, dut2);
        }

        public void Run(bool infinityMode, bool debugMode, ListView.CheckedIndexCollection selectedTCs, dutController dut1, dutController dut2)
        {
            if (tdRun != null)
            {
                Stop();
            }
            _dut1 = dut1;
            _dut2 = dut2;
            Logger.Initialize(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\" + dut1.DeviceID+"_"+dut2.DeviceID);
            infinityModeFlag = infinityMode;
            debugModeFlag = debugMode;
            runFlag = true;
            prepareTestCases(selectedTCs);
            tdRun = new Thread(testlist_Runnable);
            tdRun.Start();
        }

        private void testlist_Runnable()
        {
            while (currentTestingIndex < lstTestCases.Count && runFlag)
            {
                runTestCase(lstTestCases[currentTestingIndex]);
                waitoneEventHandle.WaitOne();
                currentTestingIndex++;
                #region reset test case index when infinity mode
                if (infinityModeFlag)
                {
                    if (currentTestingIndex >= lstTestCases.Count)
                    {
                        currentTestingIndex = 0;
                    }
                }
                #endregion reset test case index when infinity mode
                Thread.Sleep(testCaseInterval);
            }
            runFlag = false;
        }

        public void Stop()
        {
            runFlag = false;
            if (tdRun != null)
            {
                tdRun.Interrupt();
                tdRun = null;
            }
        }

        private void prepareTestCases(ListView.CheckedIndexCollection selectedTCs)
        {
            currentTestingIndex = 0;
            lstTestCases.Clear();
            foreach (int index in selectedTCs)
            {
                if (index == 0)
                {
                    lstTestCases.Add(new ThreadStart(testCase1_Runnable));
                }
                else if (index == 1)
                {
                    lstTestCases.Add(new ThreadStart(testCase2_Runnable));
                }
                if (index == 2)
                {
                    lstTestCases.Add(new ThreadStart(testCase3_Runnable));
                }
                if (index == 3)
                {
                    lstTestCases.Add(new ThreadStart(testCase4_Runnable));
                }
                if (index == 4)
                {
                    lstTestCases.Add(new ThreadStart(testCase5_Runnable));
                }
                if (index == 5)
                {
                    lstTestCases.Add(new ThreadStart(testCase6_Runnable));
                }
            }
        }

        private void runTestCase(ThreadStart tsTestCase)
        {
            tdTestCase = new Thread(tsTestCase);
            tdTestCase.Start();
        }
        
        private void dutPhoneStateChanged(object sender, DutPhoneStateChangedEventArgs dscea)
        {
            //_dut1.CurrentPhoneState
        }

        private double getTimeDiffInMilliseconds()
        {
            return getTimeDiffInMilliseconds(false);
        }

        private double getTimeDiffInMilliseconds(bool resetStartTime)
        {
            double diff = -1;
            if (resetStartTime)
            {
                processStartTime = DateTime.Now;
            }
            else
            {
                diff = DateTime.Now.Subtract(processStartTime).TotalMilliseconds;
            } 
            return diff;
        }

        #region Test cases

        private void testCase1_Runnable()
        {
            Logger.WriteLog("TC", "============= Test case WA1 start =============");
            tcLoopCounter = 0;
            tcLoopTimes = 200;
            dutController dialer = _dut1;
            dutController receiver = _dut2;
            dialer.StartStateMonitor();
            receiver.StartStateMonitor();
            Logger.WriteLog("TcInfo", "Loop times = " + tcLoopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " seconds");
            Thread.Sleep(3000);
            while (tcLoopCounter < tcLoopTimes && runFlag)
            {
                tcLoopCounter++;
                getTimeDiffInMilliseconds(true);  //Reset start time
                processTimeout = 10000;
                #region Dialer calls Receiver
                dialer.Dial(receiver.PhoneNumber);
                do
                {
                    isDialOK = receiver.CurrentPhoneState.Equals(dutController.DutPhoneState.Ringing);
                    isProcessTimeout = getTimeDiffInMilliseconds() > processTimeout;
                }
                while (!isDialOK && !isProcessTimeout);
                if (isDialOK)
                {
                    Logger.WriteLog("CheckPoint", "Make a call succefully, result = Pass");
                }
                else if(isProcessTimeout)
                {
                    Logger.WriteLog("CheckPoint", "Make a call unsuccefully, result = Fail");
                    dialer.EndCall();
                    receiver.EndCall();
                    Thread.Sleep(loopDelay);
                    continue;
                }
                #endregion Dialer calls Receiver
                #region Receiver Picks up
                isPickupOK = receiver.AnswerCall(10000);
                if (isPickupOK)
                {
                    Logger.WriteLog("CheckPoint", "Pick up the incoming call successfully, result = Pass");
                    dialer.CurrentPhoneState = dutController.DutPhoneState.Connected;
                }
                else
                {
                    Logger.WriteLog("CheckPoint", "Fail to pick up the incoming call, result = Fail");
                    dialer.EndCall();
                    receiver.EndCall();
                    Thread.Sleep(loopDelay);
                    continue;
                }
                #endregion Receiver Picks up
                Thread.Sleep(10000);
                #region Calling state check
                getTimeDiffInMilliseconds(true);  //Reset start time
                do
                {
                    isCallDurationCheckOK = dialer.isPhoneConnected && receiver.isPhoneConnected;
                    isProcessTimeout = ((int)getTimeDiffInMilliseconds() > (callDuration_inSeconds * 1000));
                    Thread.Sleep(1000);
                } while (isCallDurationCheckOK && !isProcessTimeout);
                if (isCallDurationCheckOK)
                {
                    Logger.WriteLog("CheckPoint", "Callint state check OK, calling duration = "+ callDuration_inSeconds+"seconds, result = Pass");
                }
                else
                {
                    Logger.WriteLog("CheckPoint", "Callint state checking is failed, result = Fail");
                    dialer.EndCall();
                    receiver.EndCall();
                    Thread.Sleep(loopDelay);
                    continue;
                }
                #endregion Calling state check
                #region End call
                processTimeout = 5000;
                dialer.EndCall();
                do
                {
                    isEndCallOK = !(dialer.isPhoneConnected || receiver.isPhoneConnected);
                    isProcessTimeout = processTimeout > getTimeDiffInMilliseconds();
                    Thread.Sleep(1000);
                } while (!isProcessTimeout && !isEndCallOK);
                if (isEndCallOK)
                {
                    Logger.WriteLog("CheckPoint", " Hangs up a call succeefully, result = Pass");
                }
                else
                {
                    Logger.WriteLog("CheckPoint", "Fail to hang up a call , result =Fail");
                }
                #endregion End call
                Thread.Sleep(10000);                
            }
            dialer.StopStateMonitor();
            receiver.StopStateMonitor();
            waitoneEventHandle.Set();
        }

        private void testCase2_Runnable()
        {
            _dut1.StartStateMonitor();
            _dut2.StartStateMonitor();

            _dut1.StopStateMonitor();
            _dut1.StopStateMonitor();
            waitoneEventHandle.Set();
        }

        private void testCase3_Runnable()
        {
            _dut1.StartStateMonitor();
            _dut2.StartStateMonitor();

            _dut1.StopStateMonitor();
            _dut1.StopStateMonitor();
            waitoneEventHandle.Set();
        }

        private void testCase4_Runnable()
        {
            _dut1.StartStateMonitor();
            _dut2.StartStateMonitor();

            _dut1.StopStateMonitor();
            _dut1.StopStateMonitor();
            waitoneEventHandle.Set();
        }

        private void testCase5_Runnable()
        {
            Logger.WriteLog("TC", "============= Test case WA5 start =============");
            tcLoopCounter = 0;
            tcLoopTimes = 200;
            dutController dialer = _dut1;
            dutController receiver = _dut2;
            dialer.StartStateMonitor();
            receiver.StartStateMonitor();
            processTimeout = 10000;
            Logger.WriteLog("TcInfo", "Loop times = " + tcLoopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " seconds");
            while (tcLoopCounter < tcLoopTimes && runFlag)
            {
                #region Dialer calls Receiver
                dialer.Dial(receiver.PhoneNumber);
                getTimeDiffInMilliseconds(true);
                do
                {
                    isDialOK = receiver.CurrentPhoneState.Equals(dutController.DutPhoneState.Ringing);
                    isProcessTimeout = getTimeDiffInMilliseconds() > processTimeout;
                }
                while (!isDialOK && !isProcessTimeout);
                if (isDialOK)
                {
                    Logger.WriteLog("CheckPoint", "Make a call succefully, result = Pass");
                }
                else if (isProcessTimeout)
                {
                    Logger.WriteLog("CheckPoint", "Make a call unsuccefully, result = Fail");
                    dialer.EndCall();
                    receiver.EndCall();
                    Thread.Sleep(loopDelay);
                    continue;
                }
                #endregion Dialer calls Receiver
                #region End call
                dialer.EndCall();
                processTimeout = 10000;
                getTimeDiffInMilliseconds(true);
                do
                {
                    isEndCallOK = !(dialer.isPhoneConnected || receiver.isPhoneConnected);
                    double  timediff = getTimeDiffInMilliseconds();
                    isProcessTimeout = timediff > processTimeout ;
                    
                    Thread.Sleep(1000);
                } while (!(isProcessTimeout || isEndCallOK));
                if (isEndCallOK)
                {
                    Logger.WriteLog("CheckPoint", " Hangs up a call succeefully, result = Pass");
                }
                else
                {
                    Logger.WriteLog("CheckPoint", "Fail to hang up a call , result =Fail");
                }
                #endregion End call
                Thread.Sleep(loopDelay);
            }
            _dut1.StopStateMonitor();
            _dut1.StopStateMonitor();
            waitoneEventHandle.Set();
        }

        private void testCase6_Runnable()
        {
           Logger.WriteLog("TC", "============= Test case WA6 start =============");
            tcLoopCounter = 0;
            tcLoopTimes = 200;
            dutController dialer = _dut1;
            dutController receiver = _dut2;
            dialer.StartStateMonitor();
            receiver.StartStateMonitor();
            processTimeout = 10000;
            Logger.WriteLog("TcInfo", "Loop times = " + tcLoopTimes + ", " + "duration of each call = " + callDuration_inSeconds + " seconds");
            while (tcLoopCounter < tcLoopTimes && runFlag)
            {
                #region Dialer calls Receiver
                dialer.Dial(receiver.PhoneNumber);
                getTimeDiffInMilliseconds(true);
                do
                {
                    isDialOK = receiver.CurrentPhoneState.Equals(dutController.DutPhoneState.Ringing);
                    isProcessTimeout = getTimeDiffInMilliseconds() > processTimeout;
                }
                while (!isDialOK && !isProcessTimeout);
                if (isDialOK)
                {
                    Logger.WriteLog("CheckPoint", "Make a call succefully, result = Pass");
                }
                else if (isProcessTimeout)
                {
                    Logger.WriteLog("CheckPoint", "Make a call unsuccefully, result = Fail");
                    dialer.EndCall();
                    receiver.EndCall();
                    Thread.Sleep(loopDelay);
                    continue;
                }
                #endregion Dialer calls Receiver
                #region End call
                receiver.EndCall();
                processTimeout = 20000;
                getTimeDiffInMilliseconds(true);
                do
                {
                    isEndCallOK = !(dialer.isPhoneConnected || receiver.isPhoneConnected);
                    double timediff = getTimeDiffInMilliseconds();
                    isProcessTimeout = timediff > processTimeout;

                    Thread.Sleep(1000);
                } while (!(isProcessTimeout || isEndCallOK));
                if (isEndCallOK)
                {
                    Logger.WriteLog("CheckPoint", " Hangs up a call succeefully, result = Pass");
                }
                else
                {
                    Logger.WriteLog("CheckPoint", "Fail to hang up a call , result =Fail");
                }
                #endregion End call
            }
            _dut1.StopStateMonitor();
            _dut1.StopStateMonitor();
            waitoneEventHandle.Set();
        }
        
        #endregion Test cases
    }
}
    