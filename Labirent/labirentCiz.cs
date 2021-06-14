using System;
using System.Collections.Generic;
using System.Text;

namespace Labirent
{
    class labirentCiz
    {
        public void labirentCiz1()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (degiskenler.dizi[i,j]==2)                       //<-- bombalarin gozukmedigi harita
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("1");
                    }
                    else if (degiskenler.dizi[i, j] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(degiskenler.dizi[i, j]);

                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(degiskenler.dizi[i, j]);
                    }

                    
                }
                Console.Write("\n");

            }
        }
        public void labirentCiz2()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)                            //<-- bombalarin gozuktugu harita
                {
                    if (degiskenler.dizi[i,j]==2)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(degiskenler.dizi[i, j]);
                    }
                    else if (degiskenler.dizi[i,j]==0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(degiskenler.dizi[i, j]);

                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(degiskenler.dizi[i, j]);
                    }


                }
                Console.Write("\n");

            }
        }
    }
}
