using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oefeningen_selectie_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Oefening 1

            //Console.WriteLine("Naam?");
            //string naam = Console.ReadLine();
            //Console.WriteLine("Leeftijd?(jaar)");
            //string leeftijdtekst = Console.ReadLine();

            //int leeftijd;
            //bool Isleeftijdgetal = int.TryParse(leeftijdtekst, out leeftijd);
            //if (Isleeftijdgetal)
            //{
            //    int leeftijdverschil = 18 - leeftijd;
            //    if (leeftijd>=18)
            //    {
            //        Console.WriteLine($"{naam}, je mag gaan stemmen");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"{naam}, je mag nog niet gaan stemmen.");
            //        Console.WriteLine($"binnen {leeftijdverschil} jaar mag je gaan stemmen.");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("invalide leeftijd input");
            //}


            ////Oefening 2

            //Console.WriteLine("Nummer?");
            //string getalintekst = Console.ReadLine();
            //int getal;
            //bool isgetal = int.TryParse(getalintekst, out getal);
            //if (isgetal)
            //{
            //    if (getal<=0)
            //    {
            //        Console.WriteLine("getal is kleiner of gelijk aan 0");
            //    }
            //    else if (getal<=9000)
            //    {
            //        Console.WriteLine("getal zit tussen 0 en 9000");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Getal is groter dan 9000");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("input is geen geheel getal");
            //}


            ////Oefening 3

            //Console.WriteLine("Kleur verkeerslicht? (Groen, Oranje of Rood)");
            //string verkeerslichtkleur = Console.ReadLine().ToLower();

            //switch (verkeerslichtkleur)
            //{
            //    case "groen":
            //        Console.WriteLine("Het licht is groen, de auto rijdt door.");
            //        break;
            //    case "oranje":
            //        Console.WriteLine("Het licht is ornaje, de auto remt af.");
            //        break;
            //    case "rood":
            //        Console.WriteLine("Het licht is rood, de auto stopt.");
            //        break;
            //    default:
            //        Console.WriteLine("invalide input");
            //        break;
            //}


            ////Oefening 4

            //Console.WriteLine("Zijde 1?");
            //string zijde1tekst = Console.ReadLine();
            //Console.WriteLine("Zijde 2?");
            //string zijde2tekst = Console.ReadLine();
            //Console.WriteLine("Zijde 3?");
            //string zijde3tekst = Console.ReadLine();

            //bool iszijde1double = double.TryParse(zijde1tekst, out double zijde1);
            //bool iszijde2double = double.TryParse(zijde2tekst, out double zijde2);
            //bool iszijde3double = double.TryParse(zijde3tekst, out double zijde3);



            //if (iszijde1double&&iszijde2double&&iszijde3double)
            //{
            //    if (Math.Max(Math.Max(zijde1, zijde2), zijde3)<=((zijde1+zijde2+zijde3)/2))
            //    {
            //        Console.WriteLine("De gegeven driehoek is een geldige driehoek");
            //    }
            //    else
            //    {
            //        Console.WriteLine("de gegeven driekhoek is geen geldige driehoek");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("invalide input");
            //}



            ////Oefening 5

            //Console.WriteLine("Hoek 1 driehoek? (in graden)");
            //string hoek1tekst = Console.ReadLine();
            //Console.WriteLine("Hoek 2 driehoek (in graden)");
            //string hoek2tekst = Console.ReadLine();
            //Console.WriteLine("Hoek 3 driehoek (in graden)");
            //string hoek3tekst = Console.ReadLine();

            //double hoek1;
            //double hoek2;
            //double hoek3;
            //bool ishoek1double = double.TryParse(hoek1tekst, out hoek1);
            //bool ishoek2double = double.TryParse(hoek2tekst, out hoek2);
            //bool ishoek3double = double.TryParse(hoek3tekst, out hoek3);

            //double totalehoek = hoek1 + hoek2 + hoek3;

            //if (ishoek1double&&ishoek2double&&ishoek3double)
            //{
            //    if (totalehoek==180)
            //    {
            //        Console.WriteLine("De gegeven hoeken maken een geldige driehoek");
            //    }
            //    else
            //    {
            //        Console.WriteLine("De gegeven hoeken maken geen geldige driehoek");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("invalide input");
            //}


            ////OEFENING 6

            //Console.WriteLine("Temperatuur? (warm,koud)");
            //string temperatuur = Console.ReadLine().ToLower();
            //Console.WriteLine("Wind? (windstil,bries)");
            //string wind = Console.ReadLine().ToLower();
            //Console.WriteLine("Regen? (regen, geen regen)");
            //string regen = Console.ReadLine().ToLower();


            //if ((temperatuur=="warm"||temperatuur=="koud")&&
            //    (wind=="windstil"||wind=="bries")&&
            //    (regen == "regen" || regen == "geen regen"))
            //{
            //    if (temperatuur=="warm")
            //    {
            //        if (wind == "windstil")
            //        {
            //            Console.WriteLine("Het is goed wandelweer");                         //warm windstil en (geen) regen
            //        }
            //        else if (regen=="regen")
            //        {
            //            Console.WriteLine("Het is geen goed wandelweer");                    //warm bries en regen
            //        }
            //        else
            //        {
            //            Console.WriteLine("Het is goed wandelweer");                        //warm bries en geen regen
            //        }
            //    }
            //    else if (wind =="windstil")
            //    {
            //        if (regen=="regen") 
            //        {
            //            Console.WriteLine("het is geen goed wandelweer");                       //koud windstil regen
            //        }
            //        else
            //        {
            //            Console.WriteLine("het is goed wandelweer");                            //koud windstil geen regen
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("het is geen goed wandelweer");                           // koud bries en (geen) regen
            //    }  
            //}
            //else
            //{
            //    Console.WriteLine("invalide input");
            //}

            //OEFENING 7

            Console.WriteLine("GPS aanwezig?(ja,nee)");
            string GPSAanwezig = Console.ReadLine().ToLower();
            Console.WriteLine("CD-Lezer aanwezig?(ja,nee)");
            string CDlezeraanwezig = Console.ReadLine().ToLower();
            int i;
            int j;


            //numerieke waardes aan ja of nee geven en -2 als invalid input check
            switch (GPSAanwezig)
            {
                case "ja":
                    i = 1;
                    break;
                case "nee":
                    i = 0;
                    break;
                default: i = -2;
                    break;
            }

            //numerieke waardes aan je of nee geven en -2 als invalid input check
            switch (CDlezeraanwezig)
            {
                case "ja":
                    j = 1;
                    break;
                case "nee":
                    j = 0;
                    break;
                default:
                    j = -2;
                    break;
            }
            //berekening output met numeriek waardes (1x ja en 1x nee betekent wil auto kopen, negatieve waarden betekent een -2, dus invalide input)
            if (i + j<0)
            {
                Console.WriteLine("invalide input");
            }
            else if (i+j==1)
            {
                Console.WriteLine("Jan wil de auto kopen");
            }
            else
            {
                Console.WriteLine("Jan wil de auto niet kopen");
            }



        }
    }
}
