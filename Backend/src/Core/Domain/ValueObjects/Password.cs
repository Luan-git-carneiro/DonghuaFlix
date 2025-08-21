using DonghuaFlix.Backend.src.Core.Domain.Abstractions;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using BCryptNet = BCrypt.Net;


namespace DonghuaFlix.Backend.src.Core.Domain.ValueObjects;


public class Password : ValueObject
{
    public string Value {get; }
 

    public Password() { } // Construtor protegido para o EF Core
    public Password(string passwordValue)
    {
        if( string.IsNullOrWhiteSpace(passwordValue) || passwordValue.Length < 6)
        {
            throw new DomainValidationException( field: nameof(passwordValue)  , message:  "A senha deve conter no mínimo 6 caracteres.");
        }

         Value = BCryptNet.BCrypt.HashPassword(passwordValue);
    }

    public bool Validar (string passwordValue) => BCryptNet.BCrypt.Verify(passwordValue, Value);
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }


}
