namespace APC_EO_Finance
{
    partial class FormDPVendor
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
            this.comboBoxProjectDP = new System.Windows.Forms.ComboBox();
            this.label74 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxVendorDP = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.dateTimePickerTanggalDP = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownJumlah = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJumlah)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxProjectDP
            // 
            this.comboBoxProjectDP.FormattingEnabled = true;
            this.comboBoxProjectDP.Location = new System.Drawing.Point(142, 6);
            this.comboBoxProjectDP.Name = "comboBoxProjectDP";
            this.comboBoxProjectDP.Size = new System.Drawing.Size(570, 39);
            this.comboBoxProjectDP.TabIndex = 0;
            this.comboBoxProjectDP.SelectedIndexChanged += new System.EventHandler(this.comboBoxProjectDP_SelectedIndexChanged);
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(113, 9);
            this.label74.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(21, 31);
            this.label74.TabIndex = 25;
            this.label74.Text = ":";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(24, 9);
            this.label59.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(89, 31);
            this.label59.TabIndex = 24;
            this.label59.Text = "Project";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 31);
            this.label1.TabIndex = 28;
            this.label1.Text = ":";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 31);
            this.label2.TabIndex = 27;
            this.label2.Text = "Vendor";
            // 
            // comboBoxVendorDP
            // 
            this.comboBoxVendorDP.FormattingEnabled = true;
            this.comboBoxVendorDP.Location = new System.Drawing.Point(142, 51);
            this.comboBoxVendorDP.Name = "comboBoxVendorDP";
            this.comboBoxVendorDP.Size = new System.Drawing.Size(570, 39);
            this.comboBoxVendorDP.TabIndex = 26;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(232, 185);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(202, 62);
            this.buttonCancel.TabIndex = 37;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(113, 142);
            this.label8.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 31);
            this.label8.TabIndex = 36;
            this.label8.Text = ":";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 143);
            this.label9.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 31);
            this.label9.TabIndex = 35;
            this.label9.Text = "Tanggal";
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(9, 185);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(217, 62);
            this.buttonSubmit.TabIndex = 34;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.ButtonSubmit_Click);
            // 
            // dateTimePickerTanggalDP
            // 
            this.dateTimePickerTanggalDP.CustomFormat = "dd-MM-yyyy";
            this.dateTimePickerTanggalDP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTanggalDP.Location = new System.Drawing.Point(149, 139);
            this.dateTimePickerTanggalDP.MinDate = new System.DateTime(2019, 8, 31, 16, 39, 30, 0);
            this.dateTimePickerTanggalDP.Name = "dateTimePickerTanggalDP";
            this.dateTimePickerTanggalDP.Size = new System.Drawing.Size(285, 38);
            this.dateTimePickerTanggalDP.TabIndex = 33;
            this.dateTimePickerTanggalDP.Value = new System.DateTime(2019, 8, 31, 16, 39, 30, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(143, 99);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 31);
            this.label7.TabIndex = 32;
            this.label7.Text = "Rp. ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(113, 99);
            this.label6.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 31);
            this.label6.TabIndex = 31;
            this.label6.Text = ":";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(68, 100);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 31);
            this.label5.TabIndex = 30;
            this.label5.Text = "DP";
            // 
            // numericUpDownJumlah
            // 
            this.numericUpDownJumlah.DecimalPlaces = 2;
            this.numericUpDownJumlah.Location = new System.Drawing.Point(209, 95);
            this.numericUpDownJumlah.Maximum = new decimal(new int[] {
            -559939584,
            902409669,
            54,
            0});
            this.numericUpDownJumlah.Name = "numericUpDownJumlah";
            this.numericUpDownJumlah.Size = new System.Drawing.Size(225, 38);
            this.numericUpDownJumlah.TabIndex = 29;
            this.numericUpDownJumlah.ThousandsSeparator = true;
            // 
            // FormDPVendor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 263);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.dateTimePickerTanggalDP);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownJumlah);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxVendorDP);
            this.Controls.Add(this.label74);
            this.Controls.Add(this.label59);
            this.Controls.Add(this.comboBoxProjectDP);
            this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "FormDPVendor";
            this.Text = "FormDPVendor";
            this.Load += new System.EventHandler(this.FormDPVendor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownJumlah)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxProjectDP;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxVendorDP;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.DateTimePicker dateTimePickerTanggalDP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownJumlah;
    }
}