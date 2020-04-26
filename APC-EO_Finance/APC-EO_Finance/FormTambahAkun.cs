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
    public partial class FormTambahAkun : Form
    {
        public int status = 1;
        public FormTambahAkun()
        {
            InitializeComponent();
        }
        public void resetKodeGenAkun()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = comboBoxParent.SelectedValue.ToString() + ".";
                    int len = kode.Length + 2;
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX((SUBSTRING_INDEX(kode, '.', -1) * 1)) from dataakun where kode like '" + kode + "%' and LENGTH(kode) <= "+len, ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        string[] temp = cmd.ExecuteScalar().ToString().Split('.');
                        dataAutoInc = Convert.ToInt32(temp[temp.Length-1]);
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString();
                    ClassConnection.Instance().Close();
                    textBoxKodeGen.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void tampilData(string kode)
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("Select a.kode,a.nama,a.nominal,concat(p.kode,' - ',p.nama) from dataakun a, dataakun p where p.kode = a.parent and a.kode = '"+kode+"'", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    //reader = cmd.ExecuteReader();
                    reader.Fill(table);

                    comboBoxParent.SelectedIndex = comboBoxParent.FindStringExact(table.Rows[0][3].ToString());
                    comboBoxParent.Enabled = false;
                    textBoxKodeGen.Text = table.Rows[0][0].ToString();
                    textBoxNama.Text = table.Rows[0][1].ToString();
                    numericUpDownNominal.Value = Convert.ToInt64(table.Rows[0][2].ToString());

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTambahAkun_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().Connecting();
            ClassConnection.Instance().Close();
            try
            {
                MySqlDataAdapter adapterCA;
                if (status == 1)
                {
                    adapterCA = new MySqlDataAdapter("SELECT concat(kode,' - ',nama) as nama,kode from dataakun where kode != '-' order by kode", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterCA = new MySqlDataAdapter("SELECT concat(kode,' - ',nama) as nama,kode from dataakun order by kode", ClassConnection.Instance().Connection);
                }
                DataSet datasetCA = new DataSet();
                adapterCA.Fill(datasetCA);
                comboBoxParent.DisplayMember = "nama";
                comboBoxParent.ValueMember = "kode";
                comboBoxParent.DataSource = datasetCA.Tables[0];
                if (status == 1)
                {
                    resetKodeGenAkun();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormTambahAkun_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FormParent)this.MdiParent).updateComboAcc();
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        if (status == 1)
                        {
                            string commandText = "INSERT INTO dataakun VALUES(@kode,@nama,@nominal,@level,@parent,null,null)";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKodeGen.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxNama.Text);
                            cmd.Parameters.AddWithValue("@nominal", numericUpDownNominal.Value);
                            cmd.Parameters.AddWithValue("@level", (textBoxKodeGen.Text.Split('.').Length-1).ToString());
                            cmd.Parameters.AddWithValue("@parent", comboBoxParent.SelectedValue);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah Terinput", "Berhasil");
                            
                            if (textBoxKodeGen.Text.Split('.')[textBoxKodeGen.Text.Split('.').Length-1] == "1")
                            {
                                cmd = new MySqlCommand("SELECT nominal from dataakun where kode='" + comboBoxParent.SelectedValue + "'",ClassConnection.Instance().Connection);
                                int tempNominalParent = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                                commandText = "INSERT INTO dataakun VALUES(@kode,@nama,@nominal,@level,@parent,null,null)";
                                cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", textBoxKodeGen.Text.Substring(0, textBoxKodeGen.Text.Length - 1) + "2");
                                cmd.Parameters.AddWithValue("@nama", "Lain-Lain");
                                cmd.Parameters.AddWithValue("@nominal", tempNominalParent);
                                cmd.Parameters.AddWithValue("@level", (textBoxKodeGen.Text.Split('.').Length - 1).ToString());
                                cmd.Parameters.AddWithValue("@parent", comboBoxParent.SelectedValue);
                                rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show("Parent telah terforward ke lain-lain", "Berhasil");
                                cmd = new MySqlCommand("update dataakun set nominal = 0 where kode='" + comboBoxParent.SelectedValue + "'", ClassConnection.Instance().Connection);
                                rowsAffected = cmd.ExecuteNonQuery();

                            }
                            ClassConnection.Instance().Close();
                            this.Close();
                        }
                        else
                        {
                            string commandText = "UPDATE dataakun set nama = @nama, nominal = @nominal, level = @level where kode=@kode";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKodeGen.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxNama.Text);
                            cmd.Parameters.AddWithValue("@nominal", numericUpDownNominal.Value);
                            cmd.Parameters.AddWithValue("@level", (textBoxKodeGen.Text.Split('.').Length - 1).ToString());
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah terupdate", "Berhasil");
                            ClassConnection.Instance().Close();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private void ComboBoxParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (status == 1)
            {
                resetKodeGenAkun();

            }
        }
    }
}
