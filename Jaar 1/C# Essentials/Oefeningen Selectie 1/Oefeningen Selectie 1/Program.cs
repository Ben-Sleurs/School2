using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oefeningen_Selectie_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //OEFENING 1

            //Console.WriteLine("Input num1");
            //string num1intekst = Console.ReadLine();
            //Console.WriteLine("Input num2");
            //string num2intekst = Console.ReadLine();

            //double num1;
            //double num2;

            //if (double.TryParse(num1intekst, out num1) && double.TryParse(num2intekst, out num2))
            //{

            //	if (num1 > num2)
            //	{
            //		Console.WriteLine(num1);
            //	}
            //	else
            //	{
            //		Console.WriteLine(num2);
            //	}
            //}
            //else
            //{
            //	Console.WriteLine("geef es een getal gvd");
            //}


            //OEFENING 2

            //Console.WriteLine("Input num1");
            //int num1 = int.Parse(Console.ReadLine());
            //Console.WriteLine("Input num2");
            //int num2 = int.Parse(Console.ReadLine());
            //Console.WriteLine("Input num3");
            //int num3 = int.Parse(Console.ReadLine());

            //if (num1 > num2)
            //{
            //    if (num1 > num3)
            //    {
            //        Console.WriteLine(num1);
            //    }
            //    else
            //    {
            //        Console.WriteLine(num3);
            //    }
            //}
            //else
            //{
            //    if (num2 > num3)
            //    {
            //        Console.WriteLine(num2);
            //    }
            //    else
            //    {
            //        Console.WriteLine(num3); ;
            //    }
            //}


            //OEFENING 3

            //Console.WriteLine("Nummer?");
            //string nummerintekst = Console.ReadLine();
            //int nummer;

            //bool isnummer = int.TryParse(nummerintekst, out nummer);
            //int rest5 = nummer % 5;
            //int rest6 = nummer % 6;

            //if (isnummer)
            //{
            //	if (rest5==0)
            //	{
            //		Console.WriteLine("Getal is deelbaar door 5");
            //	}
            //	else
            //	{
            //		if (rest6==0)
            //		{
            //			Console.WriteLine("Getal is deelbaar door 6");
            //		}
            //		else
            //		{
            //			Console.WriteLine("Getal is niet deelbaar door 5 en 6");
            //		}
            //	}
            //}
            //else
            //{
            //	Console.WriteLine("Geef een integer getal in");
            //}


            //OEFENING 4

            //Console.WriteLine("Is dit een Klinker?");
            //string letter =Console.ReadLine();
            //int stringlengte = letter.Length;
            //bool Output;
            //         if (stringlengte == 1)
            //         {
            //             switch (letter)
            //             {
            //                 case "a":
            //                     Output = true;
            //                     break;
            //                 case "e":
            //                     Output = true;
            //                     break;
            //                 case "i":
            //                     Output = true;
            //                     break;
            //                 case "o":
            //                     Output = true;
            //                     break;
            //                 case "u":
            //                     Output = true;
            //                     break;

            //                 default:
            //                     Output = false;
            //                     break;
            //             }
            //             Console.WriteLine(Output);
            //         }
            //         else
            //         {
            //             Console.WriteLine("Geef een enkele letter in");
            //         }
            //         Console.ReadLine();


            //OEFENING 5

            //Console.WriteLine("Wat is je cijfer?");
            //string Cijferintekst = Console.ReadLine();
            //int Cijfer;
            //bool IsHetEenCijfer = int.TryParse(Cijferintekst, out Cijfer);
            //string output;
            //if (IsHetEenCijfer==true&&Cijfer<=20&&Cijfer>=0)
            //{
            //    if (Cijfer>0&&Cijfer<=5)
            //    {
            //        output = "erg slecht";
            //    }
            //    if (Cijfer>=6&&Cijfer<=10)
            //    {
            //        output = "gebuisd";
            //    }
            //    if (Cijfer >= 11 && Cijfer <= 13)
            //    {
            //        output = "geslaagd";
            //    }
            //    if (Cijfer >=14&&Cijfer<=16)
            //    {
            //        output = "goed";
            //    }
            //    else
            //    {
            //        output = "geweldig";
            //    }

            //}
            //else
            //{
            //    output = "Geen geldig cijfer";
            //}
            //Console.WriteLine(output);
            //Console.ReadLine();


            //OEFENING 6

            //Console.WriteLine("Maand?");
            //string maand = Console.ReadLine();
            //maand = maand.ToLower();
            //string output;
            //switch (maand)
            //{
            //    case "januari":
            //        output = "31";
            //            break;
            //    case "februari":
            //        output = "28";
            //        break;
            //    case "maart":
            //        output = "31";
            //        break;
            //    case "april":
            //        output = "30";
            //        break;
            //    case "mei":
            //        output = "31";
            //        break;
            //    case "juni":
            //        output = "30";
            //        break;
            //    case "juli":
            //        output = "31";
            //        break;
            //    case "augustus":
            //        output = "31";
            //        break;
            //    case "september":
            //        output = "30";
            //        break;
            //    case "oktober":
            //        output = "31";
            //        break;
            //    case "november":
            //        output = "30";
            //        break;
            //    case "december":
            //        output = "31";
            //        break;

            //    default:
            //        output = "Geen geldige maand";
            //        break;
            //}
            //Console.WriteLine(output);
            //Console.ReadLine();


            //OEFENING 7

            //Console.WriteLine("Jaar?");
            //string Jaarintekst = Console.ReadLine();
            //int Jaar;
            //bool IsJaarintekstEenInt = int.TryParse(Jaarintekst, out Jaar);
            //string output;


            //if (IsJaarintekstEenInt)
            //{
            //    if (Jaar % 400 == 0)
            //    {
            //        output = "Schrikkeljaar";
            //    }
            //    else
            //    {
            //        if (Jaar % 100 == 0)
            //        {
            //            output = "Geen schrikkeljaar";
            //        }
            //        else
            //        {
            //            if (Jaar % 4 == 0)
            //            {
            //                output = "Schrikkeljaar";
            //            }
            //            else
            //            {
            //                output = "Geen schrikkeljaar";
            //            }
            //        }
            //    }



            //}
            //else
            //{
            //    output = "Geen geldig jaar";
            //}
            //Console.WriteLine(output);
            //Console.ReadLine();


            //        //OEFENING 9

            //        Console.WriteLine("Getal?");
            //    string getalintekst = Console.ReadLine();



            //    if (short.TryParse(getalintekst, out short getalshort))
            //    {
            //        Console.WriteLine("Is een short");
            //    }
            //    else if (int.TryParse(getalintekst, out int getalint))
            //    {
            //        Console.WriteLine("is een int");
            //    }
            //    else if (long.TryParse(getalintekst, out long getallong))
            //    {
            //        Console.WriteLine("is een long");
            //    }
            //    else if (float.TryParse(getalintekst, out float getalfloat))
            //    {
            //        Console.WriteLine("is een float");
            //    }
            //    else if (double.TryParse(getalintekst, out double getaldouble))
            //    {
            //        Console.WriteLine("is een double");
            //    }
            //    else
            //    {
            //        Console.WriteLine("?????");
            //    }

            //    Console.ReadLine();




            String getal = null;
            int a = Convert.ToInt32(getal);
            //int b = int.Parse(getal);
            Console.WriteLine(a);
            //Console.WriteLine(b);
        }
    }
}
