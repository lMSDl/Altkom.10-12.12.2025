using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Delegates
{
    internal class LambdaExample
    {
        Func<int, int, int> Calculator { get; set; }
        Func<string> SomeFunc { get; set; }
        Action<int> SomeAction { get; set; }
        Action AnotherAction { get; set; }

        //wyrażenie lambda
        //<opcjonalny parametr> <operator> <ciało>
        // (a, b) => {}
        // a => {}
        // () => {}

        public void Test()
        {
            Calculator = Calc;

            //przypisanie metody anonimowej do delegata
            //metoda anonimowa to metoda, która nie ma nazwy i jest definiowana w miejscu, gdzie jest używana
            Calculator = delegate (int a, int b) { return a - b; };
            //lambda expression - wyrażenie lambda to skrócona wersja metody anonimowej
            Calculator = (int a, int b) => { return a - b; };
            Calculator = (a, b) => { return a - b; };
            Calculator = (a, b) => a - b; //najkrótsza wersja, bez nawiasów i typu zwracanego - możliwa do zastosowania tylko w przyapdku, gdy mamy pojedynczą instukcję


            SomeFunc = delegate () { return "Hello"; };
            SomeFunc = () => { return "Hello"; };
            SomeFunc = () => "Hello";

            SomeAction = delegate (int a) { Console.WriteLine(a); };
            SomeAction = (int a) => { Console.WriteLine(a); };
            SomeAction = (a) => Console.WriteLine(a);
            SomeAction = a => Console.WriteLine(a);

            AnotherAction = delegate () { Console.WriteLine("No parameters"); };
            AnotherAction = () => Console.WriteLine("No parameters");
        }

        int Calc(int a, int b)
        {
            return a + b;
        }
    }
}
