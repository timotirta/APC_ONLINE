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
    public partial class FormTampilReport : Form
    {
        public FormTampilReport()
        {
            InitializeComponent();
        }
        public CrystalReportOpr cropr = null;
        public CrystalReportPrintVendor crvendor= null;
        public CrystalReportProject crproject = null;
        public CrystalReportKas crkas = null;
        public CrystalReportPenggajian crgaji = null;
        public CrystalReportPJ crpj = null;
        public CrystalReportGiro crgiro = null;
        public CrystalReportBuktiBayar crbayar = null;
        public CrystalReportPembonusanProject crbnspr = null;
        public CrystalReportPembonusanTahunan crbnsth = null;
        public CrystalReportPOVendor crpo = null;
        public CrystalReportJurnalAcc crja = null;
        public CrystalReportLPK crlpk = null;
        public CrystalReportJurnalLabaRugi crlb = null;
        public CrystalReportJurnalLabaRugiTahun crlbt = null;
        public CrystalReportCashAdv crca = null;
        private void FormTampilReport_Load(object sender, EventArgs e)
        {
            if (cropr != null)
            {
                cropr.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = cropr;
            }
            if (crvendor != null)
            {
                crvendor.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crvendor;
            }
            if (crproject != null)
            {
                crproject.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crproject;
            }
            if (crkas != null)
            {
                crkas.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crkas;
            }
            if (crgaji != null)
            {
                crgaji.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crgaji;
            }
            if (crpj != null)
            {
                crpj.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crpj;
            }
            if (crgiro != null)
            {
                crgiro.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crgiro;
            }
            if (crbayar != null)
            {
                crbayar.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crbayar;
            }
            if (crbnspr != null)
            {
                crbnspr.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crbnspr;
            }
            if (crbnsth != null)
            {
                crbnsth.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crbnsth;
            }
            if (crpo != null)
            {
                crpo.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crpo;
            }
            if (crja != null)
            {
                crja.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crja;
            }
            if (crlpk != null)
            {
                crlpk.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crlpk;
            }
            if (crlb != null)
            {
                crlb.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crlb;
            }
            if(crlbt != null)
            {
                crlbt.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crlbt;
            }
            if(crca != null)
            {
                crca.SetDatabaseLogon("btwo", "asdf1234");
                crystalReportViewer1.ReportSource = crca;
            }
            
        }
    }
}
