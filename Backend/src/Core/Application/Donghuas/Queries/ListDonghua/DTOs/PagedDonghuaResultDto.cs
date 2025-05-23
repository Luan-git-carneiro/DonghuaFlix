//dto de resposta para a querie de listar donghuas (com metadados  de paginaçao)

using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;

namespace DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua.DTOs;

public class PagedDonghuaResultDto
{
    public List<DonghuaDto> Donghuas { get; set; } = new List<DonghuaDto>();    // Lista de Donghuas
    public int TotalItems { get; set; }             // Total de itens
    public int CurrentPage { get; set; }      // Página atual
    public int TotalPages { get; set; }     // Total de páginas
}