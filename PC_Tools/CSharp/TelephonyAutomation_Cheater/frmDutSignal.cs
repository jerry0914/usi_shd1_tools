using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using com.usi.shd1_tools.TestcasePackage;
using com.usi.shd1_tools._8960Library;
using dev.jerry_h.pc_tools.CommonLibrary;
using dev.jerry_h.pc_tools.AndroidLibrary;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    public partial class frmDutSignal : Form
    {
        private static frmDutSignal me;
        //private IStationEmulatorConnector currentConnector;
        private StationEmulator_8960 se8960;
        private clsDevice androidDevice;
        private Wwan_TestCaseInfo.SimSlot simSlot = Wwan_TestCaseInfo.SimSlot.SIM1;
        public frmDutSignal(clsDevice device, StationEmulator_8960 se)//IStationEmulatorConnector connector)
        {
            InitializeComponent();
            //currentConnector = connector;
            se8960 = se;
            cmbSimSlot.DataSource = Enum.GetValues(typeof(Wwan_TestCaseInfo.SimSlot));
            androidDevice = device;
            if (cmbSimSlot.Items.Count > 0)
            {
                cmbSimSlot.SelectedIndex = 0;
            }
        }
        delegate void ShowSignalStrengthDelegate(int param1);
        private void showCurrentSignalStrength(int signalStrength)
        {
            if (this.InvokeRequired)
            {
                ShowSignalStrengthDelegate del = new ShowSignalStrengthDelegate(showCurrentSignalStrength);
                this.BeginInvoke(del, signalStrength);
            }
            else
            {
                lblCurrentStrength.Visible = true;
                lblCurrentStrength.Text = signalStrength.ToString() + " db";
            }
        }
        public static void Display(clsDevice androidDevice,StationEmulator_8960 se)//IStationEmulatorConnector connector)
        {
            if(me==null)
            {
                me = new frmDutSignal(androidDevice,se);
            }
            else
            {
                if (me.IsDisposed)
                {
                    me.Close();
                    me = null;
                    me = new frmDutSignal(androidDevice,se);
                }
            }
            //ADB_Process.StartPhoneStateReceiverService_MC36("");
            me.Show();
        }

        private void btnSetSignal_Click(object sender, EventArgs e)
        {
            int timeout = (int)numTimeout.Value;
            int inaccuracy = (int)numInaccuracy.Value;
            int strength = (int)numTargetStrength.Value;
            Enum.TryParse<Wwan_TestCaseInfo.SimSlot>(cmbSimSlot.SelectedValue.ToString(), out simSlot);
            this.Cursor = Cursors.WaitCursor;
            if (setDutSignalStrength(simSlot, strength, timeout, inaccuracy))
            {
                lblResult.Text = "PASS";
                lblResult.ForeColor = Color.Green;
            }
            else
            {
                lblResult.Text = "FAIL";
                lblResult.ForeColor = Color.Red;
            }
            this.Cursor = Cursors.Default;
        }

        #region for setDutSignalStrength debug only
        private bool setDutSignalStrength(Wwan_TestCaseInfo.SimSlot sim, int strengthInDb, int timeoutInSeconds)
        {
            timeoutInSeconds = timeoutInSeconds >= 30 ? timeoutInSeconds : 30;
            return setDutSignalStrength(sim, strengthInDb, timeoutInSeconds, 2);
        }

        private bool setDutSignalStrength(Wwan_TestCaseInfo.SimSlot sim, int strengthInDb, int timeoutInSeconds, int inaccuracy)
        {
            bool result = false;
            bool isTimeout = false;
            int signalStrength = -999;
            int cellPowerInStationEmulator = -999;
            int diff = 999;
            int passTimesCount = 0;
            int passingTimesCriteria = 2;//取樣有n次singnal strength誤差在容許範圍內，才算pass；
            int meetTargetRetryCount = 0;
            int meetTargetRetryLimit = 3;
            int checkSignalInterval = 5000;
            int modifyCellPowerDelay = 5000;
            DateTime startTime = DateTime.Now;
            inaccuracy = Math.Abs(inaccuracy);
            
            Logger.WriteLog(Logger.LogLevels.Debug,Logger.LogTags.Action.ToString(), "Try to set DUT signal strength to " + strengthInDb + " db");
            do
            {
                #region Check if current signal strength meet to the target strength
                meetTargetRetryCount = 0;
                do
                {
                    meetTargetRetryCount++;
                    signalStrength = getSignalStrength(sim); //Get Signal Strength
                    showCurrentSignalStrength(signalStrength);
                    if (signalStrength <= -999)
                    {
                        result = false;
                        isTimeout = true;
                        break;
                    }
                    else
                    {
                        diff = strengthInDb - signalStrength;
                        Logger.WriteLog(Logger.LogLevels.Debug,Logger.LogTags.Detail.ToString(), "Difference between current signal strength and target = " + diff + " db");
                        if (Math.Abs(diff) <= inaccuracy)
                        {
                            passTimesCount++;
                        }
                        if (passTimesCount >= passingTimesCriteria)
                        {
                            result = true;
                        }
                        else
                        {
                            isTimeout = DateTime.Now.Subtract(startTime).TotalSeconds > timeoutInSeconds;
                            Thread.Sleep(checkSignalInterval);
                        }
                    }
                } while (!(result || isTimeout || (meetTargetRetryCount >= meetTargetRetryLimit)));
                #endregion Check if current signal strength meet to the target strength
                if (result || isTimeout)
                {
                    break;
                }
                else
                {
                    //cellPowerInStationEmulator = (int)currentConnector.GetCellPower();
                    cellPowerInStationEmulator = se8960.CellPower;
                    int newCellPower = cellPowerInStationEmulator + diff;
                    se8960.SetCellPower(newCellPower);
                    //currentConnector.SetCellPower(newCellPower);
                    Logger.WriteLog(Logger.LogLevels.Debug,Logger.LogTags.Action.ToString(), "Auto adjust the cell power to = " + newCellPower + " db");
                    Thread.Sleep(modifyCellPowerDelay);
                }
            } while (!(result || isTimeout));
            Logger.WriteLog("Check", "Set DUT signal strength result = " + (result ? "Pass" : "Fail") + ", timeout = " + isTimeout);
            return result;
        }

        private int getSignalStrength(Wwan_TestCaseInfo.SimSlot sim)
        {
            int signalStrength = -999;
            if (sim.Equals(Wwan_TestCaseInfo.SimSlot.SIM2))
            {
                androidDevice.SIM2.RefreshState();
                signalStrength = androidDevice.SIM2.SignalStrength;
            }
            else
            {
                androidDevice.SIM1.RefreshState();
                signalStrength = androidDevice.SIM1.SignalStrength;
            }
            return signalStrength;
        }
        
        private void frmDutSignal_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ADB_Process.StopPhoneStateReceiverService_MC36("");
        }

        private void ckbShowNitifyOnDut_CheckedChanged(object sender, EventArgs e)
        {
           // ADB_Process.SetPhoneStateReceiverParameters("", ckbShowNitifyOnDut.Checked);
        }
        #endregion for setDutSignalStrength debug only
    }
}
