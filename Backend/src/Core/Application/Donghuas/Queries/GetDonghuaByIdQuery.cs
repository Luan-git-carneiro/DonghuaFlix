using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries;

public class GetDonghuaByIdQuery : IRequest<DonghuaDto>
{
    public Guid DonghuaId { get; set; }

    public GetDonghuaByIdQuery(Guid donghuaId)
    {
        DonghuaId = donghuaId;
    }
}