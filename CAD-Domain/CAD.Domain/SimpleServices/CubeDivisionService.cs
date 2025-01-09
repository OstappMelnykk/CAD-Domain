using System.Drawing;
using CAD.Domain.ComplexFigures;

namespace CAD.Domain.SimpleServices;

public static class CubeDivisionService
{
    private static readonly ComplexCube BaseComplexCube = new ComplexCube(new SimpleCube());
    
    public static List<double> CalcNewPoints(double coordinate1, double coordinate2, int num)
    {
        int numSegments = num - 1;
        
        List<double> newPointsArray = new List<double>(numSegments + 2);
        
        if (num == 0)
            return newPointsArray;
        
        newPointsArray.Add(coordinate1);
        
        for (int i = 1; i <= numSegments; i++) 
        {
            double ti = (double)i / (numSegments + 1);
            double xi = (1 - ti) * coordinate1  +  ti * coordinate2;
            newPointsArray.Add(Math.Round(xi, 5));
        }
        
        newPointsArray.Add(coordinate2);
        return newPointsArray;
    }
    
    
    
    
    
    public static List<ComplexCube> DivideCubeBy_X_Axis(int divisionNumber, SimplePoint minCubePoint, SimplePoint maxCubePoint)
    {
        List<double> newXPoints = CalcNewPoints(
            minCubePoint.X, 
            maxCubePoint.X, 
            divisionNumber);
      
        List<ComplexCube> newComplexCubes = new();

        for (int i = 0; i < divisionNumber; i++)
        {
            List<ComplexPoint> simpleCubeComplexPoints = new List<ComplexPoint>
            {
                new ComplexPoint(new SimplePoint(newXPoints[i],     minCubePoint.Y, minCubePoint.Z)),
                new ComplexPoint(new SimplePoint(newXPoints[i + 1], minCubePoint.Y, minCubePoint.Z)),
                new ComplexPoint(new SimplePoint(newXPoints[i],     minCubePoint.Y, maxCubePoint.Z)),
                new ComplexPoint(new SimplePoint(newXPoints[i + 1], minCubePoint.Y, maxCubePoint.Z)),
                new ComplexPoint(new SimplePoint(newXPoints[i],     maxCubePoint.Y, minCubePoint.Z)),
                new ComplexPoint(new SimplePoint(newXPoints[i + 1], maxCubePoint.Y, minCubePoint.Z)),
                new ComplexPoint(new SimplePoint(newXPoints[i],     maxCubePoint.Y, maxCubePoint.Z)),
                new ComplexPoint(new SimplePoint(newXPoints[i + 1], maxCubePoint.Y, maxCubePoint.Z)),
            };
            
            newComplexCubes.Add(new ComplexCube(new SimpleCube(simpleCubeComplexPoints)));
        }

        return newComplexCubes;
    }
    
    
    public static List<ComplexCube> DivideCubeBy_Y_Axis(int divisionNumber, SimplePoint minCubePoint, SimplePoint maxCubePoint)
    {
        List<double> newYPoints = CalcNewPoints(
            minCubePoint.Y, 
            maxCubePoint.Y, 
            divisionNumber);
        
        List<ComplexCube> newComplexCubes = new();

        for (int i = 0; i < divisionNumber; i++)
        {
            List<ComplexPoint> simpleCubeComplexPoints = new List<ComplexPoint>
            {
                new ComplexPoint(new SimplePoint(minCubePoint.X, newYPoints[i],     minCubePoint.Z)),
                new ComplexPoint(new SimplePoint(maxCubePoint.X, newYPoints[i],     minCubePoint.Z)),
                new ComplexPoint(new SimplePoint(minCubePoint.X, newYPoints[i],     maxCubePoint.Z)),
                new ComplexPoint(new SimplePoint(maxCubePoint.X, newYPoints[i],     maxCubePoint.Z)),
                new ComplexPoint(new SimplePoint(minCubePoint.X, newYPoints[i + 1], minCubePoint.Z)),
                new ComplexPoint(new SimplePoint(maxCubePoint.X, newYPoints[i + 1], minCubePoint.Z)),
                new ComplexPoint(new SimplePoint(minCubePoint.X, newYPoints[i + 1], maxCubePoint.Z)),
                new ComplexPoint(new SimplePoint(maxCubePoint.X, newYPoints[i + 1], maxCubePoint.Z)),
            };
            
            newComplexCubes.Add(new ComplexCube(new SimpleCube(simpleCubeComplexPoints)));
        }
        return newComplexCubes;
    }
    
    
    public static List<ComplexCube> DivideCubeBy_Z_Axis(int divisionNumber, SimplePoint minCubePoint, SimplePoint maxCubePoint)
    {
        List<double> newZPoints = CalcNewPoints(
            minCubePoint.Z, 
            maxCubePoint.Z, 
            divisionNumber);

        List<ComplexCube> newComplexCubes = new();

        for (int i = 0; i < divisionNumber; i++)
        {
            List<ComplexPoint> simpleCubeComplexPoints = new List<ComplexPoint>
            {
                new ComplexPoint(new SimplePoint(minCubePoint.X, minCubePoint.Y, newZPoints[i])),
                new ComplexPoint(new SimplePoint(minCubePoint.X, minCubePoint.Y, newZPoints[i])),
                new ComplexPoint(new SimplePoint(minCubePoint.X, minCubePoint.Y, newZPoints[i + 1])),
                new ComplexPoint(new SimplePoint(minCubePoint.X, minCubePoint.Y, newZPoints[i + 1])),
                new ComplexPoint(new SimplePoint(minCubePoint.X, minCubePoint.Y, newZPoints[i])),
                new ComplexPoint(new SimplePoint(minCubePoint.X, minCubePoint.Y, newZPoints[i])),
                new ComplexPoint(new SimplePoint(minCubePoint.X, minCubePoint.Y, newZPoints[i + 1])),
                new ComplexPoint(new SimplePoint(minCubePoint.X, minCubePoint.Y, newZPoints[i + 1])),
            };
            
            newComplexCubes.Add(new ComplexCube(new SimpleCube(simpleCubeComplexPoints)));
        }
        return newComplexCubes;
    }
    
    
    
