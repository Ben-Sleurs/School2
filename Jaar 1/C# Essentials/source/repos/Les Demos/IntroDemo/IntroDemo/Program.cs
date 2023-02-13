using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region begin
            /*
            
            Console.WriteLine("Hello World");
            Console.WriteLine("Hello World 2");

            int GeheelGetal;
            GeheelGetal = 1;
            Console.WriteLine(GeheelGetal);

            float Breukgetal;
            Breukgetal = 1.2f;
            Console.WriteLine(Breukgetal);

            String stringg = "Hello world";
            Console.WriteLine(stringg);

            Double GroterBreukgetal;
            GroterBreukgetal = 1.2;
            Console.WriteLine(GroterBreukgetal);

            Console.Write("Dit ");
            Console.Write("is ");
            Console.Write("een ");
            Console.WriteLine("zin ");

            bool Truuueee = true;
            Console.WriteLine(Truuueee);
            */
            #endregion begin
            /*
            Console.WriteLine("Hello World");
            int a = 10;
            int b = 20;
            int c = a - b;
            Console.WriteLine(c);
            int d = 15;
            int opKlok = d % 12;

            bool IsGenoegGeld = false;
            bool isjonggenoeg = true;
            
            
            string Voornaam = "Ben";
            Console.WriteLine("Wat is je achernaam?");
            string achternaam = Console.ReadLine();
            String volledigeNaam = Voornaam + " " + achternaam;

            Console.WriteLine(volledigeNaam);
            

            int eengetal = 10;
            int andergetal = 11;
            bool gelijkvraagteken = (eengetal == andergetal);
            Console.WriteLine(gelijkvraagteken);
            */

            int a = 3;
            double b = 2.3;
            float c = 1.5f;
            string text = "dit is tekst";
            string resultaat = Convert.ToString(a) + Convert.ToString(b) + Convert.ToString(c) + " " + text;
            Console.WriteLine(resultaat);
            string metHaakjes = a + (b + (c +" "+ text));
            Console.WriteLine(metHaakjes);

           


        }
    }
}
