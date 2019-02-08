namespace PatternScanner.UI
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnImport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbBytes = new System.Windows.Forms.TextBox();
            this.txbMask = new System.Windows.Forms.TextBox();
            this.ltvOccurences = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFile = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.lblOccurences = new System.Windows.Forms.Label();
            this.cbbSection = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txbPattern = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.patternTable = new PatternScanner.UI.PatternTable();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lnkUC = new System.Windows.Forms.LinkLabel();
            this.lnkGithub = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.patternTable);
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ltvOccurences);
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(887, 570);
            this.splitContainer1.SplitterDistance = 493;
            this.splitContainer1.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnImport, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbBytes, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txbMask, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txbPattern, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 469);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(493, 101);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(415, 3);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 7;
            this.btnImport.Text = "Import...";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bytes";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mask";
            // 
            // txbBytes
            // 
            this.txbBytes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbBytes.Location = new System.Drawing.Point(50, 4);
            this.txbBytes.Name = "txbBytes";
            this.txbBytes.Size = new System.Drawing.Size(359, 20);
            this.txbBytes.TabIndex = 1;
            // 
            // txbMask
            // 
            this.txbMask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbMask.Location = new System.Drawing.Point(50, 32);
            this.txbMask.Name = "txbMask";
            this.txbMask.Size = new System.Drawing.Size(359, 20);
            this.txbMask.TabIndex = 1;
            this.txbMask.TextChanged += new System.EventHandler(this.txbMask_TextChanged);
            // 
            // ltvOccurences
            // 
            this.ltvOccurences.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ltvOccurences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltvOccurences.Font = new System.Drawing.Font("Courier New", 8.25F);
            this.ltvOccurences.Location = new System.Drawing.Point(0, 0);
            this.ltvOccurences.Name = "ltvOccurences";
            this.ltvOccurences.Size = new System.Drawing.Size(390, 466);
            this.ltvOccurences.TabIndex = 0;
            this.ltvOccurences.UseCompatibleStateImageBehavior = false;
            this.ltvOccurences.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Address";
            this.columnHeader1.Width = 136;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Bytes";
            this.columnHeader2.Width = 239;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFile, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblFile, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnScan, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblOccurences, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbbSection, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.progressBar1, 1, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 466);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(390, 104);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "File";
            // 
            // btnFile
            // 
            this.btnFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFile.Location = new System.Drawing.Point(312, 3);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(75, 23);
            this.btnFile.TabIndex = 1;
            this.btnFile.Text = "...";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // lblFile
            // 
            this.lblFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(74, 8);
            this.lblFile.Margin = new System.Windows.Forms.Padding(3);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(10, 13);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "-";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 36);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Section";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Occurences";
            // 
            // btnScan
            // 
            this.btnScan.Enabled = false;
            this.btnScan.Location = new System.Drawing.Point(312, 78);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 2;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lblOccurences
            // 
            this.lblOccurences.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOccurences.AutoSize = true;
            this.lblOccurences.Location = new System.Drawing.Point(74, 59);
            this.lblOccurences.Margin = new System.Windows.Forms.Padding(3);
            this.lblOccurences.Name = "lblOccurences";
            this.lblOccurences.Size = new System.Drawing.Size(10, 13);
            this.lblOccurences.TabIndex = 0;
            this.lblOccurences.Text = "-";
            // 
            // cbbSection
            // 
            this.cbbSection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSection.FormattingEnabled = true;
            this.cbbSection.Location = new System.Drawing.Point(74, 32);
            this.cbbSection.Name = "cbbSection";
            this.cbbSection.Size = new System.Drawing.Size(232, 21);
            this.cbbSection.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(74, 78);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(232, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // txbPattern
            // 
            this.txbPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txbPattern.Location = new System.Drawing.Point(50, 58);
            this.txbPattern.Name = "txbPattern";
            this.txbPattern.Size = new System.Drawing.Size(359, 20);
            this.txbPattern.TabIndex = 1;
            this.txbPattern.TextChanged += new System.EventHandler(this.txbMask_TextChanged);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Pattern";
            // 
            // patternTable
            // 
            this.patternTable.AutoScroll = true;
            this.patternTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.patternTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.patternTable.CodeFont = new System.Drawing.Font("Courier New", 8.25F);
            this.patternTable.CodeText = null;
            this.patternTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternTable.Location = new System.Drawing.Point(0, 0);
            this.patternTable.Name = "patternTable";
            this.patternTable.Size = new System.Drawing.Size(493, 469);
            this.patternTable.TabIndex = 6;
            this.patternTable.PatternChanged += new System.EventHandler(this.patternTable_PatternChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 3);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.lnkUC, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lnkGithub, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 81);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(493, 20);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // lnkUC
            // 
            this.lnkUC.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkUC.AutoSize = true;
            this.lnkUC.Location = new System.Drawing.Point(22, 3);
            this.lnkUC.Margin = new System.Windows.Forms.Padding(0);
            this.lnkUC.Name = "lnkUC";
            this.lnkUC.Size = new System.Drawing.Size(55, 13);
            this.lnkUC.TabIndex = 0;
            this.lnkUC.TabStop = true;
            this.lnkUC.Text = "Zat @ UC";
            this.lnkUC.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUC_LinkClicked);
            // 
            // lnkGithub
            // 
            this.lnkGithub.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lnkGithub.AutoSize = true;
            this.lnkGithub.Location = new System.Drawing.Point(89, 3);
            this.lnkGithub.Margin = new System.Windows.Forms.Padding(0);
            this.lnkGithub.Name = "lnkGithub";
            this.lnkGithub.Size = new System.Drawing.Size(85, 13);
            this.lnkGithub.TabIndex = 0;
            this.lnkGithub.TabStop = true;
            this.lnkGithub.Text = "BigMo @ Github";
            this.lnkGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGithub_LinkClicked);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "By";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(77, 3);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "/";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 570);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmMain";
            this.Text = "PatternScanner";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private UI.PatternTable patternTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbBytes;
        private System.Windows.Forms.TextBox txbMask;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView ltvOccurences;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Label lblOccurences;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbSection;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txbPattern;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.LinkLabel lnkUC;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lnkGithub;
        private System.Windows.Forms.Label label8;
    }
}

