using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.IO;
using com.usi.shd1_tools.TestcasePackage;


namespace com.usi.shd1_tools.TestGuide
{
    public partial class FormConfigEditor : CustomDialog
    {
        private String title = "Config Editor";
        private List<String> keywordOfConfigParser = new List<string>(new String[] { "IP", "Port", "Source" });
        static FormConfigEditor me = null;
        TestCase tcSelected = null;
        public FormConfigEditor()
        {
            InitializeComponent();
        }

        public static DialogResult Show()
        {
            if (me == null)
            {
                me = new FormConfigEditor();
            }
            return me.ShowDialog();
        }

        public static void ClearItems()
        {
            if (me != null)
            {
                me.dataGridViewConfigList.Rows.Clear();
            }
        }

        public static void AddItem(params object[] values)
        {
            if (me == null)
            {
                me = new FormConfigEditor();
            }
            me.dataGridViewConfigList.Rows.Add(values);
        }

        public static void SetConfigText(TestCase SelectedTestCase, String configPath)
        {
            if (me == null)
            {
                me = new FormConfigEditor();
            }
            me.tcSelected = SelectedTestCase;
            me.Text = me.title +" - "+ Path.GetFileName(configPath);
            me.showConfigContent(configPath);
        }

        public static String GetConfigText()
        {
            if (me != null)
            {
                return me.rtxtConfig.Text;
            }
            else
            {
                return "";
            }
        }

        public static void ClearConfig()
        {
            if (me != null)
            {
                me.Text = "";
            }
        }

        public static DataGridViewRowCollection GetItems()
        {
            if (me != null)
            {
                return me.dataGridViewConfigList.Rows;
            }
            else
            {
                return null;
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            quickEdit();
        }

        private void quickEdit()
        {
            DataGridViewRowCollection drc = FormConfigEditor.GetItems();
            if (drc != null)
            {
                foreach (DataGridViewRow row in drc)
                {
                    DataGridViewCell cellKey = row.Cells["ColumnElement"];
                    DataGridViewCell cellValue = row.Cells["ColumnValue"];
                    if (cellKey != null && cellKey.Value != null && cellKey.Value.ToString().Length > 0 &&
                        cellValue != null && cellValue.Value != null && cellValue.Value.ToString().Length > 0)
                    {
                        searchAndReplace(cellKey.Value.ToString(), cellValue.Value.ToString());
                    }
                }
            }
        }

        private void searchAndReplace(String keyword, String newValue)
        {
            int startIndex = 0;
            int endIndex = 0;
            while (startIndex >= 0 && endIndex >= 0)
            {
                startIndex = rtxtConfig.Text.ToLower().IndexOf("<" + keyword.ToLower() + ">", startIndex);
                endIndex = rtxtConfig.Text.ToLower().IndexOf("</" + keyword.ToLower() + ">", endIndex);
                if (startIndex >= 0 && endIndex > startIndex)
                {
                    startIndex += ("<" + keyword + ">").Length;
                    rtxtConfig.Select(startIndex, endIndex - startIndex);
                    rtxtConfig.SelectedText = newValue;
                    endIndex += ("<" + keyword + ">").Length;
                    startIndex++;
                    endIndex++;
                }
            }
        }

        private void searchAndHightlight_Config(String keyword, Color backColor)
        {
            int startIndex = 0;
            int endIndex = 0;
            while (startIndex >= 0 && endIndex >= 0)
            {
                startIndex = rtxtConfig.Text.ToLower().IndexOf("<" + keyword.ToLower() + ">", startIndex);
                endIndex = rtxtConfig.Text.ToLower().IndexOf("</" + keyword.ToLower() + ">", endIndex);
                if (startIndex >= 0 && endIndex > startIndex)
                {
                    startIndex += ("<" + keyword + ">").Length;
                    rtxtConfig.Select(startIndex, endIndex - startIndex);
                    rtxtConfig.SelectionBackColor = backColor;
                    endIndex += ("<" + keyword + ">").Length;
                    startIndex++;
                    endIndex++;
                }
            }
        }

        private void parserContentOfXml(List<String> keywords)
        {
            String regAttribute = "(?<Attribute>(\\s*)(?<AttName>(\\D\\S+))=(?<AttValue>\\\"(\\S+)\\\"(\\s*)))";
            Regex rgxAttribute = new Regex(regAttribute);
            MatchCollection matches;
            int startElement = 0;
            int endElement = 0;
            #region paint Color
            while (startElement >= 0 && endElement >= 0)
            {
                startElement = rtxtConfig.Text.IndexOf("<", startElement);
                endElement = rtxtConfig.Text.IndexOf(">", endElement);
                if (startElement >= 0 && endElement >= 0 && endElement > (startElement + 1))
                {
                    String strCurrent = rtxtConfig.Text.Substring(startElement, endElement - startElement + 1).ToLower();
                    #region Comment
                    if (strCurrent.StartsWith("<!--") && strCurrent.EndsWith("-->"))
                    {
                        rtxtConfig.Select(startElement, endElement - startElement + 1);
                        this.rtxtConfig.SelectionColor = Color.Green;
                    }
                    #endregion Comment
                    #region    Element & End Element
                    else
                    {
                        rtxtConfig.Select(startElement, 1);
                        this.rtxtConfig.SelectionColor = Color.DeepSkyBlue;

                        rtxtConfig.Select(startElement + 1, endElement - startElement - 1);
                        rtxtConfig.SelectionColor = Color.Crimson;

                        rtxtConfig.Select(endElement, 1);
                        this.rtxtConfig.SelectionColor = Color.DeepSkyBlue;

                        #region Keyword for highlighting

                        //foreach (String kw in keywords)
                        //{
                        //int indexKeyword = 0;
                        //while (indexKeyword >= 0)
                        //{
                        //    indexKeyword = strCurrent.IndexOf(kw.ToLower(), indexKeyword);
                        //    if (indexKeyword >= 0)
                        //    {
                        //        rtextConfig.Select(startElement + indexKeyword, kw.Length);
                        //        rtextConfig.SelectionBackColor = Color.Yellow;
                        //        indexKeyword++;
                        //    }
                        //}
                        //}

                        #endregion Keyword for highlighting
                        #region Attribute
                        matches = rgxAttribute.Matches(strCurrent);
                        foreach (Match m in matches)
                        {
                            rtxtConfig.Select(startElement + m.Groups["AttName"].Index, m.Groups["AttName"].Length);
                            rtxtConfig.SelectionColor = Color.Blue;

                            rtxtConfig.Select(startElement + m.Groups["AttValue"].Index, m.Groups["AttValue"].Length);
                            rtxtConfig.SelectionColor = Color.Brown;
                        }
                        #endregion Attribute
                    }
                    #endregion    Element & End Element
                    startElement++;
                    endElement++;
                }
            }

            foreach (String kw in keywords)
            {
                searchAndHightlight_Config(kw, Color.Yellow);
            }
            #endregion paint Color

        }

        private void showConfigContent(String filePath)
        {
            rtxtConfig.Clear();
            rtxtConfig.Text = "";
            rtxtConfig.Tag = filePath; // Hind the file path @ component's tag.
            if (filePath.EndsWith(".xml"))
            {
                XElement xeConfig = XElement.Load(filePath);
                rtxtConfig.Text = xeConfig.ToString();
                parserContentOfXml(keywordOfConfigParser);
            }
            else
            {
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(filePath, Encoding.UTF8);
                    bool listNameMatch = false;  //Check is the config list itme name correct ?
                    while (!sr.EndOfStream)
                    {
                        String readLine = sr.ReadLine();
                        foreach (TestCase.NecessaryFile_Info item in tcSelected.NecessaryFileList)
                        {
                            if(item.Category== TestCase.NecessaryFile_Category.Configuration && readLine.Contains(item.Name))
                            {
                                listNameMatch = true;
                                break;
                            }
                        }
                        if (!listNameMatch)
                        {
                            rtxtConfig.SelectionColor = Color.Red;
                        }
                        rtxtConfig.AppendText(readLine + "\n");
                        if (!listNameMatch)
                        {
                            rtxtConfig.SelectionColor = Color.Black;
                        }
                    }
                }
                catch
                {
                }
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

    }
}
