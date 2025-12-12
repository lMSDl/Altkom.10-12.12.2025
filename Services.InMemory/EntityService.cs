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
            //jeśli nie użyjemy DefaultIfEmpty(), to w przypadku pustej kolekcji Max rzuci wyjątek
            //więc dodajemy DefaultIfEmpty(), które zwróci 0, jeśli kolekcja jest pusta
            entity.Id = _entities.Select(x => x.Id).DefaultIfEmpty().Max() + 1;

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
            return _entities.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Entity> GetAll()
        {
            //ToList tworzy nową listę z kolekcji _entites
            return _entities.ToList();
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
