using System;

namespace CON_Arrays_1
{
    class Program
    {
        static void Main(string[] args)
        {
            ////OEF 1
            //string[] studenten = new string[5];
            //for (int i = 0; i < studenten.Length; i++)
            //{
            //    Console.WriteLine($"Naam student {i}?");
            //    studenten[i] = Console.ReadLine();
            //}

            //while (true)
            //{
            //    Console.WriteLine("welke student wil je de naam van weten(0-4)");
            //    int index = int.Parse(Console.ReadLine());
            //    Console.WriteLine(studenten[index]);
            //}

            //OEF 3
            int[] array = new int[10];
            array = GenerateRandomArray();
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
            //OEF 4
            Console.WriteLine(SumArray(array));

            //OEF 5
            MinandMaxValueArray(array);
            //OEF 6
            Console.WriteLine($"Aantal duplicates is {DuplicatesInArray(array)}");
            //OEF 7
            int[] omgekeerdeArray = new int[array.Length];
            omgekeerdeArray = Omgekeerd(array);
            Console.WriteLine("Omgekeerde array");
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(omgekeerdeArray[i]);
            }
            //OEF 8
            int[] opgeteldeArray = new int[array.Length];
            opgeteldeArray = OptellingArrays(array, omgekeerdeArray);
            Console.WriteLine("Opgetelde array");
            for (int i = 0; i < opgeteldeArray.Length; i++)
            {
                Console.WriteLine(opgeteldeArray[i]);
            }
            //OEF 9
            
            int[] insertArray = new int[array.Length + 1];
            insertArray = Insert(array, 2, 7);
            Console.WriteLine("Inserted array");
            for (int i = 0; i < insertArray.Length; i++)
            {
                Console.WriteLine(insertArray[i]);
            }
            //OEF 10
            Console.WriteLine("pasted array: array + insertArray");
            int[] pastedArray = new int[array.Length + insertArray.Length];
            pastedArray = PasteArrays(array, insertArray);
            for (int i = 0; i < pastedArray.Length; i++)
            {
                Console.Write(pastedArray[i]+" ");
            }
            Console.WriteLine();
            //OEF 11
            Console.WriteLine("tweedergrootste element van array:");
            int tweedergrootste = TweederGrootsteElement(array);
            Console.WriteLine(tweedergrootste+ " ");

            //OEF 12
            Console.WriteLine("pasted array ascending");
            int[] orderdArray = new int[pastedArray.Length];
            orderdArray = OrderAscending(pastedArray);
            for (int i = 0; i < orderdArray.Length; i++)
            {
                Console.Write(orderdArray[i] +" ");
            }
            Console.WriteLine();
            //OEF 13
            Console.WriteLine("Caesar rotatie met rotatie 3 van ordered array");
            int[] caesarArray = new int[orderdArray.Length];
            caesarArray = CaesarRotatie(3, orderdArray);
            for (int i = 0; i < caesarArray.Length; i++)
            {
                Console.Write(caesarArray[i]+" ");
            }
            Console.WriteLine();

        }

        ////OEF 2
        //private static bool IsLangerDanTien(dynamic[] array)
        //{
        //    if (array.Length > 10)
        //    {
        //        return true;
        //    }
        //    else return false;
        //}

        //OEF 3
        private static int[] GenerateRandomArray()
        {
            Random random = new Random();
            int[] array = new int[10];
            for (int i = 0; i < 10; i++)
            {
                array[i] = random.Next(1, 11);
            }
            return array;
        }

        //OEF 4
        private static double SumArray(int[] array)
        {
            double totaal = 0;
            for (int i = 0; i < array.Length; i++)
            {
                totaal += array[i];
            }
            return totaal;
        }
        //OEF 5
        private static void MinandMaxValueArray(int[] array)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
                if (array[i] < min)
                {
                    min = array[i];
                }
            }
            Console.WriteLine($"De grootste waarde is {max}");
            Console.WriteLine($"De kleinste waarde is {min}");
        }

        //OEF 6
        private static int DuplicatesInArray(int[] array)
        {
            int count = 0;
            int[] tempArray = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                int tempcount = 0;
                if (ContainsInArray(tempArray, array[i]) == false)
                {

                    for (int j = 0; j < array.Length; j++)
                    {

                        if (array[i] == array[j])
                        {
                            tempcount++;
                        }
                    }
                    if (tempcount >= 2)
                    {
                        count++;
                    }

                }
                tempArray[i] = array[i]; //steekt de int die we net gechecked hebben in een nieuwe array zodat we de volgende keer dezelfde waarde niet gaan checken

            }
            return count;


        }
        private static bool ContainsInArray(int[] array, int a)
        {
            bool temp = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (a == array[i])
                {
                    temp = true;
                }
            }
            return temp;
        }
        //OEF 7
        private static int[] Omgekeerd(int[] array)
        {
            int[] omgekeerdeArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                omgekeerdeArray[array.Length -1 - i] = array[i];
            }
            return omgekeerdeArray;
        }
        //OEF 8
        private static int[] OptellingArrays(int[] a, int [] b)
        {
            int[] opgeteldeArray = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                opgeteldeArray[i] = a[i] + b[i];
            }
            return opgeteldeArray;
        }
        //OEF 9
        private static int[] Insert(int[] a, int index, int element)
        {
            int[] completeArray = new int[a.Length + 1];
            for (int i = 0; i < index; i++)
            {
                completeArray[i] = a[i];
            }
            completeArray[index] = element;
            for (int i = index; i < completeArray.Length-1; i++)
            {
                completeArray[i + 1] = a[i];
            }
            return completeArray;
        }

        //OEF 10
        private static int[] PasteArrays(int[] a, int[] b)
        {
            int[] pastedArray = new int[a.Length + b.Length];
            for (int i = 0; i < a.Length; i++)
            {
                pastedArray[i] = a[i];
            }
            for (int i = a.Length; i < pastedArray.Length; i++)
            {
                pastedArray[i] = b[i-a.Length];
            }
            return pastedArray;

        }
        //OEF 11
        private static int TweederGrootsteElement(int[] a)
        {
            int grootste = int.MinValue;
            int tweedergrootste = int.MinValue;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i]>grootste)
                {
                    tweedergrootste = grootste;
                    grootste = a[i];
                }
                else if (a[i]>tweedergrootste)
                {
                    tweedergrootste = a[i];
                }
               
            }
            return tweedergrootste;
        }
        //OEF 12
        private static int[] OrderAscending(int[] a)
        {
            int[] orderedArray = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                orderedArray[i] = int.MaxValue;
            }
            for (int i = 0; i < a.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j]<orderedArray[i])
                    {
                        orderedArray[i] = a[j];
                        count = j;
                    }

                }
                a[count] = int.MaxValue;
            }
            return orderedArray;
        }
        //OEF 13
        private static int[] CaesarRotatie(int rotatie, int[] a)
        {
            int[] caesarArray = new int[a.Length];
            rotatie = rotatie % a.Length;
            for (int i = 0; i < rotatie; i++)
            {
                caesarArray[i] = a[a.Length - rotatie + i];
            }
            for (int i = rotatie; i < a.Length; i++)
            {
                caesarArray[i] = a[i - rotatie];
            }
            return caesarArray;
        }
    }
}
