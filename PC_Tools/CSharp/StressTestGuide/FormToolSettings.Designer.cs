namespace com.usi.shd1_tools.TestGuide
{
    partial class FormToolSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormToolSettings));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnTRSConfigSource = new System.Windows.Forms.Button();
            this.txtTRSConfigSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtAttachmentDestination = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAttachmentSource = new System.Windows.Forms.TextBox();
            this.btnAttachmentSource = new System.Windows.Forms.Button();
            this.btnProfilePath = new System.Windows.Forms.Button();
            this.txtProfilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkKeywordFilter = new System.Windows.Forms.CheckBox();
            this.chkResult_None = new System.Windows.Forms.CheckBox();
            this.txtTestCaseKeyword = new System.Windows.Forms.TextBox();
            this.chkResult_F = new System.Windows.Forms.CheckBox();
            this.chkResult_P = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkResult_I = new System.Windows.Forms.CheckBox();
            this.chkResult_B = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPreconditionScriptSource = new System.Windows.Forms.TextBox();
            this.btnPreconditionScriptSource = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.numLogIntervalLimit = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLogFolder = new System.Windows.Forms.TextBox();
            this.btnLogFolder = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLogIntervalLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnTRSConfigSource
            // 
            this.btnTRSConfigSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTRSConfigSource.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTRSConfigSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTRSConfigSource.Image = ((System.Drawing.Image)(resources.GetObject("btnTRSConfigSource.Image")));
            this.btnTRSConfigSource.Location = new System.Drawing.Point(343, 61);
            this.btnTRSConfigSource.Name = "btnTRSConfigSource";
            this.btnTRSConfigSource.Size = new System.Drawing.Size(26, 26);
            this.btnTRSConfigSource.TabIndex = 31;
            this.btnTRSConfigSource.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnTRSConfigSource.UseVisualStyleBackColor = true;
            this.btnTRSConfigSource.Click += new System.EventHandler(this.btnTRSConfigSource_Click);
            // 
            // txtTRSConfigSource
            // 
            this.txtTRSConfigSource.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTRSConfigSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtTRSConfigSource.Location = new System.Drawing.Point(128, 64);
            this.txtTRSConfigSource.Name = "txtTRSConfigSource";
            this.txtTRSConfigSource.ReadOnly = true;
            this.txtTRSConfigSource.Size = new System.Drawing.Size(207, 21);
            this.txtTRSConfigSource.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(16, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 36);
            this.label1.TabIndex = 32;
            this.label1.Text = "Configuration \r\n   source folder :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.btnProfilePath);
            this.groupBox1.Controls.Add(this.txtProfilePath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnTRSConfigSource);
            this.groupBox1.Controls.Add(this.txtTRSConfigSource);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 290);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TRS Reader";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtAttachmentDestination);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txtAttachmentSource);
            this.groupBox5.Controls.Add(this.btnAttachmentSource);
            this.groupBox5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(13, 94);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(360, 97);
            this.groupBox5.TabIndex = 50;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Application and Attachment";
            // 
            // txtAttachmentDestination
            // 
            this.txtAttachmentDestination.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAttachmentDestination.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAttachmentDestination.Location = new System.Drawing.Point(115, 64);
            this.txtAttachmentDestination.Name = "txtAttachmentDestination";
            this.txtAttachmentDestination.Size = new System.Drawing.Size(207, 21);
            this.txtAttachmentDestination.TabIndex = 50;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(15, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 44;
            this.label5.Text = "Source folder :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DimGray;
            this.label7.Location = new System.Drawing.Point(16, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 18);
            this.label7.TabIndex = 49;
            this.label7.Text = "Destination :";
            // 
            // txtAttachmentSource
            // 
            this.txtAttachmentSource.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAttachmentSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAttachmentSource.Location = new System.Drawing.Point(115, 26);
            this.txtAttachmentSource.Name = "txtAttachmentSource";
            this.txtAttachmentSource.ReadOnly = true;
            this.txtAttachmentSource.Size = new System.Drawing.Size(207, 21);
            this.txtAttachmentSource.TabIndex = 45;
            // 
            // btnAttachmentSource
            // 
            this.btnAttachmentSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAttachmentSource.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttachmentSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAttachmentSource.Image = ((System.Drawing.Image)(resources.GetObject("btnAttachmentSource.Image")));
            this.btnAttachmentSource.Location = new System.Drawing.Point(330, 27);
            this.btnAttachmentSource.Name = "btnAttachmentSource";
            this.btnAttachmentSource.Size = new System.Drawing.Size(26, 26);
            this.btnAttachmentSource.TabIndex = 43;
            this.btnAttachmentSource.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnAttachmentSource.UseVisualStyleBackColor = true;
            this.btnAttachmentSource.Click += new System.EventHandler(this.btnAttachmentSource_Click);
            // 
            // btnProfilePath
            // 
            this.btnProfilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfilePath.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfilePath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnProfilePath.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.App;
            this.btnProfilePath.Location = new System.Drawing.Point(343, 21);
            this.btnProfilePath.Name = "btnProfilePath";
            this.btnProfilePath.Size = new System.Drawing.Size(26, 26);
            this.btnProfilePath.TabIndex = 48;
            this.btnProfilePath.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnProfilePath.UseVisualStyleBackColor = true;
            this.btnProfilePath.Click += new System.EventHandler(this.btnProfilePath_Click);
            // 
            // txtProfilePath
            // 
            this.txtProfilePath.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProfilePath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtProfilePath.Location = new System.Drawing.Point(128, 25);
            this.txtProfilePath.Name = "txtProfilePath";
            this.txtProfilePath.ReadOnly = true;
            this.txtProfilePath.Size = new System.Drawing.Size(207, 21);
            this.txtProfilePath.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(16, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 18);
            this.label3.TabIndex = 46;
            this.label3.Text = "Profile app :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkKeywordFilter);
            this.groupBox2.Controls.Add(this.chkResult_None);
            this.groupBox2.Controls.Add(this.txtTestCaseKeyword);
            this.groupBox2.Controls.Add(this.chkResult_F);
            this.groupBox2.Controls.Add(this.chkResult_P);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.chkResult_I);
            this.groupBox2.Controls.Add(this.chkResult_B);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 195);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 86);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test case filter :";
            // 
            // chkKeywordFilter
            // 
            this.chkKeywordFilter.AutoSize = true;
            this.chkKeywordFilter.Checked = true;
            this.chkKeywordFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKeywordFilter.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKeywordFilter.ForeColor = System.Drawing.Color.DimGray;
            this.chkKeywordFilter.Location = new System.Drawing.Point(19, 25);
            this.chkKeywordFilter.Name = "chkKeywordFilter";
            this.chkKeywordFilter.Size = new System.Drawing.Size(89, 22);
            this.chkKeywordFilter.TabIndex = 34;
            this.chkKeywordFilter.Text = "Keyword :";
            this.chkKeywordFilter.UseVisualStyleBackColor = true;
            this.chkKeywordFilter.CheckedChanged += new System.EventHandler(this.chkKeywordFilter_CheckedChanged);
            // 
            // chkResult_None
            // 
            this.chkResult_None.AutoSize = true;
            this.chkResult_None.Checked = true;
            this.chkResult_None.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkResult_None.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkResult_None.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkResult_None.Location = new System.Drawing.Point(273, 59);
            this.chkResult_None.Name = "chkResult_None";
            this.chkResult_None.Size = new System.Drawing.Size(61, 22);
            this.chkResult_None.TabIndex = 41;
            this.chkResult_None.Text = "None";
            this.chkResult_None.UseVisualStyleBackColor = true;
            // 
            // txtTestCaseKeyword
            // 
            this.txtTestCaseKeyword.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTestCaseKeyword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtTestCaseKeyword.Location = new System.Drawing.Point(115, 25);
            this.txtTestCaseKeyword.Name = "txtTestCaseKeyword";
            this.txtTestCaseKeyword.Size = new System.Drawing.Size(207, 21);
            this.txtTestCaseKeyword.TabIndex = 35;
            // 
            // chkResult_F
            // 
            this.chkResult_F.AutoSize = true;
            this.chkResult_F.Checked = true;
            this.chkResult_F.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkResult_F.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkResult_F.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkResult_F.Location = new System.Drawing.Point(114, 59);
            this.chkResult_F.Name = "chkResult_F";
            this.chkResult_F.Size = new System.Drawing.Size(34, 22);
            this.chkResult_F.TabIndex = 40;
            this.chkResult_F.Text = "F";
            this.chkResult_F.UseVisualStyleBackColor = true;
            // 
            // chkResult_P
            // 
            this.chkResult_P.AutoSize = true;
            this.chkResult_P.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkResult_P.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkResult_P.Location = new System.Drawing.Point(232, 59);
            this.chkResult_P.Name = "chkResult_P";
            this.chkResult_P.Size = new System.Drawing.Size(35, 22);
            this.chkResult_P.TabIndex = 36;
            this.chkResult_P.Text = "P";
            this.chkResult_P.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(16, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 18);
            this.label6.TabIndex = 39;
            this.label6.Text = "Test Result :";
            // 
            // chkResult_I
            // 
            this.chkResult_I.AutoSize = true;
            this.chkResult_I.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkResult_I.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkResult_I.Location = new System.Drawing.Point(195, 59);
            this.chkResult_I.Name = "chkResult_I";
            this.chkResult_I.Size = new System.Drawing.Size(31, 22);
            this.chkResult_I.TabIndex = 37;
            this.chkResult_I.Text = "I";
            this.chkResult_I.UseVisualStyleBackColor = true;
            // 
            // chkResult_B
            // 
            this.chkResult_B.AutoSize = true;
            this.chkResult_B.Checked = true;
            this.chkResult_B.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkResult_B.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkResult_B.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkResult_B.Location = new System.Drawing.Point(154, 59);
            this.chkResult_B.Name = "chkResult_B";
            this.chkResult_B.Size = new System.Drawing.Size(35, 22);
            this.chkResult_B.TabIndex = 38;
            this.chkResult_B.Text = "B";
            this.chkResult_B.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtPreconditionScriptSource);
            this.groupBox3.Controls.Add(this.btnPreconditionScriptSource);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Blue;
            this.groupBox3.Location = new System.Drawing.Point(0, 290);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(376, 59);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pre-condition";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(16, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 18);
            this.label2.TabIndex = 9;
            this.label2.Text = "Pre-setting script :";
            // 
            // txtPreconditionScriptSource
            // 
            this.txtPreconditionScriptSource.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreconditionScriptSource.Location = new System.Drawing.Point(142, 26);
            this.txtPreconditionScriptSource.Name = "txtPreconditionScriptSource";
            this.txtPreconditionScriptSource.ReadOnly = true;
            this.txtPreconditionScriptSource.Size = new System.Drawing.Size(193, 21);
            this.txtPreconditionScriptSource.TabIndex = 7;
            // 
            // btnPreconditionScriptSource
            // 
            this.btnPreconditionScriptSource.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreconditionScriptSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPreconditionScriptSource.Image = ((System.Drawing.Image)(resources.GetObject("btnPreconditionScriptSource.Image")));
            this.btnPreconditionScriptSource.Location = new System.Drawing.Point(343, 22);
            this.btnPreconditionScriptSource.Name = "btnPreconditionScriptSource";
            this.btnPreconditionScriptSource.Size = new System.Drawing.Size(26, 26);
            this.btnPreconditionScriptSource.TabIndex = 8;
            this.btnPreconditionScriptSource.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnPreconditionScriptSource.UseVisualStyleBackColor = true;
            this.btnPreconditionScriptSource.Click += new System.EventHandler(this.btnPreconditionScriptSource_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.numLogIntervalLimit);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtLogFolder);
            this.groupBox4.Controls.Add(this.btnLogFolder);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Blue;
            this.groupBox4.Location = new System.Drawing.Point(0, 349);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(376, 93);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Test Result";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(203, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 14);
            this.label12.TabIndex = 19;
            this.label12.Text = "mins";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(16, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 18);
            this.label11.TabIndex = 18;
            this.label11.Text = "Max log interval :";
            // 
            // numLogIntervalLimit
            // 
            this.numLogIntervalLimit.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numLogIntervalLimit.Location = new System.Drawing.Point(142, 56);
            this.numLogIntervalLimit.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numLogIntervalLimit.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numLogIntervalLimit.Name = "numLogIntervalLimit";
            this.numLogIntervalLimit.Size = new System.Drawing.Size(54, 26);
            this.numLogIntervalLimit.TabIndex = 17;
            this.numLogIntervalLimit.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(16, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Log Folder:";
            // 
            // txtLogFolder
            // 
            this.txtLogFolder.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogFolder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtLogFolder.Location = new System.Drawing.Point(98, 26);
            this.txtLogFolder.Name = "txtLogFolder";
            this.txtLogFolder.ReadOnly = true;
            this.txtLogFolder.Size = new System.Drawing.Size(237, 21);
            this.txtLogFolder.TabIndex = 7;
            // 
            // btnLogFolder
            // 
            this.btnLogFolder.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnLogFolder.Image")));
            this.btnLogFolder.Location = new System.Drawing.Point(343, 22);
            this.btnLogFolder.Name = "btnLogFolder";
            this.btnLogFolder.Size = new System.Drawing.Size(26, 26);
            this.btnLogFolder.TabIndex = 8;
            this.btnLogFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnLogFolder.UseVisualStyleBackColor = true;
            this.btnLogFolder.Click += new System.EventHandler(this.btnLogFolder_Click);
            // 
            // FormToolSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 478);
            this.ControlBox = true;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(392, 388);
            this.Name = "FormToolSettings";
            this.Text = "Settings";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLogIntervalLimit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnTRSConfigSource;
        private System.Windows.Forms.TextBox txtTRSConfigSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTestCaseKeyword;
        private System.Windows.Forms.CheckBox chkKeywordFilter;
        private System.Windows.Forms.CheckBox chkResult_None;
        private System.Windows.Forms.CheckBox chkResult_F;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkResult_B;
        private System.Windows.Forms.CheckBox chkResult_I;
        private System.Windows.Forms.CheckBox chkResult_P;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPreconditionScriptSource;
        private System.Windows.Forms.Button btnPreconditionScriptSource;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLogFolder;
        private System.Windows.Forms.Button btnLogFolder;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numLogIntervalLimit;
        private System.Windows.Forms.Button btnAttachmentSource;
        private System.Windows.Forms.TextBox txtAttachmentSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnProfilePath;
        private System.Windows.Forms.TextBox txtProfilePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtAttachmentDestination;
    }
}