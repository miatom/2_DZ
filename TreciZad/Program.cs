using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreciZad
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] integers = new[] { 1, 2, 2, 2, 3, 3, 4, 5 };
            var strings = integers.GroupBy(w=>w);
            
            foreach (var item in strings)
            {
                Console.WriteLine("Broj {0} se pojavljuje {1} puta.",item.Key,item.Count());
            }
            Console.ReadLine();

            
            

        }
       
    }
}
