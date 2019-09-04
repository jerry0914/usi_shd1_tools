using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.usi.shd1_tools._8960Library;
//using com.usi.shd1_tools.TestcasePackage;
using System.Windows.Forms;
using dev.jerry_h.pc_tools.CommonLibrary;

//namespace com.usi.shd1_tools.TelephonyAutomation
namespace com.usi.shd1_tools._8960Library
{
    public class StationEmulator_8960
    {
        public enum CallStates
        {
            Unknow = -1,
            Idle = 0,
            SetupRequest = 2,
            Proceeding = 3,
            Alerting = 4,
            Disconnecting = 5,
            Connected = 6
        };

        public enum KEYS_StateChangedEvent
        {
            None=0,
            Application,
            Format,
            Band,
            CellChannel,
            TrafficChannel,
            CellPower,
            CallState
        };

        public String ApplicationName
        {
            get
            {
                return connector.Query("SYSTem:APPLication:NAME?");
            }
        }
        public String FormatName
        {
            get
            {
                return connector.Query("SYSTem:APPLication:FORMat?");
            }
        }
        public String CellBand
        {
            get
            {
                return connector.Query("CALL:CELL:BAND?");
            }
        }
        public String TrafficBand
        {
            get
            {
                return connector.Query("CALL:TCH:BAND?");
            }
        }
        public String CellChannel
        {
            get
            {
                return connector.Query("CALL:CHAN?");
            }
        }
        public String TrafficChannel
        {
            get
            {
                return connector.Query("CALL:TCH?");
            }
        }
        public int CellPower
        {
            get
            {
                int cellPw = -999;
                try
                {
                    String strCellPower = connector.Query("CALL:CELL:POWer?");
                    cellPw = (int)Convert.ToDouble(strCellPower);
                }
                catch
                {
                    cellPw = -999;
                }
                sendCellPowerChangedEvent(cellPw);
                return cellPw;
            }
        }
        private CallStates callState = CallStates.Unknow;
        public CallStates CallState
        {
            get
            {
                String state = getStationEmulatorCallState().ToUpper().TrimEnd('\n');
                switch (state)
                {
                    case "IDLE":
                        callState = CallStates.Idle;
                        break;
                    case "SREQ":
                        callState = CallStates.SetupRequest;
                        break;
                    case "ALER":
                    case "CALL": // When on the romaining state, alerting state may become to "CALL" state. 
                        callState = CallStates.Alerting;
                        break;
                    case "DISC":
                        callState = CallStates.Disconnecting;
                        break;
                    case "PROC":
                        callState = CallStates.Proceeding;
                        break;
                    case "CONN":
                        callState = CallStates.Connected;
                        break;
                    default:
                        callState = CallStates.Unknow;
                        break;
                }
                sendCallStateChangedEvent();
                return callState;
            }
        }
        public String DataState
        {
            get
            {
                return getStationEmulatorDataState();
            }
        }
        private IStationEmulatorConnector connector = null;
        public EventHandler<StationEmulatorStateChangedEventArgs> StationEmulatorStateChangedEventHandler;
        public String LookbackBER
        {
            get
            {
                double dResult = -999;
                try
                {
                    dResult = Convert.ToDouble(connector.Query(_8960_SCPI_Commands.GetBERR));
                    return dResult.ToString("0.00") + "%";
                }
                catch
                {
                    return "Unknow";
                }
            }
        }
      
        public StationEmulator_8960(IStationEmulatorConnector connector) 
        {
            this.connector = connector;
            if (connector != null)
            {
                if (!connector.IsConnected)
                {
                    connector.Connect();
                }    
            }
            else
            {
                throw new SatationEmualtorConnectionIsNotReadyException();
            }
        }

        public void SetAuto()
        {
            connector.Write(_8960_SCPI_Commands.SetAuto);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set \"Auto\" enable.");
        }

        public void SetBLER()
        {
            connector.Write(_8960_SCPI_Commands.SetBLER);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set \"BLER\" enable.");
        }

        private String getStationEmulatorCallState()
        {
            String connState = connector.Query(_8960_SCPI_Commands.GetCallState);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Get station emulator connection state = " + connState);
            return connState;
        }

        private String getStationEmulatorDataState()
        {
            String transStatus = connector.Query(_8960_SCPI_Commands.GetDataState);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Check data transfer status = " + transStatus);
            return transStatus;
        }

        public void Dial()
        {
            connector.Write(_8960_SCPI_Commands.Dial);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "StationEmulator dial to the DUT...");
            sendCallStateChangedEvent();
        }

        public void Set_EGPRS_850()
        {
            connector.Write(_8960_SCPI_Commands.Set_EGPRS_850);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to EGPRS_850");
            sendBandChangedEvent();
        }

        public void Set_EGPRS_DCS()
        {
            connector.Write(_8960_SCPI_Commands.Set_EGPRS_DCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to EGPRS_DCS");
            sendBandChangedEvent();
        }

        public void Set_EGPRS_EGSM()
        {
            connector.Write(_8960_SCPI_Commands.Set_EGPRS_EGSM);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to EGPRS_EGSM");
            sendBandChangedEvent();
        }

        public void Set_EGPRS_PCS()
        {
            connector.Write(_8960_SCPI_Commands.Set_EGPRS_PCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to EGPRS_PCS");
            sendBandChangedEvent();
        }

        public void Set_GPRS_850()
        {
            connector.Write(_8960_SCPI_Commands.Set_GPRS_850);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GPRS_850");
            sendBandChangedEvent();
        }

        public void Set_GPRS_DCS()
        {
            connector.Write(_8960_SCPI_Commands.Set_GPRS_DCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GPRS_DCS");
            sendBandChangedEvent();
        }

        public void Set_GPRS_EGSM()
        {
            connector.Write(_8960_SCPI_Commands.Set_GPRS_EGSM);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GPRS_EGSM");
            sendBandChangedEvent();
        }

        public void Set_GPRS_PCS()
        {
            connector.Write(_8960_SCPI_Commands.Set_GPRS_PCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GPRS_PCS");
            sendBandChangedEvent();
        }

        public void Set_GSM_850()
        {
            connector.Write(_8960_SCPI_Commands.Set_GSM_850);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GSM_850");
            sendBandChangedEvent();
        }

        public void Set_GSM_DCS()
        {
            connector.Write(_8960_SCPI_Commands.Set_GSM_DCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GSM_DCS (DCS 1800)");
            sendBandChangedEvent();
        }

        public void Set_GSM_EGSM()
        {
            connector.Write(_8960_SCPI_Commands.Set_GSM_EGSM);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GSM_EGSM");
            sendBandChangedEvent();
        }

        public void Set_GSM_PCS()
        {
            connector.Write(_8960_SCPI_Commands.Set_GSM_PCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GSM_PCS");
            sendBandChangedEvent();
        }

        public void SetHSDPA()
        {
            connector.Write(_8960_SCPI_Commands.SetHSDPA);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to HSDPA");
        }

        public void SetWCDMA()
        {
            connector.Write(_8960_SCPI_Commands.SetWCDMA);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to WCDMA");
        }

        public void SetCellPower(double CellPower)
        {
            if (CellPower > 0)
            {
                CellPower = 0;
            }
            else if (CellPower <= -150.0)
            {
                CellPower = -150.0;
            }
            connector.Write(_8960_SCPI_Commands.SetCellPower(CellPower));
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell power to " + CellPower.ToString("0.00") + " db");
            sendCellPowerChangedEvent((int)CellPower);
        }

        public void EndCall()
        {
            connector.Write(_8960_SCPI_Commands.EndCall);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "End call by station emulator.");
            sendCallStateChangedEvent();
        }

        public void SetAmplitudeOffsets(double[] Frequences, double[] Offsets)
        {
            String[] cmds = _8960_SCPI_Commands.SetAmplitudeOffsets(Frequences, Offsets);
            connector.Write(cmds);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set amplitude offsets table = " + cmds[0] + ";" + cmds[1]);
        }

        public void Set_UMTS_900()
        {
            connector.Write(_8960_SCPI_Commands.SetUMTS900);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to UMTS900");
            sendBandChangedEvent();
        }

        public void Set_UMTS_2100()
        {
            connector.Write(_8960_SCPI_Commands.SetUMTS2100);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to UMTS2100");
            sendBandChangedEvent();
        }

        public void Set_GSM_900()
        {
            connector.Write(_8960_SCPI_Commands.Set_GSM_900);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GSM 900");
            sendBandChangedEvent();
        }

        public void Set_TD_SCDMA_B34()
        {
            connector.Write(_8960_SCPI_Commands.SetTD_SCDMA34);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to TD-SCDMA B34");
            sendBandChangedEvent();
        }

        public void Set_TD_SCDMA_B39()
        {
            connector.Write(_8960_SCPI_Commands.SetTD_SCDMA39);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to TD-SCDMA B39");
            sendBandChangedEvent();
        }

        public void SwitchSystemApplication(String ApplicationName)
        {
            String currentAppName = connector.Query("SYSTem:APPLication:NAME?");
            if (currentAppName.Equals(ApplicationName))
            {
                Logger.WriteLog(Logger.LogLevels.Verbose, "Action", "Current system appliction is " + ApplicationName + " already, nothing is changed.");
                MessageBox.Show("Current system appliction is " + ApplicationName + " already, nothing is changed.");
            }
            else
            {
                if (DialogResult.OK.Equals(MessageBox.Show("Do you  want to switch system appliction to " + ApplicationName + "(Agilent 8960 will be rebooted!!)", "Confirm ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)))
                {
                    connector.Write(_8960_SCPI_Commands.SwitchSystemApplication(ApplicationName));
                    MessageBox.Show("Please wait for 8960 rebooting, and reconnect GPIP connector manually after rebooting completed");
                    sendApplicationChangedEvent();
                }
            }
        }

        public void SetCellChannel(int Channel)
        {
            connector.Write(_8960_SCPI_Commands.SetChannel(Channel));
            sendCellChannelChangedEvent();
            sendTrafficChannelChangedEvent();
        }

        public void SetTrafficChannel(int channel)
        {
            connector.Write(_8960_SCPI_Commands.SetTrafficChannel(channel));
            sendTrafficChannelChangedEvent();
        }

        public void SetTrafficBand(String band)
        {
            connector.Write(_8960_SCPI_Commands.SetTrafficBand(band));
            sendBandChangedEvent();
        }

        public void SetCellOff()
        {
            connector.Write(_8960_SCPI_Commands.SetCellOff);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell off");
            sendCallStateChangedEvent();
        }
   
        public String Query(String command)
        {
            return connector.Query(command);
        }

        public void Write(String[] cmds)
        {
            connector.Write(cmds);
        }
        public void Write(String command)
        {
            connector.Write(command);
        }
        public String Read()
        {
            return connector.Read();
        }

        public void Init_BER(int count, bool isContinuous, int timeout_InSeconds)
        {
            connector.Write(_8960_SCPI_Commands.Init_BER(count, isContinuous, timeout_InSeconds));
        }
        #region Send events
        private void sendApplicationChangedEvent()
        {
            sendStateChangedEvent(KEYS_StateChangedEvent.Application, ApplicationName);
        }

        private void sendFormatChangedEvent()
        {
            sendStateChangedEvent(KEYS_StateChangedEvent.Format, FormatName);
        }

        public void sendBandChangedEvent()
        {
            sendStateChangedEvent(KEYS_StateChangedEvent.Band, TrafficBand);
        }

        public void sendCellChannelChangedEvent()
        {
            sendStateChangedEvent(KEYS_StateChangedEvent.CellChannel, CellChannel);
        }        

        public void sendTrafficChannelChangedEvent()
        {
            sendStateChangedEvent(KEYS_StateChangedEvent.TrafficChannel, TrafficChannel);
        }

        public void sendCellPowerChangedEvent(int cPow)
        {
            sendStateChangedEvent(KEYS_StateChangedEvent.CellPower, cPow);
        }

        public void sendCallStateChangedEvent()
        {
            sendStateChangedEvent(KEYS_StateChangedEvent.CallState, callState);
        }

        private void sendStateChangedEvent(KEYS_StateChangedEvent key, object value)
        {
            if (StationEmulatorStateChangedEventHandler != null)
            {
                StationEmulatorStateChangedEventHandler.Invoke(this, new StationEmulatorStateChangedEventArgs(key, value));
            }
        }
        #endregion Send events
    }

    public class StationEmulatorStateChangedEventArgs : EventArgs
    {
        public readonly StationEmulator_8960.KEYS_StateChangedEvent Key = StationEmulator_8960.KEYS_StateChangedEvent.None;
        public readonly object Value;
        public StationEmulatorStateChangedEventArgs(StationEmulator_8960.KEYS_StateChangedEvent key, object value)
        {
            Key = key;
            Value = value;
        }
    }

    public class SatationEmualtorConnectionIsNotReadyException:Exception
    {

    }
}
