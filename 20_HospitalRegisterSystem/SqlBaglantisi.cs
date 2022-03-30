using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _20_HospitalRegisterSystem
{
    class SqlBaglantisi     // Olusturmus oldugumuz formlarin hepsine ayri ayri sql connection tanimlamak yerine bir tane sinif tanimlayip onun uzerinden tanimlama islemi yapacagiz.
    {
        public SqlConnection baglanti() //Baglanti metodumuzu tanimladik.
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-87H3JPB\\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True"); // Baglan ise Sqlconnection sinifimizdan turetmis oldugumuz nesnemiz ve adresimizi tutan nesnemizin adidir..
            baglan.Open();                // Open metodu ile de nesnemiz uzerinde acma islemi yaptik.

            return baglan;                // return da geriye donen degeri tutan kismimiz.
        }
    }
}
