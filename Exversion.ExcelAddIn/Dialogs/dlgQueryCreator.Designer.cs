namespace Exversion.ExcelAddIn.Dialogs
{
    partial class dlgQueryCreator
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lstVariable = new System.Windows.Forms.ComboBox();
            this.lstOperator = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtValue2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAnd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(470, 119);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 25);
            this.button1.TabIndex = 15;
            this.button1.Text = "Import";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(351, 119);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 25);
            this.button2.TabIndex = 16;
            this.button2.Text = "Import";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lstVariable
            // 
            this.lstVariable.FormattingEnabled = true;
            this.lstVariable.Location = new System.Drawing.Point(12, 52);
            this.lstVariable.Name = "lstVariable";
            this.lstVariable.Size = new System.Drawing.Size(185, 21);
            this.lstVariable.TabIndex = 17;
            // 
            // lstOperator
            // 
            this.lstOperator.FormattingEnabled = true;
            this.lstOperator.Location = new System.Drawing.Point(203, 52);
            this.lstOperator.Name = "lstOperator";
            this.lstOperator.Size = new System.Drawing.Size(80, 21);
            this.lstOperator.TabIndex = 17;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(289, 51);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(43, 20);
            this.txtValue.TabIndex = 18;
            // 
            // txtValue2
            // 
            this.txtValue2.Location = new System.Drawing.Point(368, 53);
            this.txtValue2.Name = "txtValue2";
            this.txtValue2.Size = new System.Drawing.Size(43, 20);
            this.txtValue2.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Variable";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(286, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "label1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(381, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "label1";
            // 
            // lblAnd
            // 
            this.lblAnd.AutoSize = true;
            this.lblAnd.Location = new System.Drawing.Point(338, 57);
            this.lblAnd.Name = "lblAnd";
            this.lblAnd.Size = new System.Drawing.Size(26, 13);
            this.lblAnd.TabIndex = 19;
            this.lblAnd.Text = "And";
            // 
            // dlgQueryCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 156);
            this.Controls.Add(this.lblAnd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtValue2);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lstOperator);
            this.Controls.Add(this.lstVariable);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "dlgQueryCreator";
            this.Text = "dlgQueryCreator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox lstVariable;
        private System.Windows.Forms.ComboBox lstOperator;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtValue2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAnd;
    }
}