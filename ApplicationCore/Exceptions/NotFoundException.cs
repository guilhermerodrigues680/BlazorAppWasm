
namespace BlazorAppWasm.ApplicationCore.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException() : base() { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception e) : base(message, e) { }
}