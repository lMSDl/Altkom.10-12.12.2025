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



Product product1 = new Product("Camera", DateTime.Now.AddMonths(3));
product1.Price = 299.99f + 200;
Console.WriteLine(product1.FullInfo);
Product product2 = new Product("Headphones", DateTime.Now.AddMonths(12));
product2.Price = 199.99f;
Console.WriteLine(product2.FullInfo);

//dodawanie dwóch produktów - operator +
//tworzy nowy produkt, którego nazwa to połączenie nazw dwóch produktów, cena to suma cen dwóch produktów, a data ważności to najdalsza data ważności z dwóch produktów
Product bundle = product1 + product2;
Console.WriteLine(bundle.FullInfo);

//bundle.Price = bundle.Price + 50f;

//skrótowy zapis dodawania wartości do pola Price przy użyciu przeciążenia operatora +
bundle = bundle + 50f;
Console.WriteLine(bundle.FullInfo);


Console.WriteLine( 1 + 1 ); // 2
Console.WriteLine( "1" + 1); // "11" = konkatenacja (łączenie) stringów
Console.WriteLine( 1 + "1" + 1); // (1 + "1") + 1 = "111"
Console.WriteLine( 1 + 1 + "1"); // (1 + 1) + "1" = "21"