using System.Drawing;
using CAD.Domain.CustomExceptions;

namespace CAD.Domain.Simple2DFigures;

public class SimpleRectangle : ICloneable, IEquatable<SimpleRectangle>
{
    private const double EPSILON = 1e-10;
    
    public Simple3DPoint Point1 { get; }
    public Simple3DPoint Point2 { get; }
    public Simple3DPoint Point3 { get; }
    public Simple3DPoint Point4 { get; }

    public SimpleRectangle(Simple3DPoint point1, Simple3DPoint point2, Simple3DPoint point3, Simple3DPoint point4)
    {
        _ = point1 ?? throw new ArgumentNullException(nameof(point1));
        _ = point2 ?? throw new ArgumentNullException(nameof(point2));
        _ = point3 ?? throw new ArgumentNullException(nameof(point3));
        _ = point4 ?? throw new ArgumentNullException(nameof(point4));
        
        if (!IsValidRectangle(Point1, Point2, Point3, Point4))
            throw new InvalidRectangleException(Point1, Point2, Point3, Point4);
        
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
        Point4 = point4;
    }
    
    public SimpleRectangle()
    {
        Point1 = new Simple3DPoint(-1, 0, -1);
        Point2 = new Simple3DPoint(-1, 0, 1);
        Point3 = new Simple3DPoint(1, 0, -1); 
        Point4 = new Simple3DPoint(1, 0, 1); 
    }
    
    public object Clone()
    {
        Simple3DPoint point1 = Point1.Clone() as Simple3DPoint;
        Simple3DPoint point2 = Point2.Clone() as Simple3DPoint;
        Simple3DPoint point3 = Point3.Clone() as Simple3DPoint;
        Simple3DPoint point4 = Point4.Clone() as Simple3DPoint;
        
        return new SimpleRectangle(point1, point2, point3, point4);
    }

    public bool Equals(SimpleRectangle? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        
        // Collect all points of both triangles into unordered sets
        var thisPoints = new HashSet<Simple3DPoint> { Point1, Point2, Point3, Point4 };
        var otherPoints = new HashSet<Simple3DPoint> { other.Point1, other.Point2, other.Point3, other.Point4 };

        // Compare the two sets of points
        return thisPoints.SetEquals(otherPoints);
    }
    
    public override bool Equals(object? obj) =>
        obj is SimpleRectangle other && Equals(other);
    
    public override string ToString() =>
        $"Rectangle:\n  {Point1},\n  {Point2},\n  {Point3},\n  {Point4}";
   
    public override int GetHashCode() => ToString().GetHashCode();
    
    
    private static bool IsValidRectangle(Simple3DPoint A, Simple3DPoint B, Simple3DPoint C, Simple3DPoint D)
    {
        if (!IsPointsInOnePlane(A, B, C, D))
            return false;
        
        var distances = new List<double>
        {
            Simple3DPoint.GetDistanceBetweenPoints(A, B),
            Simple3DPoint.GetDistanceBetweenPoints(A, C),
            Simple3DPoint.GetDistanceBetweenPoints(A, D),
            Simple3DPoint.GetDistanceBetweenPoints(B, C),
            Simple3DPoint.GetDistanceBetweenPoints(B, D),
            Simple3DPoint.GetDistanceBetweenPoints(C, D)
        };
        distances.Sort();
        
        bool areSidesValid = 
            Math.Abs(distances[0] - distances[1]) < EPSILON && // side1 == side2
            Math.Abs(distances[2] - distances[3]) < EPSILON && // side3 == side4
            Math.Abs(distances[4] - distances[5]) < EPSILON && // diag1 == diag2
            distances[^1] > distances[0];

        if (!areSidesValid) return false;
        
        return true;
    }
    
  

    public static bool IsPointsInOnePlane(Simple3DPoint A, Simple3DPoint B, Simple3DPoint C, Simple3DPoint D)
    {
        //calc vectors AB = (x2−x1,y2−y1,z2−z1) and AC = (x3−x1,y3−y1,z3−z1) and AD = (x4−x1,y4−y1,z4−z1)
        //calc vector product AB×AC.
        //calc scalar product (AB×AC)⋅AD
        //if scalar product == 0 => return that 4 points are in one plane
        
        var AB = new Simple3DPoint(B.X - A.X, B.Y - A.Y, B.Z - A.Z);
        var AC = new Simple3DPoint(C.X - A.X, C.Y - A.Y, C.Z - A.Z);
        var AD = new Simple3DPoint(D.X - A.X, D.Y - A.Y, D.Z - A.Z);
        
        var crossProduct = new Simple3DPoint(
            AB.Y * AC.Z - AB.Z * AC.Y,
            AB.Z * AC.X - AB.X * AC.Z,
            AB.X * AC.Y - AB.Y * AC.X
        );
        
        var dotProduct = DotProduct(crossProduct, AD);
        
        //return dotProduct == 0;
        return Math.Abs(dotProduct) < 1e-9;
    }

    private static double DotProduct(Simple3DPoint p1, Simple3DPoint p2) => 
        p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
}