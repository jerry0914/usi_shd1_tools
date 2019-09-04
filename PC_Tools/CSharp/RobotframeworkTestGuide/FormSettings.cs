using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace com.usi.shd1_tools.RobotframeworkTestGuide
{
    public partial class FormSettings : Form
    {
        public static String PybotPath = "";
        public static String HostPath = "";
        public static String ScriptFolder = "";
        public static int DeviceMonitorInterval = 6000;
        public static String SVN_Server_Address = "";
        private static FormSettings me = null;
        

        public FormSettings()
        {
            InitializeComponent();

        }
        public static DialogResult Display()
        {
            if(me==null)
            {
                me = new FormSettings();
            }
            me.loadConfig();
            return me.ShowDialog(); 
        }

        public void loadConfig()
        {
            try
            {
                XElement xeRoot = XElement.Load(FormMain.configPath);
                XElement xeRF = xeRoot.Element("Robotframework");
                XElement xePybot = xeRF.Element("pybot");
                XElement xeScriptsFolder = xeRF.Element("scripts_folder");
                XElement xeTestResultFolder = xeRF.Element("test_result_folder");
                XElement xeEmdkVariable = xeRF.Element("emdk_folder");

                XElement xeTool = xeRoot.Element("Tool_Settings");
                XElement xeMonitorInterval = xeTool.Element("monitor_interval");

                txtPybotPath.Text = xePybot.Value;
                txtScriptsFolder.Text = xeScriptsFolder.Value;
                txtTestResultFolder.Text = xeTestResultFolder.Value;
                txtEmdkVairable.Text = xeEmdkVariable.Value;
                int interval = Convert.ToInt32(xeMonitorInterval.Value);
                numMonitorDeviceInterval.Value = interval;
            }
            catch
            {

            }
        }

        private void saveConfig()
        {
            XElement xeRoot;
            if (!File.Exists(FormMain.configPath))
            {
                xeRoot = new XElement("root");
                xeRoot.Save(FormMain.configPath);
            }
            xeRoot = XElement.Load(FormMain.configPath);            
            XElement xeRF = getSubElement(ref xeRoot, "Robotframework");
            XElement xePybot = getSubElement(ref xeRF, "pybot");
            XElement xeScriptsFolder = getSubElement(ref xeRF, "scripts_folder");
            XElement xeTestResultFolder = getSubElement(ref xeRF,"test_result_folder");
            XElement xeEmdkFolder = getSubElement(ref xeRF, "emdk_folder");

            xePybot.Value = txtPybotPath.Text;
            xeScriptsFolder.Value = txtScriptsFolder.Text;
            xeTestResultFolder.Value = txtTestResultFolder.Text;
            xeEmdkFolder.Value = txtEmdkVairable.Text;

            XElement xeTool = getSubElement(ref xeRoot, "Tool_Settings");
            XElement xeMonitorInterval = getSubElement(ref xeTool, "monitor_interval");
            xeMonitorInterval.Value = numMonitorDeviceInterval.Value.ToString();
            
            xeRoot.Save(FormMain.configPath);
        }
        
        private XElement getSubElement(ref XElement xeParent, String childElementName)
        {
            XElement xeChild = xeParent.Element(childElementName);
            if (xeChild == null)
            {
                xeChild = new XElement(childElementName);
                xeParent.Add(xeChild);
            }
            return xeChild;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            saveConfig();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnPybotPath_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "pybot|pybot.bat";
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtPybotPath.Text = openFileDialog1.FileName;
            }
        }

        //private void btnHostFolder_Click(object sender, EventArgs e)
        //{
        //    if(folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
        //    {
        //        txtHostFolder.Text = folderBrowserDialog1.SelectedPath;
        //    }
        //}

        private void btnScriptsFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtScriptsFolder.Text = folderBrowserDialog1.SelectedPath+"\\";
            }
        }

        private void btnTestResultFolder_Click(object sender, EventArgs e)
        {
           if(folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
           {
               txtTestResultFolder.Text = folderBrowserDialog1.SelectedPath + "\\";
           }
        }

        private void btnEmdkVairable_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtEmdkVairable.Text = folderBrowserDialog1.SelectedPath+"\\";
            }
        }
    }
}
