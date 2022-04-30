using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtoGaleriUygulaması
{
    class Araba
    {
        public string Plaka { get; set; }
        public string Marka { get; set; }
        public float KiralamaBedeli { get; set; }
        public ARAC_TIPI AracTipi { get; set; }
        public DURUM Durum { get; set; }

        public List<int> KiralamaSureleri = new List<int>();

        public int KiralamaSayisi
        {
            get
            {
                return this.KiralamaSureleri.Count;
            }
        }

        public int ToplamKiralamaSuresi
        {
            get
            {
                //int toplam = 0;
                //foreach (int item in this.KiralanmaSureleri)
                //{
                //    toplam += item;
                //}
                //return toplam;

                return this.KiralamaSureleri.Sum(); //toplam linq ile alındı
            }
        }      

        public Araba(string plaka, string marka, float kiralamaBedeli, ARAC_TIPI aracTipi)
        {
            //Araba metodunda parametlreli constructer oluşturarak parametreden aldığımız verileri listedeki arabanın bilgilerine ekliyoruz.

            this.Plaka = plaka.ToUpper();
            this.Marka = marka.ToUpper();
            this.KiralamaBedeli = kiralamaBedeli;
            this.AracTipi = aracTipi;
            this.Durum = DURUM.Galeride;
        }
    }

    public enum DURUM
    {
        Empty,
        Galeride,
        Kirada
    }

    public enum ARAC_TIPI
    {
        Empty,
        SUV,
        Hatchback,
        Sedan
    }
}
