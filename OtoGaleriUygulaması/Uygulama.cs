using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OtoGaleriUygulaması
{
    class Uygulama
    {
        Galeri OtoGaleri = new Galeri();  //Nesnenin tüm metodlardan ulaşılabilmesi için dışarıda tanımladık.

        public void Calistir()
        {
            //Çalıştır metodu oluşturarak oluşturduğumuz metodlar
            //ile switch case yapısı ile kullanıcıya seçim yaptırarak işlem sağlatıyoruz.

            Menu();
            while (true)
            {
                string secim = SecimAl();
                Console.WriteLine();
                switch (secim)
                {

                    case "K":
                    case "1":
                        ArabaKirala();
                        break;
                    case "T":
                    case "2":
                        ArabaTeslimi();
                        break;
                    case "R":
                    case "3":
                        ArabalariListele(DURUM.Kirada);
                        break;
                    case "M":
                    case "4":
                        ArabalariListele(DURUM.Galeride);
                        break;
                    case "A":
                    case "5":
                        ArabalariListele(DURUM.Empty);
                        break;
                    case "I":
                    case "6":
                        KiralamaIptal();
                        break;
                    case "Y":
                    case "7":
                        YeniAraba();
                        break;
                    case "S":
                    case "8":
                        ArabaSil();
                        break;
                    case "G":
                    case "9":
                        BilgileriGoster();
                        break;
                }
            }
        }
        public void Menu()
        {
            Console.WriteLine("Galeri Otomasyon                   ");
            Console.WriteLine("1- Araba Kirala(K)                 ");
            Console.WriteLine("2- Araba Teslim Al(T)              ");
            Console.WriteLine("3- Kiradaki Arabaları Listele(R)   ");
            Console.WriteLine("4- Galerideki Arabaları Listele(M) ");
            Console.WriteLine("5- Tüm Arabaları Listele(A)        ");
            Console.WriteLine("6- Kiralama İptali(I)              ");
            Console.WriteLine("7- Araba Ekle(Y)                   ");
            Console.WriteLine("8- Araba Sil(S)                    ");
            Console.WriteLine("9- Bilgileri Göster(G)             ");
        }
        public string SecimAl()
        {
            Console.WriteLine();
            Console.Write("Seçiminiz: ");
            return Console.ReadLine().ToUpper();
        }

        public void ArabaKirala()
        {
            Console.WriteLine("-Araba Kirala-");
            Console.WriteLine();
            try
            {
                if (OtoGaleri.Arabalar.Count == 0)
                {
                    throw new Exception("Galeride hiç araba yok.");
                }
                else if (OtoGaleri.GaleridekiAracSayisi == 0)
                {
                    throw new Exception("Tüm araçlar kirada.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            string plaka;
            while (true)
            {
                // Kullanıcıdan almış olduğumuz plaka verisi oluşturduğumuz kiralamakontrol metodu ile
                // doğru plaka girip girmediği ve bu arabanın olup olmadığını kontrol ediyoruz.
                // sayial metodu ile kullanıcının girdiği verinin sadece sayı girip girmediği kontrol
                // edilip eğer harf girişi yaptıysa hata döndürüp tekrar giriş istiyoruz.
                // galeride oluşturduğumu kiralama metoduna kullancıdan aldığımız verileri gönderek  kiralama işlemini yapıyoruz.

                plaka = AracGerecler.PlakaAl("Kiralanacak arabanın plakası: ");

                DURUM durum = OtoGaleri.DurumGetir(plaka);

                if (durum == DURUM.Kirada)
                {
                    Console.WriteLine("Araba şu anda kirada. Farklı araba seçiniz.");
                }
                else if (durum == DURUM.Empty)
                {
                    Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                }
                else
                {
                    break;
                }
            }
            int sure = AracGerecler.SayiAl("Kiralanma süresi: ");
            OtoGaleri.ArabaKirala(plaka, sure);
            Console.WriteLine();
            Console.WriteLine(plaka.ToUpper() + " plakalı araba " + sure + " saatliğine kiralandı.");
        }

        public void ArabaTeslimi()
        {
            Console.WriteLine("-Araba Teslim Al-");
            Console.WriteLine();
            try
            {
                if (OtoGaleri.Arabalar.Count == 0)
                {
                    throw new Exception("Galeride hiç araba yok.");
                }
                else if (OtoGaleri.KiradakiAracSayisi == 0)
                {
                    throw new Exception("Kirada hiç araba yok.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // Kullanıcıdan aldığımız plaka verisi ile arabanın olup olmadığı kirada mı değil mi?
            // kontrol yapılıp eğer teslimata uygun değilse hata döndürüp tekrar veri istenir.
            // aldığımız veriyi oluşturduğumuz galeride ki teslim alma metoduna göndererek işlemi tamamlıyoruz.
            string plaka;

            while (true)
            {
                plaka = AracGerecler.PlakaAl("Teslim edilecek arabanın plakası: ");
                DURUM durum = OtoGaleri.DurumGetir(plaka);
                if (durum == DURUM.Galeride)
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                }
                else if (durum == DURUM.Empty)
                {
                    Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                }
                else
                {
                    break;
                }
            }
            OtoGaleri.ArabaTeslimAl(plaka);
            Console.WriteLine();
            Console.WriteLine("Araba galeride beklemeye alındı.");
        }

        public void ArabalariListele(DURUM durum)
        {
            if (durum == DURUM.Kirada)
            {
                Console.WriteLine("-Kiradaki Arabalar-");
                Console.WriteLine();
            }
            else if (durum == DURUM.Galeride)
            {
                Console.WriteLine("-Galerideki Arabalar-");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("-Tüm Arabalar-");
                Console.WriteLine();
            }
            ArabaListele(OtoGaleri.ArabaListesiGetir(durum));
        }

        public void KiralamaIptal()
        {
            // Eğer hiç bir araba kiralanmadıysa bu hatayı döndürecek.
            Console.WriteLine("-Kiralama İptali-");
            Console.WriteLine();
            try
            {
                if (OtoGaleri.KiradakiAracSayisi == 0)
                {
                    throw new Exception("Kirada araba yok.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // kullanıcıdan aldığımız plaka verisinin uygun , doğru olup olmadığı kontrol edilip
            // eğer doğru değilse hata döndürülüp tekrar giriş talep edilir.
            // Kiralamaiptal metodu na kullanıcıdan aldığımız veri gönderek kiralamayı iptal ediyoruz.

            string plaka;

            while (true)
            {
                plaka = AracGerecler.PlakaAl("Kiralaması iptal edilecek arabanın plakası:  ");
                DURUM durum = OtoGaleri.DurumGetir(plaka);
                if (durum == DURUM.Galeride)
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Araba zaten galeride.");
                }
                else if (durum == DURUM.Empty)
                {
                    Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                }
                else
                {
                    break;
                }
            }

            OtoGaleri.KiralamaIptal(plaka);
            Console.WriteLine();
            Console.WriteLine("İptal gerçekleştirildi.");

        }

        public void YeniAraba()
        {
            Console.WriteLine("-Araba Ekle-");
            Console.WriteLine();

            string plaka; // Döngü dışarısında değişken tanımlıyoruz çünkü döngü içerisinde ki değişkeni metoda gönderemiyoruz.
            while (true)
            {
                plaka = AracGerecler.PlakaAl("Plaka: ");

                // Araçgereçlerde oluştuğumuz plaka alma metodu ile  kullanıcıdan girdiği
                // veri kontrol edilip hata varsa hata döndürülüp tekrar giriş istenilir.

                if (OtoGaleri.DurumGetir(plaka) != DURUM.Empty) 
                //Eğer koşul sağlanıyoruz galeride böyle bir araba kayıtlıdır tekrar bilgi alınır.
                {
                    throw new Exception("Aynı plakada araba mevcut. Girdiğiniz plakayı kontrol edin.");
                }
                else
                {
                    break; // if koşulu sağlandıysa araba yoktur yeni araba için bilgiler alınır.
                }
            }
            //Araçgereçlere tanımladığımız ve hazırladığımız metodlar ile kulannıcıdan
            //veri alarak ekle metoduna gönderiyoruz ve yeni araba kaydı yapılmış oluyor.
            string marka = AracGerecler.YaziAl("Marka: ");
            float kiralamaBedeli = AracGerecler.SayiAl("Kiralama bedeli: ");
            ARAC_TIPI aracTipi = AracGerecler.AracTipiAl();
            OtoGaleri.ArabaEkle(plaka, marka, kiralamaBedeli, aracTipi);
            Console.WriteLine();
            Console.WriteLine("Araba başarılı bir şekilde eklendi.");
        }

        public void ArabaSil()
        {
            Console.WriteLine("-Araba Sil-");
            Console.WriteLine();
            string plaka;
            try
            {

                if (OtoGaleri.Arabalar.Count == 0)
                {
                    throw new Exception("Galeride silinecek araba yok.");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            while (true)
            {
                plaka = AracGerecler.PlakaAl("Silmek istediğiniz arabanın plakasını giriniz:");

                if (OtoGaleri.DurumGetir(plaka) == DURUM.Empty)
                {
                    Console.WriteLine("Galeriye ait bu plakada bir araba yok.");
                }
                else if (OtoGaleri.DurumGetir(plaka) == DURUM.Kirada)
                {
                    Console.WriteLine("Araba kirada olduğu için silme işlemi gerçekleştirilemedi.");
                }
                else
                {
                    break;
                }

                OtoGaleri.ArabaSil(plaka);
                Console.WriteLine();
                Console.WriteLine("Araba silindi.");
            }
        }

        public void ArabaListele(List<Araba> liste)
        {
            //Toplam araç sayısı 0 ise listelenecek araç yok uyarısı verilsin.
            if (liste.Count == 0)
            {
                Console.WriteLine("Listelenecek araç yok.");
                return;
            }
            Console.WriteLine("Plaka".PadRight(14) + "Marka".PadRight(12) + "K. Bedeli".PadRight(12) + "Araç Tipi".PadRight(12) +
                    "K. Sayısı".PadRight(12) + "Durum");
            Console.WriteLine("".PadRight(70, '-'));

            foreach (Araba item in liste)
            {
                Console.WriteLine(item.Plaka.PadRight(14) + item.Marka.PadRight(12) + item.KiralamaBedeli.ToString().PadRight(12) + item.AracTipi.ToString().PadRight(12) + item.KiralamaSayisi.ToString().PadRight(12) + item.Durum);
            }
        }

        public void BilgileriGoster()
        {
            // Galeri üzerine kaydettiğimiz araçların bilgilerini direkt classa ulaşarak bilgilerini döndürüyoruz.

            Console.WriteLine("-Galeri Bilgileri-");
            Console.WriteLine("Toplam araba sayısı: " + OtoGaleri.ToplamAracSayisi);
            Console.WriteLine("Kiradaki araba sayısı: " + OtoGaleri.KiradakiAracSayisi);
            Console.WriteLine("Bekleyen araba sayısı: " + OtoGaleri.GaleridekiAracSayisi);
            Console.WriteLine("Toplam araba kiralama süresi: " + OtoGaleri.ToplamAracKiralamaSuresi);
            Console.WriteLine("Toplam araba kiralama adedi: " + OtoGaleri.ToplamAracKirlamaAdedi);
            Console.WriteLine("Ciro: " + OtoGaleri.Ciro);

        }
    }
}
