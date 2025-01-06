using CAD.Domain.ComplexFigures;
using CAD.Domain.SimpleServices;

namespace CAD.Domain;

public class SimpleCube : ICloneable, IEquatable<SimpleCube>
{
    public List<ComplexPoint> ComplexPoints { get; }
    
    public SimpleCube(List<ComplexPoint> complexPoints)
    {
        _ = complexPoints ?? throw new ArgumentNullException(nameof(complexPoints));
        if (complexPoints.Count != 8) throw new ArgumentException("The list of points must contain 8 points.");

        ComplexPoints = complexPoints;
        SortingService.GlobalSorting(ComplexPoints);
    }

    public SimpleCube() : this(DefaultCube.GetDefaultCube8Points()) {}
    
    public object Clone()
    { 
        List<ComplexPoint> deepCopiedList = ComplexPoints.DeepClone();
        return new SimpleCube(deepCopiedList);
    }

    public bool Equals(SimpleCube? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return ComplexPoints.SequenceEqual(other.ComplexPoints);
    }

    public override bool Equals(object? obj) =>
        obj is SimpleCube other && Equals(other);

    public override string ToString() => $"Points: \n\t{{{string.Join("\n\t", ComplexPoints)}}}";
    public override int GetHashCode() => ToString().GetHashCode();
}