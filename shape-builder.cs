
Console.WriteLine("Welcome!!!");
double radiusCircle;

do
{
    Console.Write("Enter the radius of the circle: ");
} while (!double.TryParse(Console.ReadLine(), out radiusCircle));
double widthRect;

do
{
    Console.Write("Enter the width of the  rectangle: ");
} while (!double.TryParse(Console.ReadLine(), out widthRect));

double heightRect;

do
{
    Console.Write("Enter the height of the  rectangle:  ");
} while (!double.TryParse(Console.ReadLine(), out heightRect));
double baseTriangle;

do
{
    Console.Write("Enter the base of the triangle: ");
} while (!double.TryParse(Console.ReadLine(), out baseTriangle));
double heightTriangle;

do
{
    Console.Write("Enter the height of the triangle: ");
} while (!double.TryParse(Console.ReadLine(), out heightTriangle));



Circle circle = new Circle { Name = "Circle", Radius = radiusCircle };
Rectangle rectangle = new Rectangle { Name = "Rectangle", Width = widthRect, Height = heightRect };
Triangle triangle = new Triangle { Name = "Triangle", Base = baseTriangle, Height = heightTriangle };

PrintShapeArea(circle);
PrintShapeArea(rectangle);
PrintShapeArea(triangle);
static void PrintShapeArea(Shape shape)
{
    Console.WriteLine($"Shape: {shape.Name}, Area: {shape.CalculateArea()}");
};

public class Shape
{
    public string Name { get; set; }


    public virtual double CalculateArea()
    {
        return 0;
    }
}


public class Circle : Shape
{
    public double Radius { get; set; }

    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public override double CalculateArea()
    {
        return Width * Height;
    }
}


public class Triangle : Shape
{
    public double Base { get; set; }
    public double Height { get; set; }

    public override double CalculateArea()
    {
        return Base * Height * 0.5;
    }
}
