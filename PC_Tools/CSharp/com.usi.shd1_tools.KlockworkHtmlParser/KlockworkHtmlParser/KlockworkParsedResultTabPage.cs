using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.usi.shd1_tools.KlockworkHtmlParser
{
    public partial class KlockworkParsedResultTabPage : TabPage
    {
        public readonly KeyValuePair<String, List<KlockworkParsedMessage>> KlockworkParsedMessages;
        public KlockworkParsedResultTabPage(KeyValuePair<String,List<KlockworkParsedMessage>> messages)
        {
            InitializeComponent();
            KlockworkParsedMessages = messages;
            initializeMessageTree();
        }
        private void initializeMessageTree()
        {
            this.Text = KlockworkParsedMessages.Key;
            foreach (KlockworkParsedMessage msg in KlockworkParsedMessages.Value)
            {
                TreeNode tnHeader =new TreeNode() ;
                if (msg.Index > 0)
                {
                    tnHeader.Text  = "#" + msg.Index + ": " + msg.Header;
                }
                else
                {
                    tnHeader.Text = msg.Header;
                }
                TreeNode tnPath = new TreeNode(msg.PathAndLocation);
                tnPath.ForeColor = System.Drawing.Color.DarkRed;
                TreeNode tnDescription = new TreeNode(msg.ErrorDescription);
                tnDescription.ForeColor = System.Drawing.Color.DarkGreen;
                tnHeader.Nodes.Add(tnPath);
                tnHeader.Nodes.Add(tnDescription);
                tvMessages.Nodes.Add(tnHeader);
            }
            this.Text += "(" + KlockworkParsedMessages.Value.Count + ")";
            this.Show();
        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            tvMessages.ExpandAll();
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            tvMessages.CollapseAll();
        }
    }
}
