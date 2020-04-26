namespace APC_EO_Finance
{
    partial class FormAEPO
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
            this.components = new System.ComponentModel.Container();
            this.label96 = new System.Windows.Forms.Label();
            this.dataGridViewCA = new System.Windows.Forms.DataGridView();
            this.buttonNewCA = new System.Windows.Forms.Button();
            this.timerDGV = new System.Windows.Forms.Timer(this.components);
            this.buttonHutangKaryawan = new System.Windows.Forms.Button();
            this.buttonDPVendor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCA)).BeginInit();
            this.SuspendLayout();
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Font = new System.Drawing.Font("Arial Narrow", 10.25F, System.Drawing.FontStyle.Bold);
            this.label96.Location = new System.Drawing.Point(12, 620);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(251, 17);
            this.label96.TabIndex = 94;
            this.label96.Text = "Licensed By Abadi Premium Consulting©2019";
            // 
            // dataGridViewCA
            // 
            this.dataGridViewCA.AllowUserToAddRows = false;
            this.dataGridViewCA.AllowUserToDeleteRows = false;
            this.dataGridViewCA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCA.Location = new System.Drawing.Point(12, 13);
            this.dataGridViewCA.Name = "dataGridViewCA";
            this.dataGridViewCA.ReadOnly = true;
            this.dataGridViewCA.Size = new System.Drawing.Size(960, 578);
            this.dataGridViewCA.TabIndex = 95;
            this.dataGridViewCA.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewCA_CellContentClick);
            // 
            // buttonNewCA
            // 
            this.buttonNewCA.Location = new System.Drawing.Point(747, 597);
            this.buttonNewCA.Name = "buttonNewCA";
            this.buttonNewCA.Size = new System.Drawing.Size(225, 40);
            this.buttonNewCA.TabIndex = 96;
            this.buttonNewCA.Text = "New Cash Advance";
            this.buttonNewCA.UseVisualStyleBackColor = true;
            this.buttonNewCA.Click += new System.EventHandler(this.ButtonNewCA_Click);
            // 
            // timerDGV
            // 
            this.timerDGV.Enabled = true;
            this.timerDGV.Tick += new System.EventHandler(this.TimerDGV_Tick);
            // 
            // buttonHutangKaryawan
            // 
            this.buttonHutangKaryawan.Location = new System.Drawing.Point(502, 597);
            this.buttonHutangKaryawan.Name = "buttonHutangKaryawan";
            this.buttonHutangKaryawan.Size = new System.Drawing.Size(239, 40);
            this.buttonHutangKaryawan.TabIndex = 97;
            this.buttonHutangKaryawan.Text = "New Hutang";
            this.buttonHutangKaryawan.UseVisualStyleBackColor = true;
            this.buttonHutangKaryawan.Click += new System.EventHandler(this.Button1_Click);
            // 
            // buttonDPVendor
            // 
            this.buttonDPVendor.Location = new System.Drawing.Point(291, 597);
            this.buttonDPVendor.Name = "buttonDPVendor";
            this.buttonDPVendor.Size = new System.Drawing.Size(205, 40);
            this.buttonDPVendor.TabIndex = 98;
            this.buttonDPVendor.Text = "New DP Vendor";
            this.buttonDPVendor.UseVisualStyleBackColor = true;
            this.buttonDPVendor.Click += new System.EventHandler(this.ButtonDPVendor_Click);
            // 
            // FormAEPO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 646);
            this.Controls.Add(this.buttonDPVendor);
            this.Controls.Add(this.buttonHutangKaryawan);
            this.Controls.Add(this.buttonNewCA);
            this.Controls.Add(this.dataGridViewCA);
            this.Controls.Add(this.label96);
            this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "FormAEPO";
            this.Text = "FormAEPO";
            this.Load += new System.EventHandler(this.FormAEPO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Button buttonNewCA;
        private System.Windows.Forms.Timer timerDGV;
        public System.Windows.Forms.DataGridView dataGridViewCA;
        private System.Windows.Forms.Button buttonHutangKaryawan;
        private System.Windows.Forms.Button buttonDPVendor;
    }
}