namespace DonghuaFlix.src.Core.Aplication.DTOs.History;

public record AddHistoryInput(
    Guid UserId,
    Guid DonghuaId,
    Guid EpisodeId
);