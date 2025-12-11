namespace Inheritance
{
    internal class Circle : Shape2D
    {
        public float Radius { get; }
        public Circle(float radius) : base(nameof(Circle), (int)radius * 2, (int)radius * 2)
        {
            Radius = radius;
        }

        //override - nadpisanie metody abstrakcyjnej z klasy bazowej
        public override float GetArea()
        {
            return (float)(Math.PI * Radius * Radius);
        }

        public override string ToString()
        {
            return $"{_name} o promieniu {Radius}";
        }
    }
}
