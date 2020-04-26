namespace APC_EO_Finance
{
    partial class FormPenggajian
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
            this.buttonSubForward = new System.Windows.Forms.Button();
            this.dataGridViewGaji = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGaji)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSubForward
            // 
            this.buttonSubForward.Location = new System.Drawing.Point(789, 484);
            this.buttonSubForward.Name = "buttonSubForward";
            this.buttonSubForward.Size = new System.Drawing.Size(183, 70);
            this.buttonSubForward.TabIndex = 96;
            this.buttonSubForward.Text = "Submit";
            this.buttonSubForward.UseVisualStyleBackColor = true;
            this.buttonSubForward.Click += new System.EventHandler(this.ButtonSubForward_Click);
            // 
            // dataGridViewGaji
            // 
            this.dataGridViewGaji.AllowUserToAddRows = false;
            this.dataGridViewGaji.AllowUserToDeleteRows = false;
            this.dataGridViewGaji.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGaji.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewGaji.Name = "dataGridViewGaji";
            this.dataGridViewGaji.Size = new System.Drawing.Size(960, 466);
            this.dataGridViewGaji.TabIndex = 95;
            // 
            // FormPenggajian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.buttonSubForward);
            this.Controls.Add(this.dataGridViewGaji);
            this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Name = "FormPenggajian";
            this.Text = "FormPenggajian";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPenggajian_FormClosing);
            this.Load += new System.EventHandler(this.FormPenggajian_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGaji)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSubForward;
        private System.Windows.Forms.DataGridView dataGridViewGaji;
    }
}