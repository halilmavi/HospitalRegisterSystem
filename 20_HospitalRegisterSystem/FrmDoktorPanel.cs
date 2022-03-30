using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _20_HospitalRegisterSystem
{
    public partial class FrmDoktorPanel : Form
    {
        public FrmDoktorPanel()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void FrmDoktorPanel_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select *From Tbl_Doktorlar ", bgl.baglanti());
            da1.Fill(dt1);
            DgvDoktorPanel.DataSource = dt1;
        }

        private void BtnEkle_Click(object sender, EventArgs e)          // Doktor Panelinde yer alan Ekle butonunu aktif hale getirdik. 
        {
            SqlCommand komut = new SqlCommand("insert  into Tbl_Doktorlar(Dokt orAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values(@d1,@d2,@d3,@d4,@d5)", bgl.baglanti());              // DoktorPanelinde bulunan Ekle butonunu aktif hale getirme islemi yaptik. SqlCommand ile yeni doktorun bilgilerini parametre olarak gonderme islemi yaptik. 
            komut.Parameters.AddWithValue("@d1", TxtAd.Text);
            komut.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut.Parameters.AddWithValue("@d4", MskTc.Text);
            komut.Parameters.AddWithValue("@d5", TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DgvDoktorPanel_CellDoubleClick(object sender, DataGridViewCellEventArgs e) //Doktor panelinde ye alan DgvDoktor dataGrid'de uzerine cift tiklanilan doktorun bilgileri ekrandaki box'larin icine dolsun. 
        {

            int secilen = DgvDoktorPanel.SelectedCells[0].RowIndex; // Secilen hucrenin satir indeksini secilen degiskenine atama islemi yaptik.
            TxtAd.Text = DgvDoktorPanel.Rows[secilen].Cells[1].Value.ToString(); // Rows icerisindeki secilen degiskeni ile dataGrid uzerinde tiklamiz oldugumuz satirin indeks degerini gonderiyoruz ki o satirdaki bilgileri bize gonderme islemi yapsin. Cells icerisine tanimlamis oldugumuz degerlerde bize secilen satirda kacinci sutunda yer alan bilginin yazdirilacagini belitirtiyor.
            TxtSoyad.Text = DgvDoktorPanel.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = DgvDoktorPanel.Rows[secilen].Cells[3].Value.ToString();
            MskTc.Text = DgvDoktorPanel.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = DgvDoktorPanel.Rows[secilen].Cells[5].Value.ToString();

        }

        private void BtnSil_Click(object sender, EventArgs e) // Doktor panelinde yer alan Silme butonunu aktif hale getirme islemi yaptik.
        {
            SqlCommand komut1 = new SqlCommand("Delete From Tbl_Doktorlar where DoktorTc=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", MskTc.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Silindi !", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)      // Doktor panelinde yer alan guncelle butonunu aktif hale getirdik.
        {
            SqlCommand komut2 = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d5 where DoktorTC=@d4", bgl.baglanti());
            komut2.Parameters.AddWithValue("@d1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut2.Parameters.AddWithValue("@d4", MskTc.Text);
            komut2.Parameters.AddWithValue("@d5", TxtSifre.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgiler Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDoktorPanel frmYenile = new FrmDoktorPanel();
            frmYenile.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void CmbBrans_Click(object sender, EventArgs e)
        {
            SqlCommand komutBrans = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader drBrans = komutBrans.ExecuteReader();
            while (drBrans.Read())
            {
                CmbBrans.Items.Add(drBrans[0]);
            }
            bgl.baglanti().Close();
        }


    }
}
