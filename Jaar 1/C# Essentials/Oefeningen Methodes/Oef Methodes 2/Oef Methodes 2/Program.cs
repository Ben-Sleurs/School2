using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oef_Methodes_2
{
    class Program
    {
        static void Main(string[] args)
        {

            ////OEF 1
            //Console.WriteLine("Getal?");
            //double getal = double.Parse(Console.ReadLine());
            //Console.WriteLine(AbsoluteWaarde(getal));

            ////OEF 2
            //Console.WriteLine("typ een zin");
            //string input = Console.ReadLine();
            //Console.WriteLine(Roep(input));

            ////OEF 3
            //Console.WriteLine("typ een zin");
            //string input = Console.ReadLine();
            //Console.WriteLine(IsVraag(input));

            ////OEF 4
            //Console.WriteLine("Geef een datum (dd/mm/yyyy)");
            //DateTime date = Convert.ToDateTime(Console.ReadLine());
            //bool isWeekend = IsWeekendDag(date);
            //Console.WriteLine(date.DayOfWeek);
            //if (IsWeekendDag(date))
            //{
            //    Console.WriteLine("De datum valt op Zaterdag of Zondag");
            //}
            //else
            //{
            //    Console.WriteLine("De datum valt niet op Zaterdag of Zondag");
            //}

            ////OEF 5
            //Console.WriteLine("Geen een datum (dd/mm/yyy)");
            //DateTime date = Convert.ToDateTime(Console.ReadLine());
            //int dagen = DagenTotWeekend(date);
            //Console.WriteLine($"Het is nog {dagen} dagen tot het weekend");

            ////OEF 6
            //Console.WriteLine("Zin die je wil omdraaien");
            //string zin = Console.ReadLine();
            //Console.WriteLine(Reverse(zin));

            //OEF 7

            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(RandomBool(i));
            }
            

        }

        ////OEF 1
        //private static double AbsoluteWaarde(double waarde)
        //{
        //    if (waarde < 0)
        //    {
        //        return waarde * (-1);
        //    }
        //    else return waarde;
        //}

        ////OEF 2
        //private static string Roep(string zin)
        //{
        //    return zin + "!";
        //}

        ////OEF 3
        //private static bool IsVraag(string zin)
        //{
        //    if (zin.EndsWith("?"))
        //    {
        //        return true;
        //    }
        //    else return false;
        //}

        ////OEF 4
        //private static bool IsWeekendDag(DateTime date)
        //{
        //    string dag = Convert.ToString(date.DayOfWeek);
        //    if (dag=="Saturday"||dag=="Sunday")
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        ////OEF 5
        //private static bool IsWeekendDag(DateTime date)
        //{
        //    string dag = Convert.ToString(date.DayOfWeek);
        //    if (dag == "Saturday" || dag == "Sunday")
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private static int DagenTotWeekend(DateTime date)
        //{
         
        //    int count = 0;
        //    while (IsWeekendDag(date)==false)
        //        {
        //        date = date.AddDays(1);
        //        count++;
        //        }
        //        return count;
            
        //}

        //OEF 6

        //private static string Reverse(string tekst)
        //{
        //    string resultaat = "";
        //    for (int i = tekst.Length-1; i>=0; i--)
        //    {
        //        resultaat =resultaat + tekst.Substring(i, 1);
        //    }
        //    return resultaat;
        //}

        //OEF 7 random functie is waardeloos
        private static bool RandomBool(int seed)
        {
            Random coinFlip = new Random(seed);
            int coinSide = coinFlip.Next(1, 10001);
            if (coinSide%2==0)
            {
                return true;
            }
            else return false;
        }

    }
}
