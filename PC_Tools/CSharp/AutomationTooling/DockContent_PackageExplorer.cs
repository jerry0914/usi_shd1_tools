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
    public partial class DockContent_PackageExplorer : DockContent
    {
        public DockContent_PackageExplorer()
        {
            InitializeComponent();
        }

        private void btnOpenFolder_Move(object sender, EventArgs e)
        {
            txtScriptFolder.Width = btnOpenFolder.Location.X - txtScriptFolder.Location.X - this.DefaultMargin.Size.Width;
        }
    }
}
