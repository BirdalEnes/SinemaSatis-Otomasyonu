using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinemaSatış_Otomasyonu
{
    public partial class frmSatısLısteleme : Form
    {
        public frmSatısLısteleme()
        {
            InitializeComponent();
        }

        sinemaTableAdapters.Satis_BilgileriTableAdapter satislistesi = new sinemaTableAdapters.Satis_BilgileriTableAdapter();

        private void ToplamUcretHesapla()
        {
            int ucrettoplami = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ucrettoplami += Convert.ToInt32(dataGridView1.Rows[i].Cells["ucret"].Value);
            }
            label1.Text = "Toplam Satış =" + ucrettoplami + "TL";
        }
        private void frmSatısLısteleme_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = satislistesi.TariheGöreListele2(dateTimePicker1.Text);
            ToplamUcretHesapla();
        }

        private void btnTumSatıslar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = satislistesi.SatısListesi2();
            ToplamUcretHesapla();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = satislistesi.TariheGöreListele2(dateTimePicker1.Text);
            ToplamUcretHesapla();
        }
    }
}
