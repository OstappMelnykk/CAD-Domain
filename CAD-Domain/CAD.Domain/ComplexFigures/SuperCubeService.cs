﻿using System.Net.Security;
using CAD.Domain.Simple2DFigures;

namespace CAD.Domain.ComplexFigures;

public class SuperCubeService
{ 
    private SuperCube _superCube;

    public List<(int, int)> PairsToConnect_asListIndices { get; init; }
    public List<(SimplePoint, SimplePoint)> PairsToConnect_asSimplePoints { get; init; }
    
    public List<(int, int, int)> Polygons_asListIndices { get; init; }
    public List<(SimplePoint, SimplePoint, SimplePoint)> Polygons_asSimplePoints { get; init; }
    public List<SimpleTriangle> Polygons_asSimpleTriangles { get; init; }

    public SuperCubeService(SuperCube superCube)
    {
        PairsToConnect_asListIndices = new();
        PairsToConnect_asSimplePoints = new();
        
        Polygons_asListIndices = new();
        Polygons_asSimplePoints = new();
        Polygons_asSimpleTriangles = new();     
        
        _superCube = superCube;
    }

    public SuperCubeService() : this(new SuperCube()) { }

    public void DivideCubeByAxes(int dx, int dy, int dz)
    {
        _superCube.DivideCubeByAxes(dx, dy, dz);
        
        Get_pairsToConnect_asListIndices();
        Get_pairsToConnect_asSimplePoints(PairsToConnect_asListIndices);
        
        Get_Polygons_asListIndices();

    }

