//instrukcje najwyższego poziomu(top - level statements) - są to instrukcje, które są wykonywane bezpośrednio w pliku, bez potrzeby definiowania klasy lub metody głównej.
//wszystko co jest napisane w tym pliku jest traktowane jako kod, który będzie otoczony klasą Program i metodą Main podczas kompilacji

using ConsoleApp.Models;

Console.WriteLine("Hello, World!");

//flaga Nullable w pliku csproj - pozwala na używanie typów wartościowych i referencyjnych w pododobny sposób w zakresie nullowania

//typy wartościowe nie mogą być null
//int a = null;

int a = 5;
//Nullable - opakowanie typów wartościowych, które pozwala na przypisanie wartości null do typów wartościowych
Nullable<int> b = null; // Nullable type
int? c = null; // Nullable type używająć shorthand syntax


Product product = new Product();
product.Name = "Sample Product";
product.Id = 1;

Product? product2 = null;

Console.WriteLine($"Value of a: {a}");
Console.WriteLine($"Value of b: {b}");
Console.WriteLine($"Value of c: {c}");

Console.WriteLine($"Product Name: {product.Name}, Product Id: {product.Id}");
if(product2 is not null)
    Console.WriteLine($"Product Name: {product2.Name}, Product Id: {product2.Id}");


ChangeProduct(product2);

void ChangeProduct(Product product)
{
    product.Name = "Smartphone";
    product.Id = 2;
}