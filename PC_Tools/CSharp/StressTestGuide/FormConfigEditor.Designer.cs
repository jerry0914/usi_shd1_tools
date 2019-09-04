namespace com.usi.shd1_tools.TestGuide
{
    partial class FormConfigEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfigEditor));
            this.dataGridViewConfigList = new System.Windows.Forms.DataGridView();
            this.ColumnElement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtxtConfig = new System.Windows.Forms.RichTextBox();
            this.btnReplace = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConfigList)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewConfigList
            // 
            this.dataGridViewConfigList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewConfigList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewConfigList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnElement,
            this.ColumnValue});
            this.dataGridViewConfigList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewConfigList.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewConfigList.Name = "dataGridViewConfigList";
            this.dataGridViewConfigList.RowHeadersWidth = 24;
            this.dataGridViewConfigList.RowTemplate.Height = 24;
            this.dataGridViewConfigList.Size = new System.Drawing.Size(661, 100);
            this.dataGridViewConfigList.TabIndex = 4;
            // 
            // ColumnElement
            // 
            this.ColumnElement.HeaderText = "Element";
            this.ColumnElement.Name = "ColumnElement";
            this.ColumnElement.Width = 110;
            // 
            // ColumnValue
            // 
            this.ColumnValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnValue.HeaderText = "Value";
            this.ColumnValue.Name = "ColumnValue";
            this.ColumnValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rtxtConfig);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnReplace);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewConfigList);
            this.splitContainer1.Size = new System.Drawing.Size(661, 513);
            this.splitContainer1.SplitterDistance = 408;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // rtxtConfig
            // 
            this.rtxtConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtConfig.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtConfig.Location = new System.Drawing.Point(0, 0);
            this.rtxtConfig.Name = "rtxtConfig";
            this.rtxtConfig.Size = new System.Drawing.Size(661, 408);
            this.rtxtConfig.TabIndex = 0;
            this.rtxtConfig.Text = "";
            // 
            // btnReplace
            // 
            this.btnReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReplace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnReplace.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplace.Image = global::com.usi.shd1_tools.TestGuide.Properties.Resources.ArrowLeftDouble;
            this.btnReplace.Location = new System.Drawing.Point(558, 1);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(87, 26);
            this.btnReplace.TabIndex = 5;
            this.btnReplace.Text = " Replace";
            this.btnReplace.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReplace.UseVisualStyleBackColor = false;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // FormConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 543);
            this.ControlBox = true;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(341, 335);
            this.Name = "FormConfigEditor";
            this.Text = "Config Editor";
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConfigList)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewConfigList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnElement;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtxtConfig;
        private System.Windows.Forms.Button btnReplace;

    }
}