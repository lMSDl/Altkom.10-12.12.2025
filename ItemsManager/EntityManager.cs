using Models;
using Services.InMemory;
using Services.Interfaces;
using System.Text.Json;
using System.Xml.Serialization;

namespace ItemsManager
{
    // <T> - oznacza, że klasa jest generyczna i może być używana z różnymi typami danych
    //T jest parametrem typu, który będzie używany w klasie
    //where T : Entity - oznacza, że klasa generyczna T musi być typu Entity lub jej pochodną
    internal abstract class EntityManager<T> where T : Entity
    {
        protected IEntityService service = new EntityService();
        private readonly string _filePath;

        private JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true, //ładne formatowanie JSON-a z wcięciami
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, //właściwości obiektu będą zapisywane w formacie camelCase
            IgnoreReadOnlyProperties = true, //ignorowanie właściwości tylko do odczytu podczas serializacji
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault //ignorowanie właściwości, które mają wartość domyślną (np. null dla referencji, 0 dla int itp.)
        };

        protected EntityManager(string filePath)
        {
            _filePath = filePath;
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                //Console.WriteLine(service.GetAll().Select(x => x.ToString()).Aggregate((acc, text) => $"{acc}\n{text}"));
                Console.WriteLine(string.Join("\n", service.GetAll().Select(x => x.ToString())));

                Console.WriteLine("Commands: create, edit, delete, json, xml, import, exit");
                string? input = Console.ReadLine();

                switch (input?.ToLower())
                {
                    case "create":
                        Create();
                        break;
                    case "edit":
                        Edit();
                        break;
                    case "delete":
                        Delete();
                        break;
                    case "xml":
                        ToXml();
                        break;
                    case "json":
                        ToJson();
                        break;
                    case "import":
                        Import();
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
        }

        void Import()
        {
            string filePath = ReadString("Enter file path to import data from:").Trim("\"").ToString();
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            //using FileStream fileStream = new FileStream(filePath, FileMode.Open); //otwarcie pliku do odczytu
            //using StreamReader streamReader = new StreamReader(fileStream); //StreamReader opakowuje FileStream i pozwala na odczyt stringów bezpośrednio ze strumienia
            //string content = streamReader.ReadToEnd(); //odczyt całej zawartości pliku do stringa
            //Console.WriteLine(content);
            IEnumerable<T> items;

            switch (Path.GetExtension(filePath))
            {
                case ".json":
                    string content = File.ReadAllText(filePath); //odczyt całej zawartości pliku do stringa - prostsza wersja powyższego kodu
                    items = JsonSerializer.Deserialize<T[]>(content, _options)!;

                    break;
                case ".xml":
                    {
                        XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T[]));
                        using FileStream fileStream = new FileStream(filePath, FileMode.Open);
                        items = (IEnumerable<T>)xmlSerializer.Deserialize(fileStream)!;
                    }
                    break;
                default:
                    Console.WriteLine("Unsupported file format.");
                    items = Array.Empty<T>();
                    return;
            }

            foreach (T item in items)
            {
                service.Create(item);
            }

        }

        public void SaveToFile(string data, string extension)
        {
            Console.WriteLine("Save to file?");
            string? input = Console.ReadLine();
            if (input?.ToLower() != "y")
            {
                return; //jeśli użytkownik nie chce zapisać do pliku, to wychodzimy z metody
            }

            // File - fasada do operacji na plikach, która udostępnia statyczne metody do pracy z plikami (tworzenie, odczyt, zapis, usuwanie itp.)
            File.WriteAllText(_filePath + $".{extension}", data); //zapis do pliku o podanej ścieżce
        }

        public void SaveToFileUsingStreams(string data, string extension)
        {
            Console.WriteLine("Save to file?");
            string? input = Console.ReadLine();
            if (input?.ToLower() != "y")
            {
                return; //jeśli użytkownik nie chce zapisać do pliku, to wychodzimy z metody
            }

            //klasy strumieniowe - klasy opierające swoje działanie na strumieniu byte'ów
            //wykorzustanie using spowoduje automatyczne wywołanie funkcji Dispose
            using FileStream fileStream = new FileStream(_filePath + $".{extension}", FileMode.Create);

            //var bytes = System.Text.Encoding.UTF8.GetBytes(data); //konwersja stringa na tablicę byte'ów
            //fileStream.Write(bytes); //zapis do pliku

            using StreamWriter streamWriter = new StreamWriter(fileStream); //StreamWriter opakowuje FileStream i pozwala na zapis stringów bezpośrednio do strumienia
            streamWriter.Write(data); //zapis do pliku
            fileStream.Flush(); //wymusza zapis wszystkich danych do pliku / opróżnia bufor strumienia

            //fileStream.Dispose(); //zwalnianie zasobów po zakończeniu pracy ze strumieniem
        }



        //serializacja - proces przekształcania obiektu w format (najcześciej tekstowy), który można przechowywać lub przesyłać
        void ToJson()
        {
            T[] items = service.GetAll().Cast<T>().ToArray();



            //JsonSerializer - klasa do serializacji obiektów do formatu JSON
            //JsonSerializer może serializować obiekty bezpośrednio do stringa
            var json = JsonSerializer.Serialize(items, _options);
            Console.WriteLine(json);
            SaveToFile(json, "json");
        }

        void ToXml()
        {
            T[] items = service.GetAll().Cast<T>().ToArray();

            //xmlSerializer - klasa do serializacji obiektów do formatu XML
            XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T[])); //musimy określić typ obiektu, który będziemy serializować (w tym przypadku tablica T)

            //xmlSerializer oparty jest o strumienie, więc musimy użyć StringWriter do zapisu do stringa
            //using var stringWriter = new StringWriter();
            //xmlSerializer.Serialize(stringWriter, items);
            //var xml = stringWriter.ToString();

            using MemoryStream memoryStream = new MemoryStream(); //strumień pamięciowy - strumień oparty o pamięć RAM
            xmlSerializer.Serialize(memoryStream, items);
            var xmlArray = memoryStream.ToArray(); //pobranie tablicy byte'ów ze strumienia pamięciowego
            var xml = System.Text.Encoding.UTF8.GetString(xmlArray); //konwersja tablicy byte'ów na string


            Console.WriteLine(xml);
            SaveToFile(xml, "xml");
        }


        //string? defaultValue = null - oznacza, że parametr jest opcjonalny i jeśli nie zostanie podany, to przyjmie wartość null
        //parametry domyślne muszą być na końcu listy parametrów
        //jeśli parametr ma wartość domyślną, to nie musimy go podawać przy wywołaniu funkcji (patrz funkcja Create)
        string ReadString(string label, string? defaultValue = null)
        {
            Console.Write(label);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) && defaultValue is not null)
            {
                return defaultValue;
            }

            return input ?? string.Empty; //jeśli użytkownik nie poda nazwy, przypisujemy pusty string. ?? - operator null-coalescing
        }

        int ReadInt(string label)
        {
            int result;
            while (!TryReadInt(label, out result))
            {
                //pusta pętla - wykonuje się aż do momentu, gdy TryReadInt zwróci true
            }
            return result;
        }

        //Try Patter - metoda zwraca bool, a wynik konwersji jest przekazywany przez parametr out
        bool TryReadInt(string label, out int result)
        {
            Console.Write(label);
            string? input = Console.ReadLine();
            //try-catch - służy do obsługi wyjątków
            //w bloku try umieszczamy kod, który może zgłosić wyjątek
            try
            {
                //jeśli konwersja się powiedzie, przypisujemy wartość do zmiennej out i zwracamy true
                result = int.Parse(input);
                return true;
            }
            //catch bez parametru - przechwytuje wszystkie wyjątki, ale nie daje nam informacji o wyjątku
            catch
            {
                Console.WriteLine("Invalid integer, try again.");
                //musimy przypisać wartość do zmiennej out w przypadku niepowodzenia
                result = default; // przypisanie domyślnej wartości zgodnej z typem zmiennej (dla int to 0)
                                  // zwracamy false, ponieważ konwersja się nie powiodła
                return false;
            }
        }


        protected float ReadFloat(string label, float? defaultValue = null)
        {
            Console.Write(label);

            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) && defaultValue.HasValue)
            {
                return defaultValue.Value;
            }

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

        protected virtual DateTime ReadDate(string label, DateTime? defaultValue = null)
        {
            Console.Write(label);
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) && defaultValue.HasValue)
            {
                return defaultValue.Value;
            }

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

        void Edit()
        {
            int id = ReadInt("Id: ");
            T? item = (T?)service.Get(id);
            if (item is null)
            {
                Console.WriteLine("Entity not found.");
                return; //return poza zwrotem wartości - kończy działanie metody void
            }

            T entity = CreateEntity();
            entity.Name = ReadString($"Name ({item.Name}): ", item.Name);
            ExtraEdit(entity, item);

            service.Update(id, entity);
        }

        //metoda abstrakcyjna pozwala na edycję dodatkowych właściwości encji specyficznych dla danego menedżera
        protected abstract void ExtraEdit(T entity, T item);

        void Create()
        {
            T item = CreateEntity();
            item.Name = ReadString("Name: ");
            ExtraCreate(item);

            service.Create(item);
        }
        //metoda abstrakcyjna pozwala na utworzenie encji specyficznej dla danego menedżera (np. ProductManager tworzy Product, CustomerManager tworzy Customer)
        protected abstract T CreateEntity();
        //metoda abstrakcyjna pozwala na uzupełnienie dodatkowych właściwości encji specyficznych dla danego menedżera
        protected abstract void ExtraCreate(T entity);

        void Delete()
        {
            int id = ReadInt("Id: ");


            if (!service.Delete(id))
            {
                Console.WriteLine("Entity not found");
            }
        }

    }
}
