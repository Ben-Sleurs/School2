using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iteratie_Oefeningen_2
{
    class Program
    {
        static void Main(string[] args)
        {
            ////OEFENING 1
            //Console.WriteLine("Getal?");
            //int getal = int.Parse(Console.ReadLine());
            //int output = getal * getal;
            //for (int i = 0; i < getal; i++)
            //{
            //    for (int j = 0; j <getal; j++)
            //    {
            //        if (output>9)
            //        {
            //            Console.Write($"{output} ");
            //        }
            //        else
            //        {
            //            Console.Write($"{output}  ");
            //        }
            //        output=output - 1;
            //    }
            //    Console.WriteLine();


            ////OEFENING 2
            //Console.WriteLine("Getal?");
            //int getal = int.Parse(Console.ReadLine());

            //for (int i = 2; i <= getal; i++)                
            //{
            //    if (getal%i==0)
            //    {
            //        Console.WriteLine(i);
            //        getal = getal / i;
            //        i--;
            //    }
            //}


            ////OEFENING 3
            //Console.WriteLine("Getal?");
            //int getal = int.Parse(Console.ReadLine());

            //for (int i = 0; i < getal; i++)
            //{
            //    for (int j = 0; j < getal; j++)
            //    {
            //        if (i==0||i==(getal-1))
            //        {
            //            Console.Write("* ");
            //        }
            //        else if (j==0||j==getal-1)
            //        {
            //            Console.Write("* ");
            //        }
            //        else
            //        {
            //            Console.Write("  ");
            //        }
            //    }
            //    Console.WriteLine();


            ////OEFENING 4
            //Console.WriteLine("Getal?");
            //int getal = int.Parse(Console.ReadLine());

            //for (int i = 0; i < getal; i++)
            //{
            //    for (int j = 0; j < getal; j++)
            //    {
            //        if (i == 0 || i == (getal - 1))
            //        {
            //            Console.Write("* ");
            //        }
            //        else if (j==i||j==getal-i-1||j==0||j==getal-1)
            //        {
            //            Console.Write("* ");
            //        }
            //        else
            //        {
            //            Console.Write("  ");
            //        }
            //    }
            //    Console.WriteLine();
            //}


            //OEFENING 5
            Console.WriteLine("input zijde lengte?");
            int zijde = int.Parse(Console.ReadLine());
            Console.WriteLine("input aantal vierkanten per rij");
            int vierkanten = int.Parse(Console.ReadLine());

            int totalelengte = zijde * (vierkanten - 1) + 1;
            for (int i = 0; i < totalelengte; i++)
            {
                for (int j = 0; j < totalelengte; j++)
                {
                    
                    if (i == 0 || i == (totalelengte - 1))
                    {
                        Console.Write("* ");
                    }
                    else if (j==0||j==totalelengte-1)
                    {
                        Console.Write("* ");
                    }
                    else if (i%(zijde-1)==0)
                    {
                        Console.Write("* ");
                    }
                    else if (j % (zijde - 1) == 0)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                    
                }
                Console.WriteLine();

            }
          

    }
    }
    
}
