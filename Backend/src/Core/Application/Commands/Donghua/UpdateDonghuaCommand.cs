using DonghuaFlix.src.Core.Application.Commands.Donghua;
using MediatR;

namespace DonghuaFlix.src.Core.Application.Commands.Donghua;

public class UpdateDonghuaCommand : IRequest<Unit>
{
    // Para validação de permissão
    public Guid UserId { get; set; }
    
    // Propriedades necessárias para criar um Donghua
    public Guid DonghuaId { get; set; }
    public string? Title { get; set; }
    public string? Sinopse { get; set; }
    public string? Studio { get; set; }
    public int? releaseDate { get; set; }  // Note que usamos int em vez de DateTime
    public List<string>? Genres { get; set; } 
    public string? Type { get; set; }
    public string? Status { get; set; }
    public string? Image { get; set; }
}