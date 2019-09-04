namespace AutomationTooling
{
    partial class DockContent_Output
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
            this.lsvLiveLog = new System.Windows.Forms.ListView();
            this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLogLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClearLiveLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            this.lsvLiveLog.Location = new System.Drawing.Point(0, 0);
            this.lsvLiveLog.Name = "lsvLiveLog";
            this.lsvLiveLog.Size = new System.Drawing.Size(732, 221);
            this.lsvLiveLog.TabIndex = 43;
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
            this.btnClearLiveLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLiveLog.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearLiveLog.Location = new System.Drawing.Point(688, 3);
            this.btnClearLiveLog.Name = "btnClearLiveLog";
            this.btnClearLiveLog.Size = new System.Drawing.Size(40, 20);
            this.btnClearLiveLog.TabIndex = 44;
            this.btnClearLiveLog.Text = "Clear";
            this.btnClearLiveLog.UseVisualStyleBackColor = true;
            // 
            // DockContent_Output
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 221);
            this.Controls.Add(this.btnClearLiveLog);
            this.Controls.Add(this.lsvLiveLog);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DockContent_Output";
            this.Text = "Output";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvLiveLog;
        private System.Windows.Forms.ColumnHeader chTime;
        private System.Windows.Forms.ColumnHeader chLogLevel;
        private System.Windows.Forms.ColumnHeader chTag;
        private System.Windows.Forms.ColumnHeader chMessage;
        private System.Windows.Forms.Button btnClearLiveLog;
    }
}