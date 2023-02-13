using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oef_Methodes_1
{
    class Program
    {
        static void Main(string[] args)
        {
            ////OEF 1
            //Console.WriteLine("Getal 1?");
            //string eersteGetaltekst = Console.ReadLine();
            //Console.WriteLine("Getal 2?");
            //string tweedeGetaltekst = Console.ReadLine();
            //int eersteGetal = int.Parse(eersteGetaltekst);
            //int tweedeGetal = int.Parse(tweedeGetaltekst);
            //Wissel(ref eersteGetal, ref tweedeGetal);
            //Console.WriteLine($"Waarde eerste getal is nu {eersteGetal}");
            //Console.WriteLine($"Waarde tweede getal is nu {tweedeGetal}");

            ////OEF 2
            //Console.WriteLine("Straal?");
            //double straal = double.Parse(Console.ReadLine());
            //double omtrek = CirkelOmtrek(straal);
            //Console.WriteLine(omtrek);

            ////OEF 3
            //Random coinFlip = new Random();
            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //        Console.Write(EvenofOneven(coinFlip.Next(0, 2))+" ");
            //    }
            //    Console.WriteLine();
            //}

            ////OEF 4
            //Console.WriteLine("Getal 1?");
            //int getal1 = int.Parse(Console.ReadLine());
            //Console.WriteLine("Getal 2?");
            //int getal2 = int.Parse(Console.ReadLine());
            //Console.WriteLine("Getal 3?");
            //int getal3 = int.Parse(Console.ReadLine());
            //if (IsGroterDanHonderd(getal1,getal2,getal3))
            //{
            //    Console.WriteLine("Het getal is groter of gelijk aan 100");
            //}
            //else
            //{
            //    Console.WriteLine("Het getal is niet groter dan 100");
            //}

            ////OEF 5
            //Console.WriteLine("Grondgetal?");
            //int grondgetal = int.Parse(Console.ReadLine());
            //Console.WriteLine("Exponent?");
            //int Exponent = int.Parse(Console.ReadLine());
            //Console.WriteLine(Machten(grondgetal, Exponent));

            //OEF 6
            //OEF 6 HEEFT ENKEL METHODES

            ////OEF 7

            //Console.WriteLine("Getal?");
            //int getal = int.Parse(Console.ReadLine());
            //Verdubbel(ref getal);
            //Console.WriteLine(getal);

            ////OEF 8
            //Console.WriteLine("Waarde winkelkar");
            //double winkelkar = double.Parse(Console.ReadLine());
            //Console.WriteLine("Budget");
            //double budget = double.Parse(Console.ReadLine());
            //IsVoldoendeBudget(winkelkar, budget, out string resultaat);
            //Console.WriteLine(resultaat);

            //OEF 9
            int startgetal = 1;
            int startgetal2 = 1;
            Fibonnaci(startgetal,startgetal2);

            ////OEF 10
            //int resultaat = 1;
            //Console.WriteLine("Getal");
            //int getal = int.Parse(Console.ReadLine());
            //resultaat = Faculteit(getal,resultaat);
            //Console.WriteLine(resultaat);

        }
        ////OEF 1
        //private static void Wissel(ref int getal1,ref int getal2)
        //{
        //    int temp1 = getal1;
        //    getal1 = getal2;
        //    getal2 = temp1;
        //}

        ////OEF 2
        //private static double CirkelOmtrek(double straal)
        //{
        //    return straal * 2 * Math.PI ;
        //}
        
        ////OEF 3
        //private static string EvenofOneven(int getal)
        //{
        //    if (getal%2==0)
        //    {
        //        return "#";
        //    }
        //    else
        //    {
        //        return "%";
        //    }
        //}

        ////OEF 4
        //private static bool IsGroterDanHonderd(int a, int b, int c)
        //{
        //    if (a+b+c >= 100)
        //    {
        //        return true;
        //    }
        //    else return false;
        //}

        ////OEF 5
        //private static int Machten(int e, int x){
        //    int resultaat = 1;
        //    for (int i = 1; i <= x; i++)
        //    {
        //        resultaat = resultaat* e;
        //    }
        //    return resultaat;
        //}

        ////OEF 6
        //private static float Bereken21BTW(float getal)
        //{
        //    return getal * 0.21f;
        //}
        //private static double Bereken21BTW(double getal)
        //{
        //    return getal * 0.21;
        //}
        //private static decimal Bereken21BTW(decimal getal)
        //{
        //    return getal*0.21m;
        //}

        ////OEF 7
        //private static void Verdubbel(ref int a) {
        //    a = a * 2;
        //}

        ////OEF 8
        //private static void IsVoldoendeBudget(double winkelkar, double budget, out string kanBetalen)
        //{
            
        //    if (winkelkar<=budget)
        //    {
        //        kanBetalen = "De klant heeft voldoende budget";
        //    }
        //    else
        //    {
        //        kanBetalen = $"De klant komt $ {winkelkar-budget} tekort";
        //    }
        //}

        //OEF 9
        private static int Fibonnaci(int getal, int getal2)
        {
            if (getal < 100)
            {
                Console.WriteLine(getal);
                getal = getal + getal2;
                Fibonnaci(getal2, getal);
                return getal;
            }
            else
            {
                return getal;
            }
        }

        ////OEF 10
        //private static int Faculteit(int getal, int resultaat)
        //{
            
        //    if (getal>0)
        //    {
        //        resultaat = resultaat * getal;
        //        getal--;
        //        return Faculteit(getal,resultaat);
        //    }
        //    else
        //    {
        //        return resultaat;
        //    }
        //}

        //OEF 10 alternatief
        private static int Faculteit(int getal)
        {
            int nieuwNummer;
            if (getal==0)
            {
                return 1;
            }
            else
            {
                nieuwNummer = getal * Faculteit(getal - 1);
                return nieuwNummer;
            }
        }
    }
}
