using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CON_Lists
{
    class Program
    {
        
        static void Main(string[] args)
        {
            List<int> testList = new List<int>() { 1, 2, 3 };
            List<int> intList = new List<int>() { 0, 9, 1, 8, 7, 6, 5, 4, 0 };
            List<int> intListTwo = new List<int>() { 9,9,9,9,9,1,1,1,0,0,0,3,3,3,4,4,5,5,5 };
            List<string> stringList = new List<string>() { "Hallo", "Ja zeker", "Woord", "Test" };



            //OEF1
            Console.WriteLine("Sommatie van teslist");
            Console.WriteLine(ListSum(intList));

            //OEF2
            Console.WriteLine("Concatted tekst +!!!");
            string toevoegsel = "!!!";
            ConcatEach(stringList, toevoegsel);
            foreach (string item in stringList)
            {
                Console.WriteLine(item);
            }
            //OEF3
            Console.WriteLine("Grootste getal uit intlist");
            Console.WriteLine(GrootsteElement(intList));

            //OEF4
            Console.WriteLine("Hoevaak komt 0 voor in intList");
            Console.WriteLine(CountElement(intList,0));

            //OEF5
            Console.WriteLine("print random element uit stringList uit");
            Console.WriteLine(RandomElement(stringList));

            //OEF6
            Console.WriteLine("print subset van intList met grootte 4 af");
            int subSetGrootte = 4;
            List<int> subSet = RandomSubSet(intList, subSetGrootte);
            Console.WriteLine(String.Join(" ", subSet));
            //OEF 7
            Console.WriteLine("checked of 2 lists overlappende elementen hebben");
            Console.WriteLine(DoListsOverlap(intList,intListTwo));
            //OEF 8
            Console.WriteLine("geeft de overlappende elementen van 2 lijsten weer");
            List<int> overlappingList = new List<int>();
            overlappingList = OverlappingElements(intList, intListTwo);
            Console.WriteLine(String.Join(" ", overlappingList));

            //OEF9
            Console.WriteLine("Geeft alle subset");
            List<List<int>> finalList = new List<List<int>>();
            finalList = AllSubsets(testList);
            for (int i = 0; i < finalList.Count; i++)
            {
                Console.WriteLine(String.Join(" ",finalList[i]));
            }



           
            
            
        }
        
        

        //OEF1
        private static int ListSum(List<int> list)
        {
            int total = 0;
            foreach (var item in list)
            {
                total += item;
            }
            return total;
        }
        //OEF2
        private static void ConcatEach(List<string> list, string toevoegsel)
        {
            for (int i = 0; i < list.Count; i++)
            {
                //list[i] = list[i] + toevoegsel;
                list[i] = String.Concat(list[i], toevoegsel);
            }
        }

        //OEF3
        private static int GrootsteElement(List<int> list)
        {
            int grootste = int.MinValue;
            foreach (int item in list)
            {
                if (item>grootste)
                {
                    grootste = item;
                }
            }
            return grootste;
        }

        //OEF4
        private static int CountElement(List<int> list, int element)
        {
            int count = 0;
            foreach (int item in list)
            {
                if (item==element)
                {
                    count++;
                }
            }
            return count;
        }

        //OEF5
        private static string RandomElement(List<string> list)
        {
            Random random = new Random();
            int index = random.Next(0, list.Count);
            return list[index];
        }

        //OEF 6
        private static List<int> RandomSubSet(List<int>list, int grootteSubSet)
        {
            Random random = new Random();
            List<int> tempSubSet = new List<int>(list);
            List<int> subSet = new List<int>();
            for (int i = 0; i < grootteSubSet; i++)
            {
                int randomNummer = random.Next(0, tempSubSet.Count);
                subSet.Add(tempSubSet[randomNummer]);
                tempSubSet.RemoveAt(randomNummer);
            }
            return subSet;
        }

        //OEF7
        private static bool DoListsOverlap(List<int> listOne,List<int> listTwo)
        {
            bool result = false;
            int countOne = 0;
            int countTwo = 0;
          
            while (result==false && countOne<listOne.Count)
            {
                while (result==false && countTwo<listTwo.Count)
                {
                    if (listOne[countOne]==listTwo[countTwo])
                    {
                        result = true;
                    }
                    else
                    {
                        countTwo++;
                    }
                }
                countTwo = 0;
                countOne++;
            }
            return result;
        }

        //OEF 8
        private static List<int> OverlappingElements(List<int> listOne, List<int> listTwo)
        {
            List<int> resultList = new List<int>();
            foreach (int itemOne in listOne)
            {
                foreach (int itemTwo in listTwo)
                {
                    if (itemTwo == itemOne && resultList.Contains(itemOne) != true)
                    {
                        resultList.Add(itemOne);
                    }
                }
            }
            return resultList;
        }

        //OEF 9
        private static List<List<int>> AllSubsets(List<int> a)
        {
            List<List<int>> finalList = new List<List<int>>();
            for (int i = 1; i < Math.Pow(2, a.Count); i++)
            {
                List<int> combination = new List<int>();
                for (int j = 0; j < a.Count; j++)
                {
                    if ((i & (Convert.ToByte(Math.Pow(2,j)))) != 0) //1 << (a.Count - j - 1))
                    {
                        combination.Add(a[j]);
                    }
                }
                finalList.Add(combination);
                
            }
            return finalList;
        }
    }
}