    public List<SimplePoint> GetPoints()
    {
        return _superCube.GetSimplePoints();
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
            .Select(touple => (
                            _superCube.ComplexPoints[touple.Item1].SimplePoint,
                            _superCube.ComplexPoints[touple.Item2].SimplePoint
                            ))
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



    
    
    
    
    private List<(int, int, int)> GetPolygons(int d_Axis, List<int> Face_PointsList_Axis)
    {
        List<List<int>> ordinary = new List<List<int>>();
        List<List<int>> middle = new List<List<int>>();
        
        for (int i = 0; i < Face_PointsList_Axis.Count; i += 3 * d_Axis + 2)
        {
            List<int> ord = new List<int>();
            for (int j = i; j < i + 2 * d_Axis + 1; j++)
                ord.Add(Face_PointsList_Axis[j]);
            ordinary.Add(ord);
        }
        
        for (int i = 2 * d_Axis + 1; i < Face_PointsList_Axis.Count - 2 * d_Axis - 1; i += 3 * d_Axis + 2)
        {
            List<int> mid = new List<int>();
            for (int j = i; j < i + d_Axis + 1; j++)
                mid.Add(Face_PointsList_Axis[j]);
            middle.Add(mid);
        }
        
        
        List<(int, int, int)> polygons = new();

        for (int i = 0; i < ordinary.Count - 1; i++)
        {
            for (int j = 0, k = 0; j < ordinary[i].Count - 2; j += 2, k++)
            {
                (int, int, int) polygon1 = (ordinary[i][j], ordinary[i][j + 1], middle[i][k]);
                (int, int, int) polygon2 = (ordinary[i][j + 1], ordinary[i][j + 2], middle[i][k + 1]);
                (int, int, int) polygon3 = (ordinary[i + 1][j], ordinary[i + 1][j + 1], middle[i][k]);
                (int, int, int) polygon4 = (ordinary[i + 1][j + 1], ordinary[i + 1][j + 2], middle[i][k + 1]);
                (int, int, int) polygon5 = (ordinary[i][j + 1], ordinary[i + 1][j + 1], middle[i][k]);
                (int, int, int) polygon6 = (ordinary[i][j + 1], ordinary[i + 1][j + 1], middle[i][k + 1]);
                
                polygons.Add(polygon1);
                polygons.Add(polygon2);
                polygons.Add(polygon3);
                polygons.Add(polygon4);
                polygons.Add(polygon5);
                polygons.Add(polygon6);
            }
        }

        return polygons;
    }



    private void Get_Polygons_asListIndices()
    {
        Dictionary<string, List<int>> Face_PointsList = _superCube.Face_PointsList;
        int dx = _superCube.Dx;
        int dy = _superCube.Dy;
        int dz = _superCube.Dz;

        List<(int, int, int)> NegativeFace_X_Polygons = GetPolygons(dz, Face_PointsList["NegativeFacePoints_X"]);
        List<(int, int, int)> PositiveFace_X_Polygons = GetPolygons(dz, Face_PointsList["PositiveFacePoints_X"]);
        
        List<(int, int, int)> NegativeFace_Y_Polygons = GetPolygons(dx, Face_PointsList["NegativeFacePoints_Y"]);
        List<(int, int, int)> PositiveFace_Y_Polygons = GetPolygons(dx, Face_PointsList["PositiveFacePoints_Y"]);
        
        List<(int, int, int)> NegativeFace_Z_Polygons = GetPolygons(dx, Face_PointsList["NegativeFacePoints_Z"]);
        List<(int, int, int)> PositiveFace_Z_Polygons = GetPolygons(dx, Face_PointsList["PositiveFacePoints_Z"]);

        
        Polygons_asListIndices.Clear();
        
        Polygons_asListIndices.AddRange(NegativeFace_X_Polygons);
        Polygons_asListIndices.AddRange(PositiveFace_X_Polygons);
        Polygons_asListIndices.AddRange(NegativeFace_Y_Polygons);
        Polygons_asListIndices.AddRange(PositiveFace_Y_Polygons);
        Polygons_asListIndices.AddRange(NegativeFace_Z_Polygons);
        Polygons_asListIndices.AddRange(PositiveFace_Z_Polygons);
    }
    
    
    

    private void Get_Polygons_asSimplePoints()
    {
        List<(SimplePoint, SimplePoint, SimplePoint)> polygons_asSimplePoints = Polygons_asListIndices
                        .Select(touple => (
                                        _superCube.ComplexPoints[touple.Item1].SimplePoint,
                                        _superCube.ComplexPoints[touple.Item2].SimplePoint,
                                        _superCube.ComplexPoints[touple.Item3].SimplePoint
                                        ))
                        .ToList();

        Polygons_asSimplePoints.Clear();
        Polygons_asSimplePoints.AddRange(polygons_asSimplePoints);

    }

    private void Get_Polygons_asSimplePoints(List<(int, int, int)> polygons_asListIndices)
    {
        List<(SimplePoint, SimplePoint, SimplePoint)> polygons_asSimplePoints = polygons_asListIndices
                        .Select(touple => (
                                        _superCube.ComplexPoints[touple.Item1].SimplePoint,
                                        _superCube.ComplexPoints[touple.Item2].SimplePoint,
                                        _superCube.ComplexPoints[touple.Item3].SimplePoint
                        ))
                        .ToList();

        Polygons_asSimplePoints.Clear();
        Polygons_asSimplePoints.AddRange(polygons_asSimplePoints);
    }
    
    


    private void Get_Polygons_asSimpleTriangles()
    {
        List<SimpleTriangle> polygons_asSimpleTriangles = Polygons_asSimplePoints
                        .Select(touple => new SimpleTriangle(touple.Item1, touple.Item2, touple.Item3))
                        .ToList();

        Polygons_asSimpleTriangles.Clear();
        Polygons_asSimpleTriangles.AddRange(polygons_asSimpleTriangles);
    }

    private void Get_Polygons_asSimpleTriangles(List<(SimplePoint, SimplePoint, SimplePoint)> polygons_asSimplePoints)
    {
        List<SimpleTriangle> polygons_asSimpleTriangles = polygons_asSimplePoints
                        .Select(touple => new SimpleTriangle(touple.Item1, touple.Item2, touple.Item3))
                        .ToList();

        Polygons_asSimpleTriangles.Clear();
        Polygons_asSimpleTriangles.AddRange(polygons_asSimpleTriangles);
    }
}