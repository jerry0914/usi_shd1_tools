using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Aspose.Cells;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using ExtensionMethods;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace com.usi.shd1_tools.TestGuide
{
    public partial class FormInstall : UserControl
    {
        private FormMain frmMain = null;
        private List<TestCase> lstTestCase_All = new List<TestCase>();
        private String testPlanDefaultFolder = "";

        private List<TestCase> lstTestCase_Filtered = new List<TestCase>();
        private TestCase tcSelected;
        private const int testCase_StartRow = 8;  //First test case start row
        private const int testCaseName_Column = 0;  //TestCaseName Colume
        private const int testCase_Group1_Column = 1; // Group1
        private const int descriptionColumn = 4;//descriptcion Column
        private const int TPSDataColumn = 5;//TestCase TPS Data Column
        private const int TestResultColumn = 6;//TestCase Test Result Column
        private const int ResultCommentColumn = 10;//Result comment Column
        private List<String> keywordOfExpectedResult = new List<string>(new String[] { "max error ", " high pass & low fail", "accuracy" });
        //private bool isConfigEdited = false;
        //private String lastFilteredKeyword = "";  //Remember the last keyword of filter to avoid  redundancy searching.
        private bool selectedTCChanging = false;  // Flag to filter incorrectly action when test case selecting for test result editor.
        TestCase.EnumTestResult tempEditedResult = TestCase.EnumTestResult.None; // Temporarily variable to save edited test result. 
        private bool defaultSettingsLoading = false; //Flag of default settins loading to avoid loading action to trigger the defaultSettingsChange event.
        
        //private String profilePath = "";
        private bool installing = false;
        private bool isConfigFileReady = false;
        private bool installCheck_Flag = true;
        private Thread tdInstallCheck = null;
        private String lastKeywordFilter = "";  //Record the last filter keyword to avoid the non-necessary filtering operation
        private int lastResultFilter = -1;//Record the last result filter value to avoid the non-necessary filtering operation
        private AutoCompleteStringCollection autoCompleteCollectionResultComment = null;

        public FormInstall(FormMain mainForm)
        {
            frmMain = mainForm;
            frmMain.ToolSettingsChangedEventHandler += new EventHandler(toolSettings_Changed);
            frmMain.SelectedPlatformChangedEventHandler += new EventHandler(selectedPlatformChanged);
            autoCompleteCollectionResultComment = new AutoCompleteStringCollection();
            defaultSettingsLoading = true;
            InitializeComponent();
            tslStatusLabel.Text = "";
            loadTestplanFolder(frmMain.Platform);
            defaultSettingsLoading = false;
            startInstallCheck();
            btnAttachmentsPage_Changed(btnConfigurations, new EventArgs());
        }

        private bool loadTestCaseFromTestPlan(String testPlanFile)
        {
            bool result = true;
            Workbook workBook;
            Worksheet workSheet;
            lstTestCase_All.Clear();
            lsvConfigList.Items.Clear();
            autoCompleteCollectionResultComment.Clear();
            try
            {
                workBook = new Workbook(testPlanFile);
                workSheet = workBook.Worksheets["EnterResults"];
                if (workSheet == null)
                {
                    workSheet = workBook.Worksheets[0];
                }
                int tcCount = 0;
                try
                {
                    for (int rowIndex = testCase_StartRow; rowIndex < workSheet.Cells.Rows.Count; rowIndex++)
                    {
                        Row row = workSheet.Cells.Rows[rowIndex];
                        if (row.LastCell.Column >= TPSDataColumn)
                        {
                            TestCase tcNew = new TestCase();
                            tcNew.RowIndex = rowIndex;
                            #region getTestCaseName
                            Cell cellTemp;
                            cellTemp = row.GetCellOrNull(testCaseName_Column);
                            if (cellTemp != null && cellTemp.Value != null)
                            {
                                tcNew.Name = cellTemp.Value.ToString();
                            }
                            #endregion getTestCaseName
                            #region  Description
                            cellTemp = row.GetCellOrNull(descriptionColumn);
                            if (cellTemp != null && cellTemp.Value != null)
                            {
                                tcNew.Description = cellTemp.Value.ToString();
                            }
                            #endregion Description
                            #region get Test Case TPSData, TestCase ID, and Config file list ...
                            cellTemp = row.GetCellOrNull(TPSDataColumn);
                            if (cellTemp != null && cellTemp.Value != null)
                            {
                                tcNew.ID = getTestCaseID(cellTemp.Value.ToString());
                                tcNew.TPSData = cellTemp.Value.ToString();
                                ParametersOfTestCaseConfiguation param = getTestCaseNecessaryFileList(tcNew.TPSData);
                                tcNew.NecessaryFileList = param.NecessaryFileList;
                                 tcNew.ConfigAssociatedKeyword = param.AssociatedKeyword.Length > 0 ? param.AssociatedKeyword : tcNew.ID;// If keyword does not found, set the ID as keyword.
                            }
                            #endregion get Test Case TPSData, TestCase ID, and Config file list ...
                            #region Group1 of Test Case
                            cellTemp = row.GetCellOrNull(testCase_Group1_Column);
                            if (cellTemp != null && cellTemp.Value != null)
                            {
                                tcNew.Cycle_Group1 = cellTemp.Value.ToString();
                            }
                            #endregion Group1 of Test Case
                            #region get Test Result
                            cellTemp = row.GetCellOrNull(TestResultColumn);
                            if (cellTemp != null && cellTemp.Value != null && cellTemp.Value.ToString().Length > 0)
                            {
                                switch (cellTemp.Value.ToString().ToUpper())
                                {
                                    case "P":
                                        tcNew.TestResult = TestCase.EnumTestResult.P;
                                        break;
                                    case "I":
                                        tcNew.TestResult = TestCase.EnumTestResult.I;
                                        break;
                                    case "B":
                                        tcNew.TestResult = TestCase.EnumTestResult.B;
                                        break;
                                    case "F":
                                        tcNew.TestResult = TestCase.EnumTestResult.F;
                                        break;
                                    default:
                                        tcNew.TestResult = TestCase.EnumTestResult.None;
                                        break;
                                }
                            }
                            #endregion get Test Result
                            #region Result Comment
                            cellTemp = row.GetCellOrNull(ResultCommentColumn);
                            if (cellTemp != null && cellTemp.Value != null && cellTemp.Value.ToString().Length > 0)
                            {
                                tcNew.ResultComment = cellTemp.Value.ToString();
                                if (!autoCompleteCollectionResultComment.Contains(cellTemp.Value.ToString()))
                                {
                                    autoCompleteCollectionResultComment.Add(cellTemp.Value.ToString());
                                }
                            }
                            #endregion Result Comment
                            lstTestCase_All.Add(tcNew);
                            tcCount++;
                        }
                    }
                    refreshAutoCompleteResultComment();
                }
                catch (Exception ex)
                {
                    result = false;
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                workBook = null;

            }
            return result;
        }

        private void filterTestCase(String keyword, int testResultFilter)
        {
            //if (lastFilteredKeyword != keyword && lstTestCase_All.Count>0)
            if (lstTestCase_All.Count > 0)
            {
                lsvConfigList.Items.Clear();
                if (lstTestCase_Filtered == null)
                {
                    lstTestCase_Filtered = new List<TestCase>();
                }
                else
                {
                    lstTestCase_Filtered.Clear();
                }
                foreach (TestCase tc in lstTestCase_All)
                {
                    if ((tc.Name.Contains(keyword) || keyword.Trim().Length == 0 || !FormToolSettings.KeywordFilterEnable) && ((int)tc.TestResult & testResultFilter) > 0)
                    {
                        lstTestCase_Filtered.Add(tc);
                    }
                }
                //lastFilteredKeyword = keyword;
                showFilteredTestCase();
            }
        }

        private void showFilteredTestCase()
        {
            cmbFilteredTestCases.Items.Clear();
            foreach (TestCase tc in lstTestCase_Filtered)
            {
                cmbFilteredTestCases.Items.Add(tc.Name ); //+ "-  【"+tc.Cycle_Group1+"】");
            }
            cmbFilteredTestCases.Show();
            if (cmbFilteredTestCases.Items.Count > 0)
            {
                cmbFilteredTestCases.SelectedIndex = 0;
            }
        }

        private String getTestCaseID(String TPS_description)
        {
            String result = "";
            try
            {
                Regex rgxID = new Regex(@"^((T|t)(E|e)(S|s)(T|t) ID)((\s+)|):((\s+)|)(?<ID>VT(\d+)-(\d+))");
                Match m;
                foreach (String line in TPS_description.Replace("\r", "").Split('\n'))
                {
                    m = rgxID.Match(line);
                    if (m.Success)
                    {
                        result = m.Groups["ID"].Value;
                        break;
                    }
                }
            }
            catch
            {
                result = "";
            }
            return result;
        }

        private ParametersOfTestCaseConfiguation getTestCaseNecessaryFileList(String TPS_description)
        {
            ParametersOfTestCaseConfiguation param = new ParametersOfTestCaseConfiguation();
            List<TestCase.NecessaryFile_Info> fileList = new List<TestCase.NecessaryFile_Info>();
            Regex rgx = null;
            Regex rgxAttachment = null;
            Match m;
            bool inAppArea = false;
            foreach (String line in TPS_description.Replace("\r", "").Split(new char[] { '\n', ',' }))
            {
                String tempLine = line.Trim();
                if (tempLine.Length > 0)
                {
                    try
                    {
                        #region Application List
                        #region In the Test Application Tag
                        Regex rgxTag = new Regex(@"(?<Tag>((\S+\s)*|)\S+)  :");
                        Match mT = rgxTag.Match(tempLine);
                        if (mT.Success)
                        {
                            switch (mT.Groups["Tag"].Value.Trim())
                            {
                                case "Test Application":
                                    inAppArea = true;
                                    break;
                                default:
                                    inAppArea = false;
                                    break;
                            }
                        }
                        if (inAppArea)
                        {
                            if (mT.Groups[0].Value.Length > 0)
                            {
                                tempLine = line.Replace(mT.Groups[0].Value, "").Trim();
                            }
                            if (tempLine.Length > 0)
                            {
                                bool existed = false;
                                foreach (TestCase.NecessaryFile_Info fileInfo in fileList)
                                {
                                    if (tempLine.Equals(fileInfo.Name, StringComparison.OrdinalIgnoreCase))
                                    {
                                        existed = true;
                                        break;
                                    }
                                }
                                if (!existed)
                                {
                                    TestCase.NecessaryFile_Info fileInfo = new TestCase.NecessaryFile_Info();
                                    fileInfo.Name = tempLine;
                                    rgxAttachment = new Regex(@"(?<Pictures>\S+\.((?i:jpg)|(?i:bmp)|(?i:png)|(?i:gif)))|(?<Audios>\S+\.((?i:mp3)|(?i:wma)|(?i:ogg)|(?i:mid)))|(?<Videos>\S+\.((?i:wmv)|(?i:mp4)|(?i:avi)|(?i:wav)))|(?<Document>\S+\.((?i:doc)|(?i:docx)|(?i:txt)))");
                                    m = rgxAttachment.Match(tempLine);
                                    if (m.Success)
                                    {
                                        fileInfo.Category = TestCase.NecessaryFile_Category.Attachment;
                                        fileList.Add(fileInfo);
                                    }
                                    else
                                    {
                                        rgx = new Regex(@"(?<XMLFile>\S+\.(?i:xml))");
                                        if (rgx.Match(tempLine).Success)
                                        {
                                            //Ignore the xml files
                                        }
                                        else
                                        {
                                            fileInfo.Category = TestCase.NecessaryFile_Category.Application;
                                            fileList.Add(fileInfo);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion In the Test Application Tag
                        #region Application name match to the Regular Expression
                        if (frmMain.Platform.ToLower().Equals("ce"))
                        {
                            rgx = new Regex(@"(?<Application>[a-zA-Z]\S*\.(?i:exe))");
                        }
                        else if (frmMain.Platform.ToLower().Equals("android"))
                        {
                            rgx = new Regex(@"(?<Application>[a-zA-Z]\S*\.(?i:apk))");
                        }
                        MatchCollection mc = rgx.Matches(tempLine);
                        if (mc.Count > 0)
                        {
                            foreach (Match match in mc)
                            {
                                String appTemp = match.Groups["Application"].Value.Trim();
                                bool existed = false;
                                foreach (TestCase.NecessaryFile_Info fileInfo in fileList)
                                {
                                    if (appTemp.Equals(fileInfo.Name, StringComparison.OrdinalIgnoreCase))
                                    {
                                        existed = true;
                                        break;
                                    }
                                }
                                if (!existed)
                                {
                                    TestCase.NecessaryFile_Info fileInfo = new TestCase.NecessaryFile_Info();
                                    fileInfo.Name = appTemp;
                                    fileInfo.Category = TestCase.NecessaryFile_Category.Application;
                                    fileList.Add(fileInfo);
                                }
                            }
                        }
                        #endregion Application name match to the Regular Expression
                        #endregion Application List
                        #region Config List
                        rgx = new Regex(@"(?<Config>(?<Keyword>(?i:configlist))(\S|\s)*)");
                        m = rgx.Match(tempLine);
                        if (m.Success)
                        {
                            continue;
                            //Ignore the "ConfigList(.xml)" description in the TDS Data.
                        }
                        rgx = new Regex(@"(?<Config>(?<Keyword>VT(\d+-)*(\d+))\S*\.(?i:xml))");
                        m = rgx.Match(tempLine);
                        if (!m.Success)
                        {
                            rgx = new Regex(@"(?<Config>(?<Keyword>[a-zA-Z]\S*)\.(?i:xml))");
                            m = rgx.Match(tempLine);
                        }
                        if (m.Success)
                        {
                            //TestCase.NecessaryFile_Info fileInfo = new TestCase.NecessaryFile_Info();
                            param.AssociatedKeyword = m.Groups["Keyword"].Value;
                            String fileName = m.Groups["Config"].Value.Trim();
                            bool existed = false;
                            foreach (TestCase.NecessaryFile_Info item in fileList)
                            {
                                if (fileName.Equals(item.Name, StringComparison.OrdinalIgnoreCase))
                                {
                                    existed = true;
                                    break;
                                }
                            }
                            if (!existed)
                            {
                                TestCase.NecessaryFile_Info fileInfo = new TestCase.NecessaryFile_Info();
                                fileInfo.Name = fileName;
                                fileInfo.Category = TestCase.NecessaryFile_Category.Configuration;
                                fileList.Add(fileInfo);
                            }
                        }
                        #endregion Config List
                        #region Attachment List
                        rgxAttachment = new Regex(@"(?<Pictures>\S+\.((?i:jpg)|(?i:bmp)|(?i:png)|(?i:gif)))|(?<Audios>\S+\.((?i:mp3)|(?i:wma)|(?i:ogg)|(?i:mid)))|(?<Videos>\S+\.((?i:wmv)|(?i:mp4)|(?i:avi)|(?i:wav)))|(?<Document>\S+\.((?i:doc)|(?i:docx)|(?i:txt)))");
                        mc = rgxAttachment.Matches(tempLine);
                        foreach (Match match in mc)
                        {
                            String attachName = match.Groups[0].Value.Trim();
                            bool existed = false;
                            foreach (TestCase.NecessaryFile_Info fileInfo in fileList)
                            {
                                if (attachName.Equals(fileInfo.Name, StringComparison.OrdinalIgnoreCase))
                                {
                                    existed = true;
                                    break;
                                }
                            }
                            if (!existed)
                            {
                                TestCase.NecessaryFile_Info fileInfo = new TestCase.NecessaryFile_Info();
                                fileInfo.Name = attachName;
                                fileInfo.Category = TestCase.NecessaryFile_Category.Attachment;
                                fileList.Add(fileInfo);
                            }
                        }
                        #endregion Attachment List
                    }
                    catch (Exception ex)
                    {
                        //Ignore the error one, process next.
                    }
                }
            }
            param.NecessaryFileList = fileList;
            return param;
        }

        private void showTestCaseInformation(TestCase tcSelected)
        {
            lsvConfigList.Items.Clear();
            lsvApplication.Items.Clear();
            lsvAttachment.Items.Clear();
            rtxtDescription.Clear();
            rtxtTPSData.Clear();
            rtxtExpectedResult.Clear();
            txtResultComment.Clear();
            rdbB.Checked = rdbF.Checked = rdbI.Checked = rdbP.Checked = false;

            foreach (TestCase.NecessaryFile_Info file in tcSelected.NecessaryFileList)
            {
                #region list the configurations
                if (file.Category == TestCase.NecessaryFile_Category.Configuration)
                {
                    ListViewItem li = new ListViewItem(file.Name);
                    if (File.Exists(FormToolSettings.TRS_ConfigSourceFolder + "\\" + tcSelected.ConfigFloderName + "\\" + file.Name))
                    {
                        li.Tag = file.Name;
                        li.ForeColor = Color.Black;
                    }
                    else
                    {
                        li.ForeColor = Color.Red;
                    }
                    li.Checked = true;
                    lsvConfigList.Items.Add(li);
                }
                #endregion list the configurations
                #region list the applications
                else if (file.Category == TestCase.NecessaryFile_Category.Application)
                {
                    if (file.Name.ToLower().StartsWith("profile"))
                    {
                        if (!frmMain.Platform.ToLower().Equals("ce") || !file.Name.ToLower().Contains("wm"))  //filter the ProfileWM.exe
                        {
                            if (FormToolSettings.ProfileApplication_Path.Length > 0 && File.Exists(FormToolSettings.ProfileApplication_Path))
                            {
                                addApplication(FormToolSettings.ProfileApplication_Path);
                            }
                            else
                            {
                                addApplication(file.Name);
                            }
                        }
                    }
                    else
                    {
                        addApplication(file.Name);
                    }
                }
                #endregion list the applications
                #region list the attachements
                else if (file.Category == TestCase.NecessaryFile_Category.Attachment)
                {
                    addAttachment(file.Name);
                }
                 #endregion list the attachements
            }
            if (lsvConfigList.Items.Count > 0)
            {
                if (File.Exists(FormToolSettings.TRS_ConfigSourceFolder + "\\" + tcSelected.ConfigFloderName + "\\configlist.txt"))
                {
                    ListViewItem  liconfig = new ListViewItem("configlist.txt");
                    liconfig.Tag = FormToolSettings.TRS_ConfigSourceFolder + "\\" + tcSelected.ConfigFloderName + "\\configlist.txt";
                    lsvConfigList.Items.Add(liconfig);
                }
                ListViewItem li = new ListViewItem(" ****** Double-click the file name to edit it... ******");
                li.ForeColor = System.Drawing.Color.Green;
                lsvConfigList.Items.Add(li);
            }
            rtxtDescription.Text = tcSelected.Description;
            #region Highlight text of executing duration.
            Regex rgx1 = new Regex(@"(?<duration>\d+(\s*)(h|H)(r|R)(\s*))");
            MatchCollection mc1 = rgx1.Matches(tcSelected.Description);
            foreach (Match m in mc1)
            {
                rtxtDescription.Select(m.Index, m.Length);
                rtxtDescription.SelectionBackColor = Color.Orange;
            }
            #endregion Highlight text of executing duration.

            #region Splited TPS Data and expected result
            Regex rgxTDS = new Regex(@"(?<Tag>((\S+\s)*|)\S+)  :");
            MatchCollection mc = rgxTDS.Matches(tcSelected.TPSData);
            if (mc.Count > 0)
            {
                for (int index = 0; index < mc.Count; index++)
                {
                    Match mCurrent = mc[index];
                    int msgStart = mCurrent.Index + mCurrent.Length + 1;
                    int msgEnd = index < mc.Count - 1
                                            ? mc[index + 1].Index - 1
                                            : tcSelected.TPSData.Length - 1;
                    switch (mCurrent.Groups["Tag"].Value.Trim())
                    {
                        //case "Test Application":
                        case "Product Requirement ID":
                        case "Developers Notes":
                        case "Developer Notes":
                            //Message to discard
                            break;
                        case "Expected Results":
                            //Message to Expected result area
                            rtxtExpectedResult.SelectionBackColor = Color.DarkOliveGreen;
                            rtxtExpectedResult.SelectionColor = Color.White;
                            rtxtExpectedResult.AppendText(mCurrent.Groups["Tag"].Value + "  :\r\n");
                            rtxtExpectedResult.SelectionBackColor = Color.White;
                            rtxtExpectedResult.SelectionColor = Color.Black;
                            rtxtExpectedResult.AppendText(tcSelected.TPSData.Substring(msgStart, msgEnd - msgStart));
                            break;
                        default:
                            //Message to Produce area
                            rtxtTPSData.SelectionBackColor = Color.Black;
                            rtxtTPSData.SelectionColor = Color.White;
                            rtxtTPSData.AppendText(mCurrent.Groups["Tag"].Value + "  :\r\n");
                            rtxtTPSData.SelectionBackColor = Color.White;
                            rtxtTPSData.SelectionColor = Color.Black;
                            rtxtTPSData.AppendText(tcSelected.TPSData.Substring(msgStart, msgEnd - msgStart));
                            break;
                    }
                }
            }
            #endregion  Splited TPS Data and expected result

            #region TestResult
            switch (tcSelected.TestResult)
            {
                case TestCase.EnumTestResult.P:
                    rdbP.Checked = true;
                    break;
                case TestCase.EnumTestResult.I:
                    rdbI.Checked = true;
                    break;
                case TestCase.EnumTestResult.F:
                    rdbF.Checked = true;
                    break;
                case TestCase.EnumTestResult.B:
                    rdbB.Checked = true;
                    break;
                default:
                    break;
            }
            #endregion TestResult

            #region Result Comment
            txtResultComment.Text = tcSelected.ResultComment;
            #endregion Result Comment

            //rtxtTPSData.Text = tcSelected.TPSData;
            searchAndHightlight_TPSData();
            searchAndHightlight_ExpectedResult();
            isConfigFileReady = lsvConfigList.Items.Count > 0;
            //lsvConfigList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <param name="keyword"></param>
        /// <returns> Name of found config Folder</returns>
        private String getReleateFolderName(String sourceFolder, String keyword)
        {
            String folderName = "";
            if (keyword != null && keyword.Length > 0 && Directory.Exists(sourceFolder))
            {
                foreach (String subDir in Directory.GetDirectories(sourceFolder))
                {
                    if (subDir.Contains(keyword))
                    {
                        folderName = Path.GetFileName(subDir);
                        //clone(subDir, tempFolder);
                        break;
                    }
                }
            }
            return folderName;
        }

        private void clone(String sourcePath, String destinationDir)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(sourcePath);
                if ((attr & FileAttributes.Directory).Equals(FileAttributes.Directory))   //sourcePath is a directory
                {
                    destinationDir = Path.Combine(destinationDir, Path.GetFileName(sourcePath));
                    if (!Directory.Exists(destinationDir))
                    {
                        Directory.CreateDirectory(destinationDir);
                    }
                    foreach (String directory in Directory.GetDirectories(sourcePath))
                    {
                        clone(directory, Path.Combine(destinationDir, Path.GetFileName(directory)));
                    }
                    foreach (String file in Directory.GetFiles(sourcePath))
                    {
                        String strDestFilePath = "";
                        try
                        {
                            String fileName = Path.GetFileName(file);
                            strDestFilePath = Path.Combine(destinationDir, fileName);
                            File.Copy(file, strDestFilePath, true);
                        }
                        catch
                        {
                            MessageBox.Show("[Clone]-exception, copy \"" + file + "\" to " + strDestFilePath + "\"");
                        }
                    }
                }
                else //sourcePath is a file
                {
                    //String destinaionDir = destinationDir.Replace(Path.GetFileName(destinationDir), "");
                    if (!Directory.Exists(destinationDir))
                    {
                        Directory.CreateDirectory(destinationDir);
                    }
                    String strDestFilePath = "";
                    try
                    {
                        String fileName = Path.GetFileName(sourcePath);
                        strDestFilePath = Path.Combine(destinationDir, fileName);
                        File.Copy(sourcePath, strDestFilePath, true);
                    }
                    catch
                    {
                        MessageBox.Show("[Clone]-exception, copy \"" + sourcePath + "\" to " + strDestFilePath + "\"");
                    }
                    File.Copy(sourcePath, strDestFilePath, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[Clone]-exception, message = \r\n" + ex.Message);
            }
        }

        private bool saveFile(String filePath, String content)
        {
            return saveFile(filePath, content, true);
        }

        private bool saveFile(String filePath, String content, bool overWriteNotify)
        {
            bool result = false;
            DialogResult dr = DialogResult.Yes;
            try
            {
                if (File.Exists(filePath) && overWriteNotify)
                {
                    dr = MessageBox.Show("Do you really want to overwrite the file : \n \"" + filePath + "\" ?",
                                                        "Overwrite",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question);
                }
                if (dr.Equals(DialogResult.Yes))
                {
                    using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                    {
                        try
                        {
                            sw.Write(content);
                            result = true;
                        }
                        catch
                        {
                        }
                        finally
                        {
                            if (sw != null)
                            {
                                sw.Close();
                            }
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        private void searchAndHightlight_TPSData()
        {
            Regex[] rgxs = new Regex[]{
                                                              new Regex(@"(?<server7>(?<heightlight>(?i:server)(\s*)7(\.exe|)))") ,  //Regular expression of Server7
                                                              new Regex(@"(?<ESSID>(?i:ESSID)(\s*)(=)(\s*)(?<heightlight>(\S+)))"),  //Regular expression of ESSID
                                                              new Regex (@"(?<WifiBand>(?<heightlight>\d(\.\d+|)(\s*)(?i:ghz)))"),//Regular expression of Wifi frequency band
                                                              new Regex(@"(?<Pictures>(?<heightlight>\S+\.((?i:jpg)|(?i:bmp)|(?i:png)|(?i:gif))))"),
                                                              new Regex(@"(?<Audios>(?<heightlight>\S+\.((?i:mp3)|(?i:wma)|(?i:ogg))))"),
                                                              new Regex(@"(?<Videos>(?<heightlight>\S+\.((?i:wmv)|(?i:mp4)|(?i:avi)|(?i:wav))))")
                                                              //new Regex(@"(?<heightlight>(?i:pingstress))")  //Regular expression of PingStress
                                                            };
            for (int index = 0; index < rgxs.Length; index++)
            {
                MatchCollection mc2 = rgxs[index].Matches(rtxtTPSData.Text);
                foreach (Match m in mc2)
                {
                    rtxtTPSData.Select(m.Groups["heightlight"].Index, m.Groups["heightlight"].Length);
                    //rtxtTPSData.SelectionBackColor = colorOfrgx[index];
                    rtxtTPSData.SelectionBackColor = Color.Lime;
                }
            }
        }

        private void searchAndHightlight_ExpectedResult()
        {
            foreach (String keyword in keywordOfExpectedResult)
            {
                Regex regKeyword = new Regex("(?<heightlight>(?i:" + keyword + "))");
                Match m = regKeyword.Match(rtxtExpectedResult.Text);
                if (m.Success)
                {
                    rtxtExpectedResult.Select(m.Groups["heightlight"].Index, m.Groups["heightlight"].Length);
                    rtxtExpectedResult.SelectionBackColor = Color.Yellow;
                }
            }
        }

        private void loadTestplanFolder(String platform)
        {
            defaultSettingsLoading = true;
            XElement xeDefaultSettings = null, xeInstall = null;
            try
            {
                xeDefaultSettings = XElement.Load(frmMain.DefaultSettingsPath);
                xeInstall = xeDefaultSettings.Element("Install");
                XElement xePlatform = xeInstall.ElementByAttribute("Platform", "name", platform);
                if (xePlatform != null)
                {
                    //TestPlan default folder
                    testPlanDefaultFolder = xePlatform.Element("TestPlanFolder").Value;
                }
            }
            catch
            {
                //Fail to load default settings, do nothing.
            }
            defaultSettingsLoading = false;
        }

        private void saveTestPlanFolder()
        {
            XElement xeDefaultSettings = null, xeInstall = null;
            if (File.Exists(frmMain.DefaultSettingsPath))
            {
                try
                {
                    xeDefaultSettings = XElement.Load(frmMain.DefaultSettingsPath);
                }
                catch
                {
                }
            }
            if (xeDefaultSettings == null)
            {
                xeDefaultSettings = new XElement("DefaultSettings");
            }
            try
            {
                xeInstall = xeDefaultSettings.Element("Install");
            }
            catch
            {
            }
            if (xeInstall == null)
            {
                xeInstall = new XElement("Install");
                xeDefaultSettings.Add(xeInstall);
            }
            XElement xePlatform = xeInstall.ElementByAttribute("Platform", "name", frmMain.Platform);
            if (xePlatform == null)
            {
                xePlatform=new XElement("Platform");
                xePlatform.SetAttributeValue("name", frmMain.Platform);
            }
            XElement xeTestPlanFolder = xePlatform.Element("TestPlanFolder");
            if (xeTestPlanFolder == null)
            {
                xeTestPlanFolder = new XElement("TestPlanFolder");
            }
            xeTestPlanFolder.Value = txtTRSPath.Text;
            xeDefaultSettings.Save(frmMain.DefaultSettingsPath);
        }

        private void saveQuickEditorItems()
        {
            XElement xeDefaultSettings = null, xeInstall = null;
            if (File.Exists(frmMain.DefaultSettingsPath))
            {
                try
                {
                    xeDefaultSettings = XElement.Load(frmMain.DefaultSettingsPath);
                }
                catch
                {
                }
            }
            if (xeDefaultSettings == null)
            {
                xeDefaultSettings = new XElement("DefaultSettings");
            }
            try
            {
                xeInstall = xeDefaultSettings.Element("Install");
            }
            catch
            {
            }
            if (xeInstall == null)
            {
                xeInstall = new XElement("Install");
                xeDefaultSettings.Add(xeInstall);
            }
            XElement xePlatform = xeInstall.ElementByAttribute("Platform", "name", frmMain.Platform);
            if (xePlatform == null)
            {
                xePlatform = new XElement("Platform");
                xePlatform.SetAttributeValue("name", frmMain.Platform);
            }
            XElement xeQuickEditor = xePlatform.Element("QuickEditor");
            if (xeQuickEditor == null)
            {
                xeQuickEditor = new XElement("QuickEditor");
                xePlatform.Add(xeQuickEditor);
            }
            DataGridViewRowCollection drc = FormConfigEditor.GetItems();
            if (drc != null)
            {
                foreach (DataGridViewRow row in drc)
                {
                    if (row.Cells["ColumnElement"] != null &&
                        row.Cells["ColumnElement"].Value != null &&
                        row.Cells["ColumnElement"].Value.ToString().Length > 0 &&
                        row.Cells["ColumnValue"] != null)
                    {
                        XElement xeItem = new XElement("Item");
                        xeItem.SetAttributeValue("keyword", row.Cells["ColumnElement"].Value);
                        if (row.Cells["ColumnValue"].Value != null && row.Cells["ColumnValue"].Value.ToString().Length > 0)
                        {
                            xeItem.Value = row.Cells["ColumnValue"].Value.ToString();
                        }
                        else
                        {
                            xeItem.Value = "";
                        }
                        xeQuickEditor.Add(xeItem);
                    }
                }
            }
            xeDefaultSettings.Save(frmMain.DefaultSettingsPath);
        }

        private void startInstallCheck()
        {
            if (tdInstallCheck != null)
            {
                tdInstallCheck.Abort(1000);
                tdInstallCheck = null;
            }
            tdInstallCheck = new Thread(checkInstall_Runnable);
            tdInstallCheck.Start();
        }

        private void checkInstall_Runnable()
        {
            bool isSDCardReady = false;
            bool isDeviceReady = false;
            //bool isProfileReady = false;
            String msg = "";
            Color fontColor = Color.Black;
            while (installCheck_Flag)
            {
                if (installing)
                {
                    msg = "Installing...";
                    fontColor = Color.Black;
                }
                else
                {
                    msg = "";
                    showStatusMessage("");
                    Thread.Sleep(300);
                    switch (frmMain.Platform.ToLower())
                    {
                        case "ce":
                            isDeviceReady = frmMain.ConnectedDevices.Count == 1;
                            if (frmMain.ConnectedDevices.Count == 0)
                            {
                                msg = "Device is not connected.";
                            }
                            else if (frmMain.ConnectedDevices.Count > 1)
                            {
                                msg = "There are more too many devices connected";
                            }
                            isSDCardReady = true;
                            break;
                        case "android":
                            isDeviceReady = frmMain.ConnectedDevices.Count == 1;
                            if (frmMain.ConnectedDevices.Count == 0)
                            {
                                msg = "Device is not connected";
                            }
                            else if (frmMain.ConnectedDevices.Count > 1)
                            {
                                msg = "There are more too many devices connected";
                            }
                            else
                            {
                                String rtnValue = "";
                                ADB_Process.RunAdbCommand("shell ls /sdcard/", ref rtnValue);
                                isSDCardReady = !rtnValue.ToLower().Contains("permission denied");
                                if (!isSDCardReady)
                                {
                                    if (msg.Length > 0)
                                    {
                                        msg += " ; ";
                                    }
                                    msg += "Please check the SD Card";
                                }
                            }
                            break;
                    }

                    if (!isConfigFileReady)
                    {
                        if (msg.Length > 0)
                        {
                            msg += " ; ";
                        }
                        msg += "Configuraions are not ready";
                    }
                    //isProfileReady = File.Exists(profilePath);
                    //if (!isProfileReady)
                    //{
                    //    if (msg.Length > 0)
                    //    {
                    //        msg += " ; ";
                    //    }
                    //    msg += "Profile application is not ready";
                    //}
                    if (isConfigFileReady & isDeviceReady & isSDCardReady) //& isProfileReady)
                    {
                        msg = "Ready to install...";
                        fontColor = Color.Green;
                    }
                    else
                    {
                        fontColor = Color.Crimson;
                    }
                    btnInstallEnable(isDeviceReady & isSDCardReady); //&isConfigFileReady & isProfileReady);
                    showStatusMessage(msg, fontColor);
                    Thread.Sleep(700);
                }
            }
        }

        private delegate void delVoidStringColor(String msg, Color fontColor);
        private void showStatusMessage(String msg, Color fontColor)
        {
            if (this.InvokeRequired)
            {
                delVoidStringColor del = new delVoidStringColor(showStatusMessage);
                this.Invoke(del, msg, fontColor);
            }
            else
            {
                tslStatusLabel.Text = msg;
                tslStatusLabel.ForeColor = fontColor;
            }
        }
        private void showStatusMessage(String msg)
        {
            showStatusMessage(msg, Color.Black);
        }

        private delegate void delVoidBool(bool enable);
        private void btnInstallEnable(bool enable)
        {
            if (this.InvokeRequired)
            {
                delVoidBool del = new delVoidBool(btnInstallEnable);
                this.Invoke(del, enable);
            }
            else
            {
                btnInstall.Enabled = enable;
            }
        }

        private void install(String testCaseName)
        {
            installing = true;
            switch (frmMain.Platform.ToLower())
            {
                case "ce":
                    //CE_Process.CopyFileToDevice(profilePath, "\\Application\\" + Path.GetFileName(profilePath), true);
                    #region Attachments
                    foreach (ListViewItem li in lsvAttachment.CheckedItems)
                    {
                        if (li.Tag != null && li.SubItems[1] != null)
                        {
                            String source = li.Tag.ToString();
                            String destination = li.SubItems[1].Text;
                            if (File.Exists(source))
                            {
                                CE_Process.CopyFileToDevice(source, destination, true);
                            }
                        }
                    }
                    #endregion Attachments
                    #region Configurations
                    foreach (ListViewItem li in lsvConfigList.Items)
                    {
                        if (li.Tag != null)
                        {
                            String configPath = li.Tag.ToString();
                            if (File.Exists(configPath))
                            {
                                CE_Process.CopyFileToDevice(configPath, "\\Application\\", true);
                            }
                        }
                    }
                    #endregion Configurations
                    #region Applications
                    foreach (ListViewItem li in lsvApplication.CheckedItems)
                    {
                        if (li.Tag != null)
                        {
                            String appPath = li.Tag.ToString();
                            if (File.Exists(appPath))
                            {
                                CE_Process.CopyFileToDevice(appPath, "\\Application\\", true);
                            }
                        }
                    }
                    #endregion Applications
                    //Not implement yet.
                    break;
                case "android":
                    String Moto_Profile_Package_Name = "com.motorolasolutions.etg.yasta";
                    bool motoProfileAPK_Installed = false;
                    #region "grep" maybe not supported on Android 4.1 or older version, do it by myself.
                    /*
                    List<String> installedPackages = ADB_Process.GetPackagesList(Moto_Profile_Package_Name);
                    if (installedPackages.Count == 0)
                    {
                        ADB_Process.RunAdbCommand("install -r \"" + profilePath + "\"");
                    }
                    */
                    List<String> installedPackages = ADB_Process.GetPackagesList();
                    foreach (String packageName in installedPackages)
                    {
                        if (packageName.Contains(Moto_Profile_Package_Name))
                        {
                            motoProfileAPK_Installed = true;
                            break;
                        }
                    }
                    #endregion "grep" maybe not supported on Android 4.1 or older version, check package name by myself.
                    #region Attachments
                    foreach (ListViewItem li in lsvAttachment.CheckedItems)
                    {
                        if (li.Tag != null && li.SubItems[1] != null)
                        {
                            String source = li.Tag.ToString();
                            String destination = li.SubItems[1].Text;
                            if (File.Exists(source))
                            {
                                ADB_Process.RunAdbCommand("push \"" + source + "\" "+destination);
                            }
                        }
                    }
                    #endregion Attachments
                    #region Configurations
                    foreach (ListViewItem li in lsvConfigList.Items)
                    {
                        if (li.Tag != null)
                        {
                            String configPath = li.Tag.ToString();
                            if (File.Exists(configPath))
                            {
                                ADB_Process.RunAdbCommand("push \"" + configPath + "\" /sdcard/");
                            }
                        }
                    }
                    #endregion Configurations
                    #region Applications
                    foreach (ListViewItem li in lsvApplication.CheckedItems)
                    {
                        if (li.Tag != null)
                        {
                            String appPath = li.Tag.ToString();
                            if(File.Exists(appPath))
                            {
                                ADB_Process.RunAdbCommand("install -r \"" + appPath + "\"");
                            }
                        }
                    }
                    #endregion  Applications
                    break;
                default:
                    break;
            }
            installing = false;
        }

        private void saveResultAndComment(String trsPath)
        {
            if (tcSelected.Edited && tempEditedResult != TestCase.EnumTestResult.None)
            {
                Workbook workBook;
                Worksheet workSheet;
                // Save the temporarty status to selected test case.
                tcSelected.TestResult = tempEditedResult;
                tcSelected.ResultComment = txtResultComment.Text;
                #region Save to TRS excel file
                try
                {
                    workBook = new Workbook(trsPath);
                    for(int index = workBook.Worksheets.Count-1; index>=0;index--)
                    {
                        if (workBook.Worksheets[index].Name.StartsWith("Evaluation Warning"))
                        {
                            workBook.Worksheets.RemoveAt(index);
                        }
                    }
                    workSheet = workBook.Worksheets["EnterResults"];
                    if (workSheet == null)
                    {
                        workSheet = workBook.Worksheets[0];
                    }
                    Row currectRow = workSheet.Cells.Rows[tcSelected.RowIndex];
                    Cell cellResult = currectRow.GetCellOrNull(TestResultColumn);
                    cellResult.Value = Enum.GetName(typeof(TestCase.EnumTestResult), tcSelected.TestResult);
                    Cell cellComment = currectRow.GetCellOrNull(ResultCommentColumn);
                    cellComment.Value = txtResultComment.Text;
                    refreshAutoCompleteResultComment(txtResultComment.Text);
                    workBook.Save(trsPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fail to save data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    workBook = null;
                }
                #endregion Save to TRS excel file
            }
            tcSelected.Edited = false;
            btnSaveResult.Enabled = false;
            btnNextTestCase.Enabled = false;
        }

        private void refreshAutoCompleteResultComment()
        {
            txtResultComment.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtResultComment.AutoCompleteCustomSource = autoCompleteCollectionResultComment;
            txtResultComment.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private void refreshAutoCompleteResultComment(String autoCompleteString)
        {
            if (!autoCompleteCollectionResultComment.Contains(autoCompleteString))
            {
                autoCompleteCollectionResultComment.Add(autoCompleteString);
            }
            refreshAutoCompleteResultComment();
        }

        #region UI event

        private void selectedPlatformChanged(object sender, EventArgs ea)
        {
            loadTestplanFolder(frmMain.Platform);
            txtTRSPath.Text = "";
            lstTestCase_All.Clear();
            lstTestCase_Filtered.Clear();
            lsvApplication.Items.Clear();
            lsvConfigList.Items.Clear();
            lsvAttachment.Items.Clear();
            cmbFilteredTestCases.Items.Clear();
            cmbFilteredTestCases.Refresh();
        }

        private void btnOpenTestPlan_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = testPlanDefaultFolder;
            openFileDialog1.Filter = "Excel |*.xls| Excel |*.xlsx";
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                testPlanDefaultFolder = Path.GetDirectoryName(openFileDialog1.FileName);
                this.Cursor = Cursors.WaitCursor;
                txtTRSPath.Tag = openFileDialog1.FileName;
                txtTRSPath.Text = Path.GetFileName(txtTRSPath.Tag.ToString());
                loadTestCaseFromTestPlan(txtTRSPath.Tag.ToString());
                if (FormToolSettings.KeywordFilterEnable)
                {
                    filterTestCase(FormToolSettings.KeywordFilter_Text, FormToolSettings.TestResultFilter);
                }
                else
                {
                    lstTestCase_Filtered.Clear();
                    lstTestCase_Filtered.AddRange(lstTestCase_All);
                }
                showFilteredTestCase();
                if (!defaultSettingsLoading)
                {
                    saveTestPlanFolder();
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void cmbFilteredTestCases_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogResult dr = DialogResult.No;
            if (tcSelected != null && tcSelected.Edited)
            {
                dr = MessageBox.Show("The test result or comment is edited, do you want to save it ?", "Test case is edited", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.Equals(DialogResult.Yes))
                {
                    saveResultAndComment(txtTRSPath.Tag.ToString());
                }
                else if (dr.Equals(DialogResult.No))
                {
                    tcSelected.Edited = false; // Never notice this test case is edited again.
                }
            }
            if (dr.Equals(DialogResult.Cancel)) //Cancel the selecte action
            {
            }
            else // Load the new test case.
            {
                selectedTCChanging = true;
                tempEditedResult = TestCase.EnumTestResult.None;
                //rtextConfig.Clear();
                tcSelected = lstTestCase_Filtered[cmbFilteredTestCases.SelectedIndex];
                tcSelected.ConfigFloderName = getReleateFolderName(FormToolSettings.TRS_ConfigSourceFolder, tcSelected.ConfigAssociatedKeyword);
                #region Double check if the ConfigFloder exist for Android
                if (tcSelected.ConfigFloderName.Length == 0 || !Directory.Exists(FormToolSettings.TRS_ConfigSourceFolder + "\\" + tcSelected.ConfigFloderName))
                {
                    tcSelected.ConfigFloderName = tcSelected.ID;
                }
                #endregion Double check if the ConfigFloder exist for Android
                showTestCaseInformation(tcSelected);
                rdbB.Enabled = rdbP.Enabled = rdbI.Enabled = rdbF.Enabled = txtResultComment.Enabled = (tcSelected != null);
                btnSaveResult.Enabled = false;
                btnNextTestCase.Enabled = false;
                selectedTCChanging = false;
            }

            //pickAttachment();
        }
                
        //private void getAutoCompleteResultComment(String testPlanFile)
        //{
        //    Workbook workBook;
        //    Worksheet workSheet;
        //    workBook = new Workbook(testPlanFile);
        //    autoCompleteCollectionResultComment.Clear();
        //    workSheet = workBook.Worksheets["EnterResults"];
        //    if (workSheet == null)
        //    {
        //        workSheet = workBook.Worksheets[0];
        //    }
        //    foreach (Row row in workSheet.Cells.Rows)
        //    {
        //        Cell cellComment = row.GetCellOrNull(ResultCommentColumn);
        //        if (!autoCompleteCollectionResultComment.Contains(cellComment.Value.ToString()))
        //        {
        //            autoCompleteCollectionResultComment.Add(cellComment.Value.ToString());
        //        }
        //    }
        //}
        
        private void btnInstall_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnInstall.Enabled = false;
            install(cmbFilteredTestCases.Text);
            this.Cursor = Cursors.Default;
            MessageBox.Show("Installation is done!");
        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            int width = pnlDescription.Width - rtxtDescription.Location.X - 5;
            if (width > 0)
            {
                rtxtDescription.Width = width;
            }
        }

        private void btnSaveResult_Click(object sender, EventArgs e)
        {
            saveResultAndComment(txtTRSPath.Tag.ToString());
        }

        private void testResultChanged(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;
            if (rdb != null && rdb.Checked)
            {
                if (rdbP.Checked)
                    tempEditedResult = TestCase.EnumTestResult.P;
                else if (rdbF.Checked)
                    tempEditedResult = TestCase.EnumTestResult.F;
                else if (rdbB.Checked)
                    tempEditedResult = TestCase.EnumTestResult.B;
                else if (rdbI.Checked)
                    tempEditedResult = TestCase.EnumTestResult.I;
                else
                    tempEditedResult = TestCase.EnumTestResult.None;

                if ((tempEditedResult != TestCase.EnumTestResult.None) &&
                    tcSelected != null &&
                    tempEditedResult != tcSelected.TestResult &&
                    !selectedTCChanging)
                {
                    //tcSelected.TestResult = editedResult;
                    tcSelected.Edited = true;
                    btnSaveResult.Enabled = true;
                    btnNextTestCase.Enabled = true;
                }
            }
        }

        private void resultCommandChanged(object sender, EventArgs e)
        {
            if ((rdbB.Checked || rdbF.Checked || rdbI.Checked || rdbP.Checked) && tcSelected != null && !selectedTCChanging)
            {
                tcSelected.Edited = true;
                btnNextTestCase.Enabled = true;
                btnSaveResult.Enabled = true;
            }
        }

        private void btnOpenTestPlan_LocationChanged(object sender, EventArgs e)
        {
            txtTRSPath.Width = btnOpenTestPlan.Location.X - txtTRSPath.Location.X - 3;
        }

        private void lvAttachment_Resize(object sender, EventArgs e)
        {
            columnDestination.Width = lsvAttachment.Width - columnName.Width - 3;
        }

        private void btnReloadTestPlan_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            loadTestCaseFromTestPlan(txtTRSPath.Tag.ToString());
            if (FormToolSettings.KeywordFilterEnable)
            {
                filterTestCase(FormToolSettings.KeywordFilter_Text, FormToolSettings.TestResultFilter);
            }
            else
            {
                lstTestCase_Filtered.Clear();
                lstTestCase_Filtered.AddRange(lstTestCase_All);
            }
            showFilteredTestCase();
            this.Cursor = Cursors.Default;
        }

        private void txtTestPlanPath_TextChanged(object sender, EventArgs e)
        {
            btnReloadTestPlan.Enabled = txtTRSPath.Text.Length > 0;
        }

        private void btnAttachmentsPage_Changed(object sender, EventArgs e)
        {
            Button btnSelected = sender as Button;
            Panel pnlSelected = null;
            pnlApplications.Visible = false;
            pnlAttachments.Visible = false;
            pnlConfigurations.Visible = false;
            pnlApplications.Dock = DockStyle.None;
            pnlAttachments.Dock = DockStyle.None;
            pnlConfigurations.Dock = DockStyle.None;
            btnAttachments.BackColor = System.Drawing.SystemColors.Control;
            btnConfigurations.BackColor = System.Drawing.SystemColors.Control;
            btnApplications.BackColor = System.Drawing.SystemColors.Control;
            btnAttachments.ForeColor = System.Drawing.SystemColors.ControlText;
            btnConfigurations.ForeColor = System.Drawing.SystemColors.ControlText;
            btnApplications.ForeColor = System.Drawing.SystemColors.ControlText;
            btnSelected.BackColor = System.Drawing.Color.Purple;
            btnSelected.ForeColor = System.Drawing.Color.White;
            if (btnSelected.Equals(btnAttachments))
            {
                pnlSelected = pnlAttachments;
            }
            else if (btnSelected.Equals(btnConfigurations))
            {
                pnlSelected = pnlConfigurations;
            }
            else
            {
                pnlSelected = pnlApplications;
            }
            pnlSelected.Dock = DockStyle.Fill;
            pnlSelected.Visible = true;
        }

        private void splitContainer2_Panel2_SizeChanged(object sender, EventArgs e)
        {
            txtResultComment.Width = splitContainer2.Panel2.Width - txtResultComment.Location.X - 3;
            txtResultComment.Height = splitContainer2.Panel2.Height - txtResultComment.Location.Y - 3;
        }
        
        private void lsvConfigList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem li = null;
            try
            {
                li = lsvConfigList.GetItemAt(e.X, e.Y);
            }
            catch { }
            if (li != null && li.Index != lsvConfigList.Items.Count-1) // The last item is a message for user remark, just ignore it.
            {
                if (tcSelected != null)
                {
                    String configPath = FormToolSettings.TRS_ConfigSourceFolder + "\\" + tcSelected.ConfigFloderName + "\\" + li.Text;
                    if (File.Exists(configPath))
                    {
                        FormConfigEditor.SetConfigText(tcSelected, configPath);
                        if (FormConfigEditor.Show().Equals(DialogResult.OK))
                        {
                            saveQuickEditorItems();
                            saveFile(configPath, FormConfigEditor.GetConfigText(),false);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The configuration file \"" + configPath + "\" doesn't exist, please check the file path and try again.", "File doesn't exiist!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        
        private void splitContainer4_Panel1_SizeChanged(object sender, EventArgs e)
        {
            rtxtDescription.Width = splitContainer4.Panel1.Width - rtxtDescription.Location.X - 3;
        }

        private void toolSettings_Changed(object sender, EventArgs e)
        {
            //if (lastKeywordFilter != FormToolSettings.KeywordFilter_Text || lastResultFilter != FormToolSettings.TestResultFilter)
            //{
            lastKeywordFilter = FormToolSettings.KeywordFilter_Text;
            lastResultFilter = FormToolSettings.TestResultFilter;
            filterTestCase(FormToolSettings.KeywordFilter_Text, FormToolSettings.TestResultFilter);
            //}
        }

        private void groupBox1_SizeChanged(object sender, EventArgs e)
        {
            int totalWidth = groupBox1.Width - cmbFilteredTestCases.Location.X;
            cmbFilteredTestCases.Width = totalWidth - 6;
        }

        private void btnAppAdd_Click(object sender, EventArgs e)
        {
            if(frmMain.Platform.ToLower().Equals("ce"))
            {
                openFileDialog1.Filter = "exe file|*.exe";
                openFileDialog1.Multiselect = true;
                if(openFileDialog1.ShowDialog().Equals(DialogResult.OK))
                {
                    foreach (String file in openFileDialog1.FileNames)
                    {
                        addApplication(file);
                    }
                }
            }
            else if(frmMain.Platform.ToLower().Equals("android"))
            {
                openFileDialog1.Filter = "APK file|*.apk";
                openFileDialog1.Multiselect = true;
                if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
                {
                    foreach (String file in openFileDialog1.FileNames)
                    {
                        addApplication(file);
                    }
                }
            } 
        }

        private void btnConfigAdd_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = FormToolSettings.TRS_ConfigSourceFolder;
            openFileDialog1.Filter = "xml file|*.xml|txt file|*.txt";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    addConfig(file);
                }
            }
        }

        private void listview_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lvApplication_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                String extensionName = "";
                if (frmMain.Platform.ToLower().Equals("ce"))
                {
                    extensionName = ".exe";
                }
                else if (frmMain.Platform.ToLower().Equals("android"))
                {
                    extensionName = ".apk";
                }
                String[] files = e.Data.GetData(DataFormats.FileDrop) as String[];
                foreach (String file in files)
                {
                    if (Path.GetExtension(file).ToLower().Equals(extensionName))
                    {
                        addApplication(file);
                    }
                }
            }
        }

        private void lsvConfigList_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                String[] files = e.Data.GetData(DataFormats.FileDrop) as String[];
                foreach (String file in files)
                {
                    String extensionName = Path.GetExtension(file);
                    if (extensionName.ToLower().Equals(".xml")||extensionName.ToLower().Equals(".txt"))
                    {
                        addConfig(file);
                    }
                }
            }
        }

        private void btnApp_Remove_Click(object sender, EventArgs e)
        {
            if (lsvApplication.SelectedItems.Count > 0 && 
                MessageBox.Show("Do you really want to remove the selected file(s)?",
                "Remove item",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                foreach (ListViewItem li in lsvApplication.SelectedItems)
                {
                    li.Remove();
                }
            }
        }

        private void btnConfigRemove_Click(object sender, EventArgs e)
        {
            if (lsvConfigList.SelectedItems.Count > 0 && 
                MessageBox.Show("Do you really want to remove the selected file(s)?", 
                "Remove item", 
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                foreach (ListViewItem li in lsvConfigList.SelectedItems)
                {
                    li.Remove();
                }
            }
        }

        private void btnAttachmentRemove_Click(object sender, EventArgs e)
        {
            if (lsvAttachment.SelectedItems.Count>0 && 
                MessageBox.Show("Do you really want to remove the selected file(s)?", 
                "Remove item", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question).Equals(DialogResult.Yes))
            {
                foreach (ListViewItem li in lsvAttachment.SelectedItems)
                {
                    li.Remove();
                }
            }
        }

        private void addApplication(String appPath)
        {
            ListViewItem li = new ListViewItem(Path.GetFileName(appPath));          
            if (File.Exists(appPath))
            {
                li.Tag = appPath;
                li.Checked = true;
                li.ForeColor = SystemColors.ControlText;
            }
            else
            {
                li.Checked = false;
                li.ForeColor = Color.Crimson;
            }
            lsvApplication.Items.Add(li);
        }

        private void addConfig(String configPath)
        {
            ListViewItem li = new ListViewItem(Path.GetFileName(configPath));
            li.Tag = configPath;
            lsvConfigList.Items.Add(li);
            li.Checked = true;
        }

        private void addAttachment(String attachmentPath)
        {
            ListViewItem li = new ListViewItem(Path.GetFileName(attachmentPath));
            li.Tag = attachmentPath;
            ListViewItem.ListViewSubItem lsi = new ListViewItem.ListViewSubItem();
            if (frmMain.Platform.ToLower().Equals("ce"))
            {
                lsi.Text = "\\Application";
            }
            else if (frmMain.Platform.ToLower().Equals("android"))
            {
                lsi.Text = "/sdcard/";
            }
            li.SubItems.Add(lsi);
            lsvAttachment.Items.Add(li);
            li.Checked = true;
        }

        private void btnAttachmentAdd_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "all files|*.*";
            openFileDialog1.Multiselect = true;
            if(openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    addAttachment(file);
                }
            }
        }

        private void btnNextTestCase_Click(object sender, EventArgs e)
        {
            saveResultAndComment(txtTRSPath.Tag.ToString());
            if (cmbFilteredTestCases.SelectedIndex == cmbFilteredTestCases.Items.Count - 1)
            {
                MessageBox.Show("It's the last test case already.");
            }
            else
            {
                cmbFilteredTestCases.SelectedIndex = cmbFilteredTestCases.SelectedIndex + 1;
            }
        }

        private void lsvApplication_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem li = lsvApplication.GetItemAt(e.X, e.Y);
            if (li != null)
            {
                String filePath = "";
                if (li.Tag != null)
                {
                    filePath = li.Tag.ToString();
                }
                if (FormFilePathEditor.Show(li.Text, filePath).Equals(DialogResult.OK))
                {
                    li.Text = FormFilePathEditor.FileName;
                    li.Tag = FormFilePathEditor.FilePath;
                    if (FormFilePathEditor.FilePath.Length > 0)
                    {
                        li.ForeColor = System.Drawing.SystemColors.ControlText;
                        li.Checked = true;
                    }
                    else
                    {
                        li.ForeColor = System.Drawing.Color.Crimson;
                        li.Checked = false;
                    }
                }
            }
        }

        private void lsvApplication_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Tag == null || e.Item.Tag.ToString().Length == 0)
            {
                if (e.Item.Checked)
                {
                    e.Item.Checked = false;
                }
            }     
        }

        private void lsvAttachment_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                String[] files = e.Data.GetData(DataFormats.FileDrop) as String[];
                foreach (String file in files)
                {
                    addAttachment(file);
                }
            }
        }

        private void lsv_KeyDown(object sender, KeyEventArgs e)
        {
            ListView lsv = sender as ListView;
            if (lsv != null && lsv.SelectedItems.Count > 0)
            {
                if (e.KeyCode.Equals(Keys.Delete) && MessageBox.Show("Do you really want to remove the selected file(s)?",
                                                                                                                     "Remove item",
                                                                                                                      MessageBoxButtons.YesNo,
                                                                                                                      MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    foreach (ListViewItem li in lsv.SelectedItems)
                    {
                        li.Remove();
                    }
                }
            }
        }

        #endregion UI event

        #region Special function to filter non-applicable TCs form list
        private void button1_Click(object sender, EventArgs e)
        {
            autoMarkedIgnoreTCs();
        }

        private void autoMarkedIgnoreTCs()
        {
            Worksheet workSheet;
            Workbook workBook;
            openFileDialog1.Filter = "";
            if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                workBook = new Workbook(txtTRSPath.Tag.ToString());
                for (int index = workBook.Worksheets.Count - 1; index >= 0; index--)
                {
                    if (workBook.Worksheets[index].Name.StartsWith("Evaluation Warning"))
                    {
                        workBook.Worksheets.RemoveAt(index);
                    }
                }
                workSheet = workBook.Worksheets["EnterResults"];
                if (workSheet == null)
                {
                    workSheet = workBook.Worksheets[0];
                }

                List<String> nonApplicableList = getNonApplicableList(openFileDialog1.FileName);
                foreach (String id in nonApplicableList)
                {
                    foreach (TestCase tc in lstTestCase_Filtered)
                    {
                        if (tc.ID.Equals(id))
                        {
                            tc.TestResult = TestCase.EnumTestResult.I;
                            tc.ResultComment = "[Auto-detect] This TC is not applicable to UUT.";
                            Row currectRow = workSheet.Cells.Rows[tc.RowIndex];
                            Cell cellResult = currectRow.GetCellOrNull(TestResultColumn);
                            cellResult.Value = Enum.GetName(typeof(TestCase.EnumTestResult), tc.TestResult);
                            Cell cellComment = currectRow.GetCellOrNull(ResultCommentColumn);
                            cellComment.Value = tc.ResultComment;
                            break;
                        }
                    }
                }
                #region Save to TRS excel file
                try
                {
                    refreshAutoCompleteResultComment(txtResultComment.Text);
                    workBook.Save(txtTRSPath.Tag.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fail to save data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    workBook = null;
                }
                #endregion Save to TRS excel file
            }
        }

        private List<String> getNonApplicableList(String path)
        {
            List<String> returnValue = new List<string>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(path);
                while (!sr.EndOfStream)
                {
                    String tmp = sr.ReadLine().Trim();
                    if (tmp.Length > 0)
                    {
                        returnValue.Add(tmp);
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
            return returnValue;
        }
        #endregion Special function to filter non-applicable TCs form list
    }

    public class ParametersOfTestCaseConfiguation
    {
        public String AssociatedKeyword = "";
        public List<TestCase.NecessaryFile_Info> NecessaryFileList;
        public ParametersOfTestCaseConfiguation()
        {
            NecessaryFileList = new List<TestCase.NecessaryFile_Info>();
        }
    }

}