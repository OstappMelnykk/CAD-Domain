using CAD.Domain;
using CAD.Domain.ComplexFigures;

namespace CAD.ConsoleApplication;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(new SuperCube(2, 2, 2).GetJsonSerializedCube());
    }
}