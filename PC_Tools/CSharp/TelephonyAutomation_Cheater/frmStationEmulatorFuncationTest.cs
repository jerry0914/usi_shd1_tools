using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.usi.shd1_tools._8960Library;
using com.usi.shd1_tools.TestcasePackage;
using dev.jerry_h.pc_tools.CommonLibrary;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    public partial class frmStationEmulatorFunctionTest : Form
    {
        StationEmulator_8960 se8960;
        private static frmStationEmulatorFunctionTest me;
        public frmStationEmulatorFunctionTest(StationEmulator_8960 se)//IStationEmulatorConnector Connector)
        {
            InitializeComponent();
            se8960 = se;
            //connector = Connector;
            Logger.LiveLogEventHandler += new EventHandler<LoggerLiveMessageEventArgs>(showLiveLogMessage);
        }

        private void btnEGPRS_850_Click(object sender, EventArgs e)
        {
            se8960.Set_EGPRS_850();
        }

        private void btnEGPRS_DCS_Click(object sender, EventArgs e)
        {
            se8960.Set_EGPRS_DCS();
        }

        private void btnEGPRS_EGSM_Click(object sender, EventArgs e)
        {
            se8960.Set_EGPRS_EGSM();
        }

        private void btnEGPRS_PCS_Click(object sender, EventArgs e)
        {
            se8960.Set_EGPRS_PCS();
        }

        private void btnGPRS_850_Click(object sender, EventArgs e)
        {
            se8960.Set_GPRS_850();
        }

        private void btnGPRS_DCS_Click(object sender, EventArgs e)
        {
            se8960.Set_GPRS_DCS();
        }

        private void btnGPRS_EGSM_Click(object sender, EventArgs e)
        {
            se8960.Set_GPRS_EGSM();
        }

        private void btnGPRS_PCS_Click(object sender, EventArgs e)
        {
            se8960.Set_GPRS_PCS();
        }

        private void btnGSM_850_Click(object sender, EventArgs e)
        {
            se8960.Set_GSM_850();
        }

        private void btnGSM_DCS_Click(object sender, EventArgs e)
        {
            se8960.Set_GSM_DCS();
        }

        private void btnGSM_EGSM_Click(object sender, EventArgs e)
        {
            se8960.Set_GSM_EGSM();
        }

        private void btnGSM_PCS_Click(object sender, EventArgs e)
        {
            se8960.Set_GSM_PCS();
        }

        private void btnHSDPA_Click(object sender, EventArgs e)
        {
            se8960.SetHSDPA();
        }

        private void btnWCDMA_Click(object sender, EventArgs e)
        {
            se8960.SetWCDMA();
        }

        private void btnCallStatus_Click(object sender, EventArgs e)
        {
            StationEmulator_8960.CallStates state = se8960.CallState;
            btnCallStatus.BackColor = state.Equals(StationEmulator_8960.CallStates.Connected) ? Color.Green : Color.Red;
            lsvLiveLog.Items.Insert(0, state.ToString());
        }

        private void btnDataStatus_Click(object sender, EventArgs e)
        {
            //String state = se8960.GetStationEmulatorDataState();
            String state = se8960.DataState;
            btnDataStatus.BackColor = state.Equals("TRAN") ? Color.DeepSkyBlue : Color.OrangeRed;
            lsvLiveLog.Items.Insert(0, state);
        }
         
        private void btnSend_Click(object sender, EventArgs e)
        {
            //connector.Write(txtCommand.Lines);
            se8960.Write(txtCommand.Lines);
            if (txtCommand.Text.EndsWith("?"))
            {
                System.Threading.Thread.Sleep(2000);
                try
                {
                    lsvLiveLog.Items.Insert(0, se8960.Read());//connector.Read());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSetPower_Click(object sender, EventArgs e)
        {
            //connector.SetCellPower(Convert.ToDouble(numCellPower.Value));
            se8960.SetCellPower(Convert.ToDouble(numCellPower.Value));
        }

        private void btnDial_Click(object sender, EventArgs e)
        {
            se8960.Dial();
            //connector.Dial();
        }

        private void btnEndCall_Click(object sender, EventArgs e)
        {
            se8960.EndCall();
            //connector.EndCall();
        }

        private void showLiveLogMessage(object sender, LoggerLiveMessageEventArgs ea)
        {
            lsvLiveLog.Items.Insert(0, ea.LiveLogMessage);
        }

        private void frmStationEmulatorFunctionTest_Load(object sender, EventArgs e)
        {
           
        }


        private void btnTDSCDMA_B34_Click(object sender, EventArgs e)
        {
            //connector.Set_TD_SCDMA_B34();
            se8960.Set_TD_SCDMA_B34();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            se8960.Set_TD_SCDMA_B39();
        }

        private void btnGSM900_Click(object sender, EventArgs e)
        {
            se8960.Set_GSM_900();
        }

        private void btnUMTS900_Click(object sender, EventArgs e)
        {
            se8960.Set_UMTS_900();
        }

        private void btnUMTS2100_Click(object sender, EventArgs e)
        {
            se8960.Set_UMTS_2100();
        }

        private void lsvLiveLog_SizeChanged(object sender, EventArgs e)
        {
            columnHeader1.Width = lsvLiveLog.Width - 6;
        }

        public static void Display(StationEmulator_8960 se8960)//GPIB_Connector connector)
        {
            if (me == null) {
                me = new frmStationEmulatorFunctionTest(se8960);
            }
            else if (me.IsDisposed)
            {
                me.Close();
                me = null;
                me = new frmStationEmulatorFunctionTest(se8960);
            }
            me.Show();
        }

        private void btnBERInit_Click(object sender, EventArgs e)
        {
            int berCount = Convert.ToInt32(txtBERCount.Text);
            int berTimeout = Convert.ToInt32(txtBERTimeout.Text);

            se8960.Init_BER(berCount, ckbBERContinuous.Checked, berTimeout);
        }

        private void btnBERGetLast_Click(object sender, EventArgs e)
        {

            txtBERLast.Text = se8960.LookbackBER;//).ToString("0.00")+"%";
        }        
    }
}
