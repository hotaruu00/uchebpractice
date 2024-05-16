using System;

public class Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
}

public class Polygon
{
    private Point[] vertices;

    public Polygon(Point[] vertices)
    {
        this.vertices = vertices;
    }

    // Метод для вычисления площади многоугольника
    public double GetArea()
    {
        double area = 0;
        int j = vertices.Length - 1;

        for (int i = 0; i < vertices.Length; i++)
        {
            area += (vertices[j].X + vertices[i].X) * (vertices[j].Y - vertices[i].Y);
            j = i;
        }

        return Math.Abs(area / 2);
    }

    // Метод для проверки принадлежности точки многоугольнику
    public bool ContainsPoint(Point point)
    {
        int n = vertices.Length;
        bool result = false;

        for (int i = 0, j = n - 1; i < n; j = i++)
        {
            if (((vertices[i].Y > point.Y) != (vertices[j].Y > point.Y)) &&
                (point.X < (vertices[j].X - vertices[i].X) * (point.Y - vertices[i].Y) / (vertices[j].Y - vertices[i].Y) + vertices[i].X))
            {
                result = !result;
            }
        }

        return result;
    }

    // Метод для определения, является ли многоугольник выпуклым
    public bool IsConvex()
    {
        bool isPositive = false;
        bool isNegative = false;
        int n = vertices.Length;

        for (int i = 0; i < n; i++)
        {
            int dx1 = (int)(vertices[(i + 2) % n].X - vertices[(i + 1) % n].X);
            int dy1 = (int)(vertices[(i + 2) % n].Y - vertices[(i + 1) % n].Y);
            int dx2 = (int)(vertices[i].X - vertices[(i + 1) % n].X);
            int dy2 = (int)(vertices[i].Y - vertices[(i + 1) % n].Y);
            int crossProduct = dx1 * dy2 - dy1 * dx2;

            if (crossProduct > 0)
            {
                isPositive = true;
            }
            else if (crossProduct < 0)
            {
                isNegative = true;
            }

            if (isPositive && isNegative)
            {
                return false;
            }
        }

        return true;
    }
}

class Program
{
    static void Main()
    {
        // Создание массива вершин многоугольника
        Point[] vertices = new Point[]
        {
            new Point(4, 4),
            new Point(4, 8),
            new Point(8, 8),
            new Point(8, 4)
        };

        // Создание объекта многоугольника
        Polygon polygon = new Polygon(vertices);

        // Вызов методов для демонстрации их работы
        Console.WriteLine("Area of the polygon: " + polygon.GetArea());
        Console.WriteLine("Is the polygon convex? " + polygon.IsConvex());

        // Проверка принадлежности точки многоугольнику
        Point testPoint = new Point(6, 6);
        Console.WriteLine("Does the polygon contain the point? " + polygon.ContainsPoint(testPoint));
    }
}