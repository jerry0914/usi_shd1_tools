using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace com.usi.shd1_tools.RobotframeworkTestGuide
{
    public partial class FormScriptTree : Form
    {
        public static FormScriptTree me = null;
        public static String SelectedScript = "";
        public FormScriptTree()
        {
            InitializeComponent();
        }

        private TreeNode listRobotScripts(String folder)
        {
            String strName = Path.GetFileName(folder); ;
            TreeNode tnCurrent = new TreeNode(strName);
            if (strName != "Template")
            {
                tnCurrent.Tag = "Folder";
                tnCurrent.ToolTipText = folder;
                tnCurrent.ImageIndex = 0;
                tnCurrent.SelectedImageIndex = 0;
                foreach (String subFolder in Directory.GetDirectories(folder))
                {
                    TreeNode subFolderNode = listRobotScripts(subFolder);
                    if (subFolderNode != null)
                    {
                        tnCurrent.Nodes.Add(subFolderNode);
                    }
                }
                foreach (String file in Directory.GetFiles(folder, "*.robot"))
                {
                    String pureName = Path.GetFileNameWithoutExtension(file);
                    if(pureName=="Resource_EmdkScanner")
                    {
                        continue; //Skip the special file
                    }
                    TreeNode tnFile = new TreeNode(pureName);
                    tnFile.Tag = "Script";
                    tnFile.ToolTipText = file;//Save the full path in tag
                    tnFile.ImageIndex = 1;
                    tnFile.SelectedImageIndex = 1;
                    tnCurrent.Nodes.Add(tnFile);
                }
            }
            if (tnCurrent.Nodes.Count > 0)
            {
                return tnCurrent;
            }
            else
            {
                return null;
            }
        }

        public static DialogResult Display(String scriptFolder)
        {
            if (me == null)
            {
                me = new FormScriptTree();
            }
            me.tvMain.Nodes.Clear();
            TreeNode tnRoot = me.listRobotScripts(scriptFolder);
            if (tnRoot != null)
            {
                foreach (TreeNode tn in tnRoot.Nodes)
                {
                    me.tvMain.Nodes.Add(tn);
                    tn.Expand();
                }
            }
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

        private void tvMain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag == "Script")
            {
                SelectedScript = e.Node.ToolTipText;
            }
            else
            {
                SelectedScript = "";
            }

        }

        private void tvMain_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
