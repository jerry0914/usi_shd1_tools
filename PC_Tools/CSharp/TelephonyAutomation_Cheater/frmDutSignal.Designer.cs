namespace com.usi.shd1_tools.TelephonyAutomation
{
    partial class frmDutSignal
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
            this.btnSetSignal = new System.Windows.Forms.Button();
            this.lblStrength = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbShowNitifyOnDut = new System.Windows.Forms.CheckBox();
            this.cmbSimSlot = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numInaccuracy = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numTargetStrength = new System.Windows.Forms.NumericUpDown();
            this.lblCurrentStrength = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInaccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetStrength)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSetSignal
            // 
            this.btnSetSignal.Location = new System.Drawing.Point(176, 16);
            this.btnSetSignal.Name = "btnSetSignal";
            this.btnSetSignal.Size = new System.Drawing.Size(70, 57);
            this.btnSetSignal.TabIndex = 1;
            this.btnSetSignal.Text = "Set Signal";
            this.btnSetSignal.UseVisualStyleBackColor = true;
            this.btnSetSignal.Click += new System.EventHandler(this.btnSetSignal_Click);
            // 
            // lblStrength
            // 
            this.lblStrength.AutoSize = true;
            this.lblStrength.Location = new System.Drawing.Point(6, 42);
            this.lblStrength.Name = "lblStrength";
            this.lblStrength.Size = new System.Drawing.Size(53, 13);
            this.lblStrength.TabIndex = 2;
            this.lblStrength.Text = "Strength :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Inaccuracy : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Timeout :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Currnet Strength :";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Result :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbShowNitifyOnDut);
            this.groupBox1.Controls.Add(this.cmbSimSlot);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numTimeout);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numInaccuracy);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numTargetStrength);
            this.groupBox1.Controls.Add(this.lblStrength);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSetSignal);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(252, 145);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // ckbShowNitifyOnDut
            // 
            this.ckbShowNitifyOnDut.AutoSize = true;
            this.ckbShowNitifyOnDut.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbShowNitifyOnDut.Location = new System.Drawing.Point(9, 118);
            this.ckbShowNitifyOnDut.Name = "ckbShowNitifyOnDut";
            this.ckbShowNitifyOnDut.Size = new System.Drawing.Size(154, 17);
            this.ckbShowNitifyOnDut.TabIndex = 12;
            this.ckbShowNitifyOnDut.Text = "ShowNitification On DUT : ";
            this.ckbShowNitifyOnDut.UseVisualStyleBackColor = true;
            this.ckbShowNitifyOnDut.CheckedChanged += new System.EventHandler(this.ckbShowNitifyOnDut_CheckedChanged);
            // 
            // cmbSimSlot
            // 
            this.cmbSimSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSimSlot.FormattingEnabled = true;
            this.cmbSimSlot.Location = new System.Drawing.Point(81, 13);
            this.cmbSimSlot.Name = "cmbSimSlot";
            this.cmbSimSlot.Size = new System.Drawing.Size(84, 21);
            this.cmbSimSlot.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "SIM Slot :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(140, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "(sec)";
            // 
            // numTimeout
            // 
            this.numTimeout.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTimeout.Location = new System.Drawing.Point(81, 92);
            this.numTimeout.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.numTimeout.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(53, 20);
            this.numTimeout.TabIndex = 8;
            this.numTimeout.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(140, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "(db)";
            // 
            // numInaccuracy
            // 
            this.numInaccuracy.Location = new System.Drawing.Point(81, 66);
            this.numInaccuracy.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numInaccuracy.Name = "numInaccuracy";
            this.numInaccuracy.Size = new System.Drawing.Size(53, 20);
            this.numInaccuracy.TabIndex = 7;
            this.numInaccuracy.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(140, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "(db)";
            // 
            // numTargetStrength
            // 
            this.numTargetStrength.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numTargetStrength.Location = new System.Drawing.Point(81, 40);
            this.numTargetStrength.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numTargetStrength.Minimum = new decimal(new int[] {
            140,
            0,
            0,
            -2147483648});
            this.numTargetStrength.Name = "numTargetStrength";
            this.numTargetStrength.Size = new System.Drawing.Size(53, 20);
            this.numTargetStrength.TabIndex = 5;
            this.numTargetStrength.Value = new decimal(new int[] {
            70,
            0,
            0,
            -2147483648});
            // 
            // lblCurrentStrength
            // 
            this.lblCurrentStrength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrentStrength.AutoSize = true;
            this.lblCurrentStrength.Location = new System.Drawing.Point(102, 169);
            this.lblCurrentStrength.Name = "lblCurrentStrength";
            this.lblCurrentStrength.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentStrength.TabIndex = 8;
            this.lblCurrentStrength.Visible = false;
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(55, 152);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 13);
            this.lblResult.TabIndex = 9;
            // 
            // frmDutSignal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 188);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblCurrentStrength);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDutSignal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set DUT Signal";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDutSignal_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numInaccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetStrength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSetSignal;
        private System.Windows.Forms.Label lblStrength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numInaccuracy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numTargetStrength;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label lblCurrentStrength;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ComboBox cmbSimSlot;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox ckbShowNitifyOnDut;
    }
}