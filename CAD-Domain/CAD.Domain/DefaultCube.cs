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
        List<ComplexPoint> deepCopiedList = DefaultCube8Points.DeepClone();
        return DefaultCube8Points.DeepClone();
    }

    public static List<ComplexPoint> GetDefaultCube12MiddlePoints()
    {
        List<ComplexPoint> deepCopiedList = DefaultCube12MiddlePoints.DeepClone();
        return deepCopiedList;
    }

    public static List<ComplexPoint> GetDefaultCube20Points()
    {
        List<ComplexPoint> deepCopiedList = DefaultCube20Points.DeepClone();
        return deepCopiedList;
    }
}