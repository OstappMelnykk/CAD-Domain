using System.Drawing;
using CAD.Domain.CustomExceptions;

namespace CAD.Domain.Simple2DFigures;

public class SimpleLine : IEquatable<SimpleLine>, ICloneable
{
    public Simple3DPoint Point1 { get; }
    public Simple3DPoint Point2 { get; }

    public SimpleLine(Simple3DPoint point1, Simple3DPoint point2)
    {
        _ = point1 ?? throw new ArgumentNullException(nameof(point1));
        _ = point2 ?? throw new ArgumentNullException(nameof(point2));

        if (point1.Equals(point2))
            throw new InvalidLineException(point1, point2);
        
        Point1 = point1; 
        Point2 = point2;
    }
    
    public SimpleLine() : this (new Simple3DPoint(-1, 0, 0), new Simple3DPoint(1, 0, 0)) {}

    public bool Equals(SimpleLine? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        
        var thisPoints = new HashSet<Simple3DPoint> { Point1, Point2 };
        var otherPoints = new HashSet<Simple3DPoint> { other.Point1, other.Point2 };
        
        return thisPoints.SetEquals(otherPoints);
    }
    
    public override bool Equals(object? obj) =>
        obj is SimpleLine other && Equals(other);

    public object Clone()
    {
        Simple3DPoint point1 = Point1.Clone() as Simple3DPoint;
        Simple3DPoint point2 = Point2.Clone() as Simple3DPoint;
        
        return new SimpleLine(point1, point2);
    }
    
    public override string ToString() => $"Line:\n  {Point1},\n  {Point2}";
    public override int GetHashCode() => ToString().GetHashCode();
}