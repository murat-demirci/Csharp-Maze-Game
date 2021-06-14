using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace Labirent
{

    class imlec
    {

        public int skor = 0;
        public int X , Y = 9;
        public int deger,tercih;
        public bool spaceBar=true,cikisYap=true;
        public void ciz()
        {
            bool durum = true;
            while (durum)
            {
                labirentCiz labirentCiz = new labirentCiz();
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key==ConsoleKey.G)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    labirentCiz.labirentCiz2();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.BackgroundColor = ConsoleColor.Black;                   //g tusu yeri
                    Console.SetCursorPosition(X, Y);
                    Console.Write("K");
                    Console.SetCursorPosition(0,11);
                    ConsoleKeyInfo tus = Console.ReadKey();
                    if (tus.Key == ConsoleKey.G)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        labirentCiz.labirentCiz1();
                    }
                }
                else if (info.Key == ConsoleKey.Spacebar)
                {
                    spaceBar = false;                                       //oyundan cikmak icin basilan space tusu
                    cikisYap = false;
                    durum = false;
                }
                if (info.Key == ConsoleKey.A)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(degiskenler.dizi[Y, X]);
                    X--;
                    if (X == 9)
                    {
                        X = 8;
                    }
                    if (X == 0)
                    {
                        if (degiskenler.dizi[Y, X] == 0)
                        {
                            X++;
                            skor=skor-2;
                            Console.Clear();
                            Console.SetCursorPosition(5, 10);
                            Console.Write("WALL!! press enter");
                            info = Console.ReadKey();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Gray;
                            labirentCiz.labirentCiz1();
                            Console.SetCursorPosition(0, 12);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("1. bomb location {0},{1}", 9 - bombX1, bombY1);
                            Console.SetCursorPosition(0, 13);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("2. bomb location {0},{1}", 9 - bombX2, bombY2);
                            Console.SetCursorPosition(0, 14);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("For exit press Space!!");
                        }
                        X = 1;
                    }
                    if (Y == 11)
                    {
                        Y = 10;
                    }
                    if (Y == -1)
                    {
                        Y = 0;
                    }
                    if (degiskenler.dizi[Y, X] == 1)
                    {
                        skor++;
                    }
                    else if (degiskenler.dizi[Y, X] == 0)
                    {
                        X++;
                        skor--;
                        Console.Clear();
                        Console.SetCursorPosition(5, 10);
                        Console.Write("WALL!! press enter");
                        info = Console.ReadKey();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        labirentCiz.labirentCiz1();
                        Console.SetCursorPosition(0, 12);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("1. bomb location {0},{1}", 9 - bombX1, bombY1);
                        Console.SetCursorPosition(0, 13);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("2. bomb location {0},{1}", 9 - bombX2, bombY2);
                        Console.SetCursorPosition(0, 14);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("For exit press Space!!");
                    }
                    else if (degiskenler.dizi[Y, X] == 2)
                    {
                        //skor--;
                        durum = false;
                    }

                    puan();
                }
                if (info.Key == ConsoleKey.D)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(degiskenler.dizi[Y, X]);
                    X++;
                    if (X == 9)
                    {
                        if (degiskenler.dizi[Y, X] == 0)
                        {
                            X--;
                            skor=skor-2;
                            Console.Clear();
                            Console.SetCursorPosition(5, 10);
                            Console.Write("WALL!! press enter");
                            info = Console.ReadKey();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Gray;
                            labirentCiz.labirentCiz1();
                            Console.SetCursorPosition(0, 12);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("1. bomb location {0},{1}", 9 - bombX1, bombY1);
                            Console.SetCursorPosition(0, 13);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("2. bomb location {0},{1}", 9 - bombX2, bombY2);
                            Console.SetCursorPosition(0, 14);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("For exit press Space!!");
                        }
                        X = 8;
                    }
                    if (X == 0)
                    {
                        X = 1;
                    }
                    if (Y == 10)
                    {
                        Y = 9;
                    }
                    if (Y == -1)
                    {
                        Y = 0;
                    }
                    if (degiskenler.dizi[Y, X] == 1)
                    {
                        skor++;
                    }
                    else if (degiskenler.dizi[Y, X] == 0)
                    {
                        X--;
                        skor--;
                        Console.Clear();
                        Console.SetCursorPosition(5, 10);
                        Console.Write("WALL!! press enter");
                        info = Console.ReadKey();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        labirentCiz.labirentCiz1();
                        Console.SetCursorPosition(0, 12);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("1. bomb location{0},{1}", 9 - bombX1, bombY1);
                        Console.SetCursorPosition(0, 13);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("2. bomb location {0},{1}", 9 - bombX2, bombY2);
                        Console.SetCursorPosition(0, 14);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("For exit press Space!!");
                    }
                    else if (degiskenler.dizi[Y, X] == 2)
                    {
                        //skor--;
                        durum = false;
                    }

                    puan();

                }

                if (info.Key == ConsoleKey.W)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(degiskenler.dizi[Y, X]);
                    Y--;
                    if (X == 9)
                    {
                        X = 8;
                    }
                    if (X == 0)
                    {
                        X = 1;
                    }
                    if (Y == 10)
                    {
                        Y = 9;
                    }
                    if (Y == -1)
                    {
                        skor--;
                        Y = 0;
                        if (degiskenler.dizi[Y, X] == 0)
                        {
                            Y++;
                            skor=skor-2;
                            Console.Clear();
                            Console.SetCursorPosition(5, 10);
                            Console.Write("WALL!! press enter");
                            info = Console.ReadKey();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Gray;
                            labirentCiz.labirentCiz1();
                            Console.SetCursorPosition(0, 12);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("1. bomb location {0},{1}", 9 - bombX1, bombY1);
                            Console.SetCursorPosition(0, 13);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("2. bomb location {0},{1}", 9 - bombX2, bombY2);
                            Console.SetCursorPosition(0, 14);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("For exit press Space!!");
                        }
                        durum = false;
                    }
                    if (degiskenler.dizi[Y, X] == 1)
                    {
                        skor++;
                    }
                    else if (degiskenler.dizi[Y, X] == 0)
                    {
                        Y++;
                        skor--;
                        Console.Clear();
                        Console.SetCursorPosition(5, 10);
                        Console.Write("WALL!! press enter");
                        info = Console.ReadKey();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        labirentCiz.labirentCiz1();
                        Console.SetCursorPosition(0, 12);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("1. bomb location {0},{1}", 9 - bombX1, bombY1);
                        Console.SetCursorPosition(0, 13);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("2. bomb location {0},{1}", 9 - bombX2, bombY2);
                        Console.SetCursorPosition(0, 14);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("For exit press Space!!");
                    }
                    else if (degiskenler.dizi[Y, X] == 2)
                    {
                        //skor--;
                        durum = false;
                    }
                    else if (Y == 0)
                    {
                        durum = false;
                    }

                    puan();
                }

                if (info.Key == ConsoleKey.S)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(degiskenler.dizi[Y, X]);
                    Y++;
                    if (X == 9)
                    {
                        X = 8;
                    }
                    if (X == 0)
                    {
                        X = 1;
                    }
                    if (Y == 10)
                    {
                        Y = 9;
                        skor--;
                        if (degiskenler.dizi[Y, X] == 0)
                        {
                            Y--;
                            skor=skor-2;
                            Console.Clear();
                            Console.SetCursorPosition(5, 10);
                            Console.Write("WALL!! press enter");
                            info = Console.ReadKey();
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Gray;
                            labirentCiz.labirentCiz1();
                            Console.SetCursorPosition(0, 12);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("1. bomb location {0},{1}", 9 - bombX1, bombY1);
                            Console.SetCursorPosition(0, 13);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("2. bomb location {0},{1}", 9 - bombX2, bombY2);
                            Console.SetCursorPosition(0, 14);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("For exit press Space!!");
                        }
                        durum = false;
                    }
                    if (Y == -1)
                    {
                        Y = 0;
                    }
                    if (degiskenler.dizi[Y, X] == 1)
                    {
                        skor++;
                    }
                    else if (degiskenler.dizi[Y, X] == 0)
                    {
                        Y--;
                        skor--;
                        Console.Clear();
                        Console.SetCursorPosition(5, 10);
                        Console.Write("WALL!! press enter");
                        info = Console.ReadKey();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Gray;
                        labirentCiz.labirentCiz1();
                        Console.SetCursorPosition(0, 12);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("1. bomb location {0},{1}", 9 -bombX1, bombY1);
                        Console.SetCursorPosition(0, 13);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("2. bomb location {0},{1}", 9 - bombX2,bombY2);
                        Console.SetCursorPosition(0, 14);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("For exit press Space!!");
                    }
                    else if (degiskenler.dizi[Y, X] == 2)
                    {
                        //skor--;
                        durum = false;
                    }
                    else if (Y == 0)
                    {
                        durum = false;
                    }

                    puan();
                }
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("K");
                Console.SetCursorPosition(X, Y);
                
            }
           

        }
        public int Degerlendir()
        {
            
            bool durum = true;
            while (durum==true)
            {
                
                Console.SetCursorPosition(15, 0);
                Console.Write("Enter start point 1-2-3 ");
                
                tercih = Convert.ToInt32(Console.ReadLine());
                if (tercih== 1)
                {
                    deger=2;
                    durum = false;
                }                                                                               //hareketin baslangic konumu secimi ve atanmasi
                 else if (tercih == 2)
                {
                    deger=5;
                    durum = false;
                }
                 else if (tercih == 3)
                {
                    deger=8;
                    durum = false;
                }
                else
                {
                    durum = true;
                }
                
            }

            return deger;


        }

        public void puan()
        {
            if (skor>=0 && skor<10)
            {
                Console.SetCursorPosition(15, 5);
                Console.WriteLine("Score :  " + skor);
            }
            else if (skor<0 &&skor>-10)
            {
                Console.SetCursorPosition(15, 5);
                Console.WriteLine("Score : " + skor);                //puan yazdirma
            }
            else if(skor>=10)
            {
                Console.SetCursorPosition(15, 5);
                Console.WriteLine("Score : " + skor);
            }
            else if (skor<-10)
            {
                Console.SetCursorPosition(15, 5);
                Console.WriteLine("Score :" + skor);
            }

            
                

            
        }
        public int bombX1, bombY1;
        public void bombaOlustur1()
        {
            int frag1 = 0;
            Random gen = new Random();
            while (frag1!=1)
            {
                bombX1 = gen.Next(2, 8);
                bombY1 = gen.Next(2, 8);
                if (degiskenler.dizi[bombX1, bombY1] == 1)              //bomba olusturma
                {
                    degiskenler.dizi[bombX1, bombY1] = 2;
                    frag1++;
                }
            }
                        
                      
        }
        public int bombX2, bombY2;
        public void bombaOlustur2()
        {
            int frag2 = 0;
            Random gen = new Random();
            while (frag2 != 1)
            {
            bombX2 = gen.Next(2, 8);
            bombY2 = gen.Next(2, 8);
            if (degiskenler.dizi[bombX2, bombY2] == 1)
            {
                degiskenler.dizi[bombX2, bombY2] = 2;
                frag2++;

            }
            }
            


        }



    }
}

    


