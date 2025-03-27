using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectBaseTypeForValueAndReferenceType
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int64 num = 1234;
            string name = "abc";

            Console.WriteLine("Object method result with Int64 variable");
            Console.WriteLine("\nConvert to String \t {0}", num.ToString());
            
            Console.WriteLine("Get Hash code of number \t" + num.GetHashCode());
            Console.WriteLine("Get Type of number \t" + num.GetType());
            Console.WriteLine("Compare the value \t" + num.Equals(num));

            Console.WriteLine("\nObject method result with string variable");
            Console.WriteLine("\nConvert to String \t {0}", name.ToString());

            Console.WriteLine("Get Hash code of name \t" + name.GetHashCode());
            Console.WriteLine("Get Type of name \t" + name.GetType());
            Console.WriteLine("Compare the value \t" + name.Equals("xyz"));

            Console.ReadKey();
        }
    }
}
