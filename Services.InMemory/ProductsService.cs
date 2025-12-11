using Models;
using Services.Interfaces;

namespace Services.InMemory
{
    // implementacja interfejsu odbywa się poprzez dodanie dwukropka po nazwie klasy
    //klasa może implementować wiele interfejsów, wymieniając je po przecinku
    public class ProductsService : IProductsService
    {
        //private IEnumerable<Product> _entities; // IEnumerable nie pozwala na dodawanie/usuwanie elementów, dlatego używamy ICollection
        private readonly ICollection<Product> _entities;

        public ProductsService()
        {
            //_entities = new List<Product>();
            _entities = []; // skrócona inicjalizacja kolekcji w C#
        }

        public void Create(Product entity)
        {
            int maxId = 0;
            //szukamy największego id w kolekcji
            foreach (Product product in _entities)
            {
                if(product.Id > maxId)
                {
                    maxId = product.Id;
                }
            }

            entity.Id = maxId + 1; // ustawiamy id jako największe id + 1

            //IEnumerable było niewystarczającym interfejsem, ponieważ nie pozwalało na dodawanie elementów do kolekcji
            //dlatego używamy ICollection, który jest bardziej rozbudowanym interfejsem
            _entities.Add(entity); // dodajemy encję do kolekcji
        }

        public bool Delete(int id)
        {
            Product? entityToDelete = Get(id);
            if(entityToDelete is not null)
            {
                //usuwamy element z kolekcji
                //ICollection ma metodę Remove, która usuwa element z kolekcji
                _entities.Remove(entityToDelete);
                return true;
            }
            return false;
        }

        public Product? Get(int id)
        {
            Product? product = null;
            foreach(Product entity in _entities)
            {
                if(entity.Id == id)
                {
                    product = entity;
                    break;
                }
            }

            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            //return _entities; //zwracamy całą kolekcję produktów, narusza to hermetyzację, ponieważ dajemy dostęp do całej kolekcji
            return new List<Product>(_entities); //zwracamy kopię kolekcji, aby nie dawać dostępu do oryginalnej kolekcji
        }

        public bool Update(int id, Product entity)
        {            
            //jeśli udało się usunąć element o podanym id, to znaczy, że element istniał na liście
            //więc możemy dodać nowy (zmodyfikowany) element z tym samym id
            if (Delete(id) == true)
            {
                entity.Id = id; // Ustawiamy ID na podstawie przekazanego id
                _entities.Add(entity); // Dodajemy zaktualizowany produkt do kolekcji
                return true;
            }
            return false; // Jeśli nie udało się usunąć, zwracamy false
        }
    }
}
