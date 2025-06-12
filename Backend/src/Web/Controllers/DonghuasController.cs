using System.Security.Claims;
using DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;
using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.GetDonghua;
using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua;
using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua.DTOs;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DonghuaFlix.Backend.src.Web.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DonghuasController : ControllerBase
{
    private readonly IMediator _mediator;
    public DonghuasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{donghuaId}")]
    public async Task<IActionResult> GetDonghuaById(Guid donghuaId)
    {
        var query = new GetDonghuaByIdQuery(donghuaId);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // DonghuasController.cs
    [HttpGet]
    public async Task<ActionResult<PagedDonghuaResultDto>> ListPaged([FromQuery] ListDonghuasPagedQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> AddDonghua([FromBody] AddDonghuaInput donghua)
    {
        try
        {
            //Validação básica do ModelState
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Obter UserId do token/claims (assumindo autenticação JWT)
            var userIdClaim = User.FindFirst("UserId")?.Value
                ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("UserId não encontrado no token.");
            }

            //criar o comando
            var command = new CreateDonghuaCommand
            {
                Title = donghua.Title,
                Sinopse = donghua.Sinopse,
                Studio = donghua.Studio,
                ReleaseYear = donghua.ReleaseYear,
                Type = donghua.Type,
                Status = donghua.Status,
                Image = donghua.Image,
                Genres = donghua.Genres,
                UserId = userId
            };

            // Enviar o comando para o Mediator
            await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetDonghuaById), 
                new { id = "placehold" },
                new { message = "Donghua criado com sucesso." }   
            );
        
        } catch(DomainValidationException ex) {
            return BadRequest(new { message = ex.Message, field = ex.Field });
        } catch(BusinessRulesException ex) {
            return BadRequest(new { message = ex.Message, rulesName = ex.RulesName });
        } catch(Exception ex) {
            return StatusCode(500, new { message = "Erro interno do servidor.", details = ex.Message });
        }
    }


}