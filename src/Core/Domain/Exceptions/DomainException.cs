namespace DonghuaFlix.src.Core.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException()
    {
    }

    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
    public DomainException(string paramName, string message) : base($"{paramName} - {message}")
    {
    }
    
}