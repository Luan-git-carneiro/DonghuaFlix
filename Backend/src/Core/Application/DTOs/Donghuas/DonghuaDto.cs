

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;

public record DonghuaDto
(
    Guid DonghuaId,
    string Title,
    string Sinopse,
    string Studio,
    int ReleaseYear,  // Note que usamos int em vez de DateTime
    List<string> Genres, // Lista de nomes dos gêneros 
    string Type,
    string Status,
    string? Image
);