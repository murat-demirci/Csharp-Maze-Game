using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Labirent
{
    class 
        haritaOlustur
    {
        public void haritaOlstr()
        {
            int secim;
            Random rndm = new Random();
             secim = rndm.Next(1, 3);
            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9;j++)
                {                                                   //<-- rastgele olusturulan haritayi kurallar ile tekrar
                    if (degiskenler.dizi[9, j] == 1)                //    duzenleme
                    {
                        switch (secim)
                        {
                            case 1:
                                degiskenler.dizi[9 - i, j] = 1;
                                degiskenler.dizi[9 - i, j + 1] = 1;

                                break;
                            case 2:
                                degiskenler.dizi[9 - i, j] = 1;

                                break;

                        }
                        degiskenler.dizi[0, j+1] = 1;
                        
                    }
                }      
 
            }


            for (int i = 0; i < 10; i++)
            {
                degiskenler.dizi[i, 0] = 0;
                degiskenler.dizi[i, 9] = 0;
            }

        }
    }
}
