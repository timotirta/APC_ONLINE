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

namespace APC_EO_Finance
{
    public partial class FormVendor : Form
    {
        public int status=1;
        public FormVendor()
        {
            InitializeComponent();

        }
        private void FormVendor_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            if (status == 1)
            {
                resetKodeVendor();
            }
            else if (status == 3)
            {
                textBoxNamaKlien.ReadOnly = true;
                textBoxNPWPKlien.ReadOnly = true;
                textBoxAlamatKlien.ReadOnly = true;
                textBoxEmail.ReadOnly = true;
                textBoxKota.ReadOnly = true;
                textBoxNoTelpKlien.ReadOnly = true;
                buttonSubmitKlien.Visible = false;
                buttonClearKlien.Visible = false;
                textBoxNoRek.ReadOnly = true;
                textBoxNamaContact.ReadOnly = true;
                comboBoxJenis.Enabled = false;
            }
            comboBoxJenis.Items.Add("Produksi");
            comboBoxJenis.Items.Add("Show Management");
            comboBoxJenis.Items.Add("Digital/IT");
            comboBoxJenis.Items.Add("Merchandise");
            comboBoxJenis.Items.Add("Cetak");
            comboBoxJenis.Items.Add("Team/Tukang");
            comboBoxJenis.Items.Add("Transportasi");
            comboBoxJenis.Items.Add("Venue");
            comboBoxJenis.Items.Add("Permit");
            comboBoxJenis.Items.Add("Property");
            comboBoxJenis.Items.Add("Konsumsi");
            comboBoxJenis.Items.Add("Talent");
        }


        public void tampilData(string kode = "")
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM datavendor where kode ='" + kode + "'", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    //reader = cmd.ExecuteReader();
                    reader.Fill(table);

                    textBoxKodeKlien.Text = table.Rows[0][0].ToString();
                    textBoxNamaKlien.Text = table.Rows[0][1].ToString();
                    textBoxAlamatKlien.Text = table.Rows[0][2].ToString();
                    textBoxKota.Text = table.Rows[0][3].ToString();
                    textBoxEmail.Text = table.Rows[0][4].ToString();
                    textBoxNoTelpKlien.Text = table.Rows[0][5].ToString();
                    textBoxNPWPKlien.Text = table.Rows[0][6].ToString();
                    textBoxNoRek.Text = table.Rows[0][7].ToString();
                    textBoxNamaContact.Text = table.Rows[0][8].ToString();
                    comboBoxJenis.SelectedIndex = comboBoxJenis.FindStringExact(table.Rows[0][9].ToString());

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void resetKodeVendor()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "VD" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from datavendor where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(8, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxKodeKlien.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }


        private void ButtonClearKlien_Click(object sender, EventArgs e)
        {
            textBoxNamaKlien.Text = "";
            textBoxNoTelpKlien.Text = "";
            textBoxNPWPKlien.Text = "";
            textBoxAlamatKlien.Text = "";
            textBoxNamaContact.Text = "";
            textBoxNoRek.Text = "";
        }

        private void ButtonSubmitKlien_Click(object sender, EventArgs e)
        {
            if (status == 1)
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "INSERT INTO datavendor VALUES(@kode,@nama,@alamat,@email,@kota,@notelp,@npwp,@norek,@namakontak,@jenis,null,null)";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKodeKlien.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxNamaKlien.Text);
                            cmd.Parameters.AddWithValue("@alamat", textBoxAlamatKlien.Text);
                            cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                            cmd.Parameters.AddWithValue("@kota", textBoxKota.Text);
                            cmd.Parameters.AddWithValue("@notelp", textBoxNoTelpKlien.Text);
                            cmd.Parameters.AddWithValue("@npwp", textBoxNPWPKlien.Text);
                            cmd.Parameters.AddWithValue("@norek", textBoxNoRek.Text);
                            cmd.Parameters.AddWithValue("@namakontak", textBoxNamaContact.Text);
                            cmd.Parameters.AddWithValue("@jenis", comboBoxJenis.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah Tersimpan", "Berhasil");
                            ClassConnection.Instance().Close();
                            resetKodeVendor();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
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
                            string commandText = "UPDATE datavendor SET nama = @nama,alamat = @alamat,email = @email,kota=@kota,notelp = @notelp,npwp = @npwp,norek = @norek,namacontact = @namakontak,jenisvendor = @jenisvendor WHERE kode = @kode";
                            
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKodeKlien.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxNamaKlien.Text);
                            cmd.Parameters.AddWithValue("@alamat", textBoxAlamatKlien.Text);
                            cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                            cmd.Parameters.AddWithValue("@kota", textBoxKota.Text);
                            cmd.Parameters.AddWithValue("@notelp", textBoxNoTelpKlien.Text);
                            cmd.Parameters.AddWithValue("@npwp", textBoxNPWPKlien.Text);
                            cmd.Parameters.AddWithValue("@norek", textBoxNoRek.Text);
                            cmd.Parameters.AddWithValue("@namakontak", textBoxNamaContact.Text);
                            cmd.Parameters.AddWithValue("@jenisvendor", comboBoxJenis.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();
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

        private void ButtonCloseKlien_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormVendor_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((FormParent)this.MdiParent).updateDGVVendor();

        }
    }
}
