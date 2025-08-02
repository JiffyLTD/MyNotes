using System.Net;

namespace Core.Exceptions;

public class BaseException : Exception
{
    public BaseException() { }

    public BaseException(string message) 
        : base(message) { }
    
    public virtual HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
}