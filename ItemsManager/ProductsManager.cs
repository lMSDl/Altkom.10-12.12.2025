using Models;

namespace ItemsManager
{
    internal class ProductsManager : EntityManager<Product>
    {
        public ProductsManager(string filePath) : base(filePath)
        {
            service.Create(new Product { Name = "Laptop", Price = 3500.00f, CreatedAt = DateTime.Now.AddDays(-10) });
            service.Create(new Product { Name = "Smartphone", Price = 2000.00f, CreatedAt = DateTime.Now.AddDays(-5) });
            service.Create(new Product { Name = "Tablet", Price = 1500.00f, CreatedAt = DateTime.Now.AddDays(-2) });
        }

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
