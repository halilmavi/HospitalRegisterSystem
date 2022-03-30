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

namespace _20_HospitalRegisterSystem
{
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }


        SqlBaglantisi bgl = new SqlBaglantisi();
        public string TC;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = TC;

            //Doktor AdSoyad cekme islemi
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }


            //Randevular

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select *From Tbl_Randevular where RandevuDoktor='"+ LblAdSoyad.Text+"'" ,bgl.baglanti());
            da.Fill(dt);
            DgvDoktorRandevularListesi.DataSource = dt;
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorDuzenle fr = new FrmDoktorDuzenle();
            fr.doktorTC = LblTC.Text;
            fr.Show();
         
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DgvDoktorRandevularListesi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = DgvDoktorRandevularListesi.SelectedCells[0].RowIndex;
            RchSikayet.Text = DgvDoktorRandevularListesi.Rows[secilen].Cells[7].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmGirisler fr = new FrmGirisler();
            fr.Show();
            this.Close();

        }
    }
}
