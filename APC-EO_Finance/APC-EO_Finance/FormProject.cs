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
    public partial class FormProject : Form
    {
        public FormProject()
        {
            InitializeComponent();
        }
        public int status = 1;
        DataTable tableklien;
        DataTable tableFaktur;
        private void FormProject_Load(object sender, EventArgs e)
        {
            tableFaktur = new DataTable();
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT kode,nama FROM datakaryawan where insertdata > str_to_date('2019-08-10','%Y-%m-%d') and (upper(jabatan) = 'AE' or upper(jabatan) = 'Account Executive') union select '-' as kode, 'Tidak Ada' as nama order by kode", ClassConnection.Instance().Connection);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                comboBoxAE.DisplayMember = "nama";
                comboBoxAE.ValueMember = "kode";
                comboBoxAE.DataSource = dataset.Tables[0];
                adapter = new MySqlDataAdapter("SELECT kode,nama FROM datakaryawan where insertdata > str_to_date('2019-08-10','%Y-%m-%d') and (upper(jabatan) = 'POT' or upper(jabatan) = 'Project Officer Teknis') union select '-' as kode, 'Tidak Ada' as nama order by kode", ClassConnection.Instance().Connection);
                dataset = new DataSet();
                adapter.Fill(dataset);
                comboBoxPOT.DisplayMember = "nama";
                comboBoxPOT.ValueMember = "kode";
                comboBoxPOT.DataSource = dataset.Tables[0];
                adapter = new MySqlDataAdapter("SELECT kode,nama FROM datakaryawan where insertdata > str_to_date('2019-08-10','%Y-%m-%d') and (upper(jabatan) = 'PONT' or upper(jabatan) = 'Project Officer Non Teknis') union select '-' as kode, 'Tidak Ada' as nama order by kode", ClassConnection.Instance().Connection);
                dataset = new DataSet();
                adapter.Fill(dataset);
                comboBoxPONT.DisplayMember = "nama";
                comboBoxPONT.ValueMember = "kode";
                comboBoxPONT.DataSource = dataset.Tables[0];
                adapter = new MySqlDataAdapter("SELECT kode,nama FROM dataklien order by kode", ClassConnection.Instance().Connection);
                dataset = new DataSet();
                adapter.Fill(dataset);
                comboBoxKlien.DisplayMember = "nama";
                comboBoxKlien.ValueMember = "kode";
                comboBoxKlien.DataSource = dataset.Tables[0];
                adapter = new MySqlDataAdapter("SELECT kode,bataspiutang FROM dataklien order by kode", ClassConnection.Instance().Connection);
                dataset = new DataSet();
                adapter.Fill(dataset);
                tableklien = dataset.Tables[0];
                adapter = new MySqlDataAdapter("SELECT faktur from dataproject", ClassConnection.Instance().Connection);
                adapter.Fill(tableFaktur);
                DataTable dataInvoice = new DataTable();
                adapter = new MySqlDataAdapter("SELECT LPAD(COUNT(*)+1,3,'0') FROM dataproject", ClassConnection.Instance().Connection);
                adapter.Fill(dataInvoice);
                textBoxInvoice.Text = dataInvoice.Rows[0][0].ToString() + "/INV-SBY/" + comboBoxKlien.Text.Substring(0, 3) + "/ACT/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (status == 1)
            {
                resetKodeProject();
            }
            else if(status == 3)
            {
                textBoxNamaProject.ReadOnly = true;
                textBoxInvoice.ReadOnly = true;
                textBoxFaktur.ReadOnly = true;
                textBoxTempatProject.ReadOnly = true;
                dateTimePickerEnd.Enabled = false;
                dateTimePickerStart.Enabled = false;
                dateTimePickerTagihan.Enabled = false;
                dateTimePickerDeadline.Enabled = false;
                numericUpDownBiayaProject.Enabled = false;
                comboBoxAE.Enabled = false;
                comboBoxPONT.Enabled = false;
                comboBoxPOT.Enabled = false;
                comboBoxKlien.Enabled = false;
            }
            checkBoxEnd.Enabled = false;
            checkBoxStart.Enabled = false;
            checkBoxInvoice.Enabled = false;
            checkBoxFinish.Enabled = false;
            labelStart.AutoSize = false;
            labelStart.Size = new Size(150, 30);
            labelStart.TextAlign = ContentAlignment.MiddleCenter;
            labelEnd.AutoSize = false;
            labelEnd.Size = new Size(150, 30);
            labelEnd.TextAlign = ContentAlignment.MiddleCenter;
            labelInvoice.AutoSize = false;
            labelInvoice.Size = new Size(150, 30);
            labelInvoice.TextAlign = ContentAlignment.MiddleCenter;
            labelFinish.AutoSize = false;
            labelFinish.Size = new Size(150, 30);
            labelFinish.TextAlign = ContentAlignment.MiddleCenter;
            labelPanahEnd.Visible = false;
            labelPanahFinish.Visible = false;
            labelPanahInvoice.Visible = false;
        }
        public void resetKodeProject()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "PRJ" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from dataproject where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(9, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxKodeProject.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void tampilData(string kode = "")
        {
            try
            {
                string tempKodeAe = "-";
                string tempKodePOT = "-";
                string tempKodePONT = "-";
                if (ClassConnection.Instance().Connecting())
                {

                    MySqlDataReader reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT p.kode,p.invoice,p.faktur,p.nama,p.tempat,date_format(p.mulai,'%d-%m-%Y') as 'mulai',date_format(p.akhir,'%d-%m-%Y') as 'akhir',date_format(p.tagihan,'%d-%m-%Y') as'tagihan',date_format(p.deadline,'%d-%m-%Y') as'deadline',p.totalbiaya,p.kode_ae as ae,p.kode_pot as po ,p.kode_pont as pont,kl.nama as 'klien' FROM dataproject p, dataklien kl where p.kode ='" + kode + "' and kl.kode = p.kode_klien", ClassConnection.Instance().Connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBoxKodeProject.Text = reader["kode"].ToString();
                        textBoxInvoice.Text = reader["invoice"].ToString();
                        textBoxFaktur.Text = reader["faktur"].ToString();
                        textBoxNamaProject.Text = reader["nama"].ToString();
                        textBoxTempatProject.Text = reader["tempat"].ToString();
                        dateTimePickerStart.Value = DateTime.ParseExact(reader["mulai"].ToString(), "dd-MM-yyyy", CultureInfo.GetCultureInfo("en-US"));
                        dateTimePickerEnd.Value = DateTime.ParseExact(reader["akhir"].ToString(), "dd-MM-yyyy", CultureInfo.GetCultureInfo("en-US"));
                        dateTimePickerTagihan.Value = DateTime.ParseExact(reader["tagihan"].ToString(), "dd-MM-yyyy", CultureInfo.GetCultureInfo("en-US"));
                        dateTimePickerDeadline.Value = DateTime.ParseExact(reader["deadline"].ToString(), "dd-MM-yyyy", CultureInfo.GetCultureInfo("en-US"));
                        numericUpDownBiayaProject.Value = Convert.ToInt32(reader["totalbiaya"].ToString());

                        comboBoxAE.SelectedIndex = comboBoxAE.FindStringExact("Tidak Ada");
                        comboBoxPOT.SelectedIndex = comboBoxPOT.FindStringExact("Tidak Ada");
                        comboBoxPONT.SelectedIndex = comboBoxPONT.FindStringExact("Tidak Ada");
                        comboBoxKlien.SelectedIndex = comboBoxKlien.FindStringExact(reader["klien"].ToString());

                        tempKodeAe = reader["ae"].ToString();
                        tempKodePOT = reader["po"].ToString();
                        tempKodePONT = reader["pont"].ToString();
                    }
                    ClassConnection.Instance().Close();
                }

                if (ClassConnection.Instance().Connecting())
                {
                    MySqlDataReader reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT biaya,ppn from ppnproject where kode_project = '" + kode + "'", ClassConnection.Instance().Connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        numericUpDownBiayaProjectSebelum.Value = Convert.ToInt32(reader["biaya"].ToString());
                        numericUpDownPPN.Value = Convert.ToInt32(reader["ppn"].ToString());

                    }

                    ClassConnection.Instance().Close();
                }
                if (ClassConnection.Instance().Connecting())
                {
                    if (tempKodeAe != "-")
                    {
                        MySqlCommand cmdAE = new MySqlCommand("SELECT nama from datakaryawan where kode = '" + tempKodeAe + "'", ClassConnection.Instance().Connection);
                        MySqlCommand cmdPOT = new MySqlCommand("SELECT nama from datakaryawan where kode = '" + tempKodePOT + "'", ClassConnection.Instance().Connection);
                        MySqlCommand cmdPONT = new MySqlCommand("SELECT nama from datakaryawan where kode = '" + tempKodePONT + "'", ClassConnection.Instance().Connection);
                        comboBoxAE.SelectedIndex = comboBoxAE.FindStringExact(cmdAE.ExecuteScalar().ToString());
                        comboBoxPOT.SelectedIndex = comboBoxPOT.FindStringExact(tempKodePOT == "-" ? "Tidak Ada" : cmdPOT.ExecuteScalar().ToString());
                        comboBoxPONT.SelectedIndex = comboBoxPONT.FindStringExact(tempKodePONT == "-" ? "Tidak Ada" : cmdPONT.ExecuteScalar().ToString());

                    }
                    ClassConnection.Instance().Close();
                }
                buttonHis.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ButtonClearKlien_Click(object sender, EventArgs e)
        {
            textBoxNamaProject.Text = "";
            textBoxTempatProject.Text = "";
            numericUpDownBiayaProject.Value = 0;
            comboBoxAE.SelectedIndex = 0;
            comboBoxPONT.SelectedIndex = 0;
            comboBoxKlien.SelectedIndex = 0;
        }

        private void ButtonCloseKlien_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSubmitKlien_Click(object sender, EventArgs e)
        {
            if (status == 1)
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    bool m = true;
                    Int64 simpan = 0;
                    foreach (DataRow r in tableklien.Rows)
                    {
                        if (r[0].ToString() == comboBoxKlien.SelectedValue.ToString())
                        {
                            if (Convert.ToInt32(r[1].ToString()) >= 5000000)
                            {
                                m = false;
                                simpan = Convert.ToInt64(r[1].ToString());
                            }
                            break;
                        }
                    }
                    if (!m)
                    {
                        if (MessageBox.Show("Apakah anda yakin berproject dengan klien tersebut? Piutang klien tersebut adalah Rp. "+ simpan.ToString(), "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            m = true;
                        }
                    }
                    if (m)
                    {
                        try
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                string commandText = "INSERT INTO dataproject VALUES(@kode,@invoice,@faktur,@nama,@tempat,@mulai,@akhir,@tagihan,@deadline,@totalbiaya,0,0,@kode_ae,@kode_pot,@kode_pont,@kode_klien,null,null)";
                                MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", textBoxKodeProject.Text);
                                cmd.Parameters.AddWithValue("@invoice", textBoxInvoice.Text);
                                cmd.Parameters.AddWithValue("@faktur", textBoxFaktur.Text);
                                cmd.Parameters.AddWithValue("@nama", textBoxNamaProject.Text);
                                cmd.Parameters.AddWithValue("@tempat", textBoxTempatProject.Text);
                                cmd.Parameters.AddWithValue("@mulai", dateTimePickerStart.Value.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@akhir", dateTimePickerEnd.Value.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@tagihan", dateTimePickerTagihan.Value.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@deadline", dateTimePickerDeadline.Value.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@totalbiaya", numericUpDownBiayaProject.Value);
                                cmd.Parameters.AddWithValue("@kode_ae", comboBoxAE.SelectedValue);
                                cmd.Parameters.AddWithValue("@kode_pot", comboBoxPOT.SelectedValue);
                                cmd.Parameters.AddWithValue("@kode_pont", comboBoxPONT.SelectedValue);
                                cmd.Parameters.AddWithValue("@kode_klien", comboBoxKlien.SelectedValue);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                commandText = "INSERT INTO ppnproject VALUES(null,@kodeproject,@biaya,@ppn,@total,null,null)";
                                cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kodeproject", textBoxKodeProject.Text);
                                cmd.Parameters.AddWithValue("@biaya", numericUpDownBiayaProjectSebelum.Value);
                                cmd.Parameters.AddWithValue("@ppn", numericUpDownPPN.Value);
                                cmd.Parameters.AddWithValue("@total", numericUpDownBiayaProject.Value);
                                rowsAffected = cmd.ExecuteNonQuery();

                                commandText = "INSERT INTO historybiayaproject VALUES(null,@kodeproject,@biaya,null,null)";
                                cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kodeproject", textBoxKodeProject.Text);
                                cmd.Parameters.AddWithValue("@biaya", numericUpDownBiayaProject.Value);
                                rowsAffected = cmd.ExecuteNonQuery();

                                MessageBox.Show("Data telah Tersimpan", "Berhasil");
                                ClassConnection.Instance().Close();
                                resetKodeProject();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error");
                        }
                    }
                }
            }
            else if (status == 2)
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "UPDATE dataproject SET nama = @nama,invoice=@invoice,faktur=@faktur,tempat = @tempat, mulai = @mulai,akhir = @akhir,tagihan = @tagihan,deadline=@deadline,totalbiaya = @totalbiaya,kode_ae = @kode_ae,kode_pot = @kode_pot,kode_pont=@kode_pont,kode_klien = @kode_klien WHERE kode = @kode";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKodeProject.Text);
                            cmd.Parameters.AddWithValue("@invoice", textBoxInvoice.Text);
                            cmd.Parameters.AddWithValue("@faktur", textBoxFaktur.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxNamaProject.Text);
                            cmd.Parameters.AddWithValue("@tempat", textBoxTempatProject.Text);
                            cmd.Parameters.AddWithValue("@mulai", dateTimePickerStart.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@akhir", dateTimePickerEnd.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@tagihan", dateTimePickerTagihan.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@deadline", dateTimePickerDeadline.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@totalbiaya", numericUpDownBiayaProject.Value);
                            cmd.Parameters.AddWithValue("@kode_ae", comboBoxAE.SelectedValue);
                            cmd.Parameters.AddWithValue("@kode_pot", comboBoxPOT.SelectedValue);
                            cmd.Parameters.AddWithValue("@kode_pont", comboBoxPONT.SelectedValue);
                            cmd.Parameters.AddWithValue("@kode_klien", comboBoxKlien.SelectedValue);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            commandText = "DELETE FROM ppnproject where kode_project = @kodeproject";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kodeproject", textBoxKodeProject.Text);
                            rowsAffected = cmd.ExecuteNonQuery();

                            commandText = "INSERT INTO ppnproject VALUES(null,@kodeproject,@biaya,@ppn,@total,null,null)";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kodeproject", textBoxKodeProject.Text);
                            cmd.Parameters.AddWithValue("@biaya", numericUpDownBiayaProjectSebelum.Value);
                            cmd.Parameters.AddWithValue("@ppn", numericUpDownPPN.Value);
                            cmd.Parameters.AddWithValue("@total", numericUpDownBiayaProject.Value);
                            rowsAffected = cmd.ExecuteNonQuery();

                            commandText = "INSERT INTO historybiayaproject VALUES(null,@kodeproject,@biaya,null,null)";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kodeproject", textBoxKodeProject.Text);
                            cmd.Parameters.AddWithValue("@biaya", numericUpDownBiayaProject.Value);
                            rowsAffected = cmd.ExecuteNonQuery();

                            MessageBox.Show("Data telah Tersimpan", "Berhasil");
                            ClassConnection.Instance().Close();
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
        }

        private void FormProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((FormParent)this.MdiParent).updateDGVProject();       }

        private void DateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            labelStart.Text = dateTimePickerStart.Value.ToString("dd-MM-yyyy");
            int daysDiff = ((TimeSpan)(dateTimePickerStart.Value - DateTime.Today)).Days;
            checkBoxStart.Checked = daysDiff <= 0;
            labelPanahEnd.Visible = daysDiff <= 0;
        }

        private void DateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {

            labelEnd.Text = dateTimePickerEnd.Value.ToString("dd-MM-yyyy");
            int daysDiff = ((TimeSpan)(dateTimePickerEnd.Value - DateTime.Today)).Days;
            checkBoxEnd.Checked = daysDiff <= 0;
            labelPanahInvoice.Visible = daysDiff <= 0;
        }

        private void DateTimePickerTagihan_ValueChanged(object sender, EventArgs e)
        {
            labelInvoice.Text = dateTimePickerTagihan.Value.ToString("dd-MM-yyyy");
            int daysDiff = ((TimeSpan)(dateTimePickerTagihan.Value - DateTime.Today)).Days;
            checkBoxInvoice.Checked = daysDiff <= 0;
            labelPanahFinish.Visible = daysDiff <= 0;
        }

        private void ButtonHis_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                MySqlDataAdapter adapterHistory = new MySqlDataAdapter("SELECT updatedata as 'Tanggal Update', concat('Rp. ',format(biaya,2)) as 'Biaya' from historybiayaproject where kodeproject = '"+textBoxKodeProject.Text+"'", ClassConnection.Instance().Connection);
                adapterHistory.Fill(dt);
                DataRow r = dt.NewRow();
                r[0] = DateTime.Now;
                
                r[1] = "Rp. "+String.Format("{0:n}", numericUpDownBiayaProject.Value.ToString());
                dt.Rows.Add(r);
                ((FormParent)this.MdiParent).panggilTampilSementara(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBoxFaktur_TextChanged(object sender, EventArgs e)
        {
            foreach (DataRow row in tableFaktur.Rows)
            {
                if (row[0].ToString().ToLower() == textBoxFaktur.Text.ToLower())
                {
                    MessageBox.Show("Data Sudah Pernah Ada");
                }
            }
        }

        private void NumericUpDownBiayaProjectSebelum_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownBiayaProjectSebelum.Value > 0)
            {
                numericUpDownPPN.Value = numericUpDownBiayaProjectSebelum.Value / 10;
                numericUpDownBiayaProject.Value = numericUpDownBiayaProjectSebelum.Value + numericUpDownPPN.Value;
            }
        }
    }
}
