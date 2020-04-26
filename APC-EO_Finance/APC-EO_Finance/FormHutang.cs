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
    public partial class FormHutang : Form
    {
        public string kode;
        public FormHutang()
        {
            InitializeComponent();
        }
        public void tampilData(string kode)
        {
            this.kode = kode;
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT nama,jabatan from datakaryawan where kode = '" + kode + "'", ClassConnection.Instance().Connection);
                DataTable tb = new DataTable();
                adapter.Fill(tb);
                labelNama.Text = tb.Rows[0][0].ToString();
                labelJabatan.Text = tb.Rows[0][1].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Data");
            }
        }
        private void FormHutang_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            dateTimePickerTanggalKembali.MinDate = DateTime.Now;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (ClassConnection.Instance().Connecting())
                {
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO hutangKaryawan values(null,@kode,@hutang,@kembali,0,null,null)", ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", kode);
                        cmd.Parameters.AddWithValue("@hutang", Convert.ToInt64(numericUpDownHutang.Value));
                        cmd.Parameters.AddWithValue("@kembali", dateTimePickerTanggalKembali.Value.ToString("yyyy-MM-dd"));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        ClassConnection.Instance().Close();
                        MessageBox.Show("Hutang Telah Di inputkan", "Berhasil");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error Input");
                    }

                }
            }
        }

        private void NumericUpDownHutang_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
