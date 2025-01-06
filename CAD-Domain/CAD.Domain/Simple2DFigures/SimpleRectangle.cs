using System.Drawing;
using CAD.Domain.CustomExceptions;

namespace CAD.Domain.Simple2DFigures;

public class SimpleRectangle : ICloneable, IEquatable<SimpleRectangle>
{
    private const double EPSILON = 1e-10;
    
    public SimplePoint Point1 { get; }
    public SimplePoint Point2 { get; }
    public SimplePoint Point3 { get; }
    public SimplePoint Point4 { get; }

    public SimpleRectangle(SimplePoint point1, SimplePoint point2, SimplePoint point3, SimplePoint point4)
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
        Point1 = new SimplePoint(-1, 0, -1);
        Point2 = new SimplePoint(-1, 0, 1);
        Point3 = new SimplePoint(1, 0, -1); 
        Point4 = new SimplePoint(1, 0, 1); 
    }
    
    public object Clone()
    {
        SimplePoint point1 = Point1.Clone() as SimplePoint;
        SimplePoint point2 = Point2.Clone() as SimplePoint;
        SimplePoint point3 = Point3.Clone() as SimplePoint;
        SimplePoint point4 = Point4.Clone() as SimplePoint;
        
        return new SimpleRectangle(point1, point2, point3, point4);
    }

    public bool Equals(SimpleRectangle? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        
        // Collect all points of both triangles into unordered sets
        var thisPoints = new HashSet<SimplePoint> { Point1, Point2, Point3, Point4 };
        var otherPoints = new HashSet<SimplePoint> { other.Point1, other.Point2, other.Point3, other.Point4 };

        // Compare the two sets of points
        return thisPoints.SetEquals(otherPoints);
    }
    
    public override bool Equals(object? obj) =>
        obj is SimpleRectangle other && Equals(other);
    
    public override string ToString() =>
        $"Rectangle:\n  {Point1},\n  {Point2},\n  {Point3},\n  {Point4}";
   
    public override int GetHashCode() => ToString().GetHashCode();
    
    
    private static bool IsValidRectangle(SimplePoint A, SimplePoint B, SimplePoint C, SimplePoint D)
    {
        if (!IsPointsInOnePlane(A, B, C, D))
            return false;
        
        var distances = new List<double>
        {
            SimplePoint.GetDistanceBetweenPoints(A, B),
            SimplePoint.GetDistanceBetweenPoints(A, C),
            SimplePoint.GetDistanceBetweenPoints(A, D),
            SimplePoint.GetDistanceBetweenPoints(B, C),
            SimplePoint.GetDistanceBetweenPoints(B, D),
            SimplePoint.GetDistanceBetweenPoints(C, D)
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
    
  

    public static bool IsPointsInOnePlane(SimplePoint A, SimplePoint B, SimplePoint C, SimplePoint D)
    {
        //calc vectors AB = (x2−x1,y2−y1,z2−z1) and AC = (x3−x1,y3−y1,z3−z1) and AD = (x4−x1,y4−y1,z4−z1)
        //calc vector product AB×AC.
        //calc scalar product (AB×AC)⋅AD
        //if scalar product == 0 => return that 4 points are in one plane
        
        var AB = new SimplePoint(B.X - A.X, B.Y - A.Y, B.Z - A.Z);
        var AC = new SimplePoint(C.X - A.X, C.Y - A.Y, C.Z - A.Z);
        var AD = new SimplePoint(D.X - A.X, D.Y - A.Y, D.Z - A.Z);
        
        var crossProduct = new SimplePoint(
            AB.Y * AC.Z - AB.Z * AC.Y,
            AB.Z * AC.X - AB.X * AC.Z,
            AB.X * AC.Y - AB.Y * AC.X
        );
        
        var dotProduct = DotProduct(crossProduct, AD);
        
        //return dotProduct == 0;
        return Math.Abs(dotProduct) < 1e-9;
    }

    private static double DotProduct(SimplePoint p1, SimplePoint p2) => 
        p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
}