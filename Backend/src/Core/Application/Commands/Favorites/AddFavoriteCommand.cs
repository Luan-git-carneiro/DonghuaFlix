using MediatR;

namespace DonghuaFlix.Backend.src.Core.Aplication.Commands.Favorites;

    public record AddFavoriteCommand 
    (
        Guid  UserId ,
        Guid  DonghuaId 
    ) : IRequest<Unit>;
