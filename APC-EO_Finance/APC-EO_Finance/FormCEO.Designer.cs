namespace APC_EO_Finance
{
    partial class FormCEO
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageNotif = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewNotif = new System.Windows.Forms.DataGridView();
            this.tabPageVendor = new System.Windows.Forms.TabPage();
            this.buttonPrintVendor = new System.Windows.Forms.Button();
            this.label96 = new System.Windows.Forms.Label();
            this.dataGridViewTampilVendor = new System.Windows.Forms.DataGridView();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxSearchVendor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageKlien = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.dataGridViewTampilKlien = new System.Windows.Forms.DataGridView();
            this.buttonKlien = new System.Windows.Forms.Button();
            this.textBoxSearchKode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageProject = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewProject = new System.Windows.Forms.DataGridView();
            this.buttonSearchProject = new System.Windows.Forms.Button();
            this.textBoxSearchProject = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageAnalisa = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.chartKlien = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPageJatuhTempo = new System.Windows.Forms.TabPage();
            this.label26 = new System.Windows.Forms.Label();
            this.dataGridViewJatuhTempo = new System.Windows.Forms.DataGridView();
            this.buttonSearchJatuhTempo = new System.Windows.Forms.Button();
            this.textBoxSearchJatuhTempo = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tabPageLaporan = new System.Windows.Forms.TabPage();
            this.comboBoxBulanTahun = new System.Windows.Forms.ComboBox();
            this.comboBoxLaporan = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonLaporanTahun = new System.Windows.Forms.RadioButton();
            this.radioButtonLaporanBulan = new System.Windows.Forms.RadioButton();
            this.radioButtonLaporanProject = new System.Windows.Forms.RadioButton();
            this.dataGridViewLaporan = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPageNotif.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotif)).BeginInit();
            this.tabPageVendor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTampilVendor)).BeginInit();
            this.tabPageKlien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTampilKlien)).BeginInit();
            this.tabPageProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProject)).BeginInit();
            this.tabPageAnalisa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartKlien)).BeginInit();
            this.tabPageJatuhTempo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJatuhTempo)).BeginInit();
            this.tabPageLaporan.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLaporan)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageNotif);
            this.tabControl1.Controls.Add(this.tabPageVendor);
            this.tabControl1.Controls.Add(this.tabPageKlien);
            this.tabControl1.Controls.Add(this.tabPageProject);
            this.tabControl1.Controls.Add(this.tabPageAnalisa);
            this.tabControl1.Controls.Add(this.tabPageJatuhTempo);
            this.tabControl1.Controls.Add(this.tabPageLaporan);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1370, 646);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabPageNotif
            // 
            this.tabPageNotif.Controls.Add(this.label5);
            this.tabPageNotif.Controls.Add(this.dataGridViewNotif);
            this.tabPageNotif.Location = new System.Drawing.Point(4, 40);
            this.tabPageNotif.Margin = new System.Windows.Forms.Padding(7);
            this.tabPageNotif.Name = "tabPageNotif";
            this.tabPageNotif.Size = new System.Drawing.Size(1362, 602);
            this.tabPageNotif.TabIndex = 3;
            this.tabPageNotif.Text = "Notifikasi";
            this.tabPageNotif.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 10.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(12, 580);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(251, 17);
            this.label5.TabIndex = 95;
            this.label5.Text = "Licensed By Abadi Premium Consulting©2019";
            // 
            // dataGridViewNotif
            // 
            this.dataGridViewNotif.AllowUserToAddRows = false;
            this.dataGridViewNotif.AllowUserToDeleteRows = false;
            this.dataGridViewNotif.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNotif.Location = new System.Drawing.Point(12, 7);
            this.dataGridViewNotif.Margin = new System.Windows.Forms.Padding(7);
            this.dataGridViewNotif.Name = "dataGridViewNotif";
            this.dataGridViewNotif.ReadOnly = true;
            this.dataGridViewNotif.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewNotif.Size = new System.Drawing.Size(1366, 566);
            this.dataGridViewNotif.TabIndex = 1;
            this.dataGridViewNotif.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewNotif_CellDoubleClick);
            // 
            // tabPageVendor
            // 
            this.tabPageVendor.Controls.Add(this.buttonPrintVendor);
            this.tabPageVendor.Controls.Add(this.label96);
            this.tabPageVendor.Controls.Add(this.dataGridViewTampilVendor);
            this.tabPageVendor.Controls.Add(this.buttonSearch);
            this.tabPageVendor.Controls.Add(this.textBoxSearchVendor);
            this.tabPageVendor.Controls.Add(this.label1);
            this.tabPageVendor.Location = new System.Drawing.Point(4, 40);
            this.tabPageVendor.Name = "tabPageVendor";
            this.tabPageVendor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVendor.Size = new System.Drawing.Size(1385, 602);
            this.tabPageVendor.TabIndex = 10;
            this.tabPageVendor.Text = "Vendor";
            this.tabPageVendor.UseVisualStyleBackColor = true;
            // 
            // buttonPrintVendor
            // 
            this.buttonPrintVendor.Location = new System.Drawing.Point(756, 10);
            this.buttonPrintVendor.Name = "buttonPrintVendor";
            this.buttonPrintVendor.Size = new System.Drawing.Size(212, 38);
            this.buttonPrintVendor.TabIndex = 94;
            this.buttonPrintVendor.Text = "Print Vendor";
            this.buttonPrintVendor.UseVisualStyleBackColor = true;
            this.buttonPrintVendor.Click += new System.EventHandler(this.ButtonPrintVendor_Click);
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Font = new System.Drawing.Font("Arial Narrow", 10.25F, System.Drawing.FontStyle.Bold);
            this.label96.Location = new System.Drawing.Point(717, 597);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(251, 17);
            this.label96.TabIndex = 93;
            this.label96.Text = "Licensed By Abadi Premium Consulting©2019";
            // 
            // dataGridViewTampilVendor
            // 
            this.dataGridViewTampilVendor.AllowUserToAddRows = false;
            this.dataGridViewTampilVendor.AllowUserToDeleteRows = false;
            this.dataGridViewTampilVendor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTampilVendor.Location = new System.Drawing.Point(8, 54);
            this.dataGridViewTampilVendor.Name = "dataGridViewTampilVendor";
            this.dataGridViewTampilVendor.ReadOnly = true;
            this.dataGridViewTampilVendor.Size = new System.Drawing.Size(1374, 540);
            this.dataGridViewTampilVendor.TabIndex = 4;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(520, 10);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(230, 38);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // textBoxSearchVendor
            // 
            this.textBoxSearchVendor.Location = new System.Drawing.Point(169, 10);
            this.textBoxSearchVendor.Name = "textBoxSearchVendor";
            this.textBoxSearchVendor.Size = new System.Drawing.Size(345, 38);
            this.textBoxSearchVendor.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Nama :";
            // 
            // tabPageKlien
            // 
            this.tabPageKlien.Controls.Add(this.label18);
            this.tabPageKlien.Controls.Add(this.dataGridViewTampilKlien);
            this.tabPageKlien.Controls.Add(this.buttonKlien);
            this.tabPageKlien.Controls.Add(this.textBoxSearchKode);
            this.tabPageKlien.Controls.Add(this.label2);
            this.tabPageKlien.Location = new System.Drawing.Point(4, 40);
            this.tabPageKlien.Name = "tabPageKlien";
            this.tabPageKlien.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKlien.Size = new System.Drawing.Size(1385, 602);
            this.tabPageKlien.TabIndex = 11;
            this.tabPageKlien.Text = "Klien";
            this.tabPageKlien.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial Narrow", 10.25F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(717, 597);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(251, 17);
            this.label18.TabIndex = 89;
            this.label18.Text = "Licensed By Abadi Premium Consulting©2019";
            // 
            // dataGridViewTampilKlien
            // 
            this.dataGridViewTampilKlien.AllowUserToAddRows = false;
            this.dataGridViewTampilKlien.AllowUserToDeleteRows = false;
            this.dataGridViewTampilKlien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTampilKlien.Location = new System.Drawing.Point(9, 54);
            this.dataGridViewTampilKlien.Name = "dataGridViewTampilKlien";
            this.dataGridViewTampilKlien.ReadOnly = true;
            this.dataGridViewTampilKlien.Size = new System.Drawing.Size(1368, 536);
            this.dataGridViewTampilKlien.TabIndex = 4;
            // 
            // buttonKlien
            // 
            this.buttonKlien.Location = new System.Drawing.Point(769, 10);
            this.buttonKlien.Name = "buttonKlien";
            this.buttonKlien.Size = new System.Drawing.Size(199, 38);
            this.buttonKlien.TabIndex = 2;
            this.buttonKlien.Text = "Search";
            this.buttonKlien.UseVisualStyleBackColor = true;
            this.buttonKlien.Click += new System.EventHandler(this.ButtonKlien_Click_1);
            // 
            // textBoxSearchKode
            // 
            this.textBoxSearchKode.Location = new System.Drawing.Point(169, 10);
            this.textBoxSearchKode.Name = "textBoxSearchKode";
            this.textBoxSearchKode.Size = new System.Drawing.Size(594, 38);
            this.textBoxSearchKode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "Search Nama :";
            // 
            // tabPageProject
            // 
            this.tabPageProject.Controls.Add(this.label4);
            this.tabPageProject.Controls.Add(this.dataGridViewProject);
            this.tabPageProject.Controls.Add(this.buttonSearchProject);
            this.tabPageProject.Controls.Add(this.textBoxSearchProject);
            this.tabPageProject.Controls.Add(this.label3);
            this.tabPageProject.Location = new System.Drawing.Point(4, 40);
            this.tabPageProject.Name = "tabPageProject";
            this.tabPageProject.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProject.Size = new System.Drawing.Size(1362, 602);
            this.tabPageProject.TabIndex = 12;
            this.tabPageProject.Text = "Project";
            this.tabPageProject.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 10.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(720, 594);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(251, 17);
            this.label4.TabIndex = 89;
            this.label4.Text = "Licensed By Abadi Premium Consulting©2019";
            // 
            // dataGridViewProject
            // 
            this.dataGridViewProject.AllowUserToAddRows = false;
            this.dataGridViewProject.AllowUserToDeleteRows = false;
            this.dataGridViewProject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProject.Location = new System.Drawing.Point(8, 54);
            this.dataGridViewProject.Name = "dataGridViewProject";
            this.dataGridViewProject.ReadOnly = true;
            this.dataGridViewProject.Size = new System.Drawing.Size(1374, 526);
            this.dataGridViewProject.TabIndex = 9;
            // 
            // buttonSearchProject
            // 
            this.buttonSearchProject.Location = new System.Drawing.Point(769, 10);
            this.buttonSearchProject.Name = "buttonSearchProject";
            this.buttonSearchProject.Size = new System.Drawing.Size(199, 38);
            this.buttonSearchProject.TabIndex = 7;
            this.buttonSearchProject.Text = "Search";
            this.buttonSearchProject.UseVisualStyleBackColor = true;
            this.buttonSearchProject.Click += new System.EventHandler(this.ButtonSearchProject_Click);
            // 
            // textBoxSearchProject
            // 
            this.textBoxSearchProject.Location = new System.Drawing.Point(169, 10);
            this.textBoxSearchProject.Name = "textBoxSearchProject";
            this.textBoxSearchProject.Size = new System.Drawing.Size(594, 38);
            this.textBoxSearchProject.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 31);
            this.label3.TabIndex = 5;
            this.label3.Text = "Search Nama :";
            // 
            // tabPageAnalisa
            // 
            this.tabPageAnalisa.Controls.Add(this.label6);
            this.tabPageAnalisa.Controls.Add(this.chartKlien);
            this.tabPageAnalisa.Location = new System.Drawing.Point(4, 40);
            this.tabPageAnalisa.Name = "tabPageAnalisa";
            this.tabPageAnalisa.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAnalisa.Size = new System.Drawing.Size(1362, 602);
            this.tabPageAnalisa.TabIndex = 13;
            this.tabPageAnalisa.Text = "Analisa Piutang";
            this.tabPageAnalisa.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 10.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(720, 597);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(251, 17);
            this.label6.TabIndex = 89;
            this.label6.Text = "Licensed By Abadi Premium Consulting©2019";
            // 
            // chartKlien
            // 
            chartArea1.Name = "ChartArea1";
            this.chartKlien.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartKlien.Legends.Add(legend1);
            this.chartKlien.Location = new System.Drawing.Point(-1, 6);
            this.chartKlien.Name = "chartKlien";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartKlien.Series.Add(series1);
            this.chartKlien.Size = new System.Drawing.Size(1378, 615);
            this.chartKlien.TabIndex = 0;
            this.chartKlien.Text = "chart1";
            this.chartKlien.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChartKlien_MouseClick);
            // 
            // tabPageJatuhTempo
            // 
            this.tabPageJatuhTempo.Controls.Add(this.label26);
            this.tabPageJatuhTempo.Controls.Add(this.dataGridViewJatuhTempo);
            this.tabPageJatuhTempo.Controls.Add(this.buttonSearchJatuhTempo);
            this.tabPageJatuhTempo.Controls.Add(this.textBoxSearchJatuhTempo);
            this.tabPageJatuhTempo.Controls.Add(this.label23);
            this.tabPageJatuhTempo.Location = new System.Drawing.Point(4, 40);
            this.tabPageJatuhTempo.Name = "tabPageJatuhTempo";
            this.tabPageJatuhTempo.Size = new System.Drawing.Size(1362, 602);
            this.tabPageJatuhTempo.TabIndex = 14;
            this.tabPageJatuhTempo.Text = "Jatuh Tempo";
            this.tabPageJatuhTempo.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Arial Narrow", 10.25F, System.Drawing.FontStyle.Bold);
            this.label26.Location = new System.Drawing.Point(717, 594);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(251, 17);
            this.label26.TabIndex = 94;
            this.label26.Text = "Licensed By Abadi Premium Consulting©2019";
            // 
            // dataGridViewJatuhTempo
            // 
            this.dataGridViewJatuhTempo.AllowUserToAddRows = false;
            this.dataGridViewJatuhTempo.AllowUserToDeleteRows = false;
            this.dataGridViewJatuhTempo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewJatuhTempo.Location = new System.Drawing.Point(8, 54);
            this.dataGridViewJatuhTempo.Name = "dataGridViewJatuhTempo";
            this.dataGridViewJatuhTempo.ReadOnly = true;
            this.dataGridViewJatuhTempo.Size = new System.Drawing.Size(1369, 530);
            this.dataGridViewJatuhTempo.TabIndex = 15;
            // 
            // buttonSearchJatuhTempo
            // 
            this.buttonSearchJatuhTempo.Location = new System.Drawing.Point(743, 9);
            this.buttonSearchJatuhTempo.Name = "buttonSearchJatuhTempo";
            this.buttonSearchJatuhTempo.Size = new System.Drawing.Size(225, 38);
            this.buttonSearchJatuhTempo.TabIndex = 14;
            this.buttonSearchJatuhTempo.Text = "Search";
            this.buttonSearchJatuhTempo.UseVisualStyleBackColor = true;
            this.buttonSearchJatuhTempo.Click += new System.EventHandler(this.ButtonSearchJatuhTempo_Click);
            // 
            // textBoxSearchJatuhTempo
            // 
            this.textBoxSearchJatuhTempo.Location = new System.Drawing.Point(265, 10);
            this.textBoxSearchJatuhTempo.Name = "textBoxSearchJatuhTempo";
            this.textBoxSearchJatuhTempo.Size = new System.Drawing.Size(472, 38);
            this.textBoxSearchJatuhTempo.TabIndex = 13;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(8, 13);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(251, 31);
            this.label23.TabIndex = 12;
            this.label23.Text = "Search Nama Vendor :";
            // 
            // tabPageLaporan
            // 
            this.tabPageLaporan.Controls.Add(this.comboBoxBulanTahun);
            this.tabPageLaporan.Controls.Add(this.comboBoxLaporan);
            this.tabPageLaporan.Controls.Add(this.groupBox1);
            this.tabPageLaporan.Controls.Add(this.dataGridViewLaporan);
            this.tabPageLaporan.Location = new System.Drawing.Point(4, 40);
            this.tabPageLaporan.Name = "tabPageLaporan";
            this.tabPageLaporan.Size = new System.Drawing.Size(1362, 602);
            this.tabPageLaporan.TabIndex = 15;
            this.tabPageLaporan.Text = "Laporan Laba Rugi";
            this.tabPageLaporan.UseVisualStyleBackColor = true;
            // 
            // comboBoxBulanTahun
            // 
            this.comboBoxBulanTahun.FormattingEnabled = true;
            this.comboBoxBulanTahun.Location = new System.Drawing.Point(881, 8);
            this.comboBoxBulanTahun.Name = "comboBoxBulanTahun";
            this.comboBoxBulanTahun.Size = new System.Drawing.Size(245, 39);
            this.comboBoxBulanTahun.TabIndex = 3;
            this.comboBoxBulanTahun.SelectedIndexChanged += new System.EventHandler(this.comboBoxBulanTahun_SelectedIndexChanged);
            // 
            // comboBoxLaporan
            // 
            this.comboBoxLaporan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxLaporan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxLaporan.FormattingEnabled = true;
            this.comboBoxLaporan.Location = new System.Drawing.Point(333, 7);
            this.comboBoxLaporan.Name = "comboBoxLaporan";
            this.comboBoxLaporan.Size = new System.Drawing.Size(542, 39);
            this.comboBoxLaporan.TabIndex = 2;
            this.comboBoxLaporan.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLaporan_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonLaporanTahun);
            this.groupBox1.Controls.Add(this.radioButtonLaporanBulan);
            this.groupBox1.Controls.Add(this.radioButtonLaporanProject);
            this.groupBox1.Location = new System.Drawing.Point(8, -8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 54);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // radioButtonLaporanTahun
            // 
            this.radioButtonLaporanTahun.AutoSize = true;
            this.radioButtonLaporanTahun.Location = new System.Drawing.Point(219, 19);
            this.radioButtonLaporanTahun.Name = "radioButtonLaporanTahun";
            this.radioButtonLaporanTahun.Size = new System.Drawing.Size(100, 35);
            this.radioButtonLaporanTahun.TabIndex = 2;
            this.radioButtonLaporanTahun.Text = "Tahun";
            this.radioButtonLaporanTahun.UseVisualStyleBackColor = true;
            this.radioButtonLaporanTahun.CheckedChanged += new System.EventHandler(this.RadioButtonLaporanTahun_CheckedChanged);
            // 
            // radioButtonLaporanBulan
            // 
            this.radioButtonLaporanBulan.AutoSize = true;
            this.radioButtonLaporanBulan.Location = new System.Drawing.Point(119, 19);
            this.radioButtonLaporanBulan.Name = "radioButtonLaporanBulan";
            this.radioButtonLaporanBulan.Size = new System.Drawing.Size(94, 35);
            this.radioButtonLaporanBulan.TabIndex = 1;
            this.radioButtonLaporanBulan.Text = "Bulan";
            this.radioButtonLaporanBulan.UseVisualStyleBackColor = true;
            this.radioButtonLaporanBulan.CheckedChanged += new System.EventHandler(this.RadioButtonLaporanBulan_CheckedChanged);
            // 
            // radioButtonLaporanProject
            // 
            this.radioButtonLaporanProject.AutoSize = true;
            this.radioButtonLaporanProject.Location = new System.Drawing.Point(6, 19);
            this.radioButtonLaporanProject.Name = "radioButtonLaporanProject";
            this.radioButtonLaporanProject.Size = new System.Drawing.Size(107, 35);
            this.radioButtonLaporanProject.TabIndex = 0;
            this.radioButtonLaporanProject.Text = "Project";
            this.radioButtonLaporanProject.UseVisualStyleBackColor = true;
            this.radioButtonLaporanProject.CheckedChanged += new System.EventHandler(this.RadioButtonLaporanProject_CheckedChanged);
            // 
            // dataGridViewLaporan
            // 
            this.dataGridViewLaporan.AllowUserToAddRows = false;
            this.dataGridViewLaporan.AllowUserToDeleteRows = false;
            this.dataGridViewLaporan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLaporan.Location = new System.Drawing.Point(8, 52);
            this.dataGridViewLaporan.Name = "dataGridViewLaporan";
            this.dataGridViewLaporan.ReadOnly = true;
            this.dataGridViewLaporan.Size = new System.Drawing.Size(1369, 542);
            this.dataGridViewLaporan.TabIndex = 0;
            this.dataGridViewLaporan.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewLaporan_CellValidated);
            // 
            // FormCEO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 646);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "FormCEO";
            this.Text = "FormCEO";
            this.Load += new System.EventHandler(this.FormCEO_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageNotif.ResumeLayout(false);
            this.tabPageNotif.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotif)).EndInit();
            this.tabPageVendor.ResumeLayout(false);
            this.tabPageVendor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTampilVendor)).EndInit();
            this.tabPageKlien.ResumeLayout(false);
            this.tabPageKlien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTampilKlien)).EndInit();
            this.tabPageProject.ResumeLayout(false);
            this.tabPageProject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProject)).EndInit();
            this.tabPageAnalisa.ResumeLayout(false);
            this.tabPageAnalisa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartKlien)).EndInit();
            this.tabPageJatuhTempo.ResumeLayout(false);
            this.tabPageJatuhTempo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewJatuhTempo)).EndInit();
            this.tabPageLaporan.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLaporan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageNotif;
        private System.Windows.Forms.DataGridView dataGridViewNotif;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPageVendor;
        private System.Windows.Forms.Button buttonPrintVendor;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.DataGridView dataGridViewTampilVendor;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxSearchVendor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageKlien;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridView dataGridViewTampilKlien;
        private System.Windows.Forms.Button buttonKlien;
        private System.Windows.Forms.TextBox textBoxSearchKode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPageProject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewProject;
        private System.Windows.Forms.Button buttonSearchProject;
        private System.Windows.Forms.TextBox textBoxSearchProject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPageAnalisa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartKlien;
        private System.Windows.Forms.TabPage tabPageJatuhTempo;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DataGridView dataGridViewJatuhTempo;
        private System.Windows.Forms.Button buttonSearchJatuhTempo;
        private System.Windows.Forms.TextBox textBoxSearchJatuhTempo;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TabPage tabPageLaporan;
        private System.Windows.Forms.ComboBox comboBoxLaporan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonLaporanTahun;
        private System.Windows.Forms.RadioButton radioButtonLaporanBulan;
        private System.Windows.Forms.RadioButton radioButtonLaporanProject;
        private System.Windows.Forms.DataGridView dataGridViewLaporan;
        private System.Windows.Forms.ComboBox comboBoxBulanTahun;
    }
}