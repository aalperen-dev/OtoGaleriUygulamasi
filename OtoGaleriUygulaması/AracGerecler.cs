using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtoGaleriUygulaması
{
    class AracGerecler
    {   
        static public bool PlakaMi(string veri)
        {
            //Ön koşul: Girilen plaka minimum 7 maksimum 9 haneli, ilk iki hanesi harflerden oluşmalı,
            //6 ve 7.(5 ve 6. indeks) haneler sayı olmalı ve plakaların 3. haneleri(2. indeks) mutlaka harf olmalıdır.
            if (veri.Length > 6 && veri.Length < 10 && SayiMi(veri.Substring(0, 2)) && HarfMi(veri.Substring(2, 1)))
            {
                //11A1111 formatındaki plakalar için uygun şartlar sağlanıyorsa plakadır.
                if (veri.Length == 7 && SayiMi(veri.Substring(3)))
                {
                    return true;
                }
                //11AA111 ve 11AA1111 formatındaki plakalar için uygun şartlar sağlanıyorsa plakadır.
                else if (veri.Length < 9 && HarfMi(veri.Substring(3, 1)) && SayiMi(veri.Substring(4)))
                {
                    return true;
                }
                //11AAA11, 11AAA111 ve 11AAA1111 formatındaki plakalar için uygun şartlar sağlanıyorsa plakadır.
                else if (HarfMi(veri.Substring(3, 2)) && SayiMi(veri.Substring(5)))
                {
                    return true;
                }
            }
            return false;    //Bu şartlardan hiçbiri sağlanmıyorsa plaka değildir
        }

        //string metotların çalışması için veriyi string tipinde aldık, daha sonra char olarak karşılaştırma yapabilmek için 0. indek deki elemanı
        //ASCII tablodaki değerine göre kontrol ettik.
        static public bool HarfMi(string veri)
        {
            veri = veri.ToUpper();

            for (int i = 0; i < veri.Length; i++)
            {
                int kod = (int)veri[i];//Karakterin ASCII kod tablosundaki değerini alır.
                if ((kod >= 65 && kod <= 90) != true)//büyük harflerin ASCII tablodaki değerlerleri dışında girilmişse metot false döndürür.
                {
                    return false;
                }
            }
            return true;
        }

        //foreach birden fazla sayı girildiğinde birden fazlası için ASCII tablodaki yerini kontrol edebilmeli, o yüzden bool kontrol
        //değerini oluşturduk.

        static public bool SayiMi(string giris)
        {
            foreach (char item in giris)
            {
                if (!Char.IsNumber(item))
                {
                    return false;
                }
            }
            return true;
        }

        static public string YaziAl(string yazi)
        {
            int sayi;
            //TryParse metodu bool değişkeni sonucu üretir.
            //Eğer girilen string veri int veriye dönüştürülebiliyor ise hata döndürülüp yazı girilmesi istenir.
            //Uygun giriş sağlanıldığında girilen giriş döndürülür.

            do
            {
                try
                {
                    Console.Write(yazi);
                    string giris = Console.ReadLine().ToUpper();

                    if (int.TryParse(giris, out sayi))
                    {
                        throw new Exception("Giriş tanımlanamadı. Tekrar deneyin.");
                    }
                    else
                    {
                        return giris;
                    }
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }           
            } while (true);
        }

        static public int SayiAl(string mesaj)
        {
            int sayi;

            //TryParse metodu bool bir değişken döndür.
            //Eğer metod girilen veriyi int bir değişkene çeviremiyorsa hata döndürecek ve tekrar giriş isteyecek.
            // Koşul sağlandıysa girilen veri döndürülür.

            do
            {
                try
                {
                    Console.Write(mesaj);
                    string giris = Console.ReadLine();

                    if (int.TryParse(giris, out sayi))
                    {
                        return sayi;
                    }
                    else
                    {
                        throw new Exception("Giriş tanımlanamadı. Tekrar deneyin.");
                    }

                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (true);
        }
  
        static public string PlakaAl(string mesaj)
        {
            string plaka;
            do
            {
                try
                {
                    Console.Write(mesaj);
                    plaka = Console.ReadLine().ToUpper();
                    if (!PlakaMi(plaka))
                    {
                        throw new Exception("Bu şekilde plaka girişi yapamazsınız. Tekrar deneyin.");                        
                    }
                    return plaka;
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }          
            } while (true);
        }

        static public ARAC_TIPI AracTipiAl()
        {
            Console.WriteLine("Araç tipi: ");
            Console.WriteLine("SUV için 1");
            Console.WriteLine("Hatchback için 2");
            Console.WriteLine("Sedan için 3");

            while (true)
            {
                try
                {
                    Console.Write("Seçiminiz: ");
                    string secim = Console.ReadLine();
                    switch (secim)
                    {
                        case "1":
                            return ARAC_TIPI.SUV;

                        case "2":
                            return ARAC_TIPI.Hatchback;

                        case "3":
                            return ARAC_TIPI.Sedan;

                        default:
                            throw new Exception("Giriş tanımlanamadı. Tekrar deneyin.");                           
                    }

                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }              
            }     
        }
    }
}




