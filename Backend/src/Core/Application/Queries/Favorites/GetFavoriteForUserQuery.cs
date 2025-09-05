using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using MediatR;
using Favorite = DonghuaFlix.Backend.src.Core.Domain.ValueObjects.Favorite;

namespace DonghuaFlix.Backend.src.Core.Application.Queries.Favorites;

public record GetFavoriteForUserQuery(Guid UserID) : IRequest<ApiResponse<List<Favorite>>>;