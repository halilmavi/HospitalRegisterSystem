using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20_HospitalRegisterSystem
{
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        private void BtnHastaGirisi_Click(object sender, EventArgs e)
        {
            FrmHastaGiris fr = new FrmHastaGiris();   // Frm Hasta giris formundan bir tane fr nesnesi turettik. 
            fr.Show();                                // Turetmis oldugumuz nesnemiz araciligiyla fr ye show metodunu uygulayarak Patient(hasta) butonuna tiklayarak Hasta Giris Panel'i acilmasini sagladik.
            this.Hide();                             // Hide ile suan acik olan formumuzu butona tikladiktan sonra gizleme islemi yaptik.
        }

        private void BtnDoktorGirisi_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris fr = new FrmDoktorGiris();
            fr.Show();
            this.Hide();
        }

        private void BtnSekreterGirisi_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris fr = new FrmSekreterGiris();
            fr.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
