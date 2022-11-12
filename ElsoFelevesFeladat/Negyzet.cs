using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElsoFelevesFeladat
{
    class Negyzet
    {
        private int[,] negyzet;

        public Negyzet(int oszlopokSzama, int sorokSzama)
        {
            this.negyzet = new int[oszlopokSzama, sorokSzama];
            UjJatek();
            
        }
        public void UjJatek()
        {
            int szelesseg = this.negyzet.GetLength(0) + 2;
            int magassag = this.negyzet.GetLength(0) + 4;
            Console.SetWindowSize(szelesseg, magassag);
            NegyzetFeltoltese();
        }
        public int NegyzetMagassaga()
        {
            return this.negyzet.GetLength(0);
        }
        public int NegyzetHossza()
        {
            return this.negyzet.GetLength(1);
        }
        //megnézi, hogy üres-e a négyzet
        public bool UresEANegyzet()
        {
            for (int i = 0; i < this.negyzet.GetLength(0); i++)
            {
                for (int j = 0; j < this.negyzet.GetLength(1); j++)
                {
                    if (this.negyzet[i,j] !=0 )
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //megnézi, hogy tele van-e a négyzet
        public bool TeleVanEANegyzet()
        {
            for (int i = 0; i < this.negyzet.GetLength(0); i++)
            {
                for (int j = 0; j < this.negyzet.GetLength(1); j++)
                {
                    if (this.negyzet[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //feltölti a négyzeteket random számokkal 1 és 4 között
        private void NegyzetFeltoltese()
        {
            Random random = new Random();
            for (int i = 0; i < this.negyzet.GetLength(0) / 2; i++)
            {
                for (int j = 0; j < this.negyzet.GetLength(1); j++)
                {
                    this.negyzet[i, j] = random.Next(1, 5);
                    //this.negyzet[i, j] = 0;
                    
                }
            }
            for (int i = this.negyzet.GetLength(0) / 2; i < this.negyzet.GetLength(0); i++)
            {
                for (int j = 0; j < this.negyzet.GetLength(1); j++)
                {
                    this.negyzet[i, j] = 0;
                }
            }
        }
        //kiirajzolja a négyzetet a random számok segítségével
        public void NegyzetKirajzolasa(int szinSzama, string jatekos)
        {
            Console.Clear();
            Console.ForegroundColor = (ConsoleColor)szinSzama;
            Console.WriteLine(jatekos);
            Console.ResetColor();
            Console.Write("+");
            for (int i = 0; i < this.negyzet.GetLength(1); i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
            for (int i = 0; i < this.negyzet.GetLength(0); i++)
            {
                for (int j = 0; j < this.negyzet.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Console.Write("|");
                    }
                    Console.BackgroundColor = (ConsoleColor)this.negyzet[i, j];
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("|");
            }
            Console.Write("+");
            for (int i = 0; i < this.negyzet.GetLength(1); i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
        }
        public void NegyzetEpitese(int oszlopSzam, int szinSzama)
        {
            int index = negyzet.GetLength(0) - 1;
            while (index >= 0 && negyzet[index, oszlopSzam] == 0)
            {
                index--;
            }
            if (index != negyzet.GetLength(0)-1)
            {
                negyzet[index + 1, oszlopSzam] = szinSzama;
            }
            
        }
        
        public void NegyzetRombolasa(int oszlopSzam, int szinSzama)
        {
            int sorszam = negyzet.GetLength(0) - 1;
            while (sorszam > 0 && negyzet[sorszam, oszlopSzam] == 0)
            {
                sorszam--;
            }
            bool vanEKorulotteUgyanOlyanSzin = false;
            if (negyzet[sorszam, oszlopSzam] == szinSzama)//ha megegyezik a talált szín
            {
                vanEKorulotteUgyanOlyanSzin = true;
                negyzet[sorszam, oszlopSzam] = 0;
                //ha a negyzet legalján van akkor ott romboljon, ha nem akkor alatta eggyel és ugyan ott
                if (sorszam == this.NegyzetMagassaga()-1)
                {
                    RombolasKorulotte(sorszam, oszlopSzam, szinSzama, ref vanEKorulotteUgyanOlyanSzin);
                }
                else
                {
                    RombolasKorulotte(sorszam + 1, oszlopSzam, szinSzama, ref vanEKorulotteUgyanOlyanSzin);
                    RombolasKorulotte(sorszam, oszlopSzam, szinSzama, ref vanEKorulotteUgyanOlyanSzin);
                }
                
            }
            else if(negyzet[sorszam, oszlopSzam] != szinSzama && (sorszam != this.NegyzetMagassaga() - 1 && sorszam != 0))
            //ha nem egyezik meg a színszama és nem a matrix tetején vagy alján van akkor a megtalált szín fölött rombol
            {
                RombolasKorulotte(sorszam + 1, oszlopSzam, szinSzama, ref vanEKorulotteUgyanOlyanSzin);
            }
            else if (negyzet[sorszam, oszlopSzam] != szinSzama && sorszam == 0)
            //ha nem egyezik meg a szinszama de a matrix tetején, helyezkedik el
            {
                if (negyzet[sorszam, oszlopSzam] != 0)// megnézzük, hogy ott szín van vagy már ki van lőve
                {
                    RombolasKorulotte(sorszam + 1, oszlopSzam, szinSzama, ref vanEKorulotteUgyanOlyanSzin);
                }
                else
                {
                    RombolasKorulotte(sorszam, oszlopSzam, szinSzama, ref vanEKorulotteUgyanOlyanSzin);
                }
            }
            if (!vanEKorulotteUgyanOlyanSzin)//ha nem talált és nem volt körülötte ugyan olyan szín
            {
                if (sorszam != this.NegyzetMagassaga()-1)
                {
                    if (sorszam == 0)
                    {
                        if (negyzet[sorszam, oszlopSzam] != 0)// megnézzük, hogy ott szín van vagy már ki van lőve
                        {
                            negyzet[sorszam + 1, oszlopSzam] = szinSzama;
                        }
                        else
                        {
                            negyzet[sorszam, oszlopSzam] = szinSzama;
                        }
                    }
                    else
                    {
                        negyzet[sorszam + 1, oszlopSzam] = szinSzama;
                    }
                }
            }
        }
        public void RombolasKorulotte(int sorszam, int oszlopSzam, int szinSzama, ref bool vanEKorulotteUgyanOlyanSzin)
        {
            if (sorszam - 1 >= 0)
            {
                if (negyzet[sorszam - 1, oszlopSzam] == szinSzama)//megnézzük, hogy felette van e ugyan olyan szín
                {
                    negyzet[sorszam - 1, oszlopSzam] = 0;
                    vanEKorulotteUgyanOlyanSzin = true;
                    if (sorszam != 0)
                    {
                        RombolasKorulotte(sorszam - 1, oszlopSzam,  szinSzama, ref vanEKorulotteUgyanOlyanSzin);
                    }
                    
                }
            }

            if (oszlopSzam - 1 >= 0)
            {
                if (negyzet[sorszam, oszlopSzam - 1] == szinSzama)//megnézzük, hogy balra van e ugyan olyan szín
                {
                    negyzet[sorszam, oszlopSzam - 1] = 0;
                    vanEKorulotteUgyanOlyanSzin = true;
                    if (oszlopSzam != 0)
                    {
                        RombolasKorulotte(sorszam, oszlopSzam - 1, szinSzama, ref vanEKorulotteUgyanOlyanSzin);
                    }
                }
            }

            if (oszlopSzam + 1 <= negyzet.GetLength(1) - 1)
            {
                if (negyzet[sorszam, oszlopSzam + 1] == szinSzama)//megnézzük, hogy jobbra van e ugyan olyan szín
                {
                    negyzet[sorszam, oszlopSzam + 1] = 0;
                    vanEKorulotteUgyanOlyanSzin = true;
                    if (oszlopSzam != negyzet.GetLength(1) - 1)
                    {
                        RombolasKorulotte(sorszam, oszlopSzam + 1, szinSzama, ref vanEKorulotteUgyanOlyanSzin);
                    }
                }
            }

            if (sorszam + 1 <= negyzet.GetLength(0) - 1)
            {
                if (negyzet[sorszam + 1, oszlopSzam] == szinSzama)//megnézzük, hogy alatta van e ugyan olyan szín
                {
                    negyzet[sorszam + 1, oszlopSzam] = 0;
                    vanEKorulotteUgyanOlyanSzin = true;
                    if (sorszam != negyzet.GetLength(0) - 1)
                    {
                        RombolasKorulotte(sorszam + 1, oszlopSzam, szinSzama, ref vanEKorulotteUgyanOlyanSzin);
                    }
                }
            }
            
        }
    }
}
