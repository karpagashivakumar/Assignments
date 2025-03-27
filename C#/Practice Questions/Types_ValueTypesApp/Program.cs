using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Types_ValueTypesApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //variable of type int16,int32,int64

            Int16 mark_math = 50;
            Int32 mark_science = 78;
            Int64 mark_english = 85;
            Int64 mobile = 9876543210;

            Int64 total_marks = mark_math + mark_science + mark_english;

            //output with write line method
            Console.WriteLine("Output using write line method");
            Console.WriteLine("Math Marks: " + mark_math);
            Console.WriteLine("Science Marks: " + mark_science);
            Console.WriteLine("English Marks: " + mark_english);
            Console.WriteLine("Total Marks: " + total_marks);

            //output with write method
            Console.WriteLine("Output using write method");
            Console.Write(mark_math);
            Console.Write(mark_science);
            Console.Write(mark_english);
            Console.Write(total_marks);

            //formatting output using console.write method
            Console.WriteLine("\n Maths \t Science \t English \t Total \n"+" {0} \t {1} \t\t {2} \t\t {3}", mark_math, mark_science, mark_english, total_marks);

            string table_format=string.Format("\n\n Maths \t Science \t English \t Total \n"+" {0} \t {1} \t\t {2} \t\t {3}", mark_math, mark_science, mark_english, total_marks);
            Console.WriteLine(table_format);

            Console.ReadKey();
        }
    }
}
