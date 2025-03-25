using MediatR;

namespace DonghuaFlix.src.Core.Aplication.Commands.Favorites;

    public record AddFavoriteCommand 
    (
        Guid  UserId ,
        Guid  DonghuaId 
    ) : IRequest<Unit>;
