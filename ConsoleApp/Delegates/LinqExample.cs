using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Delegates
{
    internal class LinqExample
    {
        int[] numbers = new[] { 1, 2, 5, 7, 3, 8, 0, 9 };

        IEnumerable<string> strings = "Ala ma kota i dwa psy".Split(' ').ToList();

        ICollection<Product> products = new List<Product>()
        {
            new Product { Name = "kasza", Price = Random.Shared.NextSingle()*10, ExpirationDate = DateTime.Now.AddDays(Random.Shared.Next(30, 180)) },
            new Product { Name = "ser", Price = Random.Shared.NextSingle()*10, ExpirationDate = DateTime.Now.AddDays(Random.Shared.Next(30, 180)) },
            new Product { Name = "makaron", Price = Random.Shared.NextSingle()*10, ExpirationDate = DateTime.Now.AddDays(Random.Shared.Next(30, 180)) },
            new Product { Name = "mleko", Price = Random.Shared.NextSingle()*10, ExpirationDate = DateTime.Now.AddDays(Random.Shared.Next(30, 180)) },
            new Product { Name = "kawa", Price = Random.Shared.NextSingle()*10, ExpirationDate = DateTime.Now.AddDays(Random.Shared.Next(30, 180)) },
            new Product { Name = "jajka", Price = Random.Shared.NextSingle()*10, ExpirationDate = DateTime.Now.AddDays(Random.Shared.Next(30, 180)) },
            new Product { Name = "masło", Price = Random.Shared.NextSingle()*10, ExpirationDate = DateTime.Now.AddDays(Random.Shared.Next(30, 180)) },
            new Product { Name = "jogurt", Price = Random.Shared.NextSingle()*10, ExpirationDate = DateTime.Now.AddDays(Random.Shared.Next(30, 180)) },
        };

        public void Test()
        {
            IEnumerable<int> ints = ValuesGreatedThan(4, numbers);


            //Where - filtracja
            //ToArray - konwersja do tablicy, zakończenie operacji LINQ
            ints = numbers.Where(Filter).ToArray();
            //zastosowanie wyrażenia lambda zamiast metody Filter
            ints = numbers.Where(number => number > 4).ToArray();

            //samo wyrażenie Linq nie wykonuje się, dopóki nie zostanie zakończone
            //jeśli nie zakończymy operacji Linq metodą kończącą to operacje będą wykonywane za każdym razem, gdy będziemy używać zmiennej ints
            //metody kończące operacje LINQ to: ToArray, ToList, First, FirstOrDefault, Last, LastOrDefault, Count, Any, All
            ints = numbers.Where(number => number > 4)
                            //jeśli chcemy wyszukać po kilku kryteriach, to możemy użyć wielu Where albo użyć operatora && (i) lub || (lub)
                            .Where(number => number % 2 == 0)
                            .ToArray();

            ints = numbers.Where(number => number < 4 || number > 8)
                            .ToArray();

            //OrderBy - sortowanie rosnąco
            //(n => n) wyrażenie lambda oznaczające, że sortujemy po własnej wartości
            ints = numbers.Where(n => n > 2).OrderBy(n => n).ToArray();
            //OrderByDescending - malejąco
            ints = numbers.Where(n => n > 2).OrderByDescending(n => n).ToArray();

            //Select - wybieranie elementów / projekcja
            string[] names = products.Where(x => x.Price > 5).Select(x => x.Name).ToArray();
            names = products.Where(x => x.Price > 5).Select(x => x.Name).Where(x => x.Length > 5).ToArray();
            names = products.Where(x => x.Price > 5).Where(x => x.Name.Length > 5).Select(x => x.Name).ToArray(); //równoważne zapytanie z powyższym
            var p = products.Where(x => x.Price > 5).Where(x => x.Name.Length > 5).Select(x => new { x.Name, x.Price }).ToArray(); //tworzymy anonimowy typ z Name i Price

            //Avegage - średnia, zapytanie aggregujące
            float averagePrice = products.Average(x => x.Price);
            averagePrice = products.Select(x => x.Price).Average();
            averagePrice = products.Select(x => x.Price).Where(x => x > 3).Average();

            //GrupBy - grupowanie - grupuje elementy na podstawie wskazanej właściwości. Produkuje kolekcję kluczy i odpowiadających im grup elementów.
            //var - pozwala kompilatorowi automatycznie wywnioskować typ zmiennej na podstawie przypisanej wartości (prawej story równania)
            var groups = strings.GroupBy(x => x.Length).ToArray();
            groups = strings.GroupBy(x => x.Length).Where(x => x.Key > 2).ToArray();

            //first - zwraca pierwszy element kolekcji lub rzuca wyjątek, jeśli nie ma takiego elementu
            var result1 = products.First(x => x.Name.StartsWith("m"));
            result1 = products.Where(x => x.Name.StartsWith("m")).First();
            //firstOrDefault - zwraca pierwszy element kolekcji lub default, jeśli nie ma takiego elementu
            var result2 = products.FirstOrDefault(x => x.Name.StartsWith("mx"));
            result2 = products.Where(x => x.Name.StartsWith("mx")).FirstOrDefault();
            //single - zwraca jeden element kolekcji - jeśli jest ich więcej, to wyrzuca wyjątek
            var result3 = products.Single(x => x.Name.StartsWith("s"));
            result3 = products.Where(x => x.Name.StartsWith("s")).Single();
            //singleOrDefault - zwraca jeden element kolekcji lub default, jeśli nie ma takiego elementu - jeśli jest ich więcej, to wyrzuca wyjątek
            var result4 = products.SingleOrDefault(x => x.Name.StartsWith("sx"));
            result4 = products.Where(x => x.Name.StartsWith("sx")).SingleOrDefault();
            //last - zwraca ostatni element kolekcji
            var result5 = products.Last(x => x.Name.StartsWith("m"));
            result5 = products.Where(x => x.Name.StartsWith("m")).Last();
            //lastOrDefault - zwraca ostatni element kolekcji lub default, jeśli nie ma takiego elementu
            var result6 = products.LastOrDefault(x => x.Name.StartsWith("mx"));
            result6 = products.Where(x => x.Name.StartsWith("mx")).LastOrDefault();

            var result = products.Where(x => x.Price > products.Average(xx => xx.Price))
                .Select(x => x.Name)
                .Skip(2) //pomiń pierwsze 2 elementy
                .Take(2) //weź tylko kolejne 2 elementy
                .Aggregate((acc, name) => $"{acc}, {name}");


            Console.ReadLine();
        }

        private bool Filter(int number)
        {
            return number > 4;
        }

        private IEnumerable<int> ValuesGreatedThan(int limit, IEnumerable<int> numbers)
        {
            List<int> ints = new List<int>();
            foreach (var number in numbers)
            {
                if (number > limit)
                {
                    ints.Add(number);
                }
            }
            return ints;
        }
    }
}
