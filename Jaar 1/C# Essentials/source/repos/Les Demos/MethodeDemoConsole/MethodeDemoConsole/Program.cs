using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodeDemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int cijfer = 0;
            BerekenMethode(cijfer);
            Console.WriteLine(cijfer);
        }
        private static void BerekenMethode(int getal)
        {
            Console.WriteLine("Start methode");
            getal++;
            getal = getal * 2;
            Console.WriteLine(getal);
            Console.WriteLine("Einde methode");
            
        }
    }
}
