
namespace ConsoleApp.Models;

//partial - słowo kluczowe, które pozwala na podzielenie definicji klasy na wiele plików
//partial class - klasa, która jest podzielona na wiele plików
//wymagania: wszystkie pliki muszą być w tym samym projekcie, w tej samej przestrzeni nazw i mieć taką samą nazwę klasy
partial class Product
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


    //metoda realizucjąca to samo co powyższe read-only property
    public string GetFullInfo()
    {
        return FullInfo;
    }

}

