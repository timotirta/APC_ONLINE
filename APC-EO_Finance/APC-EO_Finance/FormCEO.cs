using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace APC_EO_Finance
{
    public partial class FormCEO : Form
    {
        public FormCEO()
        {
            InitializeComponent();
        }
        public void updateDGVJatuhTempo(string nama = "")
        {
            string cmdAmbilBayar = "SELECT kodevendor, kodeproject, sudahdibayar FROM datavendorproject ORDER BY DATE_FORMAT(jatuhtempo,'%m')";

            string cmd = "SELECT iv.`kodevendor`,ip.kodeproject,iv.namabarang, v.nama,ip.subtotal, DATE_FORMAT(ip.jatuhtempo,'%d-%m-%Y'), DATE_FORMAT(ip.jatuhtempo,'%M') FROM dataitemvendor iv,datavendor v, dataitemproject ip, datavendorproject vp WHERE vp.kodevendor = iv.kodevendor AND vp.kodeproject = ip.kodeproject AND (vp.total-vp.sudahdibayar) > 0 AND iv.kodevendor = v.kode AND ip.kodebarang = iv.kode AND LOWER(v.nama) like '%" + nama.ToLower() + "%' ORDER BY DATE_FORMAT(ip.jatuhtempo,'%m'), ip.kode ";

            try
            {
                DataTable tampil = new DataTable();
                tampil.Columns.Add("Nama Barang");
                tampil.Columns.Add("Nama Vendor");
                tampil.Columns.Add("Kekurangan Pembayaran");
                tampil.Columns.Add("Jatuh Tempo");
                DataTable tmpUang = new DataTable();
                DataTable tmpData = new DataTable();
                dataGridViewJatuhTempo.DataSource = null;
                dataGridViewJatuhTempo.Columns.Clear();
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);

                adapterKlien.Fill(tmpData);
                adapterKlien = new MySqlDataAdapter(cmdAmbilBayar, ClassConnection.Instance().Connection);

                adapterKlien.Fill(tmpUang);
                string tmpTanggal = "";
                foreach (DataRow rowUang in tmpUang.Rows)
                {
                    long UangSudahDibayar = Convert.ToInt64(rowUang[2].ToString());
                    foreach (DataRow row in tmpData.Rows)
                    {
                        if (row[0].ToString() == rowUang[0].ToString() && row[1].ToString() == rowUang[1].ToString())
                        {
                            if (UangSudahDibayar - Convert.ToInt64(row[4].ToString()) >= 0)
                            {
                                UangSudahDibayar -= Convert.ToInt64(row[4].ToString());
                                continue;
                            }
                            else
                            {
                                if (tmpTanggal != row[6].ToString())
                                {
                                    DataRow rBayangan = tampil.NewRow();
                                    rBayangan[0] = row[6].ToString();
                                    rBayangan[1] = "-";
                                    tmpTanggal = row[6].ToString();
                                    tampil.Rows.Add(rBayangan);
                                }
                                DataRow r = tampil.NewRow();
                                r[0] = row[2].ToString();
                                r[1] = row[3].ToString();
                                r[2] = "Rp." + Convert.ToInt64(Convert.ToInt64(row[4].ToString()) - UangSudahDibayar).ToString("N");
                                UangSudahDibayar = 0;
                                r[3] = row[5].ToString();
                                tampil.Rows.Add(r);
                            }
                        }
                    }
                }
                dataGridViewJatuhTempo.DataSource = tampil;
                dataGridViewJatuhTempo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewJatuhTempo.ColumnCount; i++)
                {
                    dataGridViewJatuhTempo.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                foreach (DataGridViewRow r in dataGridViewJatuhTempo.Rows)
                {
                    if (r.Cells[1].Value.ToString() == "-")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        DataTable tableChart;
        public void updateChart()
        {
            chartKlien.Series.Clear();
            int[] data = { 0, 0, 0, 0, 0 };
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT count(kode) from dataproject where datediff(now(),deadline) <= 0 and status = 0", ClassConnection.Instance().Connection);
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        data[0] = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    }
                    cmd = new MySqlCommand("SELECT count(kode) from dataproject where datediff(now(),deadline) <= 30 and datediff(now(),deadline) > 0 and status = 0", ClassConnection.Instance().Connection);
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        data[1] = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    }
                    cmd = new MySqlCommand("SELECT count(kode) from dataproject where datediff(now(),deadline) <= 60 and datediff(now(),deadline) > 30 and status = 0", ClassConnection.Instance().Connection);
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        data[2] = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    }
                    cmd = new MySqlCommand("SELECT count(kode) from dataproject where datediff(now(),deadline) <= 90 and datediff(now(),deadline) > 60 and status = 0", ClassConnection.Instance().Connection);
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        data[3] = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    }
                    cmd = new MySqlCommand("SELECT count(kode) from dataproject where datediff(now(),deadline) <= 120 and datediff(now(),deadline) > 90 and status = 0", ClassConnection.Instance().Connection);
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        data[4] = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    }

                    cmd = new MySqlCommand("SELECT k.nama as 'Nama Klien',p.nama as 'Nama Project', datediff(now(),p.deadline)as 'Perbedaan Hari', concat('Rp. ',format(k.bataspiutang,2)) as 'Piutang Klien' from dataproject p, dataklien k where k.kode = p.kode_klien and p.status = 0", ClassConnection.Instance().Connection);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    tableChart = ds.Tables[0];

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            var series = new Series("Analisa");
            series.Font = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
            series.ChartType = SeriesChartType.Pie;
            series.Label = "#PERCENT";
            // Frist parameter is X-Axis and Second is Collection of Y- Axis
            series.Points.DataBindXY(new[] { 0, 30, 60, 90, 120 }, new[] { data[0], data[1], data[2], data[3], data[4] });

            series.Points[0].LegendText = "0 Hari";
            series.Points[1].LegendText = "30 Hari";
            series.Points[2].LegendText = "60 Hari";
            series.Points[3].LegendText = "90 Hari";
            series.Points[4].LegendText = "120 Hari";

            series.Points[0].Color = Color.OrangeRed;
            series.Points[1].Color = Color.DarkOrange;
            series.Points[2].Color = Color.LightBlue;
            series.Points[3].Color = Color.MediumPurple;
            series.Points[4].Color = Color.GreenYellow;

            chartKlien.Series.Add(series);
        }
        public void updateDGVKlien(string cmd = "SELECT Kode, Nama, Principal, User, Notelp,CONCAT('Rp.',FORMAT(bataspiutang,2)) as 'Piutang' FROM dataklien order by kode")
        {
            try
            {
                DataSet datasetKlien = new DataSet();
                dataGridViewTampilKlien.DataSource = null;
                dataGridViewTampilKlien.Columns.Clear();
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);

                adapterKlien.Fill(datasetKlien);
                dataGridViewTampilKlien.DataSource = datasetKlien.Tables[0];

                dataGridViewTampilKlien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewTampilKlien.ColumnCount; i++)
                {
                    dataGridViewTampilKlien.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void updateDGVProject(string cmd = "SELECT p.Kode,k.nama as 'Nama Klien' , p.nama as 'Nama Project', date_format(p.tagihan,'%d-%m-%Y') as 'Tagihan',date_format(p.deadline,'%d-%m-%Y') as 'Deadline', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total', CONCAT('Rp.',FORMAT(p.totalbiaya-p.kurang,2)) as 'Kurang Biaya',IF(p.status = 0, 'Belum Selesai', 'Sudah Selesai') AS 'Status Project' FROM dataproject p, dataklien k where p.kode_klien = k.kode order by kode")
        {
            try
            {
                DataSet datasetKlien = new DataSet();
                dataGridViewProject.DataSource = null;
                dataGridViewProject.Columns.Clear();
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);

                adapterKlien.Fill(datasetKlien);
                dataGridViewProject.DataSource = datasetKlien.Tables[0];

                dataGridViewProject.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewProject.ColumnCount; i++)
                {
                    dataGridViewProject.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void updateDGVVendor(string cmd = "SELECT Kode,Nama,Alamat,Notelp,Kota,Jenisvendor as 'Jenis Vendor' FROM datavendor order by kode")
        {
            try
            {
                DataSet datasetVendor = new DataSet();
                dataGridViewTampilVendor.DataSource = null;
                dataGridViewTampilVendor.Columns.Clear();
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);

                adapterKlien.Fill(datasetVendor);

                dataGridViewTampilVendor.DataSource = datasetVendor.Tables[0];
                dataGridViewTampilVendor.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewTampilVendor.ColumnCount; i++)
                {
                    dataGridViewTampilVendor.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void updateDGVNotif()
        {
            try
            {
                DataTable tmp = new DataTable();
                DataTable tampil = new DataTable();

                tampil.Columns.Add("Tentang");
                tampil.Columns.Add("Kode");
                tampil.Columns.Add("Notifikasi");

                dataGridViewNotif.DataSource = null;
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT kode,nama,format(jumlah,2) from pettyops where status = 0 and dividen = 1", ClassConnection.Instance().Connection);
                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Pengeluaran Operasional";
                    row[1] = r[0].ToString();
                    row[2] = "Anda memiliki Pengeluaran Operasional yang belum terapprove dengan nama : " + r[1].ToString() + " dan total Rp. " + r[2].ToString();
                    tampil.Rows.Add(row);
                }

                adapterKlien = new MySqlDataAdapter("SELECT * FROM notifceotahunan WHERE STATUS = 0", ClassConnection.Instance().Connection);
                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Bonus Tahunan";
                    row[1] = r[0].ToString();
                    row[2] = "Anda memiliki Bonus Tahunan yang belum terapprove";
                    tampil.Rows.Add(row);
                }


                adapterKlien = new MySqlDataAdapter("SELECT bp.kodeproject,p.nama FROM bonusproject bp, dataproject p WHERE bp.STATUS = 0 and bp.kodeproject = p.kode group by bp.kodeproject", ClassConnection.Instance().Connection);
                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Bonus Project";
                    row[1] = r[0].ToString();
                    row[2] = "Anda memiliki Bonus Project : '"+r[1].ToString()+"' yang belum terapprove";
                    tampil.Rows.Add(row);
                }

                adapterKlien = new MySqlDataAdapter("SELECT v.nama, FORMAT((vp.total-vp.sudahdibayar),2), p.nama, DATE_FORMAT(vp.`jatuhtempo`,'%d-%m-%Y') FROM datavendorproject vp, datavendor v, dataproject p WHERE vp.`kodeproject` = p.kode AND vp.kodevendor = v.kode AND (vp.total-vp.sudahdibayar) > 0 GROUP BY v.nama ORDER BY vp.jatuhtempo", ClassConnection.Instance().Connection);
                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();
                    DateTime cek = DateTime.ParseExact(r[3].ToString(), "dd-MM-yyyy", CultureInfo.GetCultureInfo("en-US")); ;
                    int daysDiff = ((TimeSpan)(cek - DateTime.Today)).Days;

                    if (daysDiff < 0)
                    {
                        row[0] = "Utang Vendor";
                        row[1] = r[3].ToString();
                        row[2] = "Vendor " + r[0].ToString() + " untuk project " + r[2].ToString() + " sudah melebihi masa jatuh tempo dengan total biaya Rp. " + r[1].ToString();

                    }
                    else if (daysDiff <= 3)
                    {
                        row[0] = "Masa Tenggang Vendor";
                        row[1] = r[3].ToString();
                        row[2] = "Vendor " + r[0].ToString() + " untuk project " + r[2].ToString() + " telah memasuki masa tenggang yang akan memiliki jatuh tempo " + r[3].ToString() + " dengan total biaya Rp. " + r[1].ToString();
                    }
                    else
                    {
                        row[0] = "Jatuh Tempo Vendor";
                        row[1] = r[3].ToString();
                        row[2] = "Vendor " + r[0].ToString() + " untuk project " + r[2].ToString() + " akan memiliki jatuh tempo " + r[3].ToString() + " dengan total biaya Rp. " + r[1].ToString();
                    }
                    tampil.Rows.Add(row);
                }

                adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.deadline,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.status = 0 and p.kode_klien = k.kode order by p.kode", ClassConnection.Instance().Connection);

                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow NewRow = tampil.NewRow();
                    DateTime tanggalTagih = new DateTime(Convert.ToInt32((r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString())), Convert.ToInt32((r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString())), Convert.ToInt32((r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString())));

                    int daysDiff = ((TimeSpan)(tanggalTagih - DateTime.Today)).Days;
                    if (daysDiff < 0)
                    {
                        NewRow[0] = "Peringatan";
                        NewRow[1] = r[0].ToString();
                        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " sudah melebihi masa jatuh tempo pembayaran";

                        tampil.Rows.Add(NewRow);
                    }
                    else if (daysDiff <= 3)
                    {
                        NewRow[0] = "Pemberitahuan";
                        NewRow[1] = r[0].ToString();
                        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " telah memasuki masa tenggang pembayaran yang akan memiliki jatuh tempo " + r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString() + "-" + r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString() + "-" + r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString();
                        tampil.Rows.Add(NewRow);
                    }

                }

                adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.tagihan,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.status = 0 and p.kode_klien = k.kode and datediff(now(),p.deadline) < -3 order by p.kode", ClassConnection.Instance().Connection);

                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow NewRow = tampil.NewRow();
                    DateTime tanggalTagih = new DateTime(Convert.ToInt32((r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString())), Convert.ToInt32((r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString())), Convert.ToInt32((r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString())));

                    int daysDiff = ((TimeSpan)(tanggalTagih - DateTime.Today)).Days;
                    if (daysDiff < 0)
                    {
                        NewRow[0] = "Peringatan Invoice";
                        NewRow[1] = r[0].ToString();
                        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " sudah melebihi masa jatuh tempo invoicing";

                    }
                    else if (daysDiff <= 3)
                    {
                        NewRow[0] = "Pemberitahuan Invoice";
                        NewRow[1] = r[0].ToString();
                        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " telah memasuki masa tenggang yang akan memiliki jatuh tempo invoicing " + r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString() + "-" + r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString() + "-" + r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString();
                    }
                    else
                    {
                        NewRow[0] = "Project Berjalan";
                        NewRow[1] = r[0].ToString();
                        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " memiliki jatuh tempo " + r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString() + "-" + r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString() + "-" + r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString();
                    }
                    tampil.Rows.Add(NewRow);

                }


                dataGridViewNotif.DataSource = tampil;

                dataGridViewNotif.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewNotif.ColumnCount; i++)
                {
                    dataGridViewNotif.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    dataGridViewNotif.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                }

                foreach (DataGridViewRow r in dataGridViewNotif.Rows)
                {
                    if (r.Cells[0].Value.ToString() == "Pengeluaran Operasional")
                    {
                        r.DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                    if (r.Cells[0].Value.ToString() == "Bonus Tahunan")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                    }
                    if (r.Cells[0].Value.ToString() == "Bonus Project")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else if (r.Cells[0].Value.ToString() == "Utang Vendor")
                    {
                        r.DefaultCellStyle.BackColor = Color.DarkRed;
                    }
                    else if (r.Cells[0].Value.ToString() == "Masa Tenggang Vendor")
                    {
                        r.DefaultCellStyle.BackColor = Color.IndianRed;
                    }
                    else if (r.Cells[0].Value.ToString() == "Penggajian")
                    {
                        r.DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else if (r.Cells[0].Value.ToString() == "Jatuh Tempo Vendor")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    }
                    else if (r.Cells[0].Value.ToString() == "Pemberitahuan")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightSalmon;
                    }
                    else if (r.Cells[0].Value.ToString() == "Peringatan")
                    {
                        r.DefaultCellStyle.BackColor = Color.Red;
                    }
                    else if (r.Cells[0].Value.ToString() == "Pemberitahuan Invoice")
                    {
                        r.DefaultCellStyle.BackColor = Color.Magenta;
                    }
                    else if (r.Cells[0].Value.ToString() == "Peringatan Invoice")
                    {
                        r.DefaultCellStyle.BackColor = Color.DarkMagenta;
                    }
                    else if (r.Cells[0].Value.ToString() == "Project Berjalan")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
                dataGridViewNotif.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormCEO_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            updateDGVNotif();
        }

        private void DataGridViewNotif_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Pengeluaran Operasional")
            {
                ((FormParent)this.MdiParent).panggilAccPengeluaranOps(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString(), 1);
            }
            else if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Bonus Tahunan")
            {
                ((FormParent)this.MdiParent).panggilAccBonus(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString(), 1);
            }
            else if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Bonus Project")
            {
                ((FormParent)this.MdiParent).panggilAccBonus(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString(), 0);
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                updateDGVNotif();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                updateDGVVendor();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                updateDGVKlien();
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                updateDGVProject();
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                updateChart();
            }
            else if (tabControl1.SelectedIndex == 5)
            {
                updateDGVJatuhTempo();
            }
            else if (tabControl1.SelectedIndex == 6)
            {
                radioButtonLaporanProject.Checked = true;
            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            updateDGVVendor("SELECT Kode,Nama,Alamat,Notelp,Kota,Jenisvendor as 'Jenis Vendor' from datavendor where nama like '%" + textBoxSearchVendor.Text + "%'order by kode");
        }

        private void ButtonPrintVendor_Click(object sender, EventArgs e)
        {
            CrystalReportPrintVendor cr = new CrystalReportPrintVendor();
            ((FormParent)this.MdiParent).panggilTampilVendor(cr);
        }

        private void DataGridViewTampilKlien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ButtonKlien_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxSearchKode_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void ButtonSearchProject_Click(object sender, EventArgs e)
        {
            updateDGVProject("SELECT p.Kode,k.nama as 'Nama Klien' , p.nama as 'Nama Project', date_format(p.tagihan,'%d-%m-%Y') as 'Tagihan',date_format(p.deadline,'%d-%m-%Y') as 'Deadline', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total', CONCAT('Rp.',FORMAT(p.totalbiaya-p.kurang,2)) as 'Kurang Biaya',IF(p.status = 0, 'Belum Selesai', 'Sudah Selesai') AS 'Status Project' FROM dataproject p, dataklien k where p.kode_klien = k.kode and lower(p.nama) like '%" + textBoxSearchProject.Text.ToLower() + "%' order by kode");
        }

        private void ButtonKlien_Click_1(object sender, EventArgs e)
        {
            updateDGVKlien("SELECT Kode, Nama, Principal, User, Notelp ,'Rp.' ||FORMAT(bataspiutang,2) as 'Piutang' FROM dataklien where lower(nama) like '%" + textBoxSearchKode.Text.ToLower() + "%' order by kode");
        }

        private void RadioButtonLaporanProject_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLaporanProject.Checked)
            {
                comboBoxBulanTahun.Visible = radioButtonLaporanBulan.Checked;
                comboBoxLaporan.DataSource = null;
                try
                {
                    MySqlDataAdapter adapterPJ = new MySqlDataAdapter("SELECT kode,nama FROM dataproject order by kode", ClassConnection.Instance().Connection);
                    DataSet datasetPJ = new DataSet();
                    adapterPJ.Fill(datasetPJ);
                    comboBoxLaporan.DisplayMember = "nama";
                    comboBoxLaporan.ValueMember = "kode";
                    comboBoxLaporan.DataSource = datasetPJ.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Combo Project Vendor");
                }
            }
        }

        private void RadioButtonLaporanBulan_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLaporanBulan.Checked)
            {
                MessageBox.Show("Data Belum 100% Valid sampai Accounting Terbuat");
                comboBoxBulanTahun.Visible = radioButtonLaporanBulan.Checked;
                comboBoxBulanTahun.DataSource = Enumerable.Range(2000, DateTime.Today.Year - 1999).ToList();

                comboBoxLaporan.DataSource = null;
                comboBoxLaporan.DataSource = CultureInfo.GetCultureInfo("id-ID").DateTimeFormat.MonthNames.Take(12).ToList();
            }
        }

        private void RadioButtonLaporanTahun_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLaporanTahun.Checked)
            {
                MessageBox.Show("Data Belum 100% Valid sampai Accounting Terbuat");
                comboBoxBulanTahun.Visible = radioButtonLaporanBulan.Checked;
                comboBoxLaporan.DataSource = null;
                comboBoxLaporan.DataSource = Enumerable.Range(2000, DateTime.Today.Year - 1999).ToList();
            }
        }

        private void ButtonSearchJatuhTempo_Click(object sender, EventArgs e)
        {
            updateDGVJatuhTempo(textBoxSearchJatuhTempo.Text);
        }

        private void ComboBoxLaporan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLaporan.DataSource != null)
            {
                dataGridViewLaporan.DataSource = null;
                DataTable tb = new DataTable();
                if (radioButtonLaporanTahun.Checked)
                {
                    long totalReal = 0;
                    tb.Columns.Add("List Data");
                    tb.Columns.Add("Realisasi");
                    tb.Columns.Add("Purpose");
                    try
                    {
                        DataRow row = tb.NewRow();
                        row[0] = "Project Dibayar";
                        row[1] = "-";
                        row[2] = "-";
                        tb.Rows.Add(row);

                        MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT p.nama,sum(pg.jumlah) from dataproject p, pettygiro pg where pg.kodedebitkredit = 'D' and pg.id_project = p.kode and year(pg.tanggal) = " + comboBoxLaporan.Text + " group by p.nama", ClassConnection.Instance().Connection);
                        DataTable tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach (DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = r[0].ToString();
                            row[1] = "Rp. " + Convert.ToInt64(r[1].ToString()).ToString("N");
                            totalReal += Convert.ToInt64(r[1].ToString());
                            row[2] = "Ke Petty Giro";
                            tb.Rows.Add(row);
                        }

                        row = tb.NewRow();
                        row[0] = "Bayar Vendor";
                        row[1] = "-";
                        row[2] = "-";
                        tb.Rows.Add(row);

                        adapter = new MySqlDataAdapter("SELECT v.nama, SUM(pg.jumlah) FROM pettygiro pg, datavendor v WHERE v.kode = pg.`kode_vendor` AND YEAR(pg.`tanggal`) = " + comboBoxLaporan.Text + " GROUP BY v.nama", ClassConnection.Instance().Connection);
                        tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach (DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = r[0].ToString();
                            row[1] = "Rp. " + Convert.ToInt64(r[1].ToString()).ToString("N");
                            totalReal -= Convert.ToInt64(r[1].ToString());
                            row[2] = "Dari Petty Giro";
                            tb.Rows.Add(row);
                        }

                        adapter = new MySqlDataAdapter("SELECT v.nama, SUM(pj.jumlah) FROM pettyproject pj, datavendor v WHERE v.kode = pj.`kode_vendor` AND YEAR(pj.`tanggal`) = " + comboBoxLaporan.Text + " GROUP BY v.nama", ClassConnection.Instance().Connection);
                        tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach (DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = r[0].ToString();
                            row[1] = "Rp. " + Convert.ToInt64(r[1].ToString()).ToString("N");
                            totalReal -= Convert.ToInt64(r[1].ToString());
                            row[2] = "Dari Petty Project";
                            tb.Rows.Add(row);
                        }

                        row = tb.NewRow();
                        row[0] = "Pengeluaran Operasional";
                        row[1] = "-";
                        row[2] = "-";
                        tb.Rows.Add(row);

                        adapter = new MySqlDataAdapter("SELECT jumlah,nama from pettyops where kodedebitkredit = 'K' and  YEAR(tanggal) = " + comboBoxLaporan.Text + "", ClassConnection.Instance().Connection);
                        tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach (DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = r[1].ToString();
                            row[1] = "Rp. " + Convert.ToInt64(r[0].ToString()).ToString("N");
                            totalReal -= Convert.ToInt64(r[0].ToString());
                            row[2] = "Dari Petty Ops";
                            tb.Rows.Add(row);
                        }


                        row = tb.NewRow();
                        row[0] = "Pengeluaran Kas Kecil";
                        row[1] = "-";
                        row[2] = "-";
                        tb.Rows.Add(row);

                        adapter = new MySqlDataAdapter("SELECT jumlah,nama from pettykas where kodedebitkredit = 'K' and  YEAR(tanggal) = " + comboBoxLaporan.Text + "", ClassConnection.Instance().Connection);
                        tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach (DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = r[1].ToString();
                            row[1] = "Rp. " + Convert.ToInt64(r[0].ToString()).ToString("N");
                            totalReal -= Convert.ToInt64(r[0].ToString());
                            row[2] = "Dari Petty Kas";
                            tb.Rows.Add(row);
                        }


                        row = tb.NewRow();
                        row[0] = "Total";
                        row[1] = "-";
                        row[2] = "-";
                        tb.Rows.Add(row);

                        row = tb.NewRow();
                        row[0] = "Total Laba Rugi";
                        row[1] = "Rp. " + Convert.ToInt32(totalReal).ToString("N");
                        row[2] = "-";
                        tb.Rows.Add(row);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Data Tahunan");
                    }
                }
                else if (radioButtonLaporanBulan.Checked)
                {
                    long totalReal = 0;
                    tb.Columns.Add("List Data");
                    tb.Columns.Add("Realisasi");
                    tb.Columns.Add("Purpose");
                    try
                    {
                        DataRow row = tb.NewRow();
                        row[0] = "Project Dibayar";
                        row[1] = "-";
                        row[2] = "-";
                        tb.Rows.Add(row);

                        MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT p.nama,sum(pg.jumlah) from dataproject p, pettygiro pg where pg.kodedebitkredit = 'D' and pg.id_project = p.kode and month(pg.tanggal) = "+(comboBoxLaporan.SelectedIndex+1)+" and year(pg.tanggal) = "+comboBoxBulanTahun.Text+" group by p.nama", ClassConnection.Instance().Connection);
                        DataTable tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach(DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = r[0].ToString();
                            row[1] = "Rp. " + Convert.ToInt64(r[1].ToString()).ToString("N");
                            totalReal += Convert.ToInt64(r[1].ToString());
                            row[2] = "Ke Petty Giro";
                            tb.Rows.Add(row);
                        }

                        row = tb.NewRow();
                        row[0] = "Bayar Vendor";
                        row[1] = "-";
                        row[2] = "-";
                        tb.Rows.Add(row);

                        adapter = new MySqlDataAdapter("SELECT v.nama, SUM(pg.jumlah) FROM pettygiro pg, datavendor v WHERE v.kode = pg.`kode_vendor` AND MONTH(pg.tanggal) = " + (comboBoxLaporan.SelectedIndex + 1) + " AND YEAR(pg.`tanggal`) = " + comboBoxBulanTahun.Text + " GROUP BY v.nama", ClassConnection.Instance().Connection);
                        tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach (DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = r[0].ToString();
                            row[1] = "Rp. " + Convert.ToInt64(r[1].ToString()).ToString("N");
                            totalReal -= Convert.ToInt64(r[1].ToString());
                            row[2] = "Dari Petty Giro";
                            tb.Rows.Add(row);
                        }

                        adapter = new MySqlDataAdapter("SELECT v.nama, SUM(pj.jumlah) FROM pettyproject pj, datavendor v WHERE v.kode = pj.`kode_vendor` AND MONTH(pj.tanggal) = " + (comboBoxLaporan.SelectedIndex + 1) + " AND YEAR(pj.`tanggal`) = " + comboBoxBulanTahun.Text + " GROUP BY v.nama", ClassConnection.Instance().Connection);
                        tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach (DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = r[0].ToString();
                            row[1] = "Rp. " + Convert.ToInt64(r[1].ToString()).ToString("N");
                            totalReal -= Convert.ToInt64(r[1].ToString());
                            row[2] = "Dari Petty Project";
                            tb.Rows.Add(row);
                        }

                        row = tb.NewRow();
                        row[0] = "Pengeluaran Operasional";
                        row[1] = "-";
                        row[2] = "-";
                        tb.Rows.Add(row);

                        adapter = new MySqlDataAdapter("SELECT jumlah,nama from pettyops where kodedebitkredit = 'K' and MONTH(tanggal) = " + (comboBoxLaporan.SelectedIndex + 1) + " AND YEAR(tanggal) = " + comboBoxBulanTahun.Text + "", ClassConnection.Instance().Connection);
                        tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach (DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = r[1].ToString();
                            row[1] = "Rp. " + Convert.ToInt64(r[0].ToString()).ToString("N");
                            totalReal -= Convert.ToInt64(r[0].ToString());
                            row[2] = "Dari Petty Ops";
                            tb.Rows.Add(row);
                        }


                        row = tb.NewRow();
                        row[0] = "Pengeluaran Kas Kecil";
                        row[1] = "-";
                        row[2] = "-";
                        tb.Rows.Add(row);

                        adapter = new MySqlDataAdapter("SELECT jumlah,nama from pettykas where kodedebitkredit = 'K' and MONTH(tanggal) = " + (comboBoxLaporan.SelectedIndex + 1) + " AND YEAR(tanggal) = " + comboBoxBulanTahun.Text + "", ClassConnection.Instance().Connection);
                        tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach (DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = r[1].ToString();
                            row[1] = "Rp. " + Convert.ToInt64(r[0].ToString()).ToString("N");
                            totalReal -= Convert.ToInt64(r[0].ToString());
                            row[2] = "Dari Petty Kas";
                            tb.Rows.Add(row);
                        }


                        row = tb.NewRow();
                        row[0] = "Total";
                        row[1] = "-";
                        row[2] = "-";
                        tb.Rows.Add(row);

                        row = tb.NewRow();
                        row[0] = "Total Laba Rugi";
                        row[1] = "Rp. " + Convert.ToInt32(totalReal).ToString("N");
                        row[2] = "-";
                        tb.Rows.Add(row);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Data Bulanan");
                    }

                }
                else if (radioButtonLaporanProject.Checked)
                {
                    long totalExpec = 0;
                    long totalReal = 0;
                    tb.Columns.Add("List Data");
                    tb.Columns.Add("Expected");
                    tb.Columns.Add("Realisasi");
                    tb.Columns.Add("Status");
                    try
                    {
                        DataRow row = tb.NewRow();
                        row[0] = "Project";
                        row[1] = "-";
                        row[2] = "-";
                        row[3] = "-";
                        tb.Rows.Add(row);

                        MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT nama,totalbiaya, kurang, IF(status=1,'Sudah Selesai','Belum Selesai') from dataproject where kode = '" + comboBoxLaporan.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
                        DataTable tmp = new DataTable();
                        adapter.Fill(tmp);

                        row = tb.NewRow();
                        row[0] = tmp.Rows[0][0].ToString();
                        row[1] = "Rp. " + Convert.ToInt64(tmp.Rows[0][1].ToString()).ToString("N");
                        totalExpec += Convert.ToInt64(tmp.Rows[0][1].ToString());
                        row[2] = "Rp. " + Convert.ToInt64(tmp.Rows[0][2].ToString()).ToString("N");
                        totalReal += Convert.ToInt64(tmp.Rows[0][2].ToString());
                        row[3] = tmp.Rows[0][3].ToString();
                        tb.Rows.Add(row);

                        row = tb.NewRow();
                        row[0] = "PPN";
                        row[1] = "-";
                        row[2] = "-";
                        row[3] = "-";
                        tb.Rows.Add(row);

                        row = tb.NewRow();
                        row[0] = "PPN Project";
                        row[1] = "Rp. " + (Convert.ToInt64(tmp.Rows[0][1].ToString()) * 10 / 110).ToString("N"); ;
                        totalExpec -= (Convert.ToInt64(tmp.Rows[0][1].ToString()) * 10 / 110);
                        row[2] = "Rp. " + (Convert.ToInt64(tmp.Rows[0][2].ToString()) * 10 / 110).ToString("N");
                        totalReal -= (Convert.ToInt64(tmp.Rows[0][2].ToString()) * 10 / 110);
                        row[3] = tmp.Rows[0][3].ToString();
                        tb.Rows.Add(row);

                        row = tb.NewRow();
                        row[0] = "Vendor";
                        row[1] = "-";
                        row[2] = "-";
                        row[3] = "-";
                        tb.Rows.Add(row);

                        adapter = new MySqlDataAdapter("SELECT v.nama,vp.total, vp.sudahdibayar, IF(vp.total-vp.sudahdibayar=0,'Sudah Selesai','Belum Selesai') from datavendorproject vp, datavendor v where vp.kodevendor = v.kode and vp.kodeproject = '" + comboBoxLaporan.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
                        tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach (DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = r[0].ToString();
                            row[1] = "Rp. " + Convert.ToInt64(r[1].ToString()).ToString("N");
                            totalExpec -= Convert.ToInt64(tmp.Rows[0][1].ToString());
                            row[2] = "Rp. " + Convert.ToInt64(r[2].ToString()).ToString("N");
                            totalReal -= Convert.ToInt64(tmp.Rows[0][2].ToString());
                            row[3] = r[3].ToString();
                            tb.Rows.Add(row);
                        }

                        row = tb.NewRow();
                        row[0] = "Cash Advance";
                        row[1] = "-";
                        row[2] = "-";
                        row[3] = "-";
                        tb.Rows.Add(row);

                        adapter = new MySqlDataAdapter("SELECT kode,total from pettyca where status>=3 and caproject ='" + comboBoxLaporan.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
                        tmp = new DataTable();
                        adapter.Fill(tmp);

                        foreach (DataRow r in tmp.Rows)
                        {
                            row = tb.NewRow();
                            row[0] = "Cash Adv";
                            row[1] = "Rp. " + Convert.ToInt32(0).ToString("N");
                            row[2] = "Rp. " + Convert.ToInt32(r[1].ToString()).ToString("N");
                            totalReal -= Convert.ToInt64(r[1].ToString());
                            row[3] = "Sudah Selesai";
                            tb.Rows.Add(row);
                        }

                        row = tb.NewRow();
                        row[0] = "Total";
                        row[1] = "-";
                        row[2] = "-";
                        row[3] = "-";
                        tb.Rows.Add(row);

                        row = tb.NewRow();
                        row[0] = "Total Laba Rugi";
                        row[1] = "Rp. " + Convert.ToInt32(totalExpec).ToString("N");
                        row[2] = "Rp. " + Convert.ToInt32(totalReal).ToString("N");
                        row[3] = tb.Rows[1][3].ToString();
                        tb.Rows.Add(row);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Data Project");
                    }

                }
                dataGridViewLaporan.DataSource = tb;
                dataGridViewLaporan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewLaporan.ColumnCount; i++)
                {
                    dataGridViewLaporan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dataGridViewLaporan.Invalidate();
            }
            
        }

        private void DataGridViewLaporan_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ChartKlien_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult hit = chartKlien.HitTest(e.X, e.Y, ChartElementType.DataPoint);

            if (hit.PointIndex >= 0 && hit.Series != null)
            {
                DataPoint dp = chartKlien.Series[0].Points[hit.PointIndex];
                MessageBox.Show(dp.XValue.ToString() + " Hari");
                List<string> tampilkan = new List<string>();
                DataTable tmp = tableChart.Clone();
                foreach (DataRow r in tableChart.Rows)
                {
                    if (Convert.ToInt32(r[2].ToString()) <= dp.XValue && Convert.ToInt32(r[2].ToString()) > (dp.XValue - 30))
                    {
                        tmp.ImportRow(r);
                        //tampilkan.Add(r[0].ToString()+" dalam project "+r[1].ToString());
                    }
                }
                ((FormParent)this.MdiParent).panggilTampilSementara(tmp);
                //MessageBox.Show("Berikut adalah Orang-Orangnya : "+String.Join("-",tampilkan.ToArray()));
            }
        }

        private void comboBoxBulanTahun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
