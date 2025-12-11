using Models;
using Services.Interfaces;

namespace Services.InMemory
{
    public class EntityService : IEntityService
    {
        private readonly ICollection<Entity> _entities;

        public EntityService()
        {
            _entities = [];
        }

        public void Create(Entity entity)
        {
            int maxId = 0;
            //szukamy największego id w kolekcji
            foreach (Entity e in _entities)
            {
                if(e.Id > maxId)
                {
                    maxId = e.Id;
                }
            }

            entity.Id = maxId + 1; 

            _entities.Add(entity);
        }

        public bool Delete(int id)
        {
            Entity? entityToDelete = Get(id);
            if(entityToDelete is not null)
            {
                _entities.Remove(entityToDelete);
                return true;
            }
            return false;
        }

        public Entity? Get(int id)
        {
            Entity? product = null;
            foreach(Entity entity in _entities)
            {
                if(entity.Id == id)
                {
                    product = entity;
                    break;
                }
            }

            return product;
        }

        public IEnumerable<Entity> GetAll()
        {
            return new List<Entity>(_entities);
        }

        public bool Update(int id, Entity entity)
        {            
            if (Delete(id) == true)
            {
                entity.Id = id; 
                _entities.Add(entity); 
                return true;
            }
            return false;
        }
    }
}
