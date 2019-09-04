namespace com.usi.shd1_tools.TelephonyAutomation
{
    partial class ucDutTelephonyAutomation
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
            if (procedure != null)
            {
                procedure.Stop();
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("01. DUT1 calls DUT2 and DUT1 hangs up.");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("02. DUT1 calls DUT2 and DUT2 hangs up.");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("03. DUT2 calls DUT1 and DUT1 hangs up.");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("04. DUT2 calls DUT1 and DUT2 hangs up.");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("05. DUT1 calls DUT2 and DUT1 cancels.");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("06. DUT1 calls DUT2 and DUT2 rejects");
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDut2PhoneState = new System.Windows.Forms.TextBox();
            this.txtDut1PhoneState = new System.Windows.Forms.TextBox();
            this.txtDut2ConnectStatus = new System.Windows.Forms.TextBox();
            this.txtDut1ConnectStatus = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDUT2 = new System.Windows.Forms.ComboBox();
            this.cmbDUT1 = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lsvTestcases = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtTcDescripition = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDut2PhoneState);
            this.groupBox2.Controls.Add(this.txtDut1PhoneState);
            this.groupBox2.Controls.Add(this.txtDut2ConnectStatus);
            this.groupBox2.Controls.Add(this.txtDut1ConnectStatus);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbDUT2);
            this.groupBox2.Controls.Add(this.cmbDUT1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(836, 78);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Devices";
            // 
            // txtDut2PhoneState
            // 
            this.txtDut2PhoneState.ForeColor = System.Drawing.Color.Crimson;
            this.txtDut2PhoneState.Location = new System.Drawing.Point(508, 42);
            this.txtDut2PhoneState.Name = "txtDut2PhoneState";
            this.txtDut2PhoneState.Size = new System.Drawing.Size(140, 23);
            this.txtDut2PhoneState.TabIndex = 33;
            this.txtDut2PhoneState.Text = "Phone number???";
            this.txtDut2PhoneState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDut2PhoneState.TextChanged += new System.EventHandler(this.txtDutPhoneState_TextChanged);
            this.txtDut2PhoneState.Enter += new System.EventHandler(this.txtDutPhoneState_Enter);
            this.txtDut2PhoneState.Leave += new System.EventHandler(this.txtDutPhoneState_Leave);
            // 
            // txtDut1PhoneState
            // 
            this.txtDut1PhoneState.ForeColor = System.Drawing.Color.Crimson;
            this.txtDut1PhoneState.Location = new System.Drawing.Point(508, 15);
            this.txtDut1PhoneState.Name = "txtDut1PhoneState";
            this.txtDut1PhoneState.Size = new System.Drawing.Size(140, 23);
            this.txtDut1PhoneState.TabIndex = 32;
            this.txtDut1PhoneState.Text = "Phone number???";
            this.txtDut1PhoneState.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDut1PhoneState.TextChanged += new System.EventHandler(this.txtDutPhoneState_TextChanged);
            this.txtDut1PhoneState.Enter += new System.EventHandler(this.txtDutPhoneState_Enter);
            this.txtDut1PhoneState.Leave += new System.EventHandler(this.txtDutPhoneState_Leave);
            // 
            // txtDut2ConnectStatus
            // 
            this.txtDut2ConnectStatus.Location = new System.Drawing.Point(362, 42);
            this.txtDut2ConnectStatus.Name = "txtDut2ConnectStatus";
            this.txtDut2ConnectStatus.ReadOnly = true;
            this.txtDut2ConnectStatus.Size = new System.Drawing.Size(140, 23);
            this.txtDut2ConnectStatus.TabIndex = 31;
            this.txtDut2ConnectStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDut1ConnectStatus
            // 
            this.txtDut1ConnectStatus.Location = new System.Drawing.Point(362, 15);
            this.txtDut1ConnectStatus.Name = "txtDut1ConnectStatus";
            this.txtDut1ConnectStatus.ReadOnly = true;
            this.txtDut1ConnectStatus.Size = new System.Drawing.Size(140, 23);
            this.txtDut1ConnectStatus.TabIndex = 30;
            this.txtDut1ConnectStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(745, 15);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 48);
            this.btnStart.TabIndex = 29;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 28;
            this.label2.Text = "DUT 2  :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 27;
            this.label1.Text = "DUT  1 :";
            // 
            // cmbDUT2
            // 
            this.cmbDUT2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDUT2.FormattingEnabled = true;
            this.cmbDUT2.Location = new System.Drawing.Point(74, 42);
            this.cmbDUT2.Name = "cmbDUT2";
            this.cmbDUT2.Size = new System.Drawing.Size(278, 23);
            this.cmbDUT2.TabIndex = 0;
            this.cmbDUT2.SelectedIndexChanged += new System.EventHandler(this.cmbDUT2_SelectedIndexChanged);
            // 
            // cmbDUT1
            // 
            this.cmbDUT1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDUT1.FormattingEnabled = true;
            this.cmbDUT1.Location = new System.Drawing.Point(74, 15);
            this.cmbDUT1.Name = "cmbDUT1";
            this.cmbDUT1.Size = new System.Drawing.Size(278, 23);
            this.cmbDUT1.TabIndex = 0;
            this.cmbDUT1.SelectedIndexChanged += new System.EventHandler(this.cmbDUT1_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.splitContainer1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(836, 336);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Test cases";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 19);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lsvTestcases);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtTcDescripition);
            this.splitContainer1.Size = new System.Drawing.Size(830, 314);
            this.splitContainer1.SplitterDistance = 480;
            this.splitContainer1.TabIndex = 4;
            // 
            // lsvTestcases
            // 
            this.lsvTestcases.CheckBoxes = true;
            this.lsvTestcases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvTestcases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvTestcases.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsvTestcases.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem1.StateImageIndex = 0;
            listViewItem1.ToolTipText = "DUT1 calls DUT2 and DUT1 hangs up";
            listViewItem2.StateImageIndex = 0;
            listViewItem2.ToolTipText = "DUT1 calls DUT2 and DUT2 hangs up.";
            listViewItem3.StateImageIndex = 0;
            listViewItem3.ToolTipText = "Terminal calls StationEmulator and terminal hangs up the call.";
            listViewItem4.StateImageIndex = 0;
            listViewItem4.ToolTipText = "DUT2 calls DUT1 and DUT2 hangs up.";
            listViewItem5.StateImageIndex = 0;
            listViewItem5.ToolTipText = "DUT1 calls DUT2 and DUT1 cancels.";
            listViewItem6.StateImageIndex = 0;
            listViewItem6.ToolTipText = "DUT1 calls DUT2 and DUT2 rejects";
            this.lsvTestcases.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.lsvTestcases.Location = new System.Drawing.Point(0, 0);
            this.lsvTestcases.Name = "lsvTestcases";
            this.lsvTestcases.ShowItemToolTips = true;
            this.lsvTestcases.Size = new System.Drawing.Size(480, 314);
            this.lsvTestcases.TabIndex = 0;
            this.lsvTestcases.UseCompatibleStateImageBehavior = false;
            this.lsvTestcases.View = System.Windows.Forms.View.Details;
            this.lsvTestcases.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lsvTestcases_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 400;
            // 
            // txtTcDescripition
            // 
            this.txtTcDescripition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTcDescripition.Location = new System.Drawing.Point(0, 0);
            this.txtTcDescripition.Multiline = true;
            this.txtTcDescripition.Name = "txtTcDescripition";
            this.txtTcDescripition.ReadOnly = true;
            this.txtTcDescripition.Size = new System.Drawing.Size(346, 314);
            this.txtTcDescripition.TabIndex = 4;
            // 
            // ucDutTelephonyAutomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "ucDutTelephonyAutomation";
            this.Size = new System.Drawing.Size(836, 414);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbDUT1;
        private System.Windows.Forms.ComboBox cmbDUT2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lsvTestcases;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TextBox txtTcDescripition;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtDut2ConnectStatus;
        private System.Windows.Forms.TextBox txtDut1ConnectStatus;
        private System.Windows.Forms.TextBox txtDut2PhoneState;
        private System.Windows.Forms.TextBox txtDut1PhoneState;
    }
}
