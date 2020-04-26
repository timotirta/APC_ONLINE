using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace APC_EO_Finance
{
    public partial class FormCashAdv : Form
    {
        public FormCashAdv()
        {
            InitializeComponent();
        }
        public string kodePengirim;
        public int status;
        public bool jakartatidak = true;
        public void resetKodeCA()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = (jakartatidak ? "JCA" : "CA") + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from pettyca where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring((jakartatidak ? 9 : 8), 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    labelKodeCA.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Kode");
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
                    MySqlCommand cmd = new MySqlCommand("SELECT ca.kode,ca.paymethod,date_format(ca.datestart,'%d-%m-%Y'),date_format(ca.datepj,'%d-%m-%Y'),ca.tipeca,ca.purpose,ca.datafull,ca.total,k.nama,k.jabatan,k.kode FROM pettyca ca, datakaryawan k where ca.kode ='" + kode + "' and ca.kodekaryawan = k.kode", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    //reader = cmd.ExecuteReader();
                    reader.Fill(table);

                    labelKodeCA.Text = table.Rows[0][0].ToString();
                    RadioButton[] rd = { radioButtonCheque, radioButtonTransfer, radioButtonCash };
                    rd[Convert.ToInt32(table.Rows[0][1].ToString())].Checked = true;
                    dateTimePickerNow.Value = DateTime.ParseExact(table.Rows[0][2].ToString(),"dd-MM-yyyy",CultureInfo.GetCultureInfo("en-US"));
                    dateTimePickerExpected.Value = DateTime.ParseExact(table.Rows[0][3].ToString(), "dd-MM-yyyy", CultureInfo.GetCultureInfo("en-US"));

                    RadioButton[] rd2 = { radioButtonOpr ,radioButtonProject };
                    rd2[Convert.ToInt32(table.Rows[0][4].ToString())].Checked = true;

                    textBoxPurpose.Text = table.Rows[0][5].ToString();

                    string[] datafull = table.Rows[0][6].ToString().Split('|');

                    foreach (string x in datafull)
                    {
                        string[] data = x.Split(';');
                        DataGridViewRow r = (DataGridViewRow)dataGridViewIsi.Rows[0].Clone();
                        r.Cells[0].Value = data[0];
                        r.Cells[1].Value = data[1];
                        r.Cells[2].Value = data[2];
                        r.Cells[3].Value = data[3];
                        r.Cells[4].Value = data[4];
                        r.Cells[5].Value = data[5];
                        dataGridViewIsi.Rows.Add(r);
                    }

                    numericUpDownTotal.Value = Convert.ToInt32(table.Rows[0][7].ToString());
                    labelNama.Text = table.Rows[0][8].ToString();
                    labelDept.Text = table.Rows[0][9].ToString();
                    kodePengirim = table.Rows[0][10].ToString();

                    if (radioButtonProject.Checked)
                    {
                        table = new DataTable();
                        reader = null;
                        cmd = new MySqlCommand("SELECT p.nama from pettyca ca, dataproject p where ca.caproject = p.kode and ca.kode ='"+kode+"'", ClassConnection.Instance().Connection);
                        reader = new MySqlDataAdapter(cmd);
                        //reader = cmd.ExecuteReader();
                        reader.Fill(table);
                        comboBoxProject.SelectedIndex = comboBoxProject.FindStringExact(table.Rows[0][0].ToString());
                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Tampil");
            }
        }
        public void gantiLabel()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT nama,jabatan FROM datakaryawan where kode ='" + kodePengirim + "'", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    //reader = cmd.ExecuteReader();
                    reader.Fill(table);

                    labelNama.Text = table.Rows[0][0].ToString();
                    labelDept.Text = table.Rows[0][1].ToString();

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Label");
            }
        }
        public void checkThisKaryawan()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT status, lastpunish from punishkaryawan where kodekar = '" + kodePengirim+"' and id = (select max(id) from punishkaryawan where kodekar = '"+kodePengirim+"')", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    //reader = cmd.ExecuteReader();
                    reader.Fill(table);

                    if (table.Rows.Count != 0)
                    {
                        DateTime itung = Convert.ToDateTime(table.Rows[0][1].ToString());
                        int[] hitung2 = {0, 3, 7, 14 };
                        DateTime itung2 = itung.AddDays(hitung2[Convert.ToInt32(table.Rows[0][0].ToString())]);

                        if (((TimeSpan)(itung2 - DateTime.Today)).Days > 0)
                        {
                            MessageBox.Show("Anda masih dalam masa punishment");
                            this.MdiParent.Close();
                        }
                        else if (((TimeSpan)(DateTime.Today-itung)).Days > 45)
                        {
                            string commandText = "INSERT INTO punishkaryawan values(null,@kodekar,@status,now(),null,null)";
                            MySqlCommand cmd2 = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd2.Parameters.AddWithValue("@kodekar", kodePengirim);
                            cmd2.Parameters.AddWithValue("@status", 0);
                            int rowsAffected = cmd2.ExecuteNonQuery();
                            ClassConnection.Instance().Close();
                        }
                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Checker");
            }
            
        }
        private void FormCashAdv_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();

            resetKodeCA();
            updateComboProject();

            dataGridViewIsi.Columns[3].ValueType = System.Type.GetType("System.Decimal");
            dataGridViewIsi.Columns[4].ValueType = System.Type.GetType("System.Decimal");
            dataGridViewIsi.Columns[4].DefaultCellStyle.Format = "c2";
            dataGridViewIsi.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
            dataGridViewIsi.Columns[5].ValueType = System.Type.GetType("System.Decimal");
            dataGridViewIsi.Columns[5].DefaultCellStyle.Format = "c2";
            dataGridViewIsi.Columns[5].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
            if (status == 1)
            {
                gantiLabel();
                dateTimePickerNow.MinDate = DateTime.Now;
                dateTimePickerExpected.MinDate = dateTimePickerNow.Value;
                dateTimePickerExpected.MaxDate = dateTimePickerNow.Value.AddDays(4);
                checkThisKaryawan();
                if (MessageBox.Show("Apakah CA untuk Jakarta ?", "Pertanyaan Jakarta", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    jakartatidak = false;
                    resetKodeCA();
                }
            }
            if (status == 4)
            {
                dateTimePickerNow.MinDate = DateTime.MinValue;
                dateTimePickerNow.MaxDate = DateTime.MaxValue;
                dateTimePickerExpected.MinDate = DateTime.MinValue;
                dateTimePickerExpected.MaxDate = DateTime.MaxValue;
            }
        }

        private void DateTimePickerNow_ValueChanged(object sender, EventArgs e)
        {
            if (status == 1)
            {
                dateTimePickerExpected.MaxDate = DateTimePicker.MaximumDateTime;
                dateTimePickerExpected.MinDate = dateTimePickerNow.Value;
                dateTimePickerExpected.MaxDate = dateTimePickerNow.Value.AddDays(4);
            }
            if (status == 4)
            {
                dateTimePickerNow.MinDate = dateTimePickerNow.Value;
                dateTimePickerExpected.MinDate = dateTimePickerNow.Value;
                dateTimePickerExpected.MaxDate = dateTimePickerNow.Value.AddDays(4);
            }
        }

        private void DataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewIsi.Rows[e.RowIndex].Height = 50;

        }

        private void DataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 3 || e.ColumnIndex == 4) && e.RowIndex != dataGridViewIsi.NewRowIndex)
            {
                if (dataGridViewIsi.Rows[e.RowIndex].Cells[3].Value != null && dataGridViewIsi.Rows[e.RowIndex].Cells[4].Value != null)
                {
                    dataGridViewIsi.Rows[e.RowIndex].Cells[5].Value = Convert.ToUInt32(dataGridViewIsi.Rows[e.RowIndex].Cells[3].Value.ToString()) * Convert.ToUInt32(dataGridViewIsi.Rows[e.RowIndex].Cells[4].Value.ToString());
                    numericUpDownTotal.Value = 0;
                    for (int i = 0; i < dataGridViewIsi.Rows.Count-1; i++)
                    {
                        numericUpDownTotal.Value += Convert.ToUInt32(dataGridViewIsi.Rows[i].Cells[5].Value);
                    }
                }
            }
            dataGridViewIsi.Rows[e.RowIndex].Cells[0].Value = e.RowIndex+1;
        }

        private void DataGridViewIsi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void DataGridViewIsi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridViewIsi_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridViewIsi.CurrentCell.ColumnIndex == 3 || dataGridViewIsi.CurrentCell.ColumnIndex == 4)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
            }
        }


        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void DataGridViewIsi_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.Row.IsNewRow)
            {
               (sender as DataGridView).Rows[e.Row.Index].Cells[0].Value = e.Row.Index + 1;
            } 
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            dataGridViewIsi.Rows.Clear();

        }

        private void RadioButtonProject_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxProject.Visible = radioButtonProject.Checked;
            updateComboProject();
        }
        void updateComboProject()
        {
            try
            {
                MySqlDataAdapter adapterLP = new MySqlDataAdapter("SELECT kode,nama FROM dataproject order by kode", ClassConnection.Instance().Connection);
                DataSet datasetLP = new DataSet();
                adapterLP.Fill(datasetLP);
                comboBoxProject.DisplayMember = "nama";
                comboBoxProject.ValueMember = "kode";
                comboBoxProject.DataSource = datasetLP.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error List");
            }
        }
        private void ButtonSubmit_Click(object sender, EventArgs e)
        {

            if (status == 1)
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "INSERT INTO pettyca VALUES(@kode,@paymethod,@datestart,@datepj,@tipeca,@caproject,@purpose,@datafull,@total,@kodekaryawan,0,null,null)";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", labelKodeCA.Text);
                            cmd.Parameters.AddWithValue("@paymethod", radioButtonCheque.Checked ? 0 : radioButtonTransfer.Checked ? 1 : 2);
                            cmd.Parameters.AddWithValue("@datestart", dateTimePickerNow.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@datepj", dateTimePickerExpected.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@tipeca", radioButtonOpr.Checked ? 0 : 1);
                            cmd.Parameters.AddWithValue("@caproject", radioButtonProject.Checked ? comboBoxProject.SelectedValue : "");
                            cmd.Parameters.AddWithValue("@purpose", textBoxPurpose.Text);
                            string datafull = "";
                            foreach (DataGridViewRow row in dataGridViewIsi.Rows)
                            {
                                if (row != dataGridViewIsi.Rows[dataGridViewIsi.NewRowIndex])
                                {
                                    datafull += string.Join(";", row.Cells.Cast<DataGridViewCell>().Select(x => x.Value.ToString()).ToArray()) + "|";
                                }
                            }

                            datafull = datafull.Substring(0, datafull.Length - 1);
                            cmd.Parameters.AddWithValue("@datafull", datafull);
                            cmd.Parameters.AddWithValue("@total", numericUpDownTotal.Value);
                            cmd.Parameters.AddWithValue("@kodekaryawan", kodePengirim);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah Tersimpan", "Berhasil");
                            ClassConnection.Instance().Close();
                            resetKodeCA();
                            buttonClear.PerformClick();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data Harus Terisi Semua", "Error");
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            else if (status == 2 || status == 3 || status == 4)
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "UPDATE pettyca SET paymethod = @paymethod,datestart = @datestart,datepj = @datepj,tipeca = @tipeca,caproject = @caproject,purpose = @purpose,datafull = @datafull,total = @total,kodekaryawan = @kodekaryawan WHERE kode = @kode";

                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", labelKodeCA.Text);
                            cmd.Parameters.AddWithValue("@paymethod", radioButtonCheque.Checked ? 0 : radioButtonTransfer.Checked ? 1 : 2);
                            cmd.Parameters.AddWithValue("@datestart", dateTimePickerNow.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@datepj", dateTimePickerExpected.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@tipeca", radioButtonOpr.Checked ? 0 : 1);
                            cmd.Parameters.AddWithValue("@caproject", radioButtonProject.Checked ? comboBoxProject.SelectedValue : "");
                            cmd.Parameters.AddWithValue("@purpose", textBoxPurpose.Text);
                            string datafull = "";
                            foreach (DataGridViewRow row in dataGridViewIsi.Rows)
                            {
                                if (row != dataGridViewIsi.Rows[dataGridViewIsi.NewRowIndex])
                                {
                                    datafull += string.Join(";", row.Cells.Cast<DataGridViewCell>().Select(x => x.Value.ToString()).ToArray()) + "|";
                                }
                            }
                            datafull = datafull.Substring(0, datafull.Length - 1);
                            cmd.Parameters.AddWithValue("@datafull", datafull);
                            cmd.Parameters.AddWithValue("@total", numericUpDownTotal.Value);
                            cmd.Parameters.AddWithValue("@kodekaryawan", kodePengirim);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (status == 3)
                            {
                                MySqlCommand cmd2 = new MySqlCommand("update pettyca set status = 2 where kode = @kode", ClassConnection.Instance().Connection);

                                cmd2.Parameters.AddWithValue("@kode", labelKodeCA.Text);

                                rowsAffected = cmd2.ExecuteNonQuery();
                            }
                            
                            MessageBox.Show("Data telah Tersimpan", "Berhasil");
                            ClassConnection.Instance().Close();
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Data Harus Terisi Semua", "Error");
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
        }

        private void DataGridViewIsi_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridViewIsi.Rows[e.RowIndex].Height = 50;
        }

        private void FormCashAdv_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (status == 2)
            {
                ((FormParent)this.MdiParent).updateComboCA(labelKodeCA.Text);
            }
            else if (status == 3)
            {
                ((FormParent)this.MdiParent).updateDGVNotif();
            }
            else if (status == 4)
            {
                ((FormParent)this.MdiParent).updateDGVCAAEPO();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CrystalReportCashAdv cr = new CrystalReportCashAdv();
                cr.SetParameterValue("noca", labelKodeCA.Text);
                ((FormParent)this.MdiParent).panggilTampilCA(cr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
