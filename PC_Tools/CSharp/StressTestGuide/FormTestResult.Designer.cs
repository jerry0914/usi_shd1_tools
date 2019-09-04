namespace com.usi.shd1_tools.TestGuide
{
    partial class FormTestResult
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
            if (tdLogParse != null)
            {
                tdLogParse.Abort(5000);
                tdLogParse = null;
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTestResult));
            this.cmbDeviceList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbLogFileList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStaus = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.lblLines = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBatteryUsage = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtErrorMsgNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalMsgNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTestCaseNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtElapsedTime = new System.Windows.Forms.TextBox();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.chLineNo = new System.Windows.Forms.ColumnHeader();
            this.chText = new System.Windows.Forms.ColumnHeader();
            this.lstResultList = new System.Windows.Forms.ListView();
            this.btnPrevious = new System.Windows.Forms.ToolStripSplitButton();
            this.btnNext = new System.Windows.Forms.ToolStripSplitButton();
            this.ddlistTestCases = new System.Windows.Forms.ToolStripDropDownButton();
            this.ddlistSplitedLogPage = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnSummariesVisiable = new System.Windows.Forms.Button();
            this.btnParseSwitch = new System.Windows.Forms.Button();
            this.btnGetLog = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDeviceList
            // 
            this.cmbDeviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeviceList.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbDeviceList.FormattingEnabled = true;
            this.cmbDeviceList.Location = new System.Drawing.Point(88, 13);
            this.cmbDeviceList.Name = "cmbDeviceList";
            this.cmbDeviceList.Size = new System.Drawing.Size(333, 24);
            this.cmbDeviceList.TabIndex = 0;
            this.cmbDeviceList.SelectedIndexChanged += new System.EventHandler(this.cmbDeviceList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(27, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Device : ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSummariesVisiable);
            this.panel1.Controls.Add(this.btnParseSwitch);
            this.panel1.Controls.Add(this.cmbLogFileList);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnGetLog);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbDeviceList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 83);
            this.panel1.TabIndex = 2;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            // 
            // cmbLogFileList
            // 
            this.cmbLogFileList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogFileList.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbLogFileList.FormattingEnabled = true;
            this.cmbLogFileList.Location = new System.Drawing.Point(88, 51);
            this.cmbLogFileList.Name = "cmbLogFileList";
            this.cmbLogFileList.Size = new System.Drawing.Size(333, 24);
            this.cmbLogFileList.TabIndex = 7;
            this.cmbLogFileList.SelectedIndexChanged += new System.EventHandler(this.cmbLogFileList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(11, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Log File(s) :";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStaus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(592, 25);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStaus
            // 
            this.lblStaus.Name = "lblStaus";
            this.lblStaus.Size = new System.Drawing.Size(52, 20);
            this.lblStaus.Text = "lblStaus";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.statusStrip2);
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 529);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(592, 25);
            this.panel2.TabIndex = 6;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrevious,
            this.btnNext,
            this.ddlistTestCases,
            this.lblLines,
            this.ddlistSplitedLogPage});
            this.statusStrip2.Location = new System.Drawing.Point(388, 1);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip2.Size = new System.Drawing.Size(202, 22);
            this.statusStrip2.TabIndex = 5;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // lblLines
            // 
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(40, 17);
            this.lblLines.Text = "Line : ";
            this.lblLines.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "Start Time :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 14);
            this.label5.TabIndex = 1;
            this.label5.Text = "End Time :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(11, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 14);
            this.label6.TabIndex = 2;
            this.label6.Text = "Elapsed Time :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBatteryUsage);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtErrorMsgNo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtTotalMsgNo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtTestCaseNo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtElapsedTime);
            this.groupBox1.Controls.Add(this.txtEndTime);
            this.groupBox1.Controls.Add(this.txtStartTime);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(592, 124);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Summaries";
            // 
            // btnBatteryUsage
            // 
            this.btnBatteryUsage.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBatteryUsage.Location = new System.Drawing.Point(547, 25);
            this.btnBatteryUsage.Name = "btnBatteryUsage";
            this.btnBatteryUsage.Size = new System.Drawing.Size(23, 23);
            this.btnBatteryUsage.TabIndex = 13;
            this.btnBatteryUsage.Text = "...";
            this.btnBatteryUsage.UseVisualStyleBackColor = true;
            this.btnBatteryUsage.Click += new System.EventHandler(this.btnBatteryUsage_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(453, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 14);
            this.label10.TabIndex = 12;
            this.label10.Text = "Battery Usage :";
            // 
            // txtErrorMsgNo
            // 
            this.txtErrorMsgNo.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtErrorMsgNo.Location = new System.Drawing.Point(382, 89);
            this.txtErrorMsgNo.Name = "txtErrorMsgNo";
            this.txtErrorMsgNo.ReadOnly = true;
            this.txtErrorMsgNo.Size = new System.Drawing.Size(50, 22);
            this.txtErrorMsgNo.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(263, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 14);
            this.label9.TabIndex = 10;
            this.label9.Text = "Total messages # :";
            // 
            // txtTotalMsgNo
            // 
            this.txtTotalMsgNo.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalMsgNo.Location = new System.Drawing.Point(382, 58);
            this.txtTotalMsgNo.Name = "txtTotalMsgNo";
            this.txtTotalMsgNo.ReadOnly = true;
            this.txtTotalMsgNo.Size = new System.Drawing.Size(50, 22);
            this.txtTotalMsgNo.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(265, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 14);
            this.label8.TabIndex = 8;
            this.label8.Text = "Error messages # :";
            // 
            // txtTestCaseNo
            // 
            this.txtTestCaseNo.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTestCaseNo.Location = new System.Drawing.Point(382, 25);
            this.txtTestCaseNo.Name = "txtTestCaseNo";
            this.txtTestCaseNo.ReadOnly = true;
            this.txtTestCaseNo.Size = new System.Drawing.Size(50, 22);
            this.txtTestCaseNo.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(265, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "Test Cases # :";
            // 
            // txtElapsedTime
            // 
            this.txtElapsedTime.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtElapsedTime.Location = new System.Drawing.Point(104, 89);
            this.txtElapsedTime.Name = "txtElapsedTime";
            this.txtElapsedTime.ReadOnly = true;
            this.txtElapsedTime.Size = new System.Drawing.Size(144, 22);
            this.txtElapsedTime.TabIndex = 5;
            // 
            // txtEndTime
            // 
            this.txtEndTime.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndTime.Location = new System.Drawing.Point(104, 58);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.ReadOnly = true;
            this.txtEndTime.Size = new System.Drawing.Size(144, 22);
            this.txtEndTime.TabIndex = 4;
            // 
            // txtStartTime
            // 
            this.txtStartTime.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartTime.Location = new System.Drawing.Point(104, 25);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.ReadOnly = true;
            this.txtStartTime.Size = new System.Drawing.Size(144, 22);
            this.txtStartTime.TabIndex = 3;
            // 
            // chLineNo
            // 
            this.chLineNo.Text = "";
            this.chLineNo.Width = 50;
            // 
            // lstResultList
            // 
            this.lstResultList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLineNo,
            this.chText});
            this.lstResultList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstResultList.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstResultList.FullRowSelect = true;
            this.lstResultList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstResultList.Location = new System.Drawing.Point(0, 207);
            this.lstResultList.Name = "lstResultList";
            this.lstResultList.Size = new System.Drawing.Size(592, 322);
            this.lstResultList.TabIndex = 7;
            this.lstResultList.UseCompatibleStateImageBehavior = false;
            this.lstResultList.View = System.Windows.Forms.View.Details;
            this.lstResultList.SizeChanged += new System.EventHandler(this.listView1_SizeChanged);
            // 
            // btnPrevious
            // 
            this.btnPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrevious.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.previous;
            this.btnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(32, 20);
            this.btnPrevious.Text = "toolStripSplitButton2";
            this.btnPrevious.ToolTipText = "Previous error message";
            this.btnPrevious.Visible = false;
            this.btnPrevious.ButtonClick += new System.EventHandler(this.btnPrevious_ButtonClick);
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNext.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.next;
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(32, 20);
            this.btnNext.Text = "toolStripSplitButton1";
            this.btnNext.ToolTipText = "Next error message";
            this.btnNext.Visible = false;
            this.btnNext.ButtonClick += new System.EventHandler(this.btnNext_ButtonClick);
            // 
            // ddlistTestCases
            // 
            this.ddlistTestCases.BackColor = System.Drawing.Color.Crimson;
            this.ddlistTestCases.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ddlistTestCases.ForeColor = System.Drawing.Color.White;
            this.ddlistTestCases.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.list_20;
            this.ddlistTestCases.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddlistTestCases.Name = "ddlistTestCases";
            this.ddlistTestCases.Size = new System.Drawing.Size(185, 20);
            this.ddlistTestCases.Text = "  Check TestCase list here";
            this.ddlistTestCases.Visible = false;
            this.ddlistTestCases.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ddlistTestCases_DropDownItemClicked);
            // 
            // ddlistSplitedLogPage
            // 
            this.ddlistSplitedLogPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ddlistSplitedLogPage.Image = ((System.Drawing.Image)(resources.GetObject("ddlistSplitedLogPage.Image")));
            this.ddlistSplitedLogPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddlistSplitedLogPage.Name = "ddlistSplitedLogPage";
            this.ddlistSplitedLogPage.Size = new System.Drawing.Size(37, 20);
            this.ddlistSplitedLogPage.Text = "1~";
            this.ddlistSplitedLogPage.Visible = false;
            this.ddlistSplitedLogPage.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ddlistSplitedLogPage_DropDownItemClicked);
            // 
            // btnSummariesVisiable
            // 
            this.btnSummariesVisiable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSummariesVisiable.Font = new System.Drawing.Font("新細明體", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSummariesVisiable.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.up_1;
            this.btnSummariesVisiable.Location = new System.Drawing.Point(572, 63);
            this.btnSummariesVisiable.Name = "btnSummariesVisiable";
            this.btnSummariesVisiable.Size = new System.Drawing.Size(20, 20);
            this.btnSummariesVisiable.TabIndex = 9;
            this.btnSummariesVisiable.UseVisualStyleBackColor = true;
            this.btnSummariesVisiable.Click += new System.EventHandler(this.btnSummariesVisiable_Click);
            // 
            // btnParseSwitch
            // 
            this.btnParseSwitch.Enabled = false;
            this.btnParseSwitch.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParseSwitch.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.Start;
            this.btnParseSwitch.Location = new System.Drawing.Point(438, 48);
            this.btnParseSwitch.Name = "btnParseSwitch";
            this.btnParseSwitch.Size = new System.Drawing.Size(125, 26);
            this.btnParseSwitch.TabIndex = 8;
            this.btnParseSwitch.Text = "   Start parse";
            this.btnParseSwitch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnParseSwitch.UseVisualStyleBackColor = true;
            this.btnParseSwitch.Click += new System.EventHandler(this.btnParseSwitch_Click);
            // 
            // btnGetLog
            // 
            this.btnGetLog.Enabled = false;
            this.btnGetLog.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnGetLog.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.phone_transfer;
            this.btnGetLog.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnGetLog.Location = new System.Drawing.Point(438, 11);
            this.btnGetLog.Name = "btnGetLog";
            this.btnGetLog.Size = new System.Drawing.Size(109, 26);
            this.btnGetLog.TabIndex = 5;
            this.btnGetLog.Text = "  GET LOG";
            this.btnGetLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGetLog.UseVisualStyleBackColor = true;
            this.btnGetLog.Click += new System.EventHandler(this.btnGetLog_Click);
            // 
            // FormTestResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstResultList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormTestResult";
            this.Size = new System.Drawing.Size(592, 554);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDeviceList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGetLog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStaus;
        private System.Windows.Forms.ComboBox cmbLogFileList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnParseSwitch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripDropDownButton ddlistTestCases;
        private System.Windows.Forms.ToolStripSplitButton btnPrevious;
        private System.Windows.Forms.ToolStripSplitButton btnNext;
        private System.Windows.Forms.ToolStripDropDownButton ddlistSplitedLogPage;
        private System.Windows.Forms.ToolStripStatusLabel lblLines;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.TextBox txtTotalMsgNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTestCaseNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtElapsedTime;
        private System.Windows.Forms.TextBox txtEndTime;
        private System.Windows.Forms.Button btnBatteryUsage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtErrorMsgNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSummariesVisiable;
        private System.Windows.Forms.ColumnHeader chLineNo;
        private System.Windows.Forms.ColumnHeader chText;
        private System.Windows.Forms.ListView lstResultList;
    }
}