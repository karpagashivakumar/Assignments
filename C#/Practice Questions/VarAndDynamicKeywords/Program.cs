using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarAndDynamicKeywords
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var i = 10;
            Console.WriteLine("Get type of i : " + i.GetType());

            dynamic j = "Hello";
            Console.WriteLine("Get type of j : " + j.GetType());

            Console.ReadKey();
        }
    }
}
