using CAD.Domain.CustomExceptions;

namespace CAD.Domain.Simple2DFigures;

public class SimpleTriangle : ICloneable, IEquatable<SimpleTriangle>
{
    public SimplePoint Point1 { get; }
    public SimplePoint Point2 { get; }
    public SimplePoint Point3 { get; }
    
    public SimpleTriangle(SimplePoint point1, SimplePoint point2, SimplePoint point3)
    {
        Point1 = point1 ?? throw new ArgumentNullException(nameof(point1));
        Point2 = point2 ?? throw new ArgumentNullException(nameof(point2));
        Point3 = point3 ?? throw new ArgumentNullException(nameof(point3));
        
        if (!IsValidTriangle(Point1, Point2, Point3))
            throw new InvalidTriangleException(Point1, Point2, Point3);

    }
    
    private static bool IsValidTriangle(SimplePoint p1, SimplePoint p2, SimplePoint p3)
    {
        double abX = p2.X - p1.X;
        double abY = p2.Y - p1.Y;
        double abZ = p2.Z - p1.Z;

        double acX = p3.X - p1.X;
        double acY = p3.Y - p1.Y;
        double acZ = p3.Z - p1.Z;

        double crossX = abY * acZ - abZ * acY;
        double crossY = abZ * acX - abX * acZ;
        double crossZ = abX * acY - abY * acX;

        double crossProductMagnitude = Math.Sqrt(crossX * crossX + crossY * crossY + crossZ * crossZ);

        return crossProductMagnitude > 1e-10;
    }
    
    public SimpleTriangle()
    {
        Point1 = new SimplePoint(Math.Cos(0), 0, Math.Sin(0));  
        Point2 = new SimplePoint(Math.Cos(2 * Math.PI / 3), 0, Math.Sin(2 * Math.PI / 3));
        Point3 = new SimplePoint(Math.Cos(4 * Math.PI / 3), 0, Math.Sin(4 * Math.PI / 3));
    }

    public object Clone()
    {
        SimplePoint point1 = Point1.Clone() as SimplePoint;
        SimplePoint point2 = Point2.Clone() as SimplePoint;
        SimplePoint point3 = Point3.Clone() as SimplePoint;
        
        return new SimpleTriangle(point1, point2, point3);
    }

    public bool Equals(SimpleTriangle? other)
    {
        throw new NotImplementedException();
    }
}