using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Queries.Favorites;

public class GetFavoriteForUserQueryHandler : IRequestHandler<GetFavoriteForUserQuery , ApiResponse<List<Favorite>>>
{
    readonly IFavoriteRepository _favoriteRepository ;

    public GetFavoriteForUserQueryHandler(IFavoriteRepository  favoriteRepository)
    {
        _favoriteRepository = favoriteRepository ;
    }

    public async Task<ApiResponse<List<Favorite>>> Handle(GetFavoriteForUserQuery request , CancellationToken cancellationToken)
    {

        var listaFavorite = await _favoriteRepository.GetByUserIdAsync(request.UserID) ;

        var response = new ApiResponse<List<Favorite>>(
            sucess: true ,
            message: "Favoritos recuperados com sucesso" ,
            data: listaFavorite ,
            errorCode: null
        );

        return response ;

    }

}