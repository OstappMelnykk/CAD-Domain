using CAD.Domain;
using CAD.Domain.ComplexFigures;

namespace CAD.ConsoleApplication;

class Program
{
    static void Main(string[] args)
    {
        SuperCube sc = new SuperCube();
        SuperCubeService scs = new SuperCubeService(sc);
        
        Console.WriteLine("Default SuperCube initialization with division: {x: 1, y: 1, z: 1}");
        Console.WriteLine("Points: " + scs.GetPoints().Count);
        Console.WriteLine("Pairs of indices: " + scs.PairsToConnect_asListIndices.Count);
        Console.WriteLine("Polygons: " + scs.Polygons_asListIndices.Count);
        
        scs.DivideCubeByAxes(2, 2, 2);
        Console.WriteLine("Devide: {x: 2, y: 2, z: 2}");
        Console.WriteLine("Points: " + scs.GetPoints().Count);
        Console.WriteLine("Pairs of indices: " + scs.PairsToConnect_asListIndices.Count);
        Console.WriteLine("Polygons: " + scs.Polygons_asListIndices.Count);
        
        scs.DivideCubeByAxes(3, 3, 3);
        Console.WriteLine("Devide: {x: 3, y: 3, z: 3}");
        Console.WriteLine("Points: " + scs.GetPoints().Count);
        Console.WriteLine("Pairs of indices: " + scs.PairsToConnect_asListIndices.Count);
        Console.WriteLine("Polygons: " + scs.Polygons_asListIndices.Count);
    }
}