using System.Text.Json;
using CAD.Domain.Simple2DFigures;
using CAD.Domain.SimpleServices;

namespace CAD.Domain.ComplexFigures;

public class SuperCube : IEquatable<SuperCube>
{
    public List<ComplexPoint> ComplexPoints { get; private set; }
    public Dictionary<string, List<uint>> Face_PointsList { get; private set; }
    
    public int Dx { get; set; }
    public int Dy { get; set; }
    public int Dz { get; set; }

    public SuperCube(int dx, int dy, int dz)
    {
        ComplexPoints = new();
        Face_PointsList = new Dictionary<string, List<uint>>
        {
            { "NegativeFacePoints_X", new List<uint>() },
            { "NegativeFacePoints_Y", new List<uint>() },
            { "NegativeFacePoints_Z", new List<uint>() },
            { "PositiveFacePoints_X", new List<uint>() },
            { "PositiveFacePoints_Y", new List<uint>() },
            { "PositiveFacePoints_Z", new List<uint>() },
        };
        
        DivideCubeByAxes(dx, dy, dz);
    }
    
    public SuperCube() : this(1, 1, 1) {}
    

    public void DivideCubeByAxes(int dx, int dy, int dz)
    {
        Dx = dx;
        Dy = dy;
        Dz = dz;
        
        ComplexPoints.Clear();
        ComplexPoints.AddRange(CubeDivisionService.DivideCubeByAxes(dx, dy, dz));
        //ComplexPoints = CubeDivisionService.DivideCubeByAxes(dx, dy, dz);
        
        SetGlobalIds();
        ConfigurePointsOnFaces();
    }

    public List<SimplePoint> GetSimplePoints()
    {
        return ComplexPoints.Select(complexPoint => complexPoint.SimplePoint).ToList();
    }
    
    private void SetGlobalIds()
    {
        for (int i = 0; i < ComplexPoints.Count; i++)
        {
            ComplexPoints[i].GlobalId = (uint)(i + 1);
        }
    }
    
    public void ConfigurePointsOnFaces()
    {
        foreach (var key in Face_PointsList.Keys.ToList())
        {
            Face_PointsList[key].Clear();
        }
        
        SimplePoint minCubePoint = ComplexPoints[0].SimplePoint;
        SimplePoint maxCubePoint = ComplexPoints[^1].SimplePoint;
        
        foreach (var point in ComplexPoints)
        {
            if (point.SimplePoint.X == minCubePoint.X) Face_PointsList["NegativeFacePoints_X"].Add(point.GlobalId);
            if (point.SimplePoint.X == maxCubePoint.X) Face_PointsList["PositiveFacePoints_X"].Add(point.GlobalId);
            
            if (point.SimplePoint.Y == minCubePoint.Y) Face_PointsList["NegativeFacePoints_Y"].Add(point.GlobalId);
            if (point.SimplePoint.Y == maxCubePoint.Y) Face_PointsList["PositiveFacePoints_Y"].Add(point.GlobalId);
            
            if (point.SimplePoint.Z == minCubePoint.Z) Face_PointsList["NegativeFacePoints_Z"].Add(point.GlobalId);
            if (point.SimplePoint.Z == maxCubePoint.Z) Face_PointsList["PositiveFacePoints_Z"].Add(point.GlobalId);
        }
    }
    
    
    public override string ToString() => $"Points: \n\t{{{string.Join("\n\t", ComplexPoints)}}}";
    
    public bool Equals(SuperCube? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return ComplexPoints.SequenceEqual(other.ComplexPoints);
    }
    
    public override bool Equals(object? obj) =>
        obj is SuperCube other && Equals(other);
    
    public override int GetHashCode() => ToString().GetHashCode();
    
    
    public string GetJsonSerializedCube()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
    }
}















/*public List<(int, int)> PairsToConnect_asListIndices { get; private set; }

public List<(SimplePoint, SimplePoint)> PairsToConnect_asSimplePoints { get; private set; }*/
    
    
    
/*public SimplePoint MinCubePoint { get; private set; }
public SimplePoint MaxCubePoint { get; private set; }
*/

