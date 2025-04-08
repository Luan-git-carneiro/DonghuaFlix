namespace DonghuaFlix.src.Core.Domain.Exceptions;

public class DomainException : Exception
{
    public string ErrorCode { get; private set; } = "DOMAIN_ERROR";

    public DomainException()
    {
    }

    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }


}