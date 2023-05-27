using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinemaSatış_Otomasyonu
{
    public partial class frmSalonEkle : Form
    {
        public frmSalonEkle()
        {
            InitializeComponent();
        }

        private void frmSalonEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 anasayfa = new Form1();
            anasayfa.ShowDialog();
        }

        sinemaTableAdapters.Salon_BilgileriTableAdapter salon= new sinemaTableAdapters.Salon_BilgileriTableAdapter();
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                salon.SalonEkleme(txtSalonAdı.Text);
                MessageBox.Show("Salon Eklendi", "Kayıt");
            }
            catch (Exception)
            {
                MessageBox.Show("Aynı Salonu Daha Önce Eklediniz","Uyarı");

                
            }
           
            txtSalonAdı.Text = "";
        }

        private void frmSalonEkle_Load(object sender, EventArgs e)
        {

        }
    }
}
