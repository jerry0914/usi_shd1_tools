namespace com.usi.shd1_tools.TestGuide
{
    partial class FormMain
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
            deviceCheck_Flag =false;
            if (tdDeviceCheck!=null)
            {
                tdDeviceCheck.Abort(1000);
                tdDeviceCheck = null;
            }
            if (frmInstall != null)
            {
                frmInstall.Dispose();
            }
            if (frmTestResult != null)
            {
                frmTestResult.Dispose();
            }
            if (frmInitial != null)
            {
                frmInitial.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.cmbPlatform = new System.Windows.Forms.ToolStripComboBox();
            this.txtDeviceConnetion = new System.Windows.Forms.ToolStripTextBox();
            this.txtIPAddress = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTRSReader = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInitial = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTestResult = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDataMaintainance = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.cmbPlatform,
            this.txtDeviceConnetion,
            this.txtIPAddress,
            this.toolStripMenuItem1,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.pagesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1094, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.Size = new System.Drawing.Size(60, 23);
            this.toolStripTextBox1.Text = "Platform :";
            // 
            // cmbPlatform
            // 
            this.cmbPlatform.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cmbPlatform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlatform.Items.AddRange(new object[] {
            "Win CE",
            "Android"});
            this.cmbPlatform.Name = "cmbPlatform";
            this.cmbPlatform.Size = new System.Drawing.Size(150, 23);
            this.cmbPlatform.ToolTipText = "Platform";
            this.cmbPlatform.SelectedIndexChanged += new System.EventHandler(this.cmbPlatform_SelectedIndexChanged);
            // 
            // txtDeviceConnetion
            // 
            this.txtDeviceConnetion.BackColor = System.Drawing.Color.Crimson;
            this.txtDeviceConnetion.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtDeviceConnetion.ForeColor = System.Drawing.Color.White;
            this.txtDeviceConnetion.Name = "txtDeviceConnetion";
            this.txtDeviceConnetion.ReadOnly = true;
            this.txtDeviceConnetion.Size = new System.Drawing.Size(100, 23);
            this.txtDeviceConnetion.Text = "Disconnected";
            this.txtDeviceConnetion.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.BackColor = System.Drawing.Color.White;
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.ReadOnly = true;
            this.txtIPAddress.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem1.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.help;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(60, 23);
            this.toolStripMenuItem1.Text = "Help";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.Settings;
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 23);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 23);            
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Visible = false;
            // 
            // pagesToolStripMenuItem
            // 
            this.pagesToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.pagesToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.pagesToolStripMenuItem.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.list_20;
            this.pagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTRSReader,
            this.menuInitial,
            this.menuTestResult,
            this.menuDataMaintainance});
            this.pagesToolStripMenuItem.Name = "pagesToolStripMenuItem";
            this.pagesToolStripMenuItem.Size = new System.Drawing.Size(71, 23);
            this.pagesToolStripMenuItem.Text = "Functions";
            // 
            // menuTRSReader
            // 
            this.menuTRSReader.Checked = true;
            this.menuTRSReader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuTRSReader.Name = "menuTRSReader";
            this.menuTRSReader.Size = new System.Drawing.Size(209, 22);
            this.menuTRSReader.Text = "TRS Player";
            this.menuTRSReader.Click += new System.EventHandler(this.menuActionSelected);
            // 
            // menuInitial
            // 
            this.menuInitial.Name = "menuInitial";
            this.menuInitial.Size = new System.Drawing.Size(209, 22);
            this.menuInitial.Text = "Pre-Condition Setup";
            this.menuInitial.Click += new System.EventHandler(this.menuActionSelected);
            // 
            // menuTestResult
            // 
            this.menuTestResult.Name = "menuTestResult";
            this.menuTestResult.Size = new System.Drawing.Size(209, 22);
            this.menuTestResult.Text = "Test result and Log Parser";
            this.menuTestResult.Click += new System.EventHandler(this.menuActionSelected);
            // 
            // menuDataMaintainance
            // 
            this.menuDataMaintainance.Name = "menuDataMaintainance";
            this.menuDataMaintainance.Size = new System.Drawing.Size(209, 22);
            this.menuDataMaintainance.Text = "Data Maintance";
            this.menuDataMaintainance.Click += new System.EventHandler(this.menuActionSelected);
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 27);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1094, 744);
            this.pnlMain.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 771);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1110, 809);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cmbPlatform;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripTextBox txtDeviceConnetion;
        private System.Windows.Forms.ToolStripTextBox txtIPAddress;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuTRSReader;
        private System.Windows.Forms.ToolStripMenuItem menuInitial;
        private System.Windows.Forms.ToolStripMenuItem menuTestResult;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStripMenuItem menuDataMaintainance;
    }
}