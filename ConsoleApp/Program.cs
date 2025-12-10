//instrukcje najwyższego poziomu(top - level statements) - są to instrukcje, które są wykonywane bezpośrednio w pliku, bez potrzeby definiowania klasy lub metody głównej.
//wszystko co jest napisane w tym pliku jest traktowane jako kod, który będzie otoczony klasą Program i metodą Main podczas kompilacji


using ConsoleApp;
using ConsoleApp.Models;

Console.WriteLine(typeof(Product).Name);
Console.WriteLine(typeof(Product).Namespace);
Console.WriteLine(typeof(Product).FullName);

Product product = new Product();
product.SetProductionDate(DateTime.Now);
Console.WriteLine(product.GetProductionDate());

product.Name = "Laptop";
Console.WriteLine(product.Name);

product.ExpirationDate = DateTime.Now.AddYears(1);

Console.WriteLine(product.FullInfo);

product = new Product("Tablet");
Console.WriteLine(product.FullInfo);

product = new Product("Smartphone", DateTime.Now.AddMonths(6));
Console.WriteLine(product.FullInfo);

Introduction.Run();