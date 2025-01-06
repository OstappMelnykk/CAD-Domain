using System.Text;

namespace CAD.Domain.CustomExceptions;

public class InvalidLineException : ApplicationException
{
    private StringBuilder _message;

    public InvalidLineException(SimplePoint point1, SimplePoint point2)
    {
        _message = new StringBuilder();
        
        _message.AppendLine("Invalid Line Creation:");
        _message.AppendLine("The two endpoints of the line cannot be identical.");
        _message.AppendLine($"- Point 1: {point1}");
        _message.AppendLine($"- Point 2: {point2}");
    }

    public override string Message
    {
        get => _message.ToString();
    }
}