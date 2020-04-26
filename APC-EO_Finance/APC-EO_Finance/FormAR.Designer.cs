namespace APC_EO_Finance
{
    partial class FormAR
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabPageAnalisa = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.chartKlien = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPageProject = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewProject = new System.Windows.Forms.DataGridView();
            this.buttonNewProject = new System.Windows.Forms.Button();
            this.buttonSearchProject = new System.Windows.Forms.Button();
            this.textBoxSearchProject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageKlien = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.dataGridViewTampilKlien = new System.Windows.Forms.DataGridView();
            this.buttonKlien = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxSearchKode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlFull = new System.Windows.Forms.TabControl();
            this.tabPageNotif = new System.Windows.Forms.TabPage();
            this.dataGridViewNotifikasiAR = new System.Windows.Forms.DataGridView();
            this.label22 = new System.Windows.Forms.Label();
            this.tabPageAnalisa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartKlien)).BeginInit();
            this.tabPageProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProject)).BeginInit();
            this.tabPageKlien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTampilKlien)).BeginInit();
            this.tabControlFull.SuspendLayout();
            this.tabPageNotif.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotifikasiAR)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageAnalisa
            // 
            this.tabPageAnalisa.Controls.Add(this.label5);
            this.tabPageAnalisa.Controls.Add(this.chartKlien);
            this.tabPageAnalisa.Location = new System.Drawing.Point(4, 40);
            this.tabPageAnalisa.Name = "tabPageAnalisa";
            this.tabPageAnalisa.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAnalisa.Size = new System.Drawing.Size(977, 621);
            this.tabPageAnalisa.TabIndex = 3;
            this.tabPageAnalisa.Text = "Analisa Piutang";
            this.tabPageAnalisa.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 10.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(720, 597);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(251, 17);
            this.label5.TabIndex = 89;
            this.label5.Text = "Licensed By Abadi Premium Consulting©2019";
            // 
            // chartKlien
            // 
            chartArea2.Name = "ChartArea1";
            this.chartKlien.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartKlien.Legends.Add(legend2);
            this.chartKlien.Location = new System.Drawing.Point(-1, 6);
            this.chartKlien.Name = "chartKlien";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartKlien.Series.Add(series2);
            this.chartKlien.Size = new System.Drawing.Size(978, 615);
            this.chartKlien.TabIndex = 0;
            this.chartKlien.Text = "chart1";
            this.chartKlien.Click += new System.EventHandler(this.ChartKlien_Click);
            this.chartKlien.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChartKlien_MouseClick);
            // 
            // tabPageProject
            // 
            this.tabPageProject.Controls.Add(this.label4);
            this.tabPageProject.Controls.Add(this.dataGridViewProject);
            this.tabPageProject.Controls.Add(this.buttonNewProject);
            this.tabPageProject.Controls.Add(this.buttonSearchProject);
            this.tabPageProject.Controls.Add(this.textBoxSearchProject);
            this.tabPageProject.Controls.Add(this.label2);
            this.tabPageProject.Location = new System.Drawing.Point(4, 40);
            this.tabPageProject.Name = "tabPageProject";
            this.tabPageProject.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProject.Size = new System.Drawing.Size(977, 621);
            this.tabPageProject.TabIndex = 1;
            this.tabPageProject.Text = "Project";
            this.tabPageProject.UseVisualStyleBackColor = true;
            this.tabPageProject.Click += new System.EventHandler(this.TabPageProject_Click);
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
            this.dataGridViewProject.Size = new System.Drawing.Size(960, 526);
            this.dataGridViewProject.TabIndex = 9;
            this.dataGridViewProject.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewProject_CellContentClick);
            // 
            // buttonNewProject
            // 
            this.buttonNewProject.Location = new System.Drawing.Point(769, 10);
            this.buttonNewProject.Name = "buttonNewProject";
            this.buttonNewProject.Size = new System.Drawing.Size(199, 38);
            this.buttonNewProject.TabIndex = 8;
            this.buttonNewProject.Text = "New Project";
            this.buttonNewProject.UseVisualStyleBackColor = true;
            this.buttonNewProject.Click += new System.EventHandler(this.ButtonNewProject_Click);
            // 
            // buttonSearchProject
            // 
            this.buttonSearchProject.Location = new System.Drawing.Point(538, 10);
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
            this.textBoxSearchProject.Size = new System.Drawing.Size(345, 38);
            this.textBoxSearchProject.TabIndex = 6;
            this.textBoxSearchProject.TextChanged += new System.EventHandler(this.TextBoxSearchProject_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 31);
            this.label2.TabIndex = 5;
            this.label2.Text = "Search Nama :";
            // 
            // tabPageKlien
            // 
            this.tabPageKlien.Controls.Add(this.label18);
            this.tabPageKlien.Controls.Add(this.dataGridViewTampilKlien);
            this.tabPageKlien.Controls.Add(this.buttonKlien);
            this.tabPageKlien.Controls.Add(this.buttonSearch);
            this.tabPageKlien.Controls.Add(this.textBoxSearchKode);
            this.tabPageKlien.Controls.Add(this.label1);
            this.tabPageKlien.Location = new System.Drawing.Point(4, 40);
            this.tabPageKlien.Name = "tabPageKlien";
            this.tabPageKlien.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKlien.Size = new System.Drawing.Size(977, 621);
            this.tabPageKlien.TabIndex = 0;
            this.tabPageKlien.Text = "Klien";
            this.tabPageKlien.UseVisualStyleBackColor = true;
            this.tabPageKlien.Click += new System.EventHandler(this.TabPageKlien_Click);
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
            this.dataGridViewTampilKlien.Location = new System.Drawing.Point(8, 54);
            this.dataGridViewTampilKlien.Name = "dataGridViewTampilKlien";
            this.dataGridViewTampilKlien.ReadOnly = true;
            this.dataGridViewTampilKlien.Size = new System.Drawing.Size(960, 536);
            this.dataGridViewTampilKlien.TabIndex = 4;
            this.dataGridViewTampilKlien.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewTampilKlien_CellContentClick);
            // 
            // buttonKlien
            // 
            this.buttonKlien.Location = new System.Drawing.Point(769, 10);
            this.buttonKlien.Name = "buttonKlien";
            this.buttonKlien.Size = new System.Drawing.Size(199, 38);
            this.buttonKlien.TabIndex = 3;
            this.buttonKlien.Text = "New Klien";
            this.buttonKlien.UseVisualStyleBackColor = true;
            this.buttonKlien.Click += new System.EventHandler(this.ButtonKlien_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(538, 10);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(199, 38);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // textBoxSearchKode
            // 
            this.textBoxSearchKode.Location = new System.Drawing.Point(169, 10);
            this.textBoxSearchKode.Name = "textBoxSearchKode";
            this.textBoxSearchKode.Size = new System.Drawing.Size(345, 38);
            this.textBoxSearchKode.TabIndex = 1;
            this.textBoxSearchKode.TextChanged += new System.EventHandler(this.TextBoxSearchKode_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Nama :";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // tabControlFull
            // 
            this.tabControlFull.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlFull.Controls.Add(this.tabPageKlien);
            this.tabControlFull.Controls.Add(this.tabPageProject);
            this.tabControlFull.Controls.Add(this.tabPageAnalisa);
            this.tabControlFull.Controls.Add(this.tabPageNotif);
            this.tabControlFull.Location = new System.Drawing.Point(0, -2);
            this.tabControlFull.Name = "tabControlFull";
            this.tabControlFull.SelectedIndex = 0;
            this.tabControlFull.Size = new System.Drawing.Size(985, 665);
            this.tabControlFull.TabIndex = 4;
            this.tabControlFull.SelectedIndexChanged += new System.EventHandler(this.TabControlFull_SelectedIndexChanged);
            // 
            // tabPageNotif
            // 
            this.tabPageNotif.Controls.Add(this.dataGridViewNotifikasiAR);
            this.tabPageNotif.Controls.Add(this.label22);
            this.tabPageNotif.Location = new System.Drawing.Point(4, 40);
            this.tabPageNotif.Name = "tabPageNotif";
            this.tabPageNotif.Size = new System.Drawing.Size(977, 621);
            this.tabPageNotif.TabIndex = 4;
            this.tabPageNotif.Text = "Notifikasi";
            this.tabPageNotif.UseVisualStyleBackColor = true;
            // 
            // dataGridViewNotifikasiAR
            // 
            this.dataGridViewNotifikasiAR.AllowUserToAddRows = false;
            this.dataGridViewNotifikasiAR.AllowUserToDeleteRows = false;
            this.dataGridViewNotifikasiAR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNotifikasiAR.Location = new System.Drawing.Point(8, 17);
            this.dataGridViewNotifikasiAR.Name = "dataGridViewNotifikasiAR";
            this.dataGridViewNotifikasiAR.ReadOnly = true;
            this.dataGridViewNotifikasiAR.Size = new System.Drawing.Size(960, 577);
            this.dataGridViewNotifikasiAR.TabIndex = 97;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Arial Narrow", 10.25F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(717, 597);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(251, 17);
            this.label22.TabIndex = 96;
            this.label22.Text = "Licensed By Abadi Premium Consulting©2019";
            // 
            // FormAR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.tabControlFull);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.Name = "FormAR";
            this.Text = "FormAR";
            this.Load += new System.EventHandler(this.FormAR_Load);
            this.tabPageAnalisa.ResumeLayout(false);
            this.tabPageAnalisa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartKlien)).EndInit();
            this.tabPageProject.ResumeLayout(false);
            this.tabPageProject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProject)).EndInit();
            this.tabPageKlien.ResumeLayout(false);
            this.tabPageKlien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTampilKlien)).EndInit();
            this.tabControlFull.ResumeLayout(false);
            this.tabPageNotif.ResumeLayout(false);
            this.tabPageNotif.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotifikasiAR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageAnalisa;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartKlien;
        private System.Windows.Forms.TabPage tabPageProject;
        private System.Windows.Forms.DataGridView dataGridViewProject;
        private System.Windows.Forms.Button buttonNewProject;
        private System.Windows.Forms.Button buttonSearchProject;
        private System.Windows.Forms.TextBox textBoxSearchProject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPageKlien;
        private System.Windows.Forms.DataGridView dataGridViewTampilKlien;
        private System.Windows.Forms.Button buttonKlien;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxSearchKode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControlFull;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TabPage tabPageNotif;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.DataGridView dataGridViewNotifikasiAR;
    }
}