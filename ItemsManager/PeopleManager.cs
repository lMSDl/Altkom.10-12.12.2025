using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemsManager
{
    internal class PeopleManager : EntityManager
    {
        protected override Entity CreateEntity()
        {
            return new Person();
        }

        protected override void ExtraCreate(Entity entity)
        {
            Person item = (Person)entity;
            item.BirthDate = ReadDate("Birth Date (yyyy-MM-dd hh:mm:ss): ");
        }

        protected override void ExtraEdit(Entity entity, Entity item)
        {
            Person currentPerson = (Person)item;
            Person editedPerson = (Person)entity;
            editedPerson.BirthDate = ReadDate($"Birth Date ({currentPerson.BirthDate:yyyy-MM-dd hh:mm:ss}): ", currentPerson.BirthDate);
        }
    }
}
