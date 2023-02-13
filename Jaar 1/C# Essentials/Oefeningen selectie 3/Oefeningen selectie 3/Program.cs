using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oefeningen_selectie_3
{
    class Program
    {
        static void Main(string[] args)
        {
            ////OEFENING 1

            //Console.WriteLine("Geen een getal");
            //string getaltekst = Console.ReadLine();
            //bool isgetalvalid = int.TryParse(getaltekst,out int getal);
            //if (isgetalvalid)
            //{
            //    if (getal%2==0)
            //    {
            //        Console.WriteLine($"{getal} is een even getal");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"{getal} is een oneven getal");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Invalide input");
            //}


            ////OEFENING 2

            //Console.WriteLine("De hoeveelste dag is het van de week");
            //string weekdaggetal = Console.ReadLine();
            //switch (weekdaggetal)
            //{
            //    case "1":
            //        Console.WriteLine("Het is vandaag Maandag");
            //            break;
            //    case "2":
            //        Console.WriteLine("Het is vandaag Dinsdag");
            //        break;
            //    case "3":
            //        Console.WriteLine("Het is vandaag Woensdag");
            //        break;
            //    case "4":
            //        Console.WriteLine("Het is vandaag Donderdag");
            //        break;
            //    case "5":
            //        Console.WriteLine("Het is vandaag Vrijdag");
            //        break;
            //    case "6":
            //        Console.WriteLine("Het is vandaag Zaterdag");
            //        break;
            //    case "7":
            //        Console.WriteLine("Het is vandaag Zondag");
            //        break;

            //    default:
            //        Console.WriteLine("Invalide input");
            //        break;
            //}


            ////OEFENING 3

            //Console.WriteLine("Score voor Chemie?");
            //string Chemie = Console.ReadLine();
            //Console.WriteLine("Score voor fysica");
            //string Fysica = Console.ReadLine();
            //Console.WriteLine("Score voor Biologie");
            //string Biologie = Console.ReadLine();

            //bool ChemieCheck = double.TryParse(Chemie, out double Chemiescore);
            //bool FysicaCheck = double.TryParse(Fysica, out double Fysicascore);
            //bool BiologieCheck = double.TryParse(Biologie, out double Biologiescore);

            //Chemiescore = Chemiescore * 5;
            //Fysicascore = Fysicascore * 5;
            //Biologiescore = Biologiescore * 5;

            //if (ChemieCheck&&FysicaCheck&&BiologieCheck)
            //{
            //    Console.WriteLine($"{ScoreCheck(Chemiescore)}Chemie");
            //    Console.WriteLine($"{ScoreCheck(Fysicascore)}Fysica");
            //    Console.WriteLine($"{ScoreCheck(Biologiescore)}Biologie");
            //}
            //else
            //{
            //    Console.WriteLine("Invalide input");
            //}


            ////OEFENING 4

            //Console.WriteLine("Geef een e-mailadres");
            //string email = Console.ReadLine();
            //email = email.ToLower();

            //if (email.Contains("@")&&email.EndsWith(".com"))
            //{
            //    Console.WriteLine("Het is een geldig e-mailadres.");
            //}
            //else
            //{
            //    Console.WriteLine("Het is geen geldig e-mailadres");
            //}


            ////OEFENING 5

            //Console.WriteLine("Geen een getal");
            //string getaltekst = Console.ReadLine();

            //bool IsGetalInt = int.TryParse(getaltekst, out int getalint);
            //bool IsGetalDouble = double.TryParse(getaltekst, out double getaldouble);

            //if (!IsGetalInt&&IsGetalDouble)
            //{
            //    Console.WriteLine("Het is een kommagetal");
            //}
            //else
            //{
            //    Console.WriteLine("Het is geen kommagetal");
            //}


            //OEFENING 6

            //Console.WriteLine("Geef een land-code");
            //string landcode = Console.ReadLine().ToUpper();
            //string land = "";
            //int check =0;

            //switch (landcode)
            //{
            //    case "A": land = "Oostenrijk";
            //            break;
            //    case "B":
            //        land = "België";
            //            break;
            //    case "F":
            //        land = "Frankrijk";
            //            break;
            //    case "D":
            //        land = "Duitsland";
            //            break;
            //    case "FL":
            //        land = "Liechtenstein";
            //            break;
            //    case "L":
            //        land = "Luxemburg";
            //            break;
            //    case "MC":
            //        land = "Monaco";
            //            break;
            //    case "NL":
            //        land = "Nederland";
            //            break;
            //    case "CH":
            //        land = "Zwitserland";
            //            break;
            //    default: check = 1;
            //        break;
            //}
            //if (check==1)
            //{
            //    Console.WriteLine("Invalide input");
            //}
            //else
            //{
            //    Console.WriteLine($"De nummerplaat is geregistreerd in {land}");
            //}


            //OEFENING 7

            Console.WriteLine("Geef een getal");
            string getal1tekst = Console.ReadLine();
            Console.WriteLine("Geef een groter getal");
            string getal2tekst = Console.ReadLine();
            Console.WriteLine("Geef een groter getal");
            string getal3tekst = Console.ReadLine();
            Console.WriteLine("Geef een groter getal");
            string getal4tekst = Console.ReadLine();

            bool Isgetal1getal = double.TryParse(getal1tekst, out double getal1);
            bool Isgetal2getal = double.TryParse(getal2tekst, out double getal2);
            bool Isgetal3getal = double.TryParse(getal3tekst, out double getal3);
            bool Isgetal4getal = double.TryParse(getal4tekst, out double getal4);

            if (Isgetal1getal&&Isgetal2getal&&Isgetal3getal&&Isgetal4getal)
            {
                if (getal2<=getal1)
                {
                    Console.WriteLine($"De getallen worden niet steeds groter. Het getal {getal2} was niet groter dan {getal1}");
                }
                else if (getal3<=getal2)
                {
                    Console.WriteLine($"De getallen worden niet steeds groter. Het getal {getal3} was niet groter dan {getal2}");
                }
                else if (getal4 <= getal3)
                {
                    Console.WriteLine($"De getallen worden niet steeds groter. Het getal {getal4} was niet groter dan {getal3}");
                }
                else
                {
                    Console.WriteLine("De getallen worden steeds groter");
                }
            }
            else
            {
                Console.WriteLine("Invalid input");
            }

        }

        private static string ScoreCheck(double a)
        {
            if (a<50)
            {
                return "Er is een onvoldoende behaald voor ";
            }
            else if (a<69)
            {
                return "Voldoende wijze voor ";
            }
            else if (a < 77)
            {
                return "Onderscheiding voor ";
            }
            else if (a<85)
            {
                return "Grote onderscheiding voor ";
            }
            else if (a<90)
            {
                return "Grootste onderscheiding voor ";
            }
            else
            {
                return "Grootste onderscheiding en de gelukwensen van de examencommissie voor ";
            }

            
        }
    }

}
