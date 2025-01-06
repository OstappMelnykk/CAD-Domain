namespace CAD.Domain;

public static class ListExtensions
{
    public static List<T> DeepClone<T>(this List<T> list) where T : ICloneable
    {
        return list.Select(item => (T)item.Clone()).ToList();
    }
}