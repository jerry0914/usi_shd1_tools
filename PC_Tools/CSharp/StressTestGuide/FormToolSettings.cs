using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using com.usi.shd1_tools.TestcasePackage;
using dev.jerry_h.pc_tools.CommonLibrary;

namespace com.usi.shd1_tools.TestGuide
{
    public partial class FormToolSettings : CustomDialog
    {
        private static FormToolSettings me = null;
        private String platform = "";
        public FormToolSettings()
        {
            InitializeComponent();
        }
     
        #region Public properties

        public static int TestResultFilter
        {
            get
            {
                if (me != null)
                {
                    int result = (int)(Convert.ToInt32(me.chkResult_None.Checked) << 0 |
                            Convert.ToInt32(me.chkResult_P.Checked) << 1 |
                            Convert.ToInt32(me.chkResult_I.Checked) << 2 |
                            Convert.ToInt32(me.chkResult_B.Checked) << 3 |
                            Convert.ToInt32(me.chkResult_F.Checked) << 4);
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (me == null)
                {
                    me = new FormToolSettings();
                }
                me.setTestResultFilter(value);
            }
        }
             
        public static String ProfileApplication_Path
        {
            get
            {
                if (me != null)
                {
                    return me.txtProfilePath.Text;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (me == null)
                {
                    me = new FormToolSettings();
                }
                me.txtProfilePath.Text = value;
            }
        }

        public static String TRS_ConfigSourceFolder
        {
            get
            {
                if (me != null)
                {
                    return me.txtTRSConfigSource.Text;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (me == null)
                {
                    me = new FormToolSettings();
                }
                me.txtTRSConfigSource.Text = value;
            }
        }
        
        public static bool KeywordFilterEnable
        {
            get
            {
                if (me != null)
                {
                    return me.chkKeywordFilter.Checked;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (me == null)
                {
                    me = new FormToolSettings();
                }
                me.chkKeywordFilter.Checked = value;
            }
        }

        public static String KeywordFilter_Text
        {
            get
            {
                if (me != null)
                {
                    return me.txtTestCaseKeyword.Text;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (me == null)
                {
                    me = new FormToolSettings();
                }
                me.txtTestCaseKeyword.Text = value;
            }
        }

        public static String TRS_AttachmentSourceFolder
        {
            get
            {
                if(me!=null)
                {
                    return me.txtAttachmentSource.Text;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (me == null)
                {
                    me = new FormToolSettings();
                }
                me.txtAttachmentSource.Text = value;
            }
        }

        public static String TRS_AttachmentDestinationFolder
        {
            get
            {
                if (me != null)
                {
                    return me.txtAttachmentDestination.Text;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (me == null)
                {
                    me = new FormToolSettings();
                }
                me.txtAttachmentDestination.Text = value;
            }
        }

        public static String Pre_ConditionScriptFolder
        {
            get
            {
                if (me != null)
                {
                    return me.txtPreconditionScriptSource.Text;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (me == null)
                {
                    me = new FormToolSettings();
                }
                me.txtPreconditionScriptSource.Text = value;
            }
        }

        public static String LogFolder
        {
            get
            {
                if (me != null)
                {
                    return me.txtLogFolder.Text;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                if (me == null)
                {
                    me = new FormToolSettings();
                }
                me.txtLogFolder.Text = value;
            }
        }

        public static int MaxLogInterval
        {
            get
            {
                if (me != null)
                {
                    return (int)me.numLogIntervalLimit.Value;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (me == null)
                {
                    me = new FormToolSettings();
                }
                if (value < me.numLogIntervalLimit.Minimum)
                {
                    me.numLogIntervalLimit.Value = me.numLogIntervalLimit.Minimum;
                }
                else if (value > me.numLogIntervalLimit.Maximum)
                {
                    me.numLogIntervalLimit.Value = me.numLogIntervalLimit.Maximum;
                }
                else
                {
                    me.numLogIntervalLimit.Value = value;
                }
            }

        }

        #endregion Public properties

        public static DialogResult Show(String currentPlatform)
        {
            if (me == null)
            {
                me = new FormToolSettings();
            }
            me.platform = currentPlatform;
            return me.ShowDialog();
        }

        private void btnConfigSource_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtTRSConfigSource.Text;
            if (folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtTRSConfigSource.Text = folderBrowserDialog1.SelectedPath;
            }
        }
                
        delegate void delVoidInt(int intPara);
        private void setTestResultFilter(int filter)
        {
            if (this.InvokeRequired)
            {
                delVoidInt del = new delVoidInt(setTestResultFilter);
                this.Invoke(del, filter);
            }
            else
            {
                chkResult_None.Checked = ((filter & 0x01 << 0) > 0);
                chkResult_P.Checked = ((filter & 0x01 << 1) > 0);
                chkResult_I.Checked = ((filter & 0x01 << 2) > 0);
                chkResult_B.Checked = ((filter & 0x01 << 3) > 0);
                chkResult_F.Checked = ((filter & 0x01 << 4) > 0);
            }
        }

        #region UI Control

        private void chkKeywordFilter_CheckedChanged(object sender, EventArgs e)
        {
            txtTestCaseKeyword.Enabled = chkKeywordFilter.Checked;
        }

        private void btnConfigSource_LocationChanged(object sender, EventArgs e)
        {
            txtTRSConfigSource.Width = btnTRSConfigSource.Location.X - txtTRSConfigSource.Location.X - 3;
        }

        private void btnAttachmentSource_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtAttachmentSource.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnProfilePath_Click(object sender, EventArgs e)
        {
            if (platform.ToLower().Equals("ce"))
            {
                openFileDialog1.Filter = "exe file|*.exe";
            }
            else if (platform.ToLower().Equals("android"))
            {
                openFileDialog1.Filter = "APK file|*.apk";
            }
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtProfilePath.Text = openFileDialog1.FileName;
            }
        }

        private void btnTRSConfigSource_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtTRSConfigSource.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnPreconditionScriptSource_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtPreconditionScriptSource.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnLogFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtLogFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        #endregion UI Control
    }
}
