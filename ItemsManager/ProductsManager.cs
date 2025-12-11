using Models;

namespace ItemsManager
{
    internal class ProductsManager : EntityManager<Product>
    {
        protected override Product CreateEntity()
        {
            return new Product();
        }

        protected override void ExtraCreate(Product entity)
        {
            entity.Price = ReadFloat("Price: ");
            entity.CreatedAt = ReadDate("Date (yyyy-MM-dd hh:mm:ss): ");
        }

        protected override void ExtraEdit(Product edited, Product current)
        {
            edited.Price = ReadFloat($"Price ({current.Price}): ", current.Price);
            edited.CreatedAt = ReadDate($"Date ({current.CreatedAt:yyyy-MM-dd hh:mm:ss}): ", current.CreatedAt);
        }
    }
}
