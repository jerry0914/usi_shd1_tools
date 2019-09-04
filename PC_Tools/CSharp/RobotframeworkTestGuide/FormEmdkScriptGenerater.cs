using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace com.usi.shd1_tools.RobotframeworkTestGuide
{
    public partial class FormEmdkScriptGenerater : Form
    {
        public static FormEmdkScriptGenerater me = null;
        public static String SelectedScript = "";
        internal String DutId
        {
            get
            {
                return txtDutId.Text;
            }
            set
            {
                txtDutId.Text = value;
            }
        }
        internal String TrsPath
        {
            get
            {
                return txtTrsPath.Text;
            }
            set
            {
                txtTrsPath.Text = value;
            }
        }
        internal String LegacyId
        {
            get
            {
                return txtLegacyId.Text;
            }
            set
            {
                txtLegacyId.Text = value;
            }
        }
        internal String ScannerType
        {
            get
            {
                return cbbScannerType.Text;
            }
            set
            {
                cbbScannerType.Text = value;
            }
        }
        internal String TestPlatform 
        {
            get
            {
                return cbbTestPlatform.Text;
            }
            set
            {
                cbbTestPlatform.Text = value;
            }
        }
        internal String BarcodeDisplayType
        {
            get
            {
                return cbbDisplayType.Text;
            }
            set
            {
                cbbDisplayType.Text = value;
            }
        }
        private String scannerassistPath = "‪C:\\Python27\\Scripts\\scannerassist.bat";
        public FormEmdkScriptGenerater()
        {
            InitializeComponent();
        }
        private List<String> lstEmdkVariableLines = new List<string>();
        private List<String> lstRobotListenerConfigLines = new List<string>();
        private String _emdkScanner_VariablePath;
        private String _robotListener_ConfigPath;
        public static DialogResult Display(String scannerassistPath, String emdkScanner_VariablePath, String robotListener_ConfigPath, String dutID, String trsPath)
        {
            if (me == null)
            {
                me = new FormEmdkScriptGenerater();
            }
            me.TrsPath = trsPath;
            me.DutId = dutID;
            me.scannerassistPath = scannerassistPath;
            me.readEmdkScannerConfigs();
            me._emdkScanner_VariablePath = emdkScanner_VariablePath;
            me._robotListener_ConfigPath = robotListener_ConfigPath;
            return me.ShowDialog();
        }

        private void generateScript()
        {
            updateEmdkScannerConfigs();
            Process ps = new Process();
            ps.StartInfo = new ProcessStartInfo(scannerassistPath);
            ps.StartInfo.Arguments = "--generate";
            ps.StartInfo.WorkingDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            ps.StartInfo.CreateNoWindow = false;
            ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            ps.StartInfo.RedirectStandardOutput = false;
            ps.StartInfo.UseShellExecute = true;
            ps.Start();
        }        
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            String strErrMsg = "";
            #region input data check
            if(DutId.Length==0)
            {
                strErrMsg += "* Please enter the DUT ID" + "\r\n";
            }
            if (LegacyId.Length == 0)
            {
                strErrMsg += "* Please enter the Legacy ID" + "\r\n";
            }
            if (ScannerType.Length == 0)
            {
                strErrMsg += "* Please select the Scanner Type" + "\r\n";
            }
            if (TestPlatform.Length == 0)
            {
                strErrMsg += "* Please select the Test Platform" + "\r\n";
            }
            if (TrsPath.Length == 0)
            {
                strErrMsg += "* Please enter the TRS Path (Test Plan Path)";
            }
            if (BarcodeDisplayType.Length == 0)
            {
                strErrMsg += "* Please select a barcode display type.(LCD/E-Ink)";
            }
            #endregion input data check
            if (strErrMsg.Length==0)
            {
                updateEmdkScannerConfigs();
                generateScript();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show(strErrMsg, "Incorrect input data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void readEmdkScannerConfigs()
        {
            #region EmdkVariables
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(_emdkScanner_VariablePath);
                lstEmdkVariableLines.Clear();
                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    lstEmdkVariableLines.Add(line);
                    if (line.StartsWith("LEGACY_ID:"))
                    {
                        LegacyId = line.Replace("LEGACY_ID:", "").Trim();
                    }
                    if (line.StartsWith("SCANNER_TYPE"))
                    {
                        ScannerType = line.Replace("SCANNER_TYPE:", "").Trim();
                    }
                    if (line.StartsWith("TEST_PLATFORM:"))
                    {
                        TestPlatform = line.Replace("TEST_PLATFORM:", "").Trim();
                    }
                    if (line.StartsWith("BARCODE_DISPLAY_TYPE:"))
                    {
                        BarcodeDisplayType = line.Replace("BARCODE_DISPLAY_TYPE:", "").Trim();
                    }
                }
            }
            catch
            {

            }
            finally
            {
                sr.Close();
            }
            #endregion EmdkVariables
            #region RobotListenerConfig
            sr = null;
            try
            {
                sr = new StreamReader(_robotListener_ConfigPath);
                lstRobotListenerConfigLines.Clear();
                while (!sr.EndOfStream)
                {
                    lstRobotListenerConfigLines.Add(sr.ReadLine());
                }
            }
            catch
            {

            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
            #endregion RobotListenerConfig
        }

        private void updateEmdkScannerConfigs()
        {
            # region update emdk data
            for (int i = 0; i < lstEmdkVariableLines.Count; i++)
            {
                if (lstEmdkVariableLines[i].StartsWith("SCANNER_DUT_ID"))
                {
                    lstEmdkVariableLines[i] = "SCANNER_DUT_ID: " + DutId;
                }
                else if (lstEmdkVariableLines[i].StartsWith("LEGACY_ID"))
                {
                    lstEmdkVariableLines[i] = "LEGACY_ID: " + LegacyId;
                }
                else if (lstEmdkVariableLines[i].StartsWith("SCANNER_TYPE"))
                {
                    lstEmdkVariableLines[i] = "SCANNER_TYPE: " + ScannerType;
                }
                else if (lstEmdkVariableLines[i].StartsWith("TEST_PLATFORM"))
                {
                    lstEmdkVariableLines[i] = "TEST_PLATFORM: " + TestPlatform;
                }
                else if (lstEmdkVariableLines[i].StartsWith("TRS_PATH"))
                {
                    lstEmdkVariableLines[i] = "TRS_PATH: " + TrsPath;
                }
                else if (lstEmdkVariableLines[i].StartsWith("BARCODE_DISPLAY_TYPE"))
                {
                    lstEmdkVariableLines[i] = "BARCODE_DISPLAY_TYPE: " + BarcodeDisplayType;
                }                
            }
            # endregion update emdk data
            # region update robotlistenerconfig
            for (int i = 0; i < lstRobotListenerConfigLines.Count; i++)
            {
                if (lstRobotListenerConfigLines[i].StartsWith("DUT_ID"))
                {
                    lstRobotListenerConfigLines[i] = "DUT_ID: " + DutId;
                }
                else if (lstRobotListenerConfigLines[i].StartsWith("Update_Results_In_TRS"))
                {
                    lstRobotListenerConfigLines[i] = "Update_Results_In_TRS: " + TrsPath;
                }
            }
            # endregion update robot data
            #region write emdk file
            StreamWriter sw = null;
            try
            {
                
                sw = new StreamWriter(_emdkScanner_VariablePath, false);
                foreach (String line in lstEmdkVariableLines)
                {
                    sw.WriteLine(line);
                }
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            #endregion write emdk file
            #region write robotlistener config
            sw = null;
            try
            {
                sw = new StreamWriter(_robotListener_ConfigPath, false);
                foreach (String line in lstRobotListenerConfigLines)
                {
                    sw.WriteLine(line);
                }
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            #endregion write robotlistener config
        }
    
    }
}
