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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        
        private void LnkKaydol_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)          // FrmGirisler kisminda hasta butonunu secen kisi acilan hasta giris panelinde kaydol butonuna tikladiginda hasta kayit paneli acilma islemi yaptik.
        {           
            FrmHastaKayıt fr = new FrmHastaKayıt();
            fr.Show();
            //  this.Hide();  Bu sekilde kodumuzu aktiflestimiyoruz cunku uye olduktan sonra hasta giris paneli arka planda acik olmasi gerekiyor. Uye olduktan sonra tekrar giris yapabilmek icin.
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)     
        {
            SqlCommand komut = new SqlCommand("Select *From Tbl_Hastalar Where HastaTC=@p1 and HastaSifre=@p2", bgl.baglanti());    // Alt satirda almis oldugumuz iki parametre degeri ile Tc ve Sifre bilgisini kullanicidan alma islemi yaptiktan sonra bu bilgiler veritabanimizdaki bilgileri ile ayni ise if kosuluna girip dr nesnesini okuma islemini yaparak if kosulunda yazan komutlari gerceklestirecek.
            komut.Parameters.AddWithValue("@p1", MskTC.Text);   //Hasta giris panelinden almis oldugumuz TC bilgisini, @p1 parametresine atama islemi yapiyoruz. ve parametremizi yukardaki sqlcommand sorgusunda yer alan veritabanimizdaki HastaTC ile where kosulu araciligiyla karsilastiriyor ve dogru ise 
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();   /* Bir ya da birden fazla satırların sonuç olarak döneceği sorgularda SqlCommand' ın ExecuteReader özelliği kullanılmaktadır. ExecuteReader geriye SqlDataReader tipinde veri döndürmektedir.
                                                           SqlDataReader, sadece okunabilir olarak kullanılmaktadır. Satır satır okuma işlemi yapılmaktadır. SqlDataReader kullanımı boyunca veritabanı bağlantısı açık olacaktır. Çünkü SqlDataReader veritabanı ile bağlantılı olarak çalışmaktadır. Okuma işlemi sona erdiğinde SqlDataReader bağlantısınında kapatılması gerekmektedir.
                                                        */
            if (dr.Read())  // dr nesnesini SqlDataReader'in metodu olan Read() ile okuma islemi yapariz icinde okunacak veri varsa True yoksa False boolean degerini dondurur.
            {

                FrmHastaDetay fr = new FrmHastaDetay();
                fr.tc = MskTC.Text;          // Hasta detay panelinde yazan tc bilgisini Hasta Giris Paneline giris yaparken global olarak tanimlamis oldugumuz tc degiskenine atama islemi yaptik.
                fr.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Hatalı TC veya Şifre ! ");
            }
            bgl.baglanti().Close();

        }

        private void GeriDon_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmGirisler fr = new FrmGirisler();
            fr.Show();
        }
    }
}
