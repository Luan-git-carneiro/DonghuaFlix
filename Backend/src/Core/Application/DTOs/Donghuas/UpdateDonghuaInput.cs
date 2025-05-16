using DonghuaFlix.Backend.src.Core.Domain.Enum;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas
{
    public record UpdateDonghuaInput
    (
        string? Title,
        string? Sinopse,
        string? Studio,
        DateTime? ReleaseYear,  // Note que usamos int em vez de DateTime
        Genre? Genres,
        DonghuaType? Type,
        DonghuaStatus? Status,
        string? Image
    );
}