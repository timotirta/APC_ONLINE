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
    public partial class FormPetty : Form
    {
        public bool jakartatidak = true;
        public FormPetty()
        {
            InitializeComponent();
        }
        public void resetDataDariCA()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT k.nama,ca.total,ca.status FROM pettyca ca, datakaryawan k where ca.kode ='" + comboBoxKodeCA.Text + "' and ca.kodekaryawan = k.kode", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    //reader = cmd.ExecuteReader();
                    reader.Fill(table);

                    textBoxKasPengeluaranNama.Text = table.Rows[0][0].ToString();
                    numericUpDownKasPengeluaranJumlah.Value = Convert.ToInt32(table.Rows[0][1].ToString());
                    if (numericUpDownKasPengeluaranJumlah.Value >= 1000000)
                    {
                        checkBoxApproved.Visible = true;
                        checkBoxApproved.Checked = table.Rows[0][2].ToString() == "2" ? true : false;
                        
                    }
                    else
                    {
                        checkBoxApproved.Visible = false;
                        checkBoxApproved.Checked = false;
                    }
                    
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 6");
            }
        }

        void updateComboPJ()
        {
            try
            {
                MySqlDataAdapter adapterPJ = new MySqlDataAdapter("SELECT ca.kode,k.nama from pettyca ca, datakaryawan k where ca.kodekaryawan = k.kode and status = 3 order by kode", ClassConnection.Instance().Connection);
                DataSet datasetPJ = new DataSet();
                adapterPJ.Fill(datasetPJ);
                comboBoxPJ.DisplayMember = "kode";
                comboBoxPJ.ValueMember = "nama";
                comboBoxPJ.DataSource = datasetPJ.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 7");
            }
        }

        public void updateComboCA(string kode="")
        {
            try
            {
                MySqlDataAdapter adapterCA = new MySqlDataAdapter("SELECT kode from pettyca where status != 1 and status != 3 order by kode", ClassConnection.Instance().Connection);
                DataSet datasetCA = new DataSet();
                adapterCA.Fill(datasetCA);
                comboBoxKodeCA.DisplayMember = "kode";
                comboBoxKodeCA.ValueMember = "kode";
                comboBoxKodeCA.DataSource = datasetCA.Tables[0];
                if (kode != "")
                {
                    comboBoxKodeCA.SelectedIndex = comboBoxKodeCA.FindStringExact(kode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void resetMoney()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlDataReader reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT Nama,Money FROM saldo", ClassConnection.Instance().Connection);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < comboBoxTipe.Items.Count; i++)
                        {
                            if (comboBoxTipe.Items[i].ToString() == reader["Nama"].ToString())
                            {
                                duit[comboBoxTipe.Items[i].ToString()] = Convert.ToInt64(reader["Money"]);
                                dict[comboBoxTipe.Items[i].ToString()].Text = String.Format("{0:n}", reader["Money"]);
                                break;
                            }
                        }
                    }
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 8");
            }
        }

        public void resetKodePengeluaranProject()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "IPG" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from pettyproject where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(9, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxProjectPengeluaranKode.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 9");
            }
        }
        public void resetKodePemasukanProject()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "IPM" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from pettyproject where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(9, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxProjectPemasukanKode.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 10");
            }
        }
        public void resetKodePengeluaranKas()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "KASPG" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from pettykas where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(11, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxKasPengeluaranKode.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 11");
            }
        }
        public void resetKodePemasukanKas()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "KASPM" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from pettykas where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(11, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxKasPemasukanKode.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 12");
            }
        }
        public void resetKodePemasukanOps()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "OIPM" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from pettyops where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(10, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxOpsKodePemasukan.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro 13r");
            }
        }
        public void resetKodePengeluaranOps()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "OIPG" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from pettyops where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(10, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxOpsKodePengeluaran.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 14");
            }
        }
        public void resetKodePJKas()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "KASPJ" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from pettypj where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(11, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxKasPJKode.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 15");
            }
        }
        Dictionary<string, Label> dict = new Dictionary<string, Label>();
        Dictionary<string, long> duit = new Dictionary<string, long>();
        private void FormPetty_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            comboBoxTipe.Items.Add("Operasional");
            dict.Add("Operasional", labelOpsSaldo);
            duit.Add("Operasional", 0);
            comboBoxTipe.Items.Add("Project");
            dict.Add("Project", labelProjectSaldo);
            duit.Add("Project", 0);
            comboBoxTipe.Items.Add("Kas Kecil");
            dict.Add("Kas Kecil", labelKasSaldo);
            duit.Add("Kas Kecil", 0);
            groupBoxOps.Visible = false;
            groupBoxProject.Visible = false;
            groupBoxOpsPemasukan.Visible = false;
            groupBoxOpsPengeluaran.Visible = false;
            groupBoxOpsCetak.Visible = false;
            groupBoxProjectPemasukan.Visible = false;
            groupBoxProjectPengeluaran.Visible = false;
            groupBoxProjectCetak.Visible = false;
            groupBoxKasKecil.Visible = false;
            groupBoxKasCetak.Visible = false;
            groupBoxKasPemasukan.Visible = false;
            groupBoxKasPengeluaran.Visible = false;
            groupBoxKasPJ.Visible = false;
            resetMoney();
            if (duit["Kas Kecil"] <= 5000000)
            {
                labelIsiSaldo.Visible = true;
            }
        }

        private void ComboBoxTipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelIsiSaldo.Visible = false;
            groupBoxOps.Visible = false;
            groupBoxProject.Visible = false;
            groupBoxKasKecil.Visible = false;
            if (comboBoxTipe.SelectedItem.ToString() == "Operasional")
            {
                groupBoxOps.Visible = true;
            }
            if (comboBoxTipe.SelectedItem.ToString() == "Project")
            {
                groupBoxProject.Visible = true;
                comboBoxListProject.DataSource = null;
                //string[] dispProject = { "Project A", "Project B", "Project C" };
                //string[] valProject = { "1", "2", "3" };
                //Dictionary<string, string> dct = new Dictionary<string, string>();
                //for (int i = 0; i < 3; i++)
                //{
                //    dct.Add(dispProject[i], valProject[i]);
                //}
                //comboBoxListProject.DataSource = new BindingSource(dct, null);
                try
                {
                    MySqlDataAdapter adapterLP = new MySqlDataAdapter("SELECT kode,nama FROM dataproject where kode like 'PR%' and status = 0 order by kode", ClassConnection.Instance().Connection);
                    DataSet datasetLP = new DataSet();
                    adapterLP.Fill(datasetLP);
                    comboBoxListProject.DisplayMember = "nama";
                    comboBoxListProject.ValueMember = "kode";
                    comboBoxListProject.DataSource = datasetLP.Tables[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            if (comboBoxTipe.SelectedItem.ToString() == "Kas Kecil")
            {
                groupBoxKasKecil.Visible = true;
            }
        }

        private void ButtonOpsPemasukan_Click(object sender, EventArgs e)
        {
            resetKodePemasukanOps();
            groupBoxOpsPengeluaran.Visible = false;
            groupBoxOpsPemasukan.Visible = true;
            groupBoxOpsCetak.Visible = false;
            groupBoxApprove.Visible = false;
        }

        private void ButtonOpsPengeluaran_Click(object sender, EventArgs e)
        {
            resetKodePengeluaranOps();
            groupBoxOpsPemasukan.Visible = false;
            groupBoxOpsPengeluaran.Visible = true;
            groupBoxOpsCetak.Visible = false;
            groupBoxApprove.Visible = false;
        }
        
        private void ButtonPemasukanSubmit_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "INSERT INTO pettyops VALUES(@kode,@norek,@nama,@jumlah,@tanggal,@deskripsi,'D',0,2,null,null)";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxOpsKodePemasukan.Text);
                        cmd.Parameters.AddWithValue("@norek", textBoxOpsNoRekPemasukan.Text);
                        cmd.Parameters.AddWithValue("@nama", textBoxOpsNamaPemasukan.Text);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownOpsJumlahPemasukan.Value);
                        cmd.Parameters.AddWithValue("@tanggal", dateTimePickerOpsTanggalPemasukan.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@deskripsi", richTextBoxOpsDeskripsiPemasukan.Text);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data telah Tersimpan", "Berhasil");

                        commandText = "UPDATE saldo SET Money = @uang WHERE Nama = @nama";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@uang", duit[comboBoxTipe.SelectedItem.ToString()] + numericUpDownOpsJumlahPemasukan.Value);
                        cmd.Parameters.AddWithValue("@nama", comboBoxTipe.SelectedItem.ToString());
                        rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Uang telah Terupdate", "Berhasil");
                        ClassConnection.Instance().Close();
                        resetKodePemasukanOps();
                        resetMoney();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 16");
                }
            }
        }

        private void ButtonOpsCetak_Click(object sender, EventArgs e)
        {
            groupBoxOpsPemasukan.Visible = false;
            groupBoxOpsPengeluaran.Visible = false;
            groupBoxApprove.Visible = false;
            groupBoxOpsCetak.Visible = true;
            groupBoxOpsCetakBK.Visible = false;
            groupBoxOpsCetakBM.Visible = false;
            groupBoxOpsCetakSaldo.Visible = false;
            groupBoxPenggajian.Visible = false;
            tabControlGajiBonus.Visible = false;
        }

        private void ButtonOpsCetakSaldo_Click(object sender, EventArgs e)
        {
            groupBoxOpsCetakBK.Visible = false;
            groupBoxOpsCetakBM.Visible = false;
            groupBoxOpsCetakSaldo.Visible = true;
            groupBoxPenggajian.Visible=false;
            tabControlGajiBonus.Visible = false;
        }

        private void ButtonOpsCetakKeluar_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterBK = new MySqlDataAdapter("SELECT kode FROM pettyops WHERE kodedebitkredit = 'K' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBK = new DataSet();
                adapterBK.Fill(datasetBK);
                comboBoxOpsCetakBKKode.DataSource = datasetBK.Tables[0];
                comboBoxOpsCetakBKKode.DisplayMember = "kode";
                comboBoxOpsCetakBKKode.ValueMember = "kode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            groupBoxOpsCetakBK.Visible = true;
            groupBoxOpsCetakBM.Visible = false;
            groupBoxOpsCetakSaldo.Visible = false;
            groupBoxPenggajian.Visible = false;
            tabControlGajiBonus.Visible = false;
        }

        private void ButtonOpsCetakMasuk_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterBM = new MySqlDataAdapter("SELECT kode FROM pettyops WHERE kodedebitkredit = 'D' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBM = new DataSet();
                adapterBM.Fill(datasetBM);
                comboBoxOpsCetakBMKode.DataSource = datasetBM.Tables[0];
                comboBoxOpsCetakBMKode.DisplayMember = "kode";
                comboBoxOpsCetakBMKode.ValueMember = "kode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            groupBoxOpsCetakBK.Visible = false;
            groupBoxOpsCetakBM.Visible = true;
            groupBoxOpsCetakSaldo.Visible = false;
            groupBoxPenggajian.Visible = false;
            tabControlGajiBonus.Visible = false;
        }

        private void ButtonProjectPemasukan_Click(object sender, EventArgs e)
        {
            //if (comboBoxListProject.SelectedIndex != -1)
            //{
                groupBoxProjectPemasukan.Visible = true;
                groupBoxProjectPengeluaran.Visible = false;
                groupBoxProjectCetak.Visible = false;
                resetKodePemasukanProject();
            //}
            //else
            //{
            //    MessageBox.Show("Silahkan Memilih terlebih dahulu project tersebut","Wrong");
            //}
        }

        private void ButtonProjectPengeluaran_Click(object sender, EventArgs e)
        {

            if (comboBoxListProject.SelectedIndex != -1)
            {
                resetKodePengeluaranProject();

                groupBoxProjectPemasukan.Visible = false;
                groupBoxProjectPengeluaran.Visible = true;
                groupBoxProjectCetak.Visible = false;
            }
            else
            {
                MessageBox.Show("Silahkan Memilih terlebih dahulu project tersebut", "Wrong");
            }
        }

        private void ButtonProjectCetak_Click(object sender, EventArgs e)
        {

            if (comboBoxListProject.SelectedIndex != -1)
            {
                groupBoxProjectPemasukan.Visible = false;
                groupBoxProjectPengeluaran.Visible = false;
                groupBoxProjectCetak.Visible = true;
                groupBoxProjectCetakBK.Visible = false;
                groupBoxProjectCetakBM.Visible = false;
                groupBoxProjectCetakSaldo.Visible = false;
            }
            else
            {
                MessageBox.Show("Silahkan Memilih terlebih dahulu project tersebut", "Wrong");
            }
        }

        private void ButtonProjectCetakSaldo_Click(object sender, EventArgs e)
        {
            groupBoxProjectCetakBK.Visible = false;
            groupBoxProjectCetakBM.Visible = false;
            groupBoxProjectCetakSaldo.Visible = true;
        }

        private void ButtonProjectCetakBK_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterBK = new MySqlDataAdapter("SELECT kode FROM pettyproject WHERE kodedebitkredit = 'K' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBK = new DataSet();
                adapterBK.Fill(datasetBK);
                comboBoxProjectCetakBKKode.DataSource = datasetBK.Tables[0];
                comboBoxProjectCetakBKKode.DisplayMember = "kode";
                comboBoxProjectCetakBKKode.ValueMember = "kode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error 17");
            }
            
            groupBoxProjectCetakBK.Visible = true;
            groupBoxProjectCetakBM.Visible = false;
            groupBoxProjectCetakSaldo.Visible = false;

        }

        private void ButtonProjectCetakBM_Click(object sender, EventArgs e)
        {

            try
            {
                MySqlDataAdapter adapterBK = new MySqlDataAdapter("SELECT kode FROM pettyproject WHERE kodedebitkredit ='D' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBK = new DataSet();
                adapterBK.Fill(datasetBK);
                comboBoxProjectCetakBMKode.DataSource = datasetBK.Tables[0];
                comboBoxProjectCetakBMKode.DisplayMember = "kode";
                comboBoxProjectCetakBMKode.ValueMember = "kode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 18");
            }
            groupBoxProjectCetakBK.Visible = false;
            groupBoxProjectCetakBM.Visible = true;
            groupBoxProjectCetakSaldo.Visible = false;
        }

        private void ButtonKasCetak_Click(object sender, EventArgs e)
        {
            groupBoxKasCetak.Visible = true;
            groupBoxKasPemasukan.Visible = false;
            groupBoxKasPengeluaran.Visible = false;
            groupBoxKasPJ.Visible = false;

            groupBoxKasCetakBK.Visible = false;
            groupBoxKasCetakBM.Visible = false;
            groupBoxKasCetakPJ.Visible = false;
            groupBoxKasCetakSaldo.Visible = false;
        }

        private void ButtonKasPengeluaran_Click(object sender, EventArgs e)
        {
            resetKodePengeluaranKas();
            groupBoxKasCetak.Visible = false;
            groupBoxKasPemasukan.Visible = false;
            groupBoxKasPengeluaran.Visible = true;
            groupBoxKasPJ.Visible = false;
        }

        private void ButtonKasPemasukan_Click(object sender, EventArgs e)
        {
            resetKodePemasukanKas();
            groupBoxKasCetak.Visible = false;
            groupBoxKasPemasukan.Visible = true;
            groupBoxKasPengeluaran.Visible = false;
            groupBoxKasPJ.Visible = false;

        }

        private void ButtonKasCetakSaldo_Click(object sender, EventArgs e)
        {
            groupBoxKasCetakSaldo.Visible = true;
            groupBoxKasCetakBK.Visible = false;
            groupBoxKasCetakBM.Visible = false;
            groupBoxKasCetakPJ.Visible = false;
        }

        private void ButtonKasCetakBK_Click(object sender, EventArgs e)
        {

            try
            {
                MySqlDataAdapter adapterBK = new MySqlDataAdapter("SELECT kode FROM pettykas WHERE kodedebitkredit ='K' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBK = new DataSet();
                adapterBK.Fill(datasetBK);
                comboBoxKasCetakBKKode.DataSource = datasetBK.Tables[0];
                comboBoxKasCetakBKKode.DisplayMember = "kode";
                comboBoxKasCetakBKKode.ValueMember = "kode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 19");
            }
            groupBoxKasCetakSaldo.Visible = false;
            groupBoxKasCetakBK.Visible = true;
            groupBoxKasCetakBM.Visible = false;
            groupBoxKasCetakPJ.Visible = false;

        }

        private void ButtonKasCetakBM_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterBM = new MySqlDataAdapter("SELECT kode FROM pettykas WHERE kodedebitkredit ='D' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBM = new DataSet();
                adapterBM.Fill(datasetBM);
                comboBoxKasCetakBMKode.DataSource = datasetBM.Tables[0];
                comboBoxKasCetakBMKode.DisplayMember = "kode";
                comboBoxKasCetakBMKode.ValueMember = "kode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 21");
            }
            groupBoxKasCetakSaldo.Visible = false;
            groupBoxKasCetakBK.Visible = false;
            groupBoxKasCetakBM.Visible = true;
            groupBoxKasCetakPJ.Visible = false;
        }

        private void ButtonKasTanggungJawab_Click(object sender, EventArgs e)
        {
            resetKodePJKas();
            updateComboPJ();
            radioButtonKembalianNo.Checked = true;
            label75.Visible = false;
            label114.Visible = false;
            radioButtonNominalNo.Checked = true;
            radioButtonNotaNo.Checked = true;
            numericUpDownKasPJJumlah.Visible = false;
            groupBoxKasCetak.Visible = false;
            groupBoxKasPemasukan.Visible = false;
            groupBoxKasPengeluaran.Visible = false;
            groupBoxKasPJ.Visible = true;
        }

        private void ButtonProjectPengeluaranClear_Click(object sender, EventArgs e)
        {
            if (buttonProjectPengeluaranClear.Text == "Cancel")
            {
                textBoxProjectPengeluaranNoRek.ReadOnly = false;
                textBoxProjectPengeluaranNama.ReadOnly = false;
                numericUpDownProjectPengeluaranJumlah.Enabled = true;
                groupBox5.Enabled = true;
                comboBoxListVendorProject.Enabled = true;
                richTextBoxProjectPengeluaranDeskripsi.ReadOnly = false;
                dateTimePickerProjectPengeluaranTanggal.Enabled = true;
                buttonProjectPengeluaranClear.Text = "Clear";

            }
            textBoxProjectPengeluaranNama.Text = "";
            textBoxProjectPengeluaranNoRek.Text = "";
            numericUpDownProjectPengeluaranJumlah.Value = 0;
            richTextBoxProjectPengeluaranDeskripsi.Text = "";
            checkBoxApproveProject.Checked = false;
            resetKodePengeluaranProject();
        }

        private void ButtonProjectPengeluaranSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (checkBoxApproveProject.Checked)
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "UPDATE pettyproject set status = 2 where kode=@kode";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxProjectPengeluaranKode.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah Tersimpan", "Berhasil");

                            if (radioButtonVendor.Checked)
                            {
                                commandText = "update datavendorproject set sudahdibayar = sudahdibayar + @total where kodevendor = @kodevendor and kodeproject = @kodeproject";
                                cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@total", numericUpDownProjectPengeluaranJumlah.Value);
                                cmd.Parameters.AddWithValue("@kodevendor", comboBoxListVendorProject.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@kodeproject", comboBoxListProject.SelectedValue.ToString());

                                rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show("Vendor telah Terbayar sejumlah Rp. " + numericUpDownProjectPengeluaranJumlah.Value.ToString("N"), "Berhasil");

                            }

                            if (richTextBoxProjectPengeluaranDeskripsi.Text.Substring(0, 2) == "DP")
                            {
                                commandText = "UPDATE dpvendor SET status = 1 WHERE kodepegawai = @kode and kodeproject = @kodepr and kodevendor = @kodevd and status = 0";
                                cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", textBoxProjectPengeluaranNoRek.Text);
                                cmd.Parameters.AddWithValue("@kodepr", comboBoxListProject.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@kodevd", comboBoxListVendorProject.SelectedValue.ToString());
                                rowsAffected = cmd.ExecuteNonQuery();
                            }


                            commandText = "UPDATE saldo SET Money = @uang WHERE Nama = @nama";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@uang", duit[comboBoxTipe.SelectedItem.ToString()] - numericUpDownProjectPengeluaranJumlah.Value);
                            cmd.Parameters.AddWithValue("@nama", comboBoxTipe.SelectedItem.ToString());
                            rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Uang telah Terupdate", "Berhasil");
                            ClassConnection.Instance().Close();
                            resetKodePengeluaranProject();
                            resetMoney();

                            textBoxProjectPengeluaranNama.ReadOnly = false;
                            textBoxProjectPengeluaranNoRek.ReadOnly = false;
                            numericUpDownProjectPengeluaranJumlah.Enabled = true;
                            groupBox5.Enabled = true;
                            comboBoxListVendorProject.Enabled = true;
                            richTextBoxProjectPengeluaranDeskripsi.ReadOnly = false;
                            dateTimePickerProjectPengeluaranTanggal.Enabled = true;
                        }
                    }
                    else
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "INSERT INTO pettyproject VALUES(@kode,@norek,@nama,@jumlah,@tanggal,@deskripsi,'K',@tipe,@kodevendor,@id_project,0,null,null)";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxProjectPengeluaranKode.Text);
                            cmd.Parameters.AddWithValue("@norek", textBoxProjectPengeluaranNoRek.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxProjectPengeluaranNama.Text);
                            cmd.Parameters.AddWithValue("@jumlah", numericUpDownProjectPengeluaranJumlah.Value);
                            cmd.Parameters.AddWithValue("@tanggal", dateTimePickerProjectPengeluaranTanggal.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@deskripsi", richTextBoxProjectPengeluaranDeskripsi.Text);
                            cmd.Parameters.AddWithValue("@tipe", Convert.ToInt32(radioButtonVendor.Checked).ToString());
                            cmd.Parameters.AddWithValue("@kodevendor", (radioButtonVendor.Checked ? comboBoxListVendorProject.SelectedValue.ToString() : "-"));
                            cmd.Parameters.AddWithValue("@id_project", comboBoxListProject.SelectedValue.ToString());
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah Terforward", "Berhasil");
                            ClassConnection.Instance().Close();
                            resetKodePengeluaranProject();
                            textBoxProjectPengeluaranNama.ReadOnly = false;
                            textBoxProjectPengeluaranNoRek.ReadOnly = false;
                            numericUpDownProjectPengeluaranJumlah.Enabled = true;
                            groupBox5.Enabled = true;
                            comboBoxListVendorProject.Enabled = true;
                            richTextBoxProjectPengeluaranDeskripsi.ReadOnly = false;
                            dateTimePickerProjectPengeluaranTanggal.Enabled = true;
                        }
                    }
                    buttonProjectPengeluaranClear.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 22");
                }
            }
        }

        private void ComboBoxListProject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ButtonProjectPemasukanSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "INSERT INTO pettyproject VALUES(@kode,@norek,@nama,@jumlah,@tanggal,@deskripsi,'D','-','-',@kodeproject,2,null,null)";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxProjectPemasukanKode.Text);
                        cmd.Parameters.AddWithValue("@norek", textBoxProjectPemasukanNoRek.Text);
                        cmd.Parameters.AddWithValue("@nama", textBoxProjectPemasukanNama.Text);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownProjectPemasukanJumlah.Value);
                        cmd.Parameters.AddWithValue("@tanggal", dateTimePickerProjectPemasukanTanggal.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@deskripsi", richTextBoxProjectPemasukanDeskripsi.Text);
                        cmd.Parameters.AddWithValue("@kodeproject", (checkBoxUntukProject.Checked ? comboBoxListProject.SelectedValue.ToString() : "-"));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data telah Tersimpan", "Berhasil");

                        if (checkBoxUntukProject.Checked)
                        {
                            commandText = "UPDATE dataproject SET kurang = kurang+@uang WHERE kode = @kode";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@uang", numericUpDownProjectPemasukanJumlah.Value);
                            cmd.Parameters.AddWithValue("@kode", comboBoxListProject.SelectedValue.ToString());
                            rowsAffected = cmd.ExecuteNonQuery();
                        }

                        if (numericUpDownProjectPemasukanJumlah.Value == numericUpDownProjectPemasukanJumlah.Maximum)
                        {
                            commandText = "UPDATE dataproject SET status = 1 WHERE kode = @kode";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", comboBoxListProject.SelectedValue.ToString());
                            rowsAffected = cmd.ExecuteNonQuery();
                        }

                        commandText = "UPDATE saldo SET Money = @uang WHERE Nama = @nama";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@uang", duit[comboBoxTipe.SelectedItem.ToString()] + numericUpDownProjectPemasukanJumlah.Value);
                        cmd.Parameters.AddWithValue("@nama", comboBoxTipe.SelectedItem.ToString());
                        rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Uang telah Terupdate", "Berhasil");
                        ClassConnection.Instance().Close();
                        resetKodePemasukanProject();
                        resetMoney();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 23");
                }
            }
        }

        private void ButtonProjectPemasukanClear_Click(object sender, EventArgs e)
        {
            textBoxProjectPemasukanNama.Text = "";
            textBoxProjectPemasukanNoRek.Text = "";
            numericUpDownProjectPemasukanJumlah.Value = 0;
            richTextBoxProjectPemasukanDeskripsi.Text = "";
        }

        private void ButtonProjectCetakBKSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportProject cr = new CrystalReportProject();
                cr.SetParameterValue("kode", comboBoxProjectCetakBKKode.Text);
                cr.SetParameterValue("kodeproject", comboBoxListProject.SelectedValue);
                ((FormParent)this.MdiParent).panggilTampilProject(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonProjectCetakBMSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                CrystalReportProject cr = new CrystalReportProject();
                cr.SetParameterValue("kode", comboBoxProjectCetakBMKode.Text);
                cr.SetParameterValue("kodeproject", comboBoxListProject.Text);
                ((FormParent)this.MdiParent).panggilTampilProject(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonProjectCetakSaldoSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterSL = new MySqlDataAdapter("SELECT p.kode FROM pettyproject p, dataproject d WHERE d.kode = p.id_project and p.tanggal >= '" + dateTimePickerProjectCetakMulai.Value.ToString("yyyy-MM-dd") + "' and p.tanggal <= '" + dateTimePickerProjectCetakAkhir.Value.ToString("yyyy-MM-dd") + "' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetSL = new DataSet();
                adapterSL.Fill(datasetSL);
                CrystalReportProject cr = new CrystalReportProject();
                string lempar = "";
                foreach (DataRow row in datasetSL.Tables[0].Rows)
                {
                    lempar += row[0].ToString() + ",";
                }
                cr.SetParameterValue("kode", lempar);
                cr.SetParameterValue("kodeproject", comboBoxListProject.Text);
                ((FormParent)this.MdiParent).panggilTampilProject(cr);
                //((FormParent)this.MdiParent).panggilTampil(datasetBM);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonOpsCetakBKSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterBK = new MySqlDataAdapter("SELECT p.kode FROM pettyops p WHERE kode = '" + comboBoxOpsCetakBKKode
                    .SelectedValue.ToString() + "' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBK = new DataSet();
                adapterBK.Fill(datasetBK);
                CrystalReportOpr cr = new CrystalReportOpr();
                cr.SetParameterValue("kode", datasetBK.Tables[0].Rows[0][0].ToString());
                ((FormParent)this.MdiParent).panggilTampilOpr(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonOpsCetakBMSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterBM = new MySqlDataAdapter("SELECT p.kode FROM pettyops p WHERE kode = '" + comboBoxOpsCetakBMKode
                    .SelectedValue.ToString() + "' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBM = new DataSet();
                adapterBM.Fill(datasetBM);
                CrystalReportOpr cr = new CrystalReportOpr();
                cr.SetParameterValue("kode", datasetBM.Tables[0].Rows[0][0].ToString());
                ((FormParent)this.MdiParent).panggilTampilOpr(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonOpsCetakSaldoSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterCS = new MySqlDataAdapter("SELECT p.kode FROM pettyops p WHERE p.tanggal >= '" + dateTimePickerOpsCetakMulai.Value.ToString("yyyy-MM-dd") + "' and p.tanggal <= '" + dateTimePickerOpsCetakAkhir.Value.ToString("yyyy-MM-dd") + "' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetCS = new DataSet();
                adapterCS.Fill(datasetCS);
                CrystalReportOpr cr = new CrystalReportOpr();
                string lempar = "";
                foreach (DataRow row in datasetCS.Tables[0].Rows)
                {
                    lempar += row[0].ToString() + ",";
                }
                cr.SetParameterValue("kode", lempar);
                ((FormParent)this.MdiParent).panggilTampilOpr(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonOpsClearPengeluaran_Click(object sender, EventArgs e)
        {
            if (buttonOpsClearPengeluaran.Text == "Cancel")
            {
                textBoxOpsNamaPengeluaran.ReadOnly = false;
                textBoxOpsNoRekPengeluaran.ReadOnly = false;
                numericUpDownOpsJumlahPengeluaran.Enabled = true;
                richTextBoxOpsDeskripsiPengeluaran.ReadOnly = false;
                dateTimePickerOpsTanggalPengeluaran.Enabled = true;
                checkBoxDividen.Enabled = true;
                buttonOpsClearPengeluaran.Text = "Clear";
            }
            textBoxOpsNamaPengeluaran.Text = "";
            textBoxOpsNoRekPengeluaran.Text = "";
            numericUpDownOpsJumlahPengeluaran.Value = 0;
            richTextBoxOpsDeskripsiPengeluaran.Text = "";
            checkBoxDividen.Checked = false;
            checkBoxApprovedOps.Checked = false;
            resetKodePengeluaranOps();
        }

        private void ButtonOpsClearPemasukan_Click(object sender, EventArgs e)
        {
            textBoxOpsNamaPengeluaran.Text = "";
            textBoxOpsNoRekPengeluaran.Text = "";
            numericUpDownOpsJumlahPengeluaran.Value = 0;
            richTextBoxOpsDeskripsiPemasukan.Text = "";
        }

        private void ButtonOpsSubmitPengeluaran_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (checkBoxApprovedOps.Checked)
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "UPDATE pettyops set status = 2 where kode=@kode";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxOpsKodePengeluaran.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah Tersimpan", "Berhasil");

                            commandText = "UPDATE saldo SET Money = @uang WHERE Nama = @nama";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@uang", duit[comboBoxTipe.SelectedItem.ToString()] - numericUpDownOpsJumlahPengeluaran.Value);
                            cmd.Parameters.AddWithValue("@nama", comboBoxTipe.SelectedItem.ToString());
                            rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Uang telah Terupdate", "Berhasil");
                            if (richTextBoxOpsDeskripsiPengeluaran.Text.Substring(0, 8) == "Karyawan")
                            {
                                commandText = "UPDATE hutangKaryawan SET status = 1 WHERE kodepegawai = @kode and status = 0";
                                cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", textBoxOpsNoRekPengeluaran.Text);
                                rowsAffected = cmd.ExecuteNonQuery();
                            }

                            ClassConnection.Instance().Close();
                            resetKodePengeluaranOps();
                            resetMoney();

                            textBoxOpsNamaPengeluaran.ReadOnly = false;
                            textBoxOpsNoRekPengeluaran.ReadOnly = false;
                            numericUpDownOpsJumlahPengeluaran.Enabled = true;
                            richTextBoxOpsDeskripsiPengeluaran.ReadOnly = false;
                            dateTimePickerOpsTanggalPengeluaran.Enabled = true;
                            checkBoxApprovedOps.Checked = false;
                            checkBoxDividen.Enabled = true;
                            checkBoxDividen.Checked = false;
                            buttonOpsClearPengeluaran.PerformClick();
                        }
                    }
                    else
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "INSERT INTO pettyops VALUES(@kode,@norek,@nama,@jumlah,@tanggal,@deskripsi,'K',@dividen,0,null,null)";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxOpsKodePengeluaran.Text);
                            cmd.Parameters.AddWithValue("@norek", textBoxOpsNoRekPengeluaran.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxOpsNamaPengeluaran.Text);
                            cmd.Parameters.AddWithValue("@jumlah", numericUpDownOpsJumlahPengeluaran.Value);
                            cmd.Parameters.AddWithValue("@tanggal", dateTimePickerOpsTanggalPengeluaran.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@deskripsi", richTextBoxOpsDeskripsiPengeluaran.Text);
                            cmd.Parameters.AddWithValue("@dividen", checkBoxDividen.Checked ? 1 : 0);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah Tersimpan", "Berhasil");
                            ClassConnection.Instance().Close();
                            resetKodePengeluaranOps();
                        }
                    }
                    
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 24");
                }
            }
        }

        private void ButtonKasCetakPJ_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterPJ = new MySqlDataAdapter("SELECT kode FROM pettypj", ClassConnection.Instance().Connection);
                DataSet datasetPJ = new DataSet();
                adapterPJ.Fill(datasetPJ);
                comboBoxKasCetakPJKode.DisplayMember = "kode";
                comboBoxKasCetakPJKode.ValueMember = "kode";
                comboBoxKasCetakPJKode.DataSource = datasetPJ.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 25");
            }
            groupBoxKasCetakSaldo.Visible = false;
            groupBoxKasCetakBK.Visible = false;
            groupBoxKasCetakBM.Visible = false;
            groupBoxKasCetakPJ.Visible = true;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonKembalianYes.Checked)
            {
                label75.Visible = true;
                label114.Visible = true;
                numericUpDownKasPJJumlah.Visible = true;
            }
            else
            {
                label75.Visible = false;
                label114.Visible = false;
                numericUpDownKasPJJumlah.Visible = false;
            }
        }

        private void ButtonKasPJSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "INSERT INTO pettypj VALUES(@kode,@kodeca,@nama,@nota,@sesuai,@kembalian,@jmlbrp,@tanggal,null)";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxKasPJKode.Text);
                        cmd.Parameters.AddWithValue("@kodeca", comboBoxPJ.Text);
                        cmd.Parameters.AddWithValue("@nama", textBoxKasPJNama.Text);
                        cmd.Parameters.AddWithValue("@nota", radioButtonNotaYes.Checked ? '1' : '0');
                        cmd.Parameters.AddWithValue("@sesuai", radioButtonNominalYes.Checked ? '1' : '0');
                        cmd.Parameters.AddWithValue("@kembalian", radioButtonKembalianYes.Checked ? '1' : '0');
                        cmd.Parameters.AddWithValue("@jmlbrp", numericUpDownKasPJJumlah.Value);
                        cmd.Parameters.AddWithValue("@tanggal", dateTimePickerKasPJTanggal.Value.ToString("yyyy-MM-dd"));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data telah Tersimpan", "Berhasil");

                        if (radioButtonKembalianYes.Checked)
                        {
                            commandText = "INSERT INTO pettykas VALUES(@kode,'N','Kembalian',@kodepj,@jumlah,@tanggal,'Kembalian dari Pertanggung Jawaban','D',null,null)";
                            cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKasPemasukanKode.Text);
                            cmd.Parameters.AddWithValue("@kodepj", textBoxKasPJKode.Text);
                            cmd.Parameters.AddWithValue("@jumlah", numericUpDownKasPJJumlah.Value);
                            cmd.Parameters.AddWithValue("@tanggal", dateTimePickerKasPJTanggal.Value.ToString("yyyy-MM-dd"));
                            rowsAffected = cmd.ExecuteNonQuery();
                        }

                        commandText = "UPDATE saldo SET Money = @uang WHERE Nama = @nama";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@uang", duit[comboBoxTipe.SelectedItem.ToString()] + numericUpDownKasPJJumlah.Value);
                        cmd.Parameters.AddWithValue("@nama", comboBoxTipe.SelectedItem.ToString());
                        rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Uang telah Terupdate", "Berhasil");

                        commandText = "UPDATE pettyca SET status = 4 WHERE kode = @kode";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", comboBoxPJ.Text);
                        rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Cash Adv telah terPJ", "Berhasil");

                        
                        ClassConnection.Instance().Close();
                        resetMoney();
                        resetKodePJKas();
                        updateComboPJ();
                        resetKodePemasukanKas();
                        comboBoxPJ.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 25");
                }
            }
        }

        private void ButtonKasPJClear_Click(object sender, EventArgs e)
        {
            textBoxKasPJNama.Text = "";
            numericUpDownKasPJJumlah.Value = 0;
            radioButtonKembalianNo.Checked = true;
            radioButtonNominalNo.Checked = true;
            radioButtonNotaNo.Checked = true;
        }

        private void ButtonKasPengeluaranSubmit_Click(object sender, EventArgs e)
        {
            bool submit = true;
            if (buttonKasPengeluaranSubmit.Text != "Submit")
            {
                submit = false;
            }
            bool lanjut = true;
            if (duit["Kas Kecil"] <= 5000000)
            {
                if (MessageBox.Show("Uang tersisa kurang dari Rp. 5.000.000,- Apakah anda ingin melanjutkan?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    lanjut = false;
                }
            }
            if (lanjut)
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (submit)
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                string commandText = "INSERT INTO pettykas VALUES(@kode,@tipe,@norek,@nama,@jumlah,@tanggal,@deskripsi,'K',null,null)";
                                MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", textBoxKasPengeluaranKode.Text);
                                cmd.Parameters.AddWithValue("@tipe", radioButtonCA.Checked ? 'C' : 'N');
                                cmd.Parameters.AddWithValue("@norek", textBoxKasPengeluaranNoRek.Text == "" ? comboBoxKodeCA.Text : textBoxKasPengeluaranNoRek.Text);
                                cmd.Parameters.AddWithValue("@nama", textBoxKasPengeluaranNama.Text);
                                cmd.Parameters.AddWithValue("@jumlah", numericUpDownKasPengeluaranJumlah.Value);
                                cmd.Parameters.AddWithValue("@tanggal", dateTimePickerKasPengeluaranTanggal.Value.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@deskripsi", richTextBoxKasPengeluaranDeskripsi.Text);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show("Data telah Tersimpan", "Berhasil");

                                commandText = "UPDATE saldo SET Money = @uang WHERE Nama = @nama";
                                cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@uang", duit[comboBoxTipe.SelectedItem.ToString()] - numericUpDownKasPengeluaranJumlah.Value);
                                cmd.Parameters.AddWithValue("@nama", comboBoxTipe.SelectedItem.ToString());
                                rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show("Uang telah Terupdate", "Berhasil");
                                if (radioButtonCA.Checked)
                                {
                                    commandText = "UPDATE pettyca set status = 3 where kode = @kode";
                                    cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                    cmd.Parameters.AddWithValue("@kode", comboBoxKodeCA.Text);
                                    rowsAffected = cmd.ExecuteNonQuery();
                                    MessageBox.Show("Cash Adv telah diapprove", "Berhasil");
                                    updateComboCA();
                                }
                                ClassConnection.Instance().Close();
                                resetMoney();
                                resetKodePengeluaranKas();
                            }
                        }
                        else
                        {
                            if (ClassConnection.Instance().Connecting())
                            {
                                string commandText = "UPDATE pettyca set status = 1 where kode = @kode";
                                MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                                cmd.Parameters.AddWithValue("@kode", comboBoxKodeCA.Text);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show("Data telah terkirim ke Kepala Cabang", "Berhasil");
                                ClassConnection.Instance().Close();
                                updateComboCA();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error 27");
                    }
                }
            }
        }

        private void ButtonKasPengeluaranClear_Click(object sender, EventArgs e)
        {
            textBoxKasPengeluaranNama.Text = "";
            textBoxKasPengeluaranNoRek.Text = "";
            numericUpDownKasPengeluaranJumlah.Value = 0;
            richTextBoxKasPengeluaranDeskripsi.Text = "";
        }

        private void ButtonKasPemasukanSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        string commandText = "INSERT INTO pettykas VALUES(@kode,'N',@norek,@nama,@jumlah,@tanggal,@deskripsi,'D',null,null)";
                        MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@kode", textBoxKasPemasukanKode.Text);
                        cmd.Parameters.AddWithValue("@norek", textBoxKasPemasukanNoRek.Text);
                        cmd.Parameters.AddWithValue("@nama", textBoxKasPemasukanNama.Text);
                        cmd.Parameters.AddWithValue("@jumlah", numericUpDownKasPemasukanJumlah.Value);
                        cmd.Parameters.AddWithValue("@tanggal", dateTimePickerKasPemasukanTanggal.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@deskripsi", richTextBoxKasPemasukanDeskripsi.Text);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Data telah Tersimpan", "Berhasil");

                        commandText = "UPDATE saldo SET Money = @uang WHERE Nama = @nama";
                        cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                        cmd.Parameters.AddWithValue("@uang", duit[comboBoxTipe.SelectedItem.ToString()] + numericUpDownKasPemasukanJumlah.Value);
                        cmd.Parameters.AddWithValue("@nama", comboBoxTipe.SelectedItem.ToString());
                        rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show("Uang telah Terupdate", "Berhasil");
                        ClassConnection.Instance().Close();
                        resetKodePemasukanKas();
                        resetMoney();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 28");
                }
            }
        }
        private void ButtonKasCetakPJSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterPJ = new MySqlDataAdapter("SELECT p.kode FROM pettypj p WHERE p.kode = '" + comboBoxKasCetakPJKode.Text + "'", ClassConnection.Instance().Connection);
                DataSet datasetPJ = new DataSet();
                adapterPJ.Fill(datasetPJ,"DataTableTampilPJ");
                //CrystalReportPJOnline cr = new CrystalReportPJOnline();
                CrystalReportPJ cr = new CrystalReportPJ();
                cr.SetParameterValue("kode", comboBoxKasCetakPJKode.Text);
                ((FormParent)this.MdiParent).panggilTampilPJ(cr);
                //((FormParent)this.MdiParent).panggilTampil(datasetPJ);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonKasCetakSaldoSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter adapterSL = new MySqlDataAdapter("SELECT p.kode FROM pettykas p WHERE p.tanggal >= '" + dateTimePickerKasCetakSaldoMulai.Value.ToString("yyyy-MM-dd") + "' and p.tanggal <= '" + dateTimePickerKasCetakSaldoAkhir.Value.ToString("yyyy-MM-dd") + "' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetSL = new DataSet();
                adapterSL.Fill(datasetSL);
                CrystalReportKas cr = new CrystalReportKas();
                string lempar = "";
                foreach (DataRow row in datasetSL.Tables[0].Rows)
                {
                    lempar += row[0].ToString() + ",";
                }
                cr.SetParameterValue("kode", lempar);
                ((FormParent)this.MdiParent).panggilTampilKas(cr);
                //((FormParent)this.MdiParent).panggilTampil(datasetBM);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                MySqlDataAdapter adapterBM = new MySqlDataAdapter("SELECT p.kode, p.norek, p.nama, p.jumlah, p.tanggal, p.deskripsi, if(p.kodedebitkredit='D','Debit','Kredit') as 'Kode' FROM pettykas p WHERE p.tanggal >= '" + dateTimePickerKasCetakSaldoMulai.Value.ToString("yyyy-MM-dd") + "' and p.tanggal <= '" + dateTimePickerKasCetakSaldoAkhir.Value.ToString("yyyy-MM-dd") + "' order by kode", ClassConnection.Instance().Connection);
                DataSet datasetBM = new DataSet();
                adapterBM.Fill(datasetBM);
                //((FormParent)this.MdiParent).panggilTampil(datasetBM);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonKasCetakBKSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportKas cr = new CrystalReportKas();
                cr.SetParameterValue("kode", comboBoxKasCetakBKKode.Text);
                ((FormParent)this.MdiParent).panggilTampilKas(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonKasCetakBMSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportKas cr = new CrystalReportKas();
                cr.SetParameterValue("kode", comboBoxKasCetakBMKode.Text);
                ((FormParent)this.MdiParent).panggilTampilKas(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ComboBoxPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxKasPJNama.Text = comboBoxPJ.SelectedValue.ToString();
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT count(kode) from pettyliquidpj where kodeca = '"+ comboBoxPJ.Text + "'", ClassConnection.Instance().Connection);
                    if (Convert.ToInt32(cmd.ExecuteScalar())>0)
                    {
                        buttonLiquid.Text = "Sudah Terliquidasi";
                        buttonLiquid.Enabled = false;
                    }
                    else
                    {
                        buttonLiquid.Text = "Liquidasi";
                        buttonLiquid.Enabled = true;
                    }
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error 29");
            }
        }

        private void RadioButtonCA_CheckedChanged(object sender, EventArgs e)
        {
            labelGantiNoRekJadiCombo.Text = "Kode CA";
            comboBoxKodeCA.Visible = true;
            textBoxKasPengeluaranNoRek.Visible = false;
            buttonEditCA.Visible = true;
            textBoxKasPengeluaranNama.ReadOnly = true;
            numericUpDownKasPengeluaranJumlah.Enabled= false;
            updateComboCA();
            if (comboBoxKodeCA.Items.Count == 0 && radioButtonCA.Checked)
            {
                radioButtonNormal.Checked = true;
                MessageBox.Show("Sedang tidak terdapat permintaan cash adv");
            }
        }

        private void RadioButtonNormal_CheckedChanged(object sender, EventArgs e)
        {
            labelGantiNoRekJadiCombo.Text = "Ke No. Rek";
            comboBoxKodeCA.Visible = false;
            textBoxKasPengeluaranNoRek.Visible = true;
            buttonEditCA.Visible = false;
            textBoxKasPengeluaranNama.ReadOnly = false;
            numericUpDownKasPengeluaranJumlah.Enabled = true;
        }

        private void NumericUpDownKasPengeluaranJumlah_ValueChanged(object sender, EventArgs e)
        {
            if (comboBoxTipe.Text == "Project")
            {
                if (numericUpDownKasPengeluaranJumlah.Value >= 1000000 && !checkBoxApproved.Checked)
                {
                    buttonKasPengeluaranSubmit.Text = "Forward KepCabang";
                }
                else
                {
                    buttonKasPengeluaranSubmit.Text = "Submit";
                }
            }
        }

        private void ComboBoxKodeCA_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetDataDariCA();
        }

        private void ButtonEditCA_Click(object sender, EventArgs e)
        {
            ((FormParent)this.MdiParent).panggilCA(comboBoxKodeCA.Text,2);
        }

        private void CheckBoxApproved_CheckedChanged(object sender, EventArgs e)
        {
            buttonKasPengeluaranSubmit.Text = "Submit";
            buttonEditCA.Visible = !checkBoxApproved.Checked;
        }

        private void ButtonNotif_Click(object sender, EventArgs e)
        {
            groupBoxNotifikasi.Visible = !groupBoxNotifikasi.Visible;
            groupBoxNotifikasi.BringToFront();
            if (groupBoxNotifikasi.Visible)
            {
                updateDGVNotif();

            }
        }
        public void updateComboVendorProject()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlDataAdapter adapterPJ = new MySqlDataAdapter("SELECT v.nama, vp.kodevendor from datavendorproject vp, datavendor v where vp.kodevendor = v.kode and (vp.total-vp.sudahdibayar)>0 and vp.kodeproject = '" + comboBoxListProject.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
                    DataSet datasetPJ = new DataSet();
                    adapterPJ.Fill(datasetPJ);
                    comboBoxListVendorProject.DisplayMember = "nama";
                    comboBoxListVendorProject.ValueMember = "kodevendor";
                    comboBoxListVendorProject.DataSource = datasetPJ.Tables[0];
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro 30r");
            }
        }

        public void update_area(string area)
        {
            if (area != "JAKARTA")
            {
                jakartatidak = false;
            }
        }

        public void updateDGVNotif()
        {
            try
            {
                DataTable tmp = new DataTable();
                DataTable tampil = new DataTable();

                tampil.Columns.Add("Tentang");
                tampil.Columns.Add("Kode");
                tampil.Columns.Add("Notifikasi");

                MySqlDataAdapter adapterKlien = new MySqlDataAdapter("SELECT ca.kode,k.nama,concat('Rp. ',format(ca.total,2)),ca.status from pettyca ca, datakaryawan k where ca.kodekaryawan = k.kode and (ca.status = 0 or ca.status = 2) order by kode", ClassConnection.Instance().Connection);

                if (jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT ca.kode,k.nama,concat('Rp. ',format(ca.total,2)),ca.status from pettyca ca, datakaryawan k where ca.kode like 'JCA%' and ca.kodekaryawan = k.kode and (ca.status = 0 or ca.status = 2) order by kode", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT ca.kode,k.nama,concat('Rp. ',format(ca.total,2)),ca.status from pettyca ca, datakaryawan k where ca.kode like 'CA%' and ca.kodekaryawan = k.kode and (ca.status = 0 or ca.status = 2) order by kode", ClassConnection.Instance().Connection);
                }

                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "CashAdv";
                    row[1] = r[0].ToString();
                    row[2] = r[3].ToString() == "0" ? "Anda memiliki Cash Advance yang belum terapprove dari " + r[1].ToString() + " dengan total " + r[2].ToString() : "Anda memiliki Cash Advance yang sudah terapprove kepala cabang oleh " + r[1].ToString() + " dengan total " + r[2].ToString();
                    tampil.Rows.Add(row);
                }

                if (jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT kode from pettyproject where status = 1 and kode like 'JIP%'", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT kode from pettyproject where status = 1 and kode like 'IP%'", ClassConnection.Instance().Connection);
                }

                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Pengeluaran Project";
                    row[1] = r[0].ToString();
                    row[2] = "Anda Memiliki Pengeluaran Project yang sudah Terapprove oleh Kepala Cabang";
                    tampil.Rows.Add(row);
                }

                if (jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT kode,dividen from pettyops where status = 1 and kode like 'JOIP%'", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT kode,dividen from pettyops where status = 1 and kode like 'OIP%'", ClassConnection.Instance().Connection);
                }
                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Pengeluaran Operasional";
                    row[1] = r[0].ToString();
                    row[2] = "Anda Memiliki Pengeluaran Operasional yang sudah Terapprove oleh "+(r[1].ToString() == "0" ? "Kepala Finance" : "CEO");
                    tampil.Rows.Add(row);
                }

                adapterKlien = new MySqlDataAdapter("SELECT kode,concat('Rp. ', format(jumlah,2)), date_format(insertdata,'%d-%m-%Y %H:%i:%s'),tipe from historysaldo where DATEDIFF(NOW(),insertdata) <= 30", ClassConnection.Instance().Connection);

                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Tambahan Saldo";
                    row[1] = r[0].ToString();
                    string x = r[3].ToString() == "o" ? "Operasional" : "Project";
                    row[2] = "Saldo " + x + " anda telah ditambahkan oleh kepala Finance sebesar " + r[1].ToString() + " pada tanggal " + r[2].ToString();
                    tampil.Rows.Add(row);
                }
                adapterKlien = new MySqlDataAdapter("SELECT kode,tahun from notifceotahunan where status = 1", ClassConnection.Instance().Connection);

                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Bonus Tahunan";
                    row[1] = r[0].ToString();
                    row[2] = "Bonus Tahunan pada Tahun " + r[1].ToString() + " telah cair!";
                    tampil.Rows.Add(row);
                }
                adapterKlien = new MySqlDataAdapter("SELECT distinct bp.kodeproject, p.nama from bonusproject bp, dataproject p where p.kode = bp.kodeproject and bp.status = 1", ClassConnection.Instance().Connection);

                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Bonus Project";
                    row[1] = r[0].ToString();
                    row[2] = "Bonus Project untuk Project '" + r[1].ToString() + "' telah cair!";
                    tampil.Rows.Add(row);
                }
                if (jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT v.nama, FORMAT((vp.total-vp.sudahdibayar),2), p.nama, DATE_FORMAT(vp.`jatuhtempo`,'%d-%m-%Y') FROM datavendorproject vp, datavendor v, dataproject p WHERE vp.`kodeproject` like 'JPRJ%' and vp.`kodeproject` = p.kode AND vp.kodevendor = v.kode AND (vp.total-vp.sudahdibayar) > 0 GROUP BY v.nama ORDER BY vp.jatuhtempo", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT v.nama, FORMAT((vp.total-vp.sudahdibayar),2), p.nama, DATE_FORMAT(vp.`jatuhtempo`,'%d-%m-%Y') FROM datavendorproject vp, datavendor v, dataproject p WHERE vp.`kodeproject` like 'PRJ%' and vp.`kodeproject` = p.kode AND vp.kodevendor = v.kode AND (vp.total-vp.sudahdibayar) > 0 GROUP BY v.nama ORDER BY vp.jatuhtempo", ClassConnection.Instance().Connection);
                }
                
                tmp = new DataTable();
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
                        row[2] = "Vendor " + r[0].ToString() + " untuk project "+r[2].ToString()+"sudah melebihi masa jatuh tempo dengan total biaya Rp. " + r[1].ToString();

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

                if (jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.deadline,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.kode like 'JPRJ%' and p.status = 0 and p.kode_klien = k.kode order by p.kode", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.deadline,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.kode like 'PRJ%' and p.status = 0 and p.kode_klien = k.kode order by p.kode", ClassConnection.Instance().Connection);
                }

                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow NewRow = tampil.NewRow();
                    DateTime tanggalTagih = new DateTime(Convert.ToInt32((r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString())), Convert.ToInt32((r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString())), Convert.ToInt32((r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString())));

                    int daysDiff = ((TimeSpan)(tanggalTagih - DateTime.Today)).Days;
                    if (daysDiff < 0)
                    {
                        NewRow[0] = "Peringatan";
                        NewRow[1] = r[0].ToString();
                        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " sudah melebihi masa jatuh tempo pembayaran";

                        tampil.Rows.Add(NewRow);
                    }
                    else if (daysDiff <= 3)
                    {
                        NewRow[0] = "Pemberitahuan";
                        NewRow[1] = r[0].ToString();
                        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " telah memasuki masa tenggang pembayaran yang akan memiliki jatuh tempo " + r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString() + "-" + r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString() + "-" + r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString();
                        tampil.Rows.Add(NewRow);
                    }

                }

                if (jakartatidak == true)
                {
                    adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.tagihan,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.kode like 'JPRJ%' and p.status = 0 and p.kode_klien = k.kode and datediff(now(),p.deadline) < -3 order by p.kode", ClassConnection.Instance().Connection);
                }
                else
                {
                    adapterKlien = new MySqlDataAdapter("SELECT p.kode,k.nama , p.nama, date_format(p.tagihan,'%d%m%Y') as 'tagihan', CONCAT('Rp.',FORMAT(p.totalbiaya,2)) as 'Total',k.kode,(p.kurang-p.totalbiaya) FROM dataproject p, dataklien k where p.kode like 'PRJ%' and p.status = 0 and p.kode_klien = k.kode and datediff(now(),p.deadline) < -3 order by p.kode", ClassConnection.Instance().Connection);
                }
                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow NewRow = tampil.NewRow();
                    DateTime tanggalTagih = new DateTime(Convert.ToInt32((r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString())), Convert.ToInt32((r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString())), Convert.ToInt32((r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString())));

                    int daysDiff = ((TimeSpan)(tanggalTagih - DateTime.Today)).Days;
                    if (daysDiff < 0)
                    {
                        NewRow[0] = "Peringatan Invoice";
                        NewRow[1] = r[0].ToString();
                        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " sudah melebihi masa jatuh tempo invoicing";

                    }
                    else if (daysDiff <= 3)
                    {
                        NewRow[0] = "Pemberitahuan Invoice";
                        NewRow[1] = r[0].ToString();
                        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " telah memasuki masa tenggang yang akan memiliki jatuh tempo invoicing " + r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString() + "-" + r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString() + "-" + r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString();
                    }
                    else
                    {
                        NewRow[0] = "Project Berjalan";
                        NewRow[1] = r[0].ToString();
                        NewRow[2] = "Project " + r[2].ToString() + " oleh klien " + r[1].ToString() + " memiliki jatuh tempo " + r[3].ToString()[0].ToString() + r[3].ToString()[1].ToString() + "-" + r[3].ToString()[2].ToString() + r[3].ToString()[3].ToString() + "-" + r[3].ToString()[4].ToString() + r[3].ToString()[5].ToString() + r[3].ToString()[6].ToString() + r[3].ToString()[7].ToString();
                    }
                    tampil.Rows.Add(NewRow);

                }

                adapterKlien = new MySqlDataAdapter("SELECT tanggal from penggajian where status = 1 group by tanggal", ClassConnection.Instance().Connection);

                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Penggajian";
                    row[1] = r[0].ToString();
                    row[2] = "Anda belum mencetak Slip gaji yang telah diberikan oleh CEO";
                    tampil.Rows.Add(row);
                }



                adapterKlien = new MySqlDataAdapter("select hk.kode, p.nama, hk.jumlah from hutangKaryawan hk, datakaryawan p where p.kode = hk.kodepegawai and hk.status = 0", ClassConnection.Instance().Connection);
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Hutang Karyawan";
                    row[1] = r[0].ToString();
                    row[2] = textInfo.ToTitleCase(r[1].ToString()) + " ingin berhutang sebesar Rp, " + Convert.ToInt64(r[2].ToString()).ToString("N");
                    tampil.Rows.Add(row);
                }


                adapterKlien = new MySqlDataAdapter("select hk.kode, p.nama, date_format(hk.kembali,'%d-%M-%y') from hutangKaryawan hk, datakaryawan p where p.kode = hk.kodepegawai and hk.status = 1", ClassConnection.Instance().Connection);
                textInfo = new CultureInfo("en-US", false).TextInfo;
                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "Pengingat Hutang Karyawan";
                    row[1] = r[0].ToString();
                    row[2] = "Hutang Karyawan akan ditagih pada tanggal " + r[1].ToString();
                    tampil.Rows.Add(row);
                }


                adapterKlien = new MySqlDataAdapter("select dp.kode, p.nama, v.nama from dpvendor dp, dataproject p, datavendor v where dp.kodeproject = p.kode and dp.kodevendor = v.kode and dp.status = 0", ClassConnection.Instance().Connection);
                textInfo = new CultureInfo("en-US", false).TextInfo;
                tmp = new DataTable();
                adapterKlien.Fill(tmp);
                foreach (DataRow r in tmp.Rows)
                {
                    DataRow row = tampil.NewRow();

                    row[0] = "DP Vendor";
                    row[1] = r[0].ToString();
                    row[2] = "Terdapat Request DP Vendor " + r[2].ToString() +" pada Project " + r[1].ToString();
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
                    if (r.Cells[0].Value.ToString() == "CashAdv")
                    {
                        r.DefaultCellStyle.BackColor = Color.HotPink;
                    }
                    else if (r.Cells[0].Value.ToString() == "Pengeluaran Project")
                    {
                        r.DefaultCellStyle.BackColor = Color.DarkOrange;
                    }
                    else if (r.Cells[0].Value.ToString() == "Pengeluaran Operasional")
                    {
                        r.DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                    else if (r.Cells[0].Value.ToString() == "Utang Vendor")
                    {
                        r.DefaultCellStyle.BackColor = Color.DarkRed;
                    }
                    else if (r.Cells[0].Value.ToString() == "Masa Tenggang Vendor")
                    {
                        r.DefaultCellStyle.BackColor = Color.IndianRed;
                    }
                    else if (r.Cells[0].Value.ToString() == "Penggajian")
                    {
                        r.DefaultCellStyle.BackColor = Color.GreenYellow;
                    }
                    else if (r.Cells[0].Value.ToString() == "Bonus Tahunan" || r.Cells[0].Value.ToString() == "Bonus Project")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                    }
                    else if (r.Cells[0].Value.ToString() == "Jatuh Tempo Vendor")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    }
                    else if (r.Cells[0].Value.ToString() == "Pemberitahuan")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightSalmon;
                    }
                    else if (r.Cells[0].Value.ToString() == "Peringatan")
                    {
                        r.DefaultCellStyle.BackColor = Color.Red;
                    }
                    else if (r.Cells[0].Value.ToString() == "Pemberitahuan Invoice")
                    {
                        r.DefaultCellStyle.BackColor = Color.Magenta;
                    }
                    else if (r.Cells[0].Value.ToString() == "Peringatan Invoice")
                    {
                        r.DefaultCellStyle.BackColor = Color.DarkMagenta;
                    }
                    else if (r.Cells[0].Value.ToString() == "Project Berjalan")
                    {
                        r.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                    else if (r.Cells[0].Value.ToString() == "Hutang Karyawan")
                    {
                        r.DefaultCellStyle.BackColor = Color.Lavender;
                    }
                    else if (r.Cells[0].Value.ToString() == "Pengingat Hutang Karyawan")
                    {
                        r.DefaultCellStyle.BackColor = Color.Magenta;
                    }
                    else if (r.Cells[0].Value.ToString() == "DP Vendor")
                    {
                        r.DefaultCellStyle.BackColor = Color.DarkViolet;
                    }
                    else
                    {
                        r.DefaultCellStyle.BackColor = Color.Aqua;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVendor.Checked)
            {
                comboBoxListVendorProject.Enabled = radioButtonVendor.Checked;
                updateComboVendorProject();
            }
        }
        string simpanKodeNotif;
        private void DataGridViewNotif_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "CashAdv")
            {
                groupBoxNotifikasi.Visible = false;
                comboBoxTipe.SelectedIndex = 2;
                buttonKasPengeluaran.PerformClick();
                radioButtonCA.Checked = true;
                comboBoxKodeCA.SelectedIndex = comboBoxKodeCA.FindStringExact(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString());
                //MessageBox.Show("CA");
                if (numericUpDownKasPengeluaranJumlah.Value >= 1000000 && !checkBoxApproved.Checked)
                {
                    buttonKasPengeluaranSubmit.Text = "Forward KepCabang";
                }
                else
                {
                    buttonKasPengeluaranSubmit.Text = "Submit";
                }

            }
            else if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "DP Vendor")
            {
                groupBoxNotifikasi.Visible = false;
                comboBoxTipe.SelectedIndex = 1;
                buttonProjectPengeluaran.PerformClick();
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT k.kode, p.nama, v.nama, dp.jumlah, dp.tanggal from dataproject p, datakaryawan k, datavendor v, dpvendor dp where dp.kodepegawai = k.kode and dp.kodeproject = p.kode and v.kode = dp.kodevendor and dp.kode=" + dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString() + "", ClassConnection.Instance().Connection);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        textBoxProjectPengeluaranNoRek.Text = table.Rows[0][0].ToString();
                        textBoxProjectPengeluaranNama.Text = table.Rows[0][1].ToString();

                        dateTimePickerProjectPengeluaranTanggal.Value = Convert.ToDateTime(table.Rows[0][4].ToString());
                        radioButtonVendor.Checked = true;

                        comboBoxListProject.SelectedIndex = comboBoxListProject.FindStringExact(table.Rows[0][1].ToString());
                        updateComboVendorProject();
                        comboBoxListVendorProject.SelectedIndex = comboBoxListVendorProject.FindStringExact(table.Rows[0][2].ToString());
                        richTextBoxProjectPengeluaranDeskripsi.Text = "DP Vendor " + comboBoxListVendorProject.Text;

                        numericUpDownProjectPengeluaranJumlah.Value = Convert.ToInt64(table.Rows[0][3].ToString());

                        textBoxProjectPengeluaranNoRek.ReadOnly = true;
                        textBoxProjectPengeluaranNama.ReadOnly = true;
                        numericUpDownProjectPengeluaranJumlah.Enabled = false;
                        groupBox5.Enabled = false;
                        comboBoxListVendorProject.Enabled = false;
                        richTextBoxProjectPengeluaranDeskripsi.ReadOnly = true;
                        dateTimePickerProjectPengeluaranTanggal.Enabled = false;

                        ClassConnection.Instance().Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 1");
                }
            }
            else if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Pengeluaran Project")
            {
                groupBoxNotifikasi.Visible = false;
                comboBoxTipe.SelectedIndex = 1;
                buttonProjectPengeluaran.PerformClick();
                checkBoxApproveProject.Checked = true;
                buttonProjectPengeluaranSubmit.Text = "Submit";
                buttonProjectPengeluaranClear.Text = "Cancel";
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT pp.norek,pp.nama,pp.jumlah,pp.tanggal,pp.tipe,pp.kode_vendor,pp.deskripsi,p.nama from pettyproject pp, dataproject p where pp.id_project = p.kode and pp.kode='" + dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", ClassConnection.Instance().Connection);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        textBoxProjectPengeluaranKode.Text = dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString();
                        textBoxProjectPengeluaranNoRek.Text = table.Rows[0][0].ToString();
                        textBoxProjectPengeluaranNama.Text = table.Rows[0][1].ToString();

                        dateTimePickerProjectPengeluaranTanggal.Value = Convert.ToDateTime(table.Rows[0][3].ToString());
                        
                        richTextBoxProjectPengeluaranDeskripsi.Text = table.Rows[0][6].ToString();
                        
                        comboBoxListProject.SelectedIndex = comboBoxListProject.FindStringExact(table.Rows[0][7].ToString());
                        RadioButton[] rd = { radioButtonNormal, radioButtonVendor };

                        rd[Convert.ToInt32(table.Rows[0][4].ToString())].Checked = true;

                        if (radioButtonVendor.Checked)
                        {

                            comboBoxListVendorProject.SelectedValue = table.Rows[0][5].ToString();

                        }
                        numericUpDownProjectPengeluaranJumlah.Value = Convert.ToInt64(table.Rows[0][2].ToString());

                        textBoxProjectPengeluaranNoRek.ReadOnly = true;
                        textBoxProjectPengeluaranNama.ReadOnly = true;
                        numericUpDownProjectPengeluaranJumlah.Enabled = false;
                        groupBox5.Enabled = false;
                        comboBoxListVendorProject.Enabled = false;
                        richTextBoxProjectPengeluaranDeskripsi.ReadOnly = true;
                        dateTimePickerProjectPengeluaranTanggal.Enabled = false;

                        ClassConnection.Instance().Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error 2");
                }
            }
            else if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Hutang Karyawan")
            {
                groupBoxNotifikasi.Visible = false;
                comboBoxTipe.SelectedIndex = 0;
                buttonOpsPengeluaran.PerformClick();
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT hk.kodepegawai,k.nama, hk.jumlah, hk.kembali from hutangKaryawan hk, datakaryawan k where k.kode = hk.kodepegawai and hk.kode='" + dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", ClassConnection.Instance().Connection);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        textBoxOpsNoRekPengeluaran.Text = table.Rows[0][0].ToString();
                        textBoxOpsNamaPengeluaran.Text = table.Rows[0][1].ToString();
                        numericUpDownOpsJumlahPengeluaran.Value = Convert.ToInt64(table.Rows[0][2].ToString());

                        dateTimePickerOpsTanggalPengeluaran.Value = Convert.ToDateTime(table.Rows[0][3].ToString());
                        richTextBoxOpsDeskripsiPengeluaran.Text = "Karyawan "+ table.Rows[0][1].ToString() + " Berhutang";
                        richTextBoxOpsDeskripsiPengeluaran.Enabled = false;
                        checkBoxApprovedOps.Checked = false;

                        ClassConnection.Instance().Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 3");
                }
            }
            else if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Pengeluaran Operasional")
            {
                groupBoxNotifikasi.Visible = false;
                comboBoxTipe.SelectedIndex = 0;
                buttonOpsPengeluaran.PerformClick();
                checkBoxApprovedOps.Checked = true;
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT norek,nama,jumlah,tanggal,deskripsi,dividen from pettyops where kode='" + dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", ClassConnection.Instance().Connection);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        textBoxOpsKodePengeluaran.Text = dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString();

                        textBoxOpsNoRekPengeluaran.Text = table.Rows[0][0].ToString();
                        textBoxOpsNamaPengeluaran.Text = table.Rows[0][1].ToString();
                        numericUpDownOpsJumlahPengeluaran.Value = Convert.ToInt64(table.Rows[0][2].ToString());

                        dateTimePickerOpsTanggalPengeluaran.Value = Convert.ToDateTime(table.Rows[0][3].ToString());
                        richTextBoxOpsDeskripsiPengeluaran.Text = table.Rows[0][4].ToString();
                        checkBoxDividen.Checked = Convert.ToBoolean(Convert.ToInt32(table.Rows[0][5].ToString()));
                        checkBoxApprovedOps.Checked = true;
                        buttonOpsSubmitPengeluaran.Text = "Submit";
                        buttonOpsClearPengeluaran.Text = "Cancel";

                        textBoxOpsNamaPengeluaran.ReadOnly = true;
                        textBoxOpsNoRekPengeluaran.ReadOnly = true;
                        numericUpDownOpsJumlahPengeluaran.Enabled = false;
                        richTextBoxOpsDeskripsiPengeluaran.ReadOnly = true;
                        dateTimePickerOpsTanggalPengeluaran.Enabled = false;
                        checkBoxDividen.Enabled = false;

                        ClassConnection.Instance().Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 4");
                }
            }
            else if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Penggajian")
            {
                groupBoxNotifikasi.Visible = false;
                comboBoxTipe.SelectedIndex = 0;
                buttonApprove.PerformClick();
                try
                {
                    dataGridViewApprove.DataSource = null;
                    MySqlDataAdapter adapter = new MySqlDataAdapter("select k.nama as 'Nama Karyawan',pg.gaji as Gaji,pg.tunjangan as Tunjangan,pg.potongan as Potongan,(pg.gaji+pg.tunjangan-pg.potongan) as 'Total Gaji' from penggajian pg, datakaryawan k where k.kode = pg.kodepegawai and pg.tanggal = '" + dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString() + "'",ClassConnection.Instance().Connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridViewApprove.DataSource = table;
                    radioButtonApproveGaji.Checked = true;
                    buttonApproved.Enabled = true;
                    dataGridViewApprove.Enabled = true;

                    dataGridViewApprove.Columns[1].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewApprove.Columns[1].DefaultCellStyle.Format = "c2";
                    dataGridViewApprove.Columns[1].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
                    dataGridViewApprove.Columns[2].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewApprove.Columns[2].DefaultCellStyle.Format = "c2";
                    dataGridViewApprove.Columns[2].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
                    dataGridViewApprove.Columns[3].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewApprove.Columns[3].DefaultCellStyle.Format = "c2";
                    dataGridViewApprove.Columns[3].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
                    dataGridViewApprove.Columns[4].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewApprove.Columns[4].DefaultCellStyle.Format = "c2";
                    dataGridViewApprove.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");

                    dataGridViewApprove.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    for (int i = 0; i < dataGridViewApprove.ColumnCount; i++)
                    {
                        dataGridViewApprove.Columns[i].ReadOnly = true;
                        dataGridViewApprove.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error Penggajian");
                }
                //buttonPenggajian.PerformClick();
                //comboBoxBulan.SelectedIndex = Convert.ToInt32(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString().Split('-')[0]) - 1;
                //comboBoxTahun.SelectedIndex = comboBoxTahun.FindStringExact(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString().Split('-')[1]);
            }
            else if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Bonus Tahunan")
            {
                groupBoxNotifikasi.Visible = false;
                comboBoxTipe.SelectedIndex = 0;
                buttonApprove.PerformClick();
                try
                {
                    dataGridViewApprove.DataSource = null;
                    MySqlDataAdapter adapter = new MySqlDataAdapter("select p.nama as 'Nama Karyawan',bt.bonus as 'Bonus' from bonustahunan bt, datakaryawan p where p.kode = bt.kodepegawai and bt.status = 1 and bt.kodenotif = '"+ dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", ClassConnection.Instance().Connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridViewApprove.DataSource = table;
                    radioButtonApproveTahunan.Checked = true;
                    buttonApproved.Enabled = true;
                    dataGridViewApprove.Enabled = true;
                    simpanKodeNotif = dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString();
                    dataGridViewApprove.Columns[1].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewApprove.Columns[1].DefaultCellStyle.Format = "c2";
                    dataGridViewApprove.Columns[1].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");

                    dataGridViewApprove.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    for (int i = 0; i < dataGridViewApprove.ColumnCount; i++)
                    {
                        dataGridViewApprove.Columns[i].ReadOnly = true;
                        dataGridViewApprove.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Bonus Tahunan");
                }
                //buttonPenggajian.PerformClick();
                //comboBoxBulan.SelectedIndex = Convert.ToInt32(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString().Split('-')[0]) - 1;
                //comboBoxTahun.SelectedIndex = comboBoxTahun.FindStringExact(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString().Split('-')[1]);
            }
            else if (dataGridViewNotif.Rows[e.RowIndex].Cells[0].Value.ToString() == "Bonus Project")
            {
                groupBoxNotifikasi.Visible = false;
                comboBoxTipe.SelectedIndex = 0;
                buttonApprove.PerformClick();
                try
                {
                    dataGridViewApprove.DataSource = null;
                    MySqlDataAdapter adapter = new MySqlDataAdapter("select p.nama as 'Nama Karyawan',bp.bonus as 'Bonus' from bonusproject bp, datakaryawan p where p.kode = bp.kodepegawai and bp.status = 1 and bp.kodeproject = '" + dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", ClassConnection.Instance().Connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridViewApprove.DataSource = table;
                    radioButtonApproveProject.Checked = true;
                    buttonApproved.Enabled = true;
                    dataGridViewApprove.Enabled = true;
                    simpanKodeNotif = dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString();
                    dataGridViewApprove.Columns[1].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewApprove.Columns[1].DefaultCellStyle.Format = "c2";
                    dataGridViewApprove.Columns[1].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");

                    dataGridViewApprove.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    for (int i = 0; i < dataGridViewApprove.ColumnCount; i++)
                    {
                        dataGridViewApprove.Columns[i].ReadOnly = true;
                        dataGridViewApprove.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Bonus Project");
                }
                //buttonPenggajian.PerformClick();
                //comboBoxBulan.SelectedIndex = Convert.ToInt32(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString().Split('-')[0]) - 1;
                //comboBoxTahun.SelectedIndex = comboBoxTahun.FindStringExact(dataGridViewNotif.Rows[e.RowIndex].Cells[1].Value.ToString().Split('-')[1]);
            }
        }

        private void RadioButtonNrml_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNormal.Checked)
            {
                numericUpDownProjectPengeluaranJumlah.Maximum = long.MaxValue;
                comboBoxListVendorProject.Enabled = radioButtonVendor.Checked;
                comboBoxListVendorProject.SelectedIndex = -1;

            }
        }

        private void ComboBoxListVendorProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxListVendorProject.SelectedIndex != -1)
            {
                try
                {
                    if (ClassConnection.Instance().Connecting())
                    {
                        MySqlCommand cmd = new MySqlCommand("SELECT total-sudahdibayar from datavendorproject where kodevendor = '" + comboBoxListVendorProject.SelectedValue.ToString() + "' and kodeproject = '" + comboBoxListProject.SelectedValue.ToString() + "'", ClassConnection.Instance().Connection);
                        numericUpDownProjectPengeluaranJumlah.Maximum = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                        numericUpDownProjectPengeluaranJumlah.Value = Convert.ToInt64(cmd.ExecuteScalar().ToString());
                        MessageBox.Show("Vendor " + comboBoxListVendorProject.Text + " harus dibayar maksimal Rp. " + numericUpDownProjectPengeluaranJumlah.Maximum.ToString("N"));
                        ClassConnection.Instance().Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 5");
                }
            }
            
        }

        private void NumericUpDownProjectPengeluaranJumlah_ValueChanged(object sender, EventArgs e)
        {
            if (radioButtonVendor.Checked)
            {
                if (numericUpDownProjectPengeluaranJumlah.Value == numericUpDownProjectPengeluaranJumlah.Maximum)
                {
                    MessageBox.Show("Vendor "+comboBoxListVendorProject.Text+ " harus dibayar maksimal Rp. "+ numericUpDownProjectPengeluaranJumlah.Maximum.ToString("N"));
                }
            }
        }

        private void CheckBoxDividen_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxApprovedOps.Checked)
            {
                if (checkBoxDividen.Checked)
                {
                    buttonOpsSubmitPengeluaran.Text = "Forward CEO";
                }
                else
                {
                    buttonOpsSubmitPengeluaran.Text = "Forward Kepala Finance";
                }
            }
        }

        private void DataGridViewNotif_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ButtonLiquid_Click(object sender, EventArgs e)
        {
            ((FormParent)this.MdiParent).panggilLiquid(comboBoxPJ.Text);
        }

        private void ButtonPenggajian_Click(object sender, EventArgs e)
        {
            comboBoxBulan.DataSource = CultureInfo.GetCultureInfo("id-ID").DateTimeFormat.MonthNames.Take(12).ToList();
            comboBoxTahun.DataSource = Enumerable.Range(2000, DateTime.Today.Year-1999).ToList();
            try
            {
                MySqlCommand cmd = new MySqlCommand("select p.kode,p.nama from dataproject p, bonusproject bp where p.kode = bp.kodeproject and bp.status = 2", ClassConnection.Instance().Connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                comboBoxProjectBonus.ValueMember = "kode";
                comboBoxProjectBonus.DisplayMember = "nama";
                comboBoxProjectBonus.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error Gaji Bonus");
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("select kodenotif,tanggal from bonustahunan where status = 2 group by tanggal", ClassConnection.Instance().Connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                comboBoxBonusTahun.ValueMember = "kodenotif";
                comboBoxBonusTahun.DisplayMember = "tanggal";
                comboBoxBonusTahun.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Gaji Bonus Tahunan");
            }
            groupBoxOpsCetakBK.Visible = false;
            groupBoxOpsCetakBM.Visible = false;
            groupBoxOpsCetakSaldo.Visible = false;
            tabControlGajiBonus.Visible = true;
            groupBoxPenggajian.Visible = true;
        }

        private void ButtonCetakPenggajian_Click(object sender, EventArgs e)
        {
            resetKodePengeluaranOps();
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT count(tanggal),status from penggajian where tanggal = '"+(comboBoxBulan.SelectedIndex+1).ToString()+"-"+comboBoxTahun.Text+"' and status = 2", ClassConnection.Instance().Connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows[0][0].ToString() != "0")
                    {
                        int status = Convert.ToInt32(table.Rows[0][1]);
                        ClassConnection.Instance().Close();

                        CrystalReportPenggajian cr = new CrystalReportPenggajian();
                        cr.SetParameterValue("kode", (comboBoxBulan.SelectedIndex + 1).ToString() + "-" + comboBoxTahun.Text);
                        ((FormParent)this.MdiParent).panggilTampilGaji(cr);
                    }
                    else
                    {
                        ClassConnection.Instance().Close();
                        MessageBox.Show("Belum dilakukan penggajian pada bulan dan tahun tersebut");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error Penggajian");
            }
        }

        private void ButtonApprove_Click(object sender, EventArgs e)
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("Pemberitahuan");
            DataRow r = tb.NewRow();
            r[0] = "Silahkan membuka lewat Tabel Notifikasi";
            tb.Rows.Add(r);
            dataGridViewApprove.DataSource = tb;

            dataGridViewApprove.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for (int i = 0; i < dataGridViewApprove.ColumnCount; i++)
            {
                dataGridViewApprove.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewApprove.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            }
            buttonApproved.Enabled = false;
            groupBoxOpsPemasukan.Visible = false;
            groupBoxOpsPengeluaran.Visible = false;
            groupBoxOpsCetak.Visible = false;
            groupBoxApprove.Visible = true;
        }

        private void ButtonApproved_Click(object sender, EventArgs e)
        {
            if (radioButtonApproveGaji.Checked)
            {
                resetKodePengeluaranOps();
                if (ClassConnection.Instance().Connecting())
                {
                    string commandText = "INSERT INTO pettyops VALUES(@kode,@norek,@nama,@jumlah,@tanggal,@deskripsi,'K',1,2,null,null)";
                    MySqlCommand cmdSum = new MySqlCommand("SELECT sum(gaji+tunjangan-potongan) from penggajian where tanggal = '" + (comboBoxBulan.SelectedIndex + 1).ToString() + "-" + comboBoxTahun.Text + "' and status = 1", ClassConnection.Instance().Connection);
                    MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                    long moneyUpdate = Convert.ToInt64(cmdSum.ExecuteScalar());

                    cmd.Parameters.AddWithValue("@kode", textBoxOpsKodePengeluaran.Text);
                    cmd.Parameters.AddWithValue("@norek", "-");
                    cmd.Parameters.AddWithValue("@nama", "Penggajian");
                    cmd.Parameters.AddWithValue("@jumlah", moneyUpdate);
                    cmd.Parameters.AddWithValue("@tanggal", DateTime.Today.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@deskripsi", "Penggajian untuk " + comboBoxBulan.Text.ToString() + " - " + comboBoxTahun.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    commandText = "UPDATE penggajian SET status = 2 WHERE tanggal = @tanggal";
                    cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                    cmd.Parameters.AddWithValue("@tanggal", (comboBoxBulan.SelectedIndex + 1).ToString() + "-" + comboBoxTahun.Text);
                    rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Penggajian Telah Dilakukan", "Berhasil");

                    commandText = "UPDATE saldo SET Money = @uang WHERE Nama = @nama";
                    cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                    cmd.Parameters.AddWithValue("@uang", duit[comboBoxTipe.SelectedItem.ToString()] - moneyUpdate);
                    cmd.Parameters.AddWithValue("@nama", comboBoxTipe.SelectedItem.ToString());
                    rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Uang telah Terupdate", "Berhasil");
                    ClassConnection.Instance().Close();
                    resetKodePengeluaranOps();
                    resetMoney();
                    buttonApprove.PerformClick();
                }
            }
            else if (radioButtonApproveTahunan.Checked)
            {
                resetKodePengeluaranOps();
                if (ClassConnection.Instance().Connecting())
                {
                    string commandText = "INSERT INTO pettyops VALUES(@kode,@norek,@nama,@jumlah,@tanggal,@deskripsi,'K',1,2,null,null)";
                    MySqlCommand cmdSum = new MySqlCommand("SELECT subtotal from notifceotahunan where kode = " + simpanKodeNotif + " and status = 1", ClassConnection.Instance().Connection);
                    MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                    long moneyUpdate = Convert.ToInt64(cmdSum.ExecuteScalar());
                    MySqlCommand cmdAmbilTahun = new MySqlCommand("SELECT tahun from notifceotahunan where kode = '" + simpanKodeNotif + "'",ClassConnection.Instance().Connection);
                    cmd.Parameters.AddWithValue("@kode", textBoxOpsKodePengeluaran.Text);
                    cmd.Parameters.AddWithValue("@norek", "-");
                    cmd.Parameters.AddWithValue("@nama", "Bonus Tahunan");
                    cmd.Parameters.AddWithValue("@jumlah", moneyUpdate);
                    cmd.Parameters.AddWithValue("@tanggal", DateTime.Today.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@deskripsi", "Bonus Tahunan Untuk Tahun "+cmdAmbilTahun.ExecuteScalar().ToString());
                    int rowsAffected = cmd.ExecuteNonQuery();

                    commandText = "UPDATE notifceotahunan SET status = 2 WHERE kode = @kode";
                    cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                    cmd.Parameters.AddWithValue("@kode", simpanKodeNotif);
                    rowsAffected = cmd.ExecuteNonQuery();

                    commandText = "UPDATE bonustahunan SET status = 2 WHERE kodenotif = @kode";
                    cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                    cmd.Parameters.AddWithValue("@kode", simpanKodeNotif);
                    rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Pemberian Bonus Telah Dilakukan", "Berhasil");

                    commandText = "UPDATE saldo SET Money = @uang WHERE Nama = @nama";
                    cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                    cmd.Parameters.AddWithValue("@uang", duit[comboBoxTipe.SelectedItem.ToString()] - moneyUpdate);
                    cmd.Parameters.AddWithValue("@nama", comboBoxTipe.SelectedItem.ToString());
                    rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Uang telah Terupdate", "Berhasil");
                    ClassConnection.Instance().Close();
                    resetKodePengeluaranOps();
                    resetMoney();
                    buttonApprove.PerformClick();
                }
            }
            else if (radioButtonApproveProject.Checked)
            {
                resetKodePengeluaranOps();
                if (ClassConnection.Instance().Connecting())
                {
                    string commandText = "INSERT INTO pettyops VALUES(@kode,@norek,@nama,@jumlah,@tanggal,@deskripsi,'K',1,2,null,null)";
                    MySqlCommand cmdSum = new MySqlCommand("SELECT sum(bonus) from bonusproject where kodeproject = '" + simpanKodeNotif + "' and status = 1", ClassConnection.Instance().Connection);
                    MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                    long moneyUpdate = Convert.ToInt64(cmdSum.ExecuteScalar());
                    MySqlCommand cmdAmbilNamaProject = new MySqlCommand("SELECT nama from dataproject where kode = '" + simpanKodeNotif + "'", ClassConnection.Instance().Connection);
                    cmd.Parameters.AddWithValue("@kode", textBoxOpsKodePengeluaran.Text);
                    cmd.Parameters.AddWithValue("@norek", "-");
                    cmd.Parameters.AddWithValue("@nama", "Bonus Project");
                    cmd.Parameters.AddWithValue("@jumlah", moneyUpdate);
                    cmd.Parameters.AddWithValue("@tanggal", DateTime.Today.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@deskripsi", "Bonus Untuk Project '"+ cmdAmbilNamaProject.ExecuteScalar().ToString()+"'");
                    int rowsAffected = cmd.ExecuteNonQuery();

                    commandText = "UPDATE bonusproject SET status = 2 WHERE kodeproject = @kode";
                    cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                    cmd.Parameters.AddWithValue("@kode", simpanKodeNotif);
                    rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Pemberian Bonus Telah Dilakukan", "Berhasil");

                    commandText = "UPDATE saldo SET Money = @uang WHERE Nama = @nama";
                    cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                    cmd.Parameters.AddWithValue("@uang", duit[comboBoxTipe.SelectedItem.ToString()] - moneyUpdate);
                    cmd.Parameters.AddWithValue("@nama", comboBoxTipe.SelectedItem.ToString());
                    rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Uang telah Terupdate", "Berhasil");
                    ClassConnection.Instance().Close();
                    resetKodePengeluaranOps();
                    resetMoney();
                    buttonApprove.PerformClick();
                }
            }
        }

        private void ButtonCetakBonus_Click(object sender, EventArgs e)
        {
            CrystalReportPembonusanProject cr = new CrystalReportPembonusanProject();
            cr.SetParameterValue("kode", comboBoxProjectBonus.SelectedValue.ToString());
            ((FormParent)this.MdiParent).panggilTampilBonusProject(cr);
        }

        private void ButtonCetakBonusTahun_Click(object sender, EventArgs e)
        {
            CrystalReportPembonusanTahunan cr = new CrystalReportPembonusanTahunan();
            cr.SetParameterValue("kode", comboBoxBonusTahun.SelectedValue.ToString());
            ((FormParent)this.MdiParent).panggilTampilBonusTahun(cr);

        }

        private void groupBoxProject_Enter(object sender, EventArgs e)
        {

        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void label74_Click(object sender, EventArgs e)
        {

        }

        private void label122_Click(object sender, EventArgs e)
        {

        }

        private void labelProjectSaldo_Click(object sender, EventArgs e)
        {

        }

        private void label73_Click(object sender, EventArgs e)
        {

        }
    }
}
