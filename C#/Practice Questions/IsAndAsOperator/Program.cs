using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsAndAsOperator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dynamic avg = 123.45F;

            Boolean inttype = avg is Int32;

            Boolean floattype = avg is float;

            bool stringtype = avg is string;

            bool booltype = avg is bool;

            if (inttype == true || inttype == false)
            {
                Console.WriteLine("Is integer type : " + inttype);
            }

            if(floattype == true || floattype == false)
            {
                Console.WriteLine("Is float type : " + floattype);
            }

            if(stringtype == true || stringtype == false)
            {
                Console.WriteLine("Is string type : " + stringtype);
            }

            if(booltype == true || booltype == false)
            {
                Console.WriteLine("Is bool type : " + booltype);
            }

            if(avg is float)
            {
                Console.WriteLine("Is it Float Type : "+ avg is float);
            }

            Console.ReadKey();
        }
    }
}
