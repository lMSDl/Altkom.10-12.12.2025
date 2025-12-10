//namespace - przestrzeń nazw, czyli adres pod którym "mieszka" klasa
//namespace zaciągamy używając "using"
namespace ConsoleApp.Models; // ; zamiast klemrek powoduje, że namespace jest zastosowany do całego pliku

//klasa Product - klasa, która reprezentuje produkt
//class - szablon opisujący zachowania i cechy obiektów (instancji klas), które są wytwarzane na jej podstawie
//pełna nazwa klasy to <namespace>.<nazwa>
//internal - modyfikator dostępu - oznacza, że z klasy można korzystać w obrębie projektu
//public - modyfikator dostępu - oznacza, że z klasy można korzystać wszędzie
//brak modyfikatora dostępu = internal (dla klas) - wybierany jest najniższy poziom dostępu
/*internal*/
class Product
{

    //metoda konstrukcyjna  (konstruktor) - bezparametrowy
    //konstruktor ustawia wszystkie pola na domyślne wartości
    //metody konstrukcyjne są potrzebne, aby wstępnie skonfugorować produkt
    //brak określenia typu zwracanego i nazwa taka sama jak nazwa klasy
    //jeśli klasa nie ma żadnego zdefiniowanego konstruktora, to konstruktor bezparametrowy jest generowany automatycznie
    public Product()
    {
        SetProductionDate(DateTime.Now);
    }

    //konstruktor parametrowy - służy do zapewnienia klasie wartości początkowych przekazanych jako parametry
    //przeciążenie metody konstrukcyjnej = wiele metod o tej samej nazwie, ale przyjmujące inne parametry
    //: this(..) - odwołanie się do innego konstruktora. W ten sposób tworzy się konstruktory teleskopowe (rozszeżają swoje możliwości niwelując powtarzający się kod) - jest to opcjonalne
    public Product(string name) : this() //wywołanie konstruktora bezparametrowego
    {
        Name = name;
    }

    //jeśli w klasie występuje jakiś konstuktor parametrowy, to konstuktor bezparametrowy nie zostanie automatycznie wygenerowany
    //chcąc posiadać jednocześnie konstruktor parametrowy i bezparametrowy musimy go (ten bezparametrowy) jawnie utworzyć
    public Product(string name, DateTime expirationDate) : this(name)
    {
        ExpirationDate = expirationDate;
    }


    //pole - zmienna, która przechowuje wartość
    //private - oznacza dostęp tylko dla elementów danej klasy
    //inne możliwe modyfikatory dostępu to: public, protected, internal, protected internal
    //pola zazwyczaj są prywatne ze względu na hermetyzację, a dostęp realizowany jest przez metody dostępowe (getter i setter)
    //nazwa pola zaczyna się od podkreślenia, żeby zaznaczyć, że jest to pole prywatne - konwencja C#
    private DateTime _productionDate;

    //budowa metody: <modyfikator dostępu> <typ zwracany lub void> <nazwa metody>()
    //getter - do pobrania wartości pola - metoda zwraca typ zgodny z typem pola
    public DateTime GetProductionDate()
    {
        //instukcja zwracająca wynik działania metody - obowiązkowy gdy zadeklarowaliśmy, że klasa coś zwraca (jest inna niż void)
        return _productionDate;
    }

    //setter - do ustawiania wartości - metoda przyjmuje parametr, który zostaje wpisany w odpowiednie pole (można dodać kod "obróbki danych")
    //void - metoda nic nie zwaraca
    public void SetProductionDate(DateTime productionDate)
    {
        //metoda ustawiająca wartość pola - nie zwraca nic, więc jest typu void
        _productionDate = productionDate.Date; //obróbka danych - zapisujemy tylko datę bez czasu

        //odwołanie do bieżącej instancji klasy - this - używane gdy nazwa parametru jest taka sama jak nazwa pola
        //this.productionDate = productionDate.Date; 
    }


    //Property - właściwość


    //auto-property
    //właściwość integruje w sobie pole i metody dostępowe (getter i setter)
    public string Name { get; set; }

    //jest możliwość zmiany modyfikatora dostępu dla getter lub setter (osobno)
    public int Id { get; /*private*/ set; }
    public float Price { get; set; }


    //full-property
    //backfield do full-property - pozwala na dodatkowy kod w setterze i getterze
    private DateTime _expirationDate;
    public DateTime ExpirationDate
    {
        //getter dla property
        get
        {
            return _expirationDate;
        }
        //setter dla property - posiada niejawny parametr o nazwie value
        set
        {
            _expirationDate = value.Date; // kod obróbki danych - zapisujemy tylko datę bez czasu
        }
    }

    //od .NET 10 można używać skróconej składni dla backfield w full-property używając słowa kluczowego field zamiast definiowania osobnego pola
    public string Description
    {
        get
        {
            return field;
        }

        set
        {
            field = value;
        }
    }

    //read-only property - właściwość tylko do odczytu
    //nie posiada settera
    public string FullInfo
    {
        get { return $"Id: {Id}, Name: {Name}, Price: {Price}, ProductionDate: {GetProductionDate()}, ExpirationDate: {ExpirationDate}"; }
    }

    //metoda realizucjąca to samo co powyższe read-only property
    public string GetFullInfo()
    {
        return FullInfo;
    }

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

