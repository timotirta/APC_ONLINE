using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APC_EO_Finance
{
    public partial class FormLoading : Form
    {
        public FormLoading()
        {
            InitializeComponent();
        }

        private void FormLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((FormParent)this.MdiParent).panggilLogin();
        }
        int counting = 1;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            counting += 1;
            if (counting >= 30)
            {
                this.Close();
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
