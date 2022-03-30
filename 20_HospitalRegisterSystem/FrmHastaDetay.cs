using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;                  // ADO.NET 'in en temel isim alanidir. Tablo sutun satir kosullar ve dataset gibi veriyle dogrudan iliskisi olan turler bu isim alanindadirlar.
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;        // SQL SERVER icin gelistirilmis bircok siniflar bu isim alaninda toplanmistir. Bircok metodu kullanabilmek icin bu isim alanini programimiza dahil etmemiz gerekir. 

namespace _20_HospitalRegisterSystem
{
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string tc;                               // Hasta Giris panelinde girilen TC'yi tc degiskenine atamak icin global olarak tanimladik.  
        SqlBaglantisi bgl = new SqlBaglantisi();        // Veritabanimiza erismek icin Sqlconnection sinifindan bgl adli nesne turetip veritabanimizdan veri cekerken kullanacagiz.   


        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {   
            // TC Cekme
            LblTC.Text = tc;                                        // Hasta Giris Panelinde girilen TC bilgisini tc degiskenine atamistik. Atadigimiz bu degiskeni LblTC etiketine atama islemi yaptik.
            
            //Ad Soyad Cekme
            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad From Tbl_Hastalar where HastaTC=@p1",bgl.baglanti());  //Hasta Detay Panelinde Ad Soyad kisminda sisteme giris yapanin adini soyadini yazdirabilmek icin Sql sorgusunu kullandik. 
            komut.Parameters.AddWithValue("@p1",LblTC.Text);        // LblTC 'de yazan TC'ye gore ad soyad'i getirsin. @p1 parametresine bu TC bilgisini aktardik. Yukarda bu parametreyi Veritabanimizda veri cekerken sorgulama isleminde kullanacak.
            SqlDataReader dr = komut.ExecuteReader();               // SqlDataReader ile veri okuma islemi yapariz. Veri kaynagindan olusan sonuclari sirasiyla verimli bir sekilde islenmesini saglayan, arabellege alinmamis bir veri akisi saglar. Veriler bellekte onbellege alinmadigi icin buyuk miktarlarda veri alirken DataReader iyi bir secimdir. 
            while (dr.Read())                                       // Yukarda  veritabani ile karsilastirilan TC bilgi eslestiyse Read() metodunda okunacak deger oldugu icin True dondurur.
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];              // Bize sutun bazli iki deger donecek bu yuzden dr'den gelen iki ayri indeksi belirttik.
            }
            bgl.baglanti().Close();                                 // Acmis oldugumuz baglantiyi kapatmazsak geriye donus degeri olan ifadeyi dondurmez.


            DataTable dt = new DataTable();                         // Veritabanindaki verileri uygulama ortamına çekmek ve bu verilerin, yapilacak olan islemler için uygun hale getirilmesini saglamak "DataTable" nesnesi de bu gibi islemler icin en kullanisli nesnelerden biridir. 
            SqlDataAdapter da = new SqlDataAdapter("Select *From Tbl_Randevular where HastaTC=" + tc,bgl.baglanti());       // Randevular kisminda yer alan tc numarasi ile hasta detay panelinde yer alan tc birbirine uyumlu ise o tc ye ait randevulari getirme islemi yaptik.
            da.Fill(dt);                                            // Doldurma işlemide SqlDataAdapter' ın Fill metodu ile yapılmaktadır.  Fill metodu ile dt icerisindeki bilgileri da icerisine doldurma islemi yaptik.
            DgvRandevuGecmisi.DataSource = dt;                      // DgvRandevuGecmisi veritabanimizdan gelen randevu bilgilerini dt icerisine attik.
             

            //Branslari Cekme

            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar",bgl.baglanti());      //SqlCommand sinifi icerisinden turetmis oldugumuz komut2 adli nesnemizin icerisine select sorgusu ile baglanti adresini atama islemi yaptik.
            SqlDataReader dr2 = komut2.ExecuteReader();                  // Bir ya da birden fazla satırların sonuç olarak döneceği sorgularda SqlCommand' ın ExecuteReader özelliği kullanılmaktadır. 
            while (dr2.Read())                                           // Okuma işlemi SqlDataReader nesnesinin Read() metodu ile yapılmaktadır. Read() metodu geriye bool türünde değer döndürmektedir. Okunacak satır var ise true, yoksa false değerini döndürmektedir.
            {
                CmbBrans.Items.Add(dr2[0]);                              // CmbBrans combobox'imizin icerisine okunan degerleri aktaracagiz.
            }
            bgl.baglanti().Close();                                       // Okuma islemimiz bittikten sonra SqlDataREader nesnesini close() metodu ile kapatma islemi yapariz.

        }

        private void button1_Click(object sender, EventArgs e)          // Hasta Detay Panelinde bulunan Cikis butonu icin yazmis oldugumuz kod satirimiz. Butona tiklanildiginda programimizi tamamen kapatma islemi yapar.
        {
            this.Close();
            Application.Exit();
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();         // Branslari secme islemini her yaptigimizda daha once secmis oldugumuz doktor ismi sonrakiyle beraber cikiyor bunun onune gecmek her brans icin tiklanildiginda once clear metodu calisip CmbDoktorlar icerisini temizlesin. kullandik.
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1",CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0]+" "+dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)     //CmbDoktorlar box'i icerisinden doktor secme islemi yapildiginda DgvAktifRandevular ekranini yanda aktif hale getirme islemi yapariz.
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select *From Tbl_Randevular where RandevuBrans='"+CmbBrans.Text+"'"+"and RandevuDoktor='"+CmbDoktor.Text+"'and RandevuDurum=0",bgl.baglanti());  // Sql Server'da kelime bazli arama yaparken tek tirnak icerisinde belirtmemiz gerekir.  and baglacini kullanarak ekledigimiz kosulumuz FrmHastaDetay'da yer alan Randevu Panelinde doktor'a tiklanildiginda DgvAktifRandevular ekraninda sadece secilen doktorun aktif randevulari gozukmesi icin tanimladik. Ardindan tekrar eklemis oldugumuz RandevuDurum kosulu ile de secilen doktorun sadece bos olan randevularinin gelmesini tanimladik. 
            da.Fill(dt);
            DgvAktifRandevular.DataSource = dt;
        }

        private void LnkBilgiGuncelle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiGuncelle fr = new FrmBilgiGuncelle();   // Bilgileri guncelleme linkine bastigimizda  bilgi guncelle penceresi acilacak
            fr.TCno = LblTC.Text;
            fr.Show();          
        }

        private void DgvAktifRandevular_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = DgvAktifRandevular.SelectedCells[0].RowIndex;
            Txtid.Text = DgvAktifRandevular.Rows[secilen].Cells[0].Value.ToString();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Randevular Set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where Randevuid=@p3",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",LblTC.Text);
            komut.Parameters.AddWithValue("@p2",RchSikayet.Text);
            komut.Parameters.AddWithValue("@p3",Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu alındı","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void button2_Click(object sender, EventArgs e)      // FrmHastaDetay formundaki Randevu Panelinde yazilan bilgileri temizleme islemini tanimladik.
        {
            Txtid.Text = " ";
            CmbBrans.Text=" ";
            CmbDoktor.Text=" ";
            RchSikayet.Text = " ";
        } 
    }
}
