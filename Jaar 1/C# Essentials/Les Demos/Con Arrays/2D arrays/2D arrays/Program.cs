using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[,] array = new int[10, 20];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(0, 10000);
                    Console.Write(array[i, j]);
                    for (int k = 0; k < 5-Math.Floor(Math.Log10(array[i,j]) + 1); k++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            int grootstegetal = int.MinValue;
            int kleinstegetal = int.MaxValue;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i,j]>grootstegetal)
                    {
                        grootstegetal = array[i, j];
                    }
                    if (array[i,j]<kleinstegetal)
                    {
                        kleinstegetal = array[i, j];
                    }
                }
            }
            Console.WriteLine($"Het grootste getal is {grootstegetal}");
            Console.WriteLine($"Het kleinste getal is {kleinstegetal}");
        }
    }
}
