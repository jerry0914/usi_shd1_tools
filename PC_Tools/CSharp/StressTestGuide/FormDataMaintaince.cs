using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.usi.shd1_tools.TestcasePackage;

namespace com.usi.shd1_tools.TestGuide
{
    public partial class FormDataMaintaince : UserControl
    {
        FormMain frmMain;
        public FormDataMaintaince(FormMain mainForm)
        {
            frmMain = mainForm;
            InitializeComponent();
        }

        private void btnConnectionTest_Click(object sender, EventArgs e)
        {
            bool connected = SQL_Client.Connect(txtServerName.Text, txtUserName.Text, txtPassword.Text, "EMSTAF");
            if (connected)
            {
                btnConnectionTest.Text = "Connected";
                btnConnectionTest.BackColor = System.Drawing.Color.Green;
                btnConnectionTest.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                btnConnectionTest.Text = "Disconnected";
                btnConnectionTest.BackColor = System.Drawing.Color.Crimson;
                btnConnectionTest.ForeColor = System.Drawing.Color.White;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnConnectionTest.Enabled = txtServerName.Text.Length > 0 && txtUserName.Text.Length > 0 && txtPassword.Text.Length > 0;
        }
    }
}
