using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
    }

    internal class LINQ
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Anna Nowak", Department = "HR", Age = 34, Salary = 6200 },
            new Employee { Id = 2, Name = "Tomasz Kowalski", Department = "IT", Age = 28, Salary = 8700 },
            new Employee { Id = 3, Name = "Marta Wiśniewska", Department = "IT", Age = 41, Salary = 9100 },
            new Employee { Id = 4, Name = "Paweł Zieliński", Department = "Sales", Age = 38, Salary = 6700 },
            new Employee { Id = 5, Name = "Katarzyna Krawczyk", Department = "Sales", Age = 25, Salary = 5100 },
            new Employee { Id = 6, Name = "Jan Maj", Department = "HR", Age = 31, Salary = 6000 },
            new Employee { Id = 7, Name = "Ewa Tomaszewska", Department = "Marketing", Age = 29, Salary = 5800 }
        };

        public static void Exercises()
        {
            //1.Pobierz nazwiska pracowników z działu IT, którzy zarabiają powyżej średniej pensji wszystkich pracowników, posortowanych malejąco po pensji.
            //2.Zwróć trzech najstarszych pracowników z działów HR lub Sales, posortowanych rosnąco po imieniu i zwróć tylko pola Name i Age.
            //3.Pobierz pracowników młodszych niż 35 lat, których pensja jest większa niż 6000, następnie posortuj ich najpierw po Department, a potem po Salary malejąco.
            //4.Pobierz pracowników, których nazwisko kończy się na „ski” lub „ska”, następnie posortuj ich malejąco po Salary i zwróć tylko Name i Salary.
            //5.Zwróć listę pracowników, których imię zaczyna się od liter od A do M, posortowanych rosnąco po Age i zwróć tylko Name, Age oraz Department.

        }
    }
}
