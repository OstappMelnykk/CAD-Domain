
namespace CAD.Domain.ComplexFigures;

public class ComplexPoint : ICloneable, IEquatable<ComplexPoint>
{
    public SimplePoint SimplePoint { get; }
    public uint GlobalId { get; set; }
    public List<byte> LocalIds { get; }

    public ComplexPoint(SimplePoint simplePoint, uint globalId, List<byte>? localIds)
    {
        SimplePoint = simplePoint ?? throw new ArgumentNullException(nameof(simplePoint));
        GlobalId = globalId;
        LocalIds = localIds ?? new List<byte>();
    }
    
    public ComplexPoint(SimplePoint simplePoint) : this(simplePoint, 0, null) { } 
    public ComplexPoint() : this(new SimplePoint(), 0, null) { }

    
    public override string ToString()=> 
        SimplePoint + $"\tGlobalId: {GlobalId}\tLocalIds: {{{string.Join(", ", LocalIds)}}}";

    public override int GetHashCode() => ToString().GetHashCode();
    
    
    public object Clone()
    {
        SimplePoint simplePoint = (SimplePoint) SimplePoint.Clone();
        uint globalId = GlobalId;
        List<byte> localIds = new List<byte>(LocalIds);
        
        return new ComplexPoint(simplePoint, globalId, localIds);
    }
    
    public bool Equals(ComplexPoint? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true; 
        
        return SimplePoint.Equals(other.SimplePoint);
    }
    
    public override bool Equals(object? obj) =>
        obj is ComplexPoint other && Equals(other);
    
    
    
    public static explicit operator SimplePoint(ComplexPoint complexPoint)
    {
        if (complexPoint == null)
        {
            throw new ArgumentNullException(nameof(complexPoint), "ComplexPoint cannot be null.");
        }

        return complexPoint.SimplePoint;
    }
}