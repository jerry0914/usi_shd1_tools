using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using dev.jerry_h.pc_tools.AndroidLibrary;
using dev.jerry_h.pc_tools.CommonLibrary;
using WeifenLuo.WinFormsUI.Docking;

namespace AutomationTooling
{
    public partial class FormMain : Form
    {
        public string Title
        {
            get
            {
                object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title.Length > 0)
                        return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        public Version Version
        {
            get { return Assembly.GetCallingAssembly().GetName().Version; }
        }
       
        private Color tsbOpenedColor = SystemColors.Highlight;
        private Color tsbClosedColor = SystemColors.Control;
        private void DockContent_ShownEventHandler(object sender, EventArgs ea)
        {
            DockContent dkc = sender as DockContent;
            if (dkc.Equals(dcScriptsExplorer))
            {
                tsbScriptsExplorer.BackColor = tsbOpenedColor;
            }
            else if (dkc.Equals(dcCommands))
            {
                tsbCommandForm.BackColor = tsbOpenedColor;
            }
            else if (dkc.Equals(dcOutputFrm))
            {                
                tsbOutputForm.BackColor = tsbOpenedColor;
            }
        }
        private void DockContent_DisposedEventHandler(object sender, EventArgs ea)
        {
            DockContent dkc = sender as DockContent;
            if (dkc.Equals(dcScriptsExplorer))
            {
                tsbScriptsExplorer.BackColor = tsbClosedColor;
                dcScriptsExplorer = null;
            }
            else if (dkc.Equals(dcCommands))
            {
                tsbCommandForm.BackColor = tsbClosedColor;
                dcCommands = null;
            }
            else if (dkc.Equals(dcCommands))
            {
                tsbCommandForm.BackColor = tsbClosedColor;
                dcCommands = null;
            }
        }        
        private void addScriptEditor(String name)
        {
            DockContent_ScriptEditor dc_se = new DockContent_ScriptEditor();
            dc_se.Text = name;
            dpnlMain.DocumentStyle = DocumentStyle.DockingSdi;
            dc_se.Show(dpnlMain, DockState.Document);
        }
        private Main main;
        public FormMain()
        {
            InitializeComponent();
            main = new Main();
            this.Text = Title + " - V" + Version.ToString(3);
            addScriptEditor("New");
            tsbOutputForm_Click(tsbOutputForm, new EventArgs());
            tsbCommandForm_Click(tsbCommandForm, new EventArgs());
            tsbScriptsExplorer_Click(tsbScriptsExplorer, new EventArgs());
        }
        
        DockContent_Commands dcCommands = null;
        private void tsbCommandForm_Click(object sender, EventArgs e)
        {
            if (dcCommands == null)
            {
                dcCommands = new DockContent_Commands();
                dcCommands.Shown += new EventHandler(DockContent_ShownEventHandler);
                dcCommands.Disposed += new EventHandler(DockContent_DisposedEventHandler);
                dcCommands.SuspendLayout();
                dcCommands.DockAreas = DockAreas.DockLeft | DockAreas.DockRight;
                dcCommands.CloseButtonVisible = true;
                dcCommands.ShowHint = DockState.DockLeft;
                dcCommands.Visible = true;
            }
            dcCommands.Show(dpnlMain);
            dcCommands.ResumeLayout(true);
        }

        DockContent_PackageExplorer dcScriptsExplorer = null;
        private void tsbScriptsExplorer_Click(object sender, EventArgs e)
        {
            if (dcScriptsExplorer == null)
            {
                dcScriptsExplorer = new DockContent_PackageExplorer();
                dcScriptsExplorer.Shown += new EventHandler(DockContent_ShownEventHandler);
                dcScriptsExplorer.Disposed += new EventHandler(DockContent_DisposedEventHandler);
                dcScriptsExplorer.SuspendLayout();
                dcScriptsExplorer.DockAreas = DockAreas.DockLeft | DockAreas.DockRight;
                dcScriptsExplorer.CloseButtonVisible = true;
                dcScriptsExplorer.ShowHint = DockState.DockLeft;
                dcScriptsExplorer.Visible = true;
            }
            dcScriptsExplorer.ResumeLayout(true);
            dcScriptsExplorer.Show(dpnlMain);
        }

        DockContent_Output dcOutputFrm = null;
        private void tsbOutputForm_Click(object sender, EventArgs e)
        {
            if (dcOutputFrm == null)
            {
                dcOutputFrm = new DockContent_Output();
                dcOutputFrm.Shown += new EventHandler(DockContent_ShownEventHandler);
                dcOutputFrm.Disposed += new EventHandler(DockContent_DisposedEventHandler);
                dcOutputFrm.SuspendLayout();
                dcOutputFrm.DockAreas = DockAreas.DockBottom | DockAreas.Float;
                dcOutputFrm.CloseButtonVisible = false;
                dcOutputFrm.ShowHint = DockState.DockBottom;
                dcOutputFrm.Visible = true;
            }
            dcOutputFrm.Show(dpnlMain);
            dcOutputFrm.ResumeLayout(true);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addScriptEditor("New");
        }

        private void lsvMain_Resize(object sender, EventArgs e)
        {

        }
    }
}
