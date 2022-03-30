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


namespace _20_HospitalRegisterSystem
{
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void BtnEkle_Click(object sender, EventArgs e)          //FrmBrans icinde yer alan Ekle butonu icin gerekli kodlarimizi yazdik.
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Branslar(BransAd) values(@p1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",TxtBrans.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Eklendi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmBrans_Load(object sender, EventArgs e)           // FrmBrans icinde yer alan DgvBrans dataGrid'in brans paneli acildiginda ekranda gozukmesi icin gerekli kodlarimizi yazdik.
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select *From Tbl_Branslar",bgl.baglanti());
            da.Fill(dt);
            DgvBrans.DataSource = dt;
        }

        private void DgvBrans_CellClick(object sender, DataGridViewCellEventArgs e) // FrmBrans icinde yer alan DgvBrans dataGrid uzerindeki brans uzerine tiklanildiginda bilgiler textboxlarin icerisini dolduracak.
        {
            int secilen = DgvBrans.SelectedCells[0].RowIndex;
            Txtid.Text = DgvBrans.Rows[secilen].Cells[0].Value.ToString();
            TxtBrans.Text = DgvBrans.Rows[secilen].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)                   // Formumuz icerisinde yer alan silme butonunu aktif hale getirdik. 
        {
            SqlCommand komut1 = new SqlCommand("delete From Tbl_Branslar where BransAd=@p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",TxtBrans.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Branş Silindi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)   //Guncelle butonumuzu aktif hale getirmek icin kodlarimizi yazdik.
        {
            SqlCommand komut2 = new SqlCommand("update Tbl_Branslar set BransAd=@p1 where Bransid=@p2",bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",TxtBrans.Text);
            komut2.Parameters.AddWithValue("@p2",Txtid.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Brans Güncellendi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
