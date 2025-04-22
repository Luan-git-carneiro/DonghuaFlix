using DonghuaFlix.src.Core.Domain.Enum;

namespace DonghuaFlix.src.Core.Application.DTOs.Donghuas
{
    public record UpdateDonghuaInput
    (
        string? Title,
        string? Sinopse,
        string? Studio,
        int? ReleaseYear,  // Note que usamos int em vez de DateTime
        List<string> Genres,
        string? Type,
        string? Status,
        string? Image
    );
}