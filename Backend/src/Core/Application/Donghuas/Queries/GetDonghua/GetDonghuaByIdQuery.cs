using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.GetDonghua;

public class GetDonghuaByIdQuery : IRequest<ApiResponse<DonghuaDto>>
{
    public Guid DonghuaId { get; set; }

    public GetDonghuaByIdQuery(Guid donghuaId)
    {
        DonghuaId = donghuaId;
    }
}