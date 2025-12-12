using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemsManager
{
    internal class PeopleManager : EntityManager<Person>
    {
        public PeopleManager(string filePath) : base(filePath)
        {
        }

        protected override Person CreateEntity()
        {
            return new Person();
        }

        protected override void ExtraCreate(Person entity)
        {
            entity.BirthDate = ReadDate("Birth Date (yyyy-MM-dd hh:mm:ss): ");
        }

        protected override void ExtraEdit(Person entity, Person item)
        {
            entity.BirthDate = ReadDate($"Birth Date ({item.BirthDate:yyyy-MM-dd hh:mm:ss}): ", item.BirthDate);
        }
    }
}
