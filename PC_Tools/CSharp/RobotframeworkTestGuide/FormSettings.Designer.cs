namespace com.usi.shd1_tools.RobotframeworkTestGuide
{
    partial class FormSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEmdkVairable = new System.Windows.Forms.TextBox();
            this.btnEmdkVairable = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTestResultFolder = new System.Windows.Forms.TextBox();
            this.btnTestResultFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtScriptsFolder = new System.Windows.Forms.TextBox();
            this.btnScriptsFolder = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPybotPath = new System.Windows.Forms.TextBox();
            this.btnPybotPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numMonitorDeviceInterval = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMonitorDeviceInterval)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEmdkVairable);
            this.groupBox1.Controls.Add(this.btnEmdkVairable);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtTestResultFolder);
            this.groupBox1.Controls.Add(this.btnTestResultFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtScriptsFolder);
            this.groupBox1.Controls.Add(this.btnScriptsFolder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPybotPath);
            this.groupBox1.Controls.Add(this.btnPybotPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 133);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Robotframework Settings";
            // 
            // txtEmdkVairable
            // 
            this.txtEmdkVairable.Location = new System.Drawing.Point(131, 101);
            this.txtEmdkVairable.Name = "txtEmdkVairable";
            this.txtEmdkVairable.ReadOnly = true;
            this.txtEmdkVairable.Size = new System.Drawing.Size(242, 20);
            this.txtEmdkVairable.TabIndex = 28;
            // 
            // btnEmdkVairable
            // 
            this.btnEmdkVairable.Location = new System.Drawing.Point(379, 100);
            this.btnEmdkVairable.Name = "btnEmdkVairable";
            this.btnEmdkVairable.Size = new System.Drawing.Size(23, 23);
            this.btnEmdkVairable.TabIndex = 27;
            this.btnEmdkVairable.Text = "...";
            this.btnEmdkVairable.UseVisualStyleBackColor = true;
            this.btnEmdkVairable.Click += new System.EventHandler(this.btnEmdkVairable_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "EMDK Scanner Folder:";
            // 
            // txtTestResultFolder
            // 
            this.txtTestResultFolder.Location = new System.Drawing.Point(117, 75);
            this.txtTestResultFolder.Name = "txtTestResultFolder";
            this.txtTestResultFolder.ReadOnly = true;
            this.txtTestResultFolder.Size = new System.Drawing.Size(256, 20);
            this.txtTestResultFolder.TabIndex = 25;
            this.txtTestResultFolder.DoubleClick += new System.EventHandler(this.btnTestResultFolder_Click);
            // 
            // btnTestResultFolder
            // 
            this.btnTestResultFolder.Location = new System.Drawing.Point(379, 74);
            this.btnTestResultFolder.Name = "btnTestResultFolder";
            this.btnTestResultFolder.Size = new System.Drawing.Size(23, 23);
            this.btnTestResultFolder.TabIndex = 24;
            this.btnTestResultFolder.Text = "...";
            this.btnTestResultFolder.UseVisualStyleBackColor = true;
            this.btnTestResultFolder.Click += new System.EventHandler(this.btnTestResultFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Test Result Folder: ";
            // 
            // txtScriptsFolder
            // 
            this.txtScriptsFolder.Location = new System.Drawing.Point(95, 48);
            this.txtScriptsFolder.Name = "txtScriptsFolder";
            this.txtScriptsFolder.ReadOnly = true;
            this.txtScriptsFolder.Size = new System.Drawing.Size(278, 20);
            this.txtScriptsFolder.TabIndex = 22;
            this.txtScriptsFolder.DoubleClick += new System.EventHandler(this.btnScriptsFolder_Click);
            // 
            // btnScriptsFolder
            // 
            this.btnScriptsFolder.Location = new System.Drawing.Point(379, 47);
            this.btnScriptsFolder.Name = "btnScriptsFolder";
            this.btnScriptsFolder.Size = new System.Drawing.Size(23, 23);
            this.btnScriptsFolder.TabIndex = 21;
            this.btnScriptsFolder.Text = "...";
            this.btnScriptsFolder.UseVisualStyleBackColor = true;
            this.btnScriptsFolder.Click += new System.EventHandler(this.btnScriptsFolder_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Scripts Folder: ";
            // 
            // txtPybotPath
            // 
            this.txtPybotPath.Location = new System.Drawing.Point(95, 22);
            this.txtPybotPath.Name = "txtPybotPath";
            this.txtPybotPath.ReadOnly = true;
            this.txtPybotPath.Size = new System.Drawing.Size(278, 20);
            this.txtPybotPath.TabIndex = 16;
            this.txtPybotPath.DoubleClick += new System.EventHandler(this.btnPybotPath_Click);
            // 
            // btnPybotPath
            // 
            this.btnPybotPath.Location = new System.Drawing.Point(379, 20);
            this.btnPybotPath.Name = "btnPybotPath";
            this.btnPybotPath.Size = new System.Drawing.Size(23, 23);
            this.btnPybotPath.TabIndex = 15;
            this.btnPybotPath.Text = "...";
            this.btnPybotPath.UseVisualStyleBackColor = true;
            this.btnPybotPath.Click += new System.EventHandler(this.btnPybotPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Pybot path: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numMonitorDeviceInterval);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 57);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tool Settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "(ms)";
            // 
            // numMonitorDeviceInterval
            // 
            this.numMonitorDeviceInterval.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numMonitorDeviceInterval.Location = new System.Drawing.Point(138, 23);
            this.numMonitorDeviceInterval.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numMonitorDeviceInterval.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numMonitorDeviceInterval.Name = "numMonitorDeviceInterval";
            this.numMonitorDeviceInterval.Size = new System.Drawing.Size(54, 20);
            this.numMonitorDeviceInterval.TabIndex = 22;
            this.numMonitorDeviceInterval.Value = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Monitor Device Interval:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 190);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(414, 29);
            this.panel1.TabIndex = 26;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(246, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(327, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(414, 219);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMonitorDeviceInterval)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPybotPath;
        private System.Windows.Forms.Button btnPybotPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtScriptsFolder;
        private System.Windows.Forms.Button btnScriptsFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numMonitorDeviceInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTestResultFolder;
        private System.Windows.Forms.Button btnTestResultFolder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmdkVairable;
        private System.Windows.Forms.Button btnEmdkVairable;
    }
}