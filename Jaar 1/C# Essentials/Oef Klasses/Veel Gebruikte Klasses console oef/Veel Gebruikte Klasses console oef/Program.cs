using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veel_Gebruikte_Klasses_console_oef
{
    class Program
    {
        static void Main(string[] args)
        {

            ////OEFENING 1

            //Console.WriteLine("input Getal 1");
            //double getal1 = double.Parse(Console.ReadLine());
            //Console.WriteLine("input getal 2");
            //double getal2 = double.Parse(Console.ReadLine());
            //Console.WriteLine("input getal 3");
            //double getal3 = double.Parse(Console.ReadLine());

            //double grootstegetal = Math.Max(getal1, Math.Max(getal2, getal3));
            //Console.WriteLine(grootstegetal);

            ////OEFENING 2

            //Random dobbelsteen = new Random(73);
            //string nogeens = "y";
            //while (nogeens == "y")
            //{
            //    Console.WriteLine(dobbelsteen.Next(1, 3));
            //    Console.WriteLine("nog eens ? ");
            //    nogeens = Console.ReadLine();
            //}

            ////oefening 3

            //Random dobbelsteen = new Random(73);
            //string nogeens = "y";
            //Console.WriteLine("hoevel zijdes?");
            //int zijdes = int.Parse(Console.ReadLine());

            //while (nogeens == "y")
            //{
            //    Console.WriteLine(dobbelsteen.Next(1, zijdes));
            //    Console.WriteLine("nog eens ?");
            //    nogeens = Console.ReadLine();
            //}
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= 20; i++){ 
                sb.Append(i.ToString()); 
            }
            Console.WriteLine(sb);



        }
    }
}
