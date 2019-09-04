using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Aspose.Cells;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        List<TestCase> lstTcTar = new List<TestCase>();
        List<TestCase> lstTcRef = new List<TestCase>();
        public Form1()
        {
            InitializeComponent();
        }

        private bool compare()
        {
            bool compareResult = true;
            Workbook workBook1;
            Worksheet workSheet1;
            Workbook workBook2;
            Worksheet workSheet2;
            int tarTotalcount =0 ,tarInRefCount=0, refcount = 0;
            lstTcTar.Clear();
            lstTcRef.Clear();
            rtxtResult.Clear();
            try
            {
                workBook1 = new Workbook(txtTestPlanPath1.Tag.ToString());
                workSheet1 = workBook1.Worksheets["EnterResults"];
                int TarIdCol = (int)numTarIdCol.Value;
                int RefIdCol = (int)numRefIdCol.Value;
                int RefKeyCol = (int)numRefKeyColumn.Value;
                if (workSheet1 == null)
                {
                    workSheet1 = workBook1.Worksheets[0];
                }
                workBook2 = new Workbook(txtTestPlanPath2.Tag.ToString());
                workSheet2 = workBook2.Worksheets["EnterResults"];
                if (workSheet2 == null)
                {
                    workSheet2 = workBook2.Worksheets[0];
                }
                int rowCnt = 0;
                Cell cellTemp1=null,cellTemp2 = null,cellKeyword = null;

                #region get Reference file TCID
                for (rowCnt = 0; rowCnt < workSheet2.Cells.Rows.Count; rowCnt++)
                {
                    cellTemp2 = workSheet2.Cells.Rows[rowCnt].GetCellOrNull(RefIdCol);
                    cellKeyword = workSheet2.Cells.Rows[rowCnt].GetCellOrNull(RefKeyCol);
                    if (cellTemp2 != null && cellTemp2.Value!=null)
                    {
                        String tcidRef = "",strKeyword="";
                        tcidRef = getTCID(cellTemp2.Value.ToString());
                        if(cellKeyword!=null && cellKeyword.Value!=null)
                        {
                            strKeyword = cellKeyword.Value.ToString();
                        }
                        if (tcidRef.Length > 0 && strKeyword.Length>0 && (strKeyword.Contains(txtRefKeyword.Text) | txtRefKeyword.Text.Trim().Length == 0))
                        {
                            TestCase tcTemp = new TestCase();
                            tcTemp.ID = tcidRef;
                            tcTemp.RowIndex = rowCnt;
                            lstTcRef.Add(tcTemp);
                            lvTestPlan2.Items.Add(tcidRef);
                            refcount++;
                        }
                    }
                }
                #endregion get Reference file TCID

                #region get Target file TCID
                for (rowCnt = 0; rowCnt < workSheet1.Cells.Rows.Count; rowCnt++)
                {
                   cellTemp1 = workSheet1.Cells.Rows[rowCnt].GetCellOrNull(TarIdCol);
                   if (cellTemp1 != null && cellTemp1.Value!=null)
                   {
                       String tcidTar = getTCID(cellTemp1.Value.ToString());
                       if (tcidTar.Length > 0)
                       {
                           TestCase tcTemp = new TestCase();
                           tcTemp.ID = tcidTar;
                           tcTemp.RowIndex = rowCnt;
                           bool containInRef = false;
                           foreach (TestCase tcRef in lstTcRef)
                           {
                               if (tcTemp.ID.Trim().Equals(tcRef.ID.Trim()))
                               {
                                   containInRef = true;
                                   tarInRefCount++;
                                   break;
                               }
                           }
                           if (!containInRef)
                           {
                               if (tcTemp.ID.Contains(txtTarIdFilter.Text) || txtTarIdFilter.Text.Length == 0)
                               {
                                   tcTemp.TestResult = TestCase.EnumTestResult.I;
                                   tcTemp.ResultComment = "[Filtered by AT] This test case is not applicable to DUT";
                               }
                           }
                           ListViewItem li = new ListViewItem(tcTemp.ID);
                           ListViewItem.ListViewSubItem lsi1 = new ListViewItem.ListViewSubItem(li,tcTemp.TestResult.ToString());
                           ListViewItem.ListViewSubItem lsi2 = new ListViewItem.ListViewSubItem(li, tcTemp.ResultComment);
                           li.SubItems.AddRange(new ListViewItem.ListViewSubItem[] { lsi1, lsi2 });
                           lstTcTar.Add(tcTemp);
                           lvTestPlan1.Items.Add(li);
                           tarTotalcount++;
                       }                       
                   }
                }
                #endregion get Reference file TCID
            }
            catch (Exception ex)
            {
                compareResult = false;
                MessageBox.Show(ex.Message+"\r\n"+ex.StackTrace);
            }
            MessageBox.Show("Ref count = "+ refcount + "\r\n"+
                                             "Target total count = " + tarTotalcount+ "\r\n"+
                                             "Target file matches ref file = "+tarInRefCount);

            return compareResult;
        }

        private String getTCID(String msg)
        {
            String rtnValue = "";
            String strReg = @"(\s|\S)*(?<id>VT\d+-\d+)(\s|\S)*";
            Regex reg = new Regex(strReg);
            Match m = reg.Match(msg);
            if (m.Success)
            {
                rtnValue = m.Groups["id"].Value;
            }
            return rtnValue;
        }


        private void txtTestPlanPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (openFileDialog_TestPlan.ShowDialog().Equals(DialogResult.OK))
            {
                txtTestPlanPath1.Tag = openFileDialog_TestPlan.FileName;
                txtTestPlanPath1.Text = Path.GetFileName(txtTestPlanPath1.Tag.ToString());
            }
        }

        private void txtTestPlanPath2_DoubleClick(object sender, EventArgs e)
        {
            if (openFileDialog_TestPlan.ShowDialog().Equals(DialogResult.OK))
            {
                txtTestPlanPath2.Tag = openFileDialog_TestPlan.FileName;
                txtTestPlanPath2.Text = Path.GetFileName(txtTestPlanPath2.Tag.ToString());
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            //checkKeyColumn();
            lvTestPlan1.Clear();
            lvTestPlan2.Clear();
            #region Preparing column headers
            lvTestPlan1.Columns.Add("TCID");
            lvTestPlan1.Columns.Add("Result");
            lvTestPlan1.Columns.Add("Comment");
            lvTestPlan2.Columns.Add("TCID");
            #endregion Preparing column headers
            compare();
            lvTestPlan1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lvTestPlan2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void lvTestPlan1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            for (int i = 0; i < lvTestPlan1.Items.Count; i++)
            {
                lvTestPlan1.Items[i].BackColor = System.Drawing.Color.White;
               // lvTestPlan2.Items[i].BackColor = System.Drawing.Color.White;
            }
            if (e.IsSelected)
            {
                lvTestPlan1.Items[e.ItemIndex].BackColor = System.Drawing.Color.Lime;
                //lvTestPlan2.Items[e.ItemIndex].EnsureVisible();
                //lvTestPlan2.Items[e.ItemIndex].BackColor = System.Drawing.Color.LightPink;
                Application.DoEvents();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                Workbook workBook1 = new Workbook(saveFileDialog1.FileName);
                Worksheet workSheet1 = workBook1.Worksheets["EnterResults"];
                int TarIdCol = (int)numTarIdCol.Value;
                int TarResultCol = (int)numTarResultColumn.Value;
                int TarCommentCol = (int)numTarCommentColumn.Value;
                if (workSheet1 == null)
                {
                    workSheet1 = workBook1.Worksheets[0];
                }
                foreach (TestCase tc in lstTcTar)
                {
                    if (tc.TestResult.Equals(TestCase.EnumTestResult.I))
                    {
                        //workSheet1.Cells.Rows[tc.RowIndex].GetCellByIndex(TarResultCol).PutValue(tc.TestResult.ToString());
                        //workSheet1.Cells.Rows[tc.RowIndex].GetCellByIndex(TarCommentCol).PutValue(tc.ResultComment);
                        workSheet1.Cells.Rows[tc.RowIndex][TarResultCol].PutValue(tc.TestResult.ToString());
                        workSheet1.Cells.Rows[tc.RowIndex][TarCommentCol].PutValue(tc.ResultComment);
                    }
                }           
                workBook1.Save(saveFileDialog1.FileName);
                MessageBox.Show("Done!");
            }
        }

        private void numTarIdCol_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
