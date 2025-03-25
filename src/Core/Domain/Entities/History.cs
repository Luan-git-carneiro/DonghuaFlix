using DonghuaFlix.src.Core.Domain.Abstractions;
using DonghuaFlix.src.Core.Domain.Exceptions;

namespace DonghuaFlix.src.Core.Domain.Entities;

public class History : Entity
{
    public Guid IdUser { get; private set; }
    public Guid IdEpisode { get; private set; }
    public DateTime Date { get; private set; }
    public TimeSpan Progress { get; private set; }

    //construtor privado para o EF
    private History() { }

    public History(Guid idUser, Guid idEpisode, TimeSpan progress)
    {
        IdUser = idUser;
        IdEpisode = idEpisode;
        Progress = progress;
        Date = DateTime.UtcNow;
    }

    public void UpdateProgress(TimeSpan progress)
    {
        if (progress > TimeSpan.FromHours(4))
            throw new DomainValidationException(nameof(progress), "Tempo inválido");

        Progress = progress;
        Date = DateTime.UtcNow;
    }
}