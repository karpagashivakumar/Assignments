using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseAndTryParseApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // differentiating Parse and Try Parse Method

            // Using parse method

            Console.WriteLine("Input value for Int32");

            Int32 num = int.Parse(Console.ReadLine());
            Console.WriteLine("Result after parsing using parse method : " + num);

            // Using TryParse method
            string input = Console.ReadLine();

            Int64 num2;
            bool result=Int64.TryParse(input, out num2);

            if (result == true)
            {
                Console.WriteLine("Result after parsing using Try parse method : " + num2);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Invalid input");
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }
    }
}
