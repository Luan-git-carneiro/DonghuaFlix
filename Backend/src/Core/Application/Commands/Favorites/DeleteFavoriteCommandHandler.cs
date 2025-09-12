using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.Favorites;

public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand , ApiResponse<Favorite>>
{
    private readonly IFavoriteRepository _favoriteRepository ;

    public DeleteFavoriteCommandHandler(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository ;
    }

    public async Task<ApiResponse<Favorite>> Handle( DeleteFavoriteCommand request , CancellationToken cancellationToken)
    {
        var favorite = await _favoriteRepository.GetFavoriteAsync(request.UserId ,request.DonghuaId);

        if(favorite is null)
        {
            var responseError = new ApiResponse<Favorite>(
                sucess: false ,
                message: "favorito não encontrado" ,
                data: null ,
                errorCode: "NOT_FOUND" 
            );

            return responseError ;
        }

        var response = new ApiResponse<Favorite>(
            sucess: true ,
            message: "Favorito removido com sucesso" ,
            data: favorite ,
            errorCode: null 
        );

        return response ;
    }


}