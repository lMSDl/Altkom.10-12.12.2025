namespace ConsoleApp.Models; 
partial class Product
{
    //przeciążenie operatora + - pozwala na dodawanie dwóch obiektów klasy Product - w tym przypadku robiony jest zestaw z 2 produków
    //przeciążenie wymaga zdefiniowania metody statycznej, która przyjmuje dwa parametry (lewa i prawa strona operatora) oraz słowa kluczowego "operator"
    //możemy przeciążać operatory, które są zdefiniowane w C# (np. +, -, *, /, ==, !=, <, >, <=, >=)
    public static Product operator +(Product left, Product right)
    {
        Product newProduct = new Product();
        newProduct.Name = $"{left.Name} + {right.Name}";
        newProduct.Price = left.Price + right.Price;
        newProduct.Price *= 0.9f; //10% zniżki za zakup w pakiecie
        newProduct.ExpirationDate = left.ExpirationDate < right.ExpirationDate ? left.ExpirationDate : right.ExpirationDate;

        return newProduct;
    }

    //możemy przeciążyć operator również dla innych typów
    //w tym przypadku dodajemy do ceny produktu wartość typu float
    public static Product operator +(Product left, float right)
    {
        left.Price += right;
        return left;
    }

    //indexer - pozwala na dostęp do obiektu klasy jak do tablicy lub słownika
    //możemy dodać też setter, żeby móc ustawiać wartości w klasie
    public string this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return Name;
                case 1:
                    return Id.ToString();
                case 2:
                    return Price.ToString();
                default:
                    return "Invalid index";
            }
        }

        set
        {
            switch (index)
            {
                case 0:
                    Name = value;
                    break;
                case 1:
                    if (int.TryParse(value, out int id))
                        Id = id;
                    break;
                case 2:
                    if (float.TryParse(value, out float price))
                        Price = price;
                    break;
                default:
                    break;
            }
        }
    }

    public string this[string index]
    {
        get
        {
            //switch expression - pozwala na bardziej zwięzłe zapisywanie switcha
            return index.ToLower() switch
            {
                "name" => Name,
                "id" => Id.ToString(),
                "price" => Price.ToString(),
                _ => "Invalid index",
            };
        }
    }

}

