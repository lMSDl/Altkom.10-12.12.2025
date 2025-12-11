using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Delegates
{
    internal class BuildInDelegatesExample
    {
        void Add(int a, int b)
        {
            int result = a + b;
            Console.WriteLine(result);
        }

        bool Substract(int a, int b)
        {
            int result = a - b;
            Console.WriteLine(result);
            return result % 2 != 0;
        }

        //delegate void Method1Delegate(int a, int b);
        //delegate bool Method2Delegate(int a, int b);

        //Action - delegat wbudowany, który reprezentuje metodę zwracającą void i przyjmującą od 0 do 16 parametrów. Parametry są określane przez typy generyczne.
        //Func - delegat wbudowany, który reprezentuje metodę zwracającą wartość i przyjmującą od 0 do 16 parametrów. Ostatni typ generyczny określa typ zwracanej wartości, a poprzedzające go typy określają typy parametrów.

        void Method( Action<int, int> method1, Func<int, int, bool> method2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    method1(i, j);
                    if (method2(i, j))
                        Console.WriteLine("@@@");
                }
            }
        }

        public void Test()
        {
            Method(Add, Substract);
        }

    }
}
