namespace APC_EO_Finance
{
    partial class FormHutang
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelJabatan = new System.Windows.Forms.Label();
            this.labelNama = new System.Windows.Forms.Label();
            this.numericUpDownHutang = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePickerTanggalKembali = new System.Windows.Forms.DateTimePicker();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHutang)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Jabatan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(116, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 31);
            this.label4.TabIndex = 3;
            this.label4.Text = ":";
            // 
            // labelJabatan
            // 
            this.labelJabatan.AutoSize = true;
            this.labelJabatan.Location = new System.Drawing.Point(146, 46);
            this.labelJabatan.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.labelJabatan.Name = "labelJabatan";
            this.labelJabatan.Size = new System.Drawing.Size(97, 31);
            this.labelJabatan.TabIndex = 4;
            this.labelJabatan.Text = "Jabatan";
            // 
            // labelNama
            // 
            this.labelNama.AutoSize = true;
            this.labelNama.Location = new System.Drawing.Point(146, 11);
            this.labelNama.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.labelNama.Name = "labelNama";
            this.labelNama.Size = new System.Drawing.Size(74, 31);
            this.labelNama.TabIndex = 5;
            this.labelNama.Text = "Nama";
            // 
            // numericUpDownHutang
            // 
            this.numericUpDownHutang.DecimalPlaces = 2;
            this.numericUpDownHutang.Location = new System.Drawing.Point(212, 82);
            this.numericUpDownHutang.Maximum = new decimal(new int[] {
            -559939584,
            902409669,
            54,
            0});
            this.numericUpDownHutang.Name = "numericUpDownHutang";
            this.numericUpDownHutang.Size = new System.Drawing.Size(188, 38);
            this.numericUpDownHutang.TabIndex = 6;
            this.numericUpDownHutang.ThousandsSeparator = true;
            this.numericUpDownHutang.ValueChanged += new System.EventHandler(this.NumericUpDownHutang_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 87);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 31);
            this.label5.TabIndex = 7;
            this.label5.Text = "Hutang";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(116, 86);
            this.label6.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 31);
            this.label6.TabIndex = 8;
            this.label6.Text = ":";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(146, 86);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 31);
            this.label7.TabIndex = 9;
            this.label7.Text = "Rp. ";
            // 
            // dateTimePickerTanggalKembali
            // 
            this.dateTimePickerTanggalKembali.CustomFormat = "dd-MM-yyyy";
            this.dateTimePickerTanggalKembali.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTanggalKembali.Location = new System.Drawing.Point(152, 126);
            this.dateTimePickerTanggalKembali.MinDate = new System.DateTime(2019, 8, 31, 16, 39, 30, 0);
            this.dateTimePickerTanggalKembali.Name = "dateTimePickerTanggalKembali";
            this.dateTimePickerTanggalKembali.Size = new System.Drawing.Size(248, 38);
            this.dateTimePickerTanggalKembali.TabIndex = 10;
            this.dateTimePickerTanggalKembali.Value = new System.DateTime(2019, 8, 31, 16, 39, 30, 0);
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(12, 172);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(186, 62);
            this.buttonSubmit.TabIndex = 11;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.ButtonSubmit_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(116, 129);
            this.label8.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 31);
            this.label8.TabIndex = 13;
            this.label8.Text = ":";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 130);
            this.label9.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 31);
            this.label9.TabIndex = 12;
            this.label9.Text = "Kembali";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(210, 172);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(186, 62);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // FormHutang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 243);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.dateTimePickerTanggalKembali);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownHutang);
            this.Controls.Add(this.labelNama);
            this.Controls.Add(this.labelJabatan);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "FormHutang";
            this.Text = "FormHutang";
            this.Load += new System.EventHandler(this.FormHutang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHutang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelJabatan;
        private System.Windows.Forms.Label labelNama;
        private System.Windows.Forms.NumericUpDown numericUpDownHutang;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePickerTanggalKembali;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonCancel;
    }
}