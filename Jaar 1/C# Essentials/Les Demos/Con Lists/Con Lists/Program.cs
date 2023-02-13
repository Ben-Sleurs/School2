using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Con_Lists
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> getallenLijst = new List<int>();


            int[] reeks = new int[] { 1, 2 };
            List<int> nummersKopie = new List<int>(reeks);

            List<int> startWaarde = new List<int>() { 1, 2 };

            List<int> c; c = new List<int>() { 1, 2, 3 };

            List<string> politiekers = new List<string>();

            politiekers.Add("Politieker 1");
            politiekers.Add("Politieker 2");
            politiekers.Add("Politieker 3");
            politiekers.Add("Politieker 3");
            politiekers.Add("Politieker 3");

            Console.WriteLine(politiekers[0]);
            Console.WriteLine(politiekers[1]);
            Console.WriteLine(politiekers[2]);
            Console.WriteLine(politiekers[3]);
            Console.WriteLine(politiekers[4]);

            if (politiekers.Contains("Politieker 1"))
            {
                Console.WriteLine("politiekers contains politieker 1");
            }

            int indexInLijst = politiekers.IndexOf("Politieker 3");
            Console.WriteLine(indexInLijst);

            politiekers.Clear();

            // Console.WriteLine(politiekers[0]); gaat error geven
            //0 9 7 3



            string check;
            List<string> series = new List<string>();
            do
            {
                Console.WriteLine("Welke serie wil je toevoegen?");
                series.Add(Console.ReadLine());

                Console.WriteLine("Wil je nog een serie toevoegen?(ja/nee)");
                check = Console.ReadLine().ToLower();

            } while (check!="nee");

            for (int i = 0; i < series.Count; i++)
            {
                Console.WriteLine(series[i]);
            }
          

        }
    }
}
