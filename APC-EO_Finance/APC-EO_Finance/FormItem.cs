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
    public partial class FormItem : Form
    {
        public FormItem()
        {
            InitializeComponent();
        }
        public int status = 1;
        private void ButtonClearKlien_Click(object sender, EventArgs e)
        {
            textBoxNama.Text = "";
            textBoxSatuan.Text = "";
            numericUpDownHarga.Value = 0;
        }
        public void resetKodeItem(string vendor)
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "ITVD"+ vendor[0].ToString().ToUpper()+vendor[1].ToString().ToUpper() + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from dataitemvendor where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(12, 4));
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
        public string namavendor,kodevendor;
        public void tampilData(string kode = "")
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT i.kode, i.namabarang, i.satuan, i.hargasatuan,i.jenis FROM dataitemvendor i where kode ='" + kode + "'", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    //reader = cmd.ExecuteReader();
                    reader.Fill(table);

                    textBoxKode.Text = table.Rows[0][0].ToString();
                    textBoxNama.Text = table.Rows[0][1].ToString();
                    textBoxSatuan.Text = table.Rows[0][2].ToString();
                    numericUpDownHarga.Value = Convert.ToInt32(table.Rows[0][3].ToString());
                    textBoxJenis.Text = table.Rows[0][4].ToString();
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void FormItem_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            textBoxVendor.Text = namavendor;
            if (status == 1)
            {
                resetKodeItem(namavendor);
            }
            else if(status ==3)
            {
                textBoxSatuan.ReadOnly = true;
                textBoxNama.ReadOnly = true;
                numericUpDownHarga.Enabled= false;
            }
        }

        private void FormItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((FormParent)this.MdiParent).updateDGVItem();
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
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "INSERT INTO dataitemvendor VALUES(@kode,@nama,@satuan,@hargasatuan,@kodevendor,@jenis,null,null)";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKode.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxNama.Text);
                            cmd.Parameters.AddWithValue("@satuan", textBoxSatuan.Text);
                            cmd.Parameters.AddWithValue("@hargasatuan", numericUpDownHarga.Value);
                            cmd.Parameters.AddWithValue("@kodevendor", kodevendor);
                            cmd.Parameters.AddWithValue("@jenis", textBoxJenis.Text);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah Tersimpan", "Berhasil");
                            ClassConnection.Instance().Close();
                            resetKodeItem(namavendor);
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
                            string commandText = "UPDATE dataitemvendor SET namabarang = @nama,satuan = @satuan,hargasatuan = @hargasatuan,jenis = @jenis WHERE kode = @kode";

                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKode.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxNama.Text);
                            cmd.Parameters.AddWithValue("@satuan", textBoxSatuan.Text);
                            cmd.Parameters.AddWithValue("@jenis", textBoxJenis.Text);
                            cmd.Parameters.AddWithValue("@hargasatuan", numericUpDownHarga.Value);
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
    }
}
