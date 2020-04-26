namespace APC_EO_Finance
{
    partial class FormTambahAkun
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
            this.comboBoxParent = new System.Windows.Forms.ComboBox();
            this.textBoxKodeGen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxNama = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownNominal = new System.Windows.Forms.NumericUpDown();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNominal)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = ":";
            // 
            // comboBoxParent
            // 
            this.comboBoxParent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxParent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxParent.FormattingEnabled = true;
            this.comboBoxParent.Location = new System.Drawing.Point(160, 6);
            this.comboBoxParent.Name = "comboBoxParent";
            this.comboBoxParent.Size = new System.Drawing.Size(312, 39);
            this.comboBoxParent.TabIndex = 2;
            this.comboBoxParent.SelectedIndexChanged += new System.EventHandler(this.ComboBoxParent_SelectedIndexChanged);
            // 
            // textBoxKodeGen
            // 
            this.textBoxKodeGen.Location = new System.Drawing.Point(160, 51);
            this.textBoxKodeGen.Name = "textBoxKodeGen";
            this.textBoxKodeGen.ReadOnly = true;
            this.textBoxKodeGen.Size = new System.Drawing.Size(312, 38);
            this.textBoxKodeGen.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kode";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 31);
            this.label4.TabIndex = 5;
            this.label4.Text = ":";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(133, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 31);
            this.label5.TabIndex = 8;
            this.label5.Text = ":";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 31);
            this.label6.TabIndex = 7;
            this.label6.Text = "Nama";
            // 
            // textBoxNama
            // 
            this.textBoxNama.Location = new System.Drawing.Point(160, 95);
            this.textBoxNama.Name = "textBoxNama";
            this.textBoxNama.Size = new System.Drawing.Size(312, 38);
            this.textBoxNama.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(133, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 31);
            this.label7.TabIndex = 11;
            this.label7.Text = ":";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 31);
            this.label8.TabIndex = 10;
            this.label8.Text = "Nominal";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(160, 142);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 31);
            this.label9.TabIndex = 12;
            this.label9.Text = "Rp.";
            // 
            // numericUpDownNominal
            // 
            this.numericUpDownNominal.DecimalPlaces = 2;
            this.numericUpDownNominal.Location = new System.Drawing.Point(216, 140);
            this.numericUpDownNominal.Maximum = new decimal(new int[] {
            -1981284352,
            -1966660860,
            0,
            0});
            this.numericUpDownNominal.Name = "numericUpDownNominal";
            this.numericUpDownNominal.Size = new System.Drawing.Size(256, 38);
            this.numericUpDownNominal.TabIndex = 13;
            this.numericUpDownNominal.ThousandsSeparator = true;
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(12, 184);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(228, 79);
            this.buttonSubmit.TabIndex = 15;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.ButtonSubmit_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(246, 184);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(226, 79);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.Button1_Click);
            // 
            // FormTambahAkun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 272);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.numericUpDownNominal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxNama);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxKodeGen);
            this.Controls.Add(this.comboBoxParent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "FormTambahAkun";
            this.Text = "FormTambahAkun";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormTambahAkun_FormClosed);
            this.Load += new System.EventHandler(this.FormTambahAkun_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNominal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxParent;
        private System.Windows.Forms.TextBox textBoxKodeGen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxNama;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericUpDownNominal;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Button buttonCancel;
    }
}