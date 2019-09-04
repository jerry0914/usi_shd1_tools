namespace com.usi.shd1_tools.TelephonyAutomation
{
    partial class frmTelephonyAutomation
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
            com.usi.shd1_tools.CommonLibrary.Logger.WriteLog("End", "Application is closed manually.", true);
            if (procedureProcessor != null)
            {
                procedureProcessor.Dispose();
            }
            stopDutMonitor();
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("01. DUT calls station emulator and DUT hangs up (GSM 900)");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("02. DUT calls station emulator  and emulator hangs up (GSM 900)");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("03. Station emulator calls DUT and DUT hangs up (GSM 900)");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("04. Station emulator calls DUT and emulator hangs up (GSM 900)");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("05. DUT calls station emulator and DUT hangs up (DCS 1800)");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("06. DUT calls station emulator  and emulator hangs up (DCS 1800)");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("07. Station emulator calls DUT and DUT hangs up (DCS 1800)");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("08. Station emulator calls DUT and emulator hangs up (DCS 1800)");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("09. DUT calls station emulator and DUT hangs up (UMTS 900)");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("10. DUT calls station emulator  and emulator hangs up UMTS 900)");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("11. Station emulator calls DUT and DUT hangs up (UMTS 900)");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("12. Station emulator calls DUT and emulator hangs up (UMTS 900)");
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("13. DUT calls station emulator and DUT hangs up (UMTS 2100)");
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("14. DUT calls station emulator  and emulator hangs up UMTS 2100)");
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("15. Station emulator calls DUT and DUT hangs up (UMTS 2100)");
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem("16. Station emulator calls DUT and emulator hangs up (UMTS 2100)");
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem("17. DUT calls station emulator and DUT hangs up (TD-SCDMA B34)");
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem("18. DUT calls station emulator  and emulator hangs up (TD-SCDMA B34)");
            System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem("19. Station emulator calls DUT and DUT hangs up (TD-SCDMA B34)");
            System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem("20. Station emulator calls DUT and emulator hangs up (TD-SCDMA B34)");
            System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem("21. DUT calls station emulator and DUT hangs up (TD-SCDMA B39)");
            System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem("22. DUT calls station emulator  and emulator hangs up TD-SCDMA B39)");
            System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem("23. Station emulator calls DUT and DUT hangs up (TD-SCDMA B39)");
            System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem("24. Station emulator calls DUT and emulator hangs up (TD-SCDMA B39)");
            System.Windows.Forms.ListViewItem listViewItem25 = new System.Windows.Forms.ListViewItem("25. Establish a phone connection for a long time.(GSM 900)");
            System.Windows.Forms.ListViewItem listViewItem26 = new System.Windows.Forms.ListViewItem("26. Establish a phone connection for a long time.(DCS 1800)");
            System.Windows.Forms.ListViewItem listViewItem27 = new System.Windows.Forms.ListViewItem("27. Establish a phone connection for a long time.(UMTS 900)");
            System.Windows.Forms.ListViewItem listViewItem28 = new System.Windows.Forms.ListViewItem("28. Establish a phone connection for a long time.(UMTS 2100)");
            System.Windows.Forms.ListViewItem listViewItem29 = new System.Windows.Forms.ListViewItem("29. Establish a phone connection for a long time.(TD-SCDMA B34");
            System.Windows.Forms.ListViewItem listViewItem30 = new System.Windows.Forms.ListViewItem("30.Establish a phone connection for a long time.(TD-SCDMA B39)");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTelephonyAutomation));
            this.lsvTestcases = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbGpib = new System.Windows.Forms.GroupBox();
            this.numGPIB2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGpibConnect = new System.Windows.Forms.Button();
            this.numGPIB1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numBoard = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDutStatus = new System.Windows.Forms.Button();
            this.cmbDeviceList = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtTcDescripition = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressMain = new System.Windows.Forms.ToolStripProgressBar();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblChekPointResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTcResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lsvLiveLog = new System.Windows.Forms.ListView();
            this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLogLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRun = new System.Windows.Forms.Button();
            this.rdbGpib = new System.Windows.Forms.RadioButton();
            this.rdbVisa = new System.Windows.Forms.RadioButton();
            this.gbVisa = new System.Windows.Forms.GroupBox();
            this.txtVisaName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnVisaConnect = new System.Windows.Forms.Button();
            this.btnStationEmulatorDebug = new System.Windows.Forms.Button();
            this.btnDutDebug = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStationEmulatorState = new System.Windows.Forms.TextBox();
            this.txtDutPhoneState = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCellPower = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBand = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnTcDebug = new System.Windows.Forms.Button();
            this.btnClearLiveLog = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkInfinityMode = new System.Windows.Forms.CheckBox();
            this.gbGpib.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGPIB2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGPIB1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBoard)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.gbVisa.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvTestcases
            // 
            this.lsvTestcases.CheckBoxes = true;
            this.lsvTestcases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvTestcases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvTestcases.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvTestcases.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.StateImageIndex = 0;
            listViewItem1.ToolTipText = "DUT calls station emulator and DUT hangs up (GSM 900)";
            listViewItem2.StateImageIndex = 0;
            listViewItem2.ToolTipText = "DUT calls station emulator  and emulator hangs up (GSM 900)";
            listViewItem3.StateImageIndex = 0;
            listViewItem3.ToolTipText = "Station emulator calls DUT and DUT hangs up (GSM 900)";
            listViewItem4.StateImageIndex = 0;
            listViewItem4.ToolTipText = "Station emulator calls DUT and emulator hangs up (GSM 900)";
            listViewItem5.StateImageIndex = 0;
            listViewItem5.ToolTipText = "DUT calls station emulator and DUT hangs up (DCS 1800)";
            listViewItem6.StateImageIndex = 0;
            listViewItem6.ToolTipText = "DUT calls station emulator  and emulator hangs up (DCS 1800)";
            listViewItem7.StateImageIndex = 0;
            listViewItem7.ToolTipText = "Station emulator calls DUT and DUT hangs up (DCS 1800)";
            listViewItem8.StateImageIndex = 0;
            listViewItem8.ToolTipText = "Station emulator calls DUT and emulator hangs up (DCS 1800)";
            listViewItem9.StateImageIndex = 0;
            listViewItem9.ToolTipText = "DUT calls station emulator and DUT hangs up (UMTS 900)";
            listViewItem10.StateImageIndex = 0;
            listViewItem10.ToolTipText = "DUT calls station emulator  and emulator hangs up UMTS 900)";
            listViewItem11.StateImageIndex = 0;
            listViewItem11.ToolTipText = "Station emulator calls DUT and DUT hangs up (UMTS 900)";
            listViewItem12.StateImageIndex = 0;
            listViewItem12.ToolTipText = "Station emulator calls DUT and emulator hangs up (UMTS 900)";
            listViewItem13.StateImageIndex = 0;
            listViewItem13.ToolTipText = "DUT calls station emulator and DUT hangs up (UMTS 2100)";
            listViewItem14.StateImageIndex = 0;
            listViewItem14.ToolTipText = "DUT calls station emulator  and emulator hangs up UMTS 2100)";
            listViewItem15.StateImageIndex = 0;
            listViewItem15.ToolTipText = "Station emulator calls DUT and DUT hangs up (UMTS 2100)";
            listViewItem16.StateImageIndex = 0;
            listViewItem16.ToolTipText = "Station emulator calls DUT and emulator hangs up (UMTS 2100)";
            listViewItem17.StateImageIndex = 0;
            listViewItem17.ToolTipText = "DUT calls station emulator and DUT hangs up (TD-SCDMA B34)";
            listViewItem18.StateImageIndex = 0;
            listViewItem18.ToolTipText = "DUT calls station emulator  and emulator hangs up TD-SCDMA B34)";
            listViewItem19.StateImageIndex = 0;
            listViewItem19.ToolTipText = "Station emulator calls DUT and DUT hangs up (TD-SCDMA B34)";
            listViewItem20.StateImageIndex = 0;
            listViewItem21.StateImageIndex = 0;
            listViewItem21.ToolTipText = "DUT calls station emulator and DUT hangs up (TD-SCDMA B39)";
            listViewItem22.StateImageIndex = 0;
            listViewItem23.StateImageIndex = 0;
            listViewItem23.ToolTipText = "Station emulator calls DUT and DUT hangs up (TD-SCDMA B39)";
            listViewItem24.StateImageIndex = 0;
            listViewItem25.StateImageIndex = 0;
            listViewItem25.ToolTipText = "Establish a phone connection for a long time.(GSM 900)";
            listViewItem26.StateImageIndex = 0;
            listViewItem26.ToolTipText = "Establish a phone connection for a long time.(DCS 1800)";
            listViewItem27.StateImageIndex = 0;
            listViewItem27.ToolTipText = "Establish a phone connection for a long time.(UMTS 900) ";
            listViewItem28.StateImageIndex = 0;
            listViewItem28.ToolTipText = "Establish a phone connection for a long time.(UMTS 2100)";
            listViewItem29.StateImageIndex = 0;
            listViewItem29.ToolTipText = "Establish a phone connection for a long time.(TD-SCDMA B34)";
            listViewItem30.StateImageIndex = 0;
            listViewItem30.ToolTipText = "Establish a phone connection for a long time.(TD-SCDMA B39)";
            this.lsvTestcases.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20,
            listViewItem21,
            listViewItem22,
            listViewItem23,
            listViewItem24,
            listViewItem25,
            listViewItem26,
            listViewItem27,
            listViewItem28,
            listViewItem29,
            listViewItem30});
            this.lsvTestcases.Location = new System.Drawing.Point(0, 0);
            this.lsvTestcases.Name = "lsvTestcases";
            this.lsvTestcases.ShowItemToolTips = true;
            this.lsvTestcases.Size = new System.Drawing.Size(476, 242);
            this.lsvTestcases.TabIndex = 0;
            this.lsvTestcases.UseCompatibleStateImageBehavior = false;
            this.lsvTestcases.View = System.Windows.Forms.View.Details;
            this.lsvTestcases.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvTestcases_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 400;
            // 
            // gbGpib
            // 
            this.gbGpib.Controls.Add(this.numGPIB2);
            this.gbGpib.Controls.Add(this.label3);
            this.gbGpib.Controls.Add(this.btnGpibConnect);
            this.gbGpib.Controls.Add(this.numGPIB1);
            this.gbGpib.Controls.Add(this.label2);
            this.gbGpib.Controls.Add(this.numBoard);
            this.gbGpib.Controls.Add(this.label1);
            this.gbGpib.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGpib.Location = new System.Drawing.Point(15, 41);
            this.gbGpib.Name = "gbGpib";
            this.gbGpib.Size = new System.Drawing.Size(218, 119);
            this.gbGpib.TabIndex = 1;
            this.gbGpib.TabStop = false;
            this.gbGpib.Text = "GPIB Settings";
            // 
            // numGPIB2
            // 
            this.numGPIB2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numGPIB2.Location = new System.Drawing.Point(166, 57);
            this.numGPIB2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numGPIB2.Name = "numGPIB2";
            this.numGPIB2.Size = new System.Drawing.Size(39, 23);
            this.numGPIB2.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(107, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "GPIB # 2:";
            // 
            // btnGpibConnect
            // 
            this.btnGpibConnect.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGpibConnect.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnGpibConnect.Location = new System.Drawing.Point(6, 86);
            this.btnGpibConnect.Name = "btnGpibConnect";
            this.btnGpibConnect.Size = new System.Drawing.Size(200, 27);
            this.btnGpibConnect.TabIndex = 24;
            this.btnGpibConnect.Text = "Click here to connect";
            this.btnGpibConnect.UseVisualStyleBackColor = true;
            this.btnGpibConnect.Click += new System.EventHandler(this.btnGpibConnect_Click);
            // 
            // numGPIB1
            // 
            this.numGPIB1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numGPIB1.Location = new System.Drawing.Point(166, 28);
            this.numGPIB1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numGPIB1.Name = "numGPIB1";
            this.numGPIB1.Size = new System.Drawing.Size(39, 23);
            this.numGPIB1.TabIndex = 28;
            this.numGPIB1.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(107, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 27;
            this.label2.Text = "GPIB # 1:";
            // 
            // numBoard
            // 
            this.numBoard.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBoard.Location = new System.Drawing.Point(55, 28);
            this.numBoard.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numBoard.Name = "numBoard";
            this.numBoard.Size = new System.Drawing.Size(39, 23);
            this.numBoard.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 25;
            this.label1.Text = "Board # :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDutStatus);
            this.groupBox2.Controls.Add(this.cmbDeviceList);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(239, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(574, 55);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Android device";
            // 
            // btnDutStatus
            // 
            this.btnDutStatus.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDutStatus.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnDutStatus.Location = new System.Drawing.Point(386, 22);
            this.btnDutStatus.Name = "btnDutStatus";
            this.btnDutStatus.Size = new System.Drawing.Size(182, 23);
            this.btnDutStatus.TabIndex = 26;
            this.btnDutStatus.UseVisualStyleBackColor = true;
            this.btnDutStatus.Click += new System.EventHandler(this.btnDutStatus_Click);
            // 
            // cmbDeviceList
            // 
            this.cmbDeviceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeviceList.FormattingEnabled = true;
            this.cmbDeviceList.Location = new System.Drawing.Point(31, 22);
            this.cmbDeviceList.Name = "cmbDeviceList";
            this.cmbDeviceList.Size = new System.Drawing.Size(349, 23);
            this.cmbDeviceList.TabIndex = 0;
            this.cmbDeviceList.SelectedIndexChanged += new System.EventHandler(this.cmbDeviceList_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.splitContainer1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 177);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(827, 264);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Test cases";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 19);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lsvTestcases);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtTcDescripition);
            this.splitContainer1.Size = new System.Drawing.Size(821, 242);
            this.splitContainer1.SplitterDistance = 476;
            this.splitContainer1.TabIndex = 4;
            // 
            // txtTcDescripition
            // 
            this.txtTcDescripition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTcDescripition.Location = new System.Drawing.Point(0, 0);
            this.txtTcDescripition.Multiline = true;
            this.txtTcDescripition.Name = "txtTcDescripition";
            this.txtTcDescripition.ReadOnly = true;
            this.txtTcDescripition.Size = new System.Drawing.Size(341, 242);
            this.txtTcDescripition.TabIndex = 4;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.statusStrip1);
            this.groupBox4.Controls.Add(this.lsvLiveLog);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(0, 441);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(827, 301);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Live Log";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressMain,
            this.lblProgress,
            this.lblChekPointResult,
            this.lblTcResult,
            this.lblInfo});
            this.statusStrip1.Location = new System.Drawing.Point(3, 276);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(821, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressMain
            // 
            this.progressMain.Name = "progressMain";
            this.progressMain.Size = new System.Drawing.Size(180, 16);
            this.progressMain.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(12, 17);
            this.lblProgress.Text = "/";
            this.lblProgress.Visible = false;
            // 
            // lblChekPointResult
            // 
            this.lblChekPointResult.Name = "lblChekPointResult";
            this.lblChekPointResult.Size = new System.Drawing.Size(0, 17);
            // 
            // lblTcResult
            // 
            this.lblTcResult.Name = "lblTcResult";
            this.lblTcResult.Size = new System.Drawing.Size(0, 17);
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // lsvLiveLog
            // 
            this.lsvLiveLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTime,
            this.chLogLevel,
            this.chTag,
            this.chMessage});
            this.lsvLiveLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvLiveLog.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvLiveLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvLiveLog.Location = new System.Drawing.Point(3, 19);
            this.lsvLiveLog.Name = "lsvLiveLog";
            this.lsvLiveLog.Size = new System.Drawing.Size(821, 279);
            this.lsvLiveLog.TabIndex = 1;
            this.lsvLiveLog.UseCompatibleStateImageBehavior = false;
            this.lsvLiveLog.View = System.Windows.Forms.View.Details;
            this.lsvLiveLog.Resize += new System.EventHandler(this.lsvLiveLog_Resize);
            // 
            // chTime
            // 
            this.chTime.Text = "Time";
            this.chTime.Width = 157;
            // 
            // chLogLevel
            // 
            this.chLogLevel.Text = "Level";
            this.chLogLevel.Width = 47;
            // 
            // chTag
            // 
            this.chTag.Text = "Tag";
            this.chTag.Width = 76;
            // 
            // chMessage
            // 
            this.chMessage.Text = "Message";
            this.chMessage.Width = 497;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnRun.Location = new System.Drawing.Point(728, 150);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(85, 25);
            this.btnRun.TabIndex = 25;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            this.btnRun.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnRun_KeyDown);
            // 
            // rdbGpib
            // 
            this.rdbGpib.AutoSize = true;
            this.rdbGpib.Checked = true;
            this.rdbGpib.Location = new System.Drawing.Point(15, 14);
            this.rdbGpib.Name = "rdbGpib";
            this.rdbGpib.Size = new System.Drawing.Size(51, 19);
            this.rdbGpib.TabIndex = 26;
            this.rdbGpib.TabStop = true;
            this.rdbGpib.Text = "GPIB";
            this.rdbGpib.UseVisualStyleBackColor = true;
            this.rdbGpib.CheckedChanged += new System.EventHandler(this.rdbGpib_CheckedChanged);
            // 
            // rdbVisa
            // 
            this.rdbVisa.AutoSize = true;
            this.rdbVisa.Location = new System.Drawing.Point(72, 14);
            this.rdbVisa.Name = "rdbVisa";
            this.rdbVisa.Size = new System.Drawing.Size(49, 19);
            this.rdbVisa.TabIndex = 27;
            this.rdbVisa.Text = "VISA";
            this.rdbVisa.UseVisualStyleBackColor = true;
            this.rdbVisa.CheckedChanged += new System.EventHandler(this.rdbGpib_CheckedChanged);
            // 
            // gbVisa
            // 
            this.gbVisa.Controls.Add(this.txtVisaName);
            this.gbVisa.Controls.Add(this.label4);
            this.gbVisa.Controls.Add(this.btnVisaConnect);
            this.gbVisa.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbVisa.Location = new System.Drawing.Point(15, 41);
            this.gbVisa.Name = "gbVisa";
            this.gbVisa.Size = new System.Drawing.Size(218, 119);
            this.gbVisa.TabIndex = 28;
            this.gbVisa.TabStop = false;
            this.gbVisa.Text = "VISA Settings";
            this.gbVisa.Visible = false;
            // 
            // txtVisaName
            // 
            this.txtVisaName.Location = new System.Drawing.Point(60, 25);
            this.txtVisaName.Name = "txtVisaName";
            this.txtVisaName.Size = new System.Drawing.Size(149, 23);
            this.txtVisaName.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 25;
            this.label4.Text = "Name : ";
            // 
            // btnVisaConnect
            // 
            this.btnVisaConnect.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVisaConnect.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnVisaConnect.Location = new System.Drawing.Point(6, 86);
            this.btnVisaConnect.Name = "btnVisaConnect";
            this.btnVisaConnect.Size = new System.Drawing.Size(200, 27);
            this.btnVisaConnect.TabIndex = 24;
            this.btnVisaConnect.Text = "Click here to connect";
            this.btnVisaConnect.UseVisualStyleBackColor = true;
            this.btnVisaConnect.Click += new System.EventHandler(this.btnVisaConnect_Click);
            // 
            // btnStationEmulatorDebug
            // 
            this.btnStationEmulatorDebug.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStationEmulatorDebug.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnStationEmulatorDebug.Location = new System.Drawing.Point(728, 95);
            this.btnStationEmulatorDebug.Name = "btnStationEmulatorDebug";
            this.btnStationEmulatorDebug.Size = new System.Drawing.Size(85, 23);
            this.btnStationEmulatorDebug.TabIndex = 26;
            this.btnStationEmulatorDebug.Text = "StationEmulator Debug";
            this.btnStationEmulatorDebug.UseVisualStyleBackColor = true;
            this.btnStationEmulatorDebug.Visible = false;
            this.btnStationEmulatorDebug.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnDutDebug
            // 
            this.btnDutDebug.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDutDebug.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnDutDebug.Location = new System.Drawing.Point(728, 70);
            this.btnDutDebug.Name = "btnDutDebug";
            this.btnDutDebug.Size = new System.Drawing.Size(85, 23);
            this.btnDutDebug.TabIndex = 29;
            this.btnDutDebug.Text = "DUT Debug";
            this.btnDutDebug.UseVisualStyleBackColor = true;
            this.btnDutDebug.Visible = false;
            this.btnDutDebug.Click += new System.EventHandler(this.btnDutTest_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 15);
            this.label6.TabIndex = 31;
            this.label6.Text = "State : ";
            // 
            // txtStationEmulatorState
            // 
            this.txtStationEmulatorState.BackColor = System.Drawing.Color.Gray;
            this.txtStationEmulatorState.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStationEmulatorState.ForeColor = System.Drawing.Color.White;
            this.txtStationEmulatorState.Location = new System.Drawing.Point(66, 13);
            this.txtStationEmulatorState.Name = "txtStationEmulatorState";
            this.txtStationEmulatorState.ReadOnly = true;
            this.txtStationEmulatorState.Size = new System.Drawing.Size(135, 26);
            this.txtStationEmulatorState.TabIndex = 32;
            this.txtStationEmulatorState.Text = "Unknow";
            this.txtStationEmulatorState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDutPhoneState
            // 
            this.txtDutPhoneState.BackColor = System.Drawing.Color.Gray;
            this.txtDutPhoneState.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDutPhoneState.ForeColor = System.Drawing.Color.White;
            this.txtDutPhoneState.Location = new System.Drawing.Point(60, 13);
            this.txtDutPhoneState.Name = "txtDutPhoneState";
            this.txtDutPhoneState.ReadOnly = true;
            this.txtDutPhoneState.Size = new System.Drawing.Size(99, 26);
            this.txtDutPhoneState.TabIndex = 33;
            this.txtDutPhoneState.Text = "Unknow";
            this.txtDutPhoneState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCellPower);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtBand);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtStationEmulatorState);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(239, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 95);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instrument";
            // 
            // txtCellPower
            // 
            this.txtCellPower.BackColor = System.Drawing.Color.RoyalBlue;
            this.txtCellPower.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCellPower.ForeColor = System.Drawing.Color.White;
            this.txtCellPower.Location = new System.Drawing.Point(94, 66);
            this.txtCellPower.Name = "txtCellPower";
            this.txtCellPower.ReadOnly = true;
            this.txtCellPower.Size = new System.Drawing.Size(107, 26);
            this.txtCellPower.TabIndex = 37;
            this.txtCellPower.Text = "-0 db";
            this.txtCellPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 15);
            this.label8.TabIndex = 36;
            this.label8.Text = "Cell Power : ";
            // 
            // txtBand
            // 
            this.txtBand.BackColor = System.Drawing.Color.RoyalBlue;
            this.txtBand.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBand.ForeColor = System.Drawing.Color.White;
            this.txtBand.Location = new System.Drawing.Point(66, 40);
            this.txtBand.Name = "txtBand";
            this.txtBand.ReadOnly = true;
            this.txtBand.Size = new System.Drawing.Size(135, 26);
            this.txtBand.TabIndex = 35;
            this.txtBand.Text = "Unknow";
            this.txtBand.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 15);
            this.label7.TabIndex = 34;
            this.label7.Text = "Band :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 15);
            this.label5.TabIndex = 33;
            this.label5.Text = "State :";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txtDutPhoneState);
            this.groupBox5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(454, 75);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(165, 95);
            this.groupBox5.TabIndex = 35;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "DUT";
            // 
            // btnTcDebug
            // 
            this.btnTcDebug.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTcDebug.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnTcDebug.Location = new System.Drawing.Point(728, 120);
            this.btnTcDebug.Name = "btnTcDebug";
            this.btnTcDebug.Size = new System.Drawing.Size(85, 23);
            this.btnTcDebug.TabIndex = 36;
            this.btnTcDebug.Text = "TC Debug";
            this.btnTcDebug.UseVisualStyleBackColor = true;
            this.btnTcDebug.Visible = false;
            this.btnTcDebug.Click += new System.EventHandler(this.btnTcDebug_Click);
            // 
            // btnClearLiveLog
            // 
            this.btnClearLiveLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLiveLog.Location = new System.Drawing.Point(763, 437);
            this.btnClearLiveLog.Name = "btnClearLiveLog";
            this.btnClearLiveLog.Size = new System.Drawing.Size(55, 22);
            this.btnClearLiveLog.TabIndex = 2;
            this.btnClearLiveLog.Text = "Clear";
            this.btnClearLiveLog.UseVisualStyleBackColor = true;
            this.btnClearLiveLog.Click += new System.EventHandler(this.btnCleanLiveLog_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkInfinityMode);
            this.panel1.Controls.Add(this.rdbGpib);
            this.panel1.Controls.Add(this.btnTcDebug);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Controls.Add(this.btnDutDebug);
            this.panel1.Controls.Add(this.rdbVisa);
            this.panel1.Controls.Add(this.btnStationEmulatorDebug);
            this.panel1.Controls.Add(this.gbGpib);
            this.panel1.Controls.Add(this.gbVisa);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(827, 177);
            this.panel1.TabIndex = 37;
            // 
            // chkInfinityMode
            // 
            this.chkInfinityMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInfinityMode.AutoSize = true;
            this.chkInfinityMode.Location = new System.Drawing.Point(625, 154);
            this.chkInfinityMode.Name = "chkInfinityMode";
            this.chkInfinityMode.Size = new System.Drawing.Size(99, 19);
            this.chkInfinityMode.TabIndex = 37;
            this.chkInfinityMode.Text = "Infinity Mode";
            this.chkInfinityMode.UseVisualStyleBackColor = true;
            this.chkInfinityMode.Visible = false;
            // 
            // frmTelephonyAutomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 742);
            this.Controls.Add(this.btnClearLiveLog);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTelephonyAutomation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Telephony Automation";
            this.gbGpib.ResumeLayout(false);
            this.gbGpib.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGPIB2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGPIB1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBoard)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbVisa.ResumeLayout(false);
            this.gbVisa.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvTestcases;
        private System.Windows.Forms.GroupBox gbGpib;
        private System.Windows.Forms.NumericUpDown numGPIB2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numGPIB1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numBoard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGpibConnect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbDeviceList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtTcDescripition;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.RadioButton rdbGpib;
        private System.Windows.Forms.RadioButton rdbVisa;
        private System.Windows.Forms.GroupBox gbVisa;
        private System.Windows.Forms.Button btnVisaConnect;
        private System.Windows.Forms.TextBox txtVisaName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnStationEmulatorDebug;
        private System.Windows.Forms.Button btnDutDebug;
        private System.Windows.Forms.Button btnDutStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStationEmulatorState;
        private System.Windows.Forms.TextBox txtDutPhoneState;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtCellPower;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBand;
        private System.Windows.Forms.Button btnTcDebug;
        private System.Windows.Forms.ListView lsvLiveLog;
        private System.Windows.Forms.ColumnHeader chTime;
        private System.Windows.Forms.ColumnHeader chTag;
        private System.Windows.Forms.ColumnHeader chMessage;
        private System.Windows.Forms.ColumnHeader chLogLevel;
        private System.Windows.Forms.Button btnClearLiveLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkInfinityMode;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressMain;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblChekPointResult;
        private System.Windows.Forms.ToolStripStatusLabel lblTcResult;
        private System.Windows.Forms.ToolStripStatusLabel lblInfo;
    }
}

