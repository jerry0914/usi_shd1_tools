using System;
using NationalInstruments.VisaNS;
using System.Threading;
using dev.jerry_h.pc_tools.CommonLibrary;

namespace com.usi.shd1_tools._8960Library
{
    public class VISA_Connector : IStationEmulatorConnector
    {     
        private String _resourceName = "";
        private const int writeDelay = 500;
        private const int readDelay = 500;
        private bool isConnected = false;
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
        }
        public VISA_Connector(String resourceName)
        {
            _resourceName = resourceName;
        }
        public void Write(String cmd)
        {
            MessageBasedSession session;
            try
            {
                session = (MessageBasedSession)ResourceManager.GetLocalManager().Open(_resourceName);
            }
            catch (Exception exp)
            {
                throw new Exception("Create VISA session, resource name = " + _resourceName + "; message = " + exp.Message);
            }
            session.Write(cmd);
            Logger.WriteLog(Logger.LogLevels.Verbose, "SendCmd", cmd, false);
            session.Dispose();
        }

        public void Write(String[] commands)
        {
            String currentCmd = "";
            MessageBasedSession session;
            try
            {
                session = (MessageBasedSession)ResourceManager.GetLocalManager().Open(_resourceName);
            }
            catch (Exception exp)
            {
                throw new Exception("Create VISA session, resource name = " + _resourceName+"; message = "+exp.Message);
            }
            foreach (String cmd in commands)
            {
                currentCmd = cmd;
                try
                {
                    session.Write(currentCmd);
                    Logger.WriteLog(Logger.LogLevels.Verbose, "SendCmd", currentCmd, false);
                }
                catch (Exception ex)
                {
                    throw new Exception("Write SCPI command via VISA exception, current command = " + currentCmd + "; message = " + ex.Message);
                }
                Thread.Sleep(writeDelay);
            }
            session.Dispose();
        }

        public String Read()
        {
            String rtnValue = "";
            MessageBasedSession session;
            try
            {
                session = (MessageBasedSession)ResourceManager.GetLocalManager().Open(_resourceName);
            }
            catch (Exception exp)
            {
                throw new Exception("Create VISA session, resource name = " + _resourceName + "; message = " + exp.Message);
            }
            rtnValue = session.ReadString();
            Logger.WriteLog(Logger.LogLevels.Verbose, "Read data ", rtnValue, false);
            session.Dispose();
            Thread.Sleep(readDelay);
            return rtnValue;
        }

        public String Query(String cmd)
        {
            String rtnValue = "";
            MessageBasedSession session;
            try
            {
                session = (MessageBasedSession)ResourceManager.GetLocalManager().Open(_resourceName);
            }
            catch (Exception exp)
            {
                throw new Exception("Create VISA session, resource name = " + _resourceName + "; message = " + exp.Message);
            }
            rtnValue = session.Query(cmd);
            Logger.WriteLog(Logger.LogLevels.Verbose, "Query", "command = " + cmd, false);
            Thread.Sleep(readDelay);
            Logger.WriteLog(Logger.LogLevels.Verbose, "Query", "result = " + rtnValue, false);
            session.Dispose();
            return rtnValue;
        }

        public void Connect()
        {
            try
            {
                Write(_8960_SCPI_Commands.Connect);
            }
            catch(Exception ex)
            {
                isConnected = false;
                throw ex;
            }
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Connect to TS via VISA...", false);
        }

        public void SetAuto()
        {
            Write(_8960_SCPI_Commands.SetAuto);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set \"Auto\" enable.", false);
        }

        public void SetBLER()
        {
            Write(_8960_SCPI_Commands.SetBLER);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set \"BLER\" enable.", false);
        }

        public String GetStationEmulatorCallState()
        {
            String connState = Query(_8960_SCPI_Commands.GetCallState).Trim();
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Get station emulator connection state = " + connState);
            return connState;
        }

        public String GetStationEmulatorDataState()
        {
            String transStatus = Query(_8960_SCPI_Commands.GetDataState);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Check data transfer status = " + transStatus);
            return transStatus;
        }

        public void Dial()
        {
            Write(_8960_SCPI_Commands.Dial);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "StationEmulator dial to the DUT...");
        }

        public void Set_EGPRS_850()
        {
            Write(_8960_SCPI_Commands.Set_EGPRS_850);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to EGPRS_850");
        }

        public void Set_EGPRS_DCS()
        {
            Write(_8960_SCPI_Commands.Set_EGPRS_DCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to EGPRS_DCS");
        }

        public void Set_EGPRS_EGSM()
        {
            Write(_8960_SCPI_Commands.Set_EGPRS_EGSM);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to EGPRS_EGSM");
        }

        public void Set_EGPRS_PCS()
        {
            Write(_8960_SCPI_Commands.Set_EGPRS_PCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to EGPRS_PCS");
        }

        public void Set_GPRS_850()
        {
            Write(_8960_SCPI_Commands.Set_GPRS_850);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GPRS_850");
        }

        public void Set_GPRS_DCS()
        {
            Write(_8960_SCPI_Commands.Set_GPRS_DCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GPRS_DCS");
        }

        public void Set_GPRS_EGSM()
        {
            Write(_8960_SCPI_Commands.Set_GPRS_EGSM);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GPRS_EGSM");
        }

        public void Set_GPRS_PCS()
        {
            Write(_8960_SCPI_Commands.Set_GPRS_PCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GPRS_PCS");
        }

        public void Set_GSM_850()
        {
            Write(_8960_SCPI_Commands.Set_GSM_850);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GSM_850");
        }

        public void Set_GSM_DCS()
        {
            Write(_8960_SCPI_Commands.Set_GSM_DCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GSM_DCS");
        }

        public void Set_GSM_EGSM()
        {
            Write(_8960_SCPI_Commands.Set_GSM_EGSM);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GSM_EGSM");
        }

        public void Set_GSM_PCS()
        {
            Write(_8960_SCPI_Commands.Set_GSM_PCS);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GSM_PCS");
        }

        public void SetHSDPA()
        {
            Write(_8960_SCPI_Commands.SetHSDPA);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to HSDPA");
        }

        public void SetWCDMA()
        {
            Write(_8960_SCPI_Commands.SetWCDMA);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to WCDMA");
        }

        public void SetCellPower(double CellPower)
        {
            if (CellPower > -11.0)
            {
                CellPower = -11.0;
            }
            else if (CellPower <= -127.0)
            {
                CellPower = -127.0;
            }
            Write(_8960_SCPI_Commands.SetCellPower(CellPower));
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell power to " + CellPower.ToString("0.00") + " db");
        }

        public void EndCall()
        {
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "End call by TS.");
        }

        public void SetAutoAnswer(bool isAutoAnswer)
        {
            //Send auto answer command here            
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set auto answer = " + isAutoAnswer.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seconds">Auto hangs up by stationEmulator after this value, set-1 to disable auto end-call</param>
        public void SetAutoEndCallInSeconds(int seconds)
        {

            if (seconds < 0)
            {
                SetAutoEndCall(false);

            }
            else
            {
                //Send auto end-call command here
                Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set auto hangs up in " + seconds.ToString() + " sec");
            }

        }

        public void SetAutoEndCall(bool isAutoEndCall)
        {
            //Send auto end-call command here            
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set auto answer = " + isAutoEndCall.ToString());
        }


        public void Set_UMTS_900()
        {
            Write(_8960_SCPI_Commands.SetUMTS900);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to UMTS900");
        }

        public void Set_UMTS_2100()
        {
            Write(_8960_SCPI_Commands.SetUMTS2100);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to UMTS2100");
        }

        public void Set_GSM_900()
        {
            Write(_8960_SCPI_Commands.Set_GSM_900);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to GSM 900");
        }

        public void Set_TD_SCDMA_B34()
        {
            Write(_8960_SCPI_Commands.SetTD_SCDMA34);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to TD-SCDMA B34");
        }

        public void Set_TD_SCDMA_B39()
        {
            Write(_8960_SCPI_Commands.SetTD_SCDMA39);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell band to TD-SCDMA B39");
        }

        public void SetSignalChannel(int Channel)
        {
            Write(_8960_SCPI_Commands.SetChannel(Channel));
        }

        public void SetTrafficChannel(int channel)
        {
            Write(_8960_SCPI_Commands.SetTrafficChannel(channel));
        }
        public void SetTrafficBand(String band)
        {
            Write(_8960_SCPI_Commands.SetTrafficBand(band));
        }

        public void SetCellOff()
        {
            Write(_8960_SCPI_Commands.SetCellOff);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Set cell off.");
        }
        public double GetCellPower()
        {
            Write(_8960_SCPI_Commands.SetCellOff);
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Get Cell Power");
            return -999.0;
        }
    }
}
