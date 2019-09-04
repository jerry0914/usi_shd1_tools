namespace AutomationTooling
{
    partial class DockContent_ScriptEditor
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
            this.lsvMain = new System.Windows.Forms.ListView();
            this.chLineNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chContext = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lsvMain
            // 
            this.lsvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLineNumber,
            this.chContext});
            this.lsvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvMain.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvMain.Location = new System.Drawing.Point(0, 0);
            this.lsvMain.Name = "lsvMain";
            this.lsvMain.Size = new System.Drawing.Size(609, 423);
            this.lsvMain.TabIndex = 4;
            this.lsvMain.UseCompatibleStateImageBehavior = false;
            this.lsvMain.View = System.Windows.Forms.View.Details;
            this.lsvMain.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.lsvMain_ControlAdded);
            this.lsvMain.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.lsvMain_ControlRemoved);
            this.lsvMain.Resize += new System.EventHandler(this.lsvMain_Resize);
            // 
            // chLineNumber
            // 
            this.chLineNumber.Width = 40;
            // 
            // chContext
            // 
            this.chContext.Width = 100;
            // 
            // DockContent_ScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 423);
            this.Controls.Add(this.lsvMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DockContent_ScriptEditor";
            this.Text = "DockContent_ScriptEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvMain;
        private System.Windows.Forms.ColumnHeader chLineNumber;
        private System.Windows.Forms.ColumnHeader chContext;
    }
}