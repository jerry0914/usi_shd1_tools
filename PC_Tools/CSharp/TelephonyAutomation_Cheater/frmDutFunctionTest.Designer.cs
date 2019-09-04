namespace com.usi.shd1_tools.TelephonyAutomation
{
    partial class frmDutFunctionTest
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtDialNumber = new System.Windows.Forms.TextBox();
            this.btnDial = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPhoneCallStatus = new System.Windows.Forms.Button();
            this.txtPhoneStatus = new System.Windows.Forms.TextBox();
            this.btnAnswer = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ckbSetMobileData = new System.Windows.Forms.CheckBox();
            this.ckbSetWifiState = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbSIM2_Enable = new System.Windows.Forms.CheckBox();
            this.ckbSIM1_Enable = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetSignalStrength = new System.Windows.Forms.Button();
            this.lblSignal_SIM2 = new System.Windows.Forms.Label();
            this.lblSignal_SIM1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Phone Number";
            // 
            // txtDialNumber
            // 
            this.txtDialNumber.Location = new System.Drawing.Point(106, 46);
            this.txtDialNumber.Name = "txtDialNumber";
            this.txtDialNumber.Size = new System.Drawing.Size(151, 20);
            this.txtDialNumber.TabIndex = 1;
            // 
            // btnDial
            // 
            this.btnDial.Location = new System.Drawing.Point(263, 44);
            this.btnDial.Name = "btnDial";
            this.btnDial.Size = new System.Drawing.Size(75, 23);
            this.btnDial.TabIndex = 2;
            this.btnDial.Text = "Dial";
            this.btnDial.UseVisualStyleBackColor = true;
            this.btnDial.Click += new System.EventHandler(this.btnDial_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Phone call status";
            // 
            // btnPhoneCallStatus
            // 
            this.btnPhoneCallStatus.Location = new System.Drawing.Point(263, 9);
            this.btnPhoneCallStatus.Name = "btnPhoneCallStatus";
            this.btnPhoneCallStatus.Size = new System.Drawing.Size(75, 23);
            this.btnPhoneCallStatus.TabIndex = 4;
            this.btnPhoneCallStatus.Text = "Get Status";
            this.btnPhoneCallStatus.UseVisualStyleBackColor = true;
            this.btnPhoneCallStatus.Click += new System.EventHandler(this.btnPhoneCallStatus_Click);
            // 
            // txtPhoneStatus
            // 
            this.txtPhoneStatus.Location = new System.Drawing.Point(116, 11);
            this.txtPhoneStatus.Name = "txtPhoneStatus";
            this.txtPhoneStatus.ReadOnly = true;
            this.txtPhoneStatus.Size = new System.Drawing.Size(141, 20);
            this.txtPhoneStatus.TabIndex = 5;
            // 
            // btnAnswer
            // 
            this.btnAnswer.Location = new System.Drawing.Point(263, 79);
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.Size = new System.Drawing.Size(75, 23);
            this.btnAnswer.TabIndex = 7;
            this.btnAnswer.Text = "Answer Call";
            this.btnAnswer.UseVisualStyleBackColor = true;
            this.btnAnswer.Click += new System.EventHandler(this.btnAnswer_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(106, 82);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(125, 20);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Timeout";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "ms";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 118);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "End Call";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ckbSetMobileData
            // 
            this.ckbSetMobileData.AutoSize = true;
            this.ckbSetMobileData.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSetMobileData.Location = new System.Drawing.Point(24, 121);
            this.ckbSetMobileData.Name = "ckbSetMobileData";
            this.ckbSetMobileData.Size = new System.Drawing.Size(100, 17);
            this.ckbSetMobileData.TabIndex = 11;
            this.ckbSetMobileData.Text = "Set Mobile data";
            this.ckbSetMobileData.UseVisualStyleBackColor = true;
            this.ckbSetMobileData.CheckedChanged += new System.EventHandler(this.ckbMobileData_CheckedChanged);
            // 
            // ckbSetWifiState
            // 
            this.ckbSetWifiState.AutoSize = true;
            this.ckbSetWifiState.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSetWifiState.Location = new System.Drawing.Point(35, 145);
            this.ckbSetWifiState.Name = "ckbSetWifiState";
            this.ckbSetWifiState.Size = new System.Drawing.Size(89, 17);
            this.ckbSetWifiState.TabIndex = 12;
            this.ckbSetWifiState.Text = "Set Wifi state";
            this.ckbSetWifiState.UseVisualStyleBackColor = true;
            this.ckbSetWifiState.CheckedChanged += new System.EventHandler(this.ckbSetWifiState_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbSIM2_Enable);
            this.groupBox1.Controls.Add(this.ckbSIM1_Enable);
            this.groupBox1.Location = new System.Drawing.Point(14, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 49);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SIM Enable";
            // 
            // ckbSIM2_Enable
            // 
            this.ckbSIM2_Enable.AutoSize = true;
            this.ckbSIM2_Enable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSIM2_Enable.Checked = true;
            this.ckbSIM2_Enable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSIM2_Enable.Location = new System.Drawing.Point(77, 23);
            this.ckbSIM2_Enable.Name = "ckbSIM2_Enable";
            this.ckbSIM2_Enable.Size = new System.Drawing.Size(51, 17);
            this.ckbSIM2_Enable.TabIndex = 13;
            this.ckbSIM2_Enable.Text = "SIM2";
            this.ckbSIM2_Enable.UseVisualStyleBackColor = true;
            this.ckbSIM2_Enable.CheckedChanged += new System.EventHandler(this.ckbSIM_Enable_CheckedChanged);
            // 
            // ckbSIM1_Enable
            // 
            this.ckbSIM1_Enable.AutoSize = true;
            this.ckbSIM1_Enable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSIM1_Enable.Checked = true;
            this.ckbSIM1_Enable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSIM1_Enable.Location = new System.Drawing.Point(21, 23);
            this.ckbSIM1_Enable.Name = "ckbSIM1_Enable";
            this.ckbSIM1_Enable.Size = new System.Drawing.Size(51, 17);
            this.ckbSIM1_Enable.TabIndex = 12;
            this.ckbSIM1_Enable.Text = "SIM1";
            this.ckbSIM1_Enable.UseVisualStyleBackColor = true;
            this.ckbSIM1_Enable.CheckedChanged += new System.EventHandler(this.ckbSIM_Enable_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGetSignalStrength);
            this.groupBox2.Controls.Add(this.lblSignal_SIM2);
            this.groupBox2.Controls.Add(this.lblSignal_SIM1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(163, 221);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(177, 68);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Signal Strength";
            // 
            // btnGetSignalStrength
            // 
            this.btnGetSignalStrength.Location = new System.Drawing.Point(129, 19);
            this.btnGetSignalStrength.Name = "btnGetSignalStrength";
            this.btnGetSignalStrength.Size = new System.Drawing.Size(42, 44);
            this.btnGetSignalStrength.TabIndex = 4;
            this.btnGetSignalStrength.Text = "Get";
            this.btnGetSignalStrength.UseVisualStyleBackColor = true;
            this.btnGetSignalStrength.Click += new System.EventHandler(this.btnGetSignalStrength_Click);
            // 
            // lblSignal_SIM2
            // 
            this.lblSignal_SIM2.AutoSize = true;
            this.lblSignal_SIM2.Location = new System.Drawing.Point(62, 50);
            this.lblSignal_SIM2.Name = "lblSignal_SIM2";
            this.lblSignal_SIM2.Size = new System.Drawing.Size(0, 13);
            this.lblSignal_SIM2.TabIndex = 3;
            // 
            // lblSignal_SIM1
            // 
            this.lblSignal_SIM1.AutoSize = true;
            this.lblSignal_SIM1.Location = new System.Drawing.Point(62, 24);
            this.lblSignal_SIM1.Name = "lblSignal_SIM1";
            this.lblSignal_SIM1.Size = new System.Drawing.Size(0, 13);
            this.lblSignal_SIM1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "SIM2:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "SIM1:";
            // 
            // frmDutFunctionTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 301);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ckbSetWifiState);
            this.Controls.Add(this.ckbSetMobileData);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnAnswer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPhoneStatus);
            this.Controls.Add(this.btnPhoneCallStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDial);
            this.Controls.Add(this.txtDialNumber);
            this.Controls.Add(this.label1);
            this.Name = "frmDutFunctionTest";
            this.Text = "frmDutFunctionTest";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDialNumber;
        private System.Windows.Forms.Button btnDial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPhoneCallStatus;
        private System.Windows.Forms.TextBox txtPhoneStatus;
        private System.Windows.Forms.Button btnAnswer;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox ckbSetMobileData;
        private System.Windows.Forms.CheckBox ckbSetWifiState;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbSIM2_Enable;
        private System.Windows.Forms.CheckBox ckbSIM1_Enable;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblSignal_SIM2;
        private System.Windows.Forms.Label lblSignal_SIM1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGetSignalStrength;
    }
}