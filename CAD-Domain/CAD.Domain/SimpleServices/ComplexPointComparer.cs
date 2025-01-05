using CAD.Domain.ComplexFigures;

namespace CAD.Domain.SimpleServices;

public class ComplexPointComparer : IComparer<ComplexPoint>
{
    public int Compare(ComplexPoint? p1, ComplexPoint? p2)
    {
        if (p1 is null || p2 is null)
        {
            return p1 is null ? (p2 is null ? 0 : -1) : 1;
        }

        // Спочатку порівнюємо координату Y
        int comparison = p1.SimplePoint.Y.CompareTo(p2.SimplePoint.Y);
        if (comparison != 0) return comparison;

        // Якщо Y однаковий, порівнюємо Z
        comparison = p1.SimplePoint.Z.CompareTo(p2.SimplePoint.Z);
        if (comparison != 0) return comparison;

        // Якщо Y і Z однакові, порівнюємо X
        return p1.SimplePoint.X.CompareTo(p2.SimplePoint.X);
    }
}