namespace APC_EO_Finance
{
    partial class FormTampilSementara
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewTampil = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTampil)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTampil
            // 
            this.dataGridViewTampil.AllowUserToAddRows = false;
            this.dataGridViewTampil.AllowUserToDeleteRows = false;
            this.dataGridViewTampil.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 17.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTampil.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTampil.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTampil.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewTampil.Margin = new System.Windows.Forms.Padding(7);
            this.dataGridViewTampil.Name = "dataGridViewTampil";
            this.dataGridViewTampil.ReadOnly = true;
            this.dataGridViewTampil.Size = new System.Drawing.Size(984, 661);
            this.dataGridViewTampil.TabIndex = 0;
            // 
            // FormTampilSementara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.dataGridViewTampil);
            this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "FormTampilSementara";
            this.Text = "FormTampilSementara";
            this.Load += new System.EventHandler(this.FormTampilSementara_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTampil)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewTampil;
    }
}