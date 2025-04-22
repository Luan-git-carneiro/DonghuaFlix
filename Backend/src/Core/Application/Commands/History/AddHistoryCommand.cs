using MediatR;

namespace DonghuaFlix.src.Core.Application.Commands.History;

public record AddHistoryCommand(
    Guid UserId,
    Guid EpisodeId
) : IRequest<Unit>;