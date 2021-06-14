using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Labirent
{
    class matrisSinif
    {
        public int satir = 10, sutun = 10;
        
        public void matris()
        {
            Random gen = new Random();
            

            for (int i = 0; i < satir; i++)
            {
                for (int j = 0; j < sutun; j++)
                {
                    degiskenler.dizi[i, j] = gen.Next(0, 2);            //<-- 0 - 1 ile rastgele harita olusturma
                    degiskenler.dizi[i, 0] = 0;
                    degiskenler.dizi[i, 9] = 0;
                    degiskenler.dizi[9, j] = 0;
                    degiskenler.dizi[0, j] = 0;
                }
            }
            degiskenler.dizi[9, 2] = 1;
            degiskenler.dizi[9, 5] = 1;
            degiskenler.dizi[9, 8] = 1;
        }

    }
}
    


