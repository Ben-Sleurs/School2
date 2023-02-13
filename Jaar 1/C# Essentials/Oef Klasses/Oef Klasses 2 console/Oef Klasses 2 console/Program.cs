using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oef_Klasses_2_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ////OEF 1
            //Console.WriteLine("Datum?");
            //DateTime date = new DateTime();
            //date = Convert.ToDateTime(Console.ReadLine());
            //Console.WriteLine(AantalDagen(date).ToString());

            ////OEF 2
            //Console.WriteLine("Welk product?");
            //string naam = Console.ReadLine();
            //Console.WriteLine("Hoeveel");
            //int product = int.Parse(Console.ReadLine());
            //Console.WriteLine("Prijs?");
            //double prijs = double.Parse(Console.ReadLine());
            //BuildString(product,naam,prijs);

            ////OEF 3
            //Console.WriteLine(KassaTicket());

            //OEF 4
            Console.WriteLine(Encoded(1));

        }
        //OEF 1
        private static int AantalDagen(DateTime date)
        {
            return 365 - date.DayOfYear;
        }

        //OEF 2
        private static StringBuilder BuildString(int product, string naam, double prijs)
        {


            StringBuilder totaal = new StringBuilder();
            totaal.AppendFormat("{0,11}",product.ToString());
            totaal.AppendFormat("{0,20}",naam);
            totaal.AppendFormat("{0,20}",$"$ {prijs.ToString()}");
            totaal.AppendFormat("{0,20}",$"$ {(prijs * product).ToString()}");
            return totaal;
        }
       

        private static StringBuilder KassaTicket()
        {
            StringBuilder kassaTicket = new StringBuilder();
            kassaTicket.AppendFormat("{0,11}", "Hoeveelheid");
            kassaTicket.AppendFormat("{0,20}", "Naam");
            kassaTicket.AppendFormat("{0,20}", "Prijs");
            kassaTicket.AppendFormat("{0,20}", "Totaal");
            kassaTicket.AppendLine();

            string check = "y";
            double totaalprijs=0;
            while (check=="y")
            {
                Console.WriteLine("Welk product?");
                string naam = Console.ReadLine();
                Console.WriteLine("Hoeveel");
                int product = int.Parse(Console.ReadLine());
                Console.WriteLine("Prijs?");
                double prijs = double.Parse(Console.ReadLine());
                totaalprijs = totaalprijs + (prijs * product);

                kassaTicket.AppendLine(BuildString(product,naam,prijs).ToString());

                Console.WriteLine("Nog een item toevoegen?(y/n)");
                check = Console.ReadLine();
            }
            kassaTicket.Append("Totaal");
            kassaTicket.AppendFormat("{0,65}", $"$ {totaalprijs}");
            return kassaTicket;
        }

        private static string Encoded(double getal)
        {
            while (getal<=1000000000)
            {
                getal = getal * 7919;
            }
            
            string encode = getal.ToString();
            encode = encode.Substring(0, 10);
            encode = encode.Replace("1", "!");
            encode = encode.Replace("2", "Z");
            encode = encode.Replace("3", "E");
            encode = encode.Replace("4", "A");
            encode = encode.Replace("5", "s");
            encode = encode.Replace("6", "C");
            encode = encode.Replace("7", "T");
            encode = encode.Replace("8", "B");
            encode = encode.Replace("9", "g");

            
            return encode;
        }



    }
}
