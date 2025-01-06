using CAD.Domain.ComplexFigures;

namespace CAD.Domain;

public static class DefaultCube
{
    private static readonly List<ComplexPoint> DefaultCube8Points = new()
    {
        new ComplexPoint(new SimplePoint(-1, -1, -1)),
        new ComplexPoint(new SimplePoint(1, -1, -1)),
        new ComplexPoint(new SimplePoint(-1, -1, 1)),
        new ComplexPoint(new SimplePoint(1, -1, 1)),
        new ComplexPoint(new SimplePoint(-1, 1, -1)),
        new ComplexPoint(new SimplePoint(1, 1, -1)),
        new ComplexPoint(new SimplePoint(-1, 1, 1)),
        new ComplexPoint(new SimplePoint(1, 1, 1)),
    };
    
    private static readonly List<ComplexPoint> DefaultCube12MiddlePoints = new()
    {
        new ComplexPoint(new SimplePoint(0, -1, -1)),
        new ComplexPoint(new SimplePoint(-1, -1, 0)),
        new ComplexPoint(new SimplePoint(1, -1, 0)),
        new ComplexPoint(new SimplePoint(0, -1, 1)),
        new ComplexPoint(new SimplePoint(-1, 0, -1)),
        new ComplexPoint(new SimplePoint(1, 0, -1)),
        new ComplexPoint(new SimplePoint(-1, 0, 1)),
        new ComplexPoint(new SimplePoint(1, 0, 1) ),
        new ComplexPoint(new SimplePoint(0, 1, -1)),
        new ComplexPoint(new SimplePoint(-1, 1, 0)),
        new ComplexPoint(new SimplePoint(1, 1, 0)),
        new ComplexPoint(new SimplePoint(0, 1, 1)),
    };
    
    private static readonly List<ComplexPoint> DefaultCube20Points = new()
    {
         new ComplexPoint(new SimplePoint(-1, -1, -1)),
         new ComplexPoint(new SimplePoint(0, -1, -1)),
         new ComplexPoint(new SimplePoint(1, -1, -1)),
         new ComplexPoint(new SimplePoint(-1, -1, 0)),
         new ComplexPoint(new SimplePoint(1, -1, 0)),
         new ComplexPoint(new SimplePoint(-1, -1, 1)),
         new ComplexPoint(new SimplePoint(0, -1, 1)),
         new ComplexPoint(new SimplePoint(1, -1, 1)),
         new ComplexPoint(new SimplePoint(-1, 0, -1)),
         new ComplexPoint(new SimplePoint(1, 0, -1)),
         new ComplexPoint(new SimplePoint(-1, 0, 1)),
         new ComplexPoint(new SimplePoint(1, 0, 1)),
         new ComplexPoint(new SimplePoint(-1, 1, -1)),
         new ComplexPoint(new SimplePoint(0, 1, -1)),
         new ComplexPoint(new SimplePoint(1, 1, -1)),
         new ComplexPoint(new SimplePoint(-1, 1, 0)),
         new ComplexPoint(new SimplePoint(1, 1, 0)),
         new ComplexPoint(new SimplePoint(-1, 1, 1)),
         new ComplexPoint(new SimplePoint(0, 1, 1)),
         new ComplexPoint(new SimplePoint(1, 1, 1)),
    };
    
    
    public static List<ComplexPoint> GetDefaultCube8Points()
    {
        return DefaultCube8Points
            .Select(point => (ComplexPoint)point.Clone())
            .ToList();
    }

    public static List<ComplexPoint> GetDefaultCube12MiddlePoints()
    {
        return DefaultCube12MiddlePoints
            .Select(point => (ComplexPoint)point.Clone())
            .ToList();
    }

    public static List<ComplexPoint> GetDefaultCube20Points()
    {
        return DefaultCube20Points
            .Select(point => (ComplexPoint)point.Clone())
            .ToList();
    }
}