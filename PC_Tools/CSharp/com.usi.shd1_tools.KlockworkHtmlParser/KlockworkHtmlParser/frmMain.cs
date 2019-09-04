using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Aspose.Cells;
using HtmlAgilityPack;
using System.Xml.XPath;
using System.Collections;
using Aspose.Cells;

namespace com.usi.shd1_tools.KlockworkHtmlParser
{
    public partial class frmMain : Form
    {
        public string Title
        {
            get
            {
                object[] attributes = Assembly.GetCallingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title.Length > 0) return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        public Version Version
        {
            get { return Assembly.GetCallingAssembly().GetName().Version; }
        }
        private List<KlockworkHtmlProcessor> lstHtmlProcessors;
        private KlockworkHtmlProcessor currentProcessor;
        private String htmlDirectory = "";
        //private String[] htmlFiles;
        private int currentSelectedIndex = -1;
        private const string ACTION_UpdateParsingProgress = "UpdateParsingProgress";
        KeywordReader keyReader;
        List<KeywordCollection> currentKeywordConfigurations = null;
        public frmMain()
        {
            InitializeComponent();
            this.Text = Title + " v" + Version.ToString(3);
            lstHtmlProcessors = new List<KlockworkHtmlProcessor>();
            keyReader = new KeywordReader(System.AppDomain.CurrentDomain.BaseDirectory+"\\Keywords.xml");
        }

        private void parseHtml(KlockworkHtmlProcessor processor)
        {
            processor.UpdateProcessingProgessEventHandler += new EventHandler<UpdateProcessingProgessEventArgs>(KlockworkLogParsingProgressUpdateEventHander);
            processor.Start(keyReader.KeywordCollections);
            processor.UpdateProcessingProgessEventHandler -= new EventHandler<UpdateProcessingProgessEventArgs>(KlockworkLogParsingProgressUpdateEventHander);
            btnExport.Enabled = processor.AllParsedMessagesList.Count > 0;
        }

        int currentRow = 1;

        private void exportParsedResult(String savePath)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Workbook workBook = new Workbook();
                try
                {
                    workBook.Worksheets.RemoveAt(0);
                }
                catch
                {
                }
                foreach (KeyValuePair<String, List<KlockworkParsedMessage>> parsedMessageCollection in currentProcessor.AllParsedMessagesList)
                {
                    Worksheet workSheet = workBook.Worksheets.Add(parsedMessageCollection.Key+" ("+parsedMessageCollection.Value.Count+")");
                    currentRow = 0;
                    foreach (KlockworkParsedMessage msg in parsedMessageCollection.Value)
                    {
                        workSheet.Cells[currentRow, 0].PutValue("#" + msg.Index);
                        workSheet.Cells.Merge(currentRow, 0, 3, 1);
                        workSheet.Cells[currentRow, 1].PutValue(msg.Header);
                        currentRow++;
                        workSheet.Cells[currentRow, 1].PutValue(msg.PathAndLocation);
                        currentRow++;
                        workSheet.Cells[currentRow, 1].PutValue(msg.ErrorDescription);
                        currentRow++;
                    }
                }
                workBook.Save(savePath);
                MessageBox.Show("Done!!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Export parsing result exception, message = " + ex.Message, "Export file Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.Cursor = Cursors.Default;
        }
        
        #region UI Control
        private void btnParseAll_Click(object sender, EventArgs e)
        {
            try
            {
                    this.Cursor = Cursors.WaitCursor;
                    foreach (KlockworkHtmlProcessor processor in lstHtmlProcessors)
                    {
                        parseHtml(processor);         
                    }
                    showParsedResult(lstHtmlProcessors[cmbHtmlFiles.SelectedIndex]);
                    this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Parse Html exception, message = :" + ex.Message);
            }
        }

        private void btnParseOne_Click(object sender, EventArgs e)
        {
            try
            {
                //KlockworkHtmlProcessor currentProcess = lstHtmlProcessors[cmbHtmlFiles.SelectedIndex];
                this.Cursor = Cursors.WaitCursor;
                parseHtml(currentProcessor);
                showParsedResult(currentProcessor);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Parse Html exception, message = :" + ex.Message);
            }
        }
        
        private void btnHtmlFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                lstHtmlProcessors.Clear();
                uiDataClear();
                htmlDirectory = folderBrowserDialog1.SelectedPath+"\\";
                String []htmlFiles = Directory.GetFiles(htmlDirectory, "*.htm", SearchOption.AllDirectories);
                if (htmlFiles.Length > 0)
                {
                    showHtmlFileListAndPreparingHtmlProcessor(htmlFiles);
                    btnParse.Enabled = true;
                    btnParseOne.Enabled = true;
                }
                else
                {
                    btnParse.Enabled = false;
                    btnParseOne.Enabled = false;
                }
            }
        }
        
        private void showHtmlFileListAndPreparingHtmlProcessor(String[] htmlFiles)
        {
            txtHtmlDirectory.Text = htmlDirectory;
            foreach (String file in htmlFiles)
            {
                cmbHtmlFiles.Items.Add(file.Remove(0,htmlDirectory.Length));
                KlockworkHtmlProcessor htmlProcessor = new KlockworkHtmlProcessor(file);
                lstHtmlProcessors.Add(htmlProcessor);
            }
            if (cmbHtmlFiles.Items.Count > 0)
            {
                cmbHtmlFiles.SelectedIndex = 0;
            }
        }

        private void showParsedResult(KlockworkHtmlProcessor currentProcessor)
        {
            if (currentProcessor != null)
            {
                tabCtlMain.TabPages.Clear();
                foreach (KeyValuePair<String, List<KlockworkParsedMessage>> keyval in currentProcessor.AllParsedMessagesList)
                {
                    KlockworkParsedResultTabPage newTabPage = new KlockworkParsedResultTabPage(keyval);
                    tabCtlMain.TabPages.Add(newTabPage);                    
                }
                tabCtlMain.Show();
                currentProcessor.UpdateProcessingProgessEventHandler -= new EventHandler<UpdateProcessingProgessEventArgs>(KlockworkLogParsingProgressUpdateEventHander);
            }
        }

        private void uiDataClear()
        {
            cmbHtmlFiles.Items.Clear();
            tabCtlMain.TabPages.Clear();
            btnExport.Enabled = false;
        }

        private void cmbHtmlFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbHtmlFiles.SelectedIndex;
            if (index >= 0 && currentSelectedIndex != index && lstHtmlProcessors.Count>index)
            {
                this.Cursor = Cursors.WaitCursor;
                currentSelectedIndex = index;
                currentProcessor = lstHtmlProcessors[index];
                showParsedResult(lstHtmlProcessors[index]);
                btnExport.Enabled = currentProcessor.AllParsedMessagesList.Count > 0;
                this.Cursor = Cursors.Default;
            }
        }
        
        private delegate void delVoidAnyParams(String action, params object[] values);
        private void generalUiInvoke(String action, params object[] values)
        {
            if (this.InvokeRequired)
            {
                delVoidAnyParams del = new delVoidAnyParams(generalUiInvoke);
                this.Invoke(del, action, values);
            }
            else
            {
                switch (action)
                {
                    case ACTION_UpdateParsingProgress:
                        if (values.Length >= 3)
                        {
                            int max = (int)values[0];
                            int progress = (int)values[1];
                            if (progress > 0 && progress < max)
                            {
                                progressBar1.Visible = true;
                                lblProgress.Visible = true;
                                String processingItem = ((String)values[2]).Replace(htmlDirectory,"");
                                lblProgress.Text = progress + "/" + max + " - (" + processingItem + ")";
                                progressBar1.Maximum = max;
                                progressBar1.Value = progress;
                                Application.DoEvents();
                            }
                            else
                            {
                                progressBar1.Visible = false;
                                lblProgress.Visible = false;
                            }
                        }
                        break;
                }
            }
        }

        private void KlockworkLogParsingProgressUpdateEventHander(object sender, UpdateProcessingProgessEventArgs ea)
        {
            updateParsingProgress(ea.ProcessingItme, ea.Progress, ea.Maximum);
        }
        
        private void updateParsingProgress(String processingItem, int progress, int max)
        {
            generalUiInvoke(ACTION_UpdateParsingProgress, max, progress, processingItem);
        }
        
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                exportParsedResult(saveFileDialog1.FileName);
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (currentKeywordConfigurations == null)
            {
                currentKeywordConfigurations = keyReader.KeywordCollections;
            }
            if (frmKeywordConfiguration.Open(currentKeywordConfigurations).Equals(DialogResult.OK))
            {
                currentKeywordConfigurations = keyReader.KeywordCollections;
                //saveKeywordConfigurations(currentKeywordConfigurations);               
            }
        }

        #endregion UI Control
    }
}
