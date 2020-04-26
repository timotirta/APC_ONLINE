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
    public partial class FormPenggajian : Form
    {
        public FormPenggajian()
        {
            InitializeComponent();
        }
        public void updateDGVGaji(string kode = "")
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT pg.Kode,k.Nama,pg.Gaji, pg.Tunjangan, pg.Potongan from penggajian pg, datakaryawan k where pg.kodepegawai = k.kode and pg.status = 0 and k.area = 'Jakarta' and pg.tanggal = '" + kode+"' order by kode ", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    reader.Fill(table);
                    dataGridViewGaji.DataSource = table;

                    dataGridViewGaji.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dataGridViewGaji.Columns[2].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewGaji.Columns[2].DefaultCellStyle.Format = "c2";
                    dataGridViewGaji.Columns[2].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
                    dataGridViewGaji.Columns[3].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewGaji.Columns[3].DefaultCellStyle.Format = "c2";
                    dataGridViewGaji.Columns[3].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
                    dataGridViewGaji.Columns[4].ValueType = System.Type.GetType("System.Decimal");
                    dataGridViewGaji.Columns[4].DefaultCellStyle.Format = "c2";
                    dataGridViewGaji.Columns[4].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("id-ID");
                    for (int i = 0; i < dataGridViewGaji.ColumnCount; i++)
                    {

                        int[] contain = { 2, 3, 4 };
                        if (!contain.Contains(i)) dataGridViewGaji.Columns[i].ReadOnly = true;
                        dataGridViewGaji.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        dataGridViewGaji.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                    }
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void FormPenggajian_Load(object sender, EventArgs e)
        {
            updateDGVGaji();

        }

        private void FormPenggajian_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((FormParent)this.MdiParent).updateDGVNotif();
        }

        private void ButtonSubForward_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    DataTable dt = (DataTable)dataGridViewGaji.DataSource;
                    foreach (DataRow r in dt.Rows)
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "UPDATE penggajian set gaji = @gaji, tunjangan = @tunjangan, potongan = @potongan, status = 1 where kode = @kode";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", r[0].ToString());
                            cmd.Parameters.AddWithValue("@gaji", Convert.ToInt32(r[2].ToString()));
                            cmd.Parameters.AddWithValue("@tunjangan", Convert.ToInt32(r[3].ToString()));
                            cmd.Parameters.AddWithValue("@potongan", Convert.ToInt32(r[4].ToString()));
                            int rowsAffected = cmd.ExecuteNonQuery();
                            ClassConnection.Instance().Close();
                        }
                    }
                    MessageBox.Show("Gaji telah cair dan terlempar pada Petty", "Berhasil");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
    }
}
