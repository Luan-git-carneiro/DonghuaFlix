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
using Microsoft.CodeAnalysis.CSharp.Syntax;


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

    [HttpGet("{id}", Name = "GetDonghua")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ApiResponse<DonghuaDto>) , StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<DonghuaDto>) , StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Donghua(Guid id)
    {

        var query = new GetDonghuaByIdQuery(id);
        var result = await _mediator.Send(query);

        var linkHelper = new HateoasHelper(Url);

        var links = linkHelper.GenerateLinks("Donghua" , id , null);
                Console.WriteLine($"Context1 - Request links: {links.ToList()}");

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
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<DonghuaWithLinksDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<DonghuaWithLinksDto>>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<IEnumerable<DonghuaWithLinksDto>>>> Donghuas([FromQuery] ListDonghuasPagedQuery query)
    {
        var result = await _mediator.Send(query);
        if (result.ErrorCode == "SOME_ERROR_CODE") // Replace with the actual error code condition
        {
            throw new Exception("deu ruim");
        }
        
        if(result.ErrorCode == "NOT_FOUND")
        {
            return NotFound(result);
        }

        var linkHelper = new HateoasHelper(Url);

        //Criando o Dado para o result
        var donghuaWithLinks = result.Data?.Items?.Select((donghua) => {

            var donghuaId = donghua.DonghuaId;  
            var links = linkHelper.GenerateLinks("Donghua", donghuaId, null);
            
            return new DonghuaWithLinksDto{
                Donghua = donghua,
                Links = links?.ToList() 
            };
        }).ToList();

        //Criar a resposta
        var resposta = new ApiResponse<IEnumerable<DonghuaWithLinksDto>>(
            true,
            "Donghuas recuperados com sucesso", 
            donghuaWithLinks,
            null
        );

        AddPaginationLinks(resposta, result.Data!, query);

        return Ok(resposta);
    }

    [HttpDelete( "{id}" , Name = "DeleteDonghua")]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status203NonAuthoritative)]
    [ProducesResponseType(typeof(ApiResponse<DonghuaDto>) , StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<DonghuaDto>) , StatusCodes.Status200OK)]
    public  async Task<ActionResult<ApiResponse<DonghuaDto>>> DeleteDonghua(Guid id) 
    {
        var command = new DeleteDonghuaCommand(id);
        var result = await _mediator.Send(command);

        if(!result.IsSucess)
        {
            return NotFound(result);
        }

        var urlHelper = new HateoasHelper(Url) ;

        var links = urlHelper.GenerateLinks("Donghua" , id , null) ;

        result.AddLinks(links);

        return Ok( result ) ;        

    }


    [HttpPut("{id}" , Name = "UpdateDonghua")]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status203NonAuthoritative)]
    [ProducesResponseType(typeof(ApiResponse<DonghuaDto>) , StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<DonghuaDto>) , StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<DonghuaDto>>> UpdateDonghua(Guid id , [FromBody] UpdateDonghuaCommand request)
    {
        request.DonghuaId = id ;

        var result = await _mediator.Send(request);

        if(!result.IsSucess)
        {
            return NotFound(result);
        }

        var urlHelper = new HateoasHelper(Url);

        var links = urlHelper.GenerateLinks("Donghua" , id , null) ;

        result.AddLinks(links) ;

        return Ok( result );

    }

    

    [HttpPost(Name = "CreateDonghua")]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
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
                imaagem: donghua.Image,
                rating: donghua.Rating
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

        return StatusCode(201 ,result);
        

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