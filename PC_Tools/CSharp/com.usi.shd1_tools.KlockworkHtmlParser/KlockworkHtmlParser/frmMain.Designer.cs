namespace com.usi.shd1_tools.KlockworkHtmlParser
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
            foreach(KlockworkHtmlProcessor processor in lstHtmlProcessors)
            {
                processor.Stop();
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtHtmlDirectory = new System.Windows.Forms.TextBox();
            this.btnParse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnHtmlFolder = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnParseOne = new System.Windows.Forms.Button();
            this.cmbHtmlFiles = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabCtlMain = new System.Windows.Forms.TabControl();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.lblProgress = new System.Windows.Forms.ToolStripLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "HTML Dir : ";
            // 
            // txtHtmlDirectory
            // 
            this.txtHtmlDirectory.Location = new System.Drawing.Point(73, 8);
            this.txtHtmlDirectory.Name = "txtHtmlDirectory";
            this.txtHtmlDirectory.ReadOnly = true;
            this.txtHtmlDirectory.Size = new System.Drawing.Size(255, 22);
            this.txtHtmlDirectory.TabIndex = 1;
            // 
            // btnParse
            // 
            this.btnParse.BackColor = System.Drawing.SystemColors.Control;
            this.btnParse.Enabled = false;
            this.btnParse.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnParse.Location = new System.Drawing.Point(366, 8);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(102, 22);
            this.btnParse.TabIndex = 2;
            this.btnParse.Text = "Parse All";
            this.btnParse.UseVisualStyleBackColor = false;
            this.btnParse.Click += new System.EventHandler(this.btnParseAll_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(474, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Keyword : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(474, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Result : ";
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnExport.Enabled = false;
            this.btnExport.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(531, 33);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(112, 22);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Export this one";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnHtmlFolder
            // 
            this.btnHtmlFolder.Location = new System.Drawing.Point(334, 8);
            this.btnHtmlFolder.Name = "btnHtmlFolder";
            this.btnHtmlFolder.Size = new System.Drawing.Size(26, 22);
            this.btnHtmlFolder.TabIndex = 9;
            this.btnHtmlFolder.Text = "...";
            this.btnHtmlFolder.UseVisualStyleBackColor = true;
            this.btnHtmlFolder.Click += new System.EventHandler(this.btnHtmlFolder_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnConfig);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnParseOne);
            this.panel1.Controls.Add(this.cmbHtmlFiles);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtHtmlDirectory);
            this.panel1.Controls.Add(this.btnHtmlFolder);
            this.panel1.Controls.Add(this.btnParse);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 62);
            this.panel1.TabIndex = 11;
            // 
            // btnConfig
            // 
            this.btnConfig.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfig.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfig.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConfig.Location = new System.Drawing.Point(531, 7);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(112, 22);
            this.btnConfig.TabIndex = 13;
            this.btnConfig.Text = "Detail";
            this.btnConfig.UseVisualStyleBackColor = false;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnParseOne
            // 
            this.btnParseOne.BackColor = System.Drawing.SystemColors.Control;
            this.btnParseOne.Enabled = false;
            this.btnParseOne.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParseOne.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnParseOne.Location = new System.Drawing.Point(366, 34);
            this.btnParseOne.Name = "btnParseOne";
            this.btnParseOne.Size = new System.Drawing.Size(102, 22);
            this.btnParseOne.TabIndex = 12;
            this.btnParseOne.Text = "Parse this one";
            this.btnParseOne.UseVisualStyleBackColor = false;
            this.btnParseOne.Click += new System.EventHandler(this.btnParseOne_Click);
            // 
            // cmbHtmlFiles
            // 
            this.cmbHtmlFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHtmlFiles.FormattingEnabled = true;
            this.cmbHtmlFiles.Location = new System.Drawing.Point(73, 34);
            this.cmbHtmlFiles.Name = "cmbHtmlFiles";
            this.cmbHtmlFiles.Size = new System.Drawing.Size(287, 22);
            this.cmbHtmlFiles.TabIndex = 11;
            this.cmbHtmlFiles.SelectedIndexChanged += new System.EventHandler(this.cmbHtmlFiles_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 14);
            this.label5.TabIndex = 10;
            this.label5.Text = "HTML File";
            // 
            // tabCtlMain
            // 
            this.tabCtlMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabCtlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtlMain.Location = new System.Drawing.Point(0, 62);
            this.tabCtlMain.Multiline = true;
            this.tabCtlMain.Name = "tabCtlMain";
            this.tabCtlMain.SelectedIndex = 0;
            this.tabCtlMain.Size = new System.Drawing.Size(658, 468);
            this.tabCtlMain.TabIndex = 10;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar1,
            this.lblProgress});
            this.toolStrip1.Location = new System.Drawing.Point(0, 530);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(658, 20);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(180, 17);
            this.progressBar1.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(25, 17);
            this.lblProgress.Text = "123";
            this.lblProgress.Visible = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel File|*.xlsx";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 550);
            this.Controls.Add(this.tabCtlMain);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMain";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHtmlDirectory;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnHtmlFolder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabCtlMain;
        private System.Windows.Forms.ComboBox cmbHtmlFiles;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
        private System.Windows.Forms.ToolStripLabel lblProgress;
        private System.Windows.Forms.Button btnParseOne;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnConfig;
    }
}

