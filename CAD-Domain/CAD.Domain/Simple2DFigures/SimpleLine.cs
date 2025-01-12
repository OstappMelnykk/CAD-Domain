﻿using System.Drawing;
using System.Text.Json;
using CAD.Domain.CustomExceptions;

namespace CAD.Domain.Simple2DFigures;

public class SimpleLine : IEquatable<SimpleLine>, ICloneable
{
    public SimplePoint Point1 { get; }
    public SimplePoint Point2 { get; }

    public SimpleLine(SimplePoint point1, SimplePoint point2)
    {
        Point1 = point1 ?? throw new ArgumentNullException(nameof(point1));
        Point2 = point2 ?? throw new ArgumentNullException(nameof(point2));

        if (Point1.Equals(Point2))
            throw new InvalidLineException(Point1, Point2);
        
    }
    
    public SimpleLine() : this (new SimplePoint(-1, 0, 0), new SimplePoint(1, 0, 0)) {}

    public bool Equals(SimpleLine? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        
        var thisPoints = new HashSet<SimplePoint> { Point1, Point2 };
        var otherPoints = new HashSet<SimplePoint> { other.Point1, other.Point2 };
        
        return thisPoints.SetEquals(otherPoints);
    }
    
    public override bool Equals(object? obj) =>
        obj is SimpleLine other && Equals(other);

    public object Clone()
    {
        SimplePoint point1 = Point1.Clone() as SimplePoint;
        SimplePoint point2 = Point2.Clone() as SimplePoint;
        
        return new SimpleLine(point1, point2);
    }
    
    public override string ToString() => $"Line:\n  {Point1},\n  {Point2}";
    public override int GetHashCode() => ToString().GetHashCode();
    
    
    public string GetJsonSerializedCube()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
    }
}