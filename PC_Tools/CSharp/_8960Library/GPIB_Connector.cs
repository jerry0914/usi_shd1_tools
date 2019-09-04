using System;
using System.Threading;
using NationalInstruments;
using NationalInstruments.NI4882;
using System.Windows.Forms;
using dev.jerry_h.pc_tools.CommonLibrary;

namespace com.usi.shd1_tools._8960Library
{

    public class GPIB_Connector : IStationEmulatorConnector
    {
        private bool isConnected = false;
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
        }
        private readonly int boardNumber = 0;
        private readonly byte primaryAddress = 0;
        private readonly byte secondaryAddress = 0;
        private const int writeDelay = 50;
        private const int readDelay = 50;

        public GPIB_Connector(int BoardNumber, byte PrimaryAddress, byte SecondaryAddress)
        {
            boardNumber = BoardNumber;
            primaryAddress = PrimaryAddress;
            secondaryAddress = SecondaryAddress;
            Connect();
        }

        public GPIB_Connector(int BoardNumber, byte PrimaryAddress)
            : this(BoardNumber, PrimaryAddress, 0)
        {
        }

        public void Write(String command)
        {
            Device se = null;
            try
            {
                se = new Device(boardNumber, primaryAddress, secondaryAddress);
                se.Write(command);
                Logger.WriteLog(Logger.LogLevels.Verbose, "SendCmd", command,false);
            }
            catch (Exception ex)
            {
                throw new Exception("Write SCPI command exception, current command = " + command);
            }
            if (se != null)
            {
                se.Dispose();
            }
            Thread.Sleep(writeDelay);
        }

        public void Write(String[] commands)
        {
            String currentCmd = "";
            Device se;
            try
            {
                se = new Device(boardNumber, primaryAddress, secondaryAddress);
            }
            catch (Exception exp)
            {
                se = null;
                throw new Exception("Create GPIB session, boardNumber = " + boardNumber + ", primaryAddress =" + primaryAddress + ", secondaryAddress = " + secondaryAddress + "\r\n; message = " + exp.Message);
            }
            if (se != null)
            {
                foreach (String cmd in commands)
                {
                    currentCmd = cmd;
                    try
                    {
                        se.Write(currentCmd);
                        Logger.WriteLog(Logger.LogLevels.Verbose, "Cmd", currentCmd, false);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Write SCPI command via GPIB exception, current command = " + currentCmd + "; message = " + ex.Message);
                    }
                    Thread.Sleep(writeDelay);
                }
                se.Dispose();
            }
        }

        public String Read()
        {
            String rtnValue = "";
            Device se =null;
            try
            {
                se = new Device(boardNumber, primaryAddress, secondaryAddress);
                rtnValue = se.ReadString();
            }                        
            catch (Exception exp)
            {
                se = null;
                throw new Exception("Create GPIB session, boardNumber = " + boardNumber + ", primaryAddress =" + primaryAddress + ", secondaryAddress = " + secondaryAddress + "\r\n; message = " + exp.Message);
            }
            Logger.WriteLog(Logger.LogLevels.Verbose, "Read data ", rtnValue, false);
            se.Dispose();
            Thread.Sleep(readDelay);
            return rtnValue;
        }

        public String Query(String cmd)
        {
            String rtnValue = "";
            Device se = null;
            try
            {
                se = new Device(boardNumber, primaryAddress, secondaryAddress);
            }
            catch (Exception exp)
            {
                se = null;
                throw new Exception("Create GPIB session, boardNumber = " + boardNumber + ", primaryAddress =" + primaryAddress + ", secondaryAddress = " + secondaryAddress + "\r\n; message = " + exp.Message);
            }
            if (se != null)
            {
                se.Write(cmd);
                Logger.WriteLog(Logger.LogLevels.Verbose, "Query", "command = " + cmd, false);
                Thread.Sleep(writeDelay);
                if (cmd.Trim().EndsWith("?"))
                {
                    try
                    {
                        rtnValue = se.ReadString().Trim();
                        Logger.WriteLog(Logger.LogLevels.Verbose, "Query", "result = " + rtnValue, false);
                        Thread.Sleep(readDelay);
                    }
                    catch
                    {
                        rtnValue = "ERROR";
                    }
                }
                se.Dispose();
            }
            return rtnValue;
        }

        public void Connect()
        {
            try
            {
                Write(_8960_SCPI_Commands.Connect);
                isConnected = true;
            }
            catch(Exception ex)
            {
                isConnected = false;
                throw ex;
            }
            Logger.WriteLog(Logger.LogLevels.Information, "Action", "Connected via GPIB...", false);
        }

    }
}
