using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.usi.shd1_tools.TestcasePackage;
using System.Threading;
using dev.jerry_h.pc_tools.AndroidLibrary;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    public partial class frmDutFunctionTest : Form
    {
        private static frmDutFunctionTest me;
        private clsDevice device;
        public frmDutFunctionTest(clsDevice device)
        {
            InitializeComponent();
            this.device = device;
        }

        private void btnPhoneCallStatus_Click(object sender, EventArgs e)
        {
            txtPhoneStatus.Text = "";
            device.SIM1.RefreshState();
            txtPhoneStatus.Text = device.Telephony.CallState.ToString();
        }

        private void btnDial_Click(object sender, EventArgs e)
        {
            if (txtDialNumber.Text.Length > 0)
            {
                device.Telephony.Dial_InsLib("000");
                Thread.Sleep(1000);
                txtPhoneStatus.Text = device.Telephony.CallState.ToString();
            }
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            device.Telephony.AnswerCall_InsLib((int)numericUpDown1.Value);
            Thread.Sleep(1000);
            txtPhoneStatus.Text = device.Telephony.CallState.ToString(); ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            device.Telephony.EndCall_InsLib();
            Thread.Sleep(1000);
            txtPhoneStatus.Text = device.Telephony.CallState.ToString(); ;
        }

        private void ckbMobileData_CheckedChanged(object sender, EventArgs e)
        {
            ADB_Process.SetMobileDataStatus(ckbSetMobileData.Checked, 5000);
        }

        private void ckbSetWifiState_CheckedChanged(object sender, EventArgs e)
        {
            ADB_Process.SetWiFiState(ckbSetWifiState.Checked, 5000);
        }

        private void ckbSIM_Enable_CheckedChanged(object sender, EventArgs e)
        {
            ADB_Process.SetSimCardsEnable("", ckbSIM1_Enable.Checked, ckbSIM2_Enable.Checked);
        }

        private void btnGetSignalStrength_Click(object sender, EventArgs e)
        {
            lblSignal_SIM1.Text = "";
            lblSignal_SIM2.Text = "";
            this.Cursor = Cursors.WaitCursor;
            device.SIM1.RefreshState();
            device.SIM2.RefreshState();
            lblSignal_SIM1.Text = device.SIM1.SignalStrength.ToString();
            lblSignal_SIM2.Text = device.SIM2.SignalStrength.ToString();
            this.Cursor = Cursors.Default;
        }

        private void ckbPhoneStateReceiverService_CheckedChanged(object sender, EventArgs e)
        {
        }

        public static void Display(clsDevice device)
        {            
            if (me == null)
            {
                me = new frmDutFunctionTest(device);
            }
            else if(me.IsDisposed)
            {
                me.Close();
                me = null;
                me = new frmDutFunctionTest(device);
            }
            me.Show();
        }
    }
}
