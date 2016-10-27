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
            var str = integers.GroupBy(w=>w).AsEnumerable().Select(s=>string.Format("Broj {0} se pojavljuje {1} puta.", s.Key, s.Count())).ToArray();
            string[] strings = str;
            for (int i=0;i<strings.Length;i++)
            {
                Console.WriteLine(strings[i]);
            }          
            Console.ReadLine();

            
            

        }
       
    }
}
