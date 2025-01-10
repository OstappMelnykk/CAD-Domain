using System.Text;
using System.Text.Json;
using CAD.Domain.SimpleServices;

namespace CAD.Domain.ComplexFigures;

public class ComplexCube : IEquatable<ComplexCube>
{
    public List<ComplexPoint> ComplexPoints { get; } // SimpleCube verteces points + MiddlePoint of each edge of SimpleCube
    
    public SimplePoint MinCubePoint { get; }
    public SimplePoint MaxCubePoint { get; }

    public ComplexCube(SimpleCube simpleCube, List<ComplexPoint> complexMiddlePoints = null)
    {
        ComplexPoints = simpleCube?.ComplexPoints?.ToList() ?? 
                        throw new ArgumentNullException(nameof(complexMiddlePoints));

        if (complexMiddlePoints is null)
        {
            ComplexPoints.AddRange(CubeUtility.GenerateMiddlePoints(simpleCube));
        }
        else
        {
            if (complexMiddlePoints.Count != 12) throw new ArgumentException($"Complex points count must be 12.");
            ComplexPoints.AddRange(complexMiddlePoints);
        }
        
        if (ComplexPoints.Count != 20) throw new ArgumentException("The list of points must contain 20 points.");   
        
        SortingService.GlobalSorting(ComplexPoints);

        MinCubePoint = ComplexPoints[0].SimplePoint;
        MaxCubePoint = ComplexPoints[^1].SimplePoint;
        
        SetLocalIds();
    }
    
    public ComplexCube() : this(new SimpleCube(), DefaultCube.GetComplexCubePoints(12)) {}
    
    private void SetLocalIds()
    {
        List<byte> localIds = new()
        {
            1, 9, 2, 12, 10, 4, 11, 3, 13, 14,
            16, 15, 5, 17, 6, 20, 18, 8, 19, 7
        };
        
        for (int i = 0; i < 20; i++)
        {
            ComplexPoints[i].LocalIds.Add(localIds[i]);
        }
    }
    
    
    public override string ToString() => $"Points: \n\t{{{string.Join("\n\t", ComplexPoints)}}}";

    public bool Equals(ComplexCube? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return ComplexPoints.SequenceEqual(other.ComplexPoints);
    }
    
    public override bool Equals(object? obj) =>
        obj is ComplexCube other && Equals(other);
    
    public override int GetHashCode() => ToString().GetHashCode();
    
    
    
    public string GetJsonSerializedCube()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
    }
}