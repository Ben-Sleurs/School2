using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodeDemoConsole2
{
    class Program
    {
        int getal;
        static void Main(string[] args)
        {
            int referentietest = 0;
            AddOne(ref referentietest);
            Console.WriteLine(referentietest);
            referentietest = AddOneAlt(referentietest);
            Console.WriteLine(referentietest);

            GetNames(out string voornaam, out string achternaam);
            Console.WriteLine($"Je naam is {voornaam} {achternaam}");

            int getal = 2;
            int tweedegetal = 2;
            long longgetal = 3;
            long tweedelonggetal = 4;
            double kommagetal = 1.4;
            double tweedekommagetal = 5.6;
            int resultaat = Maal(getal, tweedegetal);
            long longresultaat = Maal(longgetal, tweedelonggetal);
            double kommaresultaat = Maal(kommagetal, tweedekommagetal);
            Console.WriteLine($"{resultaat} {longresultaat} {kommaresultaat}");



        }
        private static void AddOne(ref int number)
        {
            if (number < 73)
            {
                number++;
                AddOne(ref number);
            }
        }
        private static int AddOneAlt(int number)
        {
            number++;
            return number;
        }
        private static void AddOneForFourNumbers(ref int a, ref int b, ref int c, ref int d)
        {
            a++;
            b++;
            c++;
            d++;
        }
        // opvragen naam en voornaam
        private static void GetNames(out string voornaam, out string achternaam)
        {
            Console.WriteLine("Wat is je voornaam?");
            voornaam = Console.ReadLine();
            Console.WriteLine("Wat is je achternaam?");
            achternaam = Console.ReadLine();
        }

        private static int Maal(int a, int b)
        {
            Console.WriteLine("is int");
            return a * b;
        }
        private static long Maal(long a, long b)
        {
            Console.WriteLine("is long");
            return a * b;
        }
        private static double Maal(double a, double b)
        {
            Console.WriteLine("is double");
            return a * b;
        }
    }
}
