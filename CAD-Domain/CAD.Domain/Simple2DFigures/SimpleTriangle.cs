using CAD.Domain.CustomExceptions;

namespace CAD.Domain.Simple2DFigures;

public class SimpleTriangle : ICloneable, IEquatable<SimpleTriangle>
{
    public Simple3DPoint Point1 { get; }
    public Simple3DPoint Point2 { get; }
    public Simple3DPoint Point3 { get; }
    
    public SimpleTriangle(Simple3DPoint point1, Simple3DPoint point2, Simple3DPoint point3)
    {
        _ = point1 ?? throw new ArgumentNullException(nameof(point1));
        _ = point2 ?? throw new ArgumentNullException(nameof(point2));
        _ = point3 ?? throw new ArgumentNullException(nameof(point3));
        
        if (!IsValidTriangle(point1, point2, point3))
            throw new InvalidTriangleException(point1, point2, point3);

        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
    }
    
    private static bool IsValidTriangle(Simple3DPoint p1, Simple3DPoint p2, Simple3DPoint p3)
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
        Point1 = new Simple3DPoint(Math.Cos(0), 0, Math.Sin(0));  
        Point2 = new Simple3DPoint(Math.Cos(2 * Math.PI / 3), 0, Math.Sin(2 * Math.PI / 3));
        Point3 = new Simple3DPoint(Math.Cos(4 * Math.PI / 3), 0, Math.Sin(4 * Math.PI / 3));
    }

    public object Clone()
    {
        Simple3DPoint point1 = Point1.Clone() as Simple3DPoint;
        Simple3DPoint point2 = Point2.Clone() as Simple3DPoint;
        Simple3DPoint point3 = Point3.Clone() as Simple3DPoint;
        
        return new SimpleTriangle(point1, point2, point3);
    }

    public bool Equals(SimpleTriangle? other)
    {
        throw new NotImplementedException();
    }
}