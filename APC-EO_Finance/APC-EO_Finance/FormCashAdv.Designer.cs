namespace APC_EO_Finance
{
    partial class FormCashAdv
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxProject = new System.Windows.Forms.ComboBox();
            this.radioButtonProject = new System.Windows.Forms.RadioButton();
            this.radioButtonOpr = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelNama = new System.Windows.Forms.Label();
            this.labelDept = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelKodeCA = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dateTimePickerNow = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerExpected = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonCash = new System.Windows.Forms.RadioButton();
            this.radioButtonTransfer = new System.Windows.Forms.RadioButton();
            this.radioButtonCheque = new System.Windows.Forms.RadioButton();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxPurpose = new System.Windows.Forms.TextBox();
            this.dataGridViewIsi = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Purpose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDownTotal = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIsi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxProject);
            this.groupBox1.Controls.Add(this.radioButtonProject);
            this.groupBox1.Controls.Add(this.radioButtonOpr);
            this.groupBox1.Location = new System.Drawing.Point(12, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(960, 61);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipe Cash Advance";
            // 
            // comboBoxProject
            // 
            this.comboBoxProject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxProject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxProject.FormattingEnabled = true;
            this.comboBoxProject.Location = new System.Drawing.Point(471, 19);
            this.comboBoxProject.Name = "comboBoxProject";
            this.comboBoxProject.Size = new System.Drawing.Size(483, 39);
            this.comboBoxProject.TabIndex = 2;
            this.comboBoxProject.Visible = false;
            // 
            // radioButtonProject
            // 
            this.radioButtonProject.AutoSize = true;
            this.radioButtonProject.Location = new System.Drawing.Point(367, 24);
            this.radioButtonProject.Name = "radioButtonProject";
            this.radioButtonProject.Size = new System.Drawing.Size(107, 35);
            this.radioButtonProject.TabIndex = 1;
            this.radioButtonProject.TabStop = true;
            this.radioButtonProject.Text = "Project";
            this.radioButtonProject.UseVisualStyleBackColor = true;
            this.radioButtonProject.CheckedChanged += new System.EventHandler(this.RadioButtonProject_CheckedChanged);
            // 
            // radioButtonOpr
            // 
            this.radioButtonOpr.AutoSize = true;
            this.radioButtonOpr.Checked = true;
            this.radioButtonOpr.Location = new System.Drawing.Point(220, 24);
            this.radioButtonOpr.Name = "radioButtonOpr";
            this.radioButtonOpr.Size = new System.Drawing.Size(155, 35);
            this.radioButtonOpr.TabIndex = 0;
            this.radioButtonOpr.TabStop = true;
            this.radioButtonOpr.Text = "Operational";
            this.radioButtonOpr.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 31);
            this.label4.TabIndex = 3;
            this.label4.Text = "Dept";
            // 
            // labelNama
            // 
            this.labelNama.AutoSize = true;
            this.labelNama.Location = new System.Drawing.Point(119, 164);
            this.labelNama.Name = "labelNama";
            this.labelNama.Size = new System.Drawing.Size(74, 31);
            this.labelNama.TabIndex = 5;
            this.labelNama.Text = "Nama";
            // 
            // labelDept
            // 
            this.labelDept.AutoSize = true;
            this.labelDept.Location = new System.Drawing.Point(119, 209);
            this.labelDept.Name = "labelDept";
            this.labelDept.Size = new System.Drawing.Size(74, 31);
            this.labelDept.TabIndex = 6;
            this.labelDept.Text = "Nama";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(391, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 31);
            this.label6.TabIndex = 7;
            this.label6.Text = "DateTime";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(391, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 31);
            this.label7.TabIndex = 9;
            this.label7.Text = "No CA ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(607, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 31);
            this.label8.TabIndex = 10;
            this.label8.Text = ":";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(607, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 31);
            this.label9.TabIndex = 11;
            this.label9.Text = ":";
            // 
            // labelKodeCA
            // 
            this.labelKodeCA.AutoSize = true;
            this.labelKodeCA.Location = new System.Drawing.Point(634, 131);
            this.labelKodeCA.Name = "labelKodeCA";
            this.labelKodeCA.Size = new System.Drawing.Size(88, 31);
            this.labelKodeCA.TabIndex = 13;
            this.labelKodeCA.Text = "No CA ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(391, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 31);
            this.label5.TabIndex = 14;
            this.label5.Text = "Expected Payment";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(607, 208);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 31);
            this.label10.TabIndex = 15;
            this.label10.Text = ":";
            // 
            // dateTimePickerNow
            // 
            this.dateTimePickerNow.CustomFormat = "ddd,dd-MM-yyyy HH:mm:ss";
            this.dateTimePickerNow.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerNow.Location = new System.Drawing.Point(640, 164);
            this.dateTimePickerNow.Name = "dateTimePickerNow";
            this.dateTimePickerNow.Size = new System.Drawing.Size(332, 38);
            this.dateTimePickerNow.TabIndex = 70;
            this.dateTimePickerNow.ValueChanged += new System.EventHandler(this.DateTimePickerNow_ValueChanged);
            // 
            // dateTimePickerExpected
            // 
            this.dateTimePickerExpected.CustomFormat = "ddd,dd-MM-yyyy HH:mm:ss";
            this.dateTimePickerExpected.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerExpected.Location = new System.Drawing.Point(640, 208);
            this.dateTimePickerExpected.Name = "dateTimePickerExpected";
            this.dateTimePickerExpected.Size = new System.Drawing.Size(332, 38);
            this.dateTimePickerExpected.TabIndex = 71;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 266);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(191, 31);
            this.label11.TabIndex = 72;
            this.label11.Text = "Payment Method";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(209, 266);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 31);
            this.label12.TabIndex = 73;
            this.label12.Text = ":";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonCash);
            this.groupBox2.Controls.Add(this.radioButtonTransfer);
            this.groupBox2.Controls.Add(this.radioButtonCheque);
            this.groupBox2.Location = new System.Drawing.Point(243, 246);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(729, 56);
            this.groupBox2.TabIndex = 74;
            this.groupBox2.TabStop = false;
            // 
            // radioButtonCash
            // 
            this.radioButtonCash.AutoSize = true;
            this.radioButtonCash.Location = new System.Drawing.Point(522, 18);
            this.radioButtonCash.Name = "radioButtonCash";
            this.radioButtonCash.Size = new System.Drawing.Size(86, 35);
            this.radioButtonCash.TabIndex = 2;
            this.radioButtonCash.Text = "Cash";
            this.radioButtonCash.UseVisualStyleBackColor = true;
            // 
            // radioButtonTransfer
            // 
            this.radioButtonTransfer.AutoSize = true;
            this.radioButtonTransfer.Location = new System.Drawing.Point(297, 16);
            this.radioButtonTransfer.Name = "radioButtonTransfer";
            this.radioButtonTransfer.Size = new System.Drawing.Size(121, 35);
            this.radioButtonTransfer.TabIndex = 1;
            this.radioButtonTransfer.Text = "Transfer";
            this.radioButtonTransfer.UseVisualStyleBackColor = true;
            // 
            // radioButtonCheque
            // 
            this.radioButtonCheque.AutoSize = true;
            this.radioButtonCheque.Checked = true;
            this.radioButtonCheque.Location = new System.Drawing.Point(67, 18);
            this.radioButtonCheque.Name = "radioButtonCheque";
            this.radioButtonCheque.Size = new System.Drawing.Size(114, 35);
            this.radioButtonCheque.TabIndex = 0;
            this.radioButtonCheque.TabStop = true;
            this.radioButtonCheque.Text = "Cheque";
            this.radioButtonCheque.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 310);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(340, 31);
            this.label13.TabIndex = 75;
            this.label13.Text = "Purpose of Advance Payment :";
            // 
            // textBoxPurpose
            // 
            this.textBoxPurpose.Location = new System.Drawing.Point(18, 344);
            this.textBoxPurpose.Name = "textBoxPurpose";
            this.textBoxPurpose.Size = new System.Drawing.Size(954, 38);
            this.textBoxPurpose.TabIndex = 76;
            // 
            // dataGridViewIsi
            // 
            this.dataGridViewIsi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIsi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Purpose,
            this.Description,
            this.Qty,
            this.Price,
            this.Amount});
            this.dataGridViewIsi.Location = new System.Drawing.Point(18, 388);
            this.dataGridViewIsi.Name = "dataGridViewIsi";
            this.dataGridViewIsi.RowTemplate.Height = 50;
            this.dataGridViewIsi.Size = new System.Drawing.Size(954, 150);
            this.dataGridViewIsi.TabIndex = 77;
            this.dataGridViewIsi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewIsi_CellContentClick);
            this.dataGridViewIsi.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewIsi_CellFormatting);
            this.dataGridViewIsi.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellValidated);
            this.dataGridViewIsi.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataGridViewIsi_EditingControlShowing);
            this.dataGridViewIsi.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DataGridViewIsi_RowsAdded);
            this.dataGridViewIsi.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.DataGridViewIsi_RowStateChanged);
            this.dataGridViewIsi.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_RowValidated);
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 70;
            // 
            // Purpose
            // 
            this.Purpose.HeaderText = "Purpose";
            this.Purpose.Name = "Purpose";
            this.Purpose.Width = 150;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.Width = 250;
            // 
            // Qty
            // 
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0";
            this.Qty.DefaultCellStyle = dataGridViewCellStyle1;
            this.Qty.HeaderText = "Qty";
            this.Qty.MaxInputLength = 1000000;
            this.Qty.Name = "Qty";
            this.Qty.Width = 80;
            // 
            // Price
            // 
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = "0";
            this.Price.DefaultCellStyle = dataGridViewCellStyle2;
            this.Price.HeaderText = "Price";
            this.Price.MaxInputLength = 1000000;
            this.Price.Name = "Price";
            this.Price.Width = 150;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.MaxInputLength = 100000000;
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 200;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(640, 553);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(21, 31);
            this.label15.TabIndex = 79;
            this.label15.Text = ":";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(423, 553);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(219, 31);
            this.label16.TabIndex = 78;
            this.label16.Text = "Total amount spent";
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(640, 592);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(159, 62);
            this.buttonSubmit.TabIndex = 81;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.ButtonSubmit_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(813, 592);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(159, 62);
            this.buttonClear.TabIndex = 82;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(667, 553);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 31);
            this.label14.TabIndex = 83;
            this.label14.Text = "Rp.";
            // 
            // numericUpDownTotal
            // 
            this.numericUpDownTotal.DecimalPlaces = 2;
            this.numericUpDownTotal.Enabled = false;
            this.numericUpDownTotal.Location = new System.Drawing.Point(718, 546);
            this.numericUpDownTotal.Maximum = new decimal(new int[] {
            -469762048,
            -590869294,
            5421010,
            0});
            this.numericUpDownTotal.Name = "numericUpDownTotal";
            this.numericUpDownTotal.Size = new System.Drawing.Size(254, 38);
            this.numericUpDownTotal.TabIndex = 84;
            this.numericUpDownTotal.ThousandsSeparator = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::APC_EO_Finance.Properties.Resources.BTWO2;
            this.pictureBox1.Location = new System.Drawing.Point(18, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 86;
            this.pictureBox1.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(69, 18);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(382, 31);
            this.label17.TabIndex = 87;
            this.label17.Text = "BTWO Organization Cash Advance";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial Narrow", 10.25F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(670, 29);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(251, 17);
            this.label18.TabIndex = 88;
            this.label18.Text = "Licensed By Abadi Premium Consulting©2019";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::APC_EO_Finance.Properties.Resources.LOGO_APC;
            this.pictureBox2.Location = new System.Drawing.Point(927, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(45, 54);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 89;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(468, 592);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 62);
            this.button1.TabIndex = 90;
            this.button1.Text = "Cetak";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormCashAdv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.numericUpDownTotal);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.dataGridViewIsi);
            this.Controls.Add(this.textBoxPurpose);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dateTimePickerExpected);
            this.Controls.Add(this.dateTimePickerNow);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelKodeCA);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelDept);
            this.Controls.Add(this.labelNama);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "FormCashAdv";
            this.Text = "FormCashAdv";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCashAdv_FormClosing);
            this.Load += new System.EventHandler(this.FormCashAdv_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIsi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonProject;
        private System.Windows.Forms.RadioButton radioButtonOpr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelNama;
        private System.Windows.Forms.Label labelDept;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelKodeCA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dateTimePickerNow;
        private System.Windows.Forms.DateTimePicker dateTimePickerExpected;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonCash;
        private System.Windows.Forms.RadioButton radioButtonTransfer;
        private System.Windows.Forms.RadioButton radioButtonCheque;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxPurpose;
        private System.Windows.Forms.DataGridView dataGridViewIsi;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numericUpDownTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Purpose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.ComboBox comboBoxProject;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
    }
}