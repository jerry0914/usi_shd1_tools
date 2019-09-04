namespace com.usi.shd1_tools.RobotframeworkTestGuide
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
            this.btnRf_Run = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.functionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.generateScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runningModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.gpbDevices = new System.Windows.Forms.GroupBox();
            this.cbbSecondaryDevice3 = new System.Windows.Forms.ComboBox();
            this.ckbSecondaryDevice3 = new System.Windows.Forms.CheckBox();
            this.cbbSecondaryDevice2 = new System.Windows.Forms.ComboBox();
            this.ckbSecondaryDevice2 = new System.Windows.Forms.CheckBox();
            this.cbbSecondaryDevice1 = new System.Windows.Forms.ComboBox();
            this.ckbSecondaryDevice1 = new System.Windows.Forms.CheckBox();
            this.cbbPrimaryDevice = new System.Windows.Forms.ComboBox();
            this.ckbRf_TestPlanPath = new System.Windows.Forms.CheckBox();
            this.txtRf_ScrpitPath = new System.Windows.Forms.TextBox();
            this.btnScriptBrowser = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ckbRf_RerunFailed = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslNotification1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslDownloading = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtRf_OtherArguments = new System.Windows.Forms.TextBox();
            this.txtRf_RerunFailed = new System.Windows.Forms.TextBox();
            this.txtRf_TestPlanPath = new System.Windows.Forms.TextBox();
            this.ckbRf_OtherArguments = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbbDisplayType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbbScannerType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbTestPlatform = new System.Windows.Forms.ComboBox();
            this.txtLegacyId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEs_TrsPathBrowser = new System.Windows.Forms.Button();
            this.btnEs_Run = new System.Windows.Forms.Button();
            this.ckbEs_RefunFailed = new System.Windows.Forms.CheckBox();
            this.txtEs_TestPlanPath = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.gpbDevices.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRf_Run
            // 
            this.btnRf_Run.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRf_Run.Location = new System.Drawing.Point(313, 37);
            this.btnRf_Run.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRf_Run.Name = "btnRf_Run";
            this.btnRf_Run.Size = new System.Drawing.Size(76, 78);
            this.btnRf_Run.TabIndex = 0;
            this.btnRf_Run.Text = "Run";
            this.btnRf_Run.UseVisualStyleBackColor = true;
            this.btnRf_Run.Click += new System.EventHandler(this.btnRfRun_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.functionToolStripMenuItem,
            this.runningModeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(403, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripMenuItem.Image = global::com.usi.shd1_tools.RobotframeworkTestGuide.Properties.Resources.help_16;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.toolSettingsToolStripMenuItem_Click);
            // 
            // functionToolStripMenuItem
            // 
            this.functionToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.functionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.syncScriptsToolStripMenuItem,
            this.cleanUpToolStripMenuItem,
            this.toolStripSeparator1,
            this.generateScriptToolStripMenuItem,
            this.toolStripSeparator2,
            this.mergeToolStripMenuItem});
            this.functionToolStripMenuItem.Name = "functionToolStripMenuItem";
            this.functionToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.functionToolStripMenuItem.Text = "Functions";
            // 
            // syncScriptsToolStripMenuItem
            // 
            this.syncScriptsToolStripMenuItem.Name = "syncScriptsToolStripMenuItem";
            this.syncScriptsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.syncScriptsToolStripMenuItem.Text = "Sync Scripts";
            this.syncScriptsToolStripMenuItem.Click += new System.EventHandler(this.syncScriptsToolStripMenuItem_Click);
            // 
            // cleanUpToolStripMenuItem
            // 
            this.cleanUpToolStripMenuItem.Name = "cleanUpToolStripMenuItem";
            this.cleanUpToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.cleanUpToolStripMenuItem.Text = "Cleanup Scripts";
            this.cleanUpToolStripMenuItem.Click += new System.EventHandler(this.cleanUpToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // generateScriptToolStripMenuItem
            // 
            this.generateScriptToolStripMenuItem.Name = "generateScriptToolStripMenuItem";
            this.generateScriptToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.generateScriptToolStripMenuItem.Text = "Generate Script";
            this.generateScriptToolStripMenuItem.Visible = false;
            this.generateScriptToolStripMenuItem.Click += new System.EventHandler(this.generateScriptToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.mergeToolStripMenuItem.Text = "Merge Result";
            this.mergeToolStripMenuItem.Click += new System.EventHandler(this.mergeToolStripMenuItem_Click);
            // 
            // runningModeToolStripMenuItem
            // 
            this.runningModeToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.runningModeToolStripMenuItem.Name = "runningModeToolStripMenuItem";
            this.runningModeToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.runningModeToolStripMenuItem.Text = "Running Mode";
            this.runningModeToolStripMenuItem.Click += new System.EventHandler(this.runningModeToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Primary:";
            // 
            // gpbDevices
            // 
            this.gpbDevices.Controls.Add(this.cbbSecondaryDevice3);
            this.gpbDevices.Controls.Add(this.ckbSecondaryDevice3);
            this.gpbDevices.Controls.Add(this.cbbSecondaryDevice2);
            this.gpbDevices.Controls.Add(this.ckbSecondaryDevice2);
            this.gpbDevices.Controls.Add(this.cbbSecondaryDevice1);
            this.gpbDevices.Controls.Add(this.ckbSecondaryDevice1);
            this.gpbDevices.Controls.Add(this.cbbPrimaryDevice);
            this.gpbDevices.Controls.Add(this.label2);
            this.gpbDevices.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpbDevices.Location = new System.Drawing.Point(0, 24);
            this.gpbDevices.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gpbDevices.Name = "gpbDevices";
            this.gpbDevices.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.gpbDevices.Size = new System.Drawing.Size(403, 142);
            this.gpbDevices.TabIndex = 6;
            this.gpbDevices.TabStop = false;
            this.gpbDevices.Text = "Devices";
            // 
            // cbbSecondaryDevice3
            // 
            this.cbbSecondaryDevice3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSecondaryDevice3.Enabled = false;
            this.cbbSecondaryDevice3.FormattingEnabled = true;
            this.cbbSecondaryDevice3.Location = new System.Drawing.Point(107, 106);
            this.cbbSecondaryDevice3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbbSecondaryDevice3.Name = "cbbSecondaryDevice3";
            this.cbbSecondaryDevice3.Size = new System.Drawing.Size(282, 22);
            this.cbbSecondaryDevice3.TabIndex = 12;
            // 
            // ckbSecondaryDevice3
            // 
            this.ckbSecondaryDevice3.AutoSize = true;
            this.ckbSecondaryDevice3.Location = new System.Drawing.Point(12, 109);
            this.ckbSecondaryDevice3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckbSecondaryDevice3.Name = "ckbSecondaryDevice3";
            this.ckbSecondaryDevice3.Size = new System.Drawing.Size(93, 18);
            this.ckbSecondaryDevice3.TabIndex = 11;
            this.ckbSecondaryDevice3.Text = "Secondary 3:";
            this.ckbSecondaryDevice3.UseVisualStyleBackColor = true;
            this.ckbSecondaryDevice3.CheckedChanged += new System.EventHandler(this.ckbSecondaryDevice3_CheckedChanged);
            // 
            // cbbSecondaryDevice2
            // 
            this.cbbSecondaryDevice2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSecondaryDevice2.Enabled = false;
            this.cbbSecondaryDevice2.FormattingEnabled = true;
            this.cbbSecondaryDevice2.Location = new System.Drawing.Point(107, 77);
            this.cbbSecondaryDevice2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbbSecondaryDevice2.Name = "cbbSecondaryDevice2";
            this.cbbSecondaryDevice2.Size = new System.Drawing.Size(282, 22);
            this.cbbSecondaryDevice2.TabIndex = 10;
            // 
            // ckbSecondaryDevice2
            // 
            this.ckbSecondaryDevice2.AutoSize = true;
            this.ckbSecondaryDevice2.Location = new System.Drawing.Point(12, 79);
            this.ckbSecondaryDevice2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckbSecondaryDevice2.Name = "ckbSecondaryDevice2";
            this.ckbSecondaryDevice2.Size = new System.Drawing.Size(93, 18);
            this.ckbSecondaryDevice2.TabIndex = 9;
            this.ckbSecondaryDevice2.Text = "Secondary 2:";
            this.ckbSecondaryDevice2.UseVisualStyleBackColor = true;
            this.ckbSecondaryDevice2.CheckedChanged += new System.EventHandler(this.ckbSecondaryDevice2_CheckedChanged);
            // 
            // cbbSecondaryDevice1
            // 
            this.cbbSecondaryDevice1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSecondaryDevice1.Enabled = false;
            this.cbbSecondaryDevice1.FormattingEnabled = true;
            this.cbbSecondaryDevice1.Location = new System.Drawing.Point(107, 49);
            this.cbbSecondaryDevice1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbbSecondaryDevice1.Name = "cbbSecondaryDevice1";
            this.cbbSecondaryDevice1.Size = new System.Drawing.Size(282, 22);
            this.cbbSecondaryDevice1.TabIndex = 8;
            // 
            // ckbSecondaryDevice1
            // 
            this.ckbSecondaryDevice1.AutoSize = true;
            this.ckbSecondaryDevice1.Location = new System.Drawing.Point(12, 50);
            this.ckbSecondaryDevice1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckbSecondaryDevice1.Name = "ckbSecondaryDevice1";
            this.ckbSecondaryDevice1.Size = new System.Drawing.Size(93, 18);
            this.ckbSecondaryDevice1.TabIndex = 7;
            this.ckbSecondaryDevice1.Text = "Secondary 1:";
            this.ckbSecondaryDevice1.UseVisualStyleBackColor = true;
            this.ckbSecondaryDevice1.CheckedChanged += new System.EventHandler(this.ckbSecondaryDevice1_CheckedChanged);
            // 
            // cbbPrimaryDevice
            // 
            this.cbbPrimaryDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPrimaryDevice.FormattingEnabled = true;
            this.cbbPrimaryDevice.Location = new System.Drawing.Point(107, 20);
            this.cbbPrimaryDevice.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbbPrimaryDevice.Name = "cbbPrimaryDevice";
            this.cbbPrimaryDevice.Size = new System.Drawing.Size(282, 22);
            this.cbbPrimaryDevice.TabIndex = 6;
            // 
            // ckbRf_TestPlanPath
            // 
            this.ckbRf_TestPlanPath.AutoSize = true;
            this.ckbRf_TestPlanPath.Location = new System.Drawing.Point(11, 67);
            this.ckbRf_TestPlanPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckbRf_TestPlanPath.Name = "ckbRf_TestPlanPath";
            this.ckbRf_TestPlanPath.Size = new System.Drawing.Size(68, 18);
            this.ckbRf_TestPlanPath.TabIndex = 21;
            this.ckbRf_TestPlanPath.Text = "TRS file:";
            this.ckbRf_TestPlanPath.UseVisualStyleBackColor = true;
            this.ckbRf_TestPlanPath.CheckedChanged += new System.EventHandler(this.ckbTestPlanPath_CheckedChanged);
            // 
            // txtRf_ScrpitPath
            // 
            this.txtRf_ScrpitPath.Enabled = false;
            this.txtRf_ScrpitPath.Location = new System.Drawing.Point(112, 9);
            this.txtRf_ScrpitPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtRf_ScrpitPath.Name = "txtRf_ScrpitPath";
            this.txtRf_ScrpitPath.ReadOnly = true;
            this.txtRf_ScrpitPath.Size = new System.Drawing.Size(251, 22);
            this.txtRf_ScrpitPath.TabIndex = 24;
            this.txtRf_ScrpitPath.DoubleClick += new System.EventHandler(this.btnScriptBrowser_Click);
            // 
            // btnScriptBrowser
            // 
            this.btnScriptBrowser.Enabled = false;
            this.btnScriptBrowser.Location = new System.Drawing.Point(368, 9);
            this.btnScriptBrowser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnScriptBrowser.Name = "btnScriptBrowser";
            this.btnScriptBrowser.Size = new System.Drawing.Size(23, 22);
            this.btnScriptBrowser.TabIndex = 23;
            this.btnScriptBrowser.Text = "...";
            this.btnScriptBrowser.UseVisualStyleBackColor = true;
            this.btnScriptBrowser.Click += new System.EventHandler(this.btnScriptBrowser_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ckbRf_RerunFailed
            // 
            this.ckbRf_RerunFailed.AutoSize = true;
            this.ckbRf_RerunFailed.Location = new System.Drawing.Point(11, 39);
            this.ckbRf_RerunFailed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckbRf_RerunFailed.Name = "ckbRf_RerunFailed";
            this.ckbRf_RerunFailed.Size = new System.Drawing.Size(99, 18);
            this.ckbRf_RerunFailed.TabIndex = 26;
            this.ckbRf_RerunFailed.Text = "Rerun Failed:";
            this.ckbRf_RerunFailed.UseVisualStyleBackColor = true;
            this.ckbRf_RerunFailed.CheckedChanged += new System.EventHandler(this.ckbRerunFailed_CheckedChanged_1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslNotification1,
            this.tslSpace,
            this.tslDownloading});
            this.statusStrip1.Location = new System.Drawing.Point(0, 464);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(403, 22);
            this.statusStrip1.TabIndex = 29;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslNotification1
            // 
            this.tslNotification1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslNotification1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tslNotification1.Name = "tslNotification1";
            this.tslNotification1.Size = new System.Drawing.Size(0, 17);
            this.tslNotification1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tslSpace
            // 
            this.tslSpace.Name = "tslSpace";
            this.tslSpace.Size = new System.Drawing.Size(388, 17);
            this.tslSpace.Spring = true;
            // 
            // tslDownloading
            // 
            this.tslDownloading.BackColor = System.Drawing.Color.Green;
            this.tslDownloading.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslDownloading.ForeColor = System.Drawing.Color.White;
            this.tslDownloading.Name = "tslDownloading";
            this.tslDownloading.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tslDownloading.Size = new System.Drawing.Size(89, 17);
            this.tslDownloading.Text = "Downloading...";
            this.tslDownloading.Visible = false;
            // 
            // txtRf_OtherArguments
            // 
            this.txtRf_OtherArguments.Location = new System.Drawing.Point(112, 93);
            this.txtRf_OtherArguments.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtRf_OtherArguments.Name = "txtRf_OtherArguments";
            this.txtRf_OtherArguments.Size = new System.Drawing.Size(189, 22);
            this.txtRf_OtherArguments.TabIndex = 31;
            this.txtRf_OtherArguments.Visible = false;
            // 
            // txtRf_RerunFailed
            // 
            this.txtRf_RerunFailed.Location = new System.Drawing.Point(112, 37);
            this.txtRf_RerunFailed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtRf_RerunFailed.Name = "txtRf_RerunFailed";
            this.txtRf_RerunFailed.ReadOnly = true;
            this.txtRf_RerunFailed.Size = new System.Drawing.Size(189, 22);
            this.txtRf_RerunFailed.TabIndex = 32;
            this.txtRf_RerunFailed.Visible = false;
            this.txtRf_RerunFailed.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtRerunFailed_MouseDoubleClick);
            // 
            // txtRf_TestPlanPath
            // 
            this.txtRf_TestPlanPath.Location = new System.Drawing.Point(112, 65);
            this.txtRf_TestPlanPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtRf_TestPlanPath.Name = "txtRf_TestPlanPath";
            this.txtRf_TestPlanPath.ReadOnly = true;
            this.txtRf_TestPlanPath.Size = new System.Drawing.Size(189, 22);
            this.txtRf_TestPlanPath.TabIndex = 33;
            this.txtRf_TestPlanPath.Visible = false;
            this.txtRf_TestPlanPath.DoubleClick += new System.EventHandler(this.txtTestPlanPath_DoubleClick);
            // 
            // ckbRf_OtherArguments
            // 
            this.ckbRf_OtherArguments.AutoSize = true;
            this.ckbRf_OtherArguments.Location = new System.Drawing.Point(11, 95);
            this.ckbRf_OtherArguments.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckbRf_OtherArguments.Name = "ckbRf_OtherArguments";
            this.ckbRf_OtherArguments.Size = new System.Drawing.Size(85, 18);
            this.ckbRf_OtherArguments.TabIndex = 34;
            this.ckbRf_OtherArguments.Text = "Other Args:";
            this.ckbRf_OtherArguments.UseVisualStyleBackColor = true;
            this.ckbRf_OtherArguments.CheckedChanged += new System.EventHandler(this.ckbOtherArguments_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 35;
            this.label1.Text = "Robot Script:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 166);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbbDisplayType);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.cbbScannerType);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.cbbTestPlatform);
            this.splitContainer1.Panel1.Controls.Add(this.txtLegacyId);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btnEs_TrsPathBrowser);
            this.splitContainer1.Panel1.Controls.Add(this.btnEs_Run);
            this.splitContainer1.Panel1.Controls.Add(this.ckbEs_RefunFailed);
            this.splitContainer1.Panel1.Controls.Add(this.txtEs_TestPlanPath);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.btnRf_Run);
            this.splitContainer1.Panel2.Controls.Add(this.ckbRf_OtherArguments);
            this.splitContainer1.Panel2.Controls.Add(this.ckbRf_RerunFailed);
            this.splitContainer1.Panel2.Controls.Add(this.ckbRf_TestPlanPath);
            this.splitContainer1.Panel2.Controls.Add(this.txtRf_OtherArguments);
            this.splitContainer1.Panel2.Controls.Add(this.txtRf_TestPlanPath);
            this.splitContainer1.Panel2.Controls.Add(this.txtRf_ScrpitPath);
            this.splitContainer1.Panel2.Controls.Add(this.btnScriptBrowser);
            this.splitContainer1.Panel2.Controls.Add(this.txtRf_RerunFailed);
            this.splitContainer1.Size = new System.Drawing.Size(403, 298);
            this.splitContainer1.SplitterDistance = 164;
            this.splitContainer1.TabIndex = 37;
            // 
            // cbbDisplayType
            // 
            this.cbbDisplayType.FormattingEnabled = true;
            this.cbbDisplayType.Items.AddRange(new object[] {
            "LCD",
            "E-INK"});
            this.cbbDisplayType.Location = new System.Drawing.Point(107, 117);
            this.cbbDisplayType.Name = "cbbDisplayType";
            this.cbbDisplayType.Size = new System.Drawing.Size(194, 22);
            this.cbbDisplayType.TabIndex = 52;
            this.cbbDisplayType.Text = "LCD";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 14);
            this.label6.TabIndex = 51;
            this.label6.Text = "Display Type :";
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
            this.cbbScannerType.Location = new System.Drawing.Point(107, 63);
            this.cbbScannerType.Name = "cbbScannerType";
            this.cbbScannerType.Size = new System.Drawing.Size(194, 22);
            this.cbbScannerType.TabIndex = 50;
            this.cbbScannerType.Text = "2D_Imager";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 14);
            this.label5.TabIndex = 49;
            this.label5.Text = "Scanner Type :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 14);
            this.label4.TabIndex = 48;
            this.label4.Text = "Platform :";
            // 
            // cbbTestPlatform
            // 
            this.cbbTestPlatform.FormattingEnabled = true;
            this.cbbTestPlatform.Items.AddRange(new object[] {
            "Android",
            "Xamarin"});
            this.cbbTestPlatform.Location = new System.Drawing.Point(107, 90);
            this.cbbTestPlatform.Name = "cbbTestPlatform";
            this.cbbTestPlatform.Size = new System.Drawing.Size(194, 22);
            this.cbbTestPlatform.TabIndex = 47;
            this.cbbTestPlatform.Text = "Android";
            // 
            // txtLegacyId
            // 
            this.txtLegacyId.Location = new System.Drawing.Point(107, 37);
            this.txtLegacyId.Name = "txtLegacyId";
            this.txtLegacyId.Size = new System.Drawing.Size(194, 22);
            this.txtLegacyId.TabIndex = 46;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 14);
            this.label7.TabIndex = 45;
            this.label7.Text = "Legacy ID :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 14);
            this.label3.TabIndex = 13;
            this.label3.Text = "TRS file:";
            // 
            // btnEs_TrsPathBrowser
            // 
            this.btnEs_TrsPathBrowser.Location = new System.Drawing.Point(368, 9);
            this.btnEs_TrsPathBrowser.Name = "btnEs_TrsPathBrowser";
            this.btnEs_TrsPathBrowser.Size = new System.Drawing.Size(22, 22);
            this.btnEs_TrsPathBrowser.TabIndex = 44;
            this.btnEs_TrsPathBrowser.Text = "...";
            this.btnEs_TrsPathBrowser.UseVisualStyleBackColor = true;
            this.btnEs_TrsPathBrowser.Click += new System.EventHandler(this.btnEs_TrsPathBrowser_Click);
            // 
            // btnEs_Run
            // 
            this.btnEs_Run.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEs_Run.Location = new System.Drawing.Point(313, 61);
            this.btnEs_Run.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnEs_Run.Name = "btnEs_Run";
            this.btnEs_Run.Size = new System.Drawing.Size(76, 78);
            this.btnEs_Run.TabIndex = 36;
            this.btnEs_Run.Text = "Run";
            this.btnEs_Run.UseVisualStyleBackColor = true;
            this.btnEs_Run.Click += new System.EventHandler(this.btnEs_Run_Click);
            // 
            // ckbEs_RefunFailed
            // 
            this.ckbEs_RefunFailed.AutoSize = true;
            this.ckbEs_RefunFailed.Location = new System.Drawing.Point(12, 145);
            this.ckbEs_RefunFailed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckbEs_RefunFailed.Name = "ckbEs_RefunFailed";
            this.ckbEs_RefunFailed.Size = new System.Drawing.Size(99, 18);
            this.ckbEs_RefunFailed.TabIndex = 40;
            this.ckbEs_RefunFailed.Text = "Rerun Failed:";
            this.ckbEs_RefunFailed.UseVisualStyleBackColor = true;
            // 
            // txtEs_TestPlanPath
            // 
            this.txtEs_TestPlanPath.Location = new System.Drawing.Point(107, 9);
            this.txtEs_TestPlanPath.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtEs_TestPlanPath.Name = "txtEs_TestPlanPath";
            this.txtEs_TestPlanPath.ReadOnly = true;
            this.txtEs_TestPlanPath.Size = new System.Drawing.Size(256, 22);
            this.txtEs_TestPlanPath.TabIndex = 43;
            this.txtEs_TestPlanPath.DoubleClick += new System.EventHandler(this.btnEs_TrsPathBrowser_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 486);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gpbDevices);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Robotframework Test Guide";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gpbDevices.ResumeLayout(false);
            this.gpbDevices.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRf_Run;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gpbDevices;
        private System.Windows.Forms.ComboBox cbbSecondaryDevice3;
        private System.Windows.Forms.CheckBox ckbSecondaryDevice3;
        private System.Windows.Forms.ComboBox cbbSecondaryDevice2;
        private System.Windows.Forms.CheckBox ckbSecondaryDevice2;
        private System.Windows.Forms.ComboBox cbbSecondaryDevice1;
        private System.Windows.Forms.CheckBox ckbSecondaryDevice1;
        private System.Windows.Forms.ComboBox cbbPrimaryDevice;
        private System.Windows.Forms.CheckBox ckbRf_TestPlanPath;
        private System.Windows.Forms.TextBox txtRf_ScrpitPath;
        private System.Windows.Forms.Button btnScriptBrowser;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem functionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem syncScriptsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;
        private System.Windows.Forms.CheckBox ckbRf_RerunFailed;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslNotification1;
        private System.Windows.Forms.ToolStripMenuItem cleanUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel tslDownloading;
        private System.Windows.Forms.ToolStripStatusLabel tslSpace;
        private System.Windows.Forms.TextBox txtRf_OtherArguments;
        private System.Windows.Forms.TextBox txtRf_RerunFailed;
        private System.Windows.Forms.TextBox txtRf_TestPlanPath;
        private System.Windows.Forms.CheckBox ckbRf_OtherArguments;
        private System.Windows.Forms.ToolStripMenuItem generateScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem runningModeToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEs_TrsPathBrowser;
        private System.Windows.Forms.Button btnEs_Run;
        private System.Windows.Forms.CheckBox ckbEs_RefunFailed;
        private System.Windows.Forms.TextBox txtEs_TestPlanPath;
        private System.Windows.Forms.ComboBox cbbDisplayType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbbScannerType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbTestPlatform;
        private System.Windows.Forms.TextBox txtLegacyId;
        private System.Windows.Forms.Label label7;
    }
}

