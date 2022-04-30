using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtoGaleriUygulaması
{
    class Galeri
    {
        public List<Araba> Arabalar = new List<Araba>();

        public Galeri()
        {
            SahteVeriGir();
        }

        public int ToplamAracSayisi
        {
            get
            {
                return this.Arabalar.Count; // Count ile galeride ki toplam araç sayısını buluyoruz.
            }
        }

        public int GaleridekiAracSayisi
        {
            get
            {
                //int adet = 0;
                //foreach (Araba item in this.Arabalar)
                //{
                //    if (item.Durum == DURUM.Galeride)
                //    {
                //        adet++;
                //    }
                //}
                //return adet;

                return this.Arabalar.Where(a => a.Durum == DURUM.Galeride).ToList().Count;
            }
        }

        public int KiradakiAracSayisi
        {
            get
            {
                return this.Arabalar.Where(t => t.Durum == DURUM.Kirada).ToList().Count;
            }
        }

        public int ToplamAracKiralamaSuresi
        {
            get
            {
                return this.Arabalar.Sum(a => a.KiralamaSureleri.Sum());
            }
        } //arabalar listesindeki tüm arabaların toplamkiralanmasuresi'nin toplamı

        public int ToplamAracKirlamaAdedi
        {
            get
            {
                return this.Arabalar.Sum(a => a.KiralamaSayisi);
            }
        }//Sum ile kiralanan araçları kiralanma adedinin toplamına ulaşıyoruz.

        public float Ciro
        {
            get
            {
                return this.Arabalar.Sum(a => a.ToplamKiralamaSuresi * a.KiralamaBedeli);
            }
        }// Sum ile kiralanan araçların cirolarının toplamını buluyoruz.

        public void ArabaKirala(string plaka, int sure)
        {
            //parametreden aldığımız plaka bilgisi ile araba listesinden aradığımız aracı buluyoruz.
            // FirsOrDefault metodu çağırdığımız listede ki ilk veriyi alır.
            // Eğer böyle bir araç var ise ve durumu galeride ise durumunu kirada olarak güncelleyip kiralanma süresine ekliyoruz.

            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper()).FirstOrDefault();
            if (a != null && a.Durum == DURUM.Galeride)
            {

                a.Durum = DURUM.Kirada;
                a.KiralamaSureleri.Add(sure);
            }
        }
        
        public void KiralamaIptal(string plaka)
        {
            //parametreden aldığımız plaka bilgisi ile aradığımız aracı buluyoruz.
            // FirsOrDefault metodu çağırdığımız listede ki ilk veriyi alır.
            // Eğer böyle bir araç var ise kiralamayı iptal edeceğimiz için durumu galeride olarak güncelleyip kiralama süresini düşüyoruz.

            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper()).FirstOrDefault();

            if (a != null)
            {
                a.Durum = DURUM.Galeride;
                a.KiralamaSureleri.RemoveAt(a.KiralamaSureleri.Count - 1);
            }
        }

        public DURUM DurumGetir(string plaka)
        {
            //parametreden aldığımız plaka bilgisi ile aradığımız aracı buluyoruz.
            // FirsOrDefault metodu çağırdığımız listede ki ilk veriyi alır.
            // Eğer böyle bir araç varsa bulduğumuz aracın güncel durumunu döndürür eğer araç yoksa araç olmadığı için Empty döndürür.

            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper()).FirstOrDefault();
            if (a != null)
            {
                return a.Durum;
            }
            return DURUM.Empty;
        }

        public void ArabaTeslimAl(string plaka)
        {
            //parametreden aldığımız plaka bilgisi ile aradığımız aracı buluyoruz.
            // FirsOrDefault metodu çağırdığımız listede ki ilk veriyi alır.
            // Eğer böyle bir araç varsa bulduğumuz aracın durumununu aracı teslim alacağımız için galeride olarak güncellenir.

            Araba a = this.Arabalar.Where(a => a.Plaka == plaka.ToUpper() && a.Durum == DURUM.Kirada).FirstOrDefault();

            if (a != null)
            {
                a.Durum = DURUM.Galeride;
            }
        }

        public void ArabaEkle(string plaka, string marka, float kiralamaBedeli, ARAC_TIPI aracTipi)
        {
            //Parametreden aldığımız bilgiler ile yeni bir araba listesi oluşturarak bu bilgileri Araba classına göndererek bilgileri kaydederiz.
            //Add metodu ile eklediğimiz arabayı galeriye kaydederiz.

            Araba a = new Araba(plaka, marka, kiralamaBedeli, aracTipi);

            this.Arabalar.Add(a);
        }

        public List<Araba> ArabaListesiGetir(DURUM durum)
        {
            // parametreden aldığımız durum veri tipinde aldığımız veri ile otogaleride araç durumlarına göre listeleme gerçekleştiriyoruz.

            List<Araba> liste = this.Arabalar;
            if (durum == DURUM.Kirada)
            {
                liste = this.Arabalar.Where(a => a.Durum == durum).ToList();
            }
            else if (durum == DURUM.Galeride)
            {
                liste = this.Arabalar.Where(a => a.Durum == durum).ToList();
            }
            return liste;
        }

        public void ArabaSil(string plaka)
        {
            // parametreden aldığımız plaka bilgisi ile aradığımız aracı buluyoruz.
            // FirsOrDefault metodu çağırdığımız listede ki ilk veriyi alır.
            // remove metodu ile eğer böyle bir araç var ise ve galeride ise listeden bu aracın silinmesini sağlıyoruz.

            Araba a = this.Arabalar.Where(x => x.Plaka == plaka.ToUpper()).FirstOrDefault();

            if (a != null && a.Durum == DURUM.Galeride)
            {
                this.Arabalar.Remove(a);
            }
        }

        public void SahteVeriGir()
        {
            // Oluşturduğumuz Arabaekle metodu ile manuel bilgi girerek sahteveri oluştururuz.

            ArabaEkle("34arb3434", "FIAT", 70, ARAC_TIPI.Sedan);
            ArabaEkle("35arb3535", "KIA", 60, ARAC_TIPI.SUV);
            ArabaEkle("34us2342", "OPEL", 50, ARAC_TIPI.Hatchback);
        }

    }
}
