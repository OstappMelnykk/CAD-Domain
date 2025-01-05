using CAD.Domain.ComplexFigures;

namespace CAD.Domain.SimpleServices;

public static class SortingService
{
    public static void GlobalSorting(List<ComplexPoint> points) =>
        points.Sort(new ComplexPointComparer());
}
