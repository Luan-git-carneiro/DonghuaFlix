//dto de resposta para a querie de listar donghuas (com metadados  de paginaçao)

using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;

namespace DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua.DTOs;

public class PagedResult<T>
{
    public List<T> Items { get; set; }     
    public int TotalItems { get; set; }             // Total de itens
    public int CurrentPage { get; set; }      // Página atual
    public int TotalPages { get; set; }     // Total de páginas
    public int PageSize { get; set;}

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedResult(List<T> items , int count , int pageNumber , int pageSize)
    {
        Items = items;
        TotalItems = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}