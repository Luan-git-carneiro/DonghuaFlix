using DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;

public class UpdateDonghuaCommand : IRequest<Unit>
{
    // Para validação de permissão
    public Guid UserId { get; set; }
    
    // Propriedades necessárias para criar um Donghua
    public Guid DonghuaId { get; set; }

    public string? Title { get; set; }
    public string? Sinopse { get; set; }
    public string? Studio { get; set; }
    public DateTime? releaseDate { get; set; }  // Note que usamos int em vez de DateTime
    public Genre? Genres { get; set; } 
    public DonghuaType? Type { get; set; }
    public DonghuaStatus? Status { get; set; }
    public string? Image { get; set; }
}