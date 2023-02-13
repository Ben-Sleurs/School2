using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iteratie_Oefeningen_3
{
    class Program
    {
        static void Main(string[] args)
        {
            ////OEFENING 1

            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine("HALLO");
            //}

            ////OEFENING 2

            //Console.WriteLine("geef string");
            //string tekst = Console.ReadLine();
            //Console.WriteLine();
            //for(int i = 0; i < tekst.Length; i++)
            //{
            //    Console.WriteLine(tekst.Substring(0,tekst.Length-i));
            //}

            ////OEFENING 3

            //Console.WriteLine("Getal?");
            //double getal = double.Parse(Console.ReadLine());
            //do
            //{
            //    getal = getal / 2;
            //    Console.WriteLine(getal);
            //} while (getal >= 1);

            ////OEFENING 4

            //Console.WriteLine("input hoogte driehoek");
            //int getal = int.Parse(Console.ReadLine());
            //int uitkomst = 1;
            //int lengte;
            //int totalelengte = (getal*getal/2).ToString().Length;
            //for (int i = 1; i <= getal; i++)
            //{
            //    for (int j = 0; j < i; j++)
            //    {
            //        lengte = uitkomst.ToString().Length;
            //        Console.Write($"{uitkomst}");
            //        for (int k = 0; k <= totalelengte-lengte; k++)
            //        {
            //            Console.Write(" ");
            //        }
            //        uitkomst++;
            //    }
            //    Console.WriteLine();
            //}

            ////OEFENING 5
            //bool isgetalgroter=true;
            //int getal1;
            //int getal2;
            //do
            //{
            //    Console.WriteLine("Input getal 1");
            //    getal1 = int.Parse(Console.ReadLine());
            //    Console.WriteLine("Input getal 2 (groter dan getal 1)");
            //    getal2 = int.Parse(Console.ReadLine());
            //    if (getal2<getal1)
            //    {
            //        isgetalgroter = false;
            //        Console.WriteLine("Getal 2 is niet groter dan getal 1");
            //    }
            //    else
            //    {
            //        isgetalgroter = true;
            //    }
            //} while (isgetalgroter==false);

            //for (int i = getal1; i < getal2; i++)
            //{
            //    Console.Write($"{i} ");
            //}

            ////OEFENING 6

            //Console.WriteLine("Getal?");
            //int getal = int.Parse(Console.ReadLine());
            //int uitkomst = 1;
            //int voriggetal = 0;
            //int tempgetal;
            //for (int i = 0; i < getal; i++)
            //{
            //    Console.WriteLine(uitkomst);
            //    tempgetal = voriggetal;
            //    voriggetal = uitkomst;
            //    uitkomst = uitkomst + tempgetal;
            //}

            ////OEFENING 7

            //Console.WriteLine("Getal");
            //int getal = int.Parse(Console.ReadLine());
            //for (int i = 0; i < getal; i++)
            //{
            //    for (int j = 0; j < getal; j++)
            //    {
            //        if ((i+j)%2==0)
            //        {
            //            Console.Write("o ");
            //        }
            //        else
            //        {
            //            Console.Write("x ");
            //        }
            //    }
            //    Console.WriteLine();
            //}

            ////OEFENING 8



            int no_row, c = 1, blk, i, j;

            Console.Write("\n\n");
            Console.Write("Display the Pascal's triangle:\n");
            Console.Write("--------------------------------");
            Console.Write("\n\n");

            Console.Write("Input number of rows: ");
            no_row = Convert.ToInt32(Console.ReadLine());
            for (i = 0; i < no_row; i++)
            {
                for (blk = 1; blk <= no_row - i; blk++)
                    Console.Write("  ");
                for (j = 0; j <= i; j++)
                {
                    if (j == 0 || i == 0)
                        c = 1;
                    else
                        c = c * (i - j + 1) / j;
                    Console.Write($"{c}   ");
                }
                Console.WriteLine();



            }
        }
        

    }
}
