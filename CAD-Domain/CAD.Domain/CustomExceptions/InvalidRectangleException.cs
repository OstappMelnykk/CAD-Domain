using System.Text;

namespace CAD.Domain.CustomExceptions;

public class InvalidRectangleException : ApplicationException
{
    private StringBuilder _message;
    
    public InvalidRectangleException(Simple3DPoint point1, Simple3DPoint point2, Simple3DPoint point3, Simple3DPoint point4)
    {
        _message = new StringBuilder();
        
        _message.AppendLine("The following points failed to form a rectangle:");
        _message.AppendLine($"- Point 1: {point1}");
        _message.AppendLine($"- Point 2: {point2}");
        _message.AppendLine($"- Point 3: {point3}");
        _message.AppendLine($"- Point 4: {point4}");
        _message.AppendLine();
        _message.AppendLine("Possible reasons:");
        _message.AppendLine("- The points may not be in the same plane.");
        _message.AppendLine("- The distances between points do not match the properties of a rectangle (equal opposite sides and equal diagonals).");
        _message.AppendLine("- The angles between adjacent sides may not be 90 degrees.");
    }
    
    public override string Message
    {
        get => _message.ToString();
    }
}