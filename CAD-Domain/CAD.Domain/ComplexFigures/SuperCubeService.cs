namespace CAD.Domain.ComplexFigures;

public class SuperCubeService
{ 
    private SuperCube _superCube;
    
    public List<(int, int)> PairsToConnect_asListIndices { get; init; }
    public List<(SimplePoint, SimplePoint)> PairsToConnect_asSimplePoints { get; init; }

    public SuperCubeService(SuperCube superCube)
    {
        PairsToConnect_asListIndices = new();
        PairsToConnect_asSimplePoints = new();
        
        _superCube = superCube;
    }

    public SuperCubeService() : this(new SuperCube()) { }

    public void DivideCubeByAxes(int dx, int dy, int dz)
    {
        _superCube.DivideCubeByAxes(dx, dy, dz);
        
        Get_pairsToConnect_asListIndices();
        Get_pairsToConnect_asSimplePoints(PairsToConnect_asListIndices);


        /*
         
        Console.WriteLine("Dx: " + dx + ", Dy: " + dy + ", Dz: " + dz);
        Console.WriteLine("ComplexPoints:" + _superCube.ComplexPoints.Count);
        Console.WriteLine("ListIndices pairs: " + PairsToConnect_asListIndices.Count);
        Console.WriteLine("SimplePoints pairs: " + PairsToConnect_asSimplePoints.Count);
        
        */
    }

    private void Get_pairsToConnect_asListIndices()
    {
        List<(int, int)> X_AxisParallelPairs = Get_X_AxisParallelPairs();
        List<(int, int)> Y_AxisParallelPairs = Get_Y_AxisParallelPairs();
        List<(int, int)> Z_AxisParallelPairs = Get_Z_AxisParallelPairs();
        
        List<(int, int)> pairsToConnect_asListIndices = new List<(int, int)>();

        pairsToConnect_asListIndices.AddRange(X_AxisParallelPairs);
        pairsToConnect_asListIndices.AddRange(Y_AxisParallelPairs);
        pairsToConnect_asListIndices.AddRange(Z_AxisParallelPairs);

        PairsToConnect_asListIndices.Clear();
        PairsToConnect_asListIndices.AddRange(pairsToConnect_asListIndices);
    }
    
    
    private void Get_pairsToConnect_asSimplePoints()
    {
        List<(SimplePoint, SimplePoint)> pairsToConnect_asSimplePoints = PairsToConnect_asListIndices
            .Select(touple => (_superCube.ComplexPoints[touple.Item1].SimplePoint, _superCube.ComplexPoints[touple.Item2].SimplePoint))
            .ToList();
        
        
        PairsToConnect_asSimplePoints.Clear();
        PairsToConnect_asSimplePoints.AddRange(pairsToConnect_asSimplePoints);
    }
    
    private void Get_pairsToConnect_asSimplePoints(List<(int, int)> pairsToConnect_asListIndices)
    {
    
        List<(SimplePoint, SimplePoint)> pairsToConnect_asSimplePoints = pairsToConnect_asListIndices
            .Select(touple => (_superCube.ComplexPoints[touple.Item1].SimplePoint, _superCube.ComplexPoints[touple.Item2].SimplePoint))
            .ToList();
        
        PairsToConnect_asSimplePoints.Clear();
        PairsToConnect_asSimplePoints.AddRange(pairsToConnect_asSimplePoints);
    }




    private List<(int, int)> Get_X_AxisParallelPairs()
    {
        List<(int, int)> pairs = new List<(int, int)>();
        
        int stepFor_X = 3 * _superCube.Dx + 2;
        int stepFor_y = _superCube.Face_PointsList["NegativeFacePoints_Y"].Count + (_superCube.Dx + 1) * (_superCube.Dz + 1);

        for (int i = 0; i < _superCube.Dy + 1; i++)
        {
            int temp = 0;
            for (int j = 0; j < _superCube.Dz + 1; j++)
            {
                for (int k = 0; k < 2 * _superCube.Dx; k += 2)
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
    
    private List<(int, int)> Get_Y_AxisParallelPairs()
    {
        List<(int, int)> pairs = new List<(int, int)>();
        
        int stepFor_X = 3 * _superCube.Dx + 2;
        int stepFor_y = _superCube.Face_PointsList["NegativeFacePoints_Y"].Count + (_superCube.Dx + 1) * (_superCube.Dz + 1);
        
        for (int i = 0; i < _superCube.Dy; i++)
        {
            int temp = 0;
            int midcounter = i * stepFor_y + _superCube.Face_PointsList["NegativeFacePoints_Y"].Count;
            for (int j = 0; j < _superCube.Dz + 1; j++)
            {
                for (int k = 0; k < 2 * _superCube.Dx + 1; k += 2)
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
    
    private List<(int, int)> Get_Z_AxisParallelPairs()
    {
        List<(int, int)> pairs = new List<(int, int)>();

        int stepFor_y = _superCube.Face_PointsList["NegativeFacePoints_Y"].Count + (_superCube.Dx + 1) * (_superCube.Dz + 1);
        int max_counter = 2 * _superCube.Dx + 1;
        int min_counter = _superCube.Dx + 1;
        
        int t = 3 * _superCube.Dx + 2;

        for (int i = 0; i < 2 * _superCube.Dx + 1; i += 2)
        {
            for (int j = 0; j < _superCube.Dz; j++)
            {
                for (int k = 0; k < _superCube.Dy + 1; k++)
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
}