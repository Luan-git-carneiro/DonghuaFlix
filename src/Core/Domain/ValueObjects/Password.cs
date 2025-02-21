using DonghuaFlix.src.Core.Domain.Abstractions;
using DonghuaFlix.src.Core.Domain.Exceptions;
using BCryptNet = BCrypt.Net;


namespace DonghuaFlix.src.Core.Domain.ValueObjects;


public class Password : ValueObject
{
    public string Value {get; }

    public Password(string passwordValue)
    {
        if( string.IsNullOrWhiteSpace(passwordValue) || passwordValue.Length < 6)
        {
            throw new DomainException("A senha deve conter no mínimo 6 caracteres.");
        }

        Value = BCryptNet.BCrypt.HashPassword(passwordValue);
    }

    public bool Validar (string passwordValue) => BCryptNet.BCrypt.Verify(passwordValue, Value);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }


}
