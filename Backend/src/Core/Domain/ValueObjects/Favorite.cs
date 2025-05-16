using DonghuaFlix.Backend.src.Core.Domain.Abstractions;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;

namespace DonghuaFlix.Backend.src.Core.Domain.ValueObjects;

public class Favorite : ValueObject
{
    public Guid UserId { get; init; }
    public Guid DonghuaId { get; init; }
    public DateTime DateCreat { get; init; }

    public Favorite(  Guid userId , Guid donghuaId, DateTime dateCreat) 
    {
        if(donghuaId == Guid.Empty)
        {
            throw new DomainValidationException(field: nameof(donghuaId) , message: "Id do donghua é inválido.");
        }

        UserId = userId;
        DonghuaId = donghuaId;
        DateCreat = dateCreat;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DonghuaId;
        yield return DateCreat;
        yield return UserId;
    }
}