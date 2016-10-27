using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SedmiZad
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Unesite pozitivni jednoznamenkasti  broj: ");
            string userInput = Console.ReadLine();
            int number;
            Int32.TryParse(userInput, out number);
            var rez = Task.Run(()=>FactorialDigitSum(number));
            
                       
            Console.ReadLine();
        }
        
        public static async Task<int> FactorialDigitSum(int n)
        {
           
            int fakt = 1;
            int suma = 0;
            for (int i = 1; i <= n; i++)
            {
                fakt = fakt * i; //računamo n!
            }
            Console.WriteLine("{0}! = {1}",n,fakt);
            //trebamo sumu znamenaka
            var znamenke=new List<int>();
            while (fakt > 0)
            {
                znamenke.Add(fakt%10);
                fakt = fakt / 10;
            }
            //sumiramo znamenke
            foreach (int item in znamenke)
            {
                suma += item; 
            }
            Console.WriteLine("suma znamenaka iznosi: {0}", suma);
            return suma;

        }
    }
}