    public static List<ComplexPoint> DivideCubeByAxes(int dx, int dy, int dz)
    {
        //List<ComplexCube> ComplexCubes = new();
        List<ComplexPoint> ComplexPoints = new();
        List<ComplexPoint> DistinctComplexPoints;
        
        List<ComplexCube> newComplexCubesByY = DivideCubeBy_Y_Axis(dy, BaseComplexCube.MinCubePoint, BaseComplexCube.MaxCubePoint);
        for (int i = 0; i < newComplexCubesByY.Count; i++)
        {
            List<ComplexCube> newComplexCubesByZ = DivideCubeBy_Z_Axis(dz, newComplexCubesByY[i].MinCubePoint, newComplexCubesByY[i].MaxCubePoint);
            for (int j = 0; j < newComplexCubesByZ.Count; j++)
            {
                List<ComplexCube> newComplexCubesByX = DivideCubeBy_X_Axis(dx, newComplexCubesByZ[j].MinCubePoint, newComplexCubesByZ[j].MaxCubePoint);
                for (int k = 0; k < newComplexCubesByX.Count; k++)
                { 
                    //ComplexCubes.Add(newComplexCubesByX[k]);
                    for (int l = 0; l < newComplexCubesByX[k].ComplexPoints.Count; l++)
                    {
                        ComplexPoints.Add(newComplexCubesByX[k].ComplexPoints[l]);
                    }
                }
            }
        }
        
        DistinctComplexPoints = ComplexPoints.DistinctAndMergeLocalIds();
        SortingService.GlobalSorting(ComplexPoints);
        
        for (int i = 0; i < DistinctComplexPoints.Count; i++)
        {
            DistinctComplexPoints[i].GlobalId = (uint)(i + 1);
        }
        
        return DistinctComplexPoints;
    }
}