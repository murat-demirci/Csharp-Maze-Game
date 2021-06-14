using System;

namespace Labirent
{                                                       //camelcase yazi standarti ile yazilmistir
    class Program                                   // TUM ODEVI HICKIMSEDEN KOPYA ALMADAN VE HICKIMSEYE KOPYA VERMEDEN YAPTIGIMA SEREFIM UZERINE YEMIN EDIYORUM. 
    {                                                  //Oyunu kazanmak icin labirentten tamamen cikmak gerekir veya 
                                                       //tekrar yol secmek icin giris noktasindan tamamen cikmak gerekir
        static void Main(string[] args)
        {
            matrisSinif labirent = new matrisSinif();
            imlec kHarfi = new imlec();
            haritaOlustur haritaOlustur = new haritaOlustur();
            labirentCiz labirentCiz = new labirentCiz();
            
            labirent.matris();
            haritaOlustur.haritaOlstr();
            kHarfi.bombaOlustur1();
            kHarfi.bombaOlustur2();
        sonDurum:
            //Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            labirentCiz.labirentCiz1();
            Console.SetCursorPosition(0, 12);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("1. bomb location {0},{1}", 9 - kHarfi.bombX1, kHarfi.bombY1);
            Console.SetCursorPosition(0, 13);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("2. bomb location {0},{1}", 9 - kHarfi.bombX2, kHarfi.bombY2);
            kHarfi.X = kHarfi.Degerlendir();
            Console.SetCursorPosition(0, 14);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("For exit press Space!!");
            

            bool oyunBitir = true;
            bool option = true;
            
            

            do
                {
                kHarfi.puan();

                    kHarfi.ciz();
                    Console.Clear();
                    labirentCiz.labirentCiz2();
                    while (option)
                    {
                        if (degiskenler.dizi[kHarfi.Y, kHarfi.X] == 2)
                        {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("Game Over");
                        kHarfi.puan();
                        oyunBitir = false;
                        option = false;
                    }
                    else if (kHarfi.spaceBar == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.Clear();
                        Console.SetCursorPosition(15, 15);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("Game Closed, press enter!");
                        oyunBitir = false;
                        option = false;
                    }
                    else if (kHarfi.Y==0)
                        {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("Victory!!");
                        kHarfi.puan();
                        oyunBitir = false;
                        option = false;
                        
                        }
                    else if (degiskenler.dizi[kHarfi.Y,kHarfi.X]==1)
                    {
                        goto sonDurum;
                    }

                    }
                

                


            } while (oyunBitir!=false);
            if (kHarfi.spaceBar == false)
            //{
            //    Console.Clear();
            //    Console.Write("oyundan cikildi, pencereyi kapatmak icin enter a basiniz!");
            //    oyunBitir = false;
            //    option = false;
            //}







            Console.ReadLine();
        }
    }
   

    }
