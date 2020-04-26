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
    public partial class FormFinance : Form
    {
        public FormFinance()
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
        public void updateComboVendor()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlDataAdapter adapterPJ = new MySqlDataAdapter("SELECT v.nama, vp.kodevendor from datavendorproject vp, datavendor v where vp.kodevendor = v.kode and vp.kodeproject = '" + comboBoxListProject.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
                    DataSet datasetPJ = new DataSet();
                    adapterPJ.Fill(datasetPJ);
                    comboBoxListVendorProject.DisplayMember = "nama";
                    comboBoxListVendorProject.ValueMember = "kodevendor";
                    comboBoxListVendorProject.DataSource = datasetPJ.Tables[0];
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void updateComboListProject()
        {
            comboBoxListProject.DataSource = null;
            try
            {
                MySqlDataAdapter adapterLP = new MySqlDataAdapter("SELECT kode,nama FROM dataproject order by kode", ClassConnection.Instance().Connection);
                DataSet datasetLP = new DataSet();
                adapterLP.Fill(datasetLP);
                comboBoxListProject.DisplayMember = "nama";
                comboBoxListProject.ValueMember = "kode";
                comboBoxListProject.DataSource = datasetLP.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void resetKodePengeluaranProject()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "GIPG" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from pettygiro where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(10, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxProjectPengeluaranKode.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void resetKodePemasukanProject()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "GIPM" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from pettygiro where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(10, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxProjectPemasukanKode.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public long MoneyGiro = 0;
        public void resetMoney()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT Money FROM saldo where Nama = 'Giro'", ClassConnection.Instance().Connection);
                    MoneyGiro = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                    labelProjectSaldo.Text = MoneyGiro.ToString("N");
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
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
        int sudah=0;
        public void cekBulan()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT count(kode) from penggajian where tanggal = concat('"+(comboBoxBulan.SelectedIndex+1).ToString()+"','-',YEAR(NOW()))", ClassConnection.Instance().Connection);
                    if (cmd.ExecuteScalar().ToString() != "0")
                    {
                        sudah = 1;
                    }
                    else
                    {
                        sudah = 0;
                    }
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error Cek");
            }
        }
        public void resetKodeKirim()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = (radioButtonKirimJkt.Checked ? "JUG" : "UG") + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from historysaldo where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring((radioButtonKirimJkt.Checked ? 9 : 8), 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxKode.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        bool awal = true;
        public void updateDGVGaji()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT kode as Kode,nama as Nama,gajipokok as Gaji,0 as 'Tunjangan',0 as 'Potongan', date_format(tanggalmasuk,'%d-%m-%Y') as 'Tanggal Masuk', jabatan as 'Jabatan' from datakaryawan where insertdata > str_to_date('2019-08-10','%Y-%m-%d') order by kode ", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    reader.Fill(table);
                    dataGridViewGaji.DataSource = table;

                    dataGridViewGaji.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dataGridViewGaji.Columns[2].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewGaji.Columns[2].DefaultCellStyle.Format = "c2";
                    dataGridViewGaji.Columns[2].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
                    dataGridViewGaji.Columns[3].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewGaji.Columns[3].DefaultCellStyle.Format = "c2";
                    dataGridViewGaji.Columns[3].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
                    dataGridViewGaji.Columns[4].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewGaji.Columns[4].DefaultCellStyle.Format = "c2";
                    dataGridViewGaji.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
                    for (int i = 0; i < dataGridViewGaji.ColumnCount; i++)
                    {

                        int[] contain = { 2, 3, 4 };
                        if (!contain.Contains(i)) dataGridViewGaji.Columns[i].ReadOnly = true;
                        dataGridViewGaji.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridViewGaji.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                    }
                    ClassConnection.Instance().Close();
                    awal = true;
                    comboBoxBulan.DataSource = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
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
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT kode,nama,format(jumlah,2) from pettyops where status = 0 and dividen = 0", ClassConnection.Instance().Connection);
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
                MessageBox.Show(ex.Message);
            }
        }
        public void updateDGVUang()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    DataTable tampil = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT kode,tipe,format(jumlah,2),date_format(insertdata,'%d-%m-%Y %H:%i:%s') from historysaldo", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    reader.Fill(table);

                    tampil.Columns.Add("Tentang");
                    tampil.Columns.Add("Kode");
                    tampil.Columns.Add("Deskripsi");

                    foreach(DataRow r in table.Rows)
                    {
                        DataRow row = tampil.NewRow();
                        row[0] = "Penambahan "+(r[1].ToString() == "p" ? "Project" : "Operasional");
                        row[1] = r[0].ToString();
                        row[2] = r[0]+" sebesar Rp." + r[2].ToString() + " pada tanggal "+ r[3].ToString();
                        tampil.Rows.Add(row);
                    }

                    table = new DataTable();
                    cmd = new MySqlCommand("SELECT nama,format(money,2) from saldo where nama !='Kas Kecil'", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    reader.Fill(table);

                    foreach (DataRow r in table.Rows)
                    {
                        DataRow row = tampil.NewRow();
                        row[0] = "Saldo Sekarang";
                        row[1] = r[0].ToString();
                        row[2] = "Rp. " + r[1].ToString();
                        tampil.Rows.Add(row);
                    }

                    dataGridViewUang.DataSource = tampil;

                    dataGridViewUang.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    for (int i = 0; i < dataGridViewUang.ColumnCount; i++)
                    {
                        dataGridViewUang.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridViewUang.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                    }
                    ClassConnection.Instance().Close();
                    awal = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void FormFinance_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            updateDGVGaji();
            resetMoney();
            tabPageTrfUang.Enter += TabPageTrfUang_Enter;
        }

        private void TabPageTrfUang_Enter(object sender, EventArgs e)
        {
            //MessageBox.Show("Sedang Dalam Masa Perbaikan","Pemberitahuan");
            //tabControl1.SelectedIndex = 0;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                updateDGVGaji();
            }
            else if(tabControl1.SelectedIndex == 1)
            {
                radioButtonProject.Checked = true;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                updateDGVUang();
                resetKodeKirim();
                resetMoney();
                labelUang.Text = "Rp. "+MoneyGiro.ToString("N");
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                updateDGVNotif();
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                updateDGVKaryawan();
            }
            else if (tabControl1.SelectedIndex == 5)
            {
                labelProjectSaldo.Text = MoneyGiro.ToString("N");
                updateComboListProject();
                groupBoxProjectPengeluaran.Visible = false;
                groupBoxProjectPemasukan.Visible = false;
                groupBoxProjectCetak.Visible = false;
            }
            else if (tabControl1.SelectedIndex == 6)
            {
                updateDGVVendor();
            }
            else if (tabControl1.SelectedIndex == 7)
            {
                updateDGVKlien();
            }
            else if (tabControl1.SelectedIndex == 8)
            {
                updateDGVProject();
            }
            else if (tabControl1.SelectedIndex == 9)
            {
                updateChart();
            }
            else if (tabControl1.SelectedIndex == 10)
            {
                updateDGVJatuhTempo();
            }
            else if(tabControl1.SelectedIndex == 11)
            {
                updateCA();
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void DataGridViewGaji_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            int[] contain = { 2, 3, 4 };
            if (contain.Contains(dataGridViewGaji.CurrentCell.ColumnIndex))
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void DataGridViewGaji_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewGaji.Columns[2].ValueType = System.Type.GetType("System.Decimal");
            dataGridViewGaji.Columns[2].DefaultCellStyle.Format = "c2";
            dataGridViewGaji.Columns[2].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
            dataGridViewGaji.Columns[3].ValueType = System.Type.GetType("System.Decimal");
            dataGridViewGaji.Columns[3].DefaultCellStyle.Format = "c2";
            dataGridViewGaji.Columns[3].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
            dataGridViewGaji.Columns[4].ValueType = System.Type.GetType("System.Decimal");
            dataGridViewGaji.Columns[4].DefaultCellStyle.Format = "c2";
            dataGridViewGaji.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
            if (!awal && e.ColumnIndex == 2)
            {
                labelEdited.Visible = true;
                buttonCetak.Text = "Submit";
                buttonSubForward.Text = "Cancel";
                awal = true;
            }
        }

        private void ButtonCetak_Click(object sender, EventArgs e)
        {
            if (buttonCetak.Text == "Cetak")
            {
                CrystalReportPenggajian cr = new CrystalReportPenggajian();
                ((FormParent)this.MdiParent).panggilTampilGaji(cr);
            }
            else
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        DataTable dt = (DataTable)dataGridViewGaji.DataSource;
                        foreach (DataRow r in dt.Rows)
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                string commandText = "UPDATE datakaryawan set gajipokok = @gaji where kode = @kode";
                                MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", r[0].ToString());
                                cmd.Parameters.AddWithValue("@gaji", Convert.ToInt32(r[2].ToString()));
                                int rowsAffected = cmd.ExecuteNonQuery();
                                ClassConnection.Instance().Close();
                            }
                        }
                        updateDGVGaji();
                        labelEdited.Visible = false;
                        buttonCetak.Text = "Cetak";
                        buttonSubForward.Text = "Forward Kepala Cabang";
                        MessageBox.Show("Data telah terupdate", "Berhasil");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
        }

        private void DataGridViewGaji_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            awal = false;
        }
        
        private void ButtonSubForward_Click(object sender, EventArgs e)
        {
            if (buttonSubForward.Text == "Cancel")
            {
                updateDGVGaji();
                labelEdited.Visible = false;
                buttonCetak.Text = "Cetak";
                buttonSubForward.Text = "Forward Kepala Cabang";
            }
            else
            {
                if (sudah == 0)
                {
                    if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                foreach (DataGridViewRow r in dataGridViewGaji.Rows)
                                {
                                    string commandText = "INSERT INTO penggajian VALUES(null,@kodepegawai,@gaji,@tunjangan,@potongan,0,concat(@bulan,'-',year(now())),null,null)";
                                    MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                    cmd.Parameters.AddWithValue("@kodepegawai", r.Cells[0].Value.ToString());
                                    cmd.Parameters.AddWithValue("@gaji", Convert.ToInt32(r.Cells[2].Value.ToString()));
                                    cmd.Parameters.AddWithValue("@tunjangan", Convert.ToInt32(r.Cells[3].Value.ToString()));
                                    cmd.Parameters.AddWithValue("@bulan", (comboBoxBulan.SelectedIndex+1).ToString());
                                    cmd.Parameters.AddWithValue("@potongan", Convert.ToInt32(r.Cells[4].Value.ToString()));
                                    int rowsAffected = cmd.ExecuteNonQuery();

                                }
                                MessageBox.Show("Gaji telah terforward ke kepala cabang", "Berhasil");
                                sudah = 1;
                                ClassConnection.Instance().Close();
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error");
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Penggajian untuk Bulan "+comboBoxBulan.Text+" pada Tahun ini telah dilakukan");
                }
                
            }
        }

        private void ButtonKirimOpr_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "INSERT INTO historysaldo VALUES(@kode,'o',@jumlah,null,null)";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxKode.Text);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        string kodeOpsMasuk = "OIPM" + DateTime.Now.ToString("ddMMyy");
                        cmd = new MySqlCommand("SELECT MAX(kode) from pettyops where kode like '" + kodeOpsMasuk + "%'", ClassConnection.Instance().Connection);
                        int dataAutoInc = 1;
                        if (cmd.ExecuteScalar().ToString() != "")
                        {
                            dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(10, 4));
                            dataAutoInc += 1;
                        }
                        kodeOpsMasuk = kodeOpsMasuk + dataAutoInc.ToString().PadLeft(4, '0');

                        commandText = "INSERT INTO pettyops VALUES(@kode,@norek,@nama,@jumlah,NOW(),@deskripsi,'D',0,2,null,null)";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", kodeOpsMasuk);
                        cmd.Parameters.AddWithValue("@norek", "ATM Giro");
                        cmd.Parameters.AddWithValue("@nama", "Giro");
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        cmd.Parameters.AddWithValue("@deskripsi", "Penambahan Uang untuk Rekening Operasional dari Giro");
                        rowsAffected = cmd.ExecuteNonQuery();

                        commandText = "UPDATE saldo set Money = Money + @jumlah where nama = 'Operasional'";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        rowsAffected = cmd.ExecuteNonQuery();

                        string kodeGiroPengeluaran = "GIPG" + DateTime.Now.ToString("ddMMyy");
                        cmd = new MySqlCommand("SELECT MAX(kode) from pettygiro where kode like '" + kodeGiroPengeluaran + "%'", ClassConnection.Instance().Connection);
                        dataAutoInc = 1;
                        if (cmd.ExecuteScalar().ToString() != "")
                        {
                            dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(10, 4));
                            dataAutoInc += 1;
                        }
                        kodeGiroPengeluaran = kodeGiroPengeluaran + dataAutoInc.ToString().PadLeft(4, '0');

                        commandText = "INSERT INTO pettygiro VALUES(@kode,@norek,@nama,@jumlah,NOW(),@deskripsi,'K',@tipe,'-','-',2,null,null)";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", kodeGiroPengeluaran);
                        cmd.Parameters.AddWithValue("@norek", "Rekening Operasional");
                        cmd.Parameters.AddWithValue("@nama", "PettyKas");
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        cmd.Parameters.AddWithValue("@deskripsi", "Transfer ke Kas Operasional");
                        cmd.Parameters.AddWithValue("@tipe", '0');
                        rowsAffected = cmd.ExecuteNonQuery();

                        commandText = "UPDATE saldo set Money = Money - @jumlah where nama = 'Giro'";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        rowsAffected = cmd.ExecuteNonQuery();

                        MessageBox.Show("Uang telah terkirim", "Berhasil");
                        ClassConnection.Instance().Close();
                        resetKodeKirim();
                        resetMoney();
                        labelUang.Text = "Rp. " + MoneyGiro.ToString("N");
                    }
                
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }

            }
        }

        private void ButtonKirimProject_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "INSERT INTO historysaldo VALUES(@kode,'p',@jumlah,null,null)";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxKode.Text);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        string kodeProjectPemasukan = "IPM" + DateTime.Now.ToString("ddMMyy");
                        cmd = new MySqlCommand("SELECT MAX(kode) from pettyproject where kode like '" + kodeProjectPemasukan + "%'", ClassConnection.Instance().Connection);
                        int dataAutoInc = 1;
                        if (cmd.ExecuteScalar().ToString() != "")
                        {
                            dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(9, 4));
                            dataAutoInc += 1;
                        }
                        kodeProjectPemasukan = kodeProjectPemasukan + dataAutoInc.ToString().PadLeft(4, '0');

                        commandText = "INSERT INTO pettyproject VALUES(@kode,@norek,@nama,@jumlah,NOW(),@deskripsi,'D','-','-','-',2,null,null)";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", kodeProjectPemasukan);
                        cmd.Parameters.AddWithValue("@norek", "ATM Giro");
                        cmd.Parameters.AddWithValue("@nama", "Giro");
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        cmd.Parameters.AddWithValue("@deskripsi", "Penambahan Uang untuk Rekening Project dari Giro");
                        rowsAffected = cmd.ExecuteNonQuery();

                        commandText = "UPDATE saldo set Money = Money + @jumlah where nama = 'Project'";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);                        
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        rowsAffected = cmd.ExecuteNonQuery();

                        string kodeGiroPengeluaran = "GIPG" + DateTime.Now.ToString("ddMMyy");
                        cmd = new MySqlCommand("SELECT MAX(kode) from pettygiro where kode like '" + kodeGiroPengeluaran + "%'", ClassConnection.Instance().Connection);
                        dataAutoInc = 1;
                        if (cmd.ExecuteScalar().ToString() != "")
                        {
                            dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(10, 4));
                            dataAutoInc += 1;
                        }
                        kodeGiroPengeluaran = kodeGiroPengeluaran + dataAutoInc.ToString().PadLeft(4, '0');

                        commandText = "INSERT INTO pettygiro VALUES(@kode,@norek,@nama,@jumlah,NOW(),@deskripsi,'K',@tipe,'-','-',2,null,null)";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", kodeGiroPengeluaran);
                        cmd.Parameters.AddWithValue("@norek", "Rekening Operasional");
                        cmd.Parameters.AddWithValue("@nama", "PettyKas");
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        cmd.Parameters.AddWithValue("@deskripsi", "Transfer ke Kas Project");
                        cmd.Parameters.AddWithValue("@tipe", '0');
                        rowsAffected = cmd.ExecuteNonQuery();

                        commandText = "UPDATE saldo set Money = Money - @jumlah where nama = 'Giro'";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        rowsAffected = cmd.ExecuteNonQuery();

                        MessageBox.Show("Uang telah terkirim", "Berhasil");
                        ClassConnection.Instance().Close();
                        resetKodeKirim();
                        resetMoney();
                        labelUang.Text = "Rp. " + MoneyGiro.ToString("N");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }

            }
        }

        private void DataGridViewNotif_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Pengeluaran Operasional")
            {
                ((FormParent)this.MdiParent).panggilAccPengeluaranOps(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString(),0);
            }
        }

        private void ComboBoxBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            cekBulan();
        }

        private void DataGridViewTampilKaryawan_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void ButtonKaryawan_Click(object sender, EventArgs e)
        {
            ((FormParent)this.MdiParent).panggilKaryawan(1);

        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            updateDGVKaryawan("SELECT Kode,Nama,notelp as 'No. Telp',Jabatan,Area from datakaryawan where nama like '%" + textBoxSearchKode.Text + "%' order by kode");
        }

        private void DataGridViewNotif_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ButtonProjectPemasukan_Click(object sender, EventArgs e)
        {
            groupBoxProjectCetak.Visible = false;
            groupBoxProjectPemasukan.Visible = true;
            groupBoxProjectPengeluaran.Visible = false;
            resetKodePemasukanProject();
        }

        private void ButtonProjectPengeluaran_Click(object sender, EventArgs e)
        {
            groupBoxProjectCetak.Visible = false;
            groupBoxProjectPemasukan.Visible = false;
            groupBoxProjectPengeluaran.Visible = true;
            resetKodePengeluaranProject();
        }

        private void ButtonProjectCetak_Click(object sender, EventArgs e)
        {
            if (comboBoxListProject.SelectedIndex != -1)
            {
                groupBoxProjectPemasukan.Visible = false;
                groupBoxProjectPengeluaran.Visible = false;
                groupBoxProjectCetak.Visible = true;
                groupBoxProjectCetakBK.Visible = false;
                groupBoxProjectCetakBM.Visible = false;
                groupBoxProjectCetakSaldo.Visible = false;
                groupBoxPembayaran.Visible = false;
            }
            else
            {
                MessageBox.Show("Silahkan Memilih terlebih dahulu project tersebut", "Wrong");
            }
        }

        private void ButtonProjectCetakSaldo_Click(object sender, EventArgs e)
        {
            groupBoxProjectCetakBK.Visible = false;
            groupBoxProjectCetakBM.Visible = false;
            groupBoxProjectCetakSaldo.Visible = true;
            groupBoxPembayaran.Visible = false;

        }

        private void ButtonProjectCetakBK_Click(object sender, EventArgs e)
        {

            try
            {
                MySqlDataAdapter adapterBK = new MySqlDataAdapter("SELECT kode FROM pettygiro WHERE kodedebitkredit = 'K' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBK = new DataSet();
                adapterBK.Fill(datasetBK);
                comboBoxProjectCetakBKKode.DataSource = datasetBK.Tables[0];
                comboBoxProjectCetakBKKode.DisplayMember = "kode";
                comboBoxProjectCetakBKKode.ValueMember = "kode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            groupBoxProjectCetakBK.Visible = true;
            groupBoxProjectCetakBM.Visible = false;
            groupBoxProjectCetakSaldo.Visible = false;

            groupBoxPembayaran.Visible = false;
        }

        private void ButtonProjectCetakBM_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterBK = new MySqlDataAdapter("SELECT kode FROM pettygiro WHERE kodedebitkredit ='D' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBK = new DataSet();
                adapterBK.Fill(datasetBK);
                comboBoxProjectCetakBMKode.DataSource = datasetBK.Tables[0];
                comboBoxProjectCetakBMKode.DisplayMember = "kode";
                comboBoxProjectCetakBMKode.ValueMember = "kode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            groupBoxProjectCetakBK.Visible = false;
            groupBoxProjectCetakBM.Visible = true;
            groupBoxProjectCetakSaldo.Visible = false;
            groupBoxPembayaran.Visible = false;
        }

        private void ButtonProjectCetakSaldoSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterSL = new MySqlDataAdapter("SELECT p.kode FROM pettygiro p WHERE p.tanggal >= '" + dateTimePickerProjectCetakMulai.Value.ToString("yyyy-MM-dd") + "' and p.tanggal <= '" + dateTimePickerProjectCetakAkhir.Value.ToString("yyyy-MM-dd") + "' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetSL = new DataSet();
                adapterSL.Fill(datasetSL);
                CrystalReportGiro cr = new CrystalReportGiro();
                string lempar = "";
                foreach (DataRow row in datasetSL.Tables[0].Rows)
                {
                    lempar += row[0].ToString() + ",";
                }
                cr.SetParameterValue("kode", lempar);
                ((FormParent)this.MdiParent).panggilTampilGiro(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonProjectCetakBKSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportGiro cr = new CrystalReportGiro();
                cr.SetParameterValue("kode", comboBoxProjectCetakBKKode.Text);
                ((FormParent)this.MdiParent).panggilTampilGiro(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonProjectCetakBMSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportGiro cr = new CrystalReportGiro();
                cr.SetParameterValue("kode", comboBoxProjectCetakBMKode.Text);
                ((FormParent)this.MdiParent).panggilTampilGiro(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonProjectPengeluaranSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "INSERT INTO pettygiro VALUES(@kode,@norek,@nama,@jumlah,@tanggal,@deskripsi,'K',@tipe,@kodevendor,@id_project,2,null,null)";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxProjectPengeluaranKode.Text);
                        cmd.Parameters.AddWithValue("@norek", textBoxProjectPengeluaranNoRek.Text);
                        cmd.Parameters.AddWithValue("@nama", textBoxProjectPengeluaranNama.Text);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownProjectPengeluaranJumlah.Value);
                        cmd.Parameters.AddWithValue("@tanggal", dateTimePickerProjectPengeluaranTanggal.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@deskripsi", richTextBoxProjectPengeluaranDeskripsi.Text);
                        cmd.Parameters.AddWithValue("@tipe", Convert.ToInt32(radioButtonVendor.Checked).ToString());
                        cmd.Parameters.AddWithValue("@kodevendor", (radioButtonVendor.Checked ? comboBoxListVendorProject.SelectedValue.ToString() : "-"));
                        cmd.Parameters.AddWithValue("@id_project", (checkBoxUntukProject.Checked ? comboBoxListProject.SelectedValue.ToString() : "-"));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data telah tersimpan", "Berhasil");

                        if (radioButtonVendor.Checked)
                        {

                            commandText = "update datavendorproject set sudahdibayar = sudahdibayar + @total where kodevendor = @kodevendor and kodeproject = @kodeproject";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@total", numericUpDownProjectPengeluaranJumlah.Value);
                            cmd.Parameters.AddWithValue("@kodevendor", comboBoxListVendorProject.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@kodeproject", comboBoxListProject.SelectedValue.ToString());

                            rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Vendor telah Terbayar sejumlah Rp. " + numericUpDownProjectPengeluaranJumlah.Value.ToString("N"), "Berhasil");

                        }
                        commandText = "UPDATE saldo SET Money = @uang WHERE Nama = @nama";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@uang", MoneyGiro - numericUpDownProjectPengeluaranJumlah.Value);
                        cmd.Parameters.AddWithValue("@nama", "Giro");
                        rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Uang telah Terupdate", "Berhasil");
                        ClassConnection.Instance().Close();
                        resetKodePengeluaranProject();
                        resetMoney();
                    }
                    buttonProjectPengeluaranClear.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void ButtonProjectPengeluaranClear_Click(object sender, EventArgs e)
        {
            textBoxProjectPengeluaranNama.Text = "";
            textBoxProjectPengeluaranNoRek.Text = "";
            numericUpDownProjectPengeluaranJumlah.Value = 0;
            richTextBoxProjectPengeluaranDeskripsi.Text = "";
            resetKodePengeluaranProject();
        }

        private void ButtonProjectPemasukanSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "INSERT INTO pettygiro VALUES(@kode,@norek,@nama,@jumlah,@tanggal,@deskripsi,'D','-','-',@project,2,null,null)";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxProjectPemasukanKode.Text);
                        cmd.Parameters.AddWithValue("@norek", textBoxProjectPemasukanNoRek.Text);
                        cmd.Parameters.AddWithValue("@nama", textBoxProjectPemasukanNama.Text);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownProjectPemasukanJumlah.Value);
                        cmd.Parameters.AddWithValue("@tanggal", dateTimePickerProjectPemasukanTanggal.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@deskripsi", richTextBoxProjectPemasukanDeskripsi.Text);
                        cmd.Parameters.AddWithValue("@project", checkBoxBayar.Checked ? comboBoxListProject.SelectedValue.ToString() : "-");
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data telah Tersimpan", "Berhasil");

                        if (checkBoxBayar.Checked)
                        {
                            commandText = "UPDATE dataproject SET kurang = kurang+@uang WHERE kode = @kode";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@uang", numericUpDownProjectPemasukanJumlah.Value);
                            cmd.Parameters.AddWithValue("@kode", comboBoxListProject.SelectedValue.ToString());
                            rowsAffected = cmd.ExecuteNonQuery();

                            if (numericUpDownProjectPemasukanJumlah.Value == numericUpDownProjectPemasukanJumlah.Maximum)
                            {
                                commandText = "UPDATE dataproject SET status = 1 WHERE kode = @kode";
                                cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", comboBoxListProject.SelectedValue.ToString());
                                rowsAffected = cmd.ExecuteNonQuery();
                            }
                        }

                        commandText = "UPDATE saldo SET Money = @uang WHERE Nama = 'Giro'";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@uang", MoneyGiro + numericUpDownProjectPemasukanJumlah.Value);
                        rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Uang telah Terupdate", "Berhasil");
                        ClassConnection.Instance().Close();
                        resetKodePemasukanProject();
                        resetMoney();
                    }
                    buttonProjectPengeluaranClear.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void ButtonProjectPemasukanClear_Click(object sender, EventArgs e)
        {

            textBoxProjectPemasukanNama.Text = "";
            textBoxProjectPemasukanNoRek.Text = "";
            numericUpDownProjectPemasukanJumlah.Value = 0;
            richTextBoxProjectPemasukanDeskripsi.Text = "";
            resetKodePemasukanProject();
        }

        private void RadioButtonVendor_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVendor.Checked)
            {
                if (comboBoxListProject.SelectedIndex == -1)
                {
                    MessageBox.Show("Silahkan Memilih Project terlebih dahulu","Gagal");
                    radioButtonVendor.Checked = false;
                }
                else
                {
                    comboBoxListVendorProject.Enabled = radioButtonVendor.Checked;
                    updateComboVendor();
                }
            }
        }

        private void RadioButtonNrml_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNrml.Checked)
            {
                numericUpDownProjectPengeluaranJumlah.Maximum = long.MaxValue;
                comboBoxListVendorProject.Enabled = radioButtonVendor.Checked;
                comboBoxListVendorProject.SelectedIndex = -1;
            }
        }

        private void ComboBoxListVendorProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxListVendorProject.SelectedIndex != -1)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        MySqlCommand cmd = new MySqlCommand("SELECT total-sudahdibayar from datavendorproject where kodevendor = '" + comboBoxListVendorProject.SelectedValue.ToString() + "' and kodeproject = '" + comboBoxListProject.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
                        numericUpDownProjectPengeluaranJumlah.Maximum = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                        numericUpDownProjectPengeluaranJumlah.Value = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                        MessageBox.Show("Vendor " + comboBoxListVendorProject.Text + " harus dibayar maksimal Rp. " + numericUpDownProjectPengeluaranJumlah.Maximum.ToString("N"));
                        ClassConnection.Instance().Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void ButtonPembayaran_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterBK = new MySqlDataAdapter("SELECT kode FROM pettygiro WHERE kodedebitkredit ='D' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBK = new DataSet();
                adapterBK.Fill(datasetBK);
                comboBoxPembayaran.DataSource = datasetBK.Tables[0];
                comboBoxPembayaran.DisplayMember = "kode";
                comboBoxPembayaran.ValueMember = "kode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            groupBoxProjectCetakBK.Visible = false;
            groupBoxProjectCetakBM.Visible = false;
            groupBoxProjectCetakSaldo.Visible = false;
            groupBoxPembayaran.Visible = true;
        }

        private void ButtonCetakPembayaran_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportBuktiBayar cr = new CrystalReportBuktiBayar();
                cr.SetParameterValue("kode", comboBoxPembayaran.Text);
                ((FormParent)this.MdiParent).panggilTampilBayar(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ButtonPrintVendor_Click(object sender, EventArgs e)
        {
            CrystalReportPrintVendor cr = new CrystalReportPrintVendor();
            ((FormParent)this.MdiParent).panggilTampilVendor(cr);

        }

        private void RadioButtonProject_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonProject.Checked)
            {
                comboBoxTampil.DataSource = null;
                try
                {
                    MySqlDataAdapter adapterLP = new MySqlDataAdapter("SELECT kode,nama FROM dataproject where status = 1 order by kode", ClassConnection.Instance().Connection);
                    DataSet datasetLP = new DataSet();
                    adapterLP.Fill(datasetLP);
                    comboBoxTampil.DisplayMember = "nama";
                    comboBoxTampil.ValueMember = "kode";
                    comboBoxTampil.DataSource = datasetLP.Tables[0];
                    checkBoxKlienSendiri.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CheckBoxBayar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBayar.Checked)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        MySqlCommand cmd = new MySqlCommand("SELECT totalbiaya-kurang from dataproject where kode = '" + comboBoxListProject.SelectedValue.ToString() + "'",ClassConnection.Instance().Connection);
                        numericUpDownProjectPemasukanJumlah.Maximum = Convert.ToDecimal(cmd.ExecuteScalar().ToString());
                        numericUpDownProjectPemasukanJumlah.Value = numericUpDownProjectPemasukanJumlah.Maximum;


                        MessageBox.Show("Project ini memiliki hutang yang belum dibayar sebesar Rp. "+numericUpDownProjectPemasukanJumlah.Maximum.ToString("N"),"Pemberitahuan");
                        ClassConnection.Instance().Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTahunan.Checked)
            {
                MessageBox.Show("Mohon menunggu hingga accounting dapat berjalan","Pemberitahuan");
                radioButtonProject.Checked = true;
                //comboBoxTampil.DataSource = null;
                //checkBoxKlienSendiri.Visible = false;

                //comboBoxTampil.DataSource = Enumerable.Range(2000, DateTime.Today.Year - 1999).ToList();
            }

        }
        long hasilLabaProject, OmsetProject, rugiProject;
        long hasilLabaBersih, OmsetBersih, rugiBersih;

        private void Label20_Click(object sender, EventArgs e)
        {

        }

        private void NumericUpDownPersen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownTotal.Value = Convert.ToDecimal(hasilLabaBersih * Convert.ToInt32(numericUpDownPersen.Value) / 100);
        }

        private void TabPageBonus_Click(object sender, EventArgs e)
        {

        }

        private void DataGridViewTampilBonus_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex != dataGridViewTampilBonus.Rows.Count-1)
            {
                DataTable tb = (dataGridViewTampilBonus.DataSource as DataTable);
                if (checkBoxKlienSendiri.Checked)
                {
                    if (e.RowIndex == 0)
                    {
                        tb.Rows[0][3] = Convert.ToInt64((OmsetProject * Convert.ToInt32(tb.Rows[0][2].ToString())) / 100);

                    }
                    else
                    {
                        tb.Rows[e.RowIndex][3] = Convert.ToInt64((hasilLabaProject * Convert.ToInt32(tb.Rows[e.RowIndex][2].ToString())) / 100);

                    }
                }
                else
                {
                    tb.Rows[e.RowIndex][3] = Convert.ToInt64((hasilLabaProject * Convert.ToInt32(tb.Rows[e.RowIndex][2].ToString())) / 100);
                }
                long totalBonus = 0;

                for (int i =0; i < tb.Rows.Count-1; i++)
                {
                    totalBonus += Convert.ToInt64(tb.Rows[i][3].ToString());
                }
                tb.Rows[tb.Rows.Count - 1][3] = totalBonus;
                dataGridViewTampilBonus.Invalidate();
            }
        }

        private void DataGridViewTampilBonus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridViewTampilBonus_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridViewTampilBonus.CurrentCell.ColumnIndex == 2)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }

        private void ButtonSearchProject_Click(object sender, EventArgs e)
        {
            updateDGVProject("SELECT p.Kode,k.nama as 'Nama Klien' , p.nama as 'Nama Project', date_format(p.tagihan,'%d-%m-%Y') as 'Tagihan',date_format(p.deadline,'%d-%m-%Y') as 'Deadline', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total', CONCAT('Rp.',FORMAT(p.totalbiaya-p.kurang,2)) as 'Kurang Biaya',IF(p.status = 0, 'Belum Selesai', 'Sudah Selesai') AS 'Status Project' FROM dataproject p, dataklien k where p.kode_klien = k.kode and lower(p.nama) like '%" + textBoxSearchProject.Text.ToLower() + "%' order by kode");
        }

        private void DataGridViewTampilKaryawan_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
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
                            updateDGVKaryawan();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
        }

        private void ChartKlien_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult hit = chartKlien.HitTest(e.X, e.Y, ChartElementType.DataPoint);

            if (hit.PointIndex >= 0 && hit.Series != null)
            {
                DataPoint dp = chartKlien.Series[0].Points[hit.PointIndex];
                MessageBox.Show(dp.XValue.ToString()+" Hari");
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

        }

        private void TextBoxSearchJatuhTempo_TextChanged(object sender, EventArgs e)
        {

        }

        private void RadioButtonKirimSby_CheckedChanged(object sender, EventArgs e)
        {
            buttonKirimJKT.Visible = !radioButtonKirimSby.Checked;
            resetKodeKirim();
        }

        private void ButtonKirimJKT_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "INSERT INTO historysaldo VALUES(@kode,'o',@jumlah,null,null)";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxKode.Text);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        string kodeOpsMasuk = "JOIPM" + DateTime.Now.ToString("ddMMyy");
                        cmd = new MySqlCommand("SELECT MAX(kode) from pettyops where kode like '" + kodeOpsMasuk + "%'", ClassConnection.Instance().Connection);
                        int dataAutoInc = 1;
                        if (cmd.ExecuteScalar().ToString() != "")
                        {
                            dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(11, 4));
                            dataAutoInc += 1;
                        }
                        kodeOpsMasuk = kodeOpsMasuk + dataAutoInc.ToString().PadLeft(4, '0');

                        commandText = "INSERT INTO pettyops VALUES(@kode,@norek,@nama,@jumlah,NOW(),@deskripsi,'D',0,2,null,null)";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", kodeOpsMasuk);
                        cmd.Parameters.AddWithValue("@norek", "ATM Giro");
                        cmd.Parameters.AddWithValue("@nama", "Giro");
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        cmd.Parameters.AddWithValue("@deskripsi", "Penambahan Uang untuk Rekening Operasional dari Giro");
                        rowsAffected = cmd.ExecuteNonQuery();

                        commandText = "UPDATE saldo set Money = Money + @jumlah where nama = 'Jkt Operasional'";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        rowsAffected = cmd.ExecuteNonQuery();

                        string kodeGiroPengeluaran = "GIPG" + DateTime.Now.ToString("ddMMyy");
                        cmd = new MySqlCommand("SELECT MAX(kode) from pettygiro where kode like '" + kodeGiroPengeluaran + "%'", ClassConnection.Instance().Connection);
                        dataAutoInc = 1;
                        if (cmd.ExecuteScalar().ToString() != "")
                        {
                            dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(10, 4));
                            dataAutoInc += 1;
                        }
                        kodeGiroPengeluaran = kodeGiroPengeluaran + dataAutoInc.ToString().PadLeft(4, '0');

                        commandText = "INSERT INTO pettygiro VALUES(@kode,@norek,@nama,@jumlah,NOW(),@deskripsi,'K',@tipe,'-','-',2,null,null)";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", kodeGiroPengeluaran);
                        cmd.Parameters.AddWithValue("@norek", "Rekening Operasional");
                        cmd.Parameters.AddWithValue("@nama", "PettyKas");
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        cmd.Parameters.AddWithValue("@deskripsi", "Transfer ke Kas Operasional");
                        cmd.Parameters.AddWithValue("@tipe", '0');
                        rowsAffected = cmd.ExecuteNonQuery();

                        commandText = "UPDATE saldo set Money = Money - @jumlah where nama = 'Giro'";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownJumlah.Value);
                        rowsAffected = cmd.ExecuteNonQuery();

                        MessageBox.Show("Uang telah terkirim", "Berhasil");
                        ClassConnection.Instance().Close();
                        resetKodeKirim();
                        resetMoney();
                        labelUang.Text = "Rp. " + MoneyGiro.ToString("N");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }

            }
        }

        private void tabCA_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(datagridCA.Rows[datagridCA.CurrentCell.RowIndex].Cells[0].Value.ToString());
        }

        private void CheckBoxKlienSendiri_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKlienSendiri.Checked)
            {
                dataGridViewTampilBonus.DataSource = null;
                MySqlDataAdapter adapterBonusProject = new MySqlDataAdapter("SELECT K.kode as 'Kode Karyawan',K.nama AS 'Nama Karyawan', '2' AS 'Bonus (%)', 0 AS 'Subtotal' FROM dataproject P, datakaryawan K WHERE P.kode_ae = K.kode AND P.kode = '" + comboBoxTampil.SelectedValue.ToString() + "' UNION SELECT K.kode as 'Kode Karyawan',K.nama AS 'Nama Karyawan', '2' AS 'Bonus (%)', 0 AS 'Subtotal' FROM dataproject P, datakaryawan K WHERE P.kode_pot = K.kode AND P.kode = '" + comboBoxTampil.SelectedValue.ToString() + "' UNION SELECT K.kode as 'Kode Karyawan',K.nama AS 'Nama Karyawan', '2' AS 'Bonus (%)', 0 AS 'Subtotal' FROM dataproject P, datakaryawan K WHERE P.kode_pont = K.kode AND P.kode = '" + comboBoxTampil.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
                DataTable table = new DataTable();
                adapterBonusProject.Fill(table);
                long totalBonus = 0;
                if (table.Rows.Count == 2)
                {
                    table.Rows[table.Rows.Count - 1][2] = '4';
                }
                table.Rows[0][3] = Convert.ToInt64((OmsetProject * Convert.ToInt32(table.Rows[0][2].ToString())) / 100);
                totalBonus += Convert.ToInt64((OmsetProject * Convert.ToInt32(table.Rows[0][2].ToString())) / 100);

                for (int i = 1; i < table.Rows.Count; i++)
                {
                    table.Rows[i][3] = Convert.ToInt64((hasilLabaProject * Convert.ToInt32(table.Rows[i][2].ToString())) / 100);
                    totalBonus += Convert.ToInt64((hasilLabaProject * Convert.ToInt32(table.Rows[i][2].ToString())) / 100);
                }
                DataRow akhir = table.NewRow();
                akhir[0] = "Total";
                akhir[1] = "-";
                akhir[2] = "-";
                akhir[3] = totalBonus;
                table.Rows.Add(akhir);
                dataGridViewTampilBonus.DataSource = table;
                dataGridViewTampilBonus.Columns[3].ValueType = System.Type.GetType("System.Decimal");
                dataGridViewTampilBonus.Columns[3].DefaultCellStyle.Format = "c2";
                dataGridViewTampilBonus.Columns[3].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
                dataGridViewTampilBonus.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewTampilBonus.ColumnCount; i++)
                {
                    if (i!=2) dataGridViewTampilBonus.Columns[i].ReadOnly = true;
                    dataGridViewTampilBonus.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewTampilBonus.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                }
                dataGridViewTampilBonus.Rows[dataGridViewTampilBonus.RowCount - 1].Cells[2].ReadOnly = true;

            }
            else
            {
                updateDGVBonusProject();
            }
        }

        private void labelProjectSaldo_Click(object sender, EventArgs e)
        {

        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void label122_Click(object sender, EventArgs e)
        {

        }

        private void label74_Click(object sender, EventArgs e)
        {

        }

        private void ButtonForward_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (radioButtonProject.Checked)
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            foreach (DataGridViewRow r in dataGridViewTampilBonus.Rows)
                            {
                                if (r.Cells[0].Value.ToString() != "Total")
                                {
                                    MySqlCommand cmd = new MySqlCommand("INSERT INTO bonusproject VALUES(null,@kodeproject,@kodepegawai,@bonus,@lababersih,@persen,@omset,@klien,0,NOW(),null,null)", ClassConnection.Instance().Connection);
                                    cmd.Parameters.AddWithValue("@kodeproject", comboBoxTampil.SelectedValue.ToString());
                                    cmd.Parameters.AddWithValue("@kodepegawai", r.Cells[0].Value.ToString());
                                    cmd.Parameters.AddWithValue("@lababersih", hasilLabaProject);
                                    cmd.Parameters.AddWithValue("@bonus", Convert.ToInt32(r.Cells[3].Value));
                                    cmd.Parameters.AddWithValue("@persen", Convert.ToInt32(r.Cells[2].Value));
                                    cmd.Parameters.AddWithValue("@omset", OmsetProject);
                                    cmd.Parameters.AddWithValue("@klien", checkBoxKlienSendiri.Checked ? 1 : 0);
                                    int rowaffect = cmd.ExecuteNonQuery();
                                }
                                
                            }
                            MySqlCommand cmd2 = new MySqlCommand("UPDATE dataproject set status = 2 where kodeproject = '"+comboBoxTampil.SelectedValue.ToString()+"'", ClassConnection.Instance().Connection);
                            int rowaffect2 = cmd2.ExecuteNonQuery();
                            MessageBox.Show("Data Berhasil diforward", "Berhasil");
                            ClassConnection.Instance().Close();
                        }
                    }
                    else if (radioButtonTahunan.Checked)
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            MySqlCommand cmd = new MySqlCommand("INSERT INTO notifceotahunan values(null,@laba,@persen,@subtotal,@tahun,0,null,null)", ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@laba", hasilLabaBersih);
                            cmd.Parameters.AddWithValue("@persen", numericUpDownPersen.Value);
                            cmd.Parameters.AddWithValue("@subtotal", numericUpDownTotal.Value);
                            cmd.Parameters.AddWithValue("@tahun", comboBoxTampil.Text);
                            int rowaffect = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data Berhasil diforward","Berhasil");
                            ClassConnection.Instance().Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        public void updateDGVBonusProject()
        {
            MySqlDataAdapter adapterBonusProject = new MySqlDataAdapter("select k.kode as 'Kode Karyawan',k.nama as 'Nama Karyawan', '2' as 'Bonus (%)', 0 as 'Subtotal' from dataproject p, datakaryawan k where p.kode_ae = k.kode and p.kode = '" + comboBoxTampil.SelectedValue.ToString() + "' union select k.kode as 'Kode Karyawan',k.nama as 'Nama Karyawan', '2' as 'Bonus (%)', 0 as 'Subtotal' from dataproject p, datakaryawan k where p.kode_pot = k.kode and p.kode = '" + comboBoxTampil.SelectedValue.ToString() + "' union select k.kode as 'Kode Karyawan',k.nama as 'Nama Karyawan', '2' as 'Bonus (%)', 0 as 'Subtotal' from dataproject p, datakaryawan k where p.kode_pont = k.kode and p.kode = '" + comboBoxTampil.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
            DataTable table = new DataTable();
            adapterBonusProject.Fill(table);
            long totalBonus = 0;
            if (table.Rows.Count == 2)
            {
                table.Rows[table.Rows.Count - 1][2] = '4';
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i][3] = Convert.ToInt64((hasilLabaProject * Convert.ToInt32(table.Rows[i][2].ToString())) / 100);
                totalBonus += Convert.ToInt64((hasilLabaProject * Convert.ToInt32(table.Rows[i][2].ToString())) / 100);
            }
            DataRow akhir = table.NewRow();
            akhir[0] = "Total";
            akhir[1] = "-";
            akhir[2] = "-";
            akhir[3] = totalBonus;
            table.Rows.Add(akhir);
            dataGridViewTampilBonus.DataSource = table;
            dataGridViewTampilBonus.Columns[3].ValueType = System.Type.GetType("System.Decimal");
            dataGridViewTampilBonus.Columns[3].DefaultCellStyle.Format = "c2";
            dataGridViewTampilBonus.Columns[3].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
            dataGridViewTampilBonus.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for (int i = 0; i < dataGridViewTampilBonus.ColumnCount; i++)
            {
                if (i != 2) dataGridViewTampilBonus.Columns[i].ReadOnly = true;
                dataGridViewTampilBonus.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewTampilBonus.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            dataGridViewTampilBonus.Rows[dataGridViewTampilBonus.RowCount - 1].Cells[2].ReadOnly = true;
        }

        public void updateCA()
        {
            MySqlDataAdapter adapterBonusProject = new MySqlDataAdapter("SELECT CONCAT('Karyawan ',k.nama,' mengajukan cash advance sejumlah ',ca.total, ' pada tanggal ',ca.datestart,' untuk project ',p.nama) as CA FROM pettyca ca,datakaryawan k,dataproject p WHERE ca.kodekaryawan = k.kode AND p.kode = ca.caproject", ClassConnection.Instance().Connection);
            DataTable table = new DataTable();
            adapterBonusProject.Fill(table);
            datagridCA.DataSource = table;
            datagridCA.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for (int i = 0; i < datagridCA.ColumnCount; i++)
            {
                datagridCA.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                datagridCA.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

        }


        private void ComboBoxTampil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTampil.DataSource != null)
            {

                if (radioButtonProject.Checked)
                {
                    labelOmset.Visible = false;
                    dataGridViewTampilBonus.Visible = true;
                    groupBoxBonus.Visible = false;
                    dataGridViewTampilBonus.DataSource = null;
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            MessageBox.Show(comboBoxTampil.SelectedValue.ToString());
                            MySqlCommand cmdRugi = new MySqlCommand("select sum(hasil.jml) from (select sum(k.jumlah) as jml from pettyca ca, pettykas k where k.norek = ca.kode and ca.caproject = '" + comboBoxTampil.SelectedValue.ToString() + "' UNION select sum(vd.total) as jml from datavendorproject vd where vd.kodeproject = '" + comboBoxTampil.SelectedValue.ToString() + "') hasil", ClassConnection.Instance().Connection);
                            MySqlCommand cmdLaba = new MySqlCommand("select totalbiaya from dataproject where kode='" + comboBoxTampil.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
                            OmsetProject = Convert.ToInt64(cmdLaba.ExecuteScalar().ToString());
                            rugiProject = Convert.ToInt64(cmdRugi.ExecuteScalar().ToString());
                            hasilLabaProject =  OmsetProject - rugiProject;
                            labelLaba.Text = "Laba Project '" + comboBoxTampil.Text + "' : Rp. " + hasilLabaProject.ToString("N");
                            labelOmset.Text = "Omset Project: Rp. " + OmsetProject.ToString("N");
                            updateDGVBonusProject();
                            ClassConnection.Instance().Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Laba");
                    }
                }
                else if (radioButtonTahunan.Checked)
                {
                    dataGridViewTampilBonus.DataSource = null;
                    dataGridViewTampilBonus.Visible = false;
                    groupBoxBonus.Visible = true;
                    labelOmset.Visible = false;
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            MySqlCommand cmdRugi = new MySqlCommand("select sum(hasil.jml) from (select sum(jumlah) as jml from pettykas pk where kodedebitkredit = 'k' and year(insertdata) = '" + comboBoxTampil.Text + "' union select sum(jumlah) as jml from pettyops po where kodedebitkredit = 'k' and year(insertdata) = '" + comboBoxTampil.Text + "' and status = 2 union select sum(jumlah) as jml from pettyproject pj where kodedebitkredit = 'k' and year(insertdata) = '" + comboBoxTampil.Text + "' and status = 2 union select sum(jumlah) as jml from pettygiro pg where kodedebitkredit = 'k' and year(insertdata) = '" + comboBoxTampil.Text + "' and status = 2) hasil", ClassConnection.Instance().Connection);
                            MySqlCommand cmdLaba = new MySqlCommand("select sum(totalbiaya) from dataproject where year(tagihan) = '" + comboBoxTampil.Text + "' and status = 1", ClassConnection.Instance().Connection);
                            string tempOmset = cmdLaba.ExecuteScalar().ToString();
                            string tempRugi = cmdRugi.ExecuteScalar().ToString();
                            if (tempOmset == "" && tempRugi == "")
                            {
                                buttonForward.Enabled = false;
                                numericUpDownPersen.Enabled = false;
                                labelLaba.Text = "Laba Bersih " + comboBoxTampil.Text + " : Rp. - ";

                            }
                            else
                            {

                                numericUpDownPersen.Enabled = true;
                                buttonForward.Enabled = true;
                                OmsetBersih = Convert.ToInt64(tempOmset);
                                rugiBersih = Convert.ToInt64(tempRugi);
                                hasilLabaBersih = OmsetBersih - rugiBersih;
                                labelLaba.Text = "Laba Bersih " + comboBoxTampil.Text + " : Rp. " + hasilLabaBersih.ToString("N");

                            }
                            ClassConnection.Instance().Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Laba");
                    }
                }
            }
        }
    }
}
