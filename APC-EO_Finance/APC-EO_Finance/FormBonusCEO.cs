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

namespace APC_EO_Finance
{
    public partial class FormBonusCEO : Form
    {
        public FormBonusCEO()
        {
            InitializeComponent();
        }
        public void tampilDataProject(string kode)
        {
            this.kode = kode;
            groupBoxBonus.Visible = false;
            checkBoxKlienSendiri.Visible = true;
            dataGridViewTampilBonus.Visible = true;
            try
            {
                dataGridViewTampilBonus.DataSource = null;
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT p.nama,k.nama,bp.bonus,bp.lababersih,bp.persen,bp.omset,bp.kodepegawai,p.kode,bp.kliensendiri FROM bonusproject bp, dataproject p, datakaryawan k where bp.kodeproject= p.kode and bp.kodepegawai = k.kode and bp.kodeproject = '" + kode + "'", ClassConnection.Instance().Connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                DataTable tampil = new DataTable();
                tampil.Columns.Add("Kode Karyawan");
                tampil.Columns.Add("Nama Karyawan");
                tampil.Columns.Add("Bonus (%)");
                tampil.Columns.Add("Subtotal",typeof(decimal));
                kodeproject = table.Rows[0][7].ToString();
                hasilLabaBersih = Convert.ToInt64(table.Rows[0][3].ToString());
                OmsetProject = Convert.ToInt64(table.Rows[0][5].ToString());
                checkBoxKlienSendiri.Checked = table.Rows[0][8].ToString() == "1" ? true : false;
                checkBoxKlienSendiri.Enabled = false;
                totalbonus = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DataRow row1 = tampil.NewRow();
                    row1[0] = table.Rows[i][6].ToString();
                    row1[1] = table.Rows[i][1].ToString();
                    row1[2] = Convert.ToInt32(table.Rows[i][4].ToString());
                    row1[3] = Convert.ToDecimal(table.Rows[i][2].ToString());
                    totalbonus += Convert.ToInt64(table.Rows[i][2].ToString());
                    tampil.Rows.Add(row1);
                }
                DataRow row = tampil.NewRow();
                row[0] = "Total";
                row[1] = "-";
                row[2] = "-";
                row[3] = totalbonus;
                tampil.Rows.Add(row);
                dataGridViewTampilBonus.DataSource = tampil;

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
                labelLaba.Text = labelLaba.Text = "Laba Bersih Project '"+table.Rows[0][0].ToString()+"' : Rp. " + Convert.ToInt32(table.Rows[0][3].ToString()).ToString("N");
                dataGridViewTampilBonus.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Tahunan");
            }
        }
        long hasilLabaBersih,OmsetProject,totalbonus;
        string tahun,kode,kodeproject;
        public void tampilDataTahunan(string kode)
        {
            this.kode = kode;
            dataGridViewTampilBonus.Visible = false;
            checkBoxKlienSendiri.Visible = false;
            groupBoxBonus.Visible = true;
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM notifceotahunan where kode = " + kode + "",ClassConnection.Instance().Connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                hasilLabaBersih = Convert.ToInt32(table.Rows[0][1].ToString());
                numericUpDownPersen.Value = Convert.ToDecimal(table.Rows[0][2].ToString());
                numericUpDownTotal.Value = Convert.ToDecimal(table.Rows[0][3].ToString());
                tahun = table.Rows[0][4].ToString();
                labelLaba.Text = labelLaba.Text = "Laba Bersih Tahun "+tahun+": Rp. " + Convert.ToInt32(table.Rows[0][1].ToString()).ToString("N");
                
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message,"Error Tahunan");
            }
        }

        private void DataGridViewTampilBonus_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex != dataGridViewTampilBonus.Rows.Count - 1)
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
                        tb.Rows[e.RowIndex][3] = Convert.ToInt64((hasilLabaBersih * Convert.ToInt32(tb.Rows[e.RowIndex][2].ToString())) / 100);

                    }
                }
                else
                {
                    tb.Rows[e.RowIndex][3] = Convert.ToInt64((hasilLabaBersih * Convert.ToInt32(tb.Rows[e.RowIndex][2].ToString())) / 100);
                }
                totalbonus = 0;

                for (int i = 0; i < tb.Rows.Count - 1; i++)
                {
                    totalbonus += Convert.ToInt64(tb.Rows[i][3].ToString());
                }
                tb.Rows[tb.Rows.Count - 1][3] = totalbonus;
                dataGridViewTampilBonus.Invalidate();
            }
        }

        private void FormBonusCEO_Load(object sender, EventArgs e)
        {

            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
        }

        private void NumericUpDownPersen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownTotal.Value = Convert.ToDecimal(hasilLabaBersih * Convert.ToInt32(numericUpDownPersen.Value) / 100);
        }

        private void FormBonusCEO_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((FormParent)this.MdiParent).updateDGVNotifCeo();
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (groupBoxBonus.Visible)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        MySqlCommand cmd = new MySqlCommand("UPDATE notifceotahunan set status = 1, persenan = @persenan, subtotal = @subtotal where kode = @kode", ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@persenan", numericUpDownPersen.Value);
                        cmd.Parameters.AddWithValue("@subtotal", numericUpDownTotal.Value);
                        cmd.Parameters.AddWithValue("@kode", kode);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        cmd = new MySqlCommand("SELECT kode from datakaryawan where insertdata > str_to_date('2019-08-10','%Y-%m-%d') order by kode", ClassConnection.Instance().Connection);
                        DataTable tb = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(tb);

                        foreach (DataRow row in tb.Rows)
                        {
                            cmd = new MySqlCommand("INSERT INTO bonustahunan values (null,@kodepegawai,@bonus,1,@tanggal,@kodenotif,null,null)", ClassConnection.Instance().Connection);

                            cmd.Parameters.AddWithValue("@kodepegawai", row[0].ToString());
                            cmd.Parameters.AddWithValue("@bonus", Convert.ToInt32(numericUpDownTotal.Value/tb.Rows.Count));
                            cmd.Parameters.AddWithValue("@tanggal", tahun);
                            cmd.Parameters.AddWithValue("@kodenotif", kode);
                            rowsAffected = cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Bonus Telah Cair!","Berhasil");
                        ClassConnection.Instance().Close();
                        this.Close();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error Update");
                }
            }
            else
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        DataTable table = (dataGridViewTampilBonus.DataSource as DataTable);
                        foreach (DataRow row in table.Rows)
                        {
                            if (row[0].ToString() != "Total")
                            {
                                MySqlCommand cmd = new MySqlCommand("UPDATE bonusproject set status = 1,bonus = @bonus, persen = @persenan where kodeproject = @kodeproject and kodepegawai = @kodepegawai", ClassConnection.Instance().Connection);

                                MessageBox.Show(row[3].ToString());
                                cmd.Parameters.AddWithValue("@bonus", Convert.ToInt64(row[3].ToString()));
                                cmd.Parameters.AddWithValue("@persenan", Convert.ToInt64(row[2].ToString()));
                                cmd.Parameters.AddWithValue("@kodepegawai", row[0].ToString());
                                cmd.Parameters.AddWithValue("@kodeproject", kodeproject);
                                int rowsAffected = cmd.ExecuteNonQuery();

                            }
                        }
                        MessageBox.Show("Bonus Telah Cair!", "Berhasil");
                        ClassConnection.Instance().Close();
                        this.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Update");
                }
            }
        }
    }
}
