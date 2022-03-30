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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }


        // Sekreter Giriş Panelindeki bulunan elemanlarimizin kodlarini tanimladik.

        SqlBaglantisi bgl = new SqlBaglantisi();
        private void BtnGirisYap_Click(object sender, EventArgs e)          // Giris Yap butonuna tiklanildiginda girilen tc ve sifre veri tabanindaki ile karsilastirilir dogru ise if kosuluna girilir. Ve icindeki kodlar calisir.
        {
            SqlCommand komut = new SqlCommand("Select *From Tbl_Sekreter where SekreterTC=@p1 and SekreterSifre=@p2",bgl.baglanti()); //komut nesnesine araciligiyla Tbl_Sekreter veritabaninda bulunan TC ve sifreleri nesnemize aktarir.
            komut.Parameters.AddWithValue("@p1",MskTC.Text);
            komut.Parameters.AddWithValue("@p2",TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            
            if (dr.Read())                              
            {
                FrmSekreterDetay fr = new FrmSekreterDetay();              // FrmSekreterDetay sinifini tanimlayip fr adinda nesne turettiik.
                fr.tcNumara = MskTC.Text;                                  // SekreterDetay panelimizde yer alan TC bilgisini Sekreter Giris panelinde tc bilgisinden gonderme islemi yaptik. Global olarak tanimlanan tcNumara degiskenimize tc degerini aktarma islemi yaptik. 
                fr.Show();
                this.Hide();                                               // Sekreter detay paneli acildiktan sonra sekretter giris paneli gizlenecek. 
            }
            else
            {
                MessageBox.Show("Hatalı TC veya Şifre !","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning ); 
            }
            bgl.baglanti().Close();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmGirisler fr = new FrmGirisler();
            fr.Show();
        }
    }
}
 