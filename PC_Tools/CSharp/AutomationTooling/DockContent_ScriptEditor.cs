using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AutomationTooling
{
    public partial class DockContent_ScriptEditor : DockContent
    {
        public DockContent_ScriptEditor()
        {
            InitializeComponent();
        }

        private void lsvMain_Resize(object sender, EventArgs e)
        {
            lsvMain.Columns[1].Width = lsvMain.Width - lsvMain.Columns[0].Width - 6;
        }

        private void lsvMain_ControlAdded(object sender, ControlEventArgs e)
        {
            showLineNumber();
        }

        private void lsvMain_ControlRemoved(object sender, ControlEventArgs e)
        {
            showLineNumber();
        }

        private void showLineNumber()
        {
            foreach (ListViewItem li in lsvMain.Items)
            {
                li.SubItems[0].Text = li.Index.ToString();
            }
        }    
    }
}
