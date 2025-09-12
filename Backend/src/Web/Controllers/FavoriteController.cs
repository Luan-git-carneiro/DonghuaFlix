using System.Security.Claims;
using DonghuaFlix.Backend.src.Core.Aplication.Commands.Favorites;
using DonghuaFlix.Backend.src.Core.Aplication.DTOs.Favorites;
using DonghuaFlix.Backend.src.Core.Application.Commands.Favorites;
using DonghuaFlix.Backend.src.Core.Application.Commands.User.DeleteUser;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Application.Queries.Favorites;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
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
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse<Favorite>) , StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<List<Favorite>>) , StatusCodes.Status200OK)]
    public async Task <ActionResult<ApiResponse<List<Favorite>>>> GetFavoriteForUser()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ;

        if (userIdClaim == null)
            return Unauthorized();   

        if(!Guid.TryParse(userIdClaim.Value , out Guid userId))
        {
            var responseError = new ApiResponse<Favorite>(
                sucess: false ,
                message: "Id do usuario esta incorreto" ,
                data: null ,
                errorCode: "NOT_FOUND"
            ); 
            return NotFound(responseError);
        }

        var result  = await _mediator.Send(new GetFavoriteForUserQuery(userId));

        var linkHelper = new HateoasHelper(Url);
         

        result.AddLinks( linkHelper.GenerateLinks("Favorite" , userId , null) );

        return result ;

    }

    
    [HttpPost("{id}" , Name ="CreateFavorite")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse<Favorite>) , StatusCodes.Status201Created)]
    public async Task<ActionResult<ApiResponse<Favorite>>> AddFavorite(Guid id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ;

         if (userIdClaim == null)
            return Unauthorized();

        if(!Guid.TryParse(userIdClaim.Value , out Guid userId))
        {
            var responseError = new ApiResponse<Favorite>(
                sucess: false ,
                message: "Id do usuario esta incorreto" ,
                data: null ,
                errorCode: "NOT_FOUND"
            ); 
            return NotFound(responseError);
        }

        var result = await _mediator.Send(new AddFavoriteCommand( UserId: userId , DonghuaId: id));

        var linkHelper = new HateoasHelper(Url);
         

        result.AddLinks( linkHelper.GenerateLinks("Favorite" , id , null) );

        return StatusCode(201 , result);
    }

    [HttpDelete("{id}" , Name = "DeleteFavorite")]
    [Produces("application/json")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<List<Favorite>>) , StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<Favorite>>> DeleteFavorite(Guid id) 
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ;

         if (userIdClaim == null)
            return Unauthorized();

        if(!Guid.TryParse(userIdClaim.Value , out Guid userId))
        {
            var responseError = new ApiResponse<Favorite>(
                sucess: false ,
                message: "Id do usuario esta incorreto" ,
                data: null ,
                errorCode: "NOT_FOUND"
            ); 
            return NotFound(responseError);
        }

        // Operação de regra de negocio 
        var command = new DeleteFavoriteCommand(userId , id);

        var reponse = await _mediator.Send(command);

        //LINHAS  para criar links para o recurso retornado

        var linkHelper = new HateoasHelper(Url);

        reponse.AddLinks(linkHelper.GenerateLinks("Favorite" , id , null));

        //linhas para retorna resposta
        return Ok(Response);

        
    }



}
