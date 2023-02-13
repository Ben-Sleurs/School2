using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CON_Arrays2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] testArray = new int[6]{ 1,2,3,4,5,6};
            int[] testArray2 = new int[7] { -1, 2, -3, 4, -5, 6, -7 };

            //OEF1
            Console.WriteLine("Som van even getallen testArray");
            Console.WriteLine(Methods.EvenSom(testArray));

            //OEF2
            Console.WriteLine("Gemiddelde van testArray");
            Console.WriteLine(Methods.GemArray(testArray));

            //OEF 3
            Console.WriteLine("Eerste positie van 4");
            Console.WriteLine(Methods.ElementFirstPosition(testArray,4));

            //OEF 4
            Console.WriteLine("Is testArray volledig positief?");
            Console.WriteLine(Methods.IsPositief(testArray));

            //OEF 5
            Console.WriteLine("zijn Arrays even lang?");
            Console.WriteLine(Methods.HaveEqualLength(testArray,testArray2));

            //OEF 6
            Console.WriteLine("Remove 4 uit testArray");
            int[] removedArray =Methods.RemoveElement(testArray,4);
            for (int i = 0; i < removedArray.Length; i++)
            {
                Console.Write(removedArray[i] + " ");
            }

            //OEF 7
            Console.WriteLine("Kleinste element uit testArray");
            Console.WriteLine(Methods.SmallestElement(testArray));

            //OEF 8
            Console.WriteLine("orders testArray2 van klein naar groot");
            int[] ascendingArray = Methods.OrderAsc(testArray2);
            Console.WriteLine(String.Join(" ", ascendingArray));
        }
    }
}
