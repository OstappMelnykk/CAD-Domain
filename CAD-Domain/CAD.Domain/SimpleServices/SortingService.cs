using CAD.Domain.ComplexFigures;

namespace CAD.Domain.SimpleServices;

public static class SortingService
{
    public static void GlobalSorting(List<ComplexPoint> points) =>
        points.Sort(new ComplexPointComparer());
    
    public static void GlobalSorting(List<SimplePoint> points) =>
        points.Sort(new SimplePointComparer());
}