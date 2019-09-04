using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.usi.shd1_tools.TelephonyAutomation
{
    public partial class frmAddFrequencyAmplitude : Form
    {
        private static frmAddFrequencyAmplitude me;
        public static double Frequency
        {
            get
            {
                if(me!=null)
                {
                    return Convert.ToDouble(me.numFrequency.Value);
                }
                else
                {
                    return 0.0;
                }
            }
        }

        public static double Amplitude
        {
            get
            {
                if (me != null)
                {
                    return Convert.ToDouble(me.numAmplitude.Value);
                }
                else
                {
                    return 0.0;
                }
            }
        }

        public frmAddFrequencyAmplitude()
        {
            InitializeComponent();
        }

        public static DialogResult SetFrequencyAmplitude()
        {
            if(me==null)
            {
                me = new frmAddFrequencyAmplitude();
            }
            return me.ShowDialog();
        }

        public static DialogResult SetFrequencyAmplitude(double frequency, double amplitude)
        {
            if (me == null)
            {
                me = new frmAddFrequencyAmplitude();
            }
            me.numFrequency.Value = Convert.ToDecimal(frequency);
            me.numAmplitude.Value = Convert.ToDecimal(amplitude);
            return me.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
