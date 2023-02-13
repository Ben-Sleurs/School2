using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringFromatDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 1;
            int b = 2;

            // oude manier
            string oudeTekst = a + " + " + b;

            // nieuwe manier
            string tekst = $"{a} + {b}";

            // andere nieuwe manier
            Console.WriteLine("{0} + {1}",a,b);

            Console.WriteLine(tekst);
            Console.WriteLine(oudeTekst);



            string normaletekst = "\t\\\ntest";
            string specialetekst = @"tab    tab     \
                      test";
            Console.WriteLine(specialetekst);
            Console.WriteLine(normaletekst);
        }
    }
}
