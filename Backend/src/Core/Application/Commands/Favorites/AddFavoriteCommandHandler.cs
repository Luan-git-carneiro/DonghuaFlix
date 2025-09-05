using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Aplication.Commands.Favorites;


public class AddFavoriteCommandHandler : IRequestHandler<AddFavoriteCommand, ApiResponse<Favorite>>
{
    readonly IFavoriteRepository _favoriteRepository ;

    public AddFavoriteCommandHandler(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository ;
    }

    public async Task<ApiResponse<Favorite>> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
    {

        var data = new DateTime();

        var favorite = new Favorite(userId: request.UserId , donghuaId: request.DonghuaId , dateCreat: data);

        await _favoriteRepository.AddAsync(favorite);

        var response = new ApiResponse<Favorite>(
            sucess: true ,
            message: "Adicionado com sucesso" ,
            data: favorite ,
            errorCode: null
        );

        return  response ;
    }
}


