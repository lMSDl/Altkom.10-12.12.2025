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
partial class Product
{
    //pole - zmienna, która przechowuje wartość
    //private - oznacza dostęp tylko dla elementów danej klasy
    //inne możliwe modyfikatory dostępu to: public, protected, internal, protected internal
    //pola zazwyczaj są prywatne ze względu na hermetyzację, a dostęp realizowany jest przez metody dostępowe (getter i setter)
    //nazwa pola zaczyna się od podkreślenia, żeby zaznaczyć, że jest to pole prywatne - konwencja C#
    private DateTime _productionDate;


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

}

