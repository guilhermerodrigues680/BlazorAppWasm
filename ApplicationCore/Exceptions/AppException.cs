namespace BlazorAppWasm.ApplicationCore.Exceptions;

public class AppException : Exception
{
    public AppException() : base() { }
    public AppException(string message) : base(message) { }
    public AppException(string message, Exception e) : base(message, e) { }
}