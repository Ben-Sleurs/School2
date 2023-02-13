using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CON_Arrays2
{
    class Methods
    {
        //OEF 1
        public static int EvenSom(int[] a)
        {
            int total = 0;
            foreach (int item in a)
            {
                if (item % 2 == 0)
                {
                    total += item;
                }
            }
            return total;
        }

        //OEF 2
        public static double GemArray(int[] a)
        {
            double total = 0;
            for (int i = 0; i < a.Length; i++)
            {
                total += a[i];
            }
            return total /= a.Length;
        }

        //OEF 3
        public static int ElementFirstPosition(int[] a, int element)
        {
            int position = -1;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == element)
                {
                    position = i;
                    i = a.Length;
                }
            }
            return position;
        }

        //OEF 4
        public static bool IsPositief(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] < 0)
                {
                    return false;
                }

            }
            return true;
        }

        //OEF 5
        public static bool HaveEqualLength(int[] a, int[] b)
        {
            if (a.Length == b.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // {1,4,3,4} getal=5 => {1,3,4}
        //OEF 6
        public static int[] RemoveElement(int[] a, int getal)
        {
            if (ElementFirstPosition(a, getal) == -1)
            {
                return a;
            }
            else
            {
                int[] newArray = new int[a.Length - 1];
                for (int i = 0; i < ElementFirstPosition(a, getal); i++)
                {
                    newArray[i] = a[i];
                }
                for (int i = ElementFirstPosition(a, getal); i < newArray.Length; i++)
                {
                    newArray[i] = a[i + 1];
                }
                return newArray;
            }
        }

        //OEF 7
        public static int SmallestElement(int[] a)
        {
            int smallest = int.MaxValue;
            foreach (int item in a)
            {
                if (item<smallest)
                {
                    smallest = item;
                }
            }
            return smallest;
        }

        //OEF 8
        public static int[] OrderAsc(int[] a)
        {
            int[] newArray = new int[a.Length];
            int[] tempArray = new int[a.Length];
            Array.Copy(a, tempArray, tempArray.Length);
            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = SmallestElement(tempArray);
                tempArray = RemoveElement(tempArray, newArray[i]);
            }
            return newArray;
        }
    }
    
}
