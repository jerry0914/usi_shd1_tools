using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.usi.shd1_tools.XmlQuickModifier
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnXmlFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
            {

            }
        }

        private void gvMain_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }
    }
}
