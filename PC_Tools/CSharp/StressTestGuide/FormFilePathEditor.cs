using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using com.usi.shd1_tools.TestcasePackage;
using dev.jerry_h.pc_tools.CommonLibrary;

namespace com.usi.shd1_tools.TestGuide
{
    public partial class FormFilePathEditor : CustomDialog
    {
        private static FormFilePathEditor me = null;

        public static String FileName
        {
            get{
                if (me != null)
                {
                    return me.txtFileName.Text;
                }
                else
                {
                    return "";
                }
            }
        }

        public static String FilePath
        {
            get
            {
                if (me != null)
                {
                    return me.txtFilePath.Text;
                }
                else
                {
                    return "";
                }
            }
        }
        
        public FormFilePathEditor()
        {
            this.PositiveButtonText = "Assign";
            this.NegativeButtonText = "Ignore";
            InitializeComponent();
        }
        
        public static DialogResult Show(String fileName,String filePath)
        {
            if (me == null)
            {
                me = new FormFilePathEditor();
            }
            me.txtFileName.Text = fileName;
            me.txtFilePath.Text = filePath;
            return me.ShowDialog();
        }

        private void btnFilePath_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                txtFilePath.Text = openFileDialog1.FileName;
            }
        }

    }
}
