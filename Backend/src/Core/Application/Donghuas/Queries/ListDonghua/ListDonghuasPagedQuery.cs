//Parametro de entrada

using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua.DTOs;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua;

public class ListDonghuasPagedQuery : IRequest<ApiResponse<PagedResult<DonghuaDto>>>
{
    public int Page { get; set; } = 1; // Página atual
    public int PageSize { get; set; } = 10; // Items por pagina 
    public string? SearchTerm { get; set; } // Termo de pesquisa opcional
}