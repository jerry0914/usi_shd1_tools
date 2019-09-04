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
    public partial class ucDbg0001 : UserControl
    {
        public int Loop
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageLoop.Text);
            }
            set
            {
                txtIoCoverageLoop.Text = value.ToString();
            }
        }
        public Wwan_TestCaseInfo.Band Band{
            get
            {
                return (Wwan_TestCaseInfo.Band)cmbIoCoverageBand.SelectedItem;
            }
            set
            {
                cmbIoCoverageBand.SelectedItem = value;
            }
        }
        public int CellChannel
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageChannel.Text);
            }
            set
            {
                txtIoCoverageChannel.Text = value.ToString();
            }
        }
        public bool G1_Enable
        {
            get
            {
                return ckbGroup1Enable.Checked;
            }
        }
        public int G1_CellPower1
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower1_1.Text);
            }
            set
            {
                txtIoCoverageCellPower1_1.Text = value.ToString();
            }
        }
        public int G1_CellPower2
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower1_2.Text);
            }
            set
            {
                txtIoCoverageCellPower1_2.Text = value.ToString();
            }
        }
        public int G1_CellPower3
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower1_3.Text);
            }
            set
            {
                txtIoCoverageCellPower1_3.Text = value.ToString();
            }
        }
        public int G1_Delay1
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay1_1.Text);
            }
            set
            {
                txtIoCoverageDelay1_1.Text = value.ToString();
            }
        }
        public int G1_Delay2
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay1_2.Text);
            }
            set
            {
                txtIoCoverageDelay1_2.Text = value.ToString();
            }
        }
        public int G1_Delay3
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay1_3.Text);
            }
            set
            {
                txtIoCoverageDelay1_3.Text = value.ToString();
            }
        }
        public bool G2_Enable
        {
            get
            {
                return ckbGroup2Enable.Checked;
            }
        }
        public int G2_CellPower1
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower2_1.Text);
            }
            set
            {
                txtIoCoverageCellPower2_1.Text = value.ToString();
            }
        }
        public int G2_CellPower2
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower2_2.Text);
            }
            set
            {
                txtIoCoverageCellPower2_2.Text = value.ToString();
            }
        }
        public int G2_CellPower3
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower2_3.Text);
            }
            set
            {
                txtIoCoverageCellPower2_3.Text = value.ToString();
            }
        }
        public int G2_Delay1
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay2_1.Text);
            }
            set
            {
                txtIoCoverageDelay2_1.Text = value.ToString();
            }
        }
        public int G2_Delay2
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay2_2.Text);
            }
            set
            {
                txtIoCoverageDelay2_2.Text = value.ToString();
            }
        }
        public int G2_Delay3
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay2_3.Text);
            }
            set
            {
                txtIoCoverageDelay2_3.Text = value.ToString();
            }
        }
        public bool G3_Enable
        {
            get
            {
                return ckbGroup3Enable.Checked;
            }
        }
        public int G3_CellPower1
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower3_1.Text);
            }
            set
            {
                txtIoCoverageCellPower3_1.Text = value.ToString();
            }
        }
        public int G3_CellPower2
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower3_2.Text);
            }
            set
            {
                txtIoCoverageCellPower3_2.Text = value.ToString();
            }
        }
        public int G3_CellPower3
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower3_3.Text);
            }
            set
            {
                txtIoCoverageCellPower3_3.Text = value.ToString();
            }
        }
        public int G3_Delay1
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay3_1.Text);
            }
            set
            {
                txtIoCoverageDelay3_1.Text = value.ToString();
            }
        }
        public int G3_Delay2
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay3_2.Text);
            }
            set
            {
                txtIoCoverageDelay3_2.Text = value.ToString();
            }
        }
        public int G3_Delay3
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay3_3.Text);
            }
            set
            {
                txtIoCoverageDelay3_3.Text = value.ToString();
            }
        }
        public bool G4_Enable
        {
            get
            {
                return ckbGroup4Enable.Checked;
            }
        }
        public int G4_CellPower1
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower4_1.Text);
            }
            set
            {
                txtIoCoverageCellPower4_1.Text = value.ToString();
            }
        }
        public int G4_CellPower2
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower4_2.Text);
            }
            set
            {
                txtIoCoverageCellPower4_2.Text = value.ToString();
            }
        }
        public int G4_CellPower3
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower4_3.Text);
            }
            set
            {
                txtIoCoverageCellPower4_3.Text = value.ToString();
            }
        }
        public int G4_CellPower4
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageCellPower4_4.Text);
            }
            set
            {
                txtIoCoverageCellPower4_4.Text = value.ToString();
            }
        }
        public int G4_Delay1
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay4_1.Text);
            }
            set
            {
                txtIoCoverageDelay4_1.Text = value.ToString();
            }
        }
        public int G4_Delay2
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay4_2.Text);
            }
            set
            {
                txtIoCoverageDelay4_2.Text = value.ToString();
            }
        }
        public int G4_Delay3
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay4_3.Text);
            }
            set
            {
                txtIoCoverageDelay4_3.Text = value.ToString();
            }
        }
        public int G4_Delay4
        {
            get
            {
                return Convert.ToInt32(txtIoCoverageDelay4_4.Text);
            }
            set
            {
                txtIoCoverageDelay4_4.Text = value.ToString();
            }
        }

        public ucDbg0001()
        {
            InitializeComponent();
            cmbIoCoverageBand.DataSource = Enum.GetValues(typeof(Wwan_TestCaseInfo.Band));
            cmbIoCoverageBand.SelectedItem = Wwan_TestCaseInfo.Band.UMTS_2100;
        }
    }
}
