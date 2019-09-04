namespace com.usi.shd1_tools.RobotframeworkTestGuide
{
    partial class FormEmdkScriptGenerater
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDutId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLegacyId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbTestPlatform = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbScannerType = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtTrsPath = new System.Windows.Forms.TextBox();
            this.cbbDisplayType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(293, 29);
            this.panel1.TabIndex = 27;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(125, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Generate";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(206, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "DUT ID :";
            // 
            // txtDutId
            // 
            this.txtDutId.Location = new System.Drawing.Point(98, 12);
            this.txtDutId.Name = "txtDutId";
            this.txtDutId.ReadOnly = true;
            this.txtDutId.Size = new System.Drawing.Size(183, 20);
            this.txtDutId.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "TRS Path :";
            // 
            // txtLegacyId
            // 
            this.txtLegacyId.Location = new System.Drawing.Point(98, 38);
            this.txtLegacyId.Name = "txtLegacyId";
            this.txtLegacyId.Size = new System.Drawing.Size(183, 20);
            this.txtLegacyId.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Legacy ID :";
            // 
            // cbbTestPlatform
            // 
            this.cbbTestPlatform.FormattingEnabled = true;
            this.cbbTestPlatform.Items.AddRange(new object[] {
            "Android",
            "Xamarin"});
            this.cbbTestPlatform.Location = new System.Drawing.Point(98, 91);
            this.cbbTestPlatform.Name = "cbbTestPlatform";
            this.cbbTestPlatform.Size = new System.Drawing.Size(183, 21);
            this.cbbTestPlatform.TabIndex = 34;
            this.cbbTestPlatform.Text = "Android";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "Platform :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Scanner Type :";
            // 
            // cbbScannerType
            // 
            this.cbbScannerType.FormattingEnabled = true;
            this.cbbScannerType.Items.AddRange(new object[] {
            "Camera_Scanner",
            "Serial_SSI_Scanner",
            "Imager",
            "2D_Barcode_Imager",
            "Serial_SSI_2D_Imager",
            "2D_Imager",
            "BLUETOOTH_IMAGER1"});
            this.cbbScannerType.Location = new System.Drawing.Point(98, 64);
            this.cbbScannerType.Name = "cbbScannerType";
            this.cbbScannerType.Size = new System.Drawing.Size(183, 21);
            this.cbbScannerType.TabIndex = 37;
            this.cbbScannerType.Text = "2D_Imager";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtTrsPath
            // 
            this.txtTrsPath.Location = new System.Drawing.Point(98, 145);
            this.txtTrsPath.Name = "txtTrsPath";
            this.txtTrsPath.ReadOnly = true;
            this.txtTrsPath.Size = new System.Drawing.Size(183, 20);
            this.txtTrsPath.TabIndex = 31;
            // 
            // cbbDisplayType
            // 
            this.cbbDisplayType.FormattingEnabled = true;
            this.cbbDisplayType.Items.AddRange(new object[] {
            "LCD",
            "E-INK"});
            this.cbbDisplayType.Location = new System.Drawing.Point(98, 118);
            this.cbbDisplayType.Name = "cbbDisplayType";
            this.cbbDisplayType.Size = new System.Drawing.Size(183, 21);
            this.cbbDisplayType.TabIndex = 39;
            this.cbbDisplayType.Text = "LCD";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "Display Type :";
            // 
            // FormEmdkScriptGenerater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 199);
            this.Controls.Add(this.cbbDisplayType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbbScannerType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbbTestPlatform);
            this.Controls.Add(this.txtLegacyId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTrsPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDutId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormEmdkScriptGenerater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generate Scripts";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDutId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLegacyId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbTestPlatform;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbScannerType;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtTrsPath;
        private System.Windows.Forms.ComboBox cbbDisplayType;
        private System.Windows.Forms.Label label6;
    }
}