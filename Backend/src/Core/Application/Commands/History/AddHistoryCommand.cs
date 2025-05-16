using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.History;

public record AddHistoryCommand(
    Guid UserId,
    Guid EpisodeId
) : IRequest<Unit>;