using System;
using System.Collections.Generic;
using System.Text;

//dziedziczenie to mechanizm, który pozwala na tworzenie nowych klas na podstawie już istniejących klas.
//Klasa, która dziedziczy, nazywana jest klasą pochodną (subclass), a klasa, z której dziedziczy, nazywana jest klasą bazową (superclass).

namespace Inheritance
{
    //każda klasa dziedziczy niejawnie po klasie Object
    // : - oznacza dziedziczenie po wskazanej klasie
    //klasa abstrakcyjna to klasa, której instancji nie możemy utworzyć  (mimo posiadania publicznego konstruktora) - służy ona jako baza dla innych klas
    internal abstract class Shape /*: Object*/
    {
        //protected - modyfikator dostępu pozwalający korzystać typom pochodnym ale na zewnątrz klasy działa jak private
        protected readonly string _name;

        public Shape(string name)
        {
            _name = name;
        }

        //virtual - pozwala zmienić implementację metody w klasach pochodnych (używając override)
        public virtual string GetName()
        {
            return _name;
        }

        //override - przesłonięcie (nadpisanie) metody 
        public override string ToString()
        {
            //base.ToString() - wywołanie metody ToString() z implementacją z klasy bazowej
            //return base.ToString(); //zwraca pełną nazwę klasy, czyli w tym przypadku "Inheritance.Shape"

            return GetName();
        }

    }
}
