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
using System.Globalization;

namespace APC_EO_Finance
{
    public partial class FormAP : Form
    {
        public FormAP()
        {
            InitializeComponent();
        }
        DataSet datasetVendor;
        public bool jakartatidak = true;
        public void updateDGVVendor(string cmd = "SELECT Kode,Nama,Alamat,Notelp,Kota,Jenisvendor as 'Jenis Vendor' FROM datavendor order by kode")
        {
            try
            {
                datasetVendor = new DataSet();
                dataGridViewTampilKlien.DataSource = null;
                dataGridViewTampilKlien.Columns.Clear();
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);

                adapterKlien.Fill(datasetVendor);
                dataGridViewTampilKlien.DataSource = datasetVendor.Tables[0];
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
        public void updateDGVItem(string kodeitem = "")
        {
            string cmd = "SELECT Kode,namabarang as 'Nama Barang',Satuan,concat('Rp. ',format(hargasatuan,2)) as 'Harga Satuan',Jenis FROM dataitemvendor where kodevendor ='" + comboBoxVendor.SelectedValue.ToString() + "' and namabarang like '%"+kodeitem+ "%' order by kode ";
            try
            {
                datasetVendor = new DataSet();
                dataGridViewItem.DataSource = null;
                dataGridViewItem.Columns.Clear();
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);

                adapterKlien.Fill(datasetVendor);
                dataGridViewItem.DataSource = datasetVendor.Tables[0];
                DataGridViewButtonColumn edit = new DataGridViewButtonColumn();
                edit.HeaderText = "Edit";
                edit.Text = "Edit";
                edit.Name = "Edit";
                edit.UseColumnTextForButtonValue = true;
                dataGridViewItem.Columns.Add(edit);

                DataGridViewButtonColumn view = new DataGridViewButtonColumn();
                view.HeaderText = "View";
                view.Text = "View";
                view.Name = "View";
                view.UseColumnTextForButtonValue = true;
                dataGridViewItem.Columns.Add(view);

                DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
                delete.HeaderText = "Delete";
                delete.Text = "Delete";
                delete.Name = "Delete";
                delete.UseColumnTextForButtonValue = true;
                dataGridViewItem.Columns.Add(delete);

                dataGridViewItem.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewItem.ColumnCount; i++)
                {
                    dataGridViewItem.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void updateDGVPrVd()
        {
            string cmd = "SELECT ip.Kode,iv.namabarang as 'Nama Barang',concat('Rp. ',format(ip.hargasatuan,2)) as 'Harga Satuan',concat(cast(format(ip.jumlah,2) as char),' ',iv.satuan) as Jumlah,concat('Rp. ',format(ip.subtotal,2)) as Subtotal,v.Nama as 'Nama Vendor' FROM dataitemvendor iv, dataitemproject ip,datavendor v where v.kode = iv.kodevendor and ip.kodebarang = iv.kode and kodeproject ='" + comboBoxPr.SelectedValue.ToString() + "' order by kode ";
            try
            {
                datasetVendor = new DataSet();
                dataGridViewPrVd.DataSource = null;
                dataGridViewPrVd.Columns.Clear();
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);

                adapterKlien.Fill(datasetVendor);
                dataGridViewPrVd.DataSource = datasetVendor.Tables[0];

                DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
                delete.HeaderText = "Delete";
                delete.Text = "Delete";
                delete.Name = "Delete";
                delete.UseColumnTextForButtonValue = true;
                dataGridViewPrVd.Columns.Add(delete);

                dataGridViewPrVd.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewPrVd.ColumnCount; i++)
                {
                    dataGridViewPrVd.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                adapterKlien = new MySqlDataAdapter("SELECT SUM(subtotal) FROM dataitemproject WHERE kodeproject = '" + comboBoxPr.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
                DataTable tb = new DataTable();
                adapterKlien.Fill(tb);
                if (tb.Rows[0][0].ToString() != "")
                {
                    long x = Convert.ToInt64(tb.Rows[0][0].ToString());
                    numericUpDownTotal.Value = x;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        public void updateComboPrVd()
        {
            try
            {
                MySqlDataAdapter adapterPJ = new MySqlDataAdapter("SELECT kode,nama FROM dataproject where kode like 'PR%' and status = 0 order by kode", ClassConnection.Instance().Connection);
                DataSet datasetPJ = new DataSet();
                adapterPJ.Fill(datasetPJ);
                comboBoxPr.DisplayMember = "nama";
                comboBoxPr.ValueMember = "kode";
                comboBoxPr.DataSource = datasetPJ.Tables[0];
                adapterPJ = new MySqlDataAdapter("SELECT kode,nama FROM datavendor where jenisvendor='"+comboBoxTipeVd.Text+"' order by kode", ClassConnection.Instance().Connection);
                datasetPJ = new DataSet();
                adapterPJ.Fill(datasetPJ);
                comboBoxVd.DisplayMember = "nama";
                comboBoxVd.ValueMember = "kode";
                comboBoxVd.DataSource = datasetPJ.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error Combo Project Vendor");
            }
        }
        public void updateComboVendor()
        {
            try
            {
                MySqlDataAdapter adapterPJ = new MySqlDataAdapter("SELECT kode,nama FROM datavendor where jenisvendor ='"+comboBoxTipeVendorItem.Text+"' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetPJ = new DataSet();
                adapterPJ.Fill(datasetPJ);
                comboBoxVendor.DisplayMember = "nama";
                comboBoxVendor.ValueMember = "kode";
                comboBoxVendor.DataSource = datasetPJ.Tables[0];
                updateDGVItem();
                groupBox1.Text = comboBoxVendor.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ButtonKlien_Click(object sender, EventArgs e)
        {
            ((FormParent)this.MdiParent).panggilVendor(1);
        }

        private void FormAP_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            updateDGVVendor();
        }
        bool awal = true;
        private void DataGridViewTampilKlien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (awal)
            {
                if (dataGridViewTampilKlien.Columns[e.ColumnIndex].Name == "Edit")
                {
                    ((FormParent)this.MdiParent).panggilVendor(2, datasetVendor.Tables[0].Rows[e.RowIndex][0].ToString());
                }
                else if (dataGridViewTampilKlien.Columns[e.ColumnIndex].Name == "View")
                {
                    ((FormParent)this.MdiParent).panggilVendor(3, datasetVendor.Tables[0].Rows[e.RowIndex][0].ToString());
                }
                else if (dataGridViewTampilKlien.Columns[e.ColumnIndex].Name == "Delete")
                {
                    if (MessageBox.Show("Apakah anda yakin ingin mendelete " + datasetVendor.Tables[0].Rows[e.RowIndex][1].ToString() + " ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                string commandText = "DELETE FROM datavendor WHERE kode = @kode";
                                MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", datasetVendor.Tables[0].Rows[e.RowIndex][0].ToString());
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
                    ((FormParent)this.MdiParent).panggilVendor(2, dataGridViewTampilKlien.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                else if (dataGridViewTampilKlien.Columns[e.ColumnIndex].Name == "View")
                {
                    ((FormParent)this.MdiParent).panggilVendor(3, dataGridViewTampilKlien.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
                else if (dataGridViewTampilKlien.Columns[e.ColumnIndex].Name == "Delete")
                {
                    if (MessageBox.Show("Apakah anda yakin ingin mendelete " + dataGridViewTampilKlien.Rows[e.RowIndex].Cells[1].Value.ToString() + " ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                string commandText = "DELETE FROM datavendor WHERE kode = @kode";
                                MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", dataGridViewTampilKlien.Rows[e.RowIndex].Cells[0].Value.ToString());
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
            updateDGVVendor();
        }

        private void ComboBoxVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Text = comboBoxVendor.Text;
            updateDGVItem();
        }

        private void TabControlFull_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlFull.SelectedIndex == 0)
            {
                updateDGVVendor();
            }
            else if (tabControlFull.SelectedIndex == 1)
            {
                comboBoxTipeVendorItem.Items.Add("Produksi");
                comboBoxTipeVendorItem.Items.Add("Show Management");
                comboBoxTipeVendorItem.Items.Add("Digital/IT");
                comboBoxTipeVendorItem.Items.Add("Merchandise");
                comboBoxTipeVendorItem.Items.Add("Cetak");
                comboBoxTipeVendorItem.Items.Add("Team/Tukang");
                comboBoxTipeVendorItem.Items.Add("Transportasi");
                comboBoxTipeVendorItem.Items.Add("Venue");
                comboBoxTipeVendorItem.Items.Add("Permit");
                comboBoxTipeVendorItem.Items.Add("Property");
                comboBoxTipeVendorItem.Items.Add("Konsumsi");
                comboBoxTipeVendorItem.Items.Add("Talent");
                comboBoxTipeVendorItem.SelectedIndex = 0;
                updateComboVendor();
            }
            else if (tabControlFull.SelectedIndex == 2)
            {
                updateComboPrVd();
                comboBoxTipeVd.Items.Add("Produksi");
                comboBoxTipeVd.Items.Add("Show Management");
                comboBoxTipeVd.Items.Add("Digital/IT");
                comboBoxTipeVd.Items.Add("Merchandise");
                comboBoxTipeVd.Items.Add("Cetak");
                comboBoxTipeVd.Items.Add("Team/Tukang");
                comboBoxTipeVd.Items.Add("Transportasi");
                comboBoxTipeVd.Items.Add("Venue");
                comboBoxTipeVd.Items.Add("Permit");
                comboBoxTipeVd.Items.Add("Property");
                comboBoxTipeVd.Items.Add("Konsumsi");
                comboBoxTipeVd.Items.Add("Talent");
                comboBoxTipeVd.SelectedIndex = 0;
            }
            else if (tabControlFull.SelectedIndex == 3)
            {
                updateDGVJatuhTempo();
            }
            else if (tabControlFull.SelectedIndex == 4)
            {
                updateNotifJatuhTempo();
            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            updateDGVVendor("SELECT Kode,Nama,Alamat,Notelp,Kota,Jenisvendor as 'Jenis Vendor' from datavendor where nama like '%" + textBoxSearchVendor.Text + "%'order by kode");
        }

        private void ButtonSearchItem_Click(object sender, EventArgs e)
        {
            updateDGVItem(textBoxSearchItem.Text);

        }

        private void ButtonNewItem_Click(object sender, EventArgs e)
        {
            ((FormParent)this.MdiParent).panggilItem(1,"", comboBoxVendor.SelectedValue.ToString(), comboBoxVendor.Text);
        }

        private void DataGridViewItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewItem.Columns[e.ColumnIndex].Name == "Edit")
            {
                ((FormParent)this.MdiParent).panggilItem(2, dataGridViewItem.Rows[e.RowIndex].Cells[0].Value.ToString(), comboBoxVendor.SelectedValue.ToString(), comboBoxVendor.Text);
            }
            else if (dataGridViewItem.Columns[e.ColumnIndex].Name == "View")
            {
                ((FormParent)this.MdiParent).panggilItem(3, dataGridViewItem.Rows[e.RowIndex].Cells[0].Value.ToString(), comboBoxVendor.SelectedValue.ToString(), comboBoxVendor.Text);
            }
            else if (dataGridViewItem.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Apakah anda yakin ingin mendelete " + dataGridViewItem.Rows[e.RowIndex].Cells[1].Value.ToString() + " ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "DELETE FROM dataitemvendor WHERE kode = @kode";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", dataGridViewItem.Rows[e.RowIndex].Cells[0].Value.ToString());
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
            updateDGVItem();
        }

        private void ComboBoxPr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPr.Items.Count != 0)
            {
                buttonAdd.Enabled = true;
                buttonClear.Enabled = true;
                updateDGVPrVd();
            }
            else
            {
                MessageBox.Show("Tidak Ada Project yang dipilih","Error");
                buttonAdd.Enabled = false;
                buttonClear.Enabled = false;
            }
        }

        private void ComboBoxVd_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBoxItem.Text = comboBoxVd.Text;
            if (comboBoxPr.Items.Count !=0)
            {
                buttonAdd.Enabled = true;
                buttonClear.Enabled = true;
                updateComboItemPrVd();
            }
            else
            {
                MessageBox.Show("Tidak Ada Project yang dipilih","Error");
                buttonAdd.Enabled = false;
                buttonClear.Enabled = false;
            }
        }

        private void ComboBoxItemVd_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateDataDalam(comboBoxItemVd.SelectedValue.ToString());
        }
        public void updateComboItemPrVd()
        {
            try
            {
                MySqlDataAdapter adapterPJ = new MySqlDataAdapter("SELECT kode,namabarang FROM dataitemvendor where kodevendor = '" + comboBoxVd.SelectedValue.ToString() + "' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetPJ = new DataSet();
                adapterPJ.Fill(datasetPJ);
                comboBoxItemVd.DisplayMember = "namabarang";
                comboBoxItemVd.ValueMember = "kode";
                comboBoxItemVd.DataSource = datasetPJ.Tables[0];
                updateDGVPrVd();
                groupBox1.Text = comboBoxVendor.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void updateDataDalam(string kode)
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT i.satuan, i.hargasatuan FROM dataitemvendor i where kode ='" + kode + "'", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    //reader = cmd.ExecuteReader();
                    reader.Fill(table);

                    labelSatuan.Text = table.Rows[0][0].ToString();
                    numericUpDownHarga.Value= Convert.ToInt32(table.Rows[0][1].ToString());

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (numericUpDownJumlah.Value != 0)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "INSERT INTO dataitemproject VALUES(null,@kodebarang,@jumlah,@hargasatuan,@subtotal,@tanggal,@kodeproject,null,null)";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kodebarang", comboBoxItemVd.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@jumlah", Convert.ToInt32(numericUpDownJumlah.Value));
                            cmd.Parameters.AddWithValue("@hargasatuan", Convert.ToInt32(numericUpDownHarga.Value));
                            cmd.Parameters.AddWithValue("@subtotal", numericUpDownSubtotal.Value);
                            cmd.Parameters.AddWithValue("@tanggal", dateTimePickerTagihan.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@kodeproject", comboBoxPr.SelectedValue.ToString());
                            int rowsAffected = cmd.ExecuteNonQuery();

                            cmd = new MySqlCommand("SELECT count(*) from datavendorproject where kodevendor =@kodevendor and kodeproject =@kodeproject", ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kodevendor", comboBoxVd.SelectedValue);
                            cmd.Parameters.AddWithValue("@kodeproject", comboBoxPr.SelectedValue);
                            int hasilCount = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                            if (hasilCount <=0)
                            {
                                commandText = "INSERT INTO datavendorproject VALUES(null,@kodevendor,@kodeproject,@total,0,@tanggal,null,null)";
                                cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kodevendor", comboBoxVd.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@kodeproject", comboBoxPr.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@total", numericUpDownSubtotal.Value);
                                cmd.Parameters.AddWithValue("@tanggal", dateTimePickerTagihan.Value.ToString("yyyy-MM-dd"));
                                rowsAffected = cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                commandText = "UPDATE datavendorproject set total = total +@subtotal, jatuhtempo = @tanggal where kodevendor = @kodevendor and kodeproject = @kodeproject";
                                cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kodevendor", comboBoxVd.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@kodeproject", comboBoxPr.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@subtotal", numericUpDownSubtotal.Value);
                                cmd.Parameters.AddWithValue("@tanggal", dateTimePickerTagihan.Value.ToString("yyyy-MM-dd"));
                                rowsAffected = cmd.ExecuteNonQuery();
                            }


                            MessageBox.Show("Data telah Tersimpan", "Berhasil");
                            ClassConnection.Instance().Close();
                            updateDGVPrVd();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }

                }
                else
                {
                    MessageBox.Show("Jumlah tidak boleh 0");
                }
            }
        }

        private void NumericUpDownJumlah_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownSubtotal.Value = numericUpDownJumlah.Value * numericUpDownHarga.Value;
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            numericUpDownJumlah.Value = 0;
        }

        private void DataGridViewPrVd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewPrVd.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Apakah anda yakin ingin mendelete " + dataGridViewPrVd.Rows[e.RowIndex].Cells[1].Value.ToString() + " ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {

                            string commandText = "select subtotal from dataitemproject where kode=@kode";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", Convert.ToInt32(dataGridViewPrVd.Rows[e.RowIndex].Cells[0].Value.ToString()));
                            int subtotal = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                            commandText = "SELECT vp.kodevendor FROM dataitemproject ip, dataitemvendor iv, datavendorproject vp WHERE ip.`kodebarang` = iv.`kode` AND iv.`kodevendor` = vp.`kodevendor` AND ip.`kode`=@kode";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", Convert.ToInt32(dataGridViewPrVd.Rows[e.RowIndex].Cells[0].Value.ToString()));
                            string kdvndr = cmd.ExecuteScalar().ToString();

                            commandText = "update datavendorproject set total = total - @subtotal WHERE kodevendor = @kodevendor and kodeproject = @kodeproject";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@subtotal", subtotal);
                            cmd.Parameters.AddWithValue("@kodevendor", kdvndr);
                            cmd.Parameters.AddWithValue("@kodeproject", comboBoxPr.SelectedValue.ToString());
                            int rowsAffected = cmd.ExecuteNonQuery();

                            numericUpDownTotal.Value -= subtotal;

                            commandText = "DELETE FROM dataitemproject WHERE kode = @kode";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", Convert.ToInt32(dataGridViewPrVd.Rows[e.RowIndex].Cells[0].Value.ToString()));
                            rowsAffected = cmd.ExecuteNonQuery();

                            commandText = "DELETE FROM datavendorproject WHERE total = 0";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", Convert.ToInt32(dataGridViewPrVd.Rows[e.RowIndex].Cells[0].Value.ToString()));
                            rowsAffected = cmd.ExecuteNonQuery();

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
            updateDGVPrVd();
        }

        private void ButtonSearchJatuhTempo_Click(object sender, EventArgs e)
        {
            updateDGVJatuhTempo(textBoxSearchJatuhTempo.Text);
        }
        public void updateNotifJatuhTempo()
        {

            try
            {
                DataTable tmp = new DataTable();
                DataTable tampil = new DataTable();

                tampil.Columns.Add("Tentang");
                tampil.Columns.Add("Kode");
                tampil.Columns.Add("Notifikasi");

                dataGridViewNotif.DataSource = null;
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT v.nama, FORMAT((vp.total-vp.sudahdibayar),2), p.nama, DATE_FORMAT(vp.`jatuhtempo`,'%d-%m-%Y') FROM datavendorproject vp, datavendor v, dataproject p WHERE vp.`kodeproject` = p.kode AND vp.kodevendor = v.kode AND (vp.total-vp.sudahdibayar) > 0 GROUP BY v.nama ORDER BY vp.jatuhtempo", ClassConnection.Instance().Connection);

                if (jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT v.nama, FORMAT((vp.total-vp.sudahdibayar),2), p.nama, DATE_FORMAT(vp.`jatuhtempo`,'%d-%m-%Y') FROM datavendorproject vp, datavendor v, dataproject p WHERE vp.`kodeproject` like 'JPRJ%' and vp.`kodeproject` = p.kode AND vp.kodevendor = v.kode AND (vp.total-vp.sudahdibayar) > 0 GROUP BY v.nama ORDER BY vp.jatuhtempo", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT v.nama, FORMAT((vp.total-vp.sudahdibayar),2), p.nama, DATE_FORMAT(vp.`jatuhtempo`,'%d-%m-%Y') FROM datavendorproject vp, datavendor v, dataproject p WHERE vp.`kodeproject` like 'PRJ%' and vp.`kodeproject` = p.kode AND vp.kodevendor = v.kode AND (vp.total-vp.sudahdibayar) > 0 GROUP BY v.nama ORDER BY vp.jatuhtempo", ClassConnection.Instance().Connection);
                }

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

                dataGridViewNotif.DataSource = tampil;

                dataGridViewNotif.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewNotif.ColumnCount; i++)
                {
                    dataGridViewNotif.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridViewNotif.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }

                foreach (DataGridViewRow r in dataGridViewNotif.Rows)
                {
                    if (r.Cells[0].Value.ToString() == "Utang Vendor")
                    {
                        r.DefaultCellStyle.BackColor = Color.DarkRed;
                    }
                    else if (r.Cells[0].Value.ToString() == "Masa Tenggang Vendor")
                    {
                        r.DefaultCellStyle.BackColor = Color.IndianRed;
                    }
                    else if (r.Cells[0].Value.ToString() == "Jatuh Tempo Vendor")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Notif");
            }
        }

        public void update_area(string area)
        {
            if (area != "JAKARTA")
            {
                jakartatidak = false;
            }
        }

        private void TabPageJatuhTempo_Click(object sender, EventArgs e)
        {

        }

        private void ButtonPrintVendor_Click(object sender, EventArgs e)
        {
            CrystalReportPrintVendor cr = new CrystalReportPrintVendor();
            ((FormParent)this.MdiParent).panggilTampilVendor(cr);
        }

        private void NumericUpDownHarga_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownSubtotal.Value = numericUpDownJumlah.Value * numericUpDownHarga.Value;
        }

        private void ComboBoxTipeVendorItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateComboVendor();
        }

        private void ComboBoxTipeVd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterPJ;
                DataSet datasetPJ;
                adapterPJ = new MySqlDataAdapter("SELECT kode,nama FROM datavendor where jenisvendor='" + comboBoxTipeVd.Text + "' order by kode", ClassConnection.Instance().Connection);
                datasetPJ = new DataSet();
                adapterPJ.Fill(datasetPJ);
                comboBoxVd.DisplayMember = "nama";
                comboBoxVd.ValueMember = "kode";
                comboBoxVd.DataSource = datasetPJ.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Combo Tipe Vendor");
            }
        }

        private void ButtonCetakPO_Click(object sender, EventArgs e)
        {
            CrystalReportPOVendor cr = new CrystalReportPOVendor();
            cr.SetParameterValue("kode", comboBoxPr.SelectedValue.ToString());
            ((FormParent)this.MdiParent).panggilTampilPO(cr);
        }

        private void NumericUpDownTotal_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
