using CAD.Domain.ComplexFigures;

namespace CAD.Domain;

public static class ListExtensions
{
    public static List<T> DeepClone<T>(this List<T> list) where T : ICloneable
    {
        return list.Select(item => (T)item.Clone()).ToList();
    }
    
    public static List<ComplexPoint> DistinctAndMergeLocalIds(this List<ComplexPoint> complexPoints)
    {
        return complexPoints
            .GroupBy(cp => cp.SimplePoint) // Group by SimplePoint
            .Select(group =>
            {
                // Merge LocalIds from all ComplexPoints in the group
                var mergedLocalIds = group.SelectMany(cp => cp.LocalIds).Distinct().ToList();
                //var mergedLocalIds = group.SelectMany(cp => cp.LocalIds).ToList();

                // Create a new ComplexPoint with the merged LocalIds and the properties of the first item in the group
                var first = group.First();
                return new ComplexPoint(first.SimplePoint, first.GlobalId, mergedLocalIds);
            })
            .ToList();
    }
}