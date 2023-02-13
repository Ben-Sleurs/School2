using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Con_Arrays
{
    class Program
    {
        static void Main(string[] args)
        {

            
            string[] weekDagen = new string[] { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag" };
            double[] uren = new double[7];
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine($"Hoelang studeer je op {weekDagen[i]}?");
                uren[i] = Double.Parse(Console.ReadLine());
            }
            double gemiddelde = GemiddeldeArray(uren);
            Console.WriteLine($"je studeert gemiddeld {gemiddelde} uur per dag");
        }
        private static double GemiddeldeArray(double[] array) {
            double totaal = 0;
            for (int i = 0; i < array.Length; i++)
            {
                totaal = totaal + array[i];
            }
            return totaal / array.Length;
        }
    }
}
