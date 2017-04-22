namespace CSP_WinApp
{
    partial class Form1
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
            this.TextBoxMaterialWidth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxMaterialLength = new System.Windows.Forms.TextBox();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.dataGridViewParts = new System.Windows.Forms.DataGridView();
            this.PartWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelPopulation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParts)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width";
            // 
            // TextBoxMaterialWidth
            // 
            this.TextBoxMaterialWidth.Location = new System.Drawing.Point(59, 24);
            this.TextBoxMaterialWidth.Name = "TextBoxMaterialWidth";
            this.TextBoxMaterialWidth.Size = new System.Drawing.Size(46, 20);
            this.TextBoxMaterialWidth.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Length";
            // 
            // TextBoxMaterialLength
            // 
            this.TextBoxMaterialLength.Location = new System.Drawing.Point(171, 24);
            this.TextBoxMaterialLength.Name = "TextBoxMaterialLength";
            this.TextBoxMaterialLength.Size = new System.Drawing.Size(46, 20);
            this.TextBoxMaterialLength.TabIndex = 3;
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(278, 21);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 4;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // dataGridViewParts
            // 
            this.dataGridViewParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PartWidth,
            this.PartLength,
            this.PartCount});
            this.dataGridViewParts.Location = new System.Drawing.Point(13, 56);
            this.dataGridViewParts.Name = "dataGridViewParts";
            this.dataGridViewParts.RowHeadersVisible = false;
            this.dataGridViewParts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewParts.Size = new System.Drawing.Size(340, 150);
            this.dataGridViewParts.TabIndex = 5;
            // 
            // PartWidth
            // 
            this.PartWidth.HeaderText = "Width";
            this.PartWidth.Name = "PartWidth";
            // 
            // PartLength
            // 
            this.PartLength.HeaderText = "Length";
            this.PartLength.Name = "PartLength";
            // 
            // PartCount
            // 
            this.PartCount.HeaderText = "#Part";
            this.PartCount.Name = "PartCount";
            // 
            // labelPopulation
            // 
            this.labelPopulation.AutoSize = true;
            this.labelPopulation.Location = new System.Drawing.Point(12, 222);
            this.labelPopulation.Name = "labelPopulation";
            this.labelPopulation.Size = new System.Drawing.Size(56, 13);
            this.labelPopulation.TabIndex = 6;
            this.labelPopulation.Text = "population";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 297);
            this.Controls.Add(this.labelPopulation);
            this.Controls.Add(this.dataGridViewParts);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.TextBoxMaterialLength);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxMaterialWidth);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxMaterialWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBoxMaterialLength;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.DataGridView dataGridViewParts;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartCount;
        private System.Windows.Forms.Label labelPopulation;
    }
}

