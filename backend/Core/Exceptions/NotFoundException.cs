using System.Net;

namespace Core.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException() { }

    public NotFoundException(string message)
        : base(message) { }

    public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;
}