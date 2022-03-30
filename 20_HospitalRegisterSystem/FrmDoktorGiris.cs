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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)          // Doktor giris panelinde ana menuye geri donme islemi icin tanimladigimiz butonumuz. 
        {
            this.Close();
            FrmGirisler fr = new FrmGirisler();
            fr.Show();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select *From Tbl_Doktorlar where DoktorTC=@p1 and DoktorSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmDoktorDetay frm = new FrmDoktorDetay();
                frm.TC = MskTC.Text;                        // FrmDoktorDetay paneline giris yaptiktan sonra ekranda gozuken TC bilgisini bu kod satiriyla FrmDoktorDetay icerisinde olusturmus oldugumuz TC global degiskeni icerisien atiyoruz.
                frm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hatalı Şifre Veya TC girdiniz");
            }
            bgl.baglanti().Close();
        }
    }
}
