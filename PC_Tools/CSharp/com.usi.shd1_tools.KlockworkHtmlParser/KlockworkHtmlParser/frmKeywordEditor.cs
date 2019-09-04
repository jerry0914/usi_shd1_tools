using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace com.usi.shd1_tools.KlockworkHtmlParser
{
    public partial class frmKeywordConfiguration : Form
    {
        //private List<KeywordCollection> keyCols= null;
        private static frmKeywordConfiguration me;
        public  static List<KeywordCollection> KeywordConfigurations
        {
            get
            {
                List<KeywordCollection> keyCols = new List<KeywordCollection>();
                if (me != null)
                {
                    Regex regRemark = new Regex(@"\[(?<remark>\s*\S+\s*)\]\s-\s*(?<key>\S+)");
                    foreach (TreeNode tnKeyCol in me.tvKeywordCollections.Nodes)
                    {
                        KeywordCollection keyCol = new KeywordCollection(tnKeyCol.Text);
                        foreach (TreeNode tnKey in tnKeyCol.Nodes)
                        {
                            Keyword keyword = null;
                            Match m = regRemark.Match(tnKey.Text);
                            if (m.Success)
                            {
                                keyword = new Keyword(m.Groups["key"].Value);
                                keyword.Remark = m.Groups["remark"].Value;
                            }
                            else
                            {
                                keyword = new Keyword(tnKey.Text);
                            }
                            keyCol.Keys.Add(keyword);
                        }
                    }
                }
                return keyCols;
            }
            set
            {
                if (me != null)
                {
                    foreach (KeywordCollection keyCol in value)
                    {
                        TreeNode tnKeyCol = new TreeNode(keyCol.Category);
                        tnKeyCol.Checked = keyCol.Enable;
                        foreach(Keyword key in keyCol.Keys)
                        {
                            String tmp = key.Value;
                            if (key.Remark != null && key.Remark.Length > 0)
                            {
                                tmp = "["+key.Remark.Trim() + "] - " + tmp;
                            }
                            TreeNode tnKey = new TreeNode(tmp);
                            tnKey.Checked = tnKeyCol.Checked;
                            tnKey.ForeColor = System.Drawing.Color.Gray;
                            tnKeyCol.Nodes.Add(tnKey);
                        }
                        me.tvKeywordCollections.Nodes.Add(tnKeyCol);
                                                }
                }
            }
        }

        public frmKeywordConfiguration()
        {
            InitializeComponent();

        }

        public static DialogResult Open(List<KeywordCollection> keywordCollections)
        {
            if (me == null)
            {
                me = new frmKeywordConfiguration();
            }
            KeywordConfigurations = keywordCollections;
            return me.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private bool autoCheckedFlag = false;
        private void tvKeywordCollections_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!autoCheckedFlag)
            {
                if (e.Node.Parent != null)
                {
                    autoCheckedFlag = true;
                    e.Cancel = true;
                    autoCheckedFlag = false;
                }
            }
        }

        private void tvKeywordCollections_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!autoCheckedFlag)
            {
                autoCheckedFlag = true;
                foreach (TreeNode tn in e.Node.Nodes)
                {
                    tn.Checked = e.Node.Checked;
                }
                autoCheckedFlag = false;
            }
        }        
    }
}
