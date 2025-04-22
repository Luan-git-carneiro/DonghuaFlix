using DonghuaFlix.src.Core.Aplication.Repositories;
using DonghuaFlix.src.Core.Application.Repositories;
using DonghuaFlix.src.Core.Domain.Entities;
using DonghuaFlix.src.Core.Domain.Exceptions;
using MediatR;

namespace DonghuaFlix.src.Core.Application.Commands.History;

public class AddHistoryCommandHandler : IRequestHandler< AddHistoryCommand , Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IEpisodeRepository _episodeRepository;


    public AddHistoryCommandHandler(IUserRepository userRepository, IEpisodeRepository episodeRepository )
    {
        _userRepository = userRepository;
        _episodeRepository = episodeRepository;

    }

    public async Task<Unit> Handle(AddHistoryCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        var episode = await _episodeRepository.GetById(request.EpisodeId);


        if (user == null || episode == null )
            throw new DomainValidationException(field: "PARAMETRO NULO" , "Dados inválidos para histórico");

        user.AddHistory(episode.Id, DateTime.UtcNow);
        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}
