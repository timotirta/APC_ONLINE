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
using System.Windows.Forms.DataVisualization.Charting;
namespace APC_EO_Finance
{
    public partial class FormAR : Form
    {
        public FormAR()
        {
            InitializeComponent();
        }
        DataTable tableChart;
        public bool jakartatidak = true;
        public void updateChart()
        {
            chartKlien.Series.Clear();
            int[] data = { 0,0,0,0,0 };
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
            series.Font = new Font(FontFamily.GenericSansSerif, 30,FontStyle.Bold);
            series.ChartType = SeriesChartType.Pie;
            series.Label = "#PERCENT";
            // Frist parameter is X-Axis and Second is Collection of Y- Axis
            series.Points.DataBindXY(new[] { 0,30,60,90,120 }, new[] { data[0],data[1],data[2],data[3],data[4] });

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
        bool awal = true;
        DataSet datasetKlien;
        public void updateNotif()
        {
            try
            {
                dataGridViewNotifikasiAR.DataSource = null;
                MySqlDataAdapter adapterKlien;
                DataTable tmp;
                DataTable tampil = new DataTable();

                tampil.Columns.Add("Tipe");
                tampil.Columns.Add("Kode");
                tampil.Columns.Add("Notifikasi");
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

                        try
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                MySqlCommand command = new MySqlCommand("SELECT count(id) from piutangklien where kodeproject = @kodepr and kodeklien = @kodeklien", ClassConnection.Instance().Connection);
                                command.Parameters.AddWithValue("@kodepr", r[0].ToString());
                                command.Parameters.AddWithValue("@kodeklien", r[5].ToString());
                                int dataAutoInc = 0;
                                if (command.ExecuteScalar().ToString() != "")
                                {
                                    dataAutoInc = Convert.ToInt32(command.ExecuteScalar().ToString());
                                }
                                MySqlCommand command2;
                                if (dataAutoInc > 0)
                                {
                                    command2 = new MySqlCommand("update piutangklien set lamahari = @datebaru where kodeproject = @kodepr and kodeklien = @kodeklien", ClassConnection.Instance().Connection);
                                    command2.Parameters.AddWithValue("@datebaru", DateTime.Now.ToString("yyyy-MM-dd"));
                                    command2.Parameters.AddWithValue("@kodepr", r[0].ToString());
                                    command2.Parameters.AddWithValue("@kodeklien", r[5].ToString());
                                }
                                else
                                {
                                    command2 = new MySqlCommand("insert into piutangklien values(null,@kodepr,@kodeklien,@datebaru,@subtotal,null,null)", ClassConnection.Instance().Connection);
                                    command2.Parameters.AddWithValue("@datebaru", DateTime.Now.ToString("yyyy-MM-dd"));
                                    command2.Parameters.AddWithValue("@kodepr", r[0].ToString());
                                    command2.Parameters.AddWithValue("@kodeklien", r[5].ToString());
                                    command2.Parameters.AddWithValue("@subtotal", Convert.ToInt32(r[6].ToString()));
                                }
                                int res = command2.ExecuteNonQuery();
                                if (dataAutoInc == 0)
                                {
                                    command2 = new MySqlCommand("update dataklien set bataspiutang = bataspiutang + @utang where kode = @kodeklien", ClassConnection.Instance().Connection);
                                    command2.Parameters.AddWithValue("@utang", Convert.ToInt32(r[6].ToString()));
                                    command2.Parameters.AddWithValue("@kodeklien", r[5].ToString());
                                }
                                res = command2.ExecuteNonQuery();
                                ClassConnection.Instance().Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error");
                        }
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
                    adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.tagihan,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.kode like 'JPRJ%' and p.status = 0 and p.kode_klien = k.kode and datediff(now(),p.deadline) < -3 order by p.kode", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.tagihan,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.kode like 'PRJ%' and p.status = 0 and p.kode_klien = k.kode and datediff(now(),p.deadline) < -3 order by p.kode", ClassConnection.Instance().Connection);
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

                //foreach (DataRow r in table.Rows)
                //{
                //    DataRow NewRow = tampil.NewRow();
                //    DateTime tanggalTagih = new DateTime(Convert.ToInt32((r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString())), Convert.ToInt32((r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString())), Convert.ToInt32((r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString())));

                //    int daysDiff = ((TimeSpan)(tanggalTagih - DateTime.Today)).Days;
                //    if (daysDiff < 0)
                //    {
                //        NewRow[0] = "Peringatan";
                //        NewRow[1] = r[0].ToString();
                //        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " sudah melebihi masa jatuh tempo";
                //        try
                //        {
                //            if (ClassConnection.Instance().Connecting())
                //            {
                //                MySqlCommand command = new MySqlCommand("SELECT count(id) from piutangklien where kodeproject = @kodepr and kodeklien = @kodeklien", ClassConnection.Instance().Connection);
                //                command.Parameters.AddWithValue("@kodepr", r[0].ToString());
                //                command.Parameters.AddWithValue("@kodeklien", r[5].ToString());
                //                int dataAutoInc = 0;
                //                if (command.ExecuteScalar().ToString() != "")
                //                {
                //                    dataAutoInc = Convert.ToInt32(command.ExecuteScalar().ToString());
                //                }
                //                MySqlCommand command2;
                //                if (dataAutoInc > 0)
                //                {
                //                    command2 = new MySqlCommand("update piutangklien set lamahari = @datebaru where kodeproject = @kodepr and kodeklien = @kodeklien", ClassConnection.Instance().Connection);
                //                    command2.Parameters.AddWithValue("@datebaru", DateTime.Now.ToString("yyyy-MM-dd"));
                //                    command2.Parameters.AddWithValue("@kodepr", r[0].ToString());
                //                    command2.Parameters.AddWithValue("@kodeklien", r[5].ToString());
                //                }
                //                else
                //                {
                //                    command2 = new MySqlCommand("insert into piutangklien values(null,@kodepr,@kodeklien,@datebaru,@subtotal,null,null)", ClassConnection.Instance().Connection);
                //                    command2.Parameters.AddWithValue("@datebaru", DateTime.Now.ToString("yyyy-MM-dd"));
                //                    command2.Parameters.AddWithValue("@kodepr", r[0].ToString());
                //                    command2.Parameters.AddWithValue("@kodeklien", r[5].ToString());
                //                    command2.Parameters.AddWithValue("@subtotal", Convert.ToInt32(r[6].ToString()));
                //                }
                //                int res = command2.ExecuteNonQuery();
                //                if (dataAutoInc == 0)
                //                {
                //                    command2 = new MySqlCommand("update dataklien set bataspiutang = bataspiutang + @utang where kode = @kodeklien", ClassConnection.Instance().Connection);
                //                    command2.Parameters.AddWithValue("@utang", Convert.ToInt32(r[6].ToString()));
                //                    command2.Parameters.AddWithValue("@kodeklien", r[5].ToString());
                //                }
                //                res = command2.ExecuteNonQuery();
                //                ClassConnection.Instance().Close();
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show(ex.Message, "Error");
                //        }
                //    }
                //    else if (daysDiff <= 3)
                //    {
                //        NewRow[0] = "Pemberitahuan";
                //        NewRow[1] = r[0].ToString();
                //        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " telah memasuki masa tenggang yang akan memiliki jatuh tempo " + r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString() + "-" + r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString() + "-" + r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString();
                //    }
                //    else 
                //    {
                //        NewRow[0] = "Project Berjalan";
                //        NewRow[1] = r[0].ToString();
                //        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " memiliki jatuh tempo " + r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString() + "-" + r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString() + "-" + r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString();
                //    }
                //    tampil.Rows.Add(NewRow);
                //}
                dataGridViewNotifikasiAR.DataSource = tampil;

                dataGridViewNotifikasiAR.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewNotifikasiAR.ColumnCount; i++)
                {
                    dataGridViewNotifikasiAR.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewNotifikasiAR.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }
                foreach (DataGridViewRow r in dataGridViewNotifikasiAR.Rows)
                {
                    if (r.Cells[0].Value.ToString() == "Pemberitahuan")
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
                MessageBox.Show(ex.Message, "Error");
            }
            
        }
        public void updateDGVKlien(string cmd = "SELECT Kode, Nama, Principal, User, Notelp,CONCAT('Rp.',FORMAT(bataspiutang,2)) as 'Piutang' FROM dataklien order by kode")
        {
            try
            {
                datasetKlien = new DataSet();
                dataGridViewTampilKlien.DataSource = null;
                dataGridViewTampilKlien.Columns.Clear();
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);
                
                adapterKlien.Fill(datasetKlien);
                dataGridViewTampilKlien.DataSource = datasetKlien.Tables[0];
                DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
                edit.HeaderText = "Edit";
                edit.Text = "Edit";
                edit.Name = "Edit";
                edit.UseColumnTextForButtonValue = true;
                dataGridViewTampilKlien.Columns.Add(edit);

                DataGridViewButtonColumn view = new DataGridViewButtonColumn();
                view.HeaderText = "View";
                view.Text = "View";
                view.Name = "View";
                view.UseColumnTextForButtonValue = true;
                dataGridViewTampilKlien.Columns.Add(view);

                DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
                delete.HeaderText = "Delete";
                delete.Text = "Delete";
                delete.Name = "Delete";
                delete.UseColumnTextForButtonValue = true;
                dataGridViewTampilKlien.Columns.Add(delete);

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
        public void updateDGVProject(string cmd = "SELECT p.Kode,k.nama as 'Nama Klien' , p.nama as 'Nama Project', date_format(p.tagihan,'%d-%m-%Y') as 'Tagihan',date_format(p.deadline,'%d-%m-%Y') as 'Deadline', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total', CONCAT('Rp.',FORMAT(p.totalbiaya-p.kurang,2)) as 'Kurang Biaya',IF(p.status = 0, 'Belum Selesai', 'Sudah Selesai') AS 'Status Project' FROM dataproject p, dataklien k where p.kode like 'PR%' and p.kode_klien = k.kode order by kode")
        {
            try
            {
                datasetKlien = new DataSet();
                dataGridViewProject.DataSource = null;
                dataGridViewProject.Columns.Clear();
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);

                adapterKlien.Fill(datasetKlien);
                dataGridViewProject.DataSource = datasetKlien.Tables[0];
                DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
                edit.HeaderText = "Edit";
                edit.Text = "Edit";
                edit.Name = "Edit";
                edit.UseColumnTextForButtonValue = true;
                dataGridViewProject.Columns.Add(edit);

                DataGridViewButtonColumn view = new DataGridViewButtonColumn();
                view.HeaderText = "View";
                view.Text = "View";
                view.Name = "View";
                view.UseColumnTextForButtonValue = true;
                dataGridViewProject.Columns.Add(view);

                DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
                delete.HeaderText = "Delete";
                delete.Text = "Delete";
                delete.Name = "Delete";
                delete.UseColumnTextForButtonValue = true;
                dataGridViewProject.Columns.Add(delete);

                dataGridViewProject.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewProject.ColumnCount; i++)
                {
                    dataGridViewProject.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                updateNotif();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormAR_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            updateDGVKlien();
        }

        private void TabPageKlien_Click(object sender, EventArgs e)
        {
        }

        private void ButtonKlien_Click(object sender, EventArgs e)
        {
            ((FormParent)this.MdiParent).panggilKlien(1);
        }

        private void DataGridViewTampilKlien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (awal)
            {
                if (dataGridViewTampilKlien.Columns[e.ColumnIndex].Name == "Edit")
                {
                    ((FormParent)this.MdiParent).panggilKlien(2, datasetKlien.Tables[0].Rows[e.RowIndex][0].ToString());
                }
                else if (dataGridViewTampilKlien.Columns[e.ColumnIndex].Name == "View")
                {
                    ((FormParent)this.MdiParent).panggilKlien(3, datasetKlien.Tables[0].Rows[e.RowIndex][0].ToString());
                }
                else if (dataGridViewTampilKlien.Columns[e.ColumnIndex].Name == "Delete")
                {
                    if (MessageBox.Show("Apakah anda yakin ingin mendelete " + datasetKlien.Tables[0].Rows[e.RowIndex][1].ToString() + " ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                string commandText = "DELETE FROM dataklien WHERE kode = @kode";
                                MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", datasetKlien.Tables[0].Rows[e.RowIndex][0].ToString());
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
                if (dataGridViewTampilKlien.Columns[e.ColumnIndex].Name == "Edit")
                {
                    ((FormParent)this.MdiParent).panggilKlien(2, dataGridViewTampilKlien.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                else if (dataGridViewTampilKlien.Columns[e.ColumnIndex].Name == "View")
                {
                    ((FormParent)this.MdiParent).panggilKlien(3, dataGridViewTampilKlien.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                else if (dataGridViewTampilKlien.Columns[e.ColumnIndex].Name == "Delete")
                {
                    if (MessageBox.Show("Apakah anda yakin ingin mendelete " + dataGridViewTampilKlien.Rows[e.RowIndex].Cells[1].Value.ToString() + " ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                string commandText = "DELETE FROM dataklien WHERE kode = @kode";
                                MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", dataGridViewTampilKlien.Rows[e.RowIndex].Cells[0].ToString());
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
            updateDGVKlien();
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            updateDGVKlien("SELECT Kode, Nama, Principal, User, Notelp ,'Rp.' ||FORMAT(bataspiutang,2) as 'Piutang' FROM dataklien where lower(nama) like '%"+textBoxSearchKode.Text.ToLower()+"%' order by kode");
        }

        private void TabControlFull_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlFull.SelectedIndex == 0)
            {
                updateDGVKlien();
            }
            else if(tabControlFull.SelectedIndex == 1)
            {
                updateDGVProject();
            }
            else if (tabControlFull.SelectedIndex == 2)
            {
                updateChart();
            }
            else if (tabControlFull.SelectedIndex == 3)
            {
                updateNotif();

            }
        }

        private void DataGridViewProject_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewProject.Columns[e.ColumnIndex].Name == "Edit")
            {
                ((FormParent)this.MdiParent).panggilProject(2, dataGridViewProject.Rows[e.RowIndex].Cells[0].Value.ToString());
                updateDGVProject();
            }
            else if (dataGridViewProject.Columns[e.ColumnIndex].Name == "View")
            {
                ((FormParent)this.MdiParent).panggilProject(3, dataGridViewProject.Rows[e.RowIndex].Cells[0].Value.ToString());
                updateDGVProject();
            }
            else if (dataGridViewProject.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Apakah anda yakin ingin mendelete " + dataGridViewProject.Rows[e.RowIndex].Cells[1].Value.ToString() + " ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "DELETE FROM dataproject WHERE kode = @kode";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", dataGridViewProject.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah terhapus", "Berhasil");
                            updateDGVProject();
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

        private void ButtonSearchProject_Click(object sender, EventArgs e)
        {
            updateDGVProject("SELECT p.Kode,k.nama as 'Nama Klien' , p.nama as 'Nama Project', date_format(p.tagihan,'%d-%m-%Y') as 'Tagihan',date_format(p.deadline,'%d-%m-%Y') as 'Deadline', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total', CONCAT('Rp.',FORMAT(p.totalbiaya-p.kurang,2)) as 'Kurang Biaya',IF(p.status = 0, 'Belum Selesai', 'Sudah Selesai') AS 'Status Project' FROM dataproject p, dataklien k where p.kode like 'PR%' and p.kode_klien = k.kode and lower(p.nama) like '%" + textBoxSearchProject.Text.ToLower() + "%' order by kode");

        }

        public void update_area(string area)
        {
            if (area != "JAKARTA")
            {
                jakartatidak = false;
            }
        }

        private void ButtonNewProject_Click(object sender, EventArgs e)
        {
            ((FormParent)this.MdiParent).panggilProject(1);
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxSearchKode_TextChanged(object sender, EventArgs e)
        {

        }

        private void TabPageProject_Click(object sender, EventArgs e)
        {

        }

        private void ListBoxNotifikasi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void ListBoxNotifikasi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ChartKlien_Click(object sender, EventArgs e)
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

        private void TextBoxSearchProject_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
