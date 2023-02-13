using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeelGebruikteKlassesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Math: wiskundige berekeningen

            Console.WriteLine($"Het getal pi is gelijk aan {Math.PI}");
            // navigeer in de metadata van Math door Ctrl+Klik
            Console.WriteLine(Math.Abs(-9));

            double getal = 4.50000000;
            Math.Floor(getal); //Afronding naar lager integer getal
            Math.Ceiling(getal); //Afronding naar hoger integer getal
            Math.Round(getal); //Afronding naar dichts integer getal (het even getal als in het midden).
            Math.Max(getal, 1);
            Math.Min(getal, 1);
            Console.WriteLine(Math.Round(getal));

            string inputVanGebruiker = "1";
            if ("0"==inputVanGebruiker)
            {

            }
            if ("0".Equals(inputVanGebruiker))
            {

            }

            // 012
            // pxl
            Console.WriteLine("pxl".IndexOf('x'));
            string nieuweTekst = "pxl".Replace('x', 'X'); //de ' ' is voor een karakter, " " voor string
            Console.WriteLine("pxl".Replace('x','X').Replace('X','Y')); // = pYl
            if (string.IsNullOrEmpty(inputVanGebruiker))
            {

            }
            Console.WriteLine(inputVanGebruiker);
            Console.WriteLine(inputVanGebruiker.PadLeft(1, 'X'));

            string input = "Hoe werkt dit?";
            if (input.EndsWith("?"))
            {
                Console.WriteLine("Dit is een vraag");
            }

            Console.WriteLine(input.Substring(4,5));

            
            DateTime vandaag = new DateTime(2021, 10, 28);

            DateTime kerst = new DateTime(2021, 12, 24);

            vandaag = DateTime.Today;

            TimeSpan duur = new TimeSpan();
            TimeSpan tweeUur = new TimeSpan(2, 0, 0);

            Console.WriteLine(vandaag.Hour); //=>0

            vandaag = DateTime.Now;
            Console.WriteLine(vandaag.Hour); //=>huidig uur

            DateTime overmorgen = vandaag.AddDays(2);
            Console.WriteLine(overmorgen.Day);

        }
    }
}
