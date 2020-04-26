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
    public partial class FormParent : Form
    {
        public int countDown = 0;
        public string area = "";
        public FormParent()
        {
            InitializeComponent();
            GlobalMouseHandler gmh = new GlobalMouseHandler();
            gmh.TheMouseMoved += new MouseMovedEvent(Gmh_TheMouseMoved);
            Application.AddMessageFilter(gmh);

        }
        Point init_pos;
        private void Gmh_TheMouseMoved()
        {
            Point cur_pos = System.Windows.Forms.Cursor.Position;
            if (cur_pos != init_pos)
            {
                init_pos = cur_pos;
                countDown = 0;
            }
        }

        string kodeuser;
        public void gantiData(string data)
        {
            kodeuser = data;
        }
        public int statusForm=0;
        private void FormParent_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().DatabaseName = "btwo";
            this.IsMdiContainer = true;

            FormLoading f = new FormLoading();
            f.MdiParent = this;
            f.FormBorderStyle = FormBorderStyle.None;
            f.ControlBox = false;
            f.Size = this.Size;
            f.Show();
            f.Location = new Point(0, 0);
        }
        public void panggilLogin()
        {
            FormLogin f = new FormLogin();
            f.MdiParent = this;
            f.FormBorderStyle = FormBorderStyle.None;
            f.ControlBox = false;
            f.Show();
            f.Location = new Point(0, 0);
        }
        public void doAfterLogin(string userSkr,string area)
        {
            this.area = area;
            if (statusForm == 1)
            {
                FormCEO f = new FormCEO();
                f.MdiParent = this;
                f.FormBorderStyle = FormBorderStyle.None;
                f.ControlBox = false;
                f.Show();
                f.Location = new Point(0, 0);
            }
            if (statusForm == 2)
            {
                FormKepCab f = new FormKepCab();
                f.MdiParent = this;
                f.FormBorderStyle = FormBorderStyle.None;
                f.ControlBox = false;
                f.update_area(area);
                f.Show();
                f.Location = new Point(0, 0);
            }
            else if (statusForm == 3)
            {
                FormFinance f = new FormFinance();
                f.MdiParent = this;
                f.FormBorderStyle = FormBorderStyle.None;
                f.ControlBox = false;
                f.Show();
                f.Location = new Point(0, 0);
            }
            if (statusForm == 4)
            {
                FormPetty f = new FormPetty();
                f.MdiParent = this;
                f.FormBorderStyle = FormBorderStyle.None;
                f.ControlBox = false;
                f.update_area(area);
                f.Show();
                f.Location = new Point(0, 0);
            }
            else if (statusForm == 5)
            {
                tambahMenuStrip();
            }
            else if (statusForm == 6)
            {
                tambahMenuStrip();
            }
            else if (statusForm == 7)
            {
                FormAccounting f = new FormAccounting();
                f.MdiParent = this;
                f.FormBorderStyle = FormBorderStyle.None;
                f.ControlBox = false;
                f.Show();
                f.Location = new Point(0, 0);
            }
            else if (statusForm == 8)
            {
                FormAEPO f = new FormAEPO();
                f.MdiParent = this;
                f.FormBorderStyle = FormBorderStyle.None;
                f.ControlBox = false;
                f.kode = kodeuser;
                f.status = 1;
                f.Text = "History Cash Adv";
                f.Show();
                f.Location = new Point(0, 0);
                f.updateDGVCashAdv();
                f.dataGridViewCA.Invalidate();
            }
            else if (statusForm == 9) 
            {

                tambahMenuStrip();
            }
        }
        public void tambahMenuStrip()
        {

            MenuStrip m = new MenuStrip();
            m.Items.Add("Form AP");
            m.Items.Add("Form AR");
            m.Items[0].Click += FormAPHandler;
            m.Items[1].Click += FormARHandler;
            this.Controls.Add(m);
        }
        private void FormARHandler(object sender, EventArgs e)
        {
            FormAR f = new FormAR();
            f.MdiParent = this;
            f.update_area(area);
            f.Show();
            f.Location = new Point(0, 0);
        }
        private void FormAPHandler(object sender, EventArgs e)
        {
            FormAP f = new FormAP();
            f.MdiParent = this;
            f.update_area(area);
            f.Show();
            f.Location = new Point(0, 0);
        }
        public void panggilKlien(int status = 1, string kode = "")
        {
            FormKlien f = new FormKlien();
            f.status = status;

            f.MdiParent = this;
            f.Show();
            f.Text = "Insert Klien";
            if (status == 2 || status == 3)
            {
                if (status == 2)
                {
                    f.Text = "Update Klien";
                }
                else if (status == 3)
                {
                    f.Text = "View Klien";

                    f.buttonSubmitKlien.Visible = false;
                    f.buttonClearKlien.Visible = false;
                    f.textBoxAlamatKlien.ReadOnly = true;
                    f.textBoxEmailKlien.ReadOnly = true;
                    f.textBoxFaxKlien.ReadOnly = true;
                    f.textBoxNamaKlien.ReadOnly = true;
                    f.textBoxNoTelpKlien.ReadOnly = true;
                    f.textBoxNPWPKlien.ReadOnly = true;
                }
                f.tampilData(kode);

            }
        }
        public void panggilCA(string kode = "", int status = 2)
        {
            FormCashAdv f = new FormCashAdv();
            f.status = status;
            f.MdiParent = this;
            f.kodePengirim = kode;
            f.Show();
            f.Text = "Insert Cash Adv";
            if (status >= 2)
            {
                f.Text = "Update Cash Adv";
                f.kodePengirim = "";
                f.tampilData(kode);
            }
        }

        public void panggilDPVendor(string kode = "")
        {
            FormDPVendor f = new FormDPVendor();
            f.MdiParent = this;
            f.kode = kode;
            f.Show();
            f.tampilData(kode);
        }
        public void panggilLiquid(string kode = "")
        {
            FormLiquidation f = new FormLiquidation();
            f.MdiParent = this;
            f.Show();
            f.tampilData(kode);
            f.Text = "Insert Liquid";
        }
        public void panggilPenggajian(string kode = "")
        {
            FormPenggajian f = new FormPenggajian();
            f.MdiParent = this;
            f.Show();
            f.updateDGVGaji(kode);
            f.Text = "Approve Penggajian";
        }
        public void panggilAccPengeluaranKepCab(string kode = "")
        {
            FormAccPengeluaran f = new FormAccPengeluaran();
            f.MdiParent = this;
            f.Show();
            f.Text = "Approve Pengeluaran";
            f.ambilBuatKepCab(kode);
        }
        public void newReqHutang(string kode)
        {
            FormHutang f = new FormHutang();
            f.MdiParent = this;
            f.Show();
            f.tampilData(kode);
            f.Text = "New Hutang";
        }
        public void panggilAccBonus(string kode = "", int status = 0)
        {
            FormBonusCEO f = new FormBonusCEO();
            f.MdiParent = this;
            f.Show();
            f.Text = "Approve Pengeluaran";
            if (status == 0)
            {
                f.tampilDataProject(kode);
            }
            else
            {
                f.tampilDataTahunan(kode);
            }
        }
        public void panggilAccPengeluaranOps(string kode = "",int status = 0)
        {
            FormAccPengeluaran f = new FormAccPengeluaran();
            f.MdiParent = this;
            f.Show();
            f.Text = "Approve Pengeluaran";
            f.ambilBuatOps(kode,status);
        }
        public void panggilProject(int status = 1, string kode = "")
        {
            FormProject f = new FormProject();
            f.status = status;
            f.MdiParent = this;
            f.Show();
            f.Text = "Insert Project";
            if (status == 2 || status == 3)
            {
                if (status == 2)
                {
                    f.Text = "Update Project";
                }
                else if (status == 3)
                {
                    f.Text = "View Project";
                }
                f.tampilData(kode);

            }
        }
        public void panggilTambahAkun(int status = 1, string kode="")
        {
            FormTambahAkun f = new FormTambahAkun();
            f.status = status;
            f.MdiParent = this;
            f.Show();
            f.Text = "Insert Akun";
            if (status == 2)
            {
                f.Text = "Update Akun";
                f.tampilData(kode);
            }
        }
        public void panggilVendor(int status = 1, string kode = "")
        {
            FormVendor f = new FormVendor();
            f.status = status;
            f.MdiParent = this;
            f.Show();
            f.Text = "Insert Vendor";
            if (status == 2 || status == 3)
            {
                if (status == 2)
                {
                    f.Text = "Update Vendor";
                }
                else if (status == 3)
                {
                    f.Text = "View Vendor";
                }
                f.tampilData(kode);

            }
        }

        public void panggilItem(int status = 1,string kode2="", string kode = "", string nama = "")
        {
            FormItem f = new FormItem();
            f.status = status;
            f.kodevendor = kode;
            f.namavendor = nama;
            f.MdiParent = this;
            f.Show();
            f.Text = "Insert Item";
            if (status == 2 || status == 3)
            {
                if (status == 2)
                {
                    f.Text = "Update Item";
                }
                else if (status == 3)
                {
                    f.Text = "View Item";
                }
                f.tampilData(kode2);

            }
        }
        public void panggilKaryawan(int status = 1, string kode = "")
        {
            FormKaryawan f = new FormKaryawan();
            f.status = status;
            f.MdiParent = this;
            f.Show();
            f.Text = "Insert Karyawan";
            if (status == 2 || status == 3)
            {
                if (status == 2)
                {
                    f.Text = "Update Karyawan";
                }
                else if (status == 3)
                {
                    f.Text = "View Karyawan";
                }
                f.tampilData(kode);
            }
        }
        public void panggilAddProjectACC()
        {
            FormAddProjectACC f = new FormAddProjectACC();
            f.MdiParent = this;
            f.Show();
            f.Text = "Insert Project";
        }
        public void updateDGVKlien()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormAR")
                {
                    ((FormAR)this.MdiChildren[i]).updateDGVKlien();
                }
            }
        }
        public void updateComboProject()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormAccounting")
                {
                    ((FormAccounting)this.MdiChildren[i]).updateComboProject();
                }
            }
        }

        public void updateDGVItem()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormAP")
                {
                    ((FormAP)this.MdiChildren[i]).updateDGVItem();
                }
            }
        }
        public void updateDGVProject()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormAR")
                {
                    ((FormAR)this.MdiChildren[i]).updateDGVProject();
                    ((FormAR)this.MdiChildren[i]).updateNotif();
                }
            }
        }
        public void updateDGVKaryawan()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormKepCab")
                {
                    ((FormKepCab)this.MdiChildren[i]).updateDGVKaryawan();
                }
                if (this.MdiChildren[i].Text == "FormFinance")
                {
                    ((FormFinance)this.MdiChildren[i]).updateDGVKaryawan();

                }
            }
        }
        public void updateDGVVendor()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormAP")
                {
                    ((FormAP)this.MdiChildren[i]).updateDGVVendor();
                }
            }
        }
        public void updateComboCA(string kode = "")
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormPetty")
                {
                    ((FormPetty)this.MdiChildren[i]).updateComboCA(kode);
                }
            }
        }
        public void updateComboAcc()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormAccounting")
                {
                    ((FormAccounting)this.MdiChildren[i]).updateComboAkun();
                }
            }
        }
        public void updateComboProjectAcc()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormAccounting")
                {
                    ((FormAccounting)this.MdiChildren[i]).updateComboProject();
                }
            }
        }
        public void updateDGVNotif()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormKepCab")
                {
                    ((FormKepCab)this.MdiChildren[i]).updateDGVNotif();
                }
            }
        }
        public void updateDGVCAAEPO()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "History Cash Adv")
                {
                    ((FormAEPO)this.MdiChildren[i]).updateDGVCashAdv();
                    ((FormAEPO)this.MdiChildren[i]).dataGridViewCA.Invalidate();
                }
            }
        }
        public void updateDGVNotifFin()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormFinance")
                {
                    ((FormFinance)this.MdiChildren[i]).updateDGVNotif();
                }
            }
        }
        public void updateDGVNotifCeo()
        {
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                if (this.MdiChildren[i].Text == "FormCEO")
                {
                    ((FormCEO)this.MdiChildren[i]).updateDGVNotif();
                }
            }
        }
        void bersikanReport (FormTampilReport f)
        {
            f.crkas = null;
            f.cropr = null;
            f.crproject = null;
            f.crvendor = null;
        }
        public void panggilTampilOpr(CrystalReportOpr cropr)
        {
            FormTampilReport f = new FormTampilReport();
            f.cropr = cropr;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilJurnalLB(CrystalReportJurnalLabaRugi crlb)
        {
            FormTampilReport f = new FormTampilReport();
            f.crlb = crlb;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilJurnalLBTahun(CrystalReportJurnalLabaRugiTahun crlbt)
        {
            FormTampilReport f = new FormTampilReport();
            f.crlbt = crlbt;
            f.MdiParent = this;
            f.Show();
        }
            public void panggilTampilLPK(CrystalReportLPK crlpk)
        {
            FormTampilReport f = new FormTampilReport();
            f.crlpk = crlpk;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilJurnal(CrystalReportJurnalAcc crja)
        {
            FormTampilReport f = new FormTampilReport();
            f.crja = crja;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilBonusProject(CrystalReportPembonusanProject crbnspr)
        {
            FormTampilReport f = new FormTampilReport();
            f.crbnspr = crbnspr;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilBonusTahun(CrystalReportPembonusanTahunan crbnsth)
        {
            FormTampilReport f = new FormTampilReport();
            f.crbnsth = crbnsth;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilPJ(CrystalReportPJ crpj)
        {
            FormTampilReport f = new FormTampilReport();
            f.crpj = crpj;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilVendor(CrystalReportPrintVendor crvendor)
        {
            FormTampilReport f = new FormTampilReport();
            f.crvendor = crvendor;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilBayar(CrystalReportBuktiBayar crbayar)
        {
            FormTampilReport f = new FormTampilReport();
            f.crbayar = crbayar;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilGiro(CrystalReportGiro crgiro)
        {
            FormTampilReport f = new FormTampilReport();
            f.crgiro = crgiro;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilProject(CrystalReportProject crproject)
        {
            FormTampilReport f = new FormTampilReport();
            f.crproject = crproject;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilKas(CrystalReportKas crkas)
        {
            FormTampilReport f = new FormTampilReport();
            f.crkas = crkas;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilGaji(CrystalReportPenggajian crgaji)
        {
            FormTampilReport f = new FormTampilReport();
            f.crgaji = crgaji;
            f.MdiParent = this;
            f.Show();
        }
        public void panggilTampilPO(CrystalReportPOVendor crpo)
        {
            FormTampilReport f = new FormTampilReport();
            f.crpo = crpo;
            f.MdiParent = this;
            f.Show();
        }

        public void panggilTampilCA(CrystalReportCashAdv crca)
        {
            FormTampilReport f = new FormTampilReport();
            f.crca = crca;
            f.MdiParent = this;
            f.Show();
        }

        public void panggilTampilSementara(DataTable tb)
        {
            FormTampilSementara f = new FormTampilSementara();
            f.dataGridViewTampil.DataSource = tb;

            f.dataGridViewTampil.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            for (int i = 0; i < f.dataGridViewTampil.ColumnCount; i++)
            {
                f.dataGridViewTampil.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                f.dataGridViewTampil.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            }

            f.MdiParent = this;
            f.Show();
        }

        private void FormParent_MouseMove(object sender, MouseEventArgs e)
        {
            countDown = 0;
        }

        private void TimerLogout_Tick(object sender, EventArgs e)
        {
            countDown += 1;
            if (countDown >= 6000)
            {
                timerLogout.Stop();
                MessageBox.Show("Silahkan Login Ulang","Auto Disconnected");
                this.Close();
            }
        }
    }
}

public delegate void MouseMovedEvent();
public class GlobalMouseHandler : IMessageFilter
{
    private const int WM_MOUSEMOVE = 0x0200;

    public event MouseMovedEvent TheMouseMoved;

    #region IMessageFilter Members

    public bool PreFilterMessage(ref Message m)
    {
        if (m.Msg == WM_MOUSEMOVE)
        {
            if (TheMouseMoved != null)
            {
                TheMouseMoved();
            }
        }
        // Always allow message to continue to the next filter control
        return false;
    }

    #endregion
}