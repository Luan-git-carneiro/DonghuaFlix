

using DonghuaFlix.Backend.src.Core.Domain.Enum;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;

public class DonghuaDto
{
    public Guid DonghuaId { get; set; }
    public required string Title { get; set; }
    public string? TitleEnglish { get; set; }
    public string? Description { get; set; }
    public required string Sinopse { get; set; }
    public string? Studio { get; set; }
    public DateTime ReleaseYear { get; set; }
    public Genre Genres { get; set; }
    public DonghuaType Type { get; set; }
    public DonghuaStatus Status { get; set; }
    public required string Image { get; set; }
    public string? Banner { get; set; }
    public float Rating { get; set;}
    public string? Trailer { get; set; }
}