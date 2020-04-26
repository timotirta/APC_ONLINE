namespace APC_EO_Finance
{
    partial class FormKlien
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
            this.textBoxKodeKlien = new System.Windows.Forms.TextBox();
            this.textBoxNamaKlien = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAlamatKlien = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxNoTelpKlien = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxEmailKlien = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxFaxKlien = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxNPWPKlien = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.numericUpDownPiutangKlien = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.buttonSubmitKlien = new System.Windows.Forms.Button();
            this.buttonClearKlien = new System.Windows.Forms.Button();
            this.buttonCloseKlien = new System.Windows.Forms.Button();
            this.textBoxPrincipal = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPiutangKlien)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = ":";
            // 
            // textBoxKodeKlien
            // 
            this.textBoxKodeKlien.Location = new System.Drawing.Point(134, 6);
            this.textBoxKodeKlien.Name = "textBoxKodeKlien";
            this.textBoxKodeKlien.ReadOnly = true;
            this.textBoxKodeKlien.Size = new System.Drawing.Size(338, 38);
            this.textBoxKodeKlien.TabIndex = 2;
            // 
            // textBoxNamaKlien
            // 
            this.textBoxNamaKlien.Location = new System.Drawing.Point(134, 50);
            this.textBoxNamaKlien.Name = "textBoxNamaKlien";
            this.textBoxNamaKlien.Size = new System.Drawing.Size(338, 38);
            this.textBoxNamaKlien.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 31);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nama";
            // 
            // textBoxAlamatKlien
            // 
            this.textBoxAlamatKlien.Location = new System.Drawing.Point(134, 181);
            this.textBoxAlamatKlien.Name = "textBoxAlamatKlien";
            this.textBoxAlamatKlien.Size = new System.Drawing.Size(338, 38);
            this.textBoxAlamatKlien.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(107, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 31);
            this.label5.TabIndex = 7;
            this.label5.Text = ":";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 31);
            this.label6.TabIndex = 6;
            this.label6.Text = "Alamat";
            // 
            // textBoxNoTelpKlien
            // 
            this.textBoxNoTelpKlien.Location = new System.Drawing.Point(134, 225);
            this.textBoxNoTelpKlien.Name = "textBoxNoTelpKlien";
            this.textBoxNoTelpKlien.Size = new System.Drawing.Size(338, 38);
            this.textBoxNoTelpKlien.TabIndex = 11;
            this.textBoxNoTelpKlien.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxNoTelpKlien_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(107, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 31);
            this.label7.TabIndex = 10;
            this.label7.Text = ":";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 31);
            this.label8.TabIndex = 9;
            this.label8.Text = "NoTelp";
            // 
            // textBoxEmailKlien
            // 
            this.textBoxEmailKlien.Location = new System.Drawing.Point(134, 269);
            this.textBoxEmailKlien.Name = "textBoxEmailKlien";
            this.textBoxEmailKlien.Size = new System.Drawing.Size(338, 38);
            this.textBoxEmailKlien.TabIndex = 14;
            this.textBoxEmailKlien.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxEmailKlien_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(107, 272);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 31);
            this.label9.TabIndex = 13;
            this.label9.Text = ":";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 272);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 31);
            this.label10.TabIndex = 12;
            this.label10.Text = "Email";
            // 
            // textBoxFaxKlien
            // 
            this.textBoxFaxKlien.Location = new System.Drawing.Point(134, 313);
            this.textBoxFaxKlien.Name = "textBoxFaxKlien";
            this.textBoxFaxKlien.Size = new System.Drawing.Size(338, 38);
            this.textBoxFaxKlien.TabIndex = 17;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(107, 316);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 31);
            this.label11.TabIndex = 16;
            this.label11.Text = ":";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 316);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 31);
            this.label12.TabIndex = 15;
            this.label12.Text = "Fax";
            // 
            // textBoxNPWPKlien
            // 
            this.textBoxNPWPKlien.Location = new System.Drawing.Point(134, 357);
            this.textBoxNPWPKlien.Name = "textBoxNPWPKlien";
            this.textBoxNPWPKlien.Size = new System.Drawing.Size(338, 38);
            this.textBoxNPWPKlien.TabIndex = 20;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(107, 360);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(21, 31);
            this.label13.TabIndex = 19;
            this.label13.Text = ":";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 360);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 31);
            this.label14.TabIndex = 18;
            this.label14.Text = "NPWP";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(107, 404);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(21, 31);
            this.label15.TabIndex = 22;
            this.label15.Text = ":";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 404);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 31);
            this.label16.TabIndex = 21;
            this.label16.Text = "Piutang";
            // 
            // numericUpDownPiutangKlien
            // 
            this.numericUpDownPiutangKlien.DecimalPlaces = 2;
            this.numericUpDownPiutangKlien.Enabled = false;
            this.numericUpDownPiutangKlien.Location = new System.Drawing.Point(190, 401);
            this.numericUpDownPiutangKlien.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numericUpDownPiutangKlien.Name = "numericUpDownPiutangKlien";
            this.numericUpDownPiutangKlien.ReadOnly = true;
            this.numericUpDownPiutangKlien.Size = new System.Drawing.Size(282, 38);
            this.numericUpDownPiutangKlien.TabIndex = 23;
            this.numericUpDownPiutangKlien.ThousandsSeparator = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(134, 404);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 31);
            this.label17.TabIndex = 24;
            this.label17.Text = "Rp.";
            // 
            // buttonSubmitKlien
            // 
            this.buttonSubmitKlien.Location = new System.Drawing.Point(12, 445);
            this.buttonSubmitKlien.Name = "buttonSubmitKlien";
            this.buttonSubmitKlien.Size = new System.Drawing.Size(149, 77);
            this.buttonSubmitKlien.TabIndex = 25;
            this.buttonSubmitKlien.Text = "Submit";
            this.buttonSubmitKlien.UseVisualStyleBackColor = true;
            this.buttonSubmitKlien.Click += new System.EventHandler(this.ButtonSubmitKlien_Click);
            // 
            // buttonClearKlien
            // 
            this.buttonClearKlien.Location = new System.Drawing.Point(167, 445);
            this.buttonClearKlien.Name = "buttonClearKlien";
            this.buttonClearKlien.Size = new System.Drawing.Size(149, 77);
            this.buttonClearKlien.TabIndex = 26;
            this.buttonClearKlien.Text = "Clear";
            this.buttonClearKlien.UseVisualStyleBackColor = true;
            this.buttonClearKlien.Click += new System.EventHandler(this.ButtonClearKlien_Click);
            // 
            // buttonCloseKlien
            // 
            this.buttonCloseKlien.Location = new System.Drawing.Point(323, 445);
            this.buttonCloseKlien.Name = "buttonCloseKlien";
            this.buttonCloseKlien.Size = new System.Drawing.Size(149, 77);
            this.buttonCloseKlien.TabIndex = 27;
            this.buttonCloseKlien.Text = "Close";
            this.buttonCloseKlien.UseVisualStyleBackColor = true;
            this.buttonCloseKlien.Click += new System.EventHandler(this.ButtonCloseKlien_Click);
            // 
            // textBoxPrincipal
            // 
            this.textBoxPrincipal.Location = new System.Drawing.Point(134, 94);
            this.textBoxPrincipal.Name = "textBoxPrincipal";
            this.textBoxPrincipal.Size = new System.Drawing.Size(338, 38);
            this.textBoxPrincipal.TabIndex = 30;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(107, 97);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(21, 31);
            this.label18.TabIndex = 29;
            this.label18.Text = ":";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 97);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(108, 31);
            this.label19.TabIndex = 28;
            this.label19.Text = "Principal";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(134, 138);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(338, 38);
            this.textBoxUser.TabIndex = 33;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(107, 141);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(21, 31);
            this.label20.TabIndex = 32;
            this.label20.Text = ":";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 141);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(63, 31);
            this.label21.TabIndex = 31;
            this.label21.Text = "User";
            // 
            // FormKlien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 530);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.textBoxPrincipal);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.buttonCloseKlien);
            this.Controls.Add(this.buttonClearKlien);
            this.Controls.Add(this.buttonSubmitKlien);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.numericUpDownPiutangKlien);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBoxNPWPKlien);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBoxFaxKlien);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxEmailKlien);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxNoTelpKlien);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxAlamatKlien);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxNamaKlien);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxKodeKlien);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "FormKlien";
            this.Text = "FormKlien";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormKlien_FormClosing);
            this.Load += new System.EventHandler(this.FormKlien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPiutangKlien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox textBoxKodeKlien;
        public System.Windows.Forms.TextBox textBoxNamaKlien;
        public System.Windows.Forms.TextBox textBoxAlamatKlien;
        public System.Windows.Forms.TextBox textBoxNoTelpKlien;
        public System.Windows.Forms.TextBox textBoxEmailKlien;
        public System.Windows.Forms.TextBox textBoxFaxKlien;
        public System.Windows.Forms.TextBox textBoxNPWPKlien;
        public System.Windows.Forms.NumericUpDown numericUpDownPiutangKlien;
        private System.Windows.Forms.Button buttonCloseKlien;
        public System.Windows.Forms.Button buttonSubmitKlien;
        public System.Windows.Forms.Button buttonClearKlien;
        public System.Windows.Forms.TextBox textBoxPrincipal;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
    }
}