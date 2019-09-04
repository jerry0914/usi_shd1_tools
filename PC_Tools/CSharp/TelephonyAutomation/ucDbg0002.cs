using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.usi.shd1_tools.TestcasePackage;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    public partial class ucDbg0002 : UserControl
    {
        public Wwan_TestCaseInfo.Band Band
        {
            get
            {
                return (Wwan_TestCaseInfo.Band)cmbBand.SelectedItem;
            }
            set
            {
                cmbBand.SelectedItem = value;
            }
        }

        public List<String> Channels{
            get
            {
                List<String> rtnVal = new List<string>();
                foreach (String str in txtChannels.Lines)
                {
                    if (str.Trim().Length > 0)
                    {
                        rtnVal.Add(str);
                    }
                }
                return rtnVal;
            }
            set
            {
                txtChannels.Text = "";
                foreach (String str in value)
                {
                    txtChannels.Text += str + "\r\n";
                }
                txtChannels.Text.TrimEnd();
            }
        }

        public int Channel_Duration
        {
            get
            {
                return Convert.ToInt32(txtDuration.Text);
            }
            set
            {
                txtDuration.Text = value.ToString();
            }
        }

        public int BER_Interval
        {
            get
            {
                return Convert.ToInt32(txtInterval.Text);
            }
            set
            {
                txtInterval.Text = value.ToString();
            }
        }

        public int RSCP
        {
            get
            {
                return Convert.ToInt32(txtRSCP.Text);
            }
            set
            {
                txtRSCP.Text = value.ToString();
            }
        }

        public int RSCP_Inaccuracy
        {
            get
            {
                return Convert.ToInt32(txtInaccuracy.Text);
            }
            set
            {
                txtInaccuracy.Text = value.ToString();
            }
        }



        public ucDbg0002()
        {
            InitializeComponent();
            cmbBand.DataSource = Enum.GetValues(typeof(Wwan_TestCaseInfo.Band));
            cmbBand.SelectedItem = Wwan_TestCaseInfo.Band.UMTS_2100;
        }

        private void cmbBand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
