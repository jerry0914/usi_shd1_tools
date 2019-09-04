namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog_TestPlan = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTestPlanPath1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTestPlanPath2 = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.rtxtResult = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtTarIdFilter = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numRefKeyColumn = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRefKeyword = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numTarCommentColumn = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numTarResultColumn = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numTarIdCol = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.numRefIdCol = new System.Windows.Forms.NumericUpDown();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lvTestPlan1 = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.lvTestPlan2 = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRefKeyColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTarCommentColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTarResultColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTarIdCol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRefIdCol)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog_TestPlan
            // 
            this.openFileDialog_TestPlan.FileName = "openFileDialog1";
            this.openFileDialog_TestPlan.Filter = "sExcel (Old)|*.xls|Excel Files|*.xlsx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Target file :";
            // 
            // txtTestPlanPath1
            // 
            this.txtTestPlanPath1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTestPlanPath1.Location = new System.Drawing.Point(85, 14);
            this.txtTestPlanPath1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTestPlanPath1.Name = "txtTestPlanPath1";
            this.txtTestPlanPath1.ReadOnly = true;
            this.txtTestPlanPath1.Size = new System.Drawing.Size(131, 21);
            this.txtTestPlanPath1.TabIndex = 3;
            this.txtTestPlanPath1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtTestPlanPath_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ref File :";
            // 
            // txtTestPlanPath2
            // 
            this.txtTestPlanPath2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTestPlanPath2.Location = new System.Drawing.Point(71, 48);
            this.txtTestPlanPath2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTestPlanPath2.Name = "txtTestPlanPath2";
            this.txtTestPlanPath2.ReadOnly = true;
            this.txtTestPlanPath2.Size = new System.Drawing.Size(145, 21);
            this.txtTestPlanPath2.TabIndex = 5;
            this.txtTestPlanPath2.DoubleClick += new System.EventHandler(this.txtTestPlanPath2_DoubleClick);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(794, 6);
            this.btnGo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(87, 35);
            this.btnGo.TabIndex = 10;
            this.btnGo.Text = "Compare";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(222, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 18);
            this.label5.TabIndex = 11;
            this.label5.Text = "TCID Column:";
            // 
            // rtxtResult
            // 
            this.rtxtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtResult.Location = new System.Drawing.Point(0, 0);
            this.rtxtResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rtxtResult.Name = "rtxtResult";
            this.rtxtResult.Size = new System.Drawing.Size(884, 633);
            this.rtxtResult.TabIndex = 13;
            this.rtxtResult.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtTarIdFilter);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.numRefKeyColumn);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.txtRefKeyword);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.numTarCommentColumn);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.numTarResultColumn);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.numTarIdCol);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.numRefIdCol);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtTestPlanPath1);
            this.splitContainer1.Panel1.Controls.Add(this.txtTestPlanPath2);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btnGo);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.rtxtResult);
            this.splitContainer1.Size = new System.Drawing.Size(884, 720);
            this.splitContainer1.SplitterDistance = 82;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 14;
            // 
            // txtTarIdFilter
            // 
            this.txtTarIdFilter.Location = new System.Drawing.Point(659, 15);
            this.txtTarIdFilter.Name = "txtTarIdFilter";
            this.txtTarIdFilter.Size = new System.Drawing.Size(120, 22);
            this.txtTarIdFilter.TabIndex = 27;
            this.txtTarIdFilter.Text = "VT262";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(595, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 18);
            this.label12.TabIndex = 26;
            this.label12.Text = "ID filter:";
            // 
            // numRefKeyColumn
            // 
            this.numRefKeyColumn.Location = new System.Drawing.Point(504, 49);
            this.numRefKeyColumn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numRefKeyColumn.Name = "numRefKeyColumn";
            this.numRefKeyColumn.Size = new System.Drawing.Size(51, 22);
            this.numRefKeyColumn.TabIndex = 25;
            this.numRefKeyColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRefKeyColumn.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(382, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 18);
            this.label11.TabIndex = 23;
            this.label11.Text = "Keyword column:";
            // 
            // txtRefKeyword
            // 
            this.txtRefKeyword.Location = new System.Drawing.Point(642, 49);
            this.txtRefKeyword.Name = "txtRefKeyword";
            this.txtRefKeyword.Size = new System.Drawing.Size(137, 22);
            this.txtRefKeyword.TabIndex = 22;
            this.txtRefKeyword.Text = "MC92";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(569, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 18);
            this.label10.TabIndex = 21;
            this.label10.Text = "Keyword:";
            // 
            // numTarCommentColumn
            // 
            this.numTarCommentColumn.Location = new System.Drawing.Point(552, 15);
            this.numTarCommentColumn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numTarCommentColumn.Name = "numTarCommentColumn";
            this.numTarCommentColumn.Size = new System.Drawing.Size(37, 22);
            this.numTarCommentColumn.TabIndex = 20;
            this.numTarCommentColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTarCommentColumn.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(473, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 18);
            this.label9.TabIndex = 19;
            this.label9.Text = "Comment:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(297, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 18);
            this.label8.TabIndex = 18;
            this.label8.Text = "ID:";
            // 
            // numTarResultColumn
            // 
            this.numTarResultColumn.Location = new System.Drawing.Point(430, 14);
            this.numTarResultColumn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numTarResultColumn.Name = "numTarResultColumn";
            this.numTarResultColumn.Size = new System.Drawing.Size(37, 22);
            this.numTarResultColumn.TabIndex = 17;
            this.numTarResultColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTarResultColumn.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(373, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 18);
            this.label3.TabIndex = 16;
            this.label3.Text = "Result:";
            // 
            // numTarIdCol
            // 
            this.numTarIdCol.Location = new System.Drawing.Point(328, 13);
            this.numTarIdCol.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numTarIdCol.Name = "numTarIdCol";
            this.numTarIdCol.Size = new System.Drawing.Size(37, 22);
            this.numTarIdCol.TabIndex = 15;
            this.numTarIdCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTarIdCol.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numTarIdCol.ValueChanged += new System.EventHandler(this.numTarIdCol_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(222, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 18);
            this.label7.TabIndex = 14;
            this.label7.Text = "Colnums->";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(794, 47);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 22);
            this.button1.TabIndex = 13;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numRefIdCol
            // 
            this.numRefIdCol.Location = new System.Drawing.Point(319, 47);
            this.numRefIdCol.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numRefIdCol.Name = "numRefIdCol";
            this.numRefIdCol.Size = new System.Drawing.Size(48, 22);
            this.numRefIdCol.TabIndex = 12;
            this.numRefIdCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numRefIdCol.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvTestPlan1);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lvTestPlan2);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Size = new System.Drawing.Size(884, 633);
            this.splitContainer2.SplitterDistance = 435;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 14;
            // 
            // lvTestPlan1
            // 
            this.lvTestPlan1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTestPlan1.Location = new System.Drawing.Point(0, 18);
            this.lvTestPlan1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lvTestPlan1.Name = "lvTestPlan1";
            this.lvTestPlan1.Size = new System.Drawing.Size(435, 615);
            this.lvTestPlan1.TabIndex = 4;
            this.lvTestPlan1.UseCompatibleStateImageBehavior = false;
            this.lvTestPlan1.View = System.Windows.Forms.View.Details;
            this.lvTestPlan1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvTestPlan1_ItemSelectionChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Target file";
            // 
            // lvTestPlan2
            // 
            this.lvTestPlan2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTestPlan2.Location = new System.Drawing.Point(0, 18);
            this.lvTestPlan2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lvTestPlan2.Name = "lvTestPlan2";
            this.lvTestPlan2.Size = new System.Drawing.Size(444, 615);
            this.lvTestPlan2.TabIndex = 6;
            this.lvTestPlan2.UseCompatibleStateImageBehavior = false;
            this.lvTestPlan2.View = System.Windows.Forms.View.Details;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "Reference File";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "xls File | *.xls";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 720);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft JhengHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRefKeyColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTarCommentColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTarResultColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTarIdCol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRefIdCol)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog_TestPlan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTestPlanPath1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTestPlanPath2;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtxtResult;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.NumericUpDown numRefIdCol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lvTestPlan1;
        private System.Windows.Forms.ListView lvTestPlan2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.NumericUpDown numTarIdCol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numTarResultColumn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numTarCommentColumn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRefKeyword;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numRefKeyColumn;
        private System.Windows.Forms.TextBox txtTarIdFilter;
        private System.Windows.Forms.Label label12;
    }
}

