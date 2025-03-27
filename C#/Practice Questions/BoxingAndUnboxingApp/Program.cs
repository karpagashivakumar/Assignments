using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingAndUnboxingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Converting value type to reference type

            Console.WriteLine("Enter a number : ");
            Int32 boxValue = Int32.Parse(Console.ReadLine());
            object obj = boxValue;  // Boxing

            Console.WriteLine("After Boxing : "+obj);

            // Converting reference type to value type
            Int32 unboxValue = (Int32)obj;  // Unboxing
            Console.WriteLine("After explicit conversion : "+unboxValue);

            unboxValue = Convert.ToInt32(boxValue);
            Console.WriteLine("Conversion using Convert class : " + unboxValue);

            Console.ReadKey();
        }
    }
}
