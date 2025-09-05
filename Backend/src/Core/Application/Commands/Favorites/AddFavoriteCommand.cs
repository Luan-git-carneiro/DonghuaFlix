using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Aplication.Commands.Favorites;

    public record AddFavoriteCommand 
    (
        Guid  UserId ,
        Guid  DonghuaId 
    ) : IRequest<ApiResponse<Favorite>>;
