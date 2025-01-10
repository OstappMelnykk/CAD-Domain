using CAD.Domain.SimpleServices;

namespace CAD.Domain.ComplexFigures;

public class SuperCube : IEquatable<SuperCube>
{
    public List<ComplexPoint> ComplexPoints { get; private set; }

    public SuperCube(int dx, int dy, int dz)
    {
        DivideCubeByAxes(dx, dy, dz);
    }
    
    public SuperCube() : this(1, 1, 1) {}
    

    public void DivideCubeByAxes(int dx, int dy, int dz)
    {
        ComplexPoints = CubeDivisionService.DivideCubeByAxes(dx, dy, dz);
        SetGlobalIds();
    }

    public List<SimplePoint> GetSimplePoints()
    {
        return ComplexPoints.Select(complexPoint => complexPoint.SimplePoint).ToList();
    }
    
    private void SetGlobalIds()
    {
        for (int i = 0; i < ComplexPoints.Count; i++)
        {
            ComplexPoints[i].GlobalId = (uint)(i + 1);
        }
    }
    
    public override string ToString() => $"Points: \n\t{{{string.Join("\n\t", ComplexPoints)}}}";
    
    public bool Equals(SuperCube? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return ComplexPoints.SequenceEqual(other.ComplexPoints);
    }
    
    public override bool Equals(object? obj) =>
        obj is SuperCube other && Equals(other);
    
    public override int GetHashCode() => ToString().GetHashCode();
    
}