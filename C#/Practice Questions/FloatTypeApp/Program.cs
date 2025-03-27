using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloatTypeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float g = 123.14f;
            double h = 543.14d;
            decimal i = 432.14m;

            
            //parsing using convert class
            string numberFloat =Console.ReadLine();
            g=Convert.ToSingle(numberFloat);

            //parsing using parse method
            string numberDouble =Console.ReadLine();
            h=Double.Parse(numberDouble);

            //parse method
            string numberDecimal =Console.ReadLine();
            i=Decimal.Parse(numberDecimal);

            //parsing the integer data type
            int num=Int16.Parse(Console.ReadLine());
            Int32 num2= Int32.Parse(Console.ReadLine());
            Int64 num3 = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine("The decimal outputs:");
            Console.WriteLine(g);
            Console.WriteLine(h);
            Console.WriteLine(i);

            Console.WriteLine("The integer outputs:");
            Console.WriteLine(num);
            Console.WriteLine(num2);
            Console.WriteLine(num3);
            //Console.WriteLine($"float : {f}");
            //Console.WriteLine($"double : {d}");
            //Console.WriteLine($"decimal : {m}");
        }
    }
}
