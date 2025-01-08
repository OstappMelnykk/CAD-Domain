namespace CAD.Domain.SimpleServices;

public class SimplePointComparer : IComparer<SimplePoint>
{
    public int Compare(SimplePoint? p1, SimplePoint? p2)
    {
        if (p1 is null || p2 is null)
        {
            return p1 is null ? (p2 is null ? 0 : -1) : 1;
        }

        // Спочатку порівнюємо координату Y
        int comparison = p1.Y.CompareTo(p2.Y);
        if (comparison != 0) return comparison;

        // Якщо Y однаковий, порівнюємо Z
        comparison = p1.Z.CompareTo(p2.Z);
        if (comparison != 0) return comparison;

        // Якщо Y і Z однакові, порівнюємо X
        return p1.X.CompareTo(p2.X);
    }
}