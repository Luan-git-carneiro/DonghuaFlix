

using DonghuaFlix.Backend.src.Core.Domain.Enum;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;

public class DonghuaDto
{
    public Guid DonghuaId { get; set; }
    public string Title { get; set; }
    public string Sinopse { get; set; }
    public string Studio { get; set; }
    public DateTime ReleaseYear { get; set; }
    public Genre Genres { get; set; }
    public DonghuaType Type { get; set; }
    public DonghuaStatus Status { get; set; }
    public string? Image { get; set; }
    public float Rating { get; set;}
}