using Models;

namespace Services.Interfaces
{
    //interface - umowa, którą musi spełniać każda klasa ją implementująca
    //klasa musi mieć wszystkie metody zadeklarowane w interfejsie i/lub właściwości
    //interfejsy są używane do definiowania zachowań, które mogą być implementowane przez różne klasy
    //wszystkie metody w interfejsie są domyślnie publiczne i abstrakcyjne (nie mają ciała)
    //konwencja nazywania interfejsów w C# zaczyna się od litery "I"
    public interface IProductsService
    {
        void Create(Product entity);
        Product? Get(int id);
        //IEnumerable - podstawowy interfejs dla kolekcji, który nie ma żadnych metod do dodawania lub usuwania elementów, a jedynie umożliwia iterację po elementach
        //IEnumerable<T> jest generycznym interfejsem, który pozwala na iterację po kolekcji elementów typu T. W naszym przypadku T to Product
        IEnumerable<Product> GetAll();
        bool Update(int id, Product entity);
        bool Delete(int id);
    }
}
