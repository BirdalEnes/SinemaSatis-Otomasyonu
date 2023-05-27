using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SinemaSatış_Otomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=SinemaSatısOtomasyonu;Integrated Security=True");

        int sayac = 0;
        private void Combo_Dolu_Koltuklar()
        {
            comboKoltukIPtal.Items.Clear();
            comboKoltukIPtal.Text = "";
            foreach (Control item in panel1.Controls)
            {
                if (item is Button)
                {
                    if (item.BackColor == Color.Red)
                    {
                        comboKoltukIPtal.Items.Add(item.Text);
                    }
                }
            }
        }
        private void YenidenRenklendir()
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is Button)
                {
                    item.BackColor = Color.White;
                }
            }
        }
        private void Veritabanı_Dolu_Koltuklar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Satis_Bilgileri where filmadi='" + comboFılmAdı.SelectedItem + "' and salonadi='" + comboSalonAdı.Text + "' and tarih='" + comboFılmTarıhı.SelectedItem + "' and saat='" + comboFIlmSeans.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in panel1.Controls)
                {
                    if (item is Button)
                    {
                        if (read["koltukno"].ToString() == item.Text)
                        {

                            item.BackColor = Color.Red;

                        }
                    }

                }
            }
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Bos_Koltuklar();
            FilmveSalonGetir(comboFılmAdı, "select * from Film_Bilgileri", "filmadi");
            FilmveSalonGetir(comboSalonAdı, "select * from Salon_Bilgileri", "salonadi");
        }
        private void FilmAfisiGoster()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from film_bilgileri where filmadi='" + comboFılmAdı.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                pictureBox1.ImageLocation = read["resim"].ToString();
            }
            baglanti.Close();
        }
        private void FilmveSalonGetir(ComboBox combo, string sql1, string sql2)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sql1, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                combo.Items.Add(read[sql2].ToString());
            }
            baglanti.Close();
        }
        private void Bos_Koltuklar()
        {
            // Panele Button Oluşturma
            sayac = 1;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(30, 30);
                    btn.Location = new Point(j * 30 + 20, i * 30 + 30);
                    btn.Name = sayac.ToString();
                    btn.Text = sayac.ToString();
                    if (j == 4)
                    {
                        continue;
                    }
                    sayac++;
                    this.panel1.Controls.Add(btn);
                    btn.Click += Btn_Click;
                }
            }
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.BackColor == Color.White)
            {
                txtKoltukNo.Text = b.Text;
            }
        }
        private void btnSalonEkle_Click(object sender, EventArgs e)
        {
            frmSalonEkle salonekle = new frmSalonEkle();
            salonekle.ShowDialog();


        }

        private void btnFılmEkle_Click(object sender, EventArgs e)
        {
            frmFilmEkle filmekle = new frmFilmEkle();
            filmekle.ShowDialog();

        }

        private void btnSeansEkle_Click(object sender, EventArgs e)
        {
            frmSeansEkle seansekle = new frmSeansEkle();
            seansekle.ShowDialog();



        }

        private void btnSeansLıstele_Click(object sender, EventArgs e)
        {
            frmSeansLısteleme seanslisteleme = new frmSeansLısteleme();
            seanslisteleme.ShowDialog();

        }

        private void btnSatıs_Click(object sender, EventArgs e)
        {
            frmSatısLısteleme satıs = new frmSatısLısteleme();
            satıs.ShowDialog();

        }

        private void comboFılmAdı_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboFIlmSeans.Items.Clear();
            comboFılmTarıhı.Items.Clear();
            comboFIlmSeans.Text = "";
            comboFılmTarıhı.Text = "";
            comboSalonAdı.Text = "";
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            FilmAfisiGoster();
            YenidenRenklendir();
            Combo_Dolu_Koltuklar();
        }

        sinemaTableAdapters.Satis_BilgileriTableAdapter satis = new sinemaTableAdapters.Satis_BilgileriTableAdapter();
        private void btnBıletSat_Click(object sender, EventArgs e)
        {
            if (txtKoltukNo.Text != "")
            {
                try
                {
                    satis.Satış_Yap(txtKoltukNo.Text, comboSalonAdı.Text, comboFılmAdı.Text, comboFılmTarıhı.Text, comboFIlmSeans.Text, txtAd.Text, txtSoyad.Text, comboUcret.Text, DateTime.Now.ToString());
                    foreach (Control item in groupBox1.Controls)
                    {
                        if (item is TextBox)
                        {
                            item.Text = "";
                        }
                    }
                    YenidenRenklendir();
                    Veritabanı_Dolu_Koltuklar();
                    Combo_Dolu_Koltuklar();
                }
                catch (Exception hata)
                {

                    MessageBox.Show("hata oluştu" + hata.Message, "Uyarı");
                }
            }
            else
            {
                MessageBox.Show("Koltuk Seçimi Yapmadınız", "Uyarı");
            }
        }

        private void Film_Tarihi_Getir()
        {
            comboFılmTarıhı.Text = "";
            comboFIlmSeans.Text = "";
            comboFılmTarıhı.Items.Clear();
            comboFIlmSeans.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from seans_bilgileri where filmadi='" + comboFılmAdı.SelectedItem + "' and salonadi='" + comboSalonAdı.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (DateTime.Parse(read["tarih"].ToString()) >= DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    if (!comboFılmTarıhı.Items.Contains(read["tarih"].ToString()))
                    {
                        comboFılmTarıhı.Items.Add(read["tarih"].ToString());
                    }

                }

            }
            baglanti.Close();
        }
        private void comboSalonAdı_SelectedIndexChanged(object sender, EventArgs e)
        {
            Film_Tarihi_Getir();
        }

        private void Film_Seansi_Getir()
        {
            comboFIlmSeans.Text = "";
            comboFIlmSeans.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from seans_bilgileri where filmadi='" + comboFılmAdı.SelectedItem + "' and salonadi='" + comboSalonAdı.SelectedItem + "' and tarih='" + comboFılmTarıhı.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (DateTime.Parse(read["tarih"].ToString()) == DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    if (DateTime.Parse(read["seans"].ToString()) > DateTime.Parse(DateTime.Now.ToShortTimeString()))
                    {
                        comboFIlmSeans.Items.Add(read["seans"].ToString());
                    }

                }

                else if (DateTime.Parse(read["tarih"].ToString()) > DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    comboFIlmSeans.Items.Add(read["seans"].ToString());
                }

            }
            baglanti.Close();
        }
        private void comboFılmTarıhı_SelectedIndexChanged(object sender, EventArgs e)
        {
            Film_Seansi_Getir();
        }

        private void comboFIlmSeans_SelectedIndexChanged(object sender, EventArgs e)
        {
            YenidenRenklendir();
            Veritabanı_Dolu_Koltuklar();
            Combo_Dolu_Koltuklar();
        }

        private void btnBıletIptal_Click(object sender, EventArgs e)
        {
            if (comboKoltukIPtal.Text != "")
            {
                try
                {
                    satis.Satış_İptal(comboFılmAdı.Text, comboSalonAdı.Text, comboFılmTarıhı.Text, comboFIlmSeans.Text, comboKoltukIPtal.Text);
                    YenidenRenklendir();
                    Veritabanı_Dolu_Koltuklar();
                    Combo_Dolu_Koltuklar();
                }
                catch (Exception hata)
                {

                    MessageBox.Show("Hata Oluştu"+hata.Message, "Uyarı");
                }
                
            }
            else
            {
                MessageBox.Show("Koltuk Seçimi Yapmadınız", "Uyarı");
            }
        }
    }
}
