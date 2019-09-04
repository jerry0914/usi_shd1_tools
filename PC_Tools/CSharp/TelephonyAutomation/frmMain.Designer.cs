namespace com.usi.shd1_tools.TelephonyAutomation
{
    partial class frmMain
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
            if (uc8960 != null)
            {
                uc8960.Dispose();
            }
            //if (ucDUT != null)
            //{
            //    ucDUT.Dispose();
            //}
            stopDevicesMonitor();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressMain = new System.Windows.Forms.ToolStripProgressBar();
            this.lblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblChekPointResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTcResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._8960toDUTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dUT1ToDUT2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbxLiveLog = new System.Windows.Forms.GroupBox();
            this.lsvLiveLog = new System.Windows.Forms.ListView();
            this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLogLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClearLiveLog = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gbxLiveLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressMain,
            this.lblProgress,
            this.lblChekPointResult,
            this.lblTcResult,
            this.lblInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 711);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(852, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressMain
            // 
            this.progressMain.Name = "progressMain";
            this.progressMain.Size = new System.Drawing.Size(180, 17);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testModeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(852, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // testModeToolStripMenuItem
            // 
            this.testModeToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.testModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._8960toDUTToolStripMenuItem,
            this.dUT1ToDUT2ToolStripMenuItem});
            this.testModeToolStripMenuItem.Name = "testModeToolStripMenuItem";
            this.testModeToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.testModeToolStripMenuItem.Text = "Test Mode";
            // 
            // _8960toDUTToolStripMenuItem
            // 
            this._8960toDUTToolStripMenuItem.Name = "_8960toDUTToolStripMenuItem";
            this._8960toDUTToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this._8960toDUTToolStripMenuItem.Text = "8960 to DUT";
            this._8960toDUTToolStripMenuItem.Click += new System.EventHandler(this.menuTestMode_Clicked);
            // 
            // dUT1ToDUT2ToolStripMenuItem
            // 
            this.dUT1ToDUT2ToolStripMenuItem.Name = "dUT1ToDUT2ToolStripMenuItem";
            this.dUT1ToDUT2ToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.dUT1ToDUT2ToolStripMenuItem.Text = "DUT1 to DUT2";
            this.dUT1ToDUT2ToolStripMenuItem.Click += new System.EventHandler(this.menuTestMode_Clicked);
            // 
            // gbxLiveLog
            // 
            this.gbxLiveLog.Controls.Add(this.lsvLiveLog);
            this.gbxLiveLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbxLiveLog.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxLiveLog.Location = new System.Drawing.Point(0, 483);
            this.gbxLiveLog.Name = "gbxLiveLog";
            this.gbxLiveLog.Size = new System.Drawing.Size(852, 228);
            this.gbxLiveLog.TabIndex = 41;
            this.gbxLiveLog.TabStop = false;
            this.gbxLiveLog.Text = "Live Log";
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
            this.lsvLiveLog.Size = new System.Drawing.Size(846, 206);
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
            // btnClearLiveLog
            // 
            this.btnClearLiveLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLiveLog.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearLiveLog.Location = new System.Drawing.Point(793, 483);
            this.btnClearLiveLog.Name = "btnClearLiveLog";
            this.btnClearLiveLog.Size = new System.Drawing.Size(55, 20);
            this.btnClearLiveLog.TabIndex = 42;
            this.btnClearLiveLog.Text = "Clear";
            this.btnClearLiveLog.UseVisualStyleBackColor = true;
            this.btnClearLiveLog.Click += new System.EventHandler(this.btnCleanLiveLog_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(852, 483);
            this.pnlMain.TabIndex = 43;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 733);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnClearLiveLog);
            this.Controls.Add(this.gbxLiveLog);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbxLiveLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressMain;
        private System.Windows.Forms.ToolStripStatusLabel lblProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblChekPointResult;
        private System.Windows.Forms.ToolStripStatusLabel lblTcResult;
        private System.Windows.Forms.ToolStripStatusLabel lblInfo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _8960toDUTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dUT1ToDUT2ToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbxLiveLog;
        private System.Windows.Forms.ListView lsvLiveLog;
        private System.Windows.Forms.ColumnHeader chTime;
        private System.Windows.Forms.ColumnHeader chLogLevel;
        private System.Windows.Forms.ColumnHeader chTag;
        private System.Windows.Forms.ColumnHeader chMessage;
        private System.Windows.Forms.Button btnClearLiveLog;
        private System.Windows.Forms.Panel pnlMain;
    }
}