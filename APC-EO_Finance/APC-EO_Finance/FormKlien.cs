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
    public partial class FormKlien : Form
    {
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        void AngkaChecker(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        public FormKlien()
        {
            InitializeComponent();
        }
        public int status = 1;
        private void ButtonClearKlien_Click(object sender, EventArgs e)
        {
            textBoxAlamatKlien.Text = "";
            textBoxEmailKlien.Text = "";
            textBoxFaxKlien.Text = "";
            textBoxNamaKlien.Text = "";
            textBoxNoTelpKlien.Text = "";
            textBoxNPWPKlien.Text = "";
            textBoxPrincipal.Text = "";
            textBoxUser.Text = "";
        }
        public void resetKodeKlien()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "KL" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from dataklien where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
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
        public void tampilData(string kode="")
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlDataReader reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM dataklien where kode ='"+kode+"'", ClassConnection.Instance().Connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBoxAlamatKlien.Text = reader["alamat"].ToString();
                        textBoxEmailKlien.Text = reader["email"].ToString();
                        textBoxFaxKlien.Text = reader["fax"].ToString();
                        textBoxNamaKlien.Text = reader["nama"].ToString();
                        textBoxNoTelpKlien.Text = reader["notelp"].ToString();
                        textBoxNPWPKlien.Text = reader["npwp"].ToString();
                        textBoxKodeKlien.Text = reader["kode"].ToString();
                        numericUpDownPiutangKlien.Value = Convert.ToInt32(reader["bataspiutang"].ToString());
                        textBoxPrincipal.Text = reader["principal"].ToString();
                        textBoxUser.Text = reader["user"].ToString();
                    }
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void FormKlien_Load(object sender, EventArgs e)
        {
            if (status == 1)
            {
                resetKodeKlien();
            }
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
                            string commandText = "INSERT INTO dataklien VALUES(@kode,@nama,@principal,@user,@alamat,@notelp,@email,@fax,@npwp,@piutang,null,null)";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKodeKlien.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxNamaKlien.Text);
                            cmd.Parameters.AddWithValue("@principal", textBoxPrincipal.Text);
                            cmd.Parameters.AddWithValue("@user", textBoxUser.Text);
                            cmd.Parameters.AddWithValue("@alamat", textBoxAlamatKlien.Text);
                            cmd.Parameters.AddWithValue("@notelp", textBoxNoTelpKlien.Text);
                            cmd.Parameters.AddWithValue("@email", textBoxEmailKlien.Text);
                            cmd.Parameters.AddWithValue("@fax", textBoxFaxKlien.Text);
                            cmd.Parameters.AddWithValue("@npwp", textBoxNPWPKlien.Text);
                            cmd.Parameters.AddWithValue("@piutang", numericUpDownPiutangKlien.Value);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah Tersimpan", "Berhasil");
                            ClassConnection.Instance().Close();
                            resetKodeKlien();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            else if(status == 2)
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "UPDATE dataklien SET nama = @nama,principal = @principal, user = @user,alamat = @alamat,notelp = @notelp,email = @email,fax = @fax,npwp = @npwp WHERE kode = @kode";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKodeKlien.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxNamaKlien.Text);
                            cmd.Parameters.AddWithValue("@principal", textBoxPrincipal.Text);
                            cmd.Parameters.AddWithValue("@user", textBoxUser.Text);
                            cmd.Parameters.AddWithValue("@alamat", textBoxAlamatKlien.Text);
                            cmd.Parameters.AddWithValue("@notelp", textBoxNoTelpKlien.Text);
                            cmd.Parameters.AddWithValue("@email", textBoxEmailKlien.Text);
                            cmd.Parameters.AddWithValue("@fax", textBoxFaxKlien.Text);
                            cmd.Parameters.AddWithValue("@npwp", textBoxNPWPKlien.Text);
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

        private void TextBoxEmailKlien_Validating(object sender, CancelEventArgs e)
        {
            //if (!IsValidEmail(textBoxEmailKlien.Text))
            //{
            //    MessageBox.Show("Email Tidak Valid","Validating");
            //}
            
        }

        private void TextBoxNoTelpKlien_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FormKlien_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((FormParent)this.MdiParent).updateDGVKlien();
        }
    }
}
