using System.Net.Http.Headers;
using DonghuaFlix.Backend.src.Core.Application.Commands.User;
using DonghuaFlix.Backend.src.Core.Application.Commands.User.DeleteUser;
using DonghuaFlix.Backend.src.Core.Application.Commands.User.Login;
using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
using DonghuaFlix.Backend.src.Core.Application.DTOs.User.Login;
using DonghuaFlix.Backend.src.Core.Application.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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


    [HttpDelete("{userId}")]
    [Authorize(Roles = "Admin")] // Apenas usuários com papel de Admin podem deletar usuários
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {

        // 1. Criar o Command com o UserId
        var command = new DeleteUserCommand(userId);

        // 2. Enviar o Command para o Mediator
        var result = await _mediator.Send(command);

        // 3. Retornar a resposta com base no resultado Handler.
        if (result.IsSucess)
        {
            return Ok(result);
        }
        if (result.ErrorCode == "USER_NOT_FOUND")
        {
            return NotFound(new { result });
        }

        return BadRequest(new { result });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")] // Apenas usuários com papel de Admin podem listar usuários
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult>  GetAllUsers(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool? isActive = null
        )
    {

        // 1. Criar a Query
        var query = new GetUsersQuery
        {
            Page = page,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            IsActive = isActive
        };
        
        // 2. Enviar a Query para o Mediator
        var result = await _mediator.Send(query);

        // Adicionar headers para informações de paginação (opcional)
                Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
                Response.Headers.Append("X-Total-Pages", result.TotalPages.ToString());
                Response.Headers.Append("X-Current-Page", result.CurrentPage.ToString());
                Response.Headers.Append("X-Page-Size", result.PageSize.ToString());
                Response.Headers.Append("X-Has-Next-Page", result.HasNextPage.ToString());
                Response.Headers.Append("X-Has-Prev-Page", result.HasPreviousPage.ToString());


        if (result == null )
            {
                return NotFound(new { message = "Nenhum usuário encontrado" });
            }
            
            return Ok(new JsonResult(result));


    }

    //End point que valida o token de autenticação
    [HttpGet("validate-token")]
    [ProducesResponseType(typeof(ValidationResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ValidateToken()
    {
        // 1. Extrair token do header Authorization
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return Unauthorized(new ValidationResult 
            { 
                IsValid = false, 
                Error = "Token não fornecido" 
            });
        }

        var token = authHeader.Substring("Bearer ".Length).Trim();
        
        // 2. Criar e enviar a query para o Mediator
        var query = new ValidationTokenQuery(token);
        var result = await _mediator.Send(query);

        // 3. Retornar resultado
        if (result.IsValid)
        {
            return Ok(result);
        }
        
        return Unauthorized(result);
    }

}