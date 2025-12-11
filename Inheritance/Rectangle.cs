namespace Inheritance
{
    internal class Rectangle : Shape2D
    {
        public Rectangle(int width, int height) : base(nameof(Rectangle), width, height)
        {
        }

        //override - nadpisanie metody abstrakcyjnej z klasy bazowej
        public override float GetArea()
        {
            return Width * Height;
        }
    }
}
