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
    public partial class FrmHastaKayıt : Form
    {
        public FrmHastaKayıt()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();  // Tum formlarda kullanacak oldugumuz SqlBaglantisi adli sinifimizi cagirdik. Ve bgl adli nesne turetip baglanti metodumuza erisim saglayabilecegiz.  

        private void BtnKayitYap_Click_1(object sender, EventArgs e)

         // SqlCommand sinifini, T-Sql sorguları ile veritabanı uzerinde sorgulama, ekleme, guncelleme, silme islemlerini yapmak icin kullaniriz. O yuzden asagida SqlCommand sinifi uzerinden komut adli nesnemizi turettik. Turetmis oldugumuz bu nesnemize kayit yapacagi degiskenlerin parametrelerini tanimlama  islemi yaptik.
         // insert into TabloAdi(Tablo degiskenleri)  values(@p1 seklinde her degiskene ayri parametre anahtari tanimlariz.), bgl.baglanti() )   Sonunda olusturmus oldugumuz baglanti sinifindan turetmis oldugumuz bgl nesnemiz ile veritabanimiza kayit islemi yapariz.
        {
            
            SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar(HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet) values(@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());  // bgl bu form icerisinde tanimlamis oldugumuz nesnemiz bu nesneyi kullanarak baglanti metodumuzu cagirdik.Parametrelerini girmis oldugumuz degerleri Baglanti metodu ile veritabanina kayit edecegiz.
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTC.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon.Text);  // MaskedBox ile olusturmus oldugumuz telefon degiskenini veritabanimizda varchar(15) olarak tanimladik.Cunku MaskedBox icerisinde numaralar disinda parantez karakterleri de oldugu icin ona gore hafiza da yer actik.
            komut.Parameters.AddWithValue("@p5", TxtSifre.Text);
            komut.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Gercekleştirilmiştir." + "\n" + "Şifre : " + TxtSifre.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); ;

        }

        private void button1_Click(object sender, EventArgs e)      // Hasta kaydi yaptiktan sonra cikis butonuna basildiginda kayit formu gizlenip hasta giris paneli kalacak sekilde duzenleme yaptik.
        {
            this.Hide();
        }

        private void FrmHastaKayıt_Load(object sender, EventArgs e)
        {

        }
    }
}
