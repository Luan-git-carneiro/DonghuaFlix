using DonghuaFlix.src.Core.Domain.Enum;
using MediatR;

namespace DonghuaFlix.src.Core.Application.Commands.Donghua;

public class CreateDonghuaCommand : IRequest<Unit>
{
    // Para validação de permissão
    public Guid UserId { get; set; }
    
    // Propriedades necessárias para criar um Donghua
    public string Title { get; set; }
    public string Sinopse { get; set; }
    public string Studio { get; set; }
    public int ReleaseYear { get; set; }  // Note que usamos int em vez de DateTime
    public List<string> Genres { get; set; }  =  new List<string>();// Lista de nomes dos gêneros
    public string Type { get; set; }
    public string Status { get; set; }
    public string? Image { get; set; }
}