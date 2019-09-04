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
    public partial class FormMergeResults : Form
    {
        public static FormMergeResults me = null;
        public static String SelectedScript = "";
        public FormMergeResults()
        {
            InitializeComponent();
        }

        //private void mergeTestResult(String resultFolder)
        //{
        //    List<String> lstOutputs = getOutputResultList(folderBrowserDialog1.SelectedPath);
        //    String rebotPath = Path.GetDirectoryName(pybotPath) + "\\rebot.bat";
        //    if (File.Exists(rebotPath))
        //    {
        //        if (lstOutputs.Count > 0)
        //        {
        //            Process ps = new Process();
        //            ps.StartInfo = new ProcessStartInfo(rebotPath);
        //            ps.StartInfo.WorkingDirectory = resultFolder;
        //            ps.StartInfo.CreateNoWindow = true;
        //            ps.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        //            ps.StartInfo.RedirectStandardOutput = false;
        //            ps.StartInfo.UseShellExecute = true;
        //            String strArgs = "--merge ";
        //            foreach (string output in lstOutputs)
        //            {
        //                strArgs += output + " ";
        //            }
        //            ps.StartInfo.Arguments = strArgs;
        //            ps.Start();
        //        }
        //        else
        //        {
        //            MessageBox.Show("No output.xml file found!!");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("[rebot.bat] does not exist:\r\nPlease check your installation of Robotframework,\r\n and try again!");
        //    }
        //}

        //private List<String> getOutputResultList(String folder)
        //{
        //    List<String> lstResults = new List<string>();
        //    foreach (String file in Directory.GetFiles(folder, "output.xml"))
        //    {
        //        lstResults.Add(file);
        //    }
        //    foreach (String subFolder in Directory.GetDirectories(folder))
        //    {
        //        lstResults.AddRange(getOutputResultList(subFolder).ToArray());
        //    }
        //    return lstResults;
        //}

        public static DialogResult Display(String resultFolder)
        {
            if (me == null)
            {
                me = new FormMergeResults();
            }
            //me.tvMain.Nodes.Clear();
            //TreeNode tnRoot = me.listRobotScripts(scriptFolder);
            //if (tnRoot != null)
            //{
            //    foreach(TreeNode tn in tnRoot.Nodes)
            //    {
            //        me.tvMain.Nodes.Add(tn);
            //        tn.Expand();
            //    }
            //}
            return me.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
