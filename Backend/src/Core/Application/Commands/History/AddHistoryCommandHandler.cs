using DonghuaFlix.src.Core.Aplication.Repositories;
using DonghuaFlix.src.Core.Domain.Exceptions;
using MediatR;

namespace DonghuaFlix.src.Core.Application.Commands.History;

public class AddHistoryCommandHandler : IRequestHandler< AddHistoryCommand , Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IDonghuaRepository _donghuaRepository;

    public AddHistoryCommandHandler(IUserRepository userRepository, IDonghuaRepository donghuaRepository)
    {
        _userRepository = userRepository;
        _donghuaRepository = donghuaRepository;
    }

    public async Task<Unit> Handle(AddHistoryCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        var donghua = await _donghuaRepository.GetByIdAsync(request.DonghuaId);
        var episode = donghua.Episodes.FirstOrDefault(e => e.Id == request.EpisodeId);

        if (user == null || donghua == null || episode == null)
            throw new DomainValidationException(field: "PARAMETRO NULO" , "Dados inválidos para histórico");

        user.AddHistory(episode.Id, DateTime.UtcNow);
        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}