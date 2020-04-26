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
    public partial class FormDPVendor : Form
    {
        public string kode;
        public FormDPVendor()
        {
            InitializeComponent();
        }

        public void tampilData(string kode)
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter("select p.kode,p.nama from dataproject p where (p.kode_pot = '" + kode + "' or p.kode_ae = '" + kode + "' or p.kode_pot = '" + kode + "') and p.status = 0 ", ClassConnection.Instance().Connection);
                    DataTable tb = new DataTable();
                    adapter.Fill(tb);
                    //MessageBox.Show(tb.Rows.Count.ToString());
                    if (tb.Rows.Count > 0)
                    {
                        comboBoxProjectDP.DisplayMember = "nama";
                        comboBoxProjectDP.ValueMember = "kode";
                        comboBoxProjectDP.DataSource = tb;
                        adapter = new MySqlDataAdapter("select v.kode, v.nama from datavendorproject vp, datavendor v where vp.kodevendor = v.kode and vp.kodeproject ='" + comboBoxProjectDP.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
                        tb = new DataTable();
                        adapter.Fill(tb);
                    }

                    if (tb.Rows.Count > 0)
                    {
                        comboBoxVendorDP.DisplayMember = "nama";
                        comboBoxVendorDP.ValueMember = "kode";
                        comboBoxVendorDP.DataSource = tb;
                    }
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error Tampil Data");
            }
        }

        private void FormDPVendor_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "INSERT INTO dpvendor VALUES(null,@kodepeg,@kodepr,@kodevd,@jumlah,@tanggal,0,null,null)";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kodepeg", kode);
                        cmd.Parameters.AddWithValue("@kodepr", comboBoxProjectDP.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@kodevd", comboBoxVendorDP.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@jumlah", Convert.ToInt64(numericUpDownJumlah.Value));
                        cmd.Parameters.AddWithValue("@tanggal", dateTimePickerTanggalDP.Value.ToString("yyyy-MM-dd"));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data telah Tersimpan", "Berhasil");
                        ClassConnection.Instance().Close();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Submit");
                }
            }
        }

        private void comboBoxProjectDP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
