

using Inheritance;

//nie możemy utworzyć instancji klasy Shape, ponieważ jest klasą abstrakcyjną
//Shape shape = new Shape("a");
//Console.WriteLine(shape.ToString());


//nie można utworzyć instancji klasy abstrakcyjnej
//Shape2D shape2D = new Shape2D("a", 1, 2);
Shape2D shape2D = new Rectangle(10, 20);

//przedstawienie obiektu jako typ klasy bazowej
//jest to tzw. polimorfizm
//takie działanie ogranicza dostęp do metod i właściwości klasy pochodnej do tych zdefiniowanych w klasie bazowej
Shape1D shape1D = new Circle(32);


List<Shape> shapes = [
    new Rectangle(10, 20),
    new Circle(15),
    new Segment(5),
];
foreach (Shape s in shapes)
{
    Console.WriteLine(s.ToString());
}

List<Shape2D> shapes2D = [
    new Rectangle(10, 20),
    new Circle(15),
];
foreach (Shape2D s in shapes2D)
{
    Console.WriteLine($"{s.ToString()} - Area: {s.GetArea()}");
}


/*List<Shape2D> shapes = [
    new Rectangle(10, 20),
    new Circle(15),
    new Segment(5), //Błąd kompilacji - Segment nie dziedziczy po Shape2D
];*/