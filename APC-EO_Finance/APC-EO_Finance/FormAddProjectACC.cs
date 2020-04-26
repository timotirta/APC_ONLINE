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
    public partial class FormAddProjectACC : Form
    {
        public FormAddProjectACC()
        {
            InitializeComponent();
        }
        public void resetKodeProjectACC()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "APRJ" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from dataprojectacc where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(10, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxKodeGen.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
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
                        string commandText = "INSERT INTO dataprojectacc VALUES(@kode,@nama,@tanggal,@lanjut,null,null)";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxKodeGen.Text);
                        cmd.Parameters.AddWithValue("@nama", textBoxNama.Text);
                        cmd.Parameters.AddWithValue("@tanggal", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@lanjut", checkBoxLanjut.Checked ? 1 : 0);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data telah Terinput", "Berhasil");

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

        private void FormAddProjectACC_FormClosed(object sender, FormClosedEventArgs e)
        {
            (this.MdiParent as FormParent).updateComboProjectAcc();
        }

        private void FormAddProjectACC_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();

            resetKodeProjectACC();
        }
    }
}
