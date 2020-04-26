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
using System.Text.RegularExpressions;

namespace APC_EO_Finance
{
    public partial class FormAccounting : Form
    {
        public FormAccounting()
        {
            InitializeComponent();
        }
        int ctrDebit = 0;
        int ctrKredit = 0;
        Dictionary<string, ComboBox> simpanKombo = new Dictionary<string, ComboBox>();
        Dictionary<string, TextBox> simpanText = new Dictionary<string, TextBox>();
        Dictionary<string, NumericUpDown> simpanNumer = new Dictionary<string, NumericUpDown>();
        Dictionary<string, Label> simpanLabel = new Dictionary<string, Label>();

        public void updateAllComboCetak()
        {
            try
            {
                MySqlDataAdapter adapterCA = new MySqlDataAdapter("SELECT DISTINCT d.nama,d.kode FROM dataprojectacc d,flowprojectacc f WHERE f.kode_project = d.kode", ClassConnection.Instance().Connection);
                DataSet datasetCA = new DataSet();
                adapterCA.Fill(datasetCA);
                comboBoxLBProject.DisplayMember = "nama";
                comboBoxLBProject.ValueMember = "kode";
                comboBoxLBProject.DataSource = datasetCA.Tables[0];
                adapterCA = new MySqlDataAdapter("SELECT DISTINCT kode from historyflowacc where kodeakun like '1.0%'", ClassConnection.Instance().Connection);
                datasetCA = new DataSet();
                adapterCA.Fill(datasetCA);
                comboBoxJKKode.DisplayMember = "kode";
                comboBoxJKKode.ValueMember = "kode";
                comboBoxJKKode.DataSource = datasetCA.Tables[0];
                adapterCA = new MySqlDataAdapter("SELECT DISTINCT kode from historyflowacc where kode not in (SELECT DISTINCT kode from historyflowacc where kodeakun like '1.0%')", ClassConnection.Instance().Connection);
                datasetCA = new DataSet();
                adapterCA.Fill(datasetCA);
                comboBoxJUKode.DisplayMember = "kode";
                comboBoxJUKode.ValueMember = "kode";
                comboBoxJUKode.DataSource = datasetCA.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void updateComboProject()
        {
            try
            {
                MySqlDataAdapter adapterCA = new MySqlDataAdapter("SELECT nama,kode from dataprojectacc where YEAR(DATE_ADD(tanggal,INTERVAL IF(lanjut = 1,1,0) YEAR))>=YEAR(NOW())", ClassConnection.Instance().Connection);
                DataSet datasetCA = new DataSet();
                adapterCA.Fill(datasetCA);
                comboBoxProject.DisplayMember = "nama";
                comboBoxProject.ValueMember = "kode";
                comboBoxProject.DataSource = datasetCA.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void updateNeraca()
        {
            DataTable tableAkun = new DataTable();
            DataTable tmp = new DataTable();
            dataGridViewNeraca.DataSource = null;
            dataGridViewNeraca.Columns.Clear();
            int level = 0;
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(level) from dataakun", ClassConnection.Instance().Connection);
                    level = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            DataTable tmpCekLevel = new DataTable();
            MySqlDataAdapter adapterTmpCekLevel = new MySqlDataAdapter("SELECT kode from dataakun where kode not in (SELECT parent from dataakun group by parent)", ClassConnection.Instance().Connection);
            adapterTmpCekLevel.Fill(tmpCekLevel);
            tmpCekLevel.PrimaryKey = new DataColumn[] { tmpCekLevel.Columns["kode"] };

            tmp.Columns.Add("Parent");
            for (int i = 0; i < level; i++)
            {
                tmp.Columns.Add("Anak-" + i.ToString());
            }
            tmp.Columns.Add("Nominal");

            DataRow r = tmp.NewRow();
            r[0] = "ASET";
            tmp.Rows.Add(r);

            MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT kode,nama,concat('Rp.',format(nominal,2)),level from dataakun where kode like '1.0%'", ClassConnection.Instance().Connection);

            adapterKlien.Fill(tableAkun);


            foreach (DataRow row in tableAkun.Rows)
            {
                r = tmp.NewRow();
                r[Convert.ToInt32(row[3]) - 1] = row[0].ToString();
                r[Convert.ToInt32(row[3])] = row[1].ToString();
                if (tmpCekLevel.Rows.Contains(row[0].ToString()))
                {
                    r[tmp.Columns.Count - 1] = row[2].ToString();
                }
                tmp.Rows.Add(r);
            }

            long totalTmp = 0;
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT sum(nominal) from dataakun where kode like '1.0%' and kode not in (select parent from dataakun)", ClassConnection.Instance().Connection);
                    totalTmp = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            r = tmp.NewRow();
            r[0] = "Total: ASET";
            r[tmp.Columns.Count - 1] = "Rp." + totalTmp.ToString("N");
            tmp.Rows.Add(r);

            r = tmp.NewRow();
            r[0] = "LIABILITAS";
            tmp.Rows.Add(r);

            adapterKlien = new MySqlDataAdapter("SELECT kode,nama,concat('Rp.',format(nominal,2)),level from dataakun where kode like '2.0%'", ClassConnection.Instance().Connection);

            tableAkun = new DataTable();
            adapterKlien.Fill(tableAkun);

            foreach (DataRow row in tableAkun.Rows)
            {
                r = tmp.NewRow();
                r[Convert.ToInt32(row[3]) - 1] = row[0].ToString();
                r[Convert.ToInt32(row[3])] = row[1].ToString();
                if (tmpCekLevel.Rows.Contains(row[0].ToString()))
                {
                    r[tmp.Columns.Count - 1] = row[2].ToString();
                }
                tmp.Rows.Add(r);
            }
            long tmpLiabilitas = 0;
            totalTmp = 0;
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT sum(nominal) from dataakun where kode like '2.0%' and kode not in (select parent from dataakun)", ClassConnection.Instance().Connection);
                    totalTmp = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            r = tmp.NewRow();
            r[0] = "Total Liabilitas";
            r[tmp.Columns.Count - 1] = "Rp." + totalTmp.ToString("N");
            tmp.Rows.Add(r);
            tmpLiabilitas = totalTmp;

            r = tmp.NewRow();
            r[0] = "EQUITAS";
            tmp.Rows.Add(r);

            adapterKlien = new MySqlDataAdapter("SELECT kode,nama,concat('Rp.',format(nominal,2)),level from dataakun where kode like '3.0%'", ClassConnection.Instance().Connection);

            tableAkun = new DataTable();
            adapterKlien.Fill(tableAkun);

            foreach (DataRow row in tableAkun.Rows)
            {
                r = tmp.NewRow();
                r[Convert.ToInt32(row[3]) - 1] = row[0].ToString();
                r[Convert.ToInt32(row[3])] = row[1].ToString();
                if (tmpCekLevel.Rows.Contains(row[0].ToString()))
                {
                    r[tmp.Columns.Count - 1] = row[2].ToString();
                }
                tmp.Rows.Add(r);
            }
            long tmpEquitas = 0;
            totalTmp = 0;
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT sum(nominal) from dataakun where kode like '3.0%' and kode not in (select parent from dataakun)", ClassConnection.Instance().Connection);
                    totalTmp = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            r = tmp.NewRow();
            r[0] = "Total Equitas";
            r[tmp.Columns.Count - 1] = "Rp." + totalTmp.ToString("N");
            tmp.Rows.Add(r);
            tmpEquitas = totalTmp;


            r = tmp.NewRow();
            r[0] = "Total: LIABILITAS + EQUITAS";
            r[tmp.Columns.Count - 1] = "Rp." + (tmpLiabilitas + tmpEquitas).ToString("N");
            tmp.Rows.Add(r);


            dataGridViewNeraca.DataSource = tmp;

            dataGridViewNeraca.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for (int i = 0; i < dataGridViewNeraca.ColumnCount; i++)
            {
                dataGridViewNeraca.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewNeraca.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            foreach (DataGridViewRow dataRow in dataGridViewNeraca.Rows)
            {
                if (dataRow.Cells[0].Value.ToString().Contains("Total:"))
                {
                    dataRow.DefaultCellStyle.BackColor = Color.Orange;
                }
                if (Regex.IsMatch(dataRow.Cells[0].Value.ToString(), @"^[a-zA-Z]+$"))
                {
                    dataRow.DefaultCellStyle.BackColor = Color.HotPink;

                }
            }
        }

        public void updatepk()
        {
            DataTable tableAkun = new DataTable();
            DataTable tmp = new DataTable();
            dataGridViewPK.DataSource = null;
            dataGridViewPK.Columns.Clear();
            int level = 0;
            tmp.Columns.Add("Nama");
            tmp.Columns.Add("Nominal");
            DataRow r = tmp.NewRow();
            r[0] = "ASET";
            tmp.Rows.Add(r);
            try
            {
                if (ClassConnection.Instance().Connecting())
                {

                    MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT nama,nominal FROM dataakun WHERE kode LIKE '1.0%' AND nominal > 0;", ClassConnection.Instance().Connection);
                    tableAkun = new DataTable();
                    adapterKlien.Fill(tableAkun);
                    foreach (DataRow row in tableAkun.Rows)
                    {
                        r = tmp.NewRow();
                        r[0] = row[0].ToString();
                        r[1] = "Rp." + row[1].ToString();
                        tmp.Rows.Add(r);
                    }
                    
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }


            long totalTmp = 0;
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT sum(nominal) from dataakun where kode like '1.0%' and kode not in (select parent from dataakun)", ClassConnection.Instance().Connection);
                    totalTmp = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            r = tmp.NewRow();
            r[0] = "Total: ASET";
            r[tmp.Columns.Count - 1] = "Rp." + totalTmp.ToString("N");
            tmp.Rows.Add(r);
            r = tmp.NewRow();
            r[0] = "Liabilitas";
            tmp.Rows.Add(r);
            try
            {
                if (ClassConnection.Instance().Connecting())
                {

                    MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT nama,nominal FROM dataakun WHERE kode LIKE '2.0%' AND nominal > 0;", ClassConnection.Instance().Connection);
                    tableAkun = new DataTable();
                    adapterKlien.Fill(tableAkun);
                    foreach (DataRow row in tableAkun.Rows)
                    {
                        r = tmp.NewRow();
                        r[0] = row[0].ToString();
                        r[1] = "Rp." + row[1].ToString();
                        tmp.Rows.Add(r);
                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            long tmpLiabilitas = 0;
            totalTmp = 0;
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT sum(nominal) from dataakun where kode like '2.0%' and kode not in (select parent from dataakun)", ClassConnection.Instance().Connection);
                    totalTmp = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            r = tmp.NewRow();
            r[0] = "Total Liabilitas";
            r[tmp.Columns.Count - 1] = "Rp." + totalTmp.ToString("N");
            tmp.Rows.Add(r);
            tmpLiabilitas = totalTmp;
            dataGridViewPK.DataSource = tmp;
            r = tmp.NewRow();
            r[0] = "EQUITAS";
            tmp.Rows.Add(r);

            try
            {
                if (ClassConnection.Instance().Connecting())
                {

                    MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT nama,nominal FROM dataakun WHERE kode LIKE '3.0%' AND nominal > 0;", ClassConnection.Instance().Connection);
                    tableAkun = new DataTable();
                    adapterKlien.Fill(tableAkun);
                    foreach (DataRow row in tableAkun.Rows)
                    {
                        r = tmp.NewRow();
                        r[0] = row[0].ToString();
                        r[1] = "Rp." + row[1].ToString();
                        tmp.Rows.Add(r);
                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            long tmpEquitas = 0;
            totalTmp = 0;
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT sum(nominal) from dataakun where kode like '3.0%' and kode not in (select parent from dataakun)", ClassConnection.Instance().Connection);
                    totalTmp = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            r = tmp.NewRow();
            r[0] = "Total Equitas";
            r[tmp.Columns.Count - 1] = "Rp." + totalTmp.ToString("N");
            tmp.Rows.Add(r);
            tmpEquitas = totalTmp;
            r = tmp.NewRow();
            r[0] = "Total Equitas+Liabilitas";
            long totalall = tmpEquitas + tmpLiabilitas;
            r[tmp.Columns.Count - 1] = "Rp." + totalall.ToString("N");
            tmp.Rows.Add(r);
            dataGridViewPK.DataSource = tmp;

            dataGridViewPK.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for (int i = 0; i < dataGridViewPK.ColumnCount; i++)
            {
                dataGridViewPK.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewPK.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            for (int i = 0; i < dataGridViewPK.RowCount - 1; i++)
            {
                if (dataGridViewPK.Rows[i].Cells[0].Value.ToString().Contains("Total: ASET"))
                {
                    dataGridViewPK.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
                if (dataGridViewPK.Rows[i].Cells[0].Value.ToString().Contains("Total Liabilitas"))
                {
                    dataGridViewPK.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
                if (dataGridViewPK.Rows[i].Cells[0].Value.ToString().Contains("Total Equitas"))
                {
                    dataGridViewPK.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
                if (dataGridViewPK.Rows[i].Cells[0].Value.ToString().Contains("Total Equitas+Liabilitas"))
                {
                    dataGridViewPK.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
            }

        }
        public void updateTrialBalance()
        {
            DataTable tmp = new DataTable();
            dataGridViewTB.DataSource = null;
            dataGridViewTB.Columns.Clear();
            
            tmp.Columns.Add("No Perkiraan");
            tmp.Columns.Add("Chart of Account");
            tmp.Columns.Add("Saldo Awal Debit");
            tmp.Columns.Add("Saldo Awal Kredit");
            tmp.Columns.Add("Kas Debit");
            tmp.Columns.Add("Kas Kredit");
            tmp.Columns.Add("AJE Debit");
            tmp.Columns.Add("AJE Kredit");
            tmp.Columns.Add("Saldo Akhir Debit");
            tmp.Columns.Add("Saldo Akhir Kredit");
            tmp.Columns.Add("Saldo");

            MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT kode,nama,nominal FROM dataakun", ClassConnection.Instance().Connection);
            DataTable tableAkun = new DataTable();
            adapterKlien.Fill(tableAkun);
            DataTable tableParent = new DataTable();
            adapterKlien = new MySqlDataAdapter("SELECT parent FROM dataakun GROUP BY parent", ClassConnection.Instance().Connection);
            adapterKlien.Fill(tableParent);
            tableParent.PrimaryKey = new DataColumn[] { tableParent.Columns["parent"] };
            DataTable tableHistoryAset = new DataTable();
            adapterKlien = new MySqlDataAdapter("SELECT DISTINCT kode from historyflowacc where kodeakun like '1.0%' and CONCAT(MONTH(tanggal),'-',YEAR(tanggal)) =  '" + (comboBoxBulan.SelectedIndex + 1) + "-" + comboBoxTahun.Text + "'", ClassConnection.Instance().Connection);
            adapterKlien.Fill(tableHistoryAset);
            tableHistoryAset.PrimaryKey = new DataColumn[] { tableHistoryAset.Columns["kode"] };
            DataTable tableHistory = new DataTable();
            adapterKlien = new MySqlDataAdapter("SELECT DISTINCT kode from historyflowacc where kode not in (SELECT kode from historyflowacc where kodeakun like '1.0%' and CONCAT(MONTH(tanggal),'-',YEAR(tanggal)) =  '" + (comboBoxBulan.SelectedIndex + 1) + "-" + comboBoxTahun.Text + "') and CONCAT(MONTH(tanggal),'-',YEAR(tanggal)) =  '" + (comboBoxBulan.SelectedIndex + 1) + "-" + comboBoxTahun.Text + "'", ClassConnection.Instance().Connection);
            adapterKlien.Fill(tableHistory);
            tableHistory.PrimaryKey = new DataColumn[] { tableHistory.Columns["kode"] };
            DataTable tableFull = new DataTable();
            adapterKlien = new MySqlDataAdapter("SELECT kodeakun,kode, kodedebitkredit, tanggal, nominal FROM historyflowacc where CONCAT(MONTH(tanggal),'-',YEAR(tanggal)) =  '" + (comboBoxBulan.SelectedIndex + 1) + "-" + comboBoxTahun.Text + "'", ClassConnection.Instance().Connection);
            adapterKlien.Fill(tableFull);
            DataTable tableKurangAkun = new DataTable();
            adapterKlien = new MySqlDataAdapter("SELECT kodeakun,SUM(IF(kodedebitkredit='K',nominal,-nominal)) FROM historyflowacc WHERE YEAR(tanggal) >= "+comboBoxTahun.Text+" AND MONTH(tanggal) >= "+(comboBoxBulan.SelectedIndex+1)+" GROUP BY kodeakun", ClassConnection.Instance().Connection);
            adapterKlien.Fill(tableKurangAkun);
            tableKurangAkun.PrimaryKey = new DataColumn[] { tableKurangAkun.Columns["kodeakun"] };

            foreach (DataRow r in tableAkun.Rows)
            {
                DataRow row = tmp.NewRow();
                row[0] = r[0].ToString();
                row[1] = r[1].ToString();
                row[4] = 0;
                row[5] = 0;
                row[6] = 0;
                row[7] = 0;
                row[8] = 0;
                row[9] = 0;
                row[10] = 0;
                if (tableParent.Rows.Contains(r[0].ToString()))
                {
                    for (int i = 2; i < tmp.Columns.Count; i++)
                    {
                        row[i] = "-";
                    }
                }
                else
                {
                    if (r[0].ToString().Contains("2.0") || r[0].ToString().Contains("3.0") || r[0].ToString().Contains("5.0"))
                    {
                        row[2] = "-";
                        row[3] = r[2].ToString();
                        if (tableKurangAkun.Rows.Contains(r[0].ToString()))
                        {
                            row[3] = Convert.ToInt64(row[3].ToString()) + Convert.ToInt64(tableKurangAkun.Rows.Find(r[0].ToString())[1].ToString());
                            
                        }
                    }
                    else
                    {
                        row[2] = r[2].ToString();
                        row[3] = "-";
                        if (tableKurangAkun.Rows.Contains(r[0].ToString()))
                        {
                            row[2] = Convert.ToInt64(row[2].ToString()) + Convert.ToInt64(tableKurangAkun.Rows.Find(r[0].ToString())[1].ToString());
                        }
                    }
                    foreach (DataRow rowTmp in tableFull.Rows)
                    {
                        if (rowTmp[0].ToString() == r[0].ToString())
                        {
                            if (tableHistoryAset.Rows.Contains(rowTmp[1].ToString()))
                            {
                                if (rowTmp[2].ToString() == "K")
                                {
                                    long tempLong = Convert.ToInt64(row[5].ToString())+Convert.ToInt64(rowTmp[4].ToString());
                                    row[5] = tempLong;
                                }
                                else
                                {
                                    long tempLong = Convert.ToInt64(row[4].ToString()) + Convert.ToInt64(rowTmp[4].ToString());
                                    row[4] = tempLong;
                                }
                            }
                            else if (tableHistory.Rows.Contains(rowTmp[1].ToString()))
                            {
                                if (rowTmp[2].ToString() == "K")
                                {
                                    long tempLong = Convert.ToInt64(row[7].ToString()) + Convert.ToInt64(rowTmp[4].ToString());
                                    row[7] = tempLong;
                                }
                                else
                                {
                                    long tempLong = Convert.ToInt64(row[6].ToString()) + Convert.ToInt64(rowTmp[4].ToString());
                                    row[6] = tempLong;
                                }
                            }
                        }
                    }
                    row[8] = Convert.ToInt64(row[8].ToString()) + Convert.ToInt64(row[6].ToString()) + Convert.ToInt64(row[4].ToString());
                    row[9] = Convert.ToInt64(row[9].ToString()) + Convert.ToInt64(row[7].ToString()) + Convert.ToInt64(row[5].ToString());
                    long tempSaldo = tempSaldo = Convert.ToInt64(row[2].ToString() != "-" ? row[2] : row[3]);

                    row[10] = tempSaldo + Convert.ToInt64(row[8].ToString()) - Convert.ToInt64(row[9].ToString());
                    
                }
                tmp.Rows.Add(row);
            }
            DataRow dr = tmp.NewRow();
            dr[1] = "Total";
            dr[2] = 0;
            dr[3] = 0;
            dr[4] = 0;
            dr[5] = 0;
            dr[6] = 0;
            dr[7] = 0;
            dr[8] = 0;
            dr[9] = 0;
            dr[10] = 0;
            for (int i = 0; i < tmp.Rows.Count; i++)
            {
                for (int j = 2; j < 11; j++)
                {
                    if (tmp.Rows[i][j].ToString() == "0")
                    {
                        tmp.Rows[i][j] = "-";
                    }
                    if (tmp.Rows[i][j].ToString() != "-")
                    {
                        dr[j] = Convert.ToInt64(dr[j].ToString()) + Convert.ToInt64(tmp.Rows[i][j].ToString());
                        tmp.Rows[i][j] = "Rp." + Convert.ToInt64(tmp.Rows[i][j].ToString()).ToString("N");
                    }
                }
            }
            dr[10] = 0;
            for (int j = 2; j < 11; j++)
            {

                if (dr[j].ToString() != "0")
                {
                    dr[j] = "Rp." + Convert.ToInt64(dr[j].ToString()).ToString("N");
                }
                else
                {
                    dr[j] = "-";
                }
            }
            tmp.Rows.Add(dr);
            dataGridViewTB.DataSource = tmp;
            dataGridViewTB.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for (int i = 0; i < dataGridViewTB.RowCount; i++)
            {
                if (tableParent.Rows.Contains(dataGridViewTB.Rows[i].Cells[0].Value.ToString()))
                {
                    dataGridViewTB.Rows[i].DefaultCellStyle.BackColor = Color.DarkGray;
                }
            }
            for (int i = 0; i < dataGridViewTB.ColumnCount; i++)
            {
                dataGridViewTB.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewTB.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }    
        }
        public void resetKodeFlowAcc()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "HFA" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from historyflowacc where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(9, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxKodeFlow.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void updateComboAkun()
        {
            try
            {
                MySqlDataAdapter adapterCA = new MySqlDataAdapter("SELECT concat(kode,' - ',nama) as nama,kode from dataakun where kode !='-' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetCA = new DataSet();
                adapterCA.Fill(datasetCA);
                comboBoxKodeAkun.DisplayMember = "nama";
                comboBoxKodeAkun.ValueMember = "kode";
                comboBoxKodeAkun.DataSource = datasetCA.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void updateComboFlow(ComboBox cb)
        {
            try
            {
                MySqlDataAdapter adapterCA = new MySqlDataAdapter("SELECT concat(kode,'-',nama) as kode,concat('Saldo : Rp. ',format(nominal,2)) as nama from dataakun where kode not in (select parent from dataakun group by parent) order by kode", ClassConnection.Instance().Connection);
                DataSet datasetCA = new DataSet();
                adapterCA.Fill(datasetCA);
                cb.DisplayMember = "kode";
                cb.ValueMember = "nama";
                cb.DataSource = datasetCA.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void spawnFlow()
        {
            resetKodeFlowAcc();
            TextBox tmpDebitText = new TextBox();
            tmpDebitText.Location = new Point(250, 100 + ctrDebit * 50);
            tmpDebitText.Size = new Size(300, 50);
            tmpDebitText.ReadOnly = true;
            simpanText.Add("Debit0", tmpDebitText);

            ComboBox tmpDebitCombo = new ComboBox();
            tmpDebitCombo.Location = new Point(250, 50 + ctrDebit * 50);
            tmpDebitCombo.Size = new Size(300, 50);
            tmpDebitCombo.SelectedIndexChanged += gantiIndex;
            tmpDebitCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tmpDebitCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            updateComboFlow(tmpDebitCombo);

            simpanKombo.Add("Debit"+ctrDebit.ToString(), tmpDebitCombo);

            NumericUpDown tmpDebitNumeric = new NumericUpDown();
            tmpDebitNumeric.ThousandsSeparator = true;
            tmpDebitNumeric.Size = new Size(250, 50);
            tmpDebitNumeric.Maximum = long.MaxValue;
            tmpDebitNumeric.DecimalPlaces = 2;
            tmpDebitNumeric.Location = new Point(300, 150 + ctrDebit * 50);
            simpanNumer.Add("Debit"+ctrDebit.ToString(), tmpDebitNumeric);

            Label rupiahPlace = new Label();
            rupiahPlace.Location = new Point(250, 150 + ctrDebit * 50);
            rupiahPlace.Text = "Rp.";
            rupiahPlace.Size = new Size(50, 50);
            simpanLabel.Add("Debit" + ctrDebit.ToString(), rupiahPlace);

            ctrDebit++;

            TextBox tmpKreditText = new TextBox();
            tmpKreditText.Location = new Point(650, 100 + ctrKredit * 100);
            tmpKreditText.Size = new Size(300, 50);
            tmpKreditText.ReadOnly = true;
            simpanText.Add("Kredit" + ctrKredit.ToString(), tmpKreditText);

            ComboBox tmpKreditCombo = new ComboBox();
            tmpKreditCombo.Location = new Point(650, 50 + ctrKredit * 100);
            tmpKreditCombo.Size = new Size(300, 50);
            tmpKreditCombo.SelectedIndexChanged += gantiIndex;
            tmpKreditCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tmpKreditCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            updateComboFlow(tmpKreditCombo);
            simpanKombo.Add("Kredit" + ctrKredit.ToString(), tmpKreditCombo);

            NumericUpDown tmpKreditNumeric = new NumericUpDown();
            tmpKreditNumeric.ThousandsSeparator = true;
            tmpKreditNumeric.Size = new Size(250, 50);
            tmpKreditNumeric.Maximum = long.MaxValue;
            tmpKreditNumeric.DecimalPlaces = 2;
            tmpKreditNumeric.Location = new Point(700, 150 + ctrKredit * 100);
            simpanNumer.Add("Kredit"+ctrKredit.ToString(), tmpKreditNumeric);

            rupiahPlace = new Label();
            rupiahPlace.Location = new Point(650, 150 + ctrKredit * 100);
            rupiahPlace.Text = "Rp.";
            rupiahPlace.Size = new Size(50, 50);
            simpanLabel.Add("Kredit"+ctrKredit.ToString(), rupiahPlace);

            ctrKredit++;
            foreach (TextBox tb in simpanText.Values)
            {
                tabPK.TabPages[1].Controls.Add(tb);
            }
            foreach (ComboBox cb in simpanKombo.Values)
            {
                tabPK.TabPages[1].Controls.Add(cb);
            }
            foreach (NumericUpDown nud in simpanNumer.Values)
            {
                tabPK.TabPages[1].Controls.Add(nud);
            }
            foreach (Label lbl in simpanLabel.Values)
            {
                tabPK.TabPages[1].Controls.Add(lbl);
            }
        }


        private void gantiIndex(object sender, EventArgs e)
        {
            foreach (var cb in simpanKombo)
            {
                if (cb.Value == (sender as ComboBox))
                {
                    simpanText[cb.Key].Text = (sender as ComboBox).SelectedValue.ToString();
                }
            }
        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void FormAccounting_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            updateComboAkun();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabPK.SelectedIndex == 0)
            {
                updateComboAkun();
            }
            else if (tabPK.SelectedIndex == 1)
            {
                buttonClearAcc.PerformClick();
                updateComboProject();
            }
            else if (tabPK.SelectedIndex == 2)
            {
                updateNeraca();
            }
            else if (tabPK.SelectedIndex == 3)
            {
                comboBoxBulan.DataSource = CultureInfo.GetCultureInfo("id-ID").DateTimeFormat.MonthNames.Take(12).ToList();
                comboBoxTahun.DataSource = Enumerable.Range(2000, DateTime.Today.Year - 1999).ToList();
                updateTrialBalance();
            }
            else if (tabPK.SelectedIndex == 4)
            {
                comboBoxLBTahunan.DataSource = Enumerable.Range(2000, DateTime.Today.Year - 1999).ToList();
                groupBoxJK.Visible = false;
                groupBoxJU.Visible = false;
                groupBoxLB.Visible = false;
                updateAllComboCetak();

            }else if(tabPK.SelectedIndex == 5)
            {
                updatepk();
            }else if(tabPK.SelectedIndex == 6)
            {
                comboBox1.DataSource = Enumerable.Range(2000, DateTime.Today.Year - 1999).ToList();
                updateComboLB();
            }
        }

        private void ButtonAddKredit_Click(object sender, EventArgs e)
        {
            Point temp = buttonAddKredit.Location;

            if (ctrKredit > 5)
            {
                vScrollBarFlow.Maximum += 150;
            }

            TextBox tmpKreditText = new TextBox();
            tmpKreditText.Location = new Point(650, simpanText["Kredit" + (ctrKredit - 1).ToString()].Location.Y + 150);
            tmpKreditText.Size = new Size(300, 50);
            tmpKreditText.ReadOnly = true;
            simpanText.Add("Kredit" + ctrKredit.ToString(), tmpKreditText);

            ComboBox tmpKreditCombo = new ComboBox();
            tmpKreditCombo.Location = new Point(650, simpanKombo["Kredit" + (ctrKredit - 1).ToString()].Location.Y + 150);
            tmpKreditCombo.Size = new Size(300, 50);
            tmpKreditCombo.SelectedIndexChanged += gantiIndex;
            tmpKreditCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tmpKreditCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            updateComboFlow(tmpKreditCombo);
            simpanKombo.Add("Kredit" + ctrKredit.ToString(), tmpKreditCombo);

            NumericUpDown tmpKreditNumeric = new NumericUpDown();
            tmpKreditNumeric.ThousandsSeparator = true;
            tmpKreditNumeric.Size = new Size(250, 50);
            tmpKreditNumeric.Maximum = long.MaxValue;
            tmpKreditNumeric.DecimalPlaces = 2;
            tmpKreditNumeric.Location = new Point(700, simpanNumer["Kredit" + (ctrKredit - 1).ToString()].Location.Y + 150);
            simpanNumer.Add("Kredit" + ctrKredit.ToString(), tmpKreditNumeric);

            Label rupiahPlace = new Label();
            rupiahPlace.Location = new Point(650, simpanLabel["Kredit" + (ctrKredit - 1).ToString()].Location.Y + 150);
            rupiahPlace.Text = "Rp.";
            rupiahPlace.Size = new Size(50, 50);
            simpanLabel.Add("Kredit" + ctrKredit.ToString(), rupiahPlace);

            ctrKredit++;

            tabPK.TabPages[1].Controls.Add(tmpKreditText);

            tabPK.TabPages[1].Controls.Add(tmpKreditCombo);

            tabPK.TabPages[1].Controls.Add(tmpKreditNumeric);

            tabPK.TabPages[1].Controls.Add(rupiahPlace);

            buttonAddKredit.Location = new Point(temp.X,temp.Y+150);
        }


        private void ButtonClearAcc_Click(object sender, EventArgs e)
        {
            foreach (TextBox tb in simpanText.Values)
            {
                tabPK.TabPages[1].Controls.Remove(tb);
            }
            foreach (ComboBox cb in simpanKombo.Values)
            {
                tabPK.TabPages[1].Controls.Remove(cb);
            }
            foreach (NumericUpDown nud in simpanNumer.Values)
            {
                tabPK.TabPages[1].Controls.Remove(nud);
            }
            foreach (Label lbl in simpanLabel.Values)
            {
                tabPK.TabPages[1].Controls.Remove(lbl);
            }
            simpanKombo.Clear();
            simpanText.Clear();
            simpanNumer.Clear();
            simpanLabel.Clear();
            richTextBoxDeskripsiFlow.Text = "";
            dateTimePickerFlow.Value = DateTime.Now;
            buttonAddKredit.Location = new Point(650, 200);
            buttonAddDebit.Location = new Point(250, 200);
            ctrDebit = 0;
            ctrKredit = 0;
            spawnFlow();
            checkBoxProject.Checked = false;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            (this.MdiParent as FormParent).panggilTambahAkun();
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            (this.MdiParent as FormParent).panggilTambahAkun(2,comboBoxKodeAkun.SelectedValue.ToString());
        }

        private void ComboBoxKodeAkun_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    
                    DataTable tableAkun = new DataTable();
                    dataGridViewDebitAkun.DataSource = null;
                    dataGridViewDebitAkun.Columns.Clear();
                    MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT hf.kodeakun AS 'Kode Akun',hf.kode AS 'Kode',DATE_FORMAT(hf.tanggal,'%d-%m-%Y') AS 'Tanggal',CONCAT('Rp.',FORMAT(hf.nominal,2)) AS 'Nominal', hf.keterangan AS 'Keterangan'  FROM historyflowacc hf WHERE hf.kodeakun like '" + comboBoxKodeAkun.SelectedValue+"%' and hf.kodedebitkredit = 'D' group by hf.kodeakun,hf.kode order by hf.kodeakun,hf.kode", ClassConnection.Instance().Connection);

                    adapterKlien.Fill(tableAkun);

                    dataGridViewDebitAkun.DataSource = tableAkun;
                    dataGridViewDebitAkun.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    for (int i = 0; i < dataGridViewDebitAkun.ColumnCount; i++)
                    {
                        dataGridViewDebitAkun.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridViewDebitAkun.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                    }

                    DataTable tableAkun2 = new DataTable();
                    dataGridViewKreditAkun.DataSource = null;
                    dataGridViewKreditAkun.Columns.Clear();
                    adapterKlien = new MySqlDataAdapter("SELECT hf.kodeakun AS 'Kode Akun',hf.kode AS 'Kode',DATE_FORMAT(hf.tanggal,'%d-%m-%Y') AS 'Tanggal',CONCAT('Rp.',FORMAT(hf.nominal,2)) AS 'Nominal', hf.keterangan AS 'Keterangan'  FROM historyflowacc hf WHERE hf.kodeakun like '" + comboBoxKodeAkun.SelectedValue + "%' and hf.kodedebitkredit = 'K' group by hf.kodeakun,hf.kode order by hf.kodeakun,hf.kode", ClassConnection.Instance().Connection);

                    adapterKlien.Fill(tableAkun2);

                    dataGridViewKreditAkun.DataSource = tableAkun2;
                    dataGridViewKreditAkun.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    for (int i = 0; i < dataGridViewKreditAkun.ColumnCount; i++)
                    {
                        dataGridViewKreditAkun.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridViewKreditAkun.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void NumericUpDownOutstanding_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ButtonSubmitAcc_Click(object sender, EventArgs e)
        {
            UInt64 hasilJumlah = 0;
            for (int i = 0; i < ctrDebit; i++)
            {
                hasilJumlah += Convert.ToUInt64(simpanNumer["Debit" + i.ToString()].Value);
            }
            for (int i = 0; i < ctrKredit; i++)
            {
                hasilJumlah -= Convert.ToUInt64(simpanNumer["Kredit" + i.ToString()].Value);
            }
            if (hasilJumlah != 0)
            {
                MessageBox.Show("Inputan Data Tidak Balance");
            }
            else if(checkBoxProject.Checked && comboBoxProject.SelectedIndex == -1)
            {
                MessageBox.Show("Belum Memilih Project");
            }
            else
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        MySqlTransaction tr = ClassConnection.Instance().Connection.BeginTransaction();
                        try
                        {
                            int rowsAffected;
                            string commandText;
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = ClassConnection.Instance().Connection;
                            cmd.Transaction = tr;
                            for (int i = 0; i < ctrDebit; i++)
                            {
                                cmd.Parameters.Clear();
                                commandText = "INSERT INTO historyflowacc VALUES(null,@kode,@kodeakun,@kodedebitkredit,@tanggal,@nominal,@keterangan,null,null)";
                                cmd.CommandText = commandText;
                                cmd.Parameters.AddWithValue("@kode", textBoxKodeFlow.Text);
                                cmd.Parameters.AddWithValue("@kodeakun", simpanKombo["Debit" + i.ToString()].Text);
                                cmd.Parameters.AddWithValue("@kodedebitkredit", "D");
                                cmd.Parameters.AddWithValue("@tanggal", dateTimePickerFlow.Value.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@nominal", simpanNumer["Debit" + i.ToString()].Value);
                                cmd.Parameters.AddWithValue("@keterangan", richTextBoxDeskripsiFlow.Text);
                                rowsAffected = cmd.ExecuteNonQuery();

                                if (!(simpanKombo["Debit" + i.ToString()].Text.Contains("2.0") || simpanKombo["Debit" + i.ToString()].Text.Contains("3.0") || simpanKombo["Debit" + i.ToString()].Text.Contains("4.0")))
                                {
                                    cmd.Parameters.Clear();
                                    commandText = "UPDATE dataakun set nominal = nominal + @nominal where kode = @kode";
                                    cmd.CommandText = commandText;
                                    cmd.Parameters.AddWithValue("@kode", simpanKombo["Debit" + i.ToString()].Text.Split('-')[0].ToString());
                                    cmd.Parameters.AddWithValue("@nominal", simpanNumer["Debit" + i.ToString()].Value);
                                    rowsAffected = cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmd.Parameters.Clear();
                                    commandText = "UPDATE dataakun set nominal = nominal - @nominal where kode = @kode";
                                    cmd.CommandText = commandText;
                                    cmd.Parameters.AddWithValue("@kode", simpanKombo["Debit" + i.ToString()].Text.Split('-')[0].ToString());
                                    cmd.Parameters.AddWithValue("@nominal", simpanNumer["Debit" + i.ToString()].Value);
                                    rowsAffected = cmd.ExecuteNonQuery();
                                }

                            }

                            for (int i = 0; i < ctrKredit; i++)
                            {
                                cmd.Parameters.Clear();
                                commandText = "INSERT INTO historyflowacc VALUES(null,@kode,@kodeakun,@kodedebitkredit,@tanggal,@nominal,@keterangan,null,null)";
                                cmd.CommandText = commandText;
                                cmd.Parameters.AddWithValue("@kode", textBoxKodeFlow.Text);
                                cmd.Parameters.AddWithValue("@kodeakun", simpanKombo["Kredit" + i.ToString()].Text);
                                cmd.Parameters.AddWithValue("@kodedebitkredit", "K");
                                cmd.Parameters.AddWithValue("@tanggal", dateTimePickerFlow.Value.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@nominal", simpanNumer["Kredit" + i.ToString()].Value);
                                cmd.Parameters.AddWithValue("@keterangan", richTextBoxDeskripsiFlow.Text);
                                rowsAffected = cmd.ExecuteNonQuery();


                                if ((simpanKombo["Kredit" + i.ToString()].Text.Contains("2.0") || simpanKombo["Kredit" + i.ToString()].Text.Contains("3.0") || simpanKombo["Kredit" + i.ToString()].Text.Contains("4.0")))
                                {
                                    cmd.Parameters.Clear();
                                    commandText = "UPDATE dataakun set nominal = nominal + @nominal where kode = @kode";
                                    cmd.CommandText = commandText;
                                    cmd.Parameters.AddWithValue("@kode", simpanKombo["Kredit" + i.ToString()].Text.Split('-')[0].ToString());
                                    cmd.Parameters.AddWithValue("@nominal", simpanNumer["Kredit" + i.ToString()].Value);
                                    rowsAffected = cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmd.Parameters.Clear();
                                    commandText = "UPDATE dataakun set nominal = nominal - @nominal where kode = @kode";
                                    cmd.CommandText = commandText;
                                    cmd.Parameters.AddWithValue("@kode", simpanKombo["Kredit" + i.ToString()].Text.Split('-')[0].ToString());
                                    cmd.Parameters.AddWithValue("@nominal", simpanNumer["Kredit" + i.ToString()].Value);
                                    rowsAffected = cmd.ExecuteNonQuery();
                                }
                            }
                            if (checkBoxProject.Checked)
                            {
                                cmd.Parameters.Clear();
                                commandText = "INSERT INTO flowprojectacc VALUES(null,@kodepr,@kodefl,@tanggal,null,null)";
                                cmd.CommandText = commandText;
                                cmd.Parameters.AddWithValue("@kodepr", comboBoxProject.SelectedValue);
                                cmd.Parameters.AddWithValue("@kodefl", textBoxKodeFlow.Text);
                                cmd.Parameters.AddWithValue("@tanggal", dateTimePickerFlow.Value.ToString("yyyy-MM-dd"));
                                rowsAffected = cmd.ExecuteNonQuery();
                            }

                            tr.Commit();
                            MessageBox.Show("Data telah Terinput", "Berhasil");
                            ClassConnection.Instance().Close();
                            buttonClearAcc.PerformClick();

                        }
                        catch (Exception ex)
                        {
                            tr.Rollback();
                            MessageBox.Show(ex.Message, "Error");
                        }
                    }
                }
            }
        }

        private void ButtonAddDebit_Click(object sender, EventArgs e)
        {
            Point temp = buttonAddDebit.Location;
            if (ctrDebit > 5)
            {
                vScrollBarFlow.Maximum += 150;
            }
            TextBox tmpDebitText = new TextBox();
            tmpDebitText.Location = new Point(250, simpanText["Debit"+(ctrDebit-1).ToString()].Location.Y+150);
            tmpDebitText.Size = new Size(300, 50);
            tmpDebitText.ReadOnly = true;
            simpanText.Add("Debit" + ctrDebit.ToString(), tmpDebitText);

            ComboBox tmpDebitCombo = new ComboBox();
            tmpDebitCombo.Location = new Point(250, simpanKombo["Debit" + (ctrDebit - 1).ToString()].Location.Y + 150);
            tmpDebitCombo.Size = new Size(300, 50);
            tmpDebitCombo.SelectedIndexChanged += gantiIndex;
            tmpDebitCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tmpDebitCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            updateComboFlow(tmpDebitCombo);
            simpanKombo.Add("Debit" + ctrDebit.ToString(), tmpDebitCombo);

            NumericUpDown tmpDebitNumeric = new NumericUpDown();
            tmpDebitNumeric.ThousandsSeparator = true;
            tmpDebitNumeric.Size = new Size(250, 50);
            tmpDebitNumeric.Maximum = long.MaxValue;
            tmpDebitNumeric.DecimalPlaces = 2;
            tmpDebitNumeric.Location = new Point(300, simpanNumer["Debit" + (ctrDebit - 1).ToString()].Location.Y + 150);
            simpanNumer.Add("Debit" + ctrDebit.ToString(), tmpDebitNumeric);

            Label rupiahPlace = new Label();
            rupiahPlace.Location = new Point(250, simpanLabel["Debit" + (ctrDebit - 1).ToString()].Location.Y + 150);
            rupiahPlace.Text = "Rp.";
            rupiahPlace.Size = new Size(50, 50);
            simpanLabel.Add("Debit" + ctrDebit.ToString(), rupiahPlace);

            ctrDebit++;

            tabPK.TabPages[1].Controls.Add(tmpDebitText);

            tabPK.TabPages[1].Controls.Add(tmpDebitCombo);

            tabPK.TabPages[1].Controls.Add(tmpDebitNumeric);

            tabPK.TabPages[1].Controls.Add(rupiahPlace);

            buttonAddDebit.Location = new Point(temp.X, temp.Y + 150);
        }

        private void DataGridViewNeraca_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void VScrollBarFlow_Scroll(object sender, ScrollEventArgs e)
        {
            int dy = -(e.NewValue - e.OldValue)*5;
            for (int i = 0; i < ctrDebit; i++)
            {
                simpanKombo["Debit" + i.ToString()].Location = new Point(simpanKombo["Debit" + i.ToString()].Location.X, simpanKombo["Debit" + i.ToString()].Location.Y + dy);
                simpanLabel["Debit" + i.ToString()].Location = new Point(simpanLabel["Debit" + i.ToString()].Location.X, simpanLabel["Debit" + i.ToString()].Location.Y + dy);
                simpanNumer["Debit" + i.ToString()].Location = new Point(simpanNumer["Debit" + i.ToString()].Location.X, simpanNumer["Debit" + i.ToString()].Location.Y + dy);
                simpanText["Debit" + i.ToString()].Location = new Point(simpanText["Debit" + i.ToString()].Location.X, simpanText["Debit" + i.ToString()].Location.Y + dy);
            }
            for (int i = 0; i < ctrKredit; i++)
            {
                simpanKombo["Kredit" + i.ToString()].Location = new Point(simpanKombo["Kredit" + i.ToString()].Location.X, simpanKombo["Kredit" + i.ToString()].Location.Y + dy);
                simpanLabel["Kredit" + i.ToString()].Location = new Point(simpanLabel["Kredit" + i.ToString()].Location.X, simpanLabel["Kredit" + i.ToString()].Location.Y + dy);
                simpanNumer["Kredit" + i.ToString()].Location = new Point(simpanNumer["Kredit" + i.ToString()].Location.X, simpanNumer["Kredit" + i.ToString()].Location.Y + dy);
                simpanText["Kredit" + i.ToString()].Location = new Point(simpanText["Kredit" + i.ToString()].Location.X, simpanText["Kredit" + i.ToString()].Location.Y + dy);
            }
            buttonAddDebit.Location = new Point(buttonAddDebit.Location.X, buttonAddDebit.Location.Y + dy);
            buttonAddKredit.Location = new Point(buttonAddKredit.Location.X, buttonAddKredit.Location.Y + dy);
        }

        private void ComboBoxBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ComboBoxTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ButtonLihat_Click(object sender, EventArgs e)
        {
            updateTrialBalance();
        }

        private void TabPageFlow_Click(object sender, EventArgs e)
        {

        }

        private void CheckBoxProject_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxProject.Visible = checkBoxProject.Checked;
        }

        private void ButtonAddProject_Click(object sender, EventArgs e)
        {
            (this.MdiParent as FormParent).panggilAddProjectACC();
        }

        private void ButtonJUTanggal_Click(object sender, EventArgs e)
        {
            groupBoxJUTanggal.Visible = true;
            groupBoxJUKode.Visible = false;
        }

        private void ButtonJUUmum_Click(object sender, EventArgs e)
        {
            groupBoxJUTanggal.Visible = false;
            groupBoxJUKode.Visible = true;

        }

        private void ButtonJKTanggal_Click(object sender, EventArgs e)
        {
            groupBoxJKTanggal.Visible = true;
            groupBoxJKKode.Visible = false;
        }

        private void ButtonJKKode_Click(object sender, EventArgs e)
        {

            groupBoxJKTanggal.Visible = false;
            groupBoxJKKode.Visible = true;
        }

        private void ButtonJurnalKas_Click(object sender, EventArgs e)
        {
            groupBoxJK.Visible = true;
            groupBoxJU.Visible = false;
            groupBoxLB.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            groupBoxJK.Visible = false;
            groupBoxJU.Visible = true;
            groupBoxLB.Visible = false;
        }

        private void ButtonLBTahunan_Click(object sender, EventArgs e)
        {
            groupBoxLBProject.Visible = false;
            groupBoxLBTahunan.Visible = true;
        }

        private void ButtonLBProject_Click(object sender, EventArgs e)
        {

            groupBoxLBProject.Visible = true;
            groupBoxLBTahunan.Visible = false;
        }

        private void ButtonLabaRugi_Click(object sender, EventArgs e)
        {

            groupBoxJK.Visible = false;
            groupBoxJU.Visible = false;
            groupBoxLB.Visible = true;
        }

        private void ButtonCetakJUKode_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportJurnalAcc cr = new CrystalReportJurnalAcc();
                cr.SetParameterValue("kode", comboBoxJUKode.Text);
                ((FormParent)this.MdiParent).panggilTampilJurnal(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonCetakJUTanggal_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterSL = new MySqlDataAdapter("SELECT kode FROM historyflowacc WHERE tanggal >= '" + dateTimePickerJUStart.Value.ToString("yyyy-MM-dd") + "' and tanggal <= '" + dateTimePickerJUStop.Value.ToString("yyyy-MM-dd") + "' and kode not in (SELECT DISTINCT kode from historyflowacc where kodeakun like '1.0%') order by kode", ClassConnection.Instance().Connection);
                DataSet datasetSL = new DataSet();
                adapterSL.Fill(datasetSL);
                CrystalReportJurnalAcc cr = new CrystalReportJurnalAcc();
                string lempar = "";
                foreach (DataRow row in datasetSL.Tables[0].Rows)
                {
                    lempar += row[0].ToString() + ",";
                }
                cr.SetParameterValue("kode", lempar);
                ((FormParent)this.MdiParent).panggilTampilJurnal(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonCetakJKTanggal_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterSL = new MySqlDataAdapter("SELECT kode FROM historyflowacc WHERE tanggal >= '" + dateTimePickerJKStart.Value.ToString("yyyy-MM-dd") + "' and tanggal <= '" + dateTimePickerJKStop.Value.ToString("yyyy-MM-dd") + "' and kodeakun like '1.0%' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetSL = new DataSet();
                adapterSL.Fill(datasetSL);
                CrystalReportJurnalAcc cr = new CrystalReportJurnalAcc();
                string lempar = "";
                foreach (DataRow row in datasetSL.Tables[0].Rows)
                {
                    lempar += row[0].ToString() + ",";
                }
                cr.SetParameterValue("kode", lempar);
                ((FormParent)this.MdiParent).panggilTampilJurnal(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonCetakJKKode_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportJurnalAcc cr = new CrystalReportJurnalAcc();
                cr.SetParameterValue("kode", comboBoxJKKode.Text);
                MessageBox.Show(comboBoxJKKode.Text);
                ((FormParent)this.MdiParent).panggilTampilJurnal(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportLPK cr = new CrystalReportLPK();
                ((FormParent)this.MdiParent).panggilTampilLPK(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonCetakLBTahunan_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportJurnalLabaRugiTahun cr = new CrystalReportJurnalLabaRugiTahun();
                cr.SetParameterValue("kode", comboBoxLBTahunan.SelectedValue.ToString());
                ((FormParent)this.MdiParent).panggilTampilJurnalLBTahun(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonCetakLBProject_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportJurnalLabaRugi cr = new CrystalReportJurnalLabaRugi();
                cr.SetParameterValue("kode", comboBoxLBProject.SelectedValue);
                //MessageBox.Show(comboBoxLBProject.SelectedValue.ToString());
                ((FormParent)this.MdiParent).panggilTampilJurnalLB(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataTable tableAkun = new DataTable();
            DataTable tmp = new DataTable();
            dataGridLB.DataSource = null;
            dataGridLB.Columns.Clear();
            int level = 0;
            DataRow r = tmp.NewRow();
            tmp.Columns.Add("Nama");
            tmp.Columns.Add("Nominal");
            long totalTmp = 0;
            try
            {
                if (ClassConnection.Instance().Connecting())
                {

                    MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT hf.kodeakun,SUM(hf.nominal) AS nominal FROM flowprojectacc fp,historyflowacc hf WHERE fp.kode_project = '" + comboLB.SelectedValue.ToString() + "' AND hf.kode = fp.kode_flow AND hf.kodeakun LIKE '4.0.%'GROUP BY hf.kodeakun; ", ClassConnection.Instance().Connection);
                    tableAkun = new DataTable();
                    adapterKlien.Fill(tableAkun);
                    foreach (DataRow row in tableAkun.Rows)
                    {
                        r = tmp.NewRow();
                        r[0] = row[0].ToString();
                        long nom = Convert.ToInt64(row[1].ToString());
                        r[1] = "Rp." + nom.ToString("N");
                        tmp.Rows.Add(r);
                        totalTmp = totalTmp + Convert.ToInt64(row[1].ToString());
                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            long totalTmp1 = 0;

            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT hf.kodeakun,SUM(hf.nominal) AS nominal FROM flowprojectacc fp,historyflowacc hf WHERE fp.kode_project = '"+  comboLB.SelectedValue.ToString()  +"' AND hf.kode = fp.kode_flow AND hf.kodeakun LIKE '5.0.1.1.%'GROUP BY hf.kodeakun; ", ClassConnection.Instance().Connection);
                    tableAkun = new DataTable();
                    adapterKlien.Fill(tableAkun);
                    foreach (DataRow row in tableAkun.Rows)
                    {
                        r = tmp.NewRow();
                        r[0] = "(-)" + row[0].ToString();
                        long nom = Convert.ToInt64(row[1].ToString());
                        r[1] = "Rp." + nom.ToString("N");
                        tmp.Rows.Add(r);
                        totalTmp1 = totalTmp1 + Convert.ToInt64(row[1].ToString());
                        
                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            totalTmp = totalTmp - totalTmp1;
            r = tmp.NewRow();
            r[0] = "LABA KOTOR";
            r[tmp.Columns.Count - 1] = "Rp." + totalTmp.ToString("N");
            tmp.Rows.Add(r);
            totalTmp1 = 0;
            try
            {
                if (ClassConnection.Instance().Connecting())
                {

                    MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT hf.kodeakun,SUM(hf.nominal) AS nominal FROM flowprojectacc fp,historyflowacc hf WHERE fp.kode_project = '" + comboLB.SelectedValue.ToString() + "' AND hf.kode = fp.kode_flow AND hf.kodeakun LIKE '5.0.1.2.%'GROUP BY hf.kodeakun; ", ClassConnection.Instance().Connection);

                    
                    tableAkun = new DataTable();
                    adapterKlien.Fill(tableAkun);
                    foreach (DataRow row in tableAkun.Rows)
                    {
                        r = tmp.NewRow();
                        r[0] = "(-)" + row[0].ToString();
                        long nom = Convert.ToInt64(row[1].ToString());
                        r[1] = "Rp." + nom.ToString("N");
                        tmp.Rows.Add(r);
                        totalTmp1 = totalTmp1 + Convert.ToInt64(row[1].ToString());
                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            totalTmp = totalTmp - totalTmp1;
            r = tmp.NewRow();
            r[0] = "Laba Bersih";
            r[tmp.Columns.Count - 1] = "Rp." + totalTmp.ToString("N");
            tmp.Rows.Add(r);
            dataGridLB.DataSource = tmp;

            dataGridLB.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for (int i = 0; i < dataGridLB.ColumnCount; i++)
            {
                dataGridLB.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridLB.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            for(int i = 0; i < dataGridLB.RowCount-1; i++)
            {
                if(dataGridLB.Rows[i].Cells[0].Value.ToString().Contains("LABA KOTOR"))
                {
                    dataGridLB.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
                if (dataGridLB.Rows[i].Cells[0].Value.ToString().Contains("Laba Bersih"))
                {
                    dataGridLB.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }


            }
        }

        public void updateComboLB()
        {
            try
            {
                MySqlDataAdapter adapterCA = new MySqlDataAdapter("SELECT nama,kode from dataprojectacc where YEAR(DATE_ADD(tanggal,INTERVAL IF(lanjut = 1,1,0) YEAR))>=YEAR(NOW())", ClassConnection.Instance().Connection);
                DataSet datasetCA = new DataSet();
                adapterCA.Fill(datasetCA);
                comboLB.DisplayMember = "nama";
                comboLB.ValueMember = "kode";
                comboLB.DataSource = datasetCA.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            DataTable tableAkun = new DataTable();
            DataTable tmp = new DataTable();
            dataGridLB.DataSource = null;
            dataGridLB.Columns.Clear();
            int level = 0;
            DataRow r = tmp.NewRow();
            tmp.Columns.Add("Parent");
            tmp.Columns.Add("Nama");
            tmp.Columns.Add("Nominal");
            long totalTmp = 0;

            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    r = tmp.NewRow();
                    r[0] = "4.0 Pendapatan";
                    tmp.Rows.Add(r);
                    MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT hf.kodeakun, SUM(hf.nominal) AS nominal FROM historyflowacc hf WHERE YEAR(hf.tanggal) = " + comboBox1.SelectedValue.ToString() + " AND hf.kodeakun LIKE '4.0%'GROUP BY hf.kodeakun", ClassConnection.Instance().Connection);
                    tableAkun = new DataTable();
                    adapterKlien.Fill(tableAkun);
                    foreach (DataRow row in tableAkun.Rows)
                    {
                        r = tmp.NewRow();
                        r[1] = row[0].ToString();
                        r[2] = "Rp." + row[1].ToString();
                        tmp.Rows.Add(r);
                        totalTmp = totalTmp + Convert.ToInt64(row[1].ToString());
                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            long totalTmp1 = 0;

            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    r = tmp.NewRow();
                    r[0] = "5.0 Beban Project";
                    tmp.Rows.Add(r);
                    MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT hf.kodeakun,SUM(hf.nominal) AS nominal FROM historyflowacc hf WHERE YEAR(hf.tanggal)="+ comboBox1.SelectedValue.ToString() + " AND hf.kodeakun LIKE '5.0.1.%'GROUP BY hf.kodeakun; ", ClassConnection.Instance().Connection);
                    tableAkun = new DataTable();
                    adapterKlien.Fill(tableAkun);
                    foreach (DataRow row in tableAkun.Rows)
                    {
                        r = tmp.NewRow();
                        r[1] = "(-)" + row[0].ToString();
                        r[2] = "Rp." + row[1].ToString();
                        tmp.Rows.Add(r);
                        totalTmp1 = totalTmp1 + Convert.ToInt64(row[1].ToString());

                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            totalTmp = totalTmp - totalTmp1;
            totalTmp1 = 0;
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT hf.kodeakun,SUM(hf.nominal) AS nominal FROM historyflowacc hf WHERE YEAR(hf.tanggal)=" + comboBox1.SelectedValue.ToString() + " AND hf.kodeakun LIKE '5.0.2.%'GROUP BY hf.kodeakun; ", ClassConnection.Instance().Connection);
                    tableAkun = new DataTable();
                    adapterKlien.Fill(tableAkun);
                    foreach (DataRow row in tableAkun.Rows)
                    {
                        r = tmp.NewRow();
                        r[1] = "(-)" + row[0].ToString();
                        r[2] = "Rp." + row[1].ToString();
                        tmp.Rows.Add(r);
                        totalTmp1 = totalTmp1 + Convert.ToInt64(row[1].ToString());

                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            totalTmp = totalTmp - totalTmp1;
            r = tmp.NewRow();
            r[0] = "LABA KOTOR";
            r[tmp.Columns.Count - 1] = "Rp." + totalTmp.ToString("N");
            tmp.Rows.Add(r);
            totalTmp1 = 0;
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    r = tmp.NewRow();
                    r[0] = "5.0.3 Beban Operasional";
                    tmp.Rows.Add(r);
                    MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT hf.kodeakun,SUM(hf.nominal) AS nominal FROM historyflowacc hf WHERE YEAR(hf.tanggal)="+ comboBox1.SelectedValue.ToString() + " AND hf.kodeakun LIKE '5.0.3.%'GROUP BY hf.kodeakun; ", ClassConnection.Instance().Connection);
                    tableAkun = new DataTable();
                    adapterKlien.Fill(tableAkun);
                    foreach (DataRow row in tableAkun.Rows)
                    {
                        r = tmp.NewRow();
                        r[1] = "(-)" + row[0].ToString();
                        r[2] = "Rp." + row[1].ToString();
                        tmp.Rows.Add(r);
                        totalTmp1 = totalTmp1 + Convert.ToInt64(row[1].ToString());
                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            totalTmp = totalTmp - totalTmp1;
            r = tmp.NewRow();
            r[0] = "Laba Bersih";
            r[tmp.Columns.Count - 1] = "Rp." + totalTmp.ToString("N");
            tmp.Rows.Add(r);
            dataGridLB.DataSource = tmp;

            dataGridLB.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for (int i = 0; i < dataGridLB.ColumnCount; i++)
            {
                dataGridLB.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridLB.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            for (int i = 0; i < dataGridLB.RowCount - 1; i++)
            {
                if (dataGridLB.Rows[i].Cells[0].Value.ToString().Contains("LABA KOTOR"))
                {
                    dataGridLB.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
                if (dataGridLB.Rows[i].Cells[0].Value.ToString().Contains("Laba Bersih"))
                {
                    dataGridLB.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }


            }
        }
    }
}
