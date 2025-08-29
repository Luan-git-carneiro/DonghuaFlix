using System.Security.Claims;
using DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;
using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.GetDonghua;
using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua;
using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua.DTOs;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Application.Interfaces;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;


namespace DonghuaFlix.Backend.src.Web.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DonghuaController : ControllerBase
{
    private readonly IMediator _mediator;

    public DonghuaController(IMediator mediator)
    {
        _mediator = mediator;

    }

    [HttpGet("{donghuaId}", Name = "GetDonghua")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse<DonghuaDto>) , StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<DonghuaDto>) , StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Donghua(Guid donghuaId)
    {
        var query = new GetDonghuaByIdQuery(donghuaId);
        var result = await _mediator.Send(query);

        var linkHelper = new HateoasHelper(Url);
        Console.WriteLine($"Context1 - Controller: {this.GetType().Name}");
        Console.WriteLine($"Context1 - HttpContext: {HttpContext != null}");
        Console.WriteLine($"Context1 - Request Path: {HttpContext?.Request?.Path}");
        var links = linkHelper.GenerateLinks("Donghua" , donghuaId  , null);
            
         result.AddLinks(links);

        if(result.ErrorCode == "NOT_FOUND")
        {
            return NotFound(result);
        }

        return Ok(result);

    }

    // DonghuasController.cs
    [HttpGet(Name = "GetDonghuasWhithPaged")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<DonghuaWithLinksDto>>) ,StatusCodes.Status200OK )]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<DonghuaWithLinksDto>>) ,StatusCodes.Status404NotFound )]
    public async Task<ActionResult<ApiResponse<IEnumerable<DonghuaWithLinksDto>>>> Donghuas([FromQuery] ListDonghuasPagedQuery query)
    {
        var result = await _mediator.Send(query);
        
        if(result.ErrorCode == "NOT_FOUND")
        {
            return NotFound(result);
        }

        var linkHelper = new HateoasHelper(Url);

        //Criando o Dado papa o result
        var donghuaWithLinks = result.Data?.Items?.Select(donghua => {
                        var donghuaId = donghua.DonghuaId;  
                         // Teste diferentes nomes de rota
                        var links1 = linkHelper.GenerateLinks("Donghua", donghua.DonghuaId, null);
                        var links2 = linkHelper.GenerateLinks("GetDonghuasWithPaged", donghua.DonghuaId, null);
                        var links3 = linkHelper.GenerateLinks("Donghuas", donghua.DonghuaId, null);


                            Console.WriteLine($"Links with 'Donghua': {links1?.Count() ?? 0}");
                            Console.WriteLine($"Links with 'GetDonghuasWithPaged': {links2?.Count() ?? 0}");
                            Console.WriteLine($"Links with 'GetDonghuasWithPaged': {links3?.Count() ?? 0}");

                        return new DonghuaWithLinksDto{
                            Donghua =  donghua ,
                            Links = linkHelper.GenerateLinks("Donghua" , donghuaId , null).ToList()
                        };
                    }
                ).ToList();


        //Criar a resposta
        var resposta = new ApiResponse<IEnumerable<DonghuaWithLinksDto>>(
            true,
            "Donghuas recuperados com sucesso" , 
            donghuaWithLinks ,
            null
        );

        //criar links de paginação
        AddPaginationLinks( resposta , result.Data! , query);

        return Ok(resposta);

    }

    [HttpPost(Name = "CreateDonghua")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status203NonAuthoritative)]
    [ProducesResponseType(typeof(ApiResponse<DonghuaDto>) , StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ApiResponse<DonghuaDto>) , StatusCodes.Status201Created)]

    public async Task<ActionResult<ApiResponse<DonghuaDto>>> Donghua([FromBody] AddDonghuaInput donghua)
    {
        var command = new CreateDonghuaCommand(
                title: donghua.Title,
                sinopse: donghua.Sinopse,
                studio: donghua.Studio,
                releaseYear: donghua.ReleaseYear,
                genres: donghua.Genres,
                type: donghua.Type,
                status: donghua.Status,
                imaagem: donghua.Image
        );

        var result = await _mediator.Send(command);

        if(!result.IsSucess)
        {
            if(result.ErrorCode!.Contains("409"))
            {
                return Conflict(result);
            }
        }

        var urls = new HateoasHelper(Url);

        result.AddLinks(urls.GenerateLinks("Donghua" , result.Data!.DonghuaId , null));

        return Ok(result);
        

    }


    private void AddPaginationLinks( ApiResponse<IEnumerable<DonghuaWithLinksDto>> response, PagedResult<DonghuaDto> pagedResult , ListDonghuasPagedQuery query)
    {
        //link para a primeira pagina
        if (pagedResult.CurrentPage > 1)
        {
            var firstPageLink = Url.Link("GetDonghuasWhithPaged", new { 
                page = 1, 
                pageSize = query.PageSize,
                searchTerm = query.SearchTerm
            });
            response.AddLink(new Link(firstPageLink, "first", "GET"));
        }

        // Link para página anterior
        if (pagedResult.HasPrevious)
        {
            var prevPageLink = Url.Link("GetDonghuasWhithPaged", new { 
                page = pagedResult.CurrentPage - 1, 
                pageSize = query.PageSize,
                searchTerm = query.SearchTerm
            });
            response.AddLink(new Link(prevPageLink, "prev", "GET"));
        }

        // Link para próxima página
        if (pagedResult.HasNext)
        {
            var nextPageLink = Url.Link("GetDonghuasWhithPaged", new { 
                page = pagedResult.CurrentPage + 1, 
                pageSize = query.PageSize,
                searchTerm = query.SearchTerm
            });
            response.AddLink(new Link(nextPageLink, "next", "GET"));
        }

        // Link para última página
        if (pagedResult.CurrentPage < pagedResult.TotalPages)
        {
            var lastPageLink = Url.Link("GetDonghuasWhithPaged", new { 
                page = pagedResult.TotalPages, 
                pageSize = query.PageSize,
                searchTerm = query.SearchTerm
            });
            response.AddLink(new Link(lastPageLink, "last", "GET"));
        }

        // Link para criar novo donghua
        var createLink = Url.Link("GetDonghuasWhithPaged", new {});
        response.AddLink(new Link(createLink, "create", "POST"));

    }


}