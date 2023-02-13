using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iteratie_Oefeningen
{
    class Program
    {
        static void Main(string[] args)
        {
            ////OEFENING 1

            //for (int i = 01; i < 11; i++)
            //{
            //    Console.Write($"{i} ");
            //}

            ////OEFENING 2
            //for (int i = 2; i < 21; i=i+2)
            //{
            //    Console.Write($"{i} ");
            //}

            ////OEFENING 3
            //Console.WriteLine("Input nummer");
            //string input = Console.ReadLine();
            //int getal = int.Parse(input);
            //for (int i = 1; i < getal; i=i+2)
            //{
            //    Console.Write($"{i} ");
            //}

            ////OEFNING 4
            //Console.WriteLine("Input nummer");
            //string input = Console.ReadLine();
            //int getal = int.Parse(input);
            //int uitkomst=1;
            //for (int i = 2; i <= getal; i++)
            //{
            //    uitkomst = uitkomst * i;
            //}
            //Console.WriteLine(uitkomst);

            ////OEFENING 5
            //Console.WriteLine("Input nummer");
            //string input = Console.ReadLine();
            //int getal = int.Parse(input);
            //for (int i = 1; i <= getal; i++)
            //{
            //    Console.WriteLine($"{i}²={i*i}");
            //}

            //OEFENING 6
            //string nogeens;
            //do
            //{
            //    Console.WriteLine("Input nummer 1");
            //    string input1 = Console.ReadLine();
            //    Console.WriteLine("Input nummer 2");
            //    string input2 = Console.ReadLine();
            //    double getal1 = double.Parse(input1);
            //    double getal2 = double.Parse(input2);
            //    Console.WriteLine(getal1*getal2);
            //    Console.WriteLine("Nog eens? (y/n)");
            //    nogeens = Console.ReadLine();
            //} while (nogeens=="y");


            ////OEFENING 7
            //double grootstegetal;
            //double kleinstegetal;
            //string input;
            //double getal;


            //Console.WriteLine("Getal?");
            //input = Console.ReadLine();
            //getal = double.Parse(input);
            //grootstegetal = getal;
            //kleinstegetal = getal;
            //while (getal!=0)
            //{
            //    Console.WriteLine("Getal?");
            //    input = Console.ReadLine();
            //    getal = double.Parse(input);
            //    if (getal>grootstegetal)
            //    {
            //        grootstegetal = getal;
            //    }
            //    else if (getal<kleinstegetal)
            //    {
            //        kleinstegetal = getal;
            //    }
            //}
            //Console.WriteLine($"Het grootste getal is {grootstegetal}");
            //Console.WriteLine($"Het kleinste getal is {kleinstegetal}");

            ////OEFENING 8
            //Console.WriteLine("Getal");
            //string input = Console.ReadLine();
            //int getal = int.Parse(input);
            //bool IsPrime =true;
            //int i = 2;

            //while (IsPrime&&i<getal)
            //{
            //    int temp = getal % i;
            //    if (temp==0)
            //    {
            //        IsPrime = false;
            //        Console.WriteLine("Getal is geen priemgetal");
            //    }

            //    i++;

            //}
            //if (IsPrime)
            //{
            //    Console.WriteLine("Getal is een priemgetal");
            //}

            ////OEFENING 9

            //Console.WriteLine("Input grondgetal");
            //string inputgrondgetal = Console.ReadLine();
            //Console.WriteLine("input exponent");
            //string inputexponent = Console.ReadLine();

            //int grondgetal = int.Parse(inputgrondgetal);
            //int exponent = int.Parse(inputexponent);
            //double uitkomst = grondgetal;
            //if (exponent > 0)
            //{


            //    for (int i = 1; i < exponent; i++)
            //    {
            //        uitkomst = uitkomst * grondgetal;
            //    }

            //    Console.WriteLine(uitkomst);
            //}
            //else
            //{
            //    for (int i = -1; i > exponent; i--)
            //    {
            //        uitkomst = uitkomst / grondgetal;
            //    }
            //    Console.WriteLine(uitkomst);
            //}

            ////OEFENING 10

            //Console.WriteLine("aantal rijen en kolommen?");
            //string input = Console.ReadLine();
            //int rijen = int.Parse(input);

            //for (int i = 0; i < rijen; i++)
            //{
            //    for (int j = 0; j <rijen; j++)
            //    {
            //        Console.Write("x ");
            //    }
            //    Console.WriteLine();
            //}

            //OEFENING 11

            //Console.WriteLine("aantal rijen?");
            //string input = Console.ReadLine();
            //int rijen = int.Parse(input);

            //for (int i = 0; i < rijen; i++)
            //{
            //    for (int j = 0; j <= i; j++)
            //    {
            //        Console.Write("x ");
            //    }
            //    Console.WriteLine();
            //}

            ////OEFNING 12

            //Console.WriteLine("aantal rijen?");
            //string input = Console.ReadLine();
            //int rijen = int.Parse(input);
            //double temp = rijen;
            //for (int i = 0; i < rijen; i++)
            //{
            //    for (int j = 0; j < (2 * rijen) + 1; j++)
            //    {

            //        if (j < temp - 1 - i || j > temp - 1 + i)
            //        {
            //            Console.Write("  ");
            //        }
            //        else
            //        {
            //            Console.Write("x ");
            //        }
            //    }
            //    Console.WriteLine();
            //}

            ////OEFENING 13
            //Console.WriteLine("Input num?");
            //int rijen = int.Parse(Console.ReadLine());
            //int k=1;
            //for (int i = 0; i < rijen; i++)
            //{
            //    for (int j = 0; j < rijen; j++)
            //    {
            //        if (k<10)
            //        {
            //            Console.Write($"{k}  ");
            //            k++;
            //        }
            //        else
            //        {
            //            Console.Write($"{k}  ");
            //            k++;
            //        }
            //    }
            //    Console.WriteLine();
            //}

            ////OEFENING 14+15+16
            //Console.WriteLine("Tot welk getal");
            //int getal = int.Parse(Console.ReadLine());
            //int uitkomst=0;
            //for (int i = 2; i < getal; i=i+2)
            //{
            //    uitkomst = uitkomst + i;
            //}
            //for (int i = 5; i < getal; i=i+10)
            //{
            //    uitkomst = uitkomst + i;
            //}
            //Console.WriteLine(uitkomst);

            //alternatief
            Console.WriteLine("Tot welk getal");
            int getal = int.Parse(Console.ReadLine());
            int uitkomst = 0;
            if (getal%2!=0)
            {
                getal = getal - 1;
            }
            uitkomst = getal / 2 * (getal / 2 + 1);
            Console.WriteLine(uitkomst);
        }
    }
}
