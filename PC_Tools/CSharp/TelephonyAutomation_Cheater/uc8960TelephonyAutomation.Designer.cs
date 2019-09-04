namespace com.usi.shd1_tools.TelephonyAutomation
{
    partial class uc8960TelephonyAutomation
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("I/O Coverage ");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("I/O Coverage - Static ");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("I/O coverage Dynamic");
            this.txtSeCellPower = new System.Windows.Forms.TextBox();
            this.gbVisa = new System.Windows.Forms.GroupBox();
            this.txtVisaName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnVisaConnect = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBand = new System.Windows.Forms.TextBox();
            this.rdbGpib = new System.Windows.Forms.RadioButton();
            this.btnRun = new System.Windows.Forms.Button();
            this.rdbVisa = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnStationEmulatorDebug = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ckbCatchMtkLog = new System.Windows.Forms.CheckBox();
            this.btnInitialSettings = new System.Windows.Forms.Button();
            this.gbxDebugTools = new System.Windows.Forms.GroupBox();
            this.btnDutSignal = new System.Windows.Forms.Button();
            this.btnDutDebug = new System.Windows.Forms.Button();
            this.ckbDebug = new System.Windows.Forms.CheckBox();
            this.ckbInfinityMode = new System.Windows.Forms.CheckBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtSimSlot = new System.Windows.Forms.TextBox();
            this.txtSignalStrength = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDutPhoneState = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDutStatus = new System.Windows.Forms.Button();
            this.cmbDeviceList = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSeChannel = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtStationEmulatorState = new System.Windows.Forms.TextBox();
            this.gbGpib = new System.Windows.Forms.GroupBox();
            this.numGPIB2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGpibConnect = new System.Windows.Forms.Button();
            this.numGPIB1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numBoard = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lsvTestcases_GsmGprsWcdma = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tpcTestcases = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lsvTestcasees_TdScdma = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lsvTestcases_Handover = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lsvTestCase_Debug = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtTcDescripition = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gbVisa.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbxDebugTools.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbGpib.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGPIB2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGPIB1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tpcTestcases.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSeCellPower
            // 
            this.txtSeCellPower.BackColor = System.Drawing.Color.White;
            this.txtSeCellPower.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeCellPower.ForeColor = System.Drawing.Color.Black;
            this.txtSeCellPower.Location = new System.Drawing.Point(157, 65);
            this.txtSeCellPower.Name = "txtSeCellPower";
            this.txtSeCellPower.ReadOnly = true;
            this.txtSeCellPower.Size = new System.Drawing.Size(59, 23);
            this.txtSeCellPower.TabIndex = 37;
            this.txtSeCellPower.Text = "-999 db";
            this.txtSeCellPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(112, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 15);
            this.label8.TabIndex = 36;
            this.label8.Text = "Pow : ";
            // 
            // txtBand
            // 
            this.txtBand.BackColor = System.Drawing.Color.RoyalBlue;
            this.txtBand.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBand.ForeColor = System.Drawing.Color.White;
            this.txtBand.Location = new System.Drawing.Point(53, 40);
            this.txtBand.Name = "txtBand";
            this.txtBand.ReadOnly = true;
            this.txtBand.Size = new System.Drawing.Size(165, 22);
            this.txtBand.TabIndex = 35;
            this.txtBand.Text = "Unknow";
            this.txtBand.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rdbGpib
            // 
            this.rdbGpib.AutoSize = true;
            this.rdbGpib.Checked = true;
            this.rdbGpib.Location = new System.Drawing.Point(15, 14);
            this.rdbGpib.Name = "rdbGpib";
            this.rdbGpib.Size = new System.Drawing.Size(50, 17);
            this.rdbGpib.TabIndex = 26;
            this.rdbGpib.TabStop = true;
            this.rdbGpib.Text = "GPIB";
            this.rdbGpib.UseVisualStyleBackColor = true;
            this.rdbGpib.CheckedChanged += new System.EventHandler(this.rdbConnector_CheckedChanged);
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnRun.Location = new System.Drawing.Point(711, 147);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(102, 25);
            this.btnRun.TabIndex = 25;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            this.btnRun.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRun_MouseDown);
            this.btnRun.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRun_MouseUp);
            // 
            // rdbVisa
            // 
            this.rdbVisa.AutoSize = true;
            this.rdbVisa.Location = new System.Drawing.Point(72, 14);
            this.rdbVisa.Name = "rdbVisa";
            this.rdbVisa.Size = new System.Drawing.Size(49, 17);
            this.rdbVisa.TabIndex = 27;
            this.rdbVisa.Text = "VISA";
            this.rdbVisa.UseVisualStyleBackColor = true;
            this.rdbVisa.Visible = false;
            this.rdbVisa.CheckedChanged += new System.EventHandler(this.rdbConnector_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 15);
            this.label7.TabIndex = 34;
            this.label7.Text = "Band :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 15);
            this.label5.TabIndex = 33;
            this.label5.Text = "State :";
            // 
            // btnStationEmulatorDebug
            // 
            this.btnStationEmulatorDebug.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStationEmulatorDebug.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnStationEmulatorDebug.Location = new System.Drawing.Point(10, 31);
            this.btnStationEmulatorDebug.Name = "btnStationEmulatorDebug";
            this.btnStationEmulatorDebug.Size = new System.Drawing.Size(75, 19);
            this.btnStationEmulatorDebug.TabIndex = 26;
            this.btnStationEmulatorDebug.Text = "8960 Debug";
            this.btnStationEmulatorDebug.UseVisualStyleBackColor = true;
            this.btnStationEmulatorDebug.Click += new System.EventHandler(this.btnStationEmulatorFuncTest_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckbCatchMtkLog);
            this.panel1.Controls.Add(this.btnInitialSettings);
            this.panel1.Controls.Add(this.gbxDebugTools);
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.rdbGpib);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Controls.Add(this.rdbVisa);
            this.panel1.Controls.Add(this.gbGpib);
            this.panel1.Controls.Add(this.gbVisa);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(821, 177);
            this.panel1.TabIndex = 41;
            // 
            // ckbCatchMtkLog
            // 
            this.ckbCatchMtkLog.AutoSize = true;
            this.ckbCatchMtkLog.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbCatchMtkLog.Location = new System.Drawing.Point(716, 123);
            this.ckbCatchMtkLog.Name = "ckbCatchMtkLog";
            this.ckbCatchMtkLog.Size = new System.Drawing.Size(93, 17);
            this.ckbCatchMtkLog.TabIndex = 41;
            this.ckbCatchMtkLog.Text = "Catch MTK Log";
            this.ckbCatchMtkLog.UseVisualStyleBackColor = true;
            // 
            // btnInitialSettings
            // 
            this.btnInitialSettings.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnInitialSettings.Image = global::com.usi.shd1_tools.TelephonyAutomation.Properties.Resources.tools;
            this.btnInitialSettings.Location = new System.Drawing.Point(635, 147);
            this.btnInitialSettings.Name = "btnInitialSettings";
            this.btnInitialSettings.Size = new System.Drawing.Size(32, 25);
            this.btnInitialSettings.TabIndex = 38;
            this.btnInitialSettings.UseVisualStyleBackColor = true;
            this.btnInitialSettings.Click += new System.EventHandler(this.btnInitialSettings_Click);
            // 
            // gbxDebugTools
            // 
            this.gbxDebugTools.Controls.Add(this.btnDutSignal);
            this.gbxDebugTools.Controls.Add(this.btnDutDebug);
            this.gbxDebugTools.Controls.Add(this.ckbDebug);
            this.gbxDebugTools.Controls.Add(this.ckbInfinityMode);
            this.gbxDebugTools.Controls.Add(this.btnStationEmulatorDebug);
            this.gbxDebugTools.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDebugTools.Location = new System.Drawing.Point(625, 75);
            this.gbxDebugTools.Name = "gbxDebugTools";
            this.gbxDebugTools.Size = new System.Drawing.Size(188, 69);
            this.gbxDebugTools.TabIndex = 41;
            this.gbxDebugTools.TabStop = false;
            this.gbxDebugTools.Text = "Debug Tools";
            this.gbxDebugTools.Visible = false;
            // 
            // btnDutSignal
            // 
            this.btnDutSignal.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDutSignal.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnDutSignal.Location = new System.Drawing.Point(10, 50);
            this.btnDutSignal.Name = "btnDutSignal";
            this.btnDutSignal.Size = new System.Drawing.Size(75, 18);
            this.btnDutSignal.TabIndex = 40;
            this.btnDutSignal.Text = "DUT Signal";
            this.btnDutSignal.UseVisualStyleBackColor = true;
            this.btnDutSignal.Click += new System.EventHandler(this.btnDutSignal_Click);
            // 
            // btnDutDebug
            // 
            this.btnDutDebug.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDutDebug.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnDutDebug.Location = new System.Drawing.Point(10, 14);
            this.btnDutDebug.Name = "btnDutDebug";
            this.btnDutDebug.Size = new System.Drawing.Size(75, 17);
            this.btnDutDebug.TabIndex = 29;
            this.btnDutDebug.Text = "DUT Debug";
            this.btnDutDebug.UseVisualStyleBackColor = true;
            this.btnDutDebug.Click += new System.EventHandler(this.btnDutTest_Click);
            // 
            // ckbDebug
            // 
            this.ckbDebug.AutoSize = true;
            this.ckbDebug.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbDebug.Location = new System.Drawing.Point(91, 14);
            this.ckbDebug.Name = "ckbDebug";
            this.ckbDebug.Size = new System.Drawing.Size(75, 17);
            this.ckbDebug.TabIndex = 39;
            this.ckbDebug.Text = "Debug flag";
            this.ckbDebug.UseVisualStyleBackColor = true;
            // 
            // ckbInfinityMode
            // 
            this.ckbInfinityMode.AutoSize = true;
            this.ckbInfinityMode.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbInfinityMode.Location = new System.Drawing.Point(91, 30);
            this.ckbInfinityMode.Name = "ckbInfinityMode";
            this.ckbInfinityMode.Size = new System.Drawing.Size(82, 17);
            this.ckbInfinityMode.TabIndex = 37;
            this.ckbInfinityMode.Text = "Infinity loop";
            this.ckbInfinityMode.UseVisualStyleBackColor = true;
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.FlatAppearance.BorderSize = 0;
            this.btnPause.Image = global::com.usi.shd1_tools.TelephonyAutomation.Properties.Resources.pause;
            this.btnPause.Location = new System.Drawing.Point(673, 147);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(32, 25);
            this.btnPause.TabIndex = 38;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtSimSlot);
            this.groupBox5.Controls.Add(this.txtSignalStrength);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txtDutPhoneState);
            this.groupBox5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(467, 75);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(152, 95);
            this.groupBox5.TabIndex = 35;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "DUT";
            // 
            // txtSimSlot
            // 
            this.txtSimSlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtSimSlot.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSimSlot.ForeColor = System.Drawing.Color.Black;
            this.txtSimSlot.Location = new System.Drawing.Point(6, 63);
            this.txtSimSlot.Name = "txtSimSlot";
            this.txtSimSlot.ReadOnly = true;
            this.txtSimSlot.Size = new System.Drawing.Size(57, 22);
            this.txtSimSlot.TabIndex = 39;
            this.txtSimSlot.Text = "Unknow";
            this.txtSimSlot.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSignalStrength
            // 
            this.txtSignalStrength.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtSignalStrength.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSignalStrength.ForeColor = System.Drawing.Color.Black;
            this.txtSignalStrength.Location = new System.Drawing.Point(69, 63);
            this.txtSignalStrength.Name = "txtSignalStrength";
            this.txtSignalStrength.ReadOnly = true;
            this.txtSignalStrength.Size = new System.Drawing.Size(75, 22);
            this.txtSignalStrength.TabIndex = 38;
            this.txtSignalStrength.Text = "Unknow";
            this.txtSignalStrength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 15);
            this.label9.TabIndex = 34;
            this.label9.Text = "Signal Strength :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 15);
            this.label6.TabIndex = 31;
            this.label6.Text = "State : ";
            // 
            // txtDutPhoneState
            // 
            this.txtDutPhoneState.BackColor = System.Drawing.Color.Gray;
            this.txtDutPhoneState.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDutPhoneState.ForeColor = System.Drawing.Color.White;
            this.txtDutPhoneState.Location = new System.Drawing.Point(57, 15);
            this.txtDutPhoneState.Name = "txtDutPhoneState";
            this.txtDutPhoneState.ReadOnly = true;
            this.txtDutPhoneState.Size = new System.Drawing.Size(87, 22);
            this.txtDutPhoneState.TabIndex = 33;
            this.txtDutPhoneState.Text = "Unknow";
            this.txtDutPhoneState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDutPhoneState.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtDutPhoneState_MouseDoubleClick);
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
            this.btnDutStatus.Location = new System.Drawing.Point(386, 21);
            this.btnDutStatus.Name = "btnDutStatus";
            this.btnDutStatus.Size = new System.Drawing.Size(182, 25);
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
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSeChannel);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtSeCellPower);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtBand);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtStationEmulatorState);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(239, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 95);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instrument";
            // 
            // txtSeChannel
            // 
            this.txtSeChannel.BackColor = System.Drawing.Color.White;
            this.txtSeChannel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeChannel.ForeColor = System.Drawing.Color.Black;
            this.txtSeChannel.Location = new System.Drawing.Point(53, 66);
            this.txtSeChannel.Name = "txtSeChannel";
            this.txtSeChannel.ReadOnly = true;
            this.txtSeChannel.Size = new System.Drawing.Size(53, 23);
            this.txtSeChannel.TabIndex = 39;
            this.txtSeChannel.Text = "0000";
            this.txtSeChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 15);
            this.label10.TabIndex = 38;
            this.label10.Text = "Chan: ";
            // 
            // txtStationEmulatorState
            // 
            this.txtStationEmulatorState.BackColor = System.Drawing.Color.Gray;
            this.txtStationEmulatorState.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStationEmulatorState.ForeColor = System.Drawing.Color.White;
            this.txtStationEmulatorState.Location = new System.Drawing.Point(53, 15);
            this.txtStationEmulatorState.Name = "txtStationEmulatorState";
            this.txtStationEmulatorState.ReadOnly = true;
            this.txtStationEmulatorState.Size = new System.Drawing.Size(163, 22);
            this.txtStationEmulatorState.TabIndex = 32;
            this.txtStationEmulatorState.Text = "Unknow";
            this.txtStationEmulatorState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            15,
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
            // columnHeader1
            // 
            this.columnHeader1.Width = 400;
            // 
            // lsvTestcases_GsmGprsWcdma
            // 
            this.lsvTestcases_GsmGprsWcdma.CheckBoxes = true;
            this.lsvTestcases_GsmGprsWcdma.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvTestcases_GsmGprsWcdma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvTestcases_GsmGprsWcdma.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvTestcases_GsmGprsWcdma.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvTestcases_GsmGprsWcdma.Location = new System.Drawing.Point(3, 3);
            this.lsvTestcases_GsmGprsWcdma.Name = "lsvTestcases_GsmGprsWcdma";
            this.lsvTestcases_GsmGprsWcdma.ShowItemToolTips = true;
            this.lsvTestcases_GsmGprsWcdma.Size = new System.Drawing.Size(362, 262);
            this.lsvTestcases_GsmGprsWcdma.TabIndex = 0;
            this.lsvTestcases_GsmGprsWcdma.UseCompatibleStateImageBehavior = false;
            this.lsvTestcases_GsmGprsWcdma.View = System.Windows.Forms.View.Details;
            this.lsvTestcases_GsmGprsWcdma.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lsvTestcases_GsmGprsWcdma_ItemCheck);
            this.lsvTestcases_GsmGprsWcdma.SizeChanged += new System.EventHandler(this.lvTestCase_SizeChanged);
            this.lsvTestcases_GsmGprsWcdma.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvTestcases_MouseClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 19);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tpcTestcases);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(815, 296);
            this.splitContainer1.SplitterDistance = 376;
            this.splitContainer1.TabIndex = 4;
            // 
            // tpcTestcases
            // 
            this.tpcTestcases.Controls.Add(this.tabPage1);
            this.tpcTestcases.Controls.Add(this.tabPage2);
            this.tpcTestcases.Controls.Add(this.tabPage3);
            this.tpcTestcases.Controls.Add(this.tabPage4);
            this.tpcTestcases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpcTestcases.Location = new System.Drawing.Point(0, 0);
            this.tpcTestcases.Name = "tpcTestcases";
            this.tpcTestcases.SelectedIndex = 0;
            this.tpcTestcases.Size = new System.Drawing.Size(376, 296);
            this.tpcTestcases.TabIndex = 1;
            this.tpcTestcases.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tpcTestcases.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvTestcases_MouseClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lsvTestcases_GsmGprsWcdma);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(368, 268);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "GSM/GPRS/WCDMA";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lsvTestcasees_TdScdma);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(368, 268);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TD-SCDMA";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lsvTestcasees_TdScdma
            // 
            this.lsvTestcasees_TdScdma.CheckBoxes = true;
            this.lsvTestcasees_TdScdma.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lsvTestcasees_TdScdma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvTestcasees_TdScdma.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvTestcasees_TdScdma.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvTestcasees_TdScdma.Location = new System.Drawing.Point(3, 3);
            this.lsvTestcasees_TdScdma.Name = "lsvTestcasees_TdScdma";
            this.lsvTestcasees_TdScdma.ShowItemToolTips = true;
            this.lsvTestcasees_TdScdma.Size = new System.Drawing.Size(362, 262);
            this.lsvTestcasees_TdScdma.TabIndex = 1;
            this.lsvTestcasees_TdScdma.UseCompatibleStateImageBehavior = false;
            this.lsvTestcasees_TdScdma.View = System.Windows.Forms.View.Details;
            this.lsvTestcasees_TdScdma.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lsvTestCasees_TdScdma_ItemCheck);
            this.lsvTestcasees_TdScdma.SizeChanged += new System.EventHandler(this.lvTestCase_SizeChanged);
            this.lsvTestcasees_TdScdma.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvTestCasees_TdScdma_MouseClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 400;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lsvTestcases_Handover);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(368, 268);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Handover";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lsvTestcases_Handover
            // 
            this.lsvTestcases_Handover.CheckBoxes = true;
            this.lsvTestcases_Handover.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lsvTestcases_Handover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvTestcases_Handover.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvTestcases_Handover.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvTestcases_Handover.Location = new System.Drawing.Point(3, 3);
            this.lsvTestcases_Handover.Name = "lsvTestcases_Handover";
            this.lsvTestcases_Handover.ShowItemToolTips = true;
            this.lsvTestcases_Handover.Size = new System.Drawing.Size(362, 262);
            this.lsvTestcases_Handover.TabIndex = 2;
            this.lsvTestcases_Handover.UseCompatibleStateImageBehavior = false;
            this.lsvTestcases_Handover.View = System.Windows.Forms.View.Details;
            this.lsvTestcases_Handover.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lsvTestcases_BandFreqNStrength_ItemCheck);
            this.lsvTestcases_Handover.SizeChanged += new System.EventHandler(this.lvTestCase_SizeChanged);
            this.lsvTestcases_Handover.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvTestcases_Handover_MouseClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 400;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lsvTestCase_Debug);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(368, 268);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Debug";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lsvTestCase_Debug
            // 
            this.lsvTestCase_Debug.CheckBoxes = true;
            this.lsvTestCase_Debug.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            this.lsvTestCase_Debug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvTestCase_Debug.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvTestCase_Debug.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            this.lsvTestCase_Debug.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.lsvTestCase_Debug.Location = new System.Drawing.Point(3, 3);
            this.lsvTestCase_Debug.Name = "lsvTestCase_Debug";
            this.lsvTestCase_Debug.ShowItemToolTips = true;
            this.lsvTestCase_Debug.Size = new System.Drawing.Size(362, 262);
            this.lsvTestCase_Debug.TabIndex = 3;
            this.lsvTestCase_Debug.UseCompatibleStateImageBehavior = false;
            this.lsvTestCase_Debug.View = System.Windows.Forms.View.Details;
            this.lsvTestCase_Debug.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lsvTestCase_Debug_ItemCheck);
            this.lsvTestCase_Debug.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lsvTestCaseDebug_ItemChecked);
            this.lsvTestCase_Debug.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvTestCaseDebug_MouseClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 400;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtTcDescripition);
            this.splitContainer2.Size = new System.Drawing.Size(435, 296);
            this.splitContainer2.SplitterDistance = 212;
            this.splitContainer2.TabIndex = 14;
            // 
            // txtTcDescripition
            // 
            this.txtTcDescripition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTcDescripition.Location = new System.Drawing.Point(0, 0);
            this.txtTcDescripition.Name = "txtTcDescripition";
            this.txtTcDescripition.ReadOnly = true;
            this.txtTcDescripition.Size = new System.Drawing.Size(212, 296);
            this.txtTcDescripition.TabIndex = 13;
            this.txtTcDescripition.Text = "";
            this.txtTcDescripition.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTcDescripition_KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.splitContainer1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 177);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(821, 318);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Test cases";
            // 
            // uc8960TelephonyAutomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Name = "uc8960TelephonyAutomation";
            this.Size = new System.Drawing.Size(821, 495);
            this.gbVisa.ResumeLayout(false);
            this.gbVisa.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbxDebugTools.ResumeLayout(false);
            this.gbxDebugTools.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbGpib.ResumeLayout(false);
            this.gbGpib.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGPIB2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGPIB1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBoard)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tpcTestcases.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSeCellPower;
        private System.Windows.Forms.GroupBox gbVisa;
        private System.Windows.Forms.TextBox txtVisaName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnVisaConnect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBand;
        private System.Windows.Forms.RadioButton rdbGpib;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.RadioButton rdbVisa;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStationEmulatorDebug;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ckbInfinityMode;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDutPhoneState;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDutStatus;
        private System.Windows.Forms.ComboBox cmbDeviceList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtStationEmulatorState;
        private System.Windows.Forms.Button btnDutDebug;
        private System.Windows.Forms.GroupBox gbGpib;
        private System.Windows.Forms.NumericUpDown numGPIB2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGpibConnect;
        private System.Windows.Forms.NumericUpDown numGPIB1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numBoard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView lsvTestcases_GsmGprsWcdma;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tpcTestcases;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lsvTestcasees_TdScdma;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.RichTextBox txtTcDescripition;
        private System.Windows.Forms.Button btnInitialSettings;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView lsvTestcases_Handover;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.CheckBox ckbDebug;
        private System.Windows.Forms.Button btnDutSignal;
        private System.Windows.Forms.GroupBox gbxDebugTools;
        private System.Windows.Forms.TextBox txtSignalStrength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSeChannel;
        private System.Windows.Forms.TextBox txtSimSlot;
        private System.Windows.Forms.CheckBox ckbCatchMtkLog;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView lsvTestCase_Debug;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}
