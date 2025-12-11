
using Models;
using Services.InMemory;
using Services.Interfaces;

IProductsService service = new ProductsService();

service.Create(new Models.Product
{
    Name = "Product 1",
    Price = 10.5f,
});
service.Create(new Models.Product
{
    Name = "Product 2",
    Price = 20.0f,
});


bool exit = false;
while (!exit)
{
    Console.Clear();
    
    foreach (var item in service.GetAll())
    {
        Console.WriteLine($"{item.Id}. {item.Name} - {item.Price} - {item.CreatedAt}");
    }

    Console.WriteLine("Commands: create, delete, exit");
    string? input = Console.ReadLine();

    switch(input?.ToLower())
    {
        case "create":
            Create();
            break;
        case "delete":
            Delete();
            break;
        case "exit":
            exit = true;
            break;
        default:
            Console.WriteLine("Unknown command");
            break;
    }

    Console.WriteLine("Press any key...");
    Console.ReadKey();
}

string ReadString(string label)
{
    Console.Write(label);
    return Console.ReadLine() ?? string.Empty; //jeśli użytkownik nie poda nazwy, przypisujemy pusty string. ?? - operator null-coalescing
}

float ReadFloat(string label)
{
    Console.Write(label);

    string? input = Console.ReadLine();
    float price;
    //TryParse - próbuje przekonwertować string na float
    if (float.TryParse(input, out price))
    {
        return price;
    }
    else
    {
        Console.WriteLine("Invalid price, try again.");
        return ReadFloat(label); //rekurencyjne wywołanie funkcji, aż użytkownik poda poprawną wartość
    }
}

DateTime ReadDate(string label)
{
    Console.Write(label);
    string? input = Console.ReadLine();

    DateTime date;
    try
    {
        date = DateTime.Parse(input ?? string.Empty);
        if (date > DateTime.Now)
            throw new InvalidDataException("Date cannot be in the future");

        return date;
    }

    //filtrowanie wyjątków - możemy obsługiwać różne wyjątki w różny sposób
    //catch z tylko typem wyjątku - oznacza, że łapiemy dowolny wyjątek dziedzicący po wskazanym typie wyjątku
    catch (FormatException) //instancja wyjątku nie jest nam potrzebna, więc nie musimy deklarować nazywanej zmiennej, która będzie przechowywać wyjątek
    {
        Console.WriteLine("Invalid date format");
        return ReadDate(label);
    }
    //catch(Exception ex) - z parametrem - przechwytuje wyjątki zgodne z klasą parametru, dając wgląd w obiekt wyjątku
    catch (InvalidDataException ex) //tutaj potrzebujemy informacji o wyjątku, więc deklarujemy zmienną ex
    {
        Console.WriteLine(ex.Message);
        return DateTime.Now;
    }
    //kolejność bloków catch ma znaczenie - najpierw sprawdzane są bardziej szczegółowe wyjątki, potem bardziej ogólne
    //nie powinniśmy umieszczać catch z typem bazowym (np. Exception) przed catch z typem pochodnym (np. FormatException), ponieważ spowoduje to, że catch z typem pochodnym nigdy nie zostanie osiągnięty
    catch /*(Exception)*/
    {
        Console.WriteLine("Unknown error");
        return ReadDate(label);
    }
}

void Create()
{
    Product item = new Product();
    item.Name = ReadString("Name: ");
    item.Price = ReadFloat("Price: ");
    item.CreatedAt = ReadDate("Date (yyyy-MM-dd hh:mm:ss): ");

    service.Create(item);
}

void Delete()
{
    string? input;
    int id;
    Console.Write("Id: ");
    input = Console.ReadLine();

    //try-catch - służy do obsługi wyjątków
    //w bloku try umieszczamy kod, który może zgłosić wyjątek
    try
    {
        id = int.Parse(input);
    }
    //catch bez parametru - przechwytuje wszystkie wyjątki, ale nie daje nam informacji o wyjątku
    catch
    {
        id = 0;
    }

    if(!service.Delete(id))
    {
        Console.WriteLine("Product not found");
    }
}