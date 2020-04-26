namespace APC_EO_Finance
{
    partial class FormBonusCEO
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
            this.groupBoxBonus = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.numericUpDownTotal = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.numericUpDownPersen = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.labelLaba = new System.Windows.Forms.Label();
            this.dataGridViewTampilBonus = new System.Windows.Forms.DataGridView();
            this.checkBoxKlienSendiri = new System.Windows.Forms.CheckBox();
            this.groupBoxBonus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPersen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTampilBonus)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxBonus
            // 
            this.groupBoxBonus.Controls.Add(this.label21);
            this.groupBoxBonus.Controls.Add(this.numericUpDownTotal);
            this.groupBoxBonus.Controls.Add(this.label20);
            this.groupBoxBonus.Controls.Add(this.numericUpDownPersen);
            this.groupBoxBonus.Controls.Add(this.label19);
            this.groupBoxBonus.Controls.Add(this.label17);
            this.groupBoxBonus.Location = new System.Drawing.Point(146, 126);
            this.groupBoxBonus.Name = "groupBoxBonus";
            this.groupBoxBonus.Size = new System.Drawing.Size(559, 206);
            this.groupBoxBonus.TabIndex = 10;
            this.groupBoxBonus.TabStop = false;
            this.groupBoxBonus.Text = "Bonus Tahunan";
            this.groupBoxBonus.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(246, 127);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(62, 31);
            this.label21.TabIndex = 11;
            this.label21.Text = "Rp.  ";
            // 
            // numericUpDownTotal
            // 
            this.numericUpDownTotal.DecimalPlaces = 2;
            this.numericUpDownTotal.Location = new System.Drawing.Point(314, 125);
            this.numericUpDownTotal.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.numericUpDownTotal.Name = "numericUpDownTotal";
            this.numericUpDownTotal.Size = new System.Drawing.Size(239, 38);
            this.numericUpDownTotal.TabIndex = 10;
            this.numericUpDownTotal.ThousandsSeparator = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(172, 127);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(86, 31);
            this.label20.TabIndex = 9;
            this.label20.Text = "Total : ";
            // 
            // numericUpDownPersen
            // 
            this.numericUpDownPersen.Location = new System.Drawing.Point(252, 58);
            this.numericUpDownPersen.Name = "numericUpDownPersen";
            this.numericUpDownPersen.Size = new System.Drawing.Size(120, 38);
            this.numericUpDownPersen.TabIndex = 6;
            this.numericUpDownPersen.ValueChanged += new System.EventHandler(this.NumericUpDownPersen_ValueChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(378, 60);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 31);
            this.label19.TabIndex = 8;
            this.label19.Text = "%";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 60);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(243, 31);
            this.label17.TabIndex = 7;
            this.label17.Text = "Persenan Karyawan : ";
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(780, 471);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(183, 70);
            this.buttonSubmit.TabIndex = 11;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.ButtonSubmit_Click);
            // 
            // labelLaba
            // 
            this.labelLaba.AutoSize = true;
            this.labelLaba.Location = new System.Drawing.Point(6, 471);
            this.labelLaba.Name = "labelLaba";
            this.labelLaba.Size = new System.Drawing.Size(202, 31);
            this.labelLaba.TabIndex = 13;
            this.labelLaba.Text = "Laba Project : Rp.";
            // 
            // dataGridViewTampilBonus
            // 
            this.dataGridViewTampilBonus.AllowUserToAddRows = false;
            this.dataGridViewTampilBonus.AllowUserToDeleteRows = false;
            this.dataGridViewTampilBonus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTampilBonus.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewTampilBonus.Name = "dataGridViewTampilBonus";
            this.dataGridViewTampilBonus.Size = new System.Drawing.Size(951, 453);
            this.dataGridViewTampilBonus.TabIndex = 12;
            this.dataGridViewTampilBonus.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewTampilBonus_CellValidated);
            // 
            // checkBoxKlienSendiri
            // 
            this.checkBoxKlienSendiri.AutoSize = true;
            this.checkBoxKlienSendiri.Location = new System.Drawing.Point(12, 506);
            this.checkBoxKlienSendiri.Name = "checkBoxKlienSendiri";
            this.checkBoxKlienSendiri.Size = new System.Drawing.Size(173, 35);
            this.checkBoxKlienSendiri.TabIndex = 14;
            this.checkBoxKlienSendiri.Text = "Klien Dari AE";
            this.checkBoxKlienSendiri.UseVisualStyleBackColor = true;
            this.checkBoxKlienSendiri.Visible = false;
            // 
            // FormBonusCEO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 554);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.groupBoxBonus);
            this.Controls.Add(this.checkBoxKlienSendiri);
            this.Controls.Add(this.labelLaba);
            this.Controls.Add(this.dataGridViewTampilBonus);
            this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "FormBonusCEO";
            this.Text = "FormBonusCEO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBonusCEO_FormClosing);
            this.Load += new System.EventHandler(this.FormBonusCEO_Load);
            this.groupBoxBonus.ResumeLayout(false);
            this.groupBoxBonus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPersen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTampilBonus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxBonus;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown numericUpDownTotal;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown numericUpDownPersen;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Label labelLaba;
        private System.Windows.Forms.DataGridView dataGridViewTampilBonus;
        private System.Windows.Forms.CheckBox checkBoxKlienSendiri;
    }
}