
namespace BlazorAppWasm.ApplicationCore.Exceptions;

public class InvalidParameterException : AppException
{
    public InvalidParameterException() : base() { }
    public InvalidParameterException(string message) : base(message) { }
    public InvalidParameterException(string message, Exception e) : base(message, e) { }
}
