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
    public partial class FrmDoktorDuzenle : Form
    {   
        public FrmDoktorDuzenle()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        public string doktorTC;
        
        private void FrmDoktorDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = doktorTC;
            SqlCommand komut = new SqlCommand("Select *From Tbl_Doktorlar where DoktorTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                CmbBrans.Text = dr[3].ToString();
                TxtSifre.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();
           
        }
        
        
        private void BtnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
   
        }
        
        private void BtnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 where DoktorTC=@p5",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",TxtAd.Text);
            komut1.Parameters.AddWithValue("@p2",TxtSoyad.Text);
            komut1.Parameters.AddWithValue("@p3",CmbBrans.Text);
            komut1.Parameters.AddWithValue("@p4",TxtSifre.Text);
            komut1.Parameters.AddWithValue("@p5",MskTC.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgiler Güncellendi", "Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            
        }
        
        
    }
}
