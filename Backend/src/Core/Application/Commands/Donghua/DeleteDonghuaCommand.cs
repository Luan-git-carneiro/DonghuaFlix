using MediatR;

namespace DonghuaFlix.src.Core.Application.Commands.Donghua;

// Exemplo: Define a intenção e os dados necessários para deletar
public class DeleteDonghuaCommand : IRequest<Unit> // Unit indica que não retorna dados
{
    public Guid DonghuaId { get; set; } // ID do Donghua a ser deletado
    public Guid UserId { get; set; }    // ID do usuário realizando a ação (para autorização)
}