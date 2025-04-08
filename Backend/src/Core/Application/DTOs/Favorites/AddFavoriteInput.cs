namespace DonghuaFlix.src.Core.Aplication.DTOs.Favorites
{
    public record AddFavoriteInput
    (
        Guid  DonghuaId ,
        Guid  UserId
    );
}