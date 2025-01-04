using System.Text;

namespace CAD.Domain.CustomExceptions;

public class InvalidTriangleException : ApplicationException
{
    private StringBuilder _message;

    public InvalidTriangleException(Simple3DPoint point1, Simple3DPoint point2, Simple3DPoint point3)
    {
        _message = new StringBuilder();
        
        _message.AppendLine("The triangle is not valid because the provided points are collinear.");
        _message.AppendLine("A valid triangle requires that the three points do not lie on a single straight line.");
        _message.AppendLine($"Details of the points:");
        _message.AppendLine($"- Point 1: {point1}");
        _message.AppendLine($"- Point 2: {point2}");
        _message.AppendLine($"- Point 3: {point3}");
        _message.AppendLine("The collinear points fail to form a valid triangle as the area of the triangle becomes zero.");
    }

    public override string Message
    {
        get => _message.ToString();
    }
}