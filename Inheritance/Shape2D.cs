namespace Inheritance
{
    internal abstract class Shape2D : Shape1D
    {
        public int Height { get; }
        public Shape2D(string name, int width, int height) : base(name, width)
        {
            Height = height;
        }

        //nadpisujemy metodę wirtualną z klasy bazowej
        public override string GetName()
        {
            return base.GetName().ToUpper();
        }

        public override string ToString()
        {
            return base.ToString() + $" i wysokości {Height}";
        }

        //metoda abstrakcyjna - nie ma implementacji w klasie bazowej, musi być zaimplementowana w klasach pochodnych
        //metoda abstrakcyjna musi znajdować się w klasie abstrakcyjnej
        public abstract float GetArea();
    }
}
