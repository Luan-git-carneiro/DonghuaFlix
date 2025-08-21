using DonghuaFlix.Backend.src.Core.Application.DTOs.User;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Queries.User;

public class GetUsersQuery : IRequest<GetUsersQueryResult>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string SearchTerm { get; set; } = "";
    public bool? IsActive { get; set; }  

}