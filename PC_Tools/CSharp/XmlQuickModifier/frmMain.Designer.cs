namespace com.usi.shd1_tools.XmlQuickModifier
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnXmlFolder = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.gvMain = new System.Windows.Forms.DataGridView();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chItemModify = new System.Windows.Forms.DataGridViewButtonColumn();
            this.chName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "XML Foler : ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(88, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(233, 23);
            this.textBox1.TabIndex = 1;
            // 
            // btnXmlFolder
            // 
            this.btnXmlFolder.Location = new System.Drawing.Point(327, 15);
            this.btnXmlFolder.Name = "btnXmlFolder";
            this.btnXmlFolder.Size = new System.Drawing.Size(23, 23);
            this.btnXmlFolder.TabIndex = 2;
            this.btnXmlFolder.Text = "...";
            this.btnXmlFolder.UseVisualStyleBackColor = true;
            this.btnXmlFolder.Click += new System.EventHandler(this.btnXmlFolder_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(246, 290);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(104, 23);
            this.btnReplace.TabIndex = 4;
            this.btnReplace.Text = "Replace All";
            this.btnReplace.UseVisualStyleBackColor = true;
            // 
            // gvMain
            // 
            this.gvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chItemModify,
            this.chName,
            this.chValue});
            this.gvMain.Location = new System.Drawing.Point(15, 47);
            this.gvMain.MultiSelect = false;
            this.gvMain.Name = "gvMain";
            this.gvMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gvMain.RowHeadersVisible = false;
            this.gvMain.Size = new System.Drawing.Size(335, 237);
            this.gvMain.TabIndex = 5;
            this.gvMain.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gvMain_RowsAdded);
            // 
            // chItemModify
            // 
            this.chItemModify.FillWeight = 24F;
            this.chItemModify.Frozen = true;
            this.chItemModify.HeaderText = "";
            this.chItemModify.MinimumWidth = 24;
            this.chItemModify.Name = "chItemModify";
            this.chItemModify.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chItemModify.Text = "";
            this.chItemModify.Width = 24;
            // 
            // chName
            // 
            this.chName.Frozen = true;
            this.chName.HeaderText = "Name";
            this.chName.Name = "chName";
            this.chName.Width = 180;
            // 
            // chValue
            // 
            this.chValue.Frozen = true;
            this.chValue.HeaderText = "Value";
            this.chValue.Name = "chValue";
            this.chValue.Width = 180;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 319);
            this.Controls.Add(this.gvMain);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnXmlFolder);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMain";
            this.Text = "XmlQuickModifier";
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnXmlFolder;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.DataGridView gvMain;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridViewButtonColumn chItemModify;
        private System.Windows.Forms.DataGridViewTextBoxColumn chName;
        private System.Windows.Forms.DataGridViewTextBoxColumn chValue;

    }
}

