using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.Favorites;

public class DeleteFavoriteCommand : IRequest<ApiResponse<Favorite>>
{
    public Guid UserId { get; set; }
    public Guid DonghuaId { get; set; }

    public DeleteFavoriteCommand(Guid userId, Guid donghuaId)
        {
            UserId = userId;
            DonghuaId = donghuaId;
        }
        
}