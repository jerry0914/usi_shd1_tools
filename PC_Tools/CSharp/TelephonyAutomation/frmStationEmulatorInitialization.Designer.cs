namespace com.usi.shd1_tools.TelephonyAutomation
{
    partial class frmStationEmulatorInitialization
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("+");
            this.btnTD_SCDMA = new System.Windows.Forms.Button();
            this.btnGSM = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOtherAppSet = new System.Windows.Forms.Button();
            this.txtSysAppName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvSignalAmplitudeTable = new System.Windows.Forms.ListView();
            this.chControl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFrequency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAmplitude = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpenConfig = new System.Windows.Forms.Button();
            this.btnSignalAmpSave = new System.Windows.Forms.Button();
            this.btnSignalAmpSet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.numAdjustSignalStrengthModifyDealy = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numAdjustSignalStrengthInaccuracy = new System.Windows.Forms.NumericUpDown();
            this.numAdjustSignalStrengthTimeout = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.numAdjustSignalStrengthCheckInterval = new System.Windows.Forms.NumericUpDown();
            this.numAdjustSignalStrengthRetryLimit = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.numAdjustSignalStrengthPassingCriteria = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numDialTimeout = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numModifyDelay = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numCellPower = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetTcSettings = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthModifyDealy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthInaccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthCheckInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthRetryLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthPassingCriteria)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDialTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numModifyDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCellPower)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTD_SCDMA
            // 
            this.btnTD_SCDMA.BackColor = System.Drawing.Color.Purple;
            this.btnTD_SCDMA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTD_SCDMA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTD_SCDMA.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTD_SCDMA.ForeColor = System.Drawing.Color.White;
            this.btnTD_SCDMA.Location = new System.Drawing.Point(172, 21);
            this.btnTD_SCDMA.Name = "btnTD_SCDMA";
            this.btnTD_SCDMA.Size = new System.Drawing.Size(145, 22);
            this.btnTD_SCDMA.TabIndex = 13;
            this.btnTD_SCDMA.Text = "Set TD-SCDMA";
            this.btnTD_SCDMA.UseVisualStyleBackColor = false;
            this.btnTD_SCDMA.Click += new System.EventHandler(this.btnTD_SCDMA_Click);
            // 
            // btnGSM
            // 
            this.btnGSM.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGSM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGSM.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGSM.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGSM.ForeColor = System.Drawing.Color.White;
            this.btnGSM.Location = new System.Drawing.Point(15, 21);
            this.btnGSM.Name = "btnGSM";
            this.btnGSM.Size = new System.Drawing.Size(145, 22);
            this.btnGSM.TabIndex = 14;
            this.btnGSM.Text = "Set GSM/GPRS/WCDMA";
            this.btnGSM.UseVisualStyleBackColor = false;
            this.btnGSM.Click += new System.EventHandler(this.btnGSM_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "Others (Name) : ";
            // 
            // btnOtherAppSet
            // 
            this.btnOtherAppSet.BackColor = System.Drawing.Color.Crimson;
            this.btnOtherAppSet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOtherAppSet.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOtherAppSet.ForeColor = System.Drawing.Color.White;
            this.btnOtherAppSet.Location = new System.Drawing.Point(278, 50);
            this.btnOtherAppSet.Name = "btnOtherAppSet";
            this.btnOtherAppSet.Size = new System.Drawing.Size(39, 24);
            this.btnOtherAppSet.TabIndex = 16;
            this.btnOtherAppSet.Text = "Set";
            this.btnOtherAppSet.UseVisualStyleBackColor = false;
            this.btnOtherAppSet.Click += new System.EventHandler(this.btnOtherAppSet_Click);
            // 
            // txtSysAppName
            // 
            this.txtSysAppName.Location = new System.Drawing.Point(106, 52);
            this.txtSysAppName.Name = "txtSysAppName";
            this.txtSysAppName.Size = new System.Drawing.Size(166, 22);
            this.txtSysAppName.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvSignalAmplitudeTable);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 309);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Signal Amplitude";
            // 
            // lvSignalAmplitudeTable
            // 
            this.lvSignalAmplitudeTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chControl,
            this.chFrequency,
            this.chAmplitude});
            this.lvSignalAmplitudeTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSignalAmplitudeTable.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSignalAmplitudeTable.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvSignalAmplitudeTable.Location = new System.Drawing.Point(3, 18);
            this.lvSignalAmplitudeTable.Name = "lvSignalAmplitudeTable";
            this.lvSignalAmplitudeTable.Size = new System.Drawing.Size(319, 258);
            this.lvSignalAmplitudeTable.TabIndex = 0;
            this.lvSignalAmplitudeTable.UseCompatibleStateImageBehavior = false;
            this.lvSignalAmplitudeTable.View = System.Windows.Forms.View.Details;
            this.lvSignalAmplitudeTable.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvSignalAmplitudeTable_MouseClick);
            // 
            // chControl
            // 
            this.chControl.Text = "";
            this.chControl.Width = 24;
            // 
            // chFrequency
            // 
            this.chFrequency.Text = "Frequency (Mhz)";
            this.chFrequency.Width = 120;
            // 
            // chAmplitude
            // 
            this.chAmplitude.Text = "Amplitude(Dbm)";
            this.chAmplitude.Width = 100;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOpenConfig);
            this.panel1.Controls.Add(this.btnSignalAmpSave);
            this.panel1.Controls.Add(this.btnSignalAmpSet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 276);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 30);
            this.panel1.TabIndex = 19;
            // 
            // btnOpenConfig
            // 
            this.btnOpenConfig.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOpenConfig.BackColor = System.Drawing.SystemColors.Control;
            this.btnOpenConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenConfig.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenConfig.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenConfig.Location = new System.Drawing.Point(140, 3);
            this.btnOpenConfig.Name = "btnOpenConfig";
            this.btnOpenConfig.Size = new System.Drawing.Size(51, 24);
            this.btnOpenConfig.TabIndex = 19;
            this.btnOpenConfig.Text = "Open";
            this.btnOpenConfig.UseVisualStyleBackColor = false;
            this.btnOpenConfig.Click += new System.EventHandler(this.btnOpenConfig_Click);
            // 
            // btnSignalAmpSave
            // 
            this.btnSignalAmpSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSignalAmpSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSignalAmpSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSignalAmpSave.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignalAmpSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSignalAmpSave.Location = new System.Drawing.Point(197, 3);
            this.btnSignalAmpSave.Name = "btnSignalAmpSave";
            this.btnSignalAmpSave.Size = new System.Drawing.Size(72, 24);
            this.btnSignalAmpSave.TabIndex = 18;
            this.btnSignalAmpSave.Text = "Save to ...";
            this.btnSignalAmpSave.UseVisualStyleBackColor = false;
            this.btnSignalAmpSave.Click += new System.EventHandler(this.btnSignalAmpSave_Click);
            // 
            // btnSignalAmpSet
            // 
            this.btnSignalAmpSet.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSignalAmpSet.BackColor = System.Drawing.Color.Crimson;
            this.btnSignalAmpSet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSignalAmpSet.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignalAmpSet.ForeColor = System.Drawing.Color.White;
            this.btnSignalAmpSet.Location = new System.Drawing.Point(275, 3);
            this.btnSignalAmpSet.Name = "btnSignalAmpSet";
            this.btnSignalAmpSet.Size = new System.Drawing.Size(39, 24);
            this.btnSignalAmpSet.TabIndex = 17;
            this.btnSignalAmpSet.Text = "Set";
            this.btnSignalAmpSet.UseVisualStyleBackColor = false;
            this.btnSignalAmpSet.Click += new System.EventHandler(this.btnSignalAmpSet_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSysAppName);
            this.groupBox2.Controls.Add(this.btnTD_SCDMA);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnOtherAppSet);
            this.groupBox2.Controls.Add(this.btnGSM);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 86);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "System Application";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "config file|*.xml";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "config file|*.xml";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.btnSetTcSettings);
            this.splitContainer1.Size = new System.Drawing.Size(625, 395);
            this.splitContainer1.SplitterDistance = 325;
            this.splitContainer1.TabIndex = 18;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.numAdjustSignalStrengthModifyDealy);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.numAdjustSignalStrengthInaccuracy);
            this.groupBox4.Controls.Add(this.numAdjustSignalStrengthTimeout);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.numAdjustSignalStrengthCheckInterval);
            this.groupBox4.Controls.Add(this.numAdjustSignalStrengthRetryLimit);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.numAdjustSignalStrengthPassingCriteria);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(0, 104);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(296, 160);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Adjust DUT signal strength";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(231, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(23, 14);
            this.label18.TabIndex = 55;
            this.label18.Text = "ms";
            // 
            // numAdjustSignalStrengthModifyDealy
            // 
            this.numAdjustSignalStrengthModifyDealy.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numAdjustSignalStrengthModifyDealy.Location = new System.Drawing.Point(172, 26);
            this.numAdjustSignalStrengthModifyDealy.Maximum = new decimal(new int[] {
            12000,
            0,
            0,
            0});
            this.numAdjustSignalStrengthModifyDealy.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numAdjustSignalStrengthModifyDealy.Name = "numAdjustSignalStrengthModifyDealy";
            this.numAdjustSignalStrengthModifyDealy.Size = new System.Drawing.Size(53, 22);
            this.numAdjustSignalStrengthModifyDealy.TabIndex = 54;
            this.numAdjustSignalStrengthModifyDealy.Value = new decimal(new int[] {
            7500,
            0,
            0,
            0});
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(258, 109);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(37, 14);
            this.label17.TabIndex = 52;
            this.label17.Text = "times";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(141, 133);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 14);
            this.label12.TabIndex = 51;
            this.label12.Text = "db";
            // 
            // numAdjustSignalStrengthInaccuracy
            // 
            this.numAdjustSignalStrengthInaccuracy.Location = new System.Drawing.Point(101, 131);
            this.numAdjustSignalStrengthInaccuracy.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAdjustSignalStrengthInaccuracy.Name = "numAdjustSignalStrengthInaccuracy";
            this.numAdjustSignalStrengthInaccuracy.Size = new System.Drawing.Size(34, 22);
            this.numAdjustSignalStrengthInaccuracy.TabIndex = 50;
            this.numAdjustSignalStrengthInaccuracy.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // numAdjustSignalStrengthTimeout
            // 
            this.numAdjustSignalStrengthTimeout.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numAdjustSignalStrengthTimeout.Location = new System.Drawing.Point(172, 53);
            this.numAdjustSignalStrengthTimeout.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numAdjustSignalStrengthTimeout.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numAdjustSignalStrengthTimeout.Name = "numAdjustSignalStrengthTimeout";
            this.numAdjustSignalStrengthTimeout.Size = new System.Drawing.Size(53, 22);
            this.numAdjustSignalStrengthTimeout.TabIndex = 49;
            this.numAdjustSignalStrengthTimeout.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(260, 81);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 14);
            this.label16.TabIndex = 48;
            this.label16.Text = "ms";
            // 
            // numAdjustSignalStrengthCheckInterval
            // 
            this.numAdjustSignalStrengthCheckInterval.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numAdjustSignalStrengthCheckInterval.Location = new System.Drawing.Point(196, 79);
            this.numAdjustSignalStrengthCheckInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numAdjustSignalStrengthCheckInterval.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numAdjustSignalStrengthCheckInterval.Name = "numAdjustSignalStrengthCheckInterval";
            this.numAdjustSignalStrengthCheckInterval.Size = new System.Drawing.Size(56, 22);
            this.numAdjustSignalStrengthCheckInterval.TabIndex = 47;
            this.numAdjustSignalStrengthCheckInterval.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // numAdjustSignalStrengthRetryLimit
            // 
            this.numAdjustSignalStrengthRetryLimit.Location = new System.Drawing.Point(218, 107);
            this.numAdjustSignalStrengthRetryLimit.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAdjustSignalStrengthRetryLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAdjustSignalStrengthRetryLimit.Name = "numAdjustSignalStrengthRetryLimit";
            this.numAdjustSignalStrengthRetryLimit.Size = new System.Drawing.Size(34, 22);
            this.numAdjustSignalStrengthRetryLimit.TabIndex = 46;
            this.numAdjustSignalStrengthRetryLimit.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(200, 109);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(12, 14);
            this.label15.TabIndex = 45;
            this.label15.Text = "/";
            // 
            // numAdjustSignalStrengthPassingCriteria
            // 
            this.numAdjustSignalStrengthPassingCriteria.Location = new System.Drawing.Point(162, 107);
            this.numAdjustSignalStrengthPassingCriteria.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAdjustSignalStrengthPassingCriteria.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAdjustSignalStrengthPassingCriteria.Name = "numAdjustSignalStrengthPassingCriteria";
            this.numAdjustSignalStrengthPassingCriteria.Size = new System.Drawing.Size(34, 22);
            this.numAdjustSignalStrengthPassingCriteria.TabIndex = 44;
            this.numAdjustSignalStrengthPassingCriteria.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(23, 133);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 14);
            this.label13.TabIndex = 42;
            this.label13.Text = "Inaccuracy : ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(23, 109);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(133, 14);
            this.label14.TabIndex = 41;
            this.label14.Text = "PassingCriteria / Retry :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(231, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 14);
            this.label11.TabIndex = 40;
            this.label11.Text = "seconds";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 14);
            this.label8.TabIndex = 39;
            this.label8.Text = "checkSignalStrengthInterval : ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 14);
            this.label9.TabIndex = 38;
            this.label9.Text = "Adjust strength Timeout :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(23, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(126, 14);
            this.label10.TabIndex = 37;
            this.label10.Text = "ModifyStrengthDelay :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.numDialTimeout);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numModifyDelay);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.numCellPower);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(296, 104);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Test Cases\' default settings";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(212, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "(ms)";
            // 
            // numDialTimeout
            // 
            this.numDialTimeout.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numDialTimeout.Location = new System.Drawing.Point(136, 71);
            this.numDialTimeout.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numDialTimeout.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numDialTimeout.Name = "numDialTimeout";
            this.numDialTimeout.Size = new System.Drawing.Size(70, 22);
            this.numDialTimeout.TabIndex = 37;
            this.numDialTimeout.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 14);
            this.label6.TabIndex = 36;
            this.label6.Text = "Dial Timeout : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(212, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "(ms)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(212, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "(db)";
            // 
            // numModifyDelay
            // 
            this.numModifyDelay.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numModifyDelay.Location = new System.Drawing.Point(136, 46);
            this.numModifyDelay.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
            this.numModifyDelay.Minimum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numModifyDelay.Name = "numModifyDelay";
            this.numModifyDelay.Size = new System.Drawing.Size(70, 22);
            this.numModifyDelay.TabIndex = 33;
            this.numModifyDelay.Value = new decimal(new int[] {
            75000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 14);
            this.label3.TabIndex = 32;
            this.label3.Text = "Cell modify delay :";
            // 
            // numCellPower
            // 
            this.numCellPower.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numCellPower.Location = new System.Drawing.Point(136, 19);
            this.numCellPower.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            -2147483648});
            this.numCellPower.Minimum = new decimal(new int[] {
            1270,
            0,
            0,
            -2147418112});
            this.numCellPower.Name = "numCellPower";
            this.numCellPower.Size = new System.Drawing.Size(70, 22);
            this.numCellPower.TabIndex = 31;
            this.numCellPower.Value = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cell power :";
            // 
            // btnSetTcSettings
            // 
            this.btnSetTcSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetTcSettings.BackColor = System.Drawing.Color.Crimson;
            this.btnSetTcSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetTcSettings.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetTcSettings.ForeColor = System.Drawing.Color.White;
            this.btnSetTcSettings.Location = new System.Drawing.Point(221, 270);
            this.btnSetTcSettings.Name = "btnSetTcSettings";
            this.btnSetTcSettings.Size = new System.Drawing.Size(72, 24);
            this.btnSetTcSettings.TabIndex = 17;
            this.btnSetTcSettings.Text = "Set";
            this.btnSetTcSettings.UseVisualStyleBackColor = false;
            this.btnSetTcSettings.Click += new System.EventHandler(this.btnSetTcSettings_Click);
            // 
            // frmStationEmulatorInitialization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 395);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmStationEmulatorInitialization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmStationEmulatorInitialization";
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthModifyDealy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthInaccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthCheckInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthRetryLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustSignalStrengthPassingCriteria)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDialTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numModifyDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCellPower)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTD_SCDMA;
        private System.Windows.Forms.Button btnGSM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOtherAppSet;
        private System.Windows.Forms.TextBox txtSysAppName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvSignalAmplitudeTable;
        private System.Windows.Forms.ColumnHeader chFrequency;
        private System.Windows.Forms.ColumnHeader chAmplitude;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOpenConfig;
        private System.Windows.Forms.Button btnSignalAmpSave;
        private System.Windows.Forms.Button btnSignalAmpSet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ColumnHeader chControl;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSetTcSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numCellPower;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numModifyDelay;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numDialTimeout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numAdjustSignalStrengthRetryLimit;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numAdjustSignalStrengthPassingCriteria;
        private System.Windows.Forms.NumericUpDown numAdjustSignalStrengthTimeout;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown numAdjustSignalStrengthCheckInterval;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numAdjustSignalStrengthInaccuracy;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numAdjustSignalStrengthModifyDealy;
    }
}