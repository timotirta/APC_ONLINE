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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormAPC_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();
            
            groupBoxLogin.Visible = true;
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            string[] tipe = { "CEO", "KEPALA CABANG", "FINANCE", "PETTY", "AR","AP", "ACCOUNTING", "KARYAWAN", "AR_AP" };
            string area = "";
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT kode, nama, pass, jabatan,user,area FROM datakaryawan where upper(kode) ='" + textUser.Text.ToUpper() + "' or upper(user) = '" + textUser.Text.ToUpper()+ "' or upper(nama) = '" + textUser.Text.ToUpper()+"'", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    //reader = cmd.ExecuteReader();
                    reader.Fill(table);
                    foreach (DataRow r in table.Rows)
                    {
                        if (textUser.Text.ToUpper() == r[1].ToString().ToUpper() || textUser.Text.ToUpper() == r[4].ToString().ToUpper() || textUser.Text.ToUpper() == r[0].ToString().ToUpper())
                        {
                            if (textPass.Text.ToUpper().ToUpper() == r[2].ToString().ToUpper())
                            {
                                ((FormParent)this.MdiParent).statusForm = 8;
                                for (int i = 0; i < tipe.Length; i++)
                                {
                                    if (r[3].ToString().ToUpper() == tipe[i])
                                    {
                                        ((FormParent)this.MdiParent).statusForm = i + 1;

                                        area = r[5].ToString().ToUpper();
                                        break;
                                    }
                                }
                                if (((FormParent)this.MdiParent).statusForm == 8)
                                {
                                    ((FormParent)this.MdiParent).gantiData(r[0].ToString().ToUpper());
                                }
                            }
                        }
                    }

                    if (((FormParent)this.MdiParent).statusForm <= 0)
                    {
                        MessageBox.Show("Username / Password Salah", "Wrong");
                    }
                    else
                    {
                        ((FormParent)this.MdiParent).doAfterLogin(textUser.Text,area);
                        this.Close();
                    }
                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void TextUser_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                buttonLogin.PerformClick();
            }
        }

        private void TextPass_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                buttonLogin.PerformClick();
            }
        }
    }
}
