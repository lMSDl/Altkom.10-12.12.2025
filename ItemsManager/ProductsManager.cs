using Models;

namespace ItemsManager
{
    internal class ProductsManager : EntityManager
    {
        protected override Entity CreateEntity()
        {
            return new Product();
        }

        protected override void ExtraCreate(Entity entity)
        {
            Product item = (Product)entity;
            item.Price = ReadFloat("Price: ");
            item.CreatedAt = ReadDate("Date (yyyy-MM-dd hh:mm:ss): ");
        }

        protected override void ExtraEdit(Entity edited, Entity current)
        {
            Product currentProduct = (Product)current;
            Product editedProduct = (Product)edited;
            editedProduct.Price = ReadFloat($"Price ({currentProduct.Price}): ", currentProduct.Price);
            editedProduct.CreatedAt = ReadDate($"Date ({currentProduct.CreatedAt:yyyy-MM-dd hh:mm:ss}): ", currentProduct.CreatedAt);
        }
    }
}
