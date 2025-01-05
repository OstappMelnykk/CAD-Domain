
namespace CAD.Domain.ComplexFigures;

public class ComplexPoint : ICloneable, IEquatable<ComplexPoint>
{
    public Simple3DPoint SimplePoint { get; }
    public uint GlobalId { get; set; }
    public List<byte> LocalIds { get; }

    public ComplexPoint(Simple3DPoint simplePoint, uint globalId, List<byte>? localIds)
    {
        SimplePoint = simplePoint ?? throw new ArgumentNullException(nameof(simplePoint));
        GlobalId = globalId;
        LocalIds = localIds ?? new List<byte>();
    }
    
    public ComplexPoint(Simple3DPoint simplePoint) : this(simplePoint, 0, null) { } 
    public ComplexPoint() : this(new Simple3DPoint(), 0, null) { }

    
    public override string ToString()=> 
        SimplePoint + $"\tGlobalId: {GlobalId}\tLocalIds: {{{string.Join(", ", LocalIds)}}}";

    public override int GetHashCode() => ToString().GetHashCode();
    
    
    public object Clone()
    {
        Simple3DPoint simplePoint = (Simple3DPoint) SimplePoint.Clone();
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
}