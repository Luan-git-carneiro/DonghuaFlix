using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries;
using MediatR;
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
}