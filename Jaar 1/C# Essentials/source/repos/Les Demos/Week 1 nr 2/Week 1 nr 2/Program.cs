using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1_nr_2
{
    class Program
    {
        static void Main(string[] args)
        {

            /*string a = Console.ReadLine();
            double aa = Convert.ToDouble(a);
            Console.WriteLine(aa*aa);*/

            /*
            // Implicit cast: geen gegevensverlies//
            Console.WriteLine("Input voor het getal?");
            string getalInTekst = Console.ReadLine();
            short getalShort;
            getalShort = Convert.ToInt16(getalInTekst);
            Console.WriteLine(getalShort);
            */
            

            /*
            // Explicit cast: gegevensverlies
            float kommaGetal = 4.5f;
            int doeUBest = (int)kommaGetal;
            Console.WriteLine(doeUBest);
            */

            
            Console.WriteLine("Naam?");
            String Naam = Console.ReadLine();

            Console.WriteLine("Leeftijd?");
            String tempLeeftijd = Console.ReadLine();
            int Leeftijd = Convert.ToInt32(tempLeeftijd);

            Console.WriteLine("Geslacht? (man=true, vrouw=false)");
            String tempGeslacht = Console.ReadLine();
            Boolean isMan = Convert.ToBoolean(tempGeslacht);

            Console.WriteLine("Postcode?");
            string tempPostcode = Console.ReadLine();
            short Postcode = Convert.ToInt16(tempPostcode);

            Console.WriteLine("Woonplaats?");
            string Woonplaats = Console.ReadLine();

            Console.WriteLine("Telefoonnummer?");
            string tempTelefoonnummer = Console.ReadLine();
            int Telefoonnummer = Convert.ToInt32(tempTelefoonnummer);

            Console.WriteLine("e-mail?");
            string email = Console.ReadLine();

            Console.WriteLine("Naam = "+Naam);
            Console.WriteLine("Leeftijd = "+Leeftijd);
            Console.WriteLine("Isman = "+ isMan);
            Console.WriteLine("Telefoonnummer = "+ Telefoonnummer);
            Console.WriteLine("E-mail = "+email);
            Console.WriteLine("Postcode - Woonplaats = " + Postcode + " " + Woonplaats);
            

            
            

        }
    }
}
