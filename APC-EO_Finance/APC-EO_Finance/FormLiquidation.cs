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
    public partial class FormLiquidation : Form
    {
        public FormLiquidation()
        {
            InitializeComponent();
        }
        public string tempDatafull;
        public string kodePengirim;
        public int totalTemp;
        public void tampilData(string kode)
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT ca.kode,ca.paymethod,date_format(ca.datestart,'%d/%m/%Y'),date_format(ca.datepj,'%d/%m/%Y'),ca.tipeca,ca.purpose,ca.datafull,ca.total,k.nama,k.jabatan,k.kode FROM pettyca ca, datakaryawan k where ca.kode ='" + kode + "' and ca.kodekaryawan = k.kode", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    //reader = cmd.ExecuteReader();
                    reader.Fill(table);

                    labelKodeCA.Text = table.Rows[0][0].ToString();
                    labelPayMethod.Text = table.Rows[0][1].ToString() == "0" ? "Cheque" : table.Rows[0][1].ToString() == "1" ? "Transfer" : "Cash";
                    labelDateCA.Text = table.Rows[0][2].ToString();
                    labelDatePJ.Text = table.Rows[0][3].ToString();
                    labelTipeCA.Text = table.Rows[0][4].ToString() == "0" ? "Operasional" : "Project";

                    labelPurpose.Text = table.Rows[0][5].ToString();

                    tempDatafull = table.Rows[0][6].ToString();
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
                    numericUpDownBaru.Value = numericUpDownTotal.Value;
                    labelNama.Text = table.Rows[0][8].ToString();
                    labelDept.Text = table.Rows[0][9].ToString();
                    kodePengirim = table.Rows[0][10].ToString();

                    if (labelTipeCA.Text == "Project")
                    {
                        table = new DataTable();
                        reader = null;
                        cmd = new MySqlCommand("SELECT p.nama from pettyca ca, dataproject p where ca.caproject = p.kode and ca.kode ='" + kode + "'", ClassConnection.Instance().Connection);
                        reader = new MySqlDataAdapter(cmd);
                        //reader = cmd.ExecuteReader();
                        reader.Fill(table);
                        labelTipeCA.Text += " "+table.Rows[0][0].ToString();
                    }

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Tampil");
            }
        }
        private void FormLiquidation_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            dataGridViewIsi.Columns[3].ValueType = System.Type.GetType("System.Decimal");
            dataGridViewIsi.Columns[4].ValueType = System.Type.GetType("System.Decimal");
            dataGridViewIsi.Columns[4].DefaultCellStyle.Format = "c2";
            dataGridViewIsi.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
            dataGridViewIsi.Columns[5].ValueType = System.Type.GetType("System.Decimal");
            dataGridViewIsi.Columns[5].DefaultCellStyle.Format = "c2";
            dataGridViewIsi.Columns[5].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");

        }

        private void DataGridViewIsi_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewIsi.Rows[e.RowIndex].Height = 50;
        }

        private void DataGridViewIsi_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 3 || e.ColumnIndex == 4) && e.RowIndex != dataGridViewIsi.NewRowIndex)
            {
                if (dataGridViewIsi.Rows[e.RowIndex].Cells[3].Value != null && dataGridViewIsi.Rows[e.RowIndex].Cells[4].Value != null)
                {
                    dataGridViewIsi.Rows[e.RowIndex].Cells[5].Value = Convert.ToUInt32(dataGridViewIsi.Rows[e.RowIndex].Cells[3].Value.ToString()) * Convert.ToUInt32(dataGridViewIsi.Rows[e.RowIndex].Cells[4].Value.ToString());
                    numericUpDownBaru.Value = 0;
                    for (int i = 0; i < dataGridViewIsi.Rows.Count - 1; i++)
                    {
                        numericUpDownBaru.Value += Convert.ToUInt32(dataGridViewIsi.Rows[i].Cells[5].Value);
                    }
                    totalTemp = Convert.ToInt32(numericUpDownBaru.Value);
                }
            }
            dataGridViewIsi.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
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

        private void CheckBoxPembulatan_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownBaru.Value = checkBoxPembulatan.Checked ? Convert.ToInt32(Math.Floor(numericUpDownBaru.Value / 1000)) * 1000 : Convert.ToDecimal(totalTemp);
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            dataGridViewIsi.Rows.Clear();
            string[] datafull = tempDatafull.Split('|') ;

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
            totalTemp = Convert.ToInt32(numericUpDownTotal.Value);
            numericUpDownBaru.Value = numericUpDownTotal.Value;
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Nominal melebihi dari yang lama. Apakah benar?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "INSERT INTO pettyliquidpj VALUES(null,@kodeca,@datafullbaru,@pembulatan,@jmlbaru,null,null)";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kodeca", labelKodeCA.Text);
                            string datafullbaru = "";
                            foreach (DataGridViewRow row in dataGridViewIsi.Rows)
                            {
                                if (row != dataGridViewIsi.Rows[dataGridViewIsi.NewRowIndex])
                                {
                                    datafullbaru += string.Join(";", row.Cells.Cast<DataGridViewCell>().Select(x => x.Value.ToString()).ToArray()) + "|";
                                }
                            }
                            cmd.Parameters.AddWithValue("@datafullbaru", datafullbaru);
                            cmd.Parameters.AddWithValue("@pembulatan", checkBoxPembulatan.Checked ? 1 : 0);
                            cmd.Parameters.AddWithValue("@jmlbaru", numericUpDownBaru.Value);

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
