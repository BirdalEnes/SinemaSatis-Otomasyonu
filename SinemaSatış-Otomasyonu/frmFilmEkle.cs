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
    public partial class frmFilmEkle : Form
    {
        public frmFilmEkle()
        {
            InitializeComponent();
        }
        //sinemaTableAdapters.Film_BilgileriTableAdapter film=new sinemaTableAdapters.Film_BilgileriTableAdapter();
        sinemaTableAdapters.Film_BilgileriTableAdapter film = new sinemaTableAdapters.Film_BilgileriTableAdapter();
        private void btnFılmEkle_Click(object sender, EventArgs e)
        {
            try
            {
                // film.FilmEkleme(txtFılmAdı.Text, txtYonetmen.Text, comboFılmTuru.Text, txtSure.Text, dateTimePicker1.Text, txtYapımYılı.Text, pictureBox1.ImageLocation);
                film.FilmEkleme(txtFılmAdı.Text, txtYonetmen.Text, comboFılmTuru.Text, txtSure.Text, dateTimePicker1.Text, txtYapımYılı.Text, pictureBox1.ImageLocation);
                MessageBox.Show("Film Bilgileri Eklendi");
            }
            catch (Exception)
            {

                MessageBox.Show("Bu Film Daha önce Eklendi","Uyarı");
            }
           
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            comboFılmTuru.Text = "";

        }
        private void btnAfısSec_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void frmFilmEkle_Load(object sender, EventArgs e)
        {
            Form1 anasayfa = new Form1();
            anasayfa.ShowDialog();
        }
    }
}
