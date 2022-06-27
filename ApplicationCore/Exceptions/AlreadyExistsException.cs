namespace BlazorAppWasm.ApplicationCore.Exceptions;

public class AlreadyExistsException : AppException
{
    public AlreadyExistsException() : base() { }
    public AlreadyExistsException(string message) : base(message) { }
    public AlreadyExistsException(string message, Exception e) : base(message, e) { }
}
