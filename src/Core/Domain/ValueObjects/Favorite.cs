using DonghuaFlix.src.Core.Domain.Abstractions;

namespace DonghuaFlix.src.Core.Domain.ValueObjects;

public class Favorite : ValueObject
{
    public Guid DonghuaId { get; init; }
    public DateTime DateCreat { get; init; }

    public Favorite(Guid donghuaId, DateTime dateCreat) 
    {
        DonghuaId = donghuaId;
        DateCreat = dateCreat;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return DonghuaId;
        yield return DateCreat;
    }
}