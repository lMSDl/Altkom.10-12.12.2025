using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemsManager
{
    internal class ToDoManager : EntityManager<ToDo>
    {
        public ToDoManager(string filePath) : base(filePath)
        {
        }

        protected override ToDo CreateEntity()
        {
            return new ToDo();
        }
        protected override void ExtraCreate(ToDo entity)
        {
            entity.DueDate = ReadDate("Due Date: ");
        }
        protected override void ExtraEdit(ToDo edited, ToDo current)
        {
            edited.DueDate = ReadDate($"Due Date ({current.DueDate.ToShortDateString()}): ", current.DueDate);
        }

        protected override DateTime ReadDate(string label, DateTime? defaultValue = null)
        {
            Console.Write(label);
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) && defaultValue.HasValue)
            {
                return defaultValue.Value;
            }

            DateTime date;
            try
            {
                date = DateTime.Parse(input ?? string.Empty);
                return date;
            }
            catch (FormatException) //instancja wyjątku nie jest nam potrzebna, więc nie musimy deklarować nazywanej zmiennej, która będzie przechowywać wyjątek
            {
                Console.WriteLine("Invalid date format");
                return ReadDate(label);
            }
        }
    }
}
