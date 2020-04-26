using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace APC_EO_Finance
{
    public partial class FormAEPO : Form
    {
        public FormAEPO()
        {
            InitializeComponent();
        }
        public int status;
        public string kode;
        public bool jakartatidak = true;
        public void updateDGVCashAdv()
        {
            try
            {
                string cmd = "SELECT Kode, if(paymethod=0, 'Cheque', if(paymethod=1, 'Transfer','Cash')) as 'Payment Method', date_format(datestart,'%d-%m-%Y') as 'Start' , date_format(datepj,'%d-%m-%Y') as 'PJ', if(tipeca = 0,'Operasional','Project') as 'Tipe Cash Adv', Purpose as 'Tujuan CA', concat('Rp. ',format(total,2)) as 'Total CA', if(status = 0,'Pending',if(status = 1,'Sedang di Kepala Cabang',if(status = 2,'Approved',if(status = 3,'Sedang Berjalan','Sudah Pertanggungjawaban')))) as 'Status' from pettyca where kodekaryawan = '" + kode + "' order by kode";
                DataTable tableCAKar = new DataTable();
                dataGridViewCA.DataSource = null;
                dataGridViewCA.Columns.Clear();
                MySqlDataAdapter adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);

                adapterKlien.Fill(tableCAKar);
                dataGridViewCA.DataSource = tableCAKar;

                DataGridViewDisableButtonColumn cetak = new DataGridViewDisableButtonColumn();
                cetak.HeaderText = "Cetak";
                cetak.Text = "Cetak";
                cetak.Name = "Cetak";
                cetak.UseColumnTextForButtonValue = true;
                dataGridViewCA.Columns.Add(cetak);

                DataGridViewDisableButtonColumn edit = new DataGridViewDisableButtonColumn();
                edit.HeaderText = "Edit";
                edit.Text = "Edit";
                edit.Name = "Edit";
                edit.UseColumnTextForButtonValue = true;
                dataGridViewCA.Columns.Add(edit);

                DataGridViewDisableButtonColumn delete = new DataGridViewDisableButtonColumn();
                delete.HeaderText = "Delete";
                delete.Text = "Delete";
                delete.Name = "Delete";
                delete.UseColumnTextForButtonValue = true;
                dataGridViewCA.Columns.Add(delete);
                cmd = "SELECT v.nama, p.nama,dp.jumlah from dpvendor dp, datavendor v, dataproject p where v.kode = dp.kodevendor and p.kode = dp.kodeproject and dp.kodepegawai = '"+kode+"' and dp.status = 1 and datediff(now(),dp.updatedata) <= 30 order by dp.kode";
                DataTable table = (dataGridViewCA.DataSource as DataTable);
                DataTable tmp = new DataTable();
                adapterKlien = new MySqlDataAdapter(cmd, ClassConnection.Instance().Connection);
                adapterKlien.Fill(tmp);
                foreach (DataRow row in tmp.Rows)
                {
                    DataRow r = table.NewRow();
                    r[0] = "DP Vendor";
                    r[1] = row[0].ToString();
                    r[4] = row[1].ToString();
                    r[6] = "Rp. "+Convert.ToInt64(row[2].ToString()).ToString("N");
                    r[7] = "Sudah Selesai";
                    table.Rows.Add(r);
                }

                dataGridViewCA.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                for (int i = 0; i < dataGridViewCA.ColumnCount; i++)
                {
                    dataGridViewCA.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    dataGridViewCA.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }
                for (int i = 0; i < dataGridViewCA.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridViewCA.Rows[i].Cells.Count; j++)
                    {
                        if (dataGridViewCA.Rows[i].Cells[7].Value.ToString() == "Pending")
                        {
                            if (dataGridViewCA.Rows[i].Cells[j].Value.ToString() == "Edit")
                            {
                                DataGridViewDisableButtonCell btn = (DataGridViewDisableButtonCell)dataGridViewCA.Rows[i].Cells[j];
                                btn.Enabled = true;
                            }
                            if (dataGridViewCA.Rows[i].Cells[j].Value.ToString() == "Delete")
                            {
                                DataGridViewDisableButtonCell btn = (DataGridViewDisableButtonCell)dataGridViewCA.Rows[i].Cells[j];
                                btn.Enabled = true;
                            }
                        }
                        if (dataGridViewCA.Rows[i].Cells[j].Value.ToString() == "Cetak")
                        {
                            DataGridViewDisableButtonCell btn = (DataGridViewDisableButtonCell)dataGridViewCA.Rows[i].Cells[j];
                            btn.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormAEPO_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            if (MessageBox.Show("Apakah untuk Jakarta ?", "Pertanyaan Jakarta", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                jakartatidak = false;
            }
        }

        private void DataGridViewCA_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCA.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Edit" && (dataGridViewCA.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewDisableButtonCell).Enabled)
            {
                ((FormParent)this.MdiParent).panggilCA(dataGridViewCA.Rows[e.RowIndex].Cells[0].Value.ToString(), 4);
            }
            if (dataGridViewCA.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Cetak" && (dataGridViewCA.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewDisableButtonCell).Enabled)
            {
                CrystalReportCashAdv cr = new CrystalReportCashAdv();
                cr.SetParameterValue("noca", dataGridViewCA.Rows[e.RowIndex].Cells[0].Value.ToString());
                ((FormParent)this.MdiParent).panggilTampilCA(cr);
            }
            if (dataGridViewCA.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "Delete" && (dataGridViewCA.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewDisableButtonCell).Enabled)
            {
                if (MessageBox.Show("Apakah anda yakin ingin mendelete CA dengan tujuan : '" + dataGridViewCA.Rows[e.RowIndex].Cells[5].Value.ToString() + "' ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "DELETE FROM pettyca WHERE kode = @kode";
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", dataGridViewCA.Rows[e.RowIndex].Cells[0].Value.ToString());
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah terhapus", "Berhasil");
                            ClassConnection.Instance().Close();
                            (this.MdiParent as FormParent).updateDGVCAAEPO();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
        }

        private void TimerDGV_Tick(object sender, EventArgs e)
        {

        }

        private void ButtonNewCA_Click(object sender, EventArgs e)
        {
            ((FormParent)this.MdiParent).panggilCA(kode,1);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ((FormParent)this.MdiParent).newReqHutang(kode);
        }

        private void ButtonDPVendor_Click(object sender, EventArgs e)
        {
            ((FormParent)this.MdiParent).panggilDPVendor(kode);
        }
    }
}