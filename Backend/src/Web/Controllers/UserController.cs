using System.Net.Http.Headers;
using DonghuaFlix.Backend.src.Core.Application.Commands.User;
using DonghuaFlix.Backend.src.Core.Application.Commands.User.Login;
using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
using DonghuaFlix.Backend.src.Core.Application.DTOs.User.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DonghuaFlix.Backend.src.Web.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthenticationResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        // 1. Criar o Command com os dados da requisição
        var command = new LoginUserCommand(request.Email, request.Password);

        // 2. Enviar o Command para o MediatorDD
        // O MediatR encontrará o Handler correto (LoginUserCommandHandler) e o executará
        var result = await _mediator.Send(command);

        // 3. Retornar a resposta com base no resultado Handler.
        
        if(result.IsSucess)
        {
            return Ok( new { result.Data});
        }

        return Unauthorized(new { result });
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthenticationResult) , StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterUserInput request)
    {
        // 1. criar o command para o Mediator
        var command = new RegisterUserCommand(request.Email, request.Password, request.Name );
        // 2. Enviar o Command para o Mediator
        var result = await _mediator.Send(command);
        // 3. Retornar a resposta com base no resultado Handler.
        if (result.IsSucess)
        {
            return CreatedAtAction(nameof(Register), new { result });
        }

        if(result.Message.Contains("Conflict"))
        {
            return Conflict(new { result });
        }

        return BadRequest(new { result });


    }



}