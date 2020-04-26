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
    public partial class FormKepCab : Form
    {
        public FormKepCab()
        {
            InitializeComponent();
        }
        Dictionary<string, long> duit = new Dictionary<string, long>();
        public bool jakartatidak = true;
        public void resetMoney()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlDataReader reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT Nama,Money FROM saldo", ClassConnection.Instance().Connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < comboBoxTipeUang.Items.Count; i++)
                        {
                            if (comboBoxTipeUang.Items[i].ToString() == reader["Nama"].ToString())
                            {
                                duit[comboBoxTipeUang.Items[i].ToString()] = Convert.ToInt64(reader["Money"]);
                                break;
                            }
                        }
                    }
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void updateLihatUang()
        {
            comboBoxTipeUang.Items.Add("Operasional");
            duit.Add("Operasional", 0);
            comboBoxTipeUang.Items.Add("Project");
            duit.Add("Project", 0);
            comboBoxTipeUang.Items.Add("Kas Kecil");
            duit.Add("Kas Kecil", 0);
            comboBoxTipeUang.Items.Add("Giro");
            duit.Add("Giro", 0);
            resetMoney();
            comboBoxTipeUang.SelectedIndex = 0;
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
        public void updateDGVProject(string cmd = "SELECT p.Kode,k.nama as 'Nama Klien' , p.nama as 'Nama Project', date_format(p.tagihan,'%d-%m-%Y') as 'Tagihan',date_format(p.deadline,'%d-%m-%Y') as 'Deadline', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total', CONCAT('Rp.',FORMAT(p.totalbiaya-p.kurang,2)) as 'Kurang Biaya' ,IF(p.status = 0, 'Belum Selesai', 'Sudah Selesai') AS 'Status Project' FROM dataproject p, dataklien k where p.kode like 'PR%' and p.kode_klien = k.kode order by kode")
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
        DataSet datasetKaryawan;
        public void updateDGVKaryawan(string cmd = "SELECT Kode,Nama,notelp as 'No. Telp',Jabatan,Area from datakaryawan where insertdata > str_to_date('2019-08-10','%Y-%m-%d') order by kode")
        {
            try
            {
                datasetKaryawan = new DataSet();
                dataGridViewTampilKaryawan.DataSource = null;
                dataGridViewTampilKaryawan.Columns.Clear();
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);

                adapterKlien.Fill(datasetKaryawan);
                dataGridViewTampilKaryawan.DataSource = datasetKaryawan.Tables[0];
                DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
                edit.HeaderText = "Edit";
                edit.Text = "Edit";
                edit.Name = "Edit";
                edit.UseColumnTextForButtonValue = true;
                dataGridViewTampilKaryawan.Columns.Add(edit);

                DataGridViewButtonColumn view = new DataGridViewButtonColumn();
                view.HeaderText = "View";
                view.Text = "View";
                view.Name = "View";
                view.UseColumnTextForButtonValue = true;
                dataGridViewTampilKaryawan.Columns.Add(view);

                DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
                delete.HeaderText = "Delete";
                delete.Text = "Delete";
                delete.Name = "Delete";
                delete.UseColumnTextForButtonValue = true;
                dataGridViewTampilKaryawan.Columns.Add(delete);

                dataGridViewTampilKaryawan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewTampilKaryawan.ColumnCount; i++)
                {
                    dataGridViewTampilKaryawan.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; 
                    dataGridViewTampilKaryawan.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Karyawan");
            }
        }
        private void ButtonKaryawan_Click(object sender, EventArgs e)
        {
            ((FormParent)this.MdiParent).panggilKaryawan(1);
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            updateDGVKaryawan("SELECT Kode,Nama,notelp as 'No. Telp',Jabatan,Area from datakaryawan where nama like '%" + textBoxSearchKode.Text + "%' order by kode");
        }
        bool awal = true;
        private void DataGridViewTampilKaryawan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (awal)
            {
                if (dataGridViewTampilKaryawan.Columns[e.ColumnIndex].Name == "Edit")
                {
                    ((FormParent)this.MdiParent).panggilKaryawan(2, datasetKaryawan.Tables[0].Rows[e.RowIndex][0].ToString());
                }
                else if (dataGridViewTampilKaryawan.Columns[e.ColumnIndex].Name == "View")
                {
                    ((FormParent)this.MdiParent).panggilKaryawan(3, datasetKaryawan.Tables[0].Rows[e.RowIndex][0].ToString());
                }
                else if (dataGridViewTampilKaryawan.Columns[e.ColumnIndex].Name == "Delete")
                {
                    if (MessageBox.Show("Apakah anda yakin ingin mendelete " + datasetKaryawan.Tables[0].Rows[e.RowIndex][1].ToString() + " ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                string commandText = "DELETE FROM datakaryawan WHERE kode = @kode";
                                MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", datasetKaryawan.Tables[0].Rows[e.RowIndex][0].ToString());
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show("Data telah terhapus", "Berhasil");
                                ClassConnection.Instance().Close();
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error");
                        }
                    }
                }
                awal = false;
            }
            else
            {
                if (dataGridViewTampilKaryawan.Columns[e.ColumnIndex].Name == "Edit")
                {
                    ((FormParent)this.MdiParent).panggilKaryawan(2, dataGridViewTampilKaryawan.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                else if (dataGridViewTampilKaryawan.Columns[e.ColumnIndex].Name == "View")
                {
                    ((FormParent)this.MdiParent).panggilKaryawan(3, dataGridViewTampilKaryawan.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                else if (dataGridViewTampilKaryawan.Columns[e.ColumnIndex].Name == "Delete")
                {
                    if (MessageBox.Show("Apakah anda yakin ingin mendelete " + dataGridViewTampilKaryawan.Rows[e.RowIndex].Cells[1].Value.ToString() + " ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                string commandText = "DELETE FROM datakaryawan WHERE kode = @kode";
                                MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", dataGridViewTampilKaryawan.Rows[e.RowIndex].Cells[0].Value.ToString());
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show("Data telah terhapus", "Berhasil");
                                ClassConnection.Instance().Close();
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error");
                        }
                    }
                }
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
                MySqlDataAdapter adapterKlien;
                if (jakartatidak == true)
                {
                     adapterKlien = new MySqlDataAdapter("SELECT ca.kode,k.nama,concat('Rp. ',format(ca.total,2)) from pettyca ca, datakaryawan k where ca.kode like 'JCA%' and ca.kodekaryawan = k.kode and ca.status = 1 order by kode", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT ca.kode,k.nama,concat('Rp. ',format(ca.total,2)) from pettyca ca, datakaryawan k where ca.kode like 'CA%' and ca.kodekaryawan = k.kode and ca.status = 1 order by kode", ClassConnection.Instance().Connection);
                }
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "CashAdv";
                    row[1] = r[0].ToString();
                    row[2] = "Anda memiliki Cash Advance yang belum terapprove oleh " + r[1].ToString() + " dengan total " + r[2].ToString();
                    tampil.Rows.Add(row);
                }
                if (jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT kode,nama,format(jumlah,2) from pettyproject where status = 0 and kode like 'JIPG%'", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT kode,nama,format(jumlah,2) from pettyproject where status = 0 and kode like 'IPG%'", ClassConnection.Instance().Connection);
                }
                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Pengeluaran Project";
                    row[1] = r[0].ToString();
                    row[2] = "Anda memiliki Pengeluaran Project yang belum terapprove dengan nama : " + r[1].ToString() + " dan total Rp. " + r[2].ToString();
                    tampil.Rows.Add(row);
                }

                if (jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT p.tanggal from penggajian p, datakaryawan k where p.status = 0 and k.area = 'Jakarta' group by tanggal", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT p.tanggal from penggajian p, datakaryawan k where p.status = 0 and k.area = 'Jakarta' group by tanggal", ClassConnection.Instance().Connection);
                }

                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                List<string> bulan = CultureInfo.GetCultureInfo("id-ID").DateTimeFormat.MonthNames.Take(12).ToList();
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Penggajian";
                    row[1] = r[0].ToString();
                    row[2] = "Anda memiliki Penggajian yang belum di Approve untuk bulan "+bulan[Convert.ToInt32(r[0].ToString().Split('-')[0])-1];
                    tampil.Rows.Add(row);
                }
                
                if(jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT v.nama, FORMAT((vp.total-vp.sudahdibayar),2), p.nama, DATE_FORMAT(vp.`jatuhtempo`,'%d-%m-%Y') FROM datavendorproject vp, datavendor v, dataproject p WHERE p.kode like 'JPRJ%' and vp.`kodeproject` = p.kode AND vp.kodevendor = v.kode AND (vp.total-vp.sudahdibayar) > 0 GROUP BY v.nama ORDER BY vp.jatuhtempo", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT v.nama, FORMAT((vp.total-vp.sudahdibayar),2), p.nama, DATE_FORMAT(vp.`jatuhtempo`,'%d-%m-%Y') FROM datavendorproject vp, datavendor v, dataproject p WHERE p.kode like 'PRJ%' and  vp.`kodeproject` = p.kode AND vp.kodevendor = v.kode AND (vp.total-vp.sudahdibayar) > 0 GROUP BY v.nama ORDER BY vp.jatuhtempo", ClassConnection.Instance().Connection);
                }
                
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

                if (jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.deadline,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.kode like 'JPRJ%' and p.status = 0 and p.kode_klien = k.kode order by p.kode", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.deadline,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.kode like 'PRJ%' and p.status = 0 and p.kode_klien = k.kode order by p.kode", ClassConnection.Instance().Connection);
                }

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

                if (jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.tagihan,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.kode like 'JPRJ%' and  p.status = 0 and p.kode_klien = k.kode and datediff(now(),p.deadline) < -3 order by p.kode", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.tagihan,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.kode like 'PRJ%' and  p.status = 0 and p.kode_klien = k.kode and datediff(now(),p.deadline) < -3 order by p.kode", ClassConnection.Instance().Connection);
                }
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
                    if (r.Cells[0].Value.ToString() == "CashAdv")
                    {
                        r.DefaultCellStyle.BackColor = Color.HotPink;
                    }
                    else if (r.Cells[0].Value.ToString() == "Pengeluaran Project")
                    {
                        r.DefaultCellStyle.BackColor = Color.DarkOrange;
                    }
                    else if (r.Cells[0].Value.ToString() == "Penggajian")
                    {
                        r.DefaultCellStyle.BackColor = Color.GreenYellow;
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
                    else if (r.Cells[0].Value.ToString() == "Bonus Tahunan" || r.Cells[0].Value.ToString() == "Bonus Project")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightSeaGreen;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error Notif");
            }
        }
        private void FormKepCab_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            updateDGVKaryawan();
            dataGridViewNotif.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void update_area(string area)
        {
            if(area != "JAKARTA")
            {
                jakartatidak = false;
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                updateDGVKaryawan();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                updateDGVNotif();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                updateDGVVendor();
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                updateDGVKlien();
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                updateDGVProject();
            }
            else if (tabControl1.SelectedIndex == 5)
            {
                updateChart();
            }
            else if (tabControl1.SelectedIndex == 6)
            {
                updateDGVJatuhTempo();
            }
            else if (tabControl1.SelectedIndex == 7)
            {
                updateLihatUang();
            }
        }

        private void DataGridViewNotif_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "CashAdv")
            {
                ((FormParent)this.MdiParent).panggilCA(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString(),3);
            }
            if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Pengeluaran Project")
            {
                ((FormParent)this.MdiParent).panggilAccPengeluaranKepCab(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Penggajian")
            {
                ((FormParent)this.MdiParent).panggilPenggajian(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
        }

        private void DataGridViewNotif_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ButtonPrintVendor_Click(object sender, EventArgs e)
        {
            CrystalReportPrintVendor cr = new CrystalReportPrintVendor();
            ((FormParent)this.MdiParent).panggilTampilVendor(cr);
        }

        private void ButtonSearchProject_Click(object sender, EventArgs e)
        {
            updateDGVProject("SELECT p.Kode,k.nama as 'Nama Klien' , p.nama as 'Nama Project', date_format(p.tagihan,'%d-%m-%Y') as 'Tagihan',date_format(p.deadline,'%d-%m-%Y') as 'Deadline', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total', CONCAT('Rp.',FORMAT(p.totalbiaya-p.kurang,2)) as 'Kurang Biaya',IF(p.status = 0, 'Belum Selesai', 'Sudah Selesai') AS 'Status Project' FROM dataproject p, dataklien k where p.kode like 'PR%' and p.kode_klien = k.kode and lower(p.nama) like '%" + textBoxSearchProject.Text.ToLower() + "%' order by kode");
        }
        
        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void ComboBoxTipeUang_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDownUang.Value = duit[comboBoxTipeUang.Text];
        }

        private void ButtonLihatUang_Click(object sender, EventArgs e)
        {
            string lihat = comboBoxTipeUang.Text == "Operasional" ? "pettyops" : comboBoxTipeUang.Text == "Project" ? "pettyproject" : comboBoxTipeUang.Text == "Kas Kecil" ? "pettykas" : "pettygiro";
            DataTable tmp = new DataTable();
            DataTable tampil = new DataTable();
            tampil.Columns.Add("Nama");
            tampil.Columns.Add("No Rekening");
            tampil.Columns.Add("Tipe");
            tampil.Columns.Add("Jumlah");
            tampil.Columns.Add("Tanggal");
            tampil.Columns.Add("Deskripsi");

            dataGridViewLihatUang.DataSource = null;
            MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT nama,norek,IF(kodedebitkredit='K','Kredit','Debit'),concat('Rp. ',format(jumlah,2)),DATE_FORMAT(tanggal,'%d-%m-%y'),deskripsi FROM " + lihat+" WHERE tanggal >= STR_TO_DATE('"+dateTimePickerStart.Value.ToString("yyyy-MM-dd")+ "', '%Y-%m-%d') AND tanggal <= STR_TO_DATE('" + dateTimePickerEnd.Value.ToString("yyyy-MM-dd") + "', '%Y-%m-%d') ORDER BY tanggal,insertdata", ClassConnection.Instance().Connection);
            string tmpTanggal = "";
            adapterKlien.Fill(tmp);
            foreach (DataRow r in tmp.Rows)
            {
                if (tmpTanggal != r[4].ToString())
                {
                    DataRow rBayangan = tampil.NewRow();
                    rBayangan[0] = r[4].ToString();
                    rBayangan[1] = "-";
                    tmpTanggal = r[4].ToString();
                    tampil.Rows.Add(rBayangan);
                }
                DataRow row = tampil.NewRow();

                row[0] = r[0];
                row[1] = r[1];
                row[2] = r[2];
                row[3] = r[3];
                row[4] = r[4];
                row[5] = r[5];
                tampil.Rows.Add(row);
            }
            dataGridViewLihatUang.DataSource = tampil;

            dataGridViewLihatUang.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for (int i = 0; i < dataGridViewLihatUang.ColumnCount; i++)
            {
                dataGridViewLihatUang.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewLihatUang.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            foreach (DataGridViewRow r in dataGridViewLihatUang.Rows)
            {
                if (r.Cells[1].Value.ToString() == "-")
                {
                    r.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
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

        private void ButtonSearchJatuhTempo_Click(object sender, EventArgs e)
        {
            updateDGVJatuhTempo(textBoxSearchJatuhTempo.Text);
        }
    }
}
