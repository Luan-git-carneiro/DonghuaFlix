using System.Text.RegularExpressions;
using DonghuaFlix.src.Core.Domain.Abstractions;
using DonghuaFlix.src.Core.Domain.Exceptions;

namespace DonghuaFlix.src.Core.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Valor { get; }
    public Email(string valor)
    {
        if(string.IsNullOrWhiteSpace(valor))
        {
            throw new DomainValidationException(field: nameof(valor) , message: "Email é obrigatório.");
        }
        
        if (!Regex.IsMatch(valor, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new DomainValidationException( field: nameof(valor) , message: "Formato de e-mail inválido");
        }
 
        Valor = valor.Trim().ToLower();
 
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Valor;
    }

    public static implicit operator string(Email email) => email.Valor;
    public static implicit operator Email(string valor) => new(valor);

}
