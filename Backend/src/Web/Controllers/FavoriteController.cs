using DonghuaFlix.src.Core.Aplication.Commands.Favorites;
using DonghuaFlix.src.Core.Aplication.DTOs.Favorites;
using MediatR;
using Microsoft.AspNetCore.Mvc;
/*
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

    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] AddFavoriteInput input)
    {
        await _mediator.Send(new AddFavoriteCommand(input.UserId, input.DonghuaId));

        return Accepted();
    }
}
*/