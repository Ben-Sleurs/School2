using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foutafh_console_1
{
    class Program
    {
        static void Main(string[] args)
        {
           
            
            
            
            

        }

        /// <summary>
        /// Verdubbelt het argument input en geeft dit terug.
        /// Indien er geen converteerbare input wordt meegegeven,
        /// dan geeft DoubleInput het getal 0 terug.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //OEF 1 A
        //private static int Doubleinput(string input)
        //{
        //    try
        //    {
        //        int getal = Convert.ToInt32(input);
        //        return getal * 2;


        //    }
        //    catch (FormatException ex)
        //    {
        //        Console.WriteLine("Fout in het converteren: " + ex.Message);
        //        throw ex;
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        Console.WriteLine("Fout in het converteren: " + ex.Message);
        //        throw ex;
        //    }
        //}
        //}
        //oef 1 b
        private static int doubleinput(string input)
        {

            int getal;
            bool isInt = int.TryParse(input, out getal);
            if (isInt && Math.Abs(getal) <= 1073741824 && String.IsNullOrEmpty(input))
            {
                return getal * 2;
            }
            else
            {
                Console.WriteLine("Er loopt iets fout (nullinput, non int input, verdubbeling wordt te groot");
                return 0;
            }

        }


        ////OEF 2 A
        //private static string VeranderSpatiesEnLeestekensNaarUnderscore(string tekst)
        //{
        //try
        //{
        //    string result = tekst.Replace(" ", "_");
        //    result = result.Replace(",", "_");
        //    result = result.Replace(".", "_");
        //    result = result.Replace(":", "_");
        //    result = result.Replace(";", "_");
        //    result = result.Replace("!", "_");
        //    result = result.Replace("?", "_");
        //    return result;
        //}
        //catch (NullReferenceException e)
        //{

        //    Console.WriteLine("String heeft null value");
        //    return "";
        //}


        //}
        //OEF 2 B
        private static string VeranderSpatiesEnLeestekensNaarUnderscore(string tekst)
        {
            if (tekst!=null)
            {
                string result = tekst.Replace(" ", "_");
                result = result.Replace(",", "_");
                result = result.Replace(".", "_");
                result = result.Replace(":", "_");
                result = result.Replace(";", "_");
                result = result.Replace("!", "_");
                result = result.Replace("?", "_");
                return result;
            }
            else
            {
                Console.WriteLine("tekst heeft null value");
                return "";
            }
            

        }
    }
}
