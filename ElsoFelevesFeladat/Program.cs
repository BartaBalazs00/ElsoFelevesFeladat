using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElsoFelevesFeladat
{
    class Program
    {
        //feltölti a négyzeteket random számokkal 1 és 4 között
        public static void NegyzetFeltoltese(ref int[,] negyzet)
        {
            Random random = new Random();
            for (int i = 0; i < negyzet.GetLength(0) / 2; i++)
            {
                for (int j = 0; j < negyzet.GetLength(1); j++)
                {
                    negyzet[i, j] = random.Next(1, 5);
                }
            }
            for (int i = negyzet.GetLength(0) / 2; i < negyzet.GetLength(0); i++)
            {
                for (int j = 0; j < negyzet.GetLength(1); j++)
                {
                    negyzet[i, j] = 0;
                }
            }
        }
        //kiirajzolja a négyzetet a random számok segítségével
        public static void NegyzetKirajzolasa(int[,] negyzet, int szinSzama)
        {
            Console.Clear();
            Console.ForegroundColor = (ConsoleColor)szinSzama;
            Console.WriteLine("Romboló");
            Console.ResetColor();
            Console.Write("+");
            for (int i = 0; i < negyzet.GetLength(1); i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
            for (int i = 0; i < negyzet.GetLength(0); i++)
            {
                for (int j = 0; j < negyzet.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Console.Write("|");
                    }
                    Console.BackgroundColor = (ConsoleColor)negyzet[i, j];
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("|");
            }
            Console.Write("+");
            for (int i = 0; i < negyzet.GetLength(1); i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("+");
        }
        //mozgatja a cursort jobbra vagy balra
        public static void SetCursorPosition(ref int positionWidth, int minusOrPlus, int positionHeight, int szinSzama)
        {
            positionWidth += minusOrPlus;
            Console.SetCursorPosition(positionWidth, positionHeight);
        }
        public static void NegyzetRombolasa(ref int[,] negyzet, int oszlopszam, int szinSzama)
        {
            int index = negyzet.GetLength(0) - 1;
            while (index >= 0 && negyzet[index, oszlopszam] == 0)
            {
                index--;
            }
            RombolasKorulotte(ref negyzet, oszlopszam, index + 1, szinSzama);
        }
        public static void RombolasKorulotte(ref int[,] negyzet, int oszlopszam, int sorszam, int szinSzama)
        {
            if (sorszam-1 >= 0)
            {
                if (negyzet[sorszam - 1, oszlopszam] == szinSzama)//megnézzük, hogy felette van e ugyan olyan szín
                {
                    negyzet[sorszam - 1, oszlopszam] = 0;
                    if (sorszam != 0)
                    {
                        RombolasKorulotte(ref negyzet, oszlopszam, sorszam - 1, szinSzama);
                    }
                    NegyzetKirajzolasa(negyzet, szinSzama);
                }
            }

            if (oszlopszam-1 >= 0)
            {
                if (negyzet[sorszam, oszlopszam - 1] == szinSzama)//megnézzük, hogy balra van e ugyan olyan szín
                {
                    negyzet[sorszam, oszlopszam - 1] = 0;
                    if (oszlopszam != 0)
                    {
                        RombolasKorulotte(ref negyzet, oszlopszam - 1, sorszam, szinSzama);
                    }
                    NegyzetKirajzolasa(negyzet, szinSzama);
                }
            }

            if (oszlopszam+1 <= negyzet.GetLength(1)-1)
            {
                if (negyzet[sorszam, oszlopszam + 1] == szinSzama)//megnézzük, hogy jobbra van e ugyan olyan szín
                {
                    negyzet[sorszam, oszlopszam + 1] = 0;
                    if (oszlopszam != negyzet.GetLength(1) - 1)
                    {
                        RombolasKorulotte(ref negyzet, oszlopszam + 1, sorszam, szinSzama);
                    }
                    NegyzetKirajzolasa(negyzet, szinSzama);
                }
            }

            if (sorszam+1 <= negyzet.GetLength(0)-1)
            {
                if (negyzet[sorszam + 1, oszlopszam] == szinSzama)//megnézzük, hogy alatta van e ugyan olyan szín
                {
                    negyzet[sorszam + 1, oszlopszam] = 0;
                    if (sorszam != negyzet.GetLength(0) - 1)
                    {
                        RombolasKorulotte(ref negyzet, oszlopszam, sorszam + 1, szinSzama);
                    }
                    NegyzetKirajzolasa(negyzet, szinSzama);
                }
            }
            
        }
        static void Main(string[] args)
        {
            //bekérjük, hogy mekkora legyen a a játéknak a tere
            Console.WriteLine("Add meg, hogy milyen nagy legyen a jaták 6-20 közötti páros szám");
            string nagysagString = Console.ReadLine();
            int nagysag = 0;
            //addig csináljuk ameddig nem számot, vagy nem 5 és 20 közötti számot adott meg
            while (!int.TryParse(nagysagString, out nagysag) || (nagysag < 6 || nagysag >20) || (nagysag % 2 == 1))
            {
                Console.WriteLine("nem számot adtál meg, vagy nem a megfelelő intervallumon belül. Add meg újra");
                nagysagString = Console.ReadLine();
            }
            Console.Clear();
            Console.WriteLine("Romboló");//még változtatni kell
            //beállítjuk a beírt nagység függvényében a játékteret, kirajzoljuk a négyzetet és 
            int[,] negyzet = new int[nagysag,nagysag];
            int width = negyzet.GetLength(0)+2;
            int height = negyzet.GetLength(0) + 4;
            Console.SetWindowSize(width, height);
            NegyzetFeltoltese(ref negyzet);

            Random random = new Random();
            int szinSzama = random.Next(1, 5);
            NegyzetKirajzolasa(negyzet, szinSzama);

            int positionWidth = negyzet.GetLength(1)/2+1;
            int positionHeight = negyzet.GetLength(1)+3;
            
            
            while (true)
            {
                NegyzetKirajzolasa(negyzet, szinSzama);
                Console.SetCursorPosition(positionWidth, positionHeight);
                ConsoleKey theKey = Console.ReadKey().Key;
                while (theKey != ConsoleKey.LeftArrow && theKey != ConsoleKey.RightArrow && theKey != ConsoleKey.Spacebar)//Amíg nem a balra, jobbra vagy a space nyilat nyomja le
                {
                    SetCursorPosition(ref positionWidth, 0, positionHeight, szinSzama);
                    theKey = Console.ReadKey().Key;
                }
                if (theKey == ConsoleKey.LeftArrow)//Ha balra mozog
                {
                    if (positionWidth > 1)//ha még tud balra mozogni
                    {
                        SetCursorPosition(ref positionWidth, -1, positionHeight, szinSzama);
                    }
                    else//ha már nem tud balra mozogni
                    {
                        SetCursorPosition(ref positionWidth, 0, positionHeight, szinSzama);
                    }
                    
                }
                else if(theKey == ConsoleKey.RightArrow)//ha jobbra mozog
                {
                    if (positionWidth < negyzet.GetLength(1))//ha még tud jobbra mozogni
                    {
                        SetCursorPosition(ref positionWidth, +1, positionHeight, szinSzama);
                    }
                    else//ha már nem tud jobbra mozogni
                    {
                        SetCursorPosition(ref positionWidth, 0, positionHeight, szinSzama);
                    }
                }
                else if (theKey == ConsoleKey.Spacebar)
                {
                    NegyzetRombolasa(ref negyzet, positionWidth - 1, szinSzama);
                    positionWidth = negyzet.GetLength(1) / 2 + 1;
                    positionHeight = negyzet.GetLength(1) + 3;
                    SetCursorPosition(ref positionWidth, 0, positionHeight, szinSzama);
                    szinSzama = random.Next(1, 5);
                }
            }
            Console.ReadKey();
        }
    }
}
