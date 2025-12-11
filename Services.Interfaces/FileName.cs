using Models;

namespace Services.Interfaces
{
    public interface IEntityService
    {
        void Create(Entity entity);
        Entity? Get(int id);
        IEnumerable<Entity> GetAll();
        bool Update(int id, Entity entity);
        bool Delete(int id);
    }
}
