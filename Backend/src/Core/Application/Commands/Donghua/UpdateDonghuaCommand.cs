using DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;

public class UpdateDonghuaCommand : IRequest<ApiResponse<DonghuaDto>>
{
    // Propriedades necessárias para criar um Donghua
    public Guid? DonghuaId { get; set; } = new Guid();

    public string? Title { get; set; }
    public string? TileEnglish { get; set;}
    public string? Description { get; set; }
    public string? Sinopse { get; set; }
    public string? Studio { get; set; }
    public DateTime? releaseDate { get; set; } = new DateTime();  
    public Genre? Genres { get; set; } 
    public DonghuaType? Type { get; set; }
    public DonghuaStatus? Status { get; set; }
    public string? Image { get; set; }
    public string? Banner { get; set; }
    public string? Trailer { get; set;}
}