/*
    private (List<(int, int)>, List<(SimplePoint, SimplePoint)>) GetPairsToConnect(int dx, int dy, int dz)
    {
        /*
        List<(int, int)> pairsOfIndicesToConect = DrawingService.GetIndicesToConnect(dx, dy, dz);
        List<(SimplePoint, SimplePoint)> pairsOfSimplePointsToConect = DrawingService.GetSimplePointsToConnect(ComplexPoints, pairsOfIndicesToConect);
        #1#
        
        
        
        List<(int, int)> X_AxisParallelPairs = Get_X_AxisParallelPairs(dx, dy, dz);
        List<(int, int)> Y_AxisParallelPairs = Get_Y_AxisParallelPairs(dx, dy, dz);
        List<(int, int)> Z_AxisParallelPairs = Get_Z_AxisParallelPairs(dx, dy, dz);
        
        
        List<(int, int)> pairsOfIndicesToConect = new List<(int, int)>();

        pairsOfIndicesToConect.AddRange(X_AxisParallelPairs);
        pairsOfIndicesToConect.AddRange(Y_AxisParallelPairs);
        pairsOfIndicesToConect.AddRange(Z_AxisParallelPairs);
        
        List<(SimplePoint, SimplePoint)> pairsOfSimplePointsToConect = pairsOfIndicesToConect
            .Select(touple => (ComplexPoints[touple.Item1].SimplePoint, ComplexPoints[touple.Item2].SimplePoint))
            .ToList();
        
        return (pairsOfIndicesToConect, pairsOfSimplePointsToConect);
    }

    private List<(int, int)> Get_X_AxisParallelPairs(int dx, int dy, int dz)
    {
        List<(int, int)> pairs = new List<(int, int)>();
        
        int stepFor_X = 3 * dx + 2;
        int stepFor_y = Face_PointsList["NegativeFacePoints_Y"].Count + (dx + 1) * (dz + 1);

        for (int i = 0; i < dy + 1; i++)
        {
            int temp = 0;
            for (int j = 0; j < dz + 1; j++)
            {
                for (int k = 0; k < 2 * dx; k += 2)
                {
                    int index = k + temp + i * stepFor_y;

                    pairs.Add((index, index + 1));
                    pairs.Add((index + 1, index + 2));
                }
                temp += stepFor_X;
            }
        }
        return pairs;
    }
    
    private List<(int, int)> Get_Y_AxisParallelPairs(int dx, int dy, int dz)
    {
        List<(int, int)> pairs = new List<(int, int)>();
        
        int stepFor_X = 3 * dx + 2;
        int stepFor_y = Face_PointsList["NegativeFacePoints_Y"].Count + (dx + 1) * (dz + 1);
        
        for (int i = 0; i < dy; i++)
        {
            int temp = 0;
            int midcounter = i * stepFor_y + Face_PointsList["NegativeFacePoints_Y"].Count;
            for (int j = 0; j < dz + 1; j++)
            {
                for (int k = 0; k < 2 * dx + 1; k += 2)
                {
                    int index = k + temp + stepFor_y * i;
                    
                    pairs.Add((index, midcounter));
                    pairs.Add((midcounter, index + stepFor_y));
                    
                    midcounter++;
                }
                temp += stepFor_X;
            }
        }
        return pairs;
    }
    
    private List<(int, int)> Get_Z_AxisParallelPairs(int dx, int dy, int dz)
    {
        List<(int, int)> pairs = new List<(int, int)>();

        int stepFor_y = Face_PointsList["NegativeFacePoints_Y"].Count + (dx + 1) * (dz + 1);
        int max_counter = 2 * dx + 1;
        int min_counter = dx + 1;
        
        int t = 3 * dx + 2;

        for (int i = 0; i < 2 * dx + 1; i += 2)
        {
            for (int j = 0; j < dz; j++)
            {
                for (int k = 0; k < dy + 1; k++)
                {
                    int firstValue =  stepFor_y * k + j * t + i;
                    int secondValue = firstValue + max_counter;
                    int thirdValue =  secondValue;
                    int fourthValue = thirdValue + min_counter;
                    
                    pairs.Add((firstValue, secondValue));
                    pairs.Add((thirdValue, fourthValue));
                }
            }
            max_counter--;
            min_counter++;
        }
        return pairs;
    }
    */