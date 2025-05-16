namespace DonghuaFlix.Backend.src.Core.Aplication.DTOs.Favorites;

    public record AddFavoriteInput
    (
        Guid  DonghuaId ,
        Guid  UserId
    );
