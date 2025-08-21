

using DonghuaFlix.Backend.src.Core.Application.DTOs.User;

namespace DonghuaFlix.Backend.src.Core.Application.Queries.User;

public class GetUsersQueryResult 
{
    public IEnumerable<UserDto> Users { get; set; } = new List<UserDto>();
    public int TotalCount { get; set; }
    public int TotalPages { get; set; } 
    public int CurrentPage { get; set; }
    public int RequestedPage { get; set; }
    public bool WasPageAdjusted { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
    public int PageSize { get; set; } 

    // Propriedades úteis extras
    public int StartRecord => TotalCount == 0 ? 0 : ((CurrentPage - 1) * PageSize) + 1;
    public int EndRecord => Math.Min(CurrentPage * PageSize, TotalCount);

    
}