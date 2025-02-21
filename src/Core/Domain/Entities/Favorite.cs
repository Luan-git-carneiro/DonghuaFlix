using DonghuaFlix.src.Core.Domain.Abstractions;

namespace DonghuaFlix.src.Core.Domain.Entities;

public class Favorite : Entity
{
    public Guid IdFavorite { get; private set; }
    public Guid IdDonghua { get; private set; }
    public Guid IdUser { get; private set; }

    public Favorite(Guid idDonghua, Guid idUser)
    {
        IdDonghua = idDonghua;
        IdUser = idUser;
        IdFavorite = Guid.NewGuid();
    }


}