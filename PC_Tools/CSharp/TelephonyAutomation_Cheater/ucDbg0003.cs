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
    public partial class ucDbg0003 : UserControl
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

        public int RSCP_Init
        {
            get
            {
                return Convert.ToInt32(txtRSCP_Init.Text);
            }
            set
            {
                txtRSCP_Init.Text = value.ToString();
            }
        }

        public int RSCP_Inaccuracy_Init
        {
            get
            {
                return Convert.ToInt32(txtInaccuracy_Init.Text);
            }
            set
            {
                txtInaccuracy_Init.Text = value.ToString();
            }
        }

        public int RSCP_High
        {
            get
            {
                return Convert.ToInt32(txtRSCP_High.Text);
            }
            set
            {
                txtRSCP_High.Text = value.ToString();
            }
        }

        public int RSCP_Inaccuracy_High
        {
            get
            {
                return Convert.ToInt32(txtInaccuracy_High.Text);
            }
            set
            {
                txtInaccuracy_High.Text = value.ToString();
            }
        }

        public int RSCP_Low
        {
            get
            {
                return Convert.ToInt32(txtRSCP_Low.Text);
            }
            set
            {
                txtRSCP_Low.Text = value.ToString();
            }
        }

        public int CyclesPerChannel
        {
            get
            {
                return Convert.ToInt32(txtCycles.Text);
            }
            set
            {
                txtCycles.Text = value.ToString();
            }
        }

        public ucDbg0003()
        {
            InitializeComponent();
            cmbBand.DataSource = Enum.GetValues(typeof(Wwan_TestCaseInfo.Band));
            cmbBand.SelectedItem = Wwan_TestCaseInfo.Band.UMTS_2100;
        }

    }
}
