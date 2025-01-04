namespace CAD.Domain;

public class Simple3DPoint : IEquatable<Simple3DPoint>, ICloneable
{
    private const double EPSILON = 1e-10; // 0.0000000001
    
    public double X { get; }
    public double Y { get; }
    public double Z { get; }
    
    public Simple3DPoint(double x, double y, double z) {
        X = x;
        Y = y;
        Z = z;
    }
    
    public Simple3DPoint() : this(0, 0, 0) {}
    
    public override string ToString() => $"({X}, {Y}, {Z})";
    public override int GetHashCode() => ToString().GetHashCode();
    public object Clone() => new Simple3DPoint(X, Y, Z);
    
    public bool Equals(Simple3DPoint? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        
        return Math.Abs(X - other.X) < EPSILON && 
               Math.Abs(Y - other.Y) < EPSILON && 
               Math.Abs(Z - other.Z) < EPSILON;
    }

    public override bool Equals(object? obj) =>
        obj is Simple3DPoint other && Equals(other);
    
    public static double GetDistanceBetweenPoints(Simple3DPoint p1, Simple3DPoint p2)
    {
        return Math.Sqrt(
            Math.Pow(p2.X - p1.X, 2) +
            Math.Pow(p2.Y - p1.Y, 2) +
            Math.Pow(p2.Z - p1.Z, 2)
        );
    }
}