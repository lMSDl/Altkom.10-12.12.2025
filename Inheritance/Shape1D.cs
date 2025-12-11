namespace Inheritance
{
    internal abstract class Shape1D : Shape
    {
        public int Width { get; } //brak set oznacza readonly

        //base - odwołanie do konstruktora klasy bazowej
        public Shape1D(string name, int width) : base(name)
        {
            Width = width;
        }

        public override string ToString()
        {
            //base - odnosi się do klasy bazowej
            //pobieramy wynik ToString() z klasy bazowej i dodajemy do niego informację o szerokości
            return base.ToString() + $" o długości {Width}";
        }
    }
}
