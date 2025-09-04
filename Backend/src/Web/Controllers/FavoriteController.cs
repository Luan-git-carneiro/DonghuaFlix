using System.Security.Claims;
using DonghuaFlix.Backend.src.Core.Aplication.Commands.Favorites;
using DonghuaFlix.Backend.src.Core.Aplication.DTOs.Favorites;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonghuaFlix.src.Web.Controllers;


[ApiController]
[Route("api/[controller]")]
public  class FavoriteController : ControllerBase
{
    private readonly IMediator _mediator;

    public FavoriteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetFavorite")]

    
    [HttpPost("{id}" , Name ="CreateFavorite")]
    [Authorize]
    public async Task<IActionResult> AddFavorite(Guid id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ;

         if (userIdClaim == null)
            return Unauthorized();

        if(!Guid.TryParse(userIdClaim.Value , out Guid userId))
        {
            var responseError = new ApiResponse<Object>(
                sucess: false ,
                message: "Id do usuario esta incorreto" ,
                data: null ,
                errorCode: "NOT_FOUND"
            ); 
            return NotFound(responseError);
        }

        await _mediator.Send(new AddFavoriteCommand( UserId: userId , DonghuaId: id));

        return Accepted();
    }
}
