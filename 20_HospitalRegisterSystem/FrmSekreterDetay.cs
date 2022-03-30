using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _20_HospitalRegisterSystem
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string tcNumara;     //
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTc.Text = tcNumara;                      // tcNumara adli degiskenimizi

            // Ad Soyad Cekme 

            SqlCommand komut = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreter where SekreterTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }
            bgl.baglanti();


            // Branslari DataGrid'e Cekme 

            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select BransAd From Tbl_Branslar", bgl.baglanti());
            da.Fill(dt1);
            DgvBranslar.DataSource = dt1;


            // Doktorlari DataGrid'e Cekme

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd+' '+DoktorSoyad), DoktorBrans From Tbl_Doktorlar ", bgl.baglanti());
            da2.Fill(dt2);
            DgvDoktorlar.DataSource = dt2;
            

            // Branslari ComboBox'imiza getirme
            SqlCommand komutBrans = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader drBrans = komutBrans.ExecuteReader();
            while (drBrans.Read())
            {
                CmbBrans.Items.Add(drBrans[0]);
            }
            bgl.baglanti().Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutKaydet = new SqlCommand("insert into Tbl_Randevular(RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values(@p1,@p2,@p3,@p4)", bgl.baglanti());
            komutKaydet.Parameters.AddWithValue("@p1", MskTarih.Text);
            komutKaydet.Parameters.AddWithValue("@p2", MskSaat.Text);
            komutKaydet.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komutKaydet.Parameters.AddWithValue("@p4", CmbDoktor.Text);
            komutKaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);




        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Text = " ";       // Bir doktor sectiktan sonra farkli bir branstaki doktora tiklanildiginda doktor text'inin ici temizlenmesi icin yazmis oldugumuz kod satiriz.
            CmbDoktor.Items.Clear();    // ARt arda farkli doktorlar secme islemi yapildiginda bir onceki doktorun adi kaldigi icin temizleme islemi yaptik.
            SqlCommand komutDoktor = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komutDoktor.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader drDoktor = komutDoktor.ExecuteReader();
            while (drDoktor.Read())
            {
                CmbDoktor.Items.Add(drDoktor[0] + " " + drDoktor[1]);
            }
            bgl.baglanti().Close();


        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular(Duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", RchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Olusturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)           // Sekreter Panelinde yer alan doktor panelini aktif hale getirdik.
        {
            FrmDoktorPanel frmDoktor = new FrmDoktorPanel();
            frmDoktor.Show();
        }

        private void BtnBransPanel_Click(object sender, EventArgs e)     // Sekreter Panelinde yer alan Brans panelini aktif hale getirdik. 
        {
            FrmBrans frmBrans = new FrmBrans();
            frmBrans.Show();
        }

        private void DuyuruEkraniTemizleme_Click(object sender, EventArgs e)
        {
            RchDuyuru.Text = " ";
        }

        private void BtnRandevuListele_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frmRandevuListele = new FrmRandevuListesi();
            frmRandevuListele.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyuru = new FrmDuyurular();
            frmDuyuru.Show();
        }
    }
}
