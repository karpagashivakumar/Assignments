using System;

using FinancialCalculation;
using Mathematical;
using Calculation;
using NamespaceLibrary;

namespace NamespaceExecutableApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FinancialCalculation.Math mathObject = new FinancialCalculation.Math();
            Mathematical.Math mathFunc = new Mathematical.Math();
            Calculation.Math mathCalc = new Calculation.Math();
            Class1 obj = new Class1();

            Console.WriteLine(mathObject);
            Console.WriteLine(mathFunc);
            Console.WriteLine(mathCalc);

            FinancialCalculation.Math mathObject2 = new FinancialCalculation.Math();
            Mathematical.Math mathFunc2 = new Mathematical.Math();
            Calculation.Math mathCalc2 = new Calculation.Math();
        }
    }
}
