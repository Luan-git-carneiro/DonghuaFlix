using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;

// Exemplo: Define a intenção e os dados necessários para deletar
public class DeleteDonghuaCommand : IRequest<ApiResponse<DonghuaDto>> // Unit indica que não retorna dados
{
    public Guid DonghuaId { get; set; } // ID do Donghua a ser deletado

    public DeleteDonghuaCommand(Guid donghuaId)
    {
        DonghuaId = donghuaId ;
    }

}