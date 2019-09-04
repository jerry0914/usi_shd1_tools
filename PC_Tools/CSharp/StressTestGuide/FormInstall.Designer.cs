namespace com.usi.shd1_tools.TestGuide
{
    partial class FormInstall
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            installCheck_Flag = false;
            System.Threading.Thread.Sleep(1000);
            if (tdInstallCheck != null)
            {
                tdInstallCheck.Abort(1000);
                tdInstallCheck = null;
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInstall));
            this.folderBrowserDialog_ConfigSource = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTRSPath = new System.Windows.Forms.TextBox();
            this.cmbFilteredTestCases = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReloadTestPlan = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpenTestPlan = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.rtxtTPSData = new System.Windows.Forms.RichTextBox();
            this.pnlDescription = new System.Windows.Forms.Panel();
            this.rtxtDescription = new System.Windows.Forms.RichTextBox();
            this.pnlAttachments = new System.Windows.Forms.Panel();
            this.lsvAttachment = new dev.jerry_h.pc_tools.CommonLibrary.ListView_DisableAutoCheckedOnDoubleClick(this.components);
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDestination = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnAttachmentRemove = new System.Windows.Forms.Button();
            this.btnAttachmentAdd = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlConfigurations = new System.Windows.Forms.Panel();
            this.lsvConfigList = new System.Windows.Forms.ListView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnConfigRemove = new System.Windows.Forms.Button();
            this.btnConfigAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlApplications = new System.Windows.Forms.Panel();
            this.lsvApplication = new dev.jerry_h.pc_tools.CommonLibrary.ListView_DisableAutoCheckedOnDoubleClick(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAppRemove = new System.Windows.Forms.Button();
            this.btnAppAdd = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnAttachments = new System.Windows.Forms.Button();
            this.btnConfigurations = new System.Windows.Forms.Button();
            this.btnApplications = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.rtxtExpectedResult = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnNextTestCase = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSaveResult = new System.Windows.Forms.Button();
            this.rdbP = new System.Windows.Forms.RadioButton();
            this.txtResultComment = new System.Windows.Forms.TextBox();
            this.rdbF = new System.Windows.Forms.RadioButton();
            this.rdbB = new System.Windows.Forms.RadioButton();
            this.rdbI = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.pnlDescription.SuspendLayout();
            this.pnlAttachments.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlConfigurations.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlApplications.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "TRS Path:";
            // 
            // txtTRSPath
            // 
            this.txtTRSPath.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTRSPath.Location = new System.Drawing.Point(100, 25);
            this.txtTRSPath.Name = "txtTRSPath";
            this.txtTRSPath.ReadOnly = true;
            this.txtTRSPath.Size = new System.Drawing.Size(311, 21);
            this.txtTRSPath.TabIndex = 1;
            this.txtTRSPath.TextChanged += new System.EventHandler(this.txtTestPlanPath_TextChanged);
            // 
            // cmbFilteredTestCases
            // 
            this.cmbFilteredTestCases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilteredTestCases.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilteredTestCases.FormattingEnabled = true;
            this.cmbFilteredTestCases.Location = new System.Drawing.Point(564, 23);
            this.cmbFilteredTestCases.Name = "cmbFilteredTestCases";
            this.cmbFilteredTestCases.Size = new System.Drawing.Size(472, 23);
            this.cmbFilteredTestCases.TabIndex = 6;
            this.cmbFilteredTestCases.SelectedIndexChanged += new System.EventHandler(this.cmbFilteredTestCases_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReloadTestPlan);
            this.groupBox1.Controls.Add(this.cmbFilteredTestCases);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTRSPath);
            this.groupBox1.Controls.Add(this.btnOpenTestPlan);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1105, 58);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test Case Filter";
            this.groupBox1.SizeChanged += new System.EventHandler(this.groupBox1_SizeChanged);
            // 
            // btnReloadTestPlan
            // 
            this.btnReloadTestPlan.Enabled = false;
            this.btnReloadTestPlan.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReloadTestPlan.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.refresh;
            this.btnReloadTestPlan.Location = new System.Drawing.Point(449, 22);
            this.btnReloadTestPlan.Name = "btnReloadTestPlan";
            this.btnReloadTestPlan.Size = new System.Drawing.Size(26, 26);
            this.btnReloadTestPlan.TabIndex = 15;
            this.btnReloadTestPlan.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnReloadTestPlan.UseVisualStyleBackColor = true;
            this.btnReloadTestPlan.Click += new System.EventHandler(this.btnReloadTestPlan_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(486, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Test Case :";
            // 
            // btnOpenTestPlan
            // 
            this.btnOpenTestPlan.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenTestPlan.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenTestPlan.Image")));
            this.btnOpenTestPlan.Location = new System.Drawing.Point(417, 22);
            this.btnOpenTestPlan.Name = "btnOpenTestPlan";
            this.btnOpenTestPlan.Size = new System.Drawing.Size(26, 26);
            this.btnOpenTestPlan.TabIndex = 2;
            this.btnOpenTestPlan.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnOpenTestPlan.UseVisualStyleBackColor = true;
            this.btnOpenTestPlan.LocationChanged += new System.EventHandler(this.btnOpenTestPlan_LocationChanged);
            this.btnOpenTestPlan.Click += new System.EventHandler(this.btnOpenTestPlan_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "file.png");
            this.imageList1.Images.SetKeyName(1, "Folder.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 713);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(1105, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslStatusLabel
            // 
            this.tslStatusLabel.Name = "tslStatusLabel";
            this.tslStatusLabel.Size = new System.Drawing.Size(85, 17);
            this.tslStatusLabel.Text = "tslStatusLabel";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 58);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer3.Size = new System.Drawing.Size(1105, 655);
            this.splitContainer3.SplitterDistance = 515;
            this.splitContainer3.TabIndex = 11;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.rtxtTPSData);
            this.splitContainer4.Panel1.Controls.Add(this.pnlDescription);
            this.splitContainer4.Panel1.SizeChanged += new System.EventHandler(this.splitContainer4_Panel1_SizeChanged);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.pnlAttachments);
            this.splitContainer4.Panel2.Controls.Add(this.pnlConfigurations);
            this.splitContainer4.Panel2.Controls.Add(this.pnlApplications);
            this.splitContainer4.Panel2.Controls.Add(this.panel4);
            this.splitContainer4.Size = new System.Drawing.Size(1105, 515);
            this.splitContainer4.SplitterDistance = 686;
            this.splitContainer4.TabIndex = 8;
            // 
            // rtxtTPSData
            // 
            this.rtxtTPSData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtTPSData.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rtxtTPSData.Location = new System.Drawing.Point(0, 25);
            this.rtxtTPSData.Name = "rtxtTPSData";
            this.rtxtTPSData.Size = new System.Drawing.Size(686, 490);
            this.rtxtTPSData.TabIndex = 3;
            this.rtxtTPSData.Text = "";
            // 
            // pnlDescription
            // 
            this.pnlDescription.Controls.Add(this.rtxtDescription);
            this.pnlDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDescription.Location = new System.Drawing.Point(0, 0);
            this.pnlDescription.Name = "pnlDescription";
            this.pnlDescription.Size = new System.Drawing.Size(686, 25);
            this.pnlDescription.TabIndex = 6;
            // 
            // rtxtDescription
            // 
            this.rtxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtDescription.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rtxtDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.rtxtDescription.Location = new System.Drawing.Point(0, 0);
            this.rtxtDescription.Multiline = false;
            this.rtxtDescription.Name = "rtxtDescription";
            this.rtxtDescription.ReadOnly = true;
            this.rtxtDescription.Size = new System.Drawing.Size(686, 25);
            this.rtxtDescription.TabIndex = 2;
            this.rtxtDescription.Text = "";
            // 
            // pnlAttachments
            // 
            this.pnlAttachments.Controls.Add(this.lsvAttachment);
            this.pnlAttachments.Controls.Add(this.panel5);
            this.pnlAttachments.Location = new System.Drawing.Point(278, 6);
            this.pnlAttachments.Name = "pnlAttachments";
            this.pnlAttachments.Size = new System.Drawing.Size(139, 331);
            this.pnlAttachments.TabIndex = 6;
            // 
            // lsvAttachment
            // 
            this.lsvAttachment.AllowDrop = true;
            this.lsvAttachment.CheckBoxes = true;
            this.lsvAttachment.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnDestination});
            this.lsvAttachment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvAttachment.Location = new System.Drawing.Point(0, 22);
            this.lsvAttachment.Name = "lsvAttachment";
            this.lsvAttachment.Size = new System.Drawing.Size(139, 309);
            this.lsvAttachment.TabIndex = 10;
            this.lsvAttachment.UseCompatibleStateImageBehavior = false;
            this.lsvAttachment.View = System.Windows.Forms.View.Details;
            this.lsvAttachment.SizeChanged += new System.EventHandler(this.lvAttachment_Resize);
            this.lsvAttachment.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsvAttachment_DragDrop);
            this.lsvAttachment.DragEnter += new System.Windows.Forms.DragEventHandler(this.listview_DragEnter);
            this.lsvAttachment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsv_KeyDown);
            // 
            // columnName
            // 
            this.columnName.Text = "Attachment";
            this.columnName.Width = 110;
            // 
            // columnDestination
            // 
            this.columnDestination.Text = "Destination";
            this.columnDestination.Width = 200;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnAttachmentRemove);
            this.panel5.Controls.Add(this.btnAttachmentAdd);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(139, 22);
            this.panel5.TabIndex = 9;
            // 
            // btnAttachmentRemove
            // 
            this.btnAttachmentRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAttachmentRemove.FlatAppearance.BorderSize = 0;
            this.btnAttachmentRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttachmentRemove.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAttachmentRemove.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.minus;
            this.btnAttachmentRemove.Location = new System.Drawing.Point(118, 1);
            this.btnAttachmentRemove.Name = "btnAttachmentRemove";
            this.btnAttachmentRemove.Size = new System.Drawing.Size(18, 18);
            this.btnAttachmentRemove.TabIndex = 3;
            this.btnAttachmentRemove.UseVisualStyleBackColor = true;
            this.btnAttachmentRemove.Click += new System.EventHandler(this.btnAttachmentRemove_Click);
            // 
            // btnAttachmentAdd
            // 
            this.btnAttachmentAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAttachmentAdd.FlatAppearance.BorderSize = 0;
            this.btnAttachmentAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttachmentAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAttachmentAdd.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.add;
            this.btnAttachmentAdd.Location = new System.Drawing.Point(94, 1);
            this.btnAttachmentAdd.Name = "btnAttachmentAdd";
            this.btnAttachmentAdd.Size = new System.Drawing.Size(18, 18);
            this.btnAttachmentAdd.TabIndex = 2;
            this.btnAttachmentAdd.UseVisualStyleBackColor = true;
            this.btnAttachmentAdd.Click += new System.EventHandler(this.btnAttachmentAdd_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 18);
            this.label11.TabIndex = 1;
            this.label11.Text = "       Attachments";
            // 
            // pnlConfigurations
            // 
            this.pnlConfigurations.Controls.Add(this.lsvConfigList);
            this.pnlConfigurations.Controls.Add(this.panel7);
            this.pnlConfigurations.Location = new System.Drawing.Point(123, 5);
            this.pnlConfigurations.Name = "pnlConfigurations";
            this.pnlConfigurations.Size = new System.Drawing.Size(149, 331);
            this.pnlConfigurations.TabIndex = 5;
            // 
            // lsvConfigList
            // 
            this.lsvConfigList.AllowDrop = true;
            this.lsvConfigList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvConfigList.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvConfigList.Location = new System.Drawing.Point(0, 22);
            this.lsvConfigList.Name = "lsvConfigList";
            this.lsvConfigList.Size = new System.Drawing.Size(149, 309);
            this.lsvConfigList.TabIndex = 10;
            this.lsvConfigList.UseCompatibleStateImageBehavior = false;
            this.lsvConfigList.View = System.Windows.Forms.View.List;
            this.lsvConfigList.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsvConfigList_DragDrop);
            this.lsvConfigList.DragEnter += new System.Windows.Forms.DragEventHandler(this.listview_DragEnter);
            this.lsvConfigList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsv_KeyDown);
            this.lsvConfigList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvConfigList_MouseDoubleClick);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnConfigRemove);
            this.panel7.Controls.Add(this.btnConfigAdd);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(149, 22);
            this.panel7.TabIndex = 9;
            // 
            // btnConfigRemove
            // 
            this.btnConfigRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfigRemove.FlatAppearance.BorderSize = 0;
            this.btnConfigRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigRemove.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConfigRemove.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.minus;
            this.btnConfigRemove.Location = new System.Drawing.Point(128, 1);
            this.btnConfigRemove.Name = "btnConfigRemove";
            this.btnConfigRemove.Size = new System.Drawing.Size(18, 18);
            this.btnConfigRemove.TabIndex = 3;
            this.btnConfigRemove.UseVisualStyleBackColor = true;
            this.btnConfigRemove.Click += new System.EventHandler(this.btnConfigRemove_Click);
            // 
            // btnConfigAdd
            // 
            this.btnConfigAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfigAdd.FlatAppearance.BorderSize = 0;
            this.btnConfigAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConfigAdd.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.add;
            this.btnConfigAdd.Location = new System.Drawing.Point(104, 1);
            this.btnConfigAdd.Name = "btnConfigAdd";
            this.btnConfigAdd.Size = new System.Drawing.Size(18, 18);
            this.btnConfigAdd.TabIndex = 2;
            this.btnConfigAdd.UseVisualStyleBackColor = true;
            this.btnConfigAdd.Click += new System.EventHandler(this.btnConfigAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "       Configurations";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlApplications
            // 
            this.pnlApplications.Controls.Add(this.lsvApplication);
            this.pnlApplications.Controls.Add(this.panel2);
            this.pnlApplications.Location = new System.Drawing.Point(3, 3);
            this.pnlApplications.Name = "pnlApplications";
            this.pnlApplications.Size = new System.Drawing.Size(119, 332);
            this.pnlApplications.TabIndex = 4;
            // 
            // lsvApplication
            // 
            this.lsvApplication.AllowDrop = true;
            this.lsvApplication.CheckBoxes = true;
            this.lsvApplication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvApplication.Location = new System.Drawing.Point(0, 22);
            this.lsvApplication.Name = "lsvApplication";
            this.lsvApplication.Size = new System.Drawing.Size(119, 310);
            this.lsvApplication.TabIndex = 9;
            this.lsvApplication.UseCompatibleStateImageBehavior = false;
            this.lsvApplication.View = System.Windows.Forms.View.List;
            this.lsvApplication.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lsvApplication_ItemChecked);
            this.lsvApplication.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvApplication_DragDrop);
            this.lsvApplication.DragEnter += new System.Windows.Forms.DragEventHandler(this.listview_DragEnter);
            this.lsvApplication.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsv_KeyDown);
            this.lsvApplication.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvApplication_MouseDoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAppRemove);
            this.panel2.Controls.Add(this.btnAppAdd);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(119, 22);
            this.panel2.TabIndex = 10;
            // 
            // btnAppRemove
            // 
            this.btnAppRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAppRemove.FlatAppearance.BorderSize = 0;
            this.btnAppRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppRemove.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAppRemove.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.minus;
            this.btnAppRemove.Location = new System.Drawing.Point(98, 1);
            this.btnAppRemove.Name = "btnAppRemove";
            this.btnAppRemove.Size = new System.Drawing.Size(18, 18);
            this.btnAppRemove.TabIndex = 3;
            this.btnAppRemove.UseVisualStyleBackColor = true;
            this.btnAppRemove.Click += new System.EventHandler(this.btnApp_Remove_Click);
            // 
            // btnAppAdd
            // 
            this.btnAppAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAppAdd.FlatAppearance.BorderSize = 0;
            this.btnAppAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAppAdd.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.add;
            this.btnAppAdd.Location = new System.Drawing.Point(74, 1);
            this.btnAppAdd.Name = "btnAppAdd";
            this.btnAppAdd.Size = new System.Drawing.Size(18, 18);
            this.btnAppAdd.TabIndex = 2;
            this.btnAppAdd.UseVisualStyleBackColor = true;
            this.btnAppAdd.Click += new System.EventHandler(this.btnAppAdd_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.App;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 18);
            this.label10.TabIndex = 1;
            this.label10.Text = "       Applications";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnAttachments);
            this.panel4.Controls.Add(this.btnConfigurations);
            this.panel4.Controls.Add(this.btnApplications);
            this.panel4.Controls.Add(this.btnInstall);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 489);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(415, 26);
            this.panel4.TabIndex = 2;
            // 
            // btnAttachments
            // 
            this.btnAttachments.BackColor = System.Drawing.SystemColors.Control;
            this.btnAttachments.FlatAppearance.BorderSize = 0;
            this.btnAttachments.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAttachments.Image = ((System.Drawing.Image)(resources.GetObject("btnAttachments.Image")));
            this.btnAttachments.Location = new System.Drawing.Point(183, 0);
            this.btnAttachments.Name = "btnAttachments";
            this.btnAttachments.Size = new System.Drawing.Size(90, 26);
            this.btnAttachments.TabIndex = 7;
            this.btnAttachments.Text = "Attach...";
            this.btnAttachments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAttachments.UseVisualStyleBackColor = false;
            this.btnAttachments.Click += new System.EventHandler(this.btnAttachmentsPage_Changed);
            // 
            // btnConfigurations
            // 
            this.btnConfigurations.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfigurations.FlatAppearance.BorderSize = 0;
            this.btnConfigurations.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConfigurations.Image = ((System.Drawing.Image)(resources.GetObject("btnConfigurations.Image")));
            this.btnConfigurations.Location = new System.Drawing.Point(93, 0);
            this.btnConfigurations.Name = "btnConfigurations";
            this.btnConfigurations.Size = new System.Drawing.Size(90, 26);
            this.btnConfigurations.TabIndex = 6;
            this.btnConfigurations.Text = "Config...";
            this.btnConfigurations.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfigurations.UseVisualStyleBackColor = false;
            this.btnConfigurations.Click += new System.EventHandler(this.btnAttachmentsPage_Changed);
            // 
            // btnApplications
            // 
            this.btnApplications.BackColor = System.Drawing.SystemColors.Control;
            this.btnApplications.FlatAppearance.BorderSize = 0;
            this.btnApplications.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnApplications.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.App;
            this.btnApplications.Location = new System.Drawing.Point(3, 0);
            this.btnApplications.Name = "btnApplications";
            this.btnApplications.Size = new System.Drawing.Size(90, 26);
            this.btnApplications.TabIndex = 5;
            this.btnApplications.Text = "App...";
            this.btnApplications.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnApplications.UseVisualStyleBackColor = false;
            this.btnApplications.Click += new System.EventHandler(this.btnAttachmentsPage_Changed);
            // 
            // btnInstall
            // 
            this.btnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInstall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnInstall.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnInstall.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.phone_transfer;
            this.btnInstall.Location = new System.Drawing.Point(334, 0);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(80, 26);
            this.btnInstall.TabIndex = 4;
            this.btnInstall.Text = " Install";
            this.btnInstall.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInstall.UseVisualStyleBackColor = false;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.rtxtExpectedResult);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Panel2.Controls.Add(this.btnNextTestCase);
            this.splitContainer2.Panel2.Controls.Add(this.label9);
            this.splitContainer2.Panel2.Controls.Add(this.btnSaveResult);
            this.splitContainer2.Panel2.Controls.Add(this.rdbP);
            this.splitContainer2.Panel2.Controls.Add(this.txtResultComment);
            this.splitContainer2.Panel2.Controls.Add(this.rdbF);
            this.splitContainer2.Panel2.Controls.Add(this.rdbB);
            this.splitContainer2.Panel2.Controls.Add(this.rdbI);
            this.splitContainer2.Panel2.Controls.Add(this.label8);
            this.splitContainer2.Panel2.SizeChanged += new System.EventHandler(this.splitContainer2_Panel2_SizeChanged);
            this.splitContainer2.Size = new System.Drawing.Size(1105, 136);
            this.splitContainer2.SplitterDistance = 687;
            this.splitContainer2.TabIndex = 3;
            // 
            // rtxtExpectedResult
            // 
            this.rtxtExpectedResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtExpectedResult.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rtxtExpectedResult.Location = new System.Drawing.Point(0, 0);
            this.rtxtExpectedResult.Name = "rtxtExpectedResult";
            this.rtxtExpectedResult.Size = new System.Drawing.Size(687, 136);
            this.rtxtExpectedResult.TabIndex = 2;
            this.rtxtExpectedResult.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(169, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnNextTestCase
            // 
            this.btnNextTestCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextTestCase.Enabled = false;
            this.btnNextTestCase.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.next;
            this.btnNextTestCase.Location = new System.Drawing.Point(247, 2);
            this.btnNextTestCase.Name = "btnNextTestCase";
            this.btnNextTestCase.Size = new System.Drawing.Size(80, 26);
            this.btnNextTestCase.TabIndex = 8;
            this.btnNextTestCase.Text = " Next";
            this.btnNextTestCase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNextTestCase.UseVisualStyleBackColor = true;
            this.btnNextTestCase.Click += new System.EventHandler(this.btnNextTestCase_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 18);
            this.label9.TabIndex = 2;
            this.label9.Text = "Test Result : ";
            // 
            // btnSaveResult
            // 
            this.btnSaveResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveResult.Enabled = false;
            this.btnSaveResult.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.save_s;
            this.btnSaveResult.Location = new System.Drawing.Point(333, 2);
            this.btnSaveResult.Name = "btnSaveResult";
            this.btnSaveResult.Size = new System.Drawing.Size(80, 26);
            this.btnSaveResult.TabIndex = 7;
            this.btnSaveResult.Text = " Save";
            this.btnSaveResult.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveResult.UseVisualStyleBackColor = true;
            this.btnSaveResult.Click += new System.EventHandler(this.btnSaveResult_Click);
            // 
            // rdbP
            // 
            this.rdbP.AutoSize = true;
            this.rdbP.Enabled = false;
            this.rdbP.Location = new System.Drawing.Point(147, 26);
            this.rdbP.Name = "rdbP";
            this.rdbP.Size = new System.Drawing.Size(34, 22);
            this.rdbP.TabIndex = 4;
            this.rdbP.TabStop = true;
            this.rdbP.Text = "P";
            this.rdbP.UseVisualStyleBackColor = true;
            this.rdbP.CheckedChanged += new System.EventHandler(this.testResultChanged);
            // 
            // txtResultComment
            // 
            this.txtResultComment.Enabled = false;
            this.txtResultComment.Location = new System.Drawing.Point(10, 84);
            this.txtResultComment.Name = "txtResultComment";
            this.txtResultComment.Size = new System.Drawing.Size(319, 26);
            this.txtResultComment.TabIndex = 0;
            this.txtResultComment.TextChanged += new System.EventHandler(this.resultCommandChanged);
            // 
            // rdbF
            // 
            this.rdbF.AutoSize = true;
            this.rdbF.Enabled = false;
            this.rdbF.Location = new System.Drawing.Point(32, 26);
            this.rdbF.Name = "rdbF";
            this.rdbF.Size = new System.Drawing.Size(33, 22);
            this.rdbF.TabIndex = 3;
            this.rdbF.TabStop = true;
            this.rdbF.Text = "F";
            this.rdbF.UseVisualStyleBackColor = true;
            this.rdbF.CheckedChanged += new System.EventHandler(this.testResultChanged);
            // 
            // rdbB
            // 
            this.rdbB.AutoSize = true;
            this.rdbB.Enabled = false;
            this.rdbB.Location = new System.Drawing.Point(71, 26);
            this.rdbB.Name = "rdbB";
            this.rdbB.Size = new System.Drawing.Size(34, 22);
            this.rdbB.TabIndex = 6;
            this.rdbB.TabStop = true;
            this.rdbB.Text = "B";
            this.rdbB.UseVisualStyleBackColor = true;
            this.rdbB.CheckedChanged += new System.EventHandler(this.testResultChanged);
            // 
            // rdbI
            // 
            this.rdbI.AutoSize = true;
            this.rdbI.Enabled = false;
            this.rdbI.Location = new System.Drawing.Point(111, 26);
            this.rdbI.Name = "rdbI";
            this.rdbI.Size = new System.Drawing.Size(30, 22);
            this.rdbI.TabIndex = 5;
            this.rdbI.TabStop = true;
            this.rdbI.Text = "I";
            this.rdbI.UseVisualStyleBackColor = true;
            this.rdbI.CheckedChanged += new System.EventHandler(this.testResultChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 18);
            this.label8.TabIndex = 1;
            this.label8.Text = "Comment : ";
            // 
            // FormInstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FormInstall";
            this.Size = new System.Drawing.Size(1105, 735);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.pnlDescription.ResumeLayout(false);
            this.pnlAttachments.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnlConfigurations.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.pnlApplications.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_ConfigSource;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTRSPath;
        private System.Windows.Forms.Button btnOpenTestPlan;
        private System.Windows.Forms.ComboBox cmbFilteredTestCases;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslStatusLabel;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.Button btnReloadTestPlan;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.RichTextBox rtxtTPSData;
        private System.Windows.Forms.Panel pnlDescription;
        private System.Windows.Forms.RichTextBox rtxtDescription;
        private System.Windows.Forms.Panel pnlAttachments;
        private dev.jerry_h.pc_tools.CommonLibrary.ListView_DisableAutoCheckedOnDoubleClick lsvAttachment;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnDestination;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnAttachmentRemove;
        private System.Windows.Forms.Button btnAttachmentAdd;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel pnlConfigurations;
        private System.Windows.Forms.ListView lsvConfigList;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnConfigRemove;
        private System.Windows.Forms.Button btnConfigAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlApplications;
        private dev.jerry_h.pc_tools.CommonLibrary.ListView_DisableAutoCheckedOnDoubleClick lsvApplication;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAppRemove;
        private System.Windows.Forms.Button btnAppAdd;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnAttachments;
        private System.Windows.Forms.Button btnConfigurations;
        private System.Windows.Forms.Button btnApplications;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox rtxtExpectedResult;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSaveResult;
        private System.Windows.Forms.RadioButton rdbP;
        private System.Windows.Forms.TextBox txtResultComment;
        private System.Windows.Forms.RadioButton rdbF;
        private System.Windows.Forms.RadioButton rdbB;
        private System.Windows.Forms.RadioButton rdbI;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnNextTestCase;
        private System.Windows.Forms.Button button1;
    }
}

