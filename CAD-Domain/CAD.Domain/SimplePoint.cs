namespace CAD.Domain;

public class SimplePoint : IEquatable<SimplePoint>, ICloneable
{
    private const double EPSILON = 1e-10; // 0.0000000001
    
    public double X { get; }
    public double Y { get; }
    public double Z { get; }
    
    public SimplePoint(double x, double y, double z) {
        
        X = Math.Round(x, 3);
        Y = Math.Round(y, 3); 
        Z = Math.Round(z, 3);
    }
    
    public SimplePoint() : this(0, 0, 0) {}
    
    public override string ToString() => $"({X}, {Y}, {Z})";
    public override int GetHashCode() => ToString().GetHashCode();
    public object Clone() => new SimplePoint(X, Y, Z);
    
    public bool Equals(SimplePoint? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        
        return Math.Abs(X - other.X) < EPSILON && 
               Math.Abs(Y - other.Y) < EPSILON && 
               Math.Abs(Z - other.Z) < EPSILON;
    }

    public override bool Equals(object? obj) =>
        obj is SimplePoint other && Equals(other);
    
    public static double GetDistanceBetweenPoints(SimplePoint p1, SimplePoint p2)
    {
        return Math.Sqrt(
            Math.Pow(p2.X - p1.X, 2) +
            Math.Pow(p2.Y - p1.Y, 2) +
            Math.Pow(p2.Z - p1.Z, 2)
        );
    }

    public static SimplePoint GetMidPointBetweenPoints(SimplePoint p1, SimplePoint p2)
    {
        _ = p1 ?? throw new ArgumentNullException(nameof(p1));
        _ = p2 ?? throw new ArgumentNullException(nameof(p2));
        
        double midX = (p1.X + p2.X) / 2.0;
        double midY = (p1.Y + p2.Y) / 2.0;
        double midZ = (p1.Z + p2.Z) / 2.0;

        return new SimplePoint(midX, midY, midZ);
    }
}