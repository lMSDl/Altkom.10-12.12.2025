using Models;
using Services.InMemory;
using Services.Interfaces;

namespace ItemsManager
{
    // <T> - oznacza, że klasa jest generyczna i może być używana z różnymi typami danych
    //T jest parametrem typu, który będzie używany w klasie
    //where T : Entity - oznacza, że klasa generyczna T musi być typu Entity lub jej pochodną
    internal abstract class EntityManager<T> where T : Entity
    {
        IEntityService service = new EntityService();

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                //Console.WriteLine(service.GetAll().Select(x => x.ToString()).Aggregate((acc, text) => $"{acc}\n{text}"));
                Console.WriteLine(string.Join("\n", service.GetAll().Select(x => x.ToString())));

                Console.WriteLine("Commands: create, edit, delete, exit");
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
