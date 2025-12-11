
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

    Console.WriteLine("Commands: delete, exit");
    string? input = Console.ReadLine();

    switch(input?.ToLower())
    {
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