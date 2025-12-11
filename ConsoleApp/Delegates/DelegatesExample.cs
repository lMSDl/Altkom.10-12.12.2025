using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Delegates
{
    //Delegaty to typy referencyjne, które reprezentują metody z określonym podpisem.
    //Pozwalają na przekazywanie metod jako argumentów do innych metod, co umożliwia tworzenie bardziej elastycznego i modularnego kodu.
    //Potocznie to wskaźniki do metod.
    internal class DelegatesExample
    {
        //delegat, który może wskazywać na metody zwracające void i nie przyjmujące żadnych parametrów. Nazwa delegata moze być dowolna, w tym przypadku to VoidWithoutParameters 
        delegate void VoidWithoutParameters();
        //delegat, który może wskazywać na metody zwracające void i przyjmujące jeden parametr typu string
        delegate void VoidWithStringParameter(string @string);
        //delegat, który może wskazywać na metody zwracające bool i przyjmujące dwa parametry typu int
        delegate bool BoolWithTwoIntParameters(int int1, int int2);

        public void Func1()
        {
            Console.WriteLine("Func1");
        }

        public void Func2(string input)
        {
            Console.WriteLine(input);
        }
        public bool Func3(int a, int b)
        {
            Console.WriteLine($"a = {a}, b = {b}");
            return a == b;
        }
        public bool Func4(int a, int b)
        {
            Console.WriteLine($"a = {a}, b = {b}");
            return a != b;
        }

        BoolWithTwoIntParameters Delegate3 { get; set; }

        public void Test()
        {
            VoidWithoutParameters delegate1 = new VoidWithoutParameters(Func1);
            delegate1.Invoke(); // wywołanie delegata

            VoidWithStringParameter delegate2 = Func2;
            delegate2.Invoke("Hello World!"); // wywołanie delegata

            Delegate3 = Func3; // przypisanie metody do delegata

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var result = Delegate3(i, j);
                    if (result)
                        Console.WriteLine("==");
                }
            }

            if (Check(Delegate3, 1, 1))
                Console.WriteLine("==");

            if (Check(Func4, 1, 2))
                Console.WriteLine("!=");
        }

        bool Check(BoolWithTwoIntParameters func, int a, int b)
        {
            Console.WriteLine("----");
            return func.Invoke(a, b);
        }
    }
}
