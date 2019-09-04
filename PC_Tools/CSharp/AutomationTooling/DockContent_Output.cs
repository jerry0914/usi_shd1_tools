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
    public partial class DockContent_Output : DockContent
    {
        public DockContent_Output()
        {
            InitializeComponent();
        }

        private void lsvLiveLog_Resize(object sender, EventArgs e)
        {
            chMessage.Width = lsvLiveLog.Width - chTime.Width - chLogLevel.Width - chTag.Width - 5;
        }
    }
}
