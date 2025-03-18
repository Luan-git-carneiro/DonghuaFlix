using DonghuaFlix.src.Core.Domain.Abstractions;
using DonghuaFlix.src.Core.Domain.Exceptions;

namespace DonghuaFlix.src.Core.Domain.Entities;

public class Favorite : Entity
{
    public Guid IdDonghua { get; private set; }
    public Guid IdUser { get; private set; }
    public DateTime AddedAt { get; } = DateTime.UtcNow;
    
/*
    internal Favorite(Guid idDonghua, Guid idUser)
    {
        IdDonghua = idDonghua;
        IdUser = idUser;
        base.Id = Guid.NewGuid();
    }
*/
  public Favorite(Guid idDonghua, Guid idUser) : base(Guid.NewGuid()) // Chama o construtor da base
    {
        IdDonghua = idDonghua;
        IdUser = idUser;
    
    }

    


}