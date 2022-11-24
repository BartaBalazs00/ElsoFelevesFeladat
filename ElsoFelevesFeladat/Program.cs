using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElsoFelevesFeladat
{
    class Program
    {
        
        
        //mozgatja a cursort jobbra vagy balra
        public static void SetCursorPosition(ref int positionWidth, int minusOrPlus, int positionHeight)
        {
            positionWidth += minusOrPlus;
            Console.SetCursorPosition(positionWidth, positionHeight);
        }
        public static int Mozgas(int positionWidth, int positionHeight, Negyzet negyzet)
        {
            Console.SetCursorPosition(positionWidth, positionHeight);
            bool space = false;
            while (!space)
            {
                ConsoleKey theKey = Console.ReadKey().Key;
                while (theKey != ConsoleKey.LeftArrow && theKey != ConsoleKey.RightArrow && theKey != ConsoleKey.Spacebar)//Amíg nem a balra, jobbra vagy a space nyilat nyomja le
                {
                    SetCursorPosition(ref positionWidth, 0, positionHeight);
                    theKey = Console.ReadKey().Key;
                }
                if (theKey == ConsoleKey.LeftArrow)//Ha balra mozog
                {
                    if (positionWidth > 1)//ha még tud balra mozogni
                    {
                        SetCursorPosition(ref positionWidth, -1, positionHeight);
                    }
                    else//ha már nem tud balra mozogni
                    {
                        SetCursorPosition(ref positionWidth, 0, positionHeight);
                    }
                }
                else if (theKey == ConsoleKey.RightArrow)//ha jobbra mozog
                {
                    if (positionWidth < negyzet.NegyzetHossza())//ha még tud jobbra mozogni
                    {
                        SetCursorPosition(ref positionWidth, +1, positionHeight);
                    }
                    else//ha már nem tud jobbra mozogni
                    {
                        SetCursorPosition(ref positionWidth, 0, positionHeight);
                    }
                }
                else if (theKey == ConsoleKey.Spacebar)
                {
                    space = true;
                }
            }
            
            return positionWidth - 1;
        }
        
        static void Main(string[] args)
        {
            bool ujJatek = true;
            while (ujJatek)
            {
                Console.WriteLine("Add meg, hogy milyen nagy legyen a jaták 6-20 közötti páros szám");
                string nagysagString = Console.ReadLine();
                int nagysag = 0;
                //addig csináljuk ameddig nem számot, vagy nem 6 és 20 közötti számot adott meg
                while (!int.TryParse(nagysagString, out nagysag) || (nagysag < 6 || nagysag > 20) || (nagysag % 2 == 1))
                {
                    Console.WriteLine("nem számot adtál meg, vagy nem a megfelelő intervallumon belül. Add meg újra");
                    nagysagString = Console.ReadLine();
                }
                Console.Clear();
                Negyzet negyzet = new Negyzet(nagysag, nagysag);

                int positionWidth = negyzet.NegyzetHossza() / 2 + 1;
                int positionHeight = negyzet.NegyzetMagassaga() + 3;
                Random random = new Random();
                int szinSzama = random.Next(1, 5);
                int oszlopSzam;
            
                while (!negyzet.TeleVanEANegyzet() && !negyzet.UresEANegyzet())
                {
                    // az építő jön
                    positionWidth = negyzet.NegyzetHossza() / 2 + 1;
                    positionHeight = negyzet.NegyzetMagassaga() + 3;
                    szinSzama = random.Next(1, 5);
                    negyzet.NegyzetKirajzolasa(szinSzama, "Építő");
                    oszlopSzam = Mozgas(positionWidth, positionHeight, negyzet);
                    negyzet.NegyzetEpitese(oszlopSzam, szinSzama);

                    //romboló jön
                    if (!negyzet.TeleVanEANegyzet())
                    {
                        positionWidth = negyzet.NegyzetHossza() / 2 + 1;
                        positionHeight = negyzet.NegyzetMagassaga() + 3;
                        szinSzama = random.Next(1, 5);
                        negyzet.NegyzetKirajzolasa(szinSzama, "Romboló");
                        oszlopSzam = Mozgas(positionWidth, positionHeight, negyzet);
                        negyzet.NegyzetRombolasa(oszlopSzam, szinSzama);
                    }
                }
                if (negyzet.TeleVanEANegyzet())
                {
                    Console.Clear();
                    Console.SetWindowSize(40, 7);
                    Console.WriteLine("Az építő nyert");
                }
                else if (negyzet.UresEANegyzet())
                {
                    Console.Clear();
                    Console.SetWindowSize(40, 7);
                    Console.WriteLine("A romboló nyert!");
                }
                Console.WriteLine("szeretnétek mégegyet játszani? I/N");
                string szeretnenekMegJatszani = Console.ReadLine().Trim();
                while (szeretnenekMegJatszani.ToLower() != "i" && szeretnenekMegJatszani.ToLower() != "n")
                {
                    Console.WriteLine("Nem jól adtad meg a válaszodat. Add meg mégyszer (I/N)");
                    szeretnenekMegJatszani = Console.ReadLine();
                }
                if (szeretnenekMegJatszani.ToLower() == "n")
                {
                    ujJatek = false;
                }
            }
        }
    }
}
