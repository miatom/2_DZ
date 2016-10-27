using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsmiZad
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();

        }
        private static void LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = GetTheMagicNumber();
            Console.WriteLine(result);
        }
        private static int GetTheMagicNumber()
        {
            return  Task<int>.Run(()=>IKnowIGuyWhoKnowsAGuy()).Result;
        }
        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            var rez1=await IKnowWhoKnowsThis(10);
            var rez2 = await IKnowWhoKnowsThis(5);
            return rez1 + rez2;
            //return Task<int>.Run(() => IKnowWhoKnowsThis(10)).Result + Task<int>.Run(() => IKnowWhoKnowsThis(5)).Result;
            
        }
        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await FactorialDigitSum(n);
           // return FactorialDigitSum(n).Result;
        }
        public static async Task<int> FactorialDigitSum(int n)
        {
            
            int fakt = 1;
            int suma = 0;
            for (int i = 1; i <= n; i++)
            {
                fakt = fakt * i; //računamo n!
            }
            Console.WriteLine("{0}! = {1}", n, fakt);
            //trebamo sumu znamenaka
            var znamenke = new List<int>();
            while (fakt > 0)
            {
                znamenke.Add(fakt % 10);
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
        

    

