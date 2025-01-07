using CAD.Domain.ComplexFigures;

namespace CAD.Domain;

public static class DefaultCube
{
    // Centralize common default cube data
    private static readonly Dictionary<int, List<SimplePoint>> DefaultSimpleCubes = new()
    {
        { 8, new List<SimplePoint>
            {
                new SimplePoint(-1, -1, -1), new SimplePoint(1, -1, -1), new SimplePoint(-1, -1, 1), new SimplePoint(1, -1, 1),
                new SimplePoint(-1, 1, -1), new SimplePoint(1, 1, -1), new SimplePoint(-1, 1, 1), new SimplePoint(1, 1, 1),
            }
        },
        { 12, new List<SimplePoint>
            {
                new SimplePoint(0, -1, -1), new SimplePoint(-1, -1, 0), new SimplePoint(1, -1, 0), new SimplePoint(0, -1, 1),
                new SimplePoint(-1, 0, -1), new SimplePoint(1, 0, -1), new SimplePoint(-1, 0, 1), new SimplePoint(1, 0, 1),
                new SimplePoint(0, 1, -1), new SimplePoint(-1, 1, 0), new SimplePoint(1, 1, 0), new SimplePoint(0, 1, 1),
            }
        },
        { 20, new List<SimplePoint>
            {
                new SimplePoint(-1, -1, -1), new SimplePoint(0, -1, -1), new SimplePoint(1, -1, -1), new SimplePoint(-1, -1, 0),
                new SimplePoint(1, -1, 0), new SimplePoint(-1, -1, 1), new SimplePoint(0, -1, 1), new SimplePoint(1, -1, 1),
                new SimplePoint(-1, 0, -1), new SimplePoint(1, 0, -1), new SimplePoint(-1, 0, 1), new SimplePoint(1, 0, 1),
                new SimplePoint(-1, 1, -1), new SimplePoint(0, 1, -1), new SimplePoint(1, 1, -1), new SimplePoint(-1, 1, 0),
                new SimplePoint(1, 1, 0), new SimplePoint(-1, 1, 1), new SimplePoint(0, 1, 1), new SimplePoint(1, 1, 1),
            }
        }
    };

    // Centralize common default complex cube data by converting SimplePoint into ComplexPoint
    private static readonly Dictionary<int, List<ComplexPoint>> DefaultComplexCubes = DefaultSimpleCubes
        .ToDictionary(
            item => item.Key,
            item => item.Value.Select(simplePoint => new ComplexPoint(simplePoint)).ToList()
        );
    
    
    
    

    // Generic getter for SimplePoint cubes
    public static List<SimplePoint> GetSimpleCubePoints(int pointCount) =>
        DeepClone(DefaultSimpleCubes.TryGetValue(pointCount, out var points) 
            ? points 
            : throw new ArgumentException($"No default simple cube with {pointCount} points.") 
        );

    // Generic getter for ComplexPoint cubes
    public static List<ComplexPoint> GetComplexCubePoints(int pointCount) =>
        DeepClone(DefaultComplexCubes.TryGetValue(pointCount, out var points) 
            ? points 
            : throw new ArgumentException($"No default complex cube with {pointCount} points.") 
        );

    
    
    // Helper method: Deep clone the input list
    private static List<T> DeepClone<T>(List<T> list) where T : ICloneable =>
        list.Select(item => (T)item.Clone()).ToList();
}