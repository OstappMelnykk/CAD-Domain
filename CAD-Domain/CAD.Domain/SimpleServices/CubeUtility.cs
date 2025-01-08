using CAD.Domain.ComplexFigures;

namespace CAD.Domain.SimpleServices;

public static class CubeUtility
{
    // Predefined edges for the cube
    private static readonly IReadOnlyList<(int, int)> EdgeIndexes = new List<(int, int)>
    {
        (0, 1), (2, 3), (4, 5), (6, 7),      // Bottom edges
        (0, 4), (1, 5), (2, 6), (3, 7),      // Vertical edges
        (0, 2), (1, 3), (4, 6), (5, 7)       // Top edges
    };

    public static IEnumerable<ComplexPoint> GenerateMiddlePoints(SimpleCube simpleCube)
    {
        foreach (var (indexA, indexB) in EdgeIndexes)
        {
            SimplePoint pointA = simpleCube.ComplexPoints[indexA].SimplePoint;
            SimplePoint pointB = simpleCube.ComplexPoints[indexB].SimplePoint;

            // Compute the middle point
            var middlePoint = SimplePoint.GetMidPointBetweenPoints(pointA, pointB);
            yield return new ComplexPoint(middlePoint);
        }
    }
}