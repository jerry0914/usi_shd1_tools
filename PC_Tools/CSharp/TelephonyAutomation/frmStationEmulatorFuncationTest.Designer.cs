namespace com.usi.shd1_tools.TelephonyAutomation
{
    partial class frmStationEmulatorFunctionTest
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
            dev.jerry_h.pc_tools.CommonLibrary.Logger.LiveLogEventHandler -= new System.EventHandler<dev.jerry_h.pc_tools.CommonLibrary.LoggerLiveMessageEventArgs>(showLiveLogMessage);
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
            this.btnEGPRS_850 = new System.Windows.Forms.Button();
            this.btnEGPRS_DCS = new System.Windows.Forms.Button();
            this.btnEGPRS_EGSM = new System.Windows.Forms.Button();
            this.btnEGPRS_PCS = new System.Windows.Forms.Button();
            this.btnGPRS_PCS = new System.Windows.Forms.Button();
            this.btnGPRS_EGSM = new System.Windows.Forms.Button();
            this.btnGPRS_DCS = new System.Windows.Forms.Button();
            this.btnGPRS_850 = new System.Windows.Forms.Button();
            this.btnGSM_PCS = new System.Windows.Forms.Button();
            this.btnGSM_EGSM = new System.Windows.Forms.Button();
            this.btnGSM_DCS = new System.Windows.Forms.Button();
            this.btnGSM_850 = new System.Windows.Forms.Button();
            this.btnWCDMA = new System.Windows.Forms.Button();
            this.btnHSDPA = new System.Windows.Forms.Button();
            this.btnCallStatus = new System.Windows.Forms.Button();
            this.btnDataStatus = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnSetPower = new System.Windows.Forms.Button();
            this.numCellPower = new System.Windows.Forms.NumericUpDown();
            this.btnDial = new System.Windows.Forms.Button();
            this.btnEndCall = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lsvLiveLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtBERCount = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBERGetLast = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBERLast = new System.Windows.Forms.TextBox();
            this.ckbBERContinuous = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBERTimeout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBERInit = new System.Windows.Forms.Button();
            this.btnUMTS2100 = new System.Windows.Forms.Button();
            this.btnUMTS900 = new System.Windows.Forms.Button();
            this.btnGSM900 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTDSCDMA_B34 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numCellPower)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEGPRS_850
            // 
            this.btnEGPRS_850.Location = new System.Drawing.Point(70, 3);
            this.btnEGPRS_850.Name = "btnEGPRS_850";
            this.btnEGPRS_850.Size = new System.Drawing.Size(80, 23);
            this.btnEGPRS_850.TabIndex = 0;
            this.btnEGPRS_850.Text = "EGPRS 850";
            this.btnEGPRS_850.UseVisualStyleBackColor = true;
            this.btnEGPRS_850.Click += new System.EventHandler(this.btnEGPRS_850_Click);
            // 
            // btnEGPRS_DCS
            // 
            this.btnEGPRS_DCS.Location = new System.Drawing.Point(70, 33);
            this.btnEGPRS_DCS.Name = "btnEGPRS_DCS";
            this.btnEGPRS_DCS.Size = new System.Drawing.Size(80, 23);
            this.btnEGPRS_DCS.TabIndex = 1;
            this.btnEGPRS_DCS.Text = "EGPRS DCS";
            this.btnEGPRS_DCS.UseVisualStyleBackColor = true;
            this.btnEGPRS_DCS.Click += new System.EventHandler(this.btnEGPRS_DCS_Click);
            // 
            // btnEGPRS_EGSM
            // 
            this.btnEGPRS_EGSM.Location = new System.Drawing.Point(70, 61);
            this.btnEGPRS_EGSM.Name = "btnEGPRS_EGSM";
            this.btnEGPRS_EGSM.Size = new System.Drawing.Size(80, 23);
            this.btnEGPRS_EGSM.TabIndex = 2;
            this.btnEGPRS_EGSM.Text = "EGPRS EGSM";
            this.btnEGPRS_EGSM.UseVisualStyleBackColor = true;
            this.btnEGPRS_EGSM.Click += new System.EventHandler(this.btnEGPRS_EGSM_Click);
            // 
            // btnEGPRS_PCS
            // 
            this.btnEGPRS_PCS.Location = new System.Drawing.Point(70, 90);
            this.btnEGPRS_PCS.Name = "btnEGPRS_PCS";
            this.btnEGPRS_PCS.Size = new System.Drawing.Size(80, 23);
            this.btnEGPRS_PCS.TabIndex = 3;
            this.btnEGPRS_PCS.Text = "EGPRS PCS";
            this.btnEGPRS_PCS.UseVisualStyleBackColor = true;
            this.btnEGPRS_PCS.Click += new System.EventHandler(this.btnEGPRS_PCS_Click);
            // 
            // btnGPRS_PCS
            // 
            this.btnGPRS_PCS.Location = new System.Drawing.Point(154, 90);
            this.btnGPRS_PCS.Name = "btnGPRS_PCS";
            this.btnGPRS_PCS.Size = new System.Drawing.Size(80, 23);
            this.btnGPRS_PCS.TabIndex = 7;
            this.btnGPRS_PCS.Text = "GPRS PCS";
            this.btnGPRS_PCS.UseVisualStyleBackColor = true;
            this.btnGPRS_PCS.Click += new System.EventHandler(this.btnGPRS_PCS_Click);
            // 
            // btnGPRS_EGSM
            // 
            this.btnGPRS_EGSM.Location = new System.Drawing.Point(154, 61);
            this.btnGPRS_EGSM.Name = "btnGPRS_EGSM";
            this.btnGPRS_EGSM.Size = new System.Drawing.Size(80, 23);
            this.btnGPRS_EGSM.TabIndex = 6;
            this.btnGPRS_EGSM.Text = "GPRS EGSM";
            this.btnGPRS_EGSM.UseVisualStyleBackColor = true;
            this.btnGPRS_EGSM.Click += new System.EventHandler(this.btnGPRS_EGSM_Click);
            // 
            // btnGPRS_DCS
            // 
            this.btnGPRS_DCS.Location = new System.Drawing.Point(154, 33);
            this.btnGPRS_DCS.Name = "btnGPRS_DCS";
            this.btnGPRS_DCS.Size = new System.Drawing.Size(80, 23);
            this.btnGPRS_DCS.TabIndex = 5;
            this.btnGPRS_DCS.Text = "GPRS DCS";
            this.btnGPRS_DCS.UseVisualStyleBackColor = true;
            this.btnGPRS_DCS.Click += new System.EventHandler(this.btnGPRS_DCS_Click);
            // 
            // btnGPRS_850
            // 
            this.btnGPRS_850.Location = new System.Drawing.Point(154, 3);
            this.btnGPRS_850.Name = "btnGPRS_850";
            this.btnGPRS_850.Size = new System.Drawing.Size(80, 23);
            this.btnGPRS_850.TabIndex = 4;
            this.btnGPRS_850.Text = "GPRS 850";
            this.btnGPRS_850.UseVisualStyleBackColor = true;
            this.btnGPRS_850.Click += new System.EventHandler(this.btnGPRS_850_Click);
            // 
            // btnGSM_PCS
            // 
            this.btnGSM_PCS.Location = new System.Drawing.Point(238, 90);
            this.btnGSM_PCS.Name = "btnGSM_PCS";
            this.btnGSM_PCS.Size = new System.Drawing.Size(80, 23);
            this.btnGSM_PCS.TabIndex = 11;
            this.btnGSM_PCS.Text = "GSM PCS";
            this.btnGSM_PCS.UseVisualStyleBackColor = true;
            this.btnGSM_PCS.Click += new System.EventHandler(this.btnGSM_PCS_Click);
            // 
            // btnGSM_EGSM
            // 
            this.btnGSM_EGSM.Location = new System.Drawing.Point(238, 61);
            this.btnGSM_EGSM.Name = "btnGSM_EGSM";
            this.btnGSM_EGSM.Size = new System.Drawing.Size(80, 23);
            this.btnGSM_EGSM.TabIndex = 10;
            this.btnGSM_EGSM.Text = "GSM EGSM";
            this.btnGSM_EGSM.UseVisualStyleBackColor = true;
            this.btnGSM_EGSM.Click += new System.EventHandler(this.btnGSM_EGSM_Click);
            // 
            // btnGSM_DCS
            // 
            this.btnGSM_DCS.Location = new System.Drawing.Point(320, 27);
            this.btnGSM_DCS.Name = "btnGSM_DCS";
            this.btnGSM_DCS.Size = new System.Drawing.Size(106, 23);
            this.btnGSM_DCS.TabIndex = 9;
            this.btnGSM_DCS.Text = "GSM DCS";
            this.btnGSM_DCS.UseVisualStyleBackColor = true;
            this.btnGSM_DCS.Click += new System.EventHandler(this.btnGSM_DCS_Click);
            // 
            // btnGSM_850
            // 
            this.btnGSM_850.Location = new System.Drawing.Point(238, 3);
            this.btnGSM_850.Name = "btnGSM_850";
            this.btnGSM_850.Size = new System.Drawing.Size(80, 23);
            this.btnGSM_850.TabIndex = 8;
            this.btnGSM_850.Text = "GSM 850";
            this.btnGSM_850.UseVisualStyleBackColor = true;
            this.btnGSM_850.Click += new System.EventHandler(this.btnGSM_850_Click);
            // 
            // btnWCDMA
            // 
            this.btnWCDMA.Location = new System.Drawing.Point(3, 90);
            this.btnWCDMA.Name = "btnWCDMA";
            this.btnWCDMA.Size = new System.Drawing.Size(66, 23);
            this.btnWCDMA.TabIndex = 13;
            this.btnWCDMA.Text = "WCDMA";
            this.btnWCDMA.UseVisualStyleBackColor = true;
            this.btnWCDMA.Click += new System.EventHandler(this.btnWCDMA_Click);
            // 
            // btnHSDPA
            // 
            this.btnHSDPA.Location = new System.Drawing.Point(3, 61);
            this.btnHSDPA.Name = "btnHSDPA";
            this.btnHSDPA.Size = new System.Drawing.Size(66, 23);
            this.btnHSDPA.TabIndex = 12;
            this.btnHSDPA.Text = "HSDPA";
            this.btnHSDPA.UseVisualStyleBackColor = true;
            this.btnHSDPA.Click += new System.EventHandler(this.btnHSDPA_Click);
            // 
            // btnCallStatus
            // 
            this.btnCallStatus.Location = new System.Drawing.Point(136, 116);
            this.btnCallStatus.Name = "btnCallStatus";
            this.btnCallStatus.Size = new System.Drawing.Size(80, 37);
            this.btnCallStatus.TabIndex = 14;
            this.btnCallStatus.Text = "Get Call Status";
            this.btnCallStatus.UseVisualStyleBackColor = true;
            this.btnCallStatus.Click += new System.EventHandler(this.btnCallStatus_Click);
            // 
            // btnDataStatus
            // 
            this.btnDataStatus.Location = new System.Drawing.Point(222, 116);
            this.btnDataStatus.Name = "btnDataStatus";
            this.btnDataStatus.Size = new System.Drawing.Size(80, 37);
            this.btnDataStatus.TabIndex = 15;
            this.btnDataStatus.Text = "Get Data Status";
            this.btnDataStatus.UseVisualStyleBackColor = true;
            this.btnDataStatus.Click += new System.EventHandler(this.btnDataStatus_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Result :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Command :";
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(12, 33);
            this.txtCommand.Multiline = true;
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(526, 81);
            this.txtCommand.TabIndex = 26;
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSend.Location = new System.Drawing.Point(75, 7);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(89, 22);
            this.btnSend.TabIndex = 27;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnSetPower
            // 
            this.btnSetPower.Location = new System.Drawing.Point(66, 116);
            this.btnSetPower.Name = "btnSetPower";
            this.btnSetPower.Size = new System.Drawing.Size(64, 23);
            this.btnSetPower.TabIndex = 28;
            this.btnSetPower.Text = "Set Power";
            this.btnSetPower.UseVisualStyleBackColor = true;
            this.btnSetPower.Click += new System.EventHandler(this.btnSetPower_Click);
            // 
            // numCellPower
            // 
            this.numCellPower.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numCellPower.Location = new System.Drawing.Point(12, 119);
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
            this.numCellPower.Size = new System.Drawing.Size(48, 20);
            this.numCellPower.TabIndex = 30;
            this.numCellPower.Value = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            // 
            // btnDial
            // 
            this.btnDial.Location = new System.Drawing.Point(3, 3);
            this.btnDial.Name = "btnDial";
            this.btnDial.Size = new System.Drawing.Size(66, 23);
            this.btnDial.TabIndex = 32;
            this.btnDial.Text = "Dial";
            this.btnDial.UseVisualStyleBackColor = true;
            this.btnDial.Click += new System.EventHandler(this.btnDial_Click);
            // 
            // btnEndCall
            // 
            this.btnEndCall.Location = new System.Drawing.Point(3, 33);
            this.btnEndCall.Name = "btnEndCall";
            this.btnEndCall.Size = new System.Drawing.Size(66, 23);
            this.btnEndCall.TabIndex = 33;
            this.btnEndCall.Text = "End Call";
            this.btnEndCall.UseVisualStyleBackColor = true;
            this.btnEndCall.Click += new System.EventHandler(this.btnEndCall_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lsvLiveLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 306);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 221);
            this.panel1.TabIndex = 34;
            // 
            // lsvLiveLog
            // 
            this.lsvLiveLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvLiveLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvLiveLog.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvLiveLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvLiveLog.Location = new System.Drawing.Point(0, 0);
            this.lsvLiveLog.Name = "lsvLiveLog";
            this.lsvLiveLog.Size = new System.Drawing.Size(545, 221);
            this.lsvLiveLog.TabIndex = 2;
            this.lsvLiveLog.UseCompatibleStateImageBehavior = false;
            this.lsvLiveLog.View = System.Windows.Forms.View.Details;
            this.lsvLiveLog.SizeChanged += new System.EventHandler(this.lsvLiveLog_SizeChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 496;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtBERCount);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnUMTS2100);
            this.panel2.Controls.Add(this.btnUMTS900);
            this.panel2.Controls.Add(this.btnGSM900);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnTDSCDMA_B34);
            this.panel2.Controls.Add(this.btnDial);
            this.panel2.Controls.Add(this.btnEGPRS_850);
            this.panel2.Controls.Add(this.btnEndCall);
            this.panel2.Controls.Add(this.btnEGPRS_DCS);
            this.panel2.Controls.Add(this.btnEGPRS_EGSM);
            this.panel2.Controls.Add(this.numCellPower);
            this.panel2.Controls.Add(this.btnEGPRS_PCS);
            this.panel2.Controls.Add(this.btnGPRS_850);
            this.panel2.Controls.Add(this.btnSetPower);
            this.panel2.Controls.Add(this.btnGPRS_DCS);
            this.panel2.Controls.Add(this.btnGPRS_EGSM);
            this.panel2.Controls.Add(this.btnGPRS_PCS);
            this.panel2.Controls.Add(this.btnGSM_850);
            this.panel2.Controls.Add(this.btnGSM_DCS);
            this.panel2.Controls.Add(this.btnDataStatus);
            this.panel2.Controls.Add(this.btnGSM_EGSM);
            this.panel2.Controls.Add(this.btnCallStatus);
            this.panel2.Controls.Add(this.btnGSM_PCS);
            this.panel2.Controls.Add(this.btnWCDMA);
            this.panel2.Controls.Add(this.btnHSDPA);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(545, 163);
            this.panel2.TabIndex = 35;
            // 
            // txtBERCount
            // 
            this.txtBERCount.Location = new System.Drawing.Point(478, 16);
            this.txtBERCount.Name = "txtBERCount";
            this.txtBERCount.Size = new System.Drawing.Size(54, 20);
            this.txtBERCount.TabIndex = 41;
            this.txtBERCount.Text = "2600";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBERGetLast);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBERLast);
            this.groupBox1.Controls.Add(this.ckbBERContinuous);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtBERTimeout);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnBERInit);
            this.groupBox1.Location = new System.Drawing.Point(432, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 154);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BER";
            // 
            // btnBERGetLast
            // 
            this.btnBERGetLast.Location = new System.Drawing.Point(63, 131);
            this.btnBERGetLast.Name = "btnBERGetLast";
            this.btnBERGetLast.Size = new System.Drawing.Size(38, 23);
            this.btnBERGetLast.TabIndex = 48;
            this.btnBERGetLast.Text = "Get";
            this.btnBERGetLast.UseVisualStyleBackColor = true;
            this.btnBERGetLast.Click += new System.EventHandler(this.btnBERGetLast_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "BER";
            // 
            // txtBERLast
            // 
            this.txtBERLast.Location = new System.Drawing.Point(41, 106);
            this.txtBERLast.Name = "txtBERLast";
            this.txtBERLast.Size = new System.Drawing.Size(59, 20);
            this.txtBERLast.TabIndex = 45;
            // 
            // ckbBERContinuous
            // 
            this.ckbBERContinuous.AutoSize = true;
            this.ckbBERContinuous.Location = new System.Drawing.Point(9, 62);
            this.ckbBERContinuous.Name = "ckbBERContinuous";
            this.ckbBERContinuous.Size = new System.Drawing.Size(79, 17);
            this.ckbBERContinuous.TabIndex = 44;
            this.ckbBERContinuous.Text = "Continuous";
            this.ckbBERContinuous.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(83, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "sec";
            // 
            // txtBERTimeout
            // 
            this.txtBERTimeout.Location = new System.Drawing.Point(47, 36);
            this.txtBERTimeout.Name = "txtBERTimeout";
            this.txtBERTimeout.Size = new System.Drawing.Size(34, 20);
            this.txtBERTimeout.TabIndex = 42;
            this.txtBERTimeout.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "timeout";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "count";
            // 
            // btnBERInit
            // 
            this.btnBERInit.Location = new System.Drawing.Point(62, 79);
            this.btnBERInit.Name = "btnBERInit";
            this.btnBERInit.Size = new System.Drawing.Size(38, 23);
            this.btnBERInit.TabIndex = 39;
            this.btnBERInit.Text = "Init";
            this.btnBERInit.UseVisualStyleBackColor = true;
            this.btnBERInit.Click += new System.EventHandler(this.btnBERInit_Click);
            // 
            // btnUMTS2100
            // 
            this.btnUMTS2100.Location = new System.Drawing.Point(320, 80);
            this.btnUMTS2100.Name = "btnUMTS2100";
            this.btnUMTS2100.Size = new System.Drawing.Size(106, 23);
            this.btnUMTS2100.TabIndex = 38;
            this.btnUMTS2100.Text = "UMTS 2100";
            this.btnUMTS2100.UseVisualStyleBackColor = true;
            this.btnUMTS2100.Click += new System.EventHandler(this.btnUMTS2100_Click);
            // 
            // btnUMTS900
            // 
            this.btnUMTS900.Location = new System.Drawing.Point(320, 52);
            this.btnUMTS900.Name = "btnUMTS900";
            this.btnUMTS900.Size = new System.Drawing.Size(106, 23);
            this.btnUMTS900.TabIndex = 37;
            this.btnUMTS900.Text = "UMTS 900";
            this.btnUMTS900.UseVisualStyleBackColor = true;
            this.btnUMTS900.Click += new System.EventHandler(this.btnUMTS900_Click);
            // 
            // btnGSM900
            // 
            this.btnGSM900.Location = new System.Drawing.Point(320, 0);
            this.btnGSM900.Name = "btnGSM900";
            this.btnGSM900.Size = new System.Drawing.Size(106, 23);
            this.btnGSM900.TabIndex = 36;
            this.btnGSM900.Text = "GSM 900";
            this.btnGSM900.UseVisualStyleBackColor = true;
            this.btnGSM900.Click += new System.EventHandler(this.btnGSM900_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(320, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 23);
            this.button1.TabIndex = 35;
            this.button1.Text = "TDSCDMA B39";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnTDSCDMA_B34
            // 
            this.btnTDSCDMA_B34.Location = new System.Drawing.Point(320, 107);
            this.btnTDSCDMA_B34.Name = "btnTDSCDMA_B34";
            this.btnTDSCDMA_B34.Size = new System.Drawing.Size(106, 23);
            this.btnTDSCDMA_B34.TabIndex = 34;
            this.btnTDSCDMA_B34.Text = "TDSCDMA B34";
            this.btnTDSCDMA_B34.UseVisualStyleBackColor = true;
            this.btnTDSCDMA_B34.Click += new System.EventHandler(this.btnTDSCDMA_B34_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtCommand);
            this.panel3.Controls.Add(this.btnSend);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 163);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(545, 143);
            this.panel3.TabIndex = 36;
            // 
            // frmStationEmulatorFunctionTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 527);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "frmStationEmulatorFunctionTest";
            this.Text = "StationEmulator Function Test";
            this.Load += new System.EventHandler(this.frmStationEmulatorFunctionTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCellPower)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEGPRS_850;
        private System.Windows.Forms.Button btnEGPRS_DCS;
        private System.Windows.Forms.Button btnEGPRS_EGSM;
        private System.Windows.Forms.Button btnEGPRS_PCS;
        private System.Windows.Forms.Button btnGPRS_PCS;
        private System.Windows.Forms.Button btnGPRS_EGSM;
        private System.Windows.Forms.Button btnGPRS_DCS;
        private System.Windows.Forms.Button btnGPRS_850;
        private System.Windows.Forms.Button btnGSM_PCS;
        private System.Windows.Forms.Button btnGSM_EGSM;
        private System.Windows.Forms.Button btnGSM_DCS;
        private System.Windows.Forms.Button btnGSM_850;
        private System.Windows.Forms.Button btnWCDMA;
        private System.Windows.Forms.Button btnHSDPA;
        private System.Windows.Forms.Button btnCallStatus;
        private System.Windows.Forms.Button btnDataStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnSetPower;
        private System.Windows.Forms.NumericUpDown numCellPower;
        private System.Windows.Forms.Button btnDial;
        private System.Windows.Forms.Button btnEndCall;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lsvLiveLog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnTDSCDMA_B34;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnUMTS2100;
        private System.Windows.Forms.Button btnUMTS900;
        private System.Windows.Forms.Button btnGSM900;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox txtBERCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBERTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBERInit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBERLast;
        private System.Windows.Forms.CheckBox ckbBERContinuous;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBERGetLast;
    }
}

