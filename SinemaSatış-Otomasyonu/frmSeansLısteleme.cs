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
    public partial class frmSeansLısteleme : Form
    {
        public frmSeansLısteleme()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=SinemaSatısOtomasyonu;Integrated Security=True");
        DataTable tablo = new DataTable();
        private void SeansListesi(string sql)
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter(sql, baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void frmSeansLısteleme_Load(object sender, EventArgs e)
        {
            tablo.Clear();
            SeansListesi("select * from Seans_Bilgileri where tarih like '" + dateTimePicker1.Text + "'");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            tablo.Clear();
            SeansListesi("select * from Seans_Bilgileri where tarih like '" + dateTimePicker1.Text + "'");
        }

        private void btnTumSeanslar_Click(object sender, EventArgs e)
        {
            tablo.Clear();
            SeansListesi("select * from Seans_Bilgileri ");
        }
    }
}
