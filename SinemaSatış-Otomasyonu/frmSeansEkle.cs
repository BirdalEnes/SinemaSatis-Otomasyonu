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
    public partial class frmSeansEkle : Form
    {
        public frmSeansEkle()
        {
            InitializeComponent();
        }
        sinemaTableAdapters.Seans_BilgileriTableAdapter filmseansi = new sinemaTableAdapters.Seans_BilgileriTableAdapter();
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=SinemaSatısOtomasyonu;Integrated Security=True");
        private void frmSeansEkle_Load(object sender, EventArgs e)
        {
            FilmveSalonGoster(combofilm, "select * from Film_Bilgileri", "FilmAdi");
            FilmveSalonGoster(combosalon, "select * from Salon_Bilgileri", "SalonAdi");
        }
        private void FilmveSalonGoster(ComboBox combo, string sql, string sql2)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand(sql, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read() == true)
            {
                combo.Items.Add(read[sql2].ToString());
            }

            baglanti.Close();
        }

        string seans = "";
        private void radiobuttonseciliyse()
        {
            if (radioButton1.Checked == true) seans = radioButton1.Text;
            else if (radioButton2.Checked == true) seans = radioButton2.Text;
            else if (radioButton3.Checked == true) seans = radioButton3.Text;
            else if (radioButton4.Checked == true) seans = radioButton4.Text;
            else if (radioButton5.Checked == true) seans = radioButton5.Text;
            else if (radioButton6.Checked == true) seans = radioButton6.Text;
            else if (radioButton7.Checked == true) seans = radioButton7.Text;
            else if (radioButton8.Checked == true) seans = radioButton8.Text;
            else if (radioButton9.Checked == true) seans = radioButton9.Text;
            else if (radioButton10.Checked == true) seans = radioButton10.Text;
            else if (radioButton11.Checked == true) seans = radioButton11.Text;
            else if (radioButton12.Checked == true) seans = radioButton12.Text;

        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            radiobuttonseciliyse();
            if (seans != "")
            {
                filmseansi.SeansEkleme(combofilm.Text, combosalon.Text, dateTimePicker1.Text, seans);
                MessageBox.Show("Seans Ekleme İşlemi Yapıldı", "Kayıt");
            }
            else if (seans == "")
            {
                MessageBox.Show("Seans Seçimi Yapmadınız", "Uyarı");
            }
            combosalon.Text = "";
            combofilm.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToShortDateString();

        }
        private void TarihiKarsilastir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Seans_Bilgileri where SalonAdi='" + combosalon.Text + "' and tarih ='" + dateTimePicker1.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read() == true)
            {
                foreach (Control item2 in groupBox1.Controls)
                {
                    if (read["seans"].ToString() == item2.Text)
                    {
                        item2.Enabled = false;
                    }
                }
            }
            baglanti.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control item3 in groupBox1.Controls)
            {
                item3.Enabled = true;
            }
            DateTime bugun = DateTime.Parse(DateTime.Now.ToShortDateString());
            DateTime yeni = DateTime.Parse(dateTimePicker1.Text);
            if (yeni == bugun)
            {
                foreach (Control item in groupBox1.Controls)
                {
                    if (DateTime.Parse(DateTime.Now.ToShortTimeString()) > DateTime.Parse(item.Text))
                    {
                        item.Enabled = false;
                    }
                }
                TarihiKarsilastir();

            }
            else if (yeni > bugun)
            {
                TarihiKarsilastir();
            }
            else if (yeni < bugun)
            {
                MessageBox.Show("Geriye Dönük İşlem Yapılamaz", "Uyarı");
                dateTimePicker1.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void combosalon_SelectedIndexChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Text = DateTime.Now.ToShortDateString();

        }
    }
}
