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
    public partial class FormAccPengeluaran : Form
    {
        public FormAccPengeluaran()
        {
            InitializeComponent();
        }
        int status = 2;
        public void ambilBuatOps(string kode="",int status = 0)
        {
            try
            {
                this.status = status;
                if (ClassConnection.Instance().Connecting())
                {
                    groupBoxProjectPengeluaran.Visible = false;
                    groupBoxOpsPengeluaran.Visible = true;
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT norek,nama,jumlah,tanggal,deskripsi,dividen from pettyops where kode='" + kode + "'", ClassConnection.Instance().Connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    textBoxOpsKodePengeluaran.Text = kode;

                    textBoxOpsNoRekPengeluaran.Text = table.Rows[0][0].ToString();
                    textBoxOpsNamaPengeluaran.Text = table.Rows[0][1].ToString();
                    numericUpDownOpsJumlahPengeluaran.Value = Convert.ToInt64(table.Rows[0][2].ToString());

                    //MessageBox.Show(table.Rows[0][3].ToString());

                    dateTimePickerOpsTanggalPengeluaran.Value = Convert.ToDateTime(table.Rows[0][3].ToString());
                    richTextBoxOpsDeskripsiPengeluaran.Text = table.Rows[0][4].ToString();
                    checkBoxDividen.Checked = Convert.ToBoolean(status);
                    checkBoxDividen.Enabled = false;
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }
        public void ambilBuatKepCab(string kode = "")
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    groupBoxProjectPengeluaran.Visible = true;
                    groupBoxOpsPengeluaran.Visible = false;
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT pp.norek,pp.nama,pp.jumlah,pp.tanggal,pp.tipe,pp.kode_vendor,pp.deskripsi,p.nama from pettyproject pp, dataproject p where pp.id_project = p.kode and pp.kode='" + kode + "'", ClassConnection.Instance().Connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    textBoxKodeProject.Text = kode;
                    textBoxNoRekProject.Text = table.Rows[0][0].ToString();
                    textBoxNamaProject.Text = table.Rows[0][1].ToString();
                    numericUpDownProjectPengeluaranJumlah.Value = Convert.ToInt64(table.Rows[0][2].ToString());

                    dateTimePickerTanggalProject.Value = Convert.ToDateTime(table.Rows[0][3].ToString());
                    RadioButton[] rd = { radioButtonNrml, radioButtonVendor };
                    rd[Convert.ToInt32(table.Rows[0][4].ToString())].Checked = true;
                    foreach (RadioButton r in rd)
                    {
                        r.Enabled = false;
                    }
                    if (radioButtonVendor.Checked)
                    {
                        textBoxVendor.Text = table.Rows[0][5].ToString();
                    }
                    richTextBoxDeskripsiProject.Text = table.Rows[0][6].ToString();
                    textBoxProject.Text = table.Rows[0][7].ToString();


                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void FormAccPengeluaran_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
        }

        private void ButtonSubmitProject_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "UPDATE pettyproject set jumlah = @jumlah,norek=@norek, tanggal = @tanggal, deskripsi = @deskripsi, status = 1 where kode=@kode";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxKodeProject.Text);
                        cmd.Parameters.AddWithValue("@norek", textBoxNoRekProject.Text);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownProjectPengeluaranJumlah.Value);
                        cmd.Parameters.AddWithValue("@tanggal", dateTimePickerTanggalProject.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@deskripsi", richTextBoxDeskripsiProject.Text);

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

        private void FormAccPengeluaran_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (status == 2)
            {
                ((FormParent)this.MdiParent).updateDGVNotif();
            }
            else if (status == 1)
            {
                ((FormParent)this.MdiParent).updateDGVNotifCeo();
            }
            else if (status == 0)
            {
                ((FormParent)this.MdiParent).updateDGVNotifFin();
            }
        }

        private void ButtonOpsSubmitPengeluaran_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "UPDATE pettyops set jumlah = @jumlah,norek=@norek, tanggal = @tanggal, deskripsi = @deskripsi, status = 1 where kode=@kode";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxOpsKodePengeluaran.Text);
                        cmd.Parameters.AddWithValue("@norek", textBoxOpsNoRekPengeluaran.Text);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownOpsJumlahPengeluaran.Value);
                        cmd.Parameters.AddWithValue("@tanggal", dateTimePickerOpsTanggalPengeluaran.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@deskripsi", richTextBoxOpsDeskripsiPengeluaran.Text);

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