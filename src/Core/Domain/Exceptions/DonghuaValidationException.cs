using System;

namespace DonghuaFlix.src.Core.Domain.Exceptions;

public class DonghuaValidationException : IOException
{
    public DonghuaValidationException(string message) : base(message)
    {
    }
    public DonghuaValidationException(string message, IOException innerException) : base(message, innerException)
    {
    }
}
