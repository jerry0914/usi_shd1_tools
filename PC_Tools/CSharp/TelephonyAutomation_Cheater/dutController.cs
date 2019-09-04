using System;
using System.Collections.Generic;
using System.Text;
using com.usi.shd1_tools.TestcasePackage;
using System.Threading;
using dev.jerry_h.pc_tools.CommonLibrary;
using dev.jerry_h.pc_tools.AndroidLibrary;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    public class dutController
    {
        public enum  DutPhoneState
        {
            Unknow = 0x0000,
            //Idle
            Idle = 0x0001,
            Rejected = 0x0003,
            EndCall = 0x0005,
            //Ring
            Ringing = 0x0010,
            //Offhook
            Offhook = 0x0100,
            Dialing = 0x0300,
            Connected = 0x0500,
            Answered = 0x0700,
        };
        public enum DutPhoneStateMask
        {
            IsIdleMask = 0x0001,
            IsRingingMask = 0x0010,
            IsOffhookMask = 0x0100
        };
        public bool isPhoneConnected
        {
            get
            {
                return((int)_currentPhoneState & (int)DutPhoneStateMask.IsOffhookMask)>0;
            }
        }

        public String DeviceID = "";
        public DutPhoneState CurrentPhoneState
        {
            get
            {
                return _currentPhoneState;
            }
            set
            {
                //only allow to change phone state form dialing to connected via outter process.
                if(_currentPhoneState.Equals(DutPhoneState.Dialing) && value.Equals(DutPhoneState.Connected))
                {
                    _currentPhoneState = value;
                    perviousPhoneState = value;
                    if (DutPhoneStateChangedEventHandler != null)
                    {
                        DutPhoneStateChangedEventHandler.Invoke(this, new DutPhoneStateChangedEventArgs(_currentPhoneState));
                    }
                }
            }
        }
        private DutPhoneState _currentPhoneState = DutPhoneState.Unknow;
        private DutPhoneState perviousPhoneState = DutPhoneState.Unknow;
        public dutController(String deviceID)
        {
            DeviceID = deviceID;
        }
        public EventHandler<DutPhoneStateChangedEventArgs> DutPhoneStateChangedEventHandler;
        private const int stateMonitorInterval = 2500;
        private const int checkStateTimeout = 2500;
        private Thread tdStateMonitor = null;
        private bool stateMonitorFlag = false;
        public   String  PhoneNumber = "";
        private bool isEndCallByMe = false;

        public void StartStateMonitor()
        {
            if (tdStateMonitor != null)
            {
                StopStateMonitor();
            }
            stateMonitorFlag = true;
            tdStateMonitor = new Thread(stateMonitor_Runnable);
            tdStateMonitor.Start();
        }

        public void StopStateMonitor()
        {
            stateMonitorFlag = false;
            if (tdStateMonitor != null)
            {
                tdStateMonitor.Interrupt();
                tdStateMonitor = null;
            }
        }

        private void stateMonitor_Runnable()
        {
            while (stateMonitorFlag)
            {
                getDutPhoneState();
                try {
                    Thread.Sleep(stateMonitorInterval);
                }
                catch(ThreadInterruptedException tie)
                {
                    //Do nothing
                }
            }
        }

        private DutPhoneState getDutPhoneState()
        {
            DutPhoneState newState = DutPhoneState.Unknow;
            String strCurrentState = ADB_Process.GetPhoneCallState(DeviceID, checkStateTimeout);
            switch (strCurrentState.ToUpper())
            {
                case "RINGING":
                    newState = DutPhoneState.Ringing;
                    perviousPhoneState = newState;
                    break;
                case "OFFHOOK":
                    newState = DutPhoneState.Offhook;
                    if (perviousPhoneState.Equals(dutController.DutPhoneState.Ringing))
                    {
                        newState = dutController.DutPhoneState.Answered;
                        perviousPhoneState = newState;
                    }
                    else if (((int)perviousPhoneState & (int)DutPhoneStateMask.IsIdleMask) > 0)
                    {
                        //form idle to offhook, it means dialing a call.
                        newState = DutPhoneState.Dialing;
                        perviousPhoneState = newState;
                    }
                    else if (((int)perviousPhoneState & (int)DutPhoneStateMask.IsOffhookMask) > 0)
                    {
                        //if perviousState is a kind of offhook, it means state doesnot changed.
                        newState = perviousPhoneState;
                    }
                    break;
                case "IDLE":
                    newState = DutPhoneState.Idle;
                    if (perviousPhoneState.Equals(DutPhoneState.Connected))
                    {
                        newState = dutController.DutPhoneState.EndCall;
                    }
                    else if (perviousPhoneState.Equals(DutPhoneState.Ringing) && isEndCallByMe)
                    {
                        newState = dutController.DutPhoneState.Rejected;
                    }
                    else if (((int)perviousPhoneState & (int)DutPhoneStateMask.IsIdleMask)> 0)
                    {
                        newState = DutPhoneState.Idle;
                    }
                    perviousPhoneState = newState;
                    isEndCallByMe = false;
                    break;
                default:
                    newState = DutPhoneState.Unknow;
                    break;
            }

            if (!_currentPhoneState.Equals(newState))
            {
                _currentPhoneState = newState;
            }
            if (DutPhoneStateChangedEventHandler != null)
            {
                DutPhoneStateChangedEventHandler.Invoke(this, new DutPhoneStateChangedEventArgs(_currentPhoneState));
            }
            return newState;
        }

        public void Dial(String number)
        {
            ADB_Process.Dial(DeviceID, number);
        }

        public bool AnswerCall(int timeoutInMillisecons)
        {
            return ADB_Process.AnswerCall(DeviceID, timeoutInMillisecons);
        }

        public void EndCall()
        {
            isEndCallByMe = true;            
            ADB_Process.EndCall(DeviceID);
        }

        public String RefreshPhoneNumberByAPI()
        {
            PhoneNumber = ADB_Process.GetPhoneNumber(DeviceID, 10000);
            if (PhoneNumber.ToLower() == "failed" || PhoneNumber.ToLower() == "error")
            {
                PhoneNumber = "";
            }
            return PhoneNumber;
        }            
    }

    public class DutPhoneStateChangedEventArgs : EventArgs
    {
        public dutController.DutPhoneState State;
        public DutPhoneStateChangedEventArgs(dutController.DutPhoneState state)
        {
            State = state;
        }
    }
}
