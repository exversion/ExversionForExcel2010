namespace Exversion.ExcelAddIn.Dialogs
{
    partial class dlgFilterImportData
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
            this.grdPreview = new System.Windows.Forms.DataGridView();
            this.cmdImport = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.chkExcludeCols = new System.Windows.Forms.CheckBox();
            this.cmdExcludeCols = new System.Windows.Forms.Button();
            this.lblAnd = new System.Windows.Forms.Label();
            this.lblVal2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtValue2 = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lstOperator = new System.Windows.Forms.ComboBox();
            this.lstColumns = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdPreview = new System.Windows.Forms.Button();
            this.chkTrackChanges = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalRows = new System.Windows.Forms.Label();
            this.chkActiveCell = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMaxRecords = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdPreview)).BeginInit();
            this.grpFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdPreview
            // 
            this.grdPreview.AllowUserToAddRows = false;
            this.grdPreview.AllowUserToDeleteRows = false;
            this.grdPreview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPreview.Location = new System.Drawing.Point(12, 279);
            this.grdPreview.Name = "grdPreview";
            this.grdPreview.ReadOnly = true;
            this.grdPreview.RowHeadersWidth = 60;
            this.grdPreview.Size = new System.Drawing.Size(472, 382);
            this.grdPreview.TabIndex = 2;
            // 
            // cmdImport
            // 
            this.cmdImport.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdImport.Location = new System.Drawing.Point(384, 667);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(100, 28);
            this.cmdImport.TabIndex = 10;
            this.cmdImport.Text = "Import";
            this.cmdImport.UseVisualStyleBackColor = true;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(278, 667);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(100, 28);
            this.cmdCancel.TabIndex = 11;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.chkExcludeCols);
            this.grpFilter.Controls.Add(this.cmdExcludeCols);
            this.grpFilter.Controls.Add(this.lblAnd);
            this.grpFilter.Controls.Add(this.lblVal2);
            this.grpFilter.Controls.Add(this.label3);
            this.grpFilter.Controls.Add(this.label2);
            this.grpFilter.Controls.Add(this.label1);
            this.grpFilter.Controls.Add(this.txtValue2);
            this.grpFilter.Controls.Add(this.txtValue);
            this.grpFilter.Controls.Add(this.lstOperator);
            this.grpFilter.Controls.Add(this.lstColumns);
            this.grpFilter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFilter.Location = new System.Drawing.Point(10, 12);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(472, 133);
            this.grpFilter.TabIndex = 5;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Filter : ";
            // 
            // chkExcludeCols
            // 
            this.chkExcludeCols.AutoSize = true;
            this.chkExcludeCols.Location = new System.Drawing.Point(15, 101);
            this.chkExcludeCols.Name = "chkExcludeCols";
            this.chkExcludeCols.Size = new System.Drawing.Size(121, 20);
            this.chkExcludeCols.TabIndex = 29;
            this.chkExcludeCols.Text = "Exclude columns";
            this.chkExcludeCols.UseVisualStyleBackColor = true;
            this.chkExcludeCols.CheckedChanged += new System.EventHandler(this.chkExcludeCols_CheckedChanged);
            // 
            // cmdExcludeCols
            // 
            this.cmdExcludeCols.Enabled = false;
            this.cmdExcludeCols.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExcludeCols.Location = new System.Drawing.Point(147, 92);
            this.cmdExcludeCols.Name = "cmdExcludeCols";
            this.cmdExcludeCols.Size = new System.Drawing.Size(209, 29);
            this.cmdExcludeCols.TabIndex = 9;
            this.cmdExcludeCols.Text = "Select Columns To Exclude..";
            this.cmdExcludeCols.UseVisualStyleBackColor = true;
            this.cmdExcludeCols.Click += new System.EventHandler(this.cmdExcludeCols_Click);
            // 
            // lblAnd
            // 
            this.lblAnd.AutoSize = true;
            this.lblAnd.Location = new System.Drawing.Point(364, 54);
            this.lblAnd.Name = "lblAnd";
            this.lblAnd.Size = new System.Drawing.Size(30, 16);
            this.lblAnd.TabIndex = 24;
            this.lblAnd.Text = "And";
            // 
            // lblVal2
            // 
            this.lblVal2.AutoSize = true;
            this.lblVal2.Location = new System.Drawing.Point(405, 30);
            this.lblVal2.Name = "lblVal2";
            this.lblVal2.Size = new System.Drawing.Size(47, 16);
            this.lblVal2.TabIndex = 25;
            this.lblVal2.Text = "Value2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "Comparator";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "Column";
            // 
            // txtValue2
            // 
            this.txtValue2.Location = new System.Drawing.Point(397, 51);
            this.txtValue2.Name = "txtValue2";
            this.txtValue2.Size = new System.Drawing.Size(60, 23);
            this.txtValue2.TabIndex = 8;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(215, 49);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(141, 23);
            this.txtValue.TabIndex = 7;
            // 
            // lstOperator
            // 
            this.lstOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstOperator.FormattingEnabled = true;
            this.lstOperator.Location = new System.Drawing.Point(147, 48);
            this.lstOperator.Name = "lstOperator";
            this.lstOperator.Size = new System.Drawing.Size(62, 24);
            this.lstOperator.TabIndex = 6;
            this.lstOperator.SelectedIndexChanged += new System.EventHandler(this.lstOperator_SelectedIndexChanged);
            // 
            // lstColumns
            // 
            this.lstColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstColumns.FormattingEnabled = true;
            this.lstColumns.Location = new System.Drawing.Point(15, 48);
            this.lstColumns.Name = "lstColumns";
            this.lstColumns.Size = new System.Drawing.Size(126, 24);
            this.lstColumns.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Sample Preview :";
            // 
            // cmdPreview
            // 
            this.cmdPreview.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPreview.Location = new System.Drawing.Point(342, 237);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(140, 29);
            this.cmdPreview.TabIndex = 9;
            this.cmdPreview.Text = "Generate Preview";
            this.cmdPreview.UseVisualStyleBackColor = true;
            this.cmdPreview.Click += new System.EventHandler(this.cmdPreview_Click);
            // 
            // chkTrackChanges
            // 
            this.chkTrackChanges.AutoSize = true;
            this.chkTrackChanges.Checked = true;
            this.chkTrackChanges.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrackChanges.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTrackChanges.Location = new System.Drawing.Point(24, 177);
            this.chkTrackChanges.Name = "chkTrackChanges";
            this.chkTrackChanges.Size = new System.Drawing.Size(345, 20);
            this.chkTrackChanges.TabIndex = 31;
            this.chkTrackChanges.Text = "Create my own branch for this dataset to track changes";
            this.chkTrackChanges.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(298, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Total available rows :";
            // 
            // lblTotalRows
            // 
            this.lblTotalRows.AutoSize = true;
            this.lblTotalRows.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRows.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalRows.Location = new System.Drawing.Point(431, 212);
            this.lblTotalRows.Name = "lblTotalRows";
            this.lblTotalRows.Size = new System.Drawing.Size(14, 16);
            this.lblTotalRows.TabIndex = 11;
            this.lblTotalRows.Text = "?";
            // 
            // chkActiveCell
            // 
            this.chkActiveCell.AutoSize = true;
            this.chkActiveCell.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActiveCell.Location = new System.Drawing.Point(24, 151);
            this.chkActiveCell.Name = "chkActiveCell";
            this.chkActiveCell.Size = new System.Drawing.Size(380, 20);
            this.chkActiveCell.TabIndex = 31;
            this.chkActiveCell.Text = "Put data starting froml the active cell (otherwise from cell A1)";
            this.chkActiveCell.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Max records to import:";
            // 
            // txtMaxRecords
            // 
            this.txtMaxRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxRecords.Location = new System.Drawing.Point(166, 208);
            this.txtMaxRecords.Name = "txtMaxRecords";
            this.txtMaxRecords.Size = new System.Drawing.Size(60, 21);
            this.txtMaxRecords.TabIndex = 8;
            this.txtMaxRecords.Text = "2500";
            // 
            // dlgFilterImportData
            // 
            this.AcceptButton = this.cmdImport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(494, 701);
            this.Controls.Add(this.chkActiveCell);
            this.Controls.Add(this.chkTrackChanges);
            this.Controls.Add(this.cmdPreview);
            this.Controls.Add(this.lblTotalRows);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMaxRecords);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdImport);
            this.Controls.Add(this.grdPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgFilterImportData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Dataset";
            ((System.ComponentModel.ISupportInitialize)(this.grdPreview)).EndInit();
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdPreview;
        private System.Windows.Forms.Button cmdImport;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.Label lblAnd;
        private System.Windows.Forms.Label lblVal2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtValue2;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox lstOperator;
        private System.Windows.Forms.ComboBox lstColumns;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdPreview;
        private System.Windows.Forms.CheckBox chkExcludeCols;
        private System.Windows.Forms.CheckBox chkTrackChanges;
        private System.Windows.Forms.Button cmdExcludeCols;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalRows;
        private System.Windows.Forms.CheckBox chkActiveCell;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMaxRecords;
    }
}