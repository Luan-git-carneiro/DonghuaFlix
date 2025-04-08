using MediatR;

namespace DonghuaFlix.src.Core.Application.Commands.History;

public record AddHistoryCommand(
    Guid UserId,
    Guid DonghuaId,
    Guid EpisodeId
) : IRequest<Unit>;