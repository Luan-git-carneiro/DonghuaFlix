using DonghuaFlix.src.Core.Aplication.Repositories;
using DonghuaFlix.src.Core.Domain.Exceptions;
using MediatR;

namespace DonghuaFlix.src.Core.Aplication.Commands.Favorites;

public class AddFavoriteCommandHandler : IRequestHandler<AddFavoriteCommand, Unit>
{
    private readonly IUserRepository _usuarioRepo;
    private readonly IDonghuaRepository _donghuaRepo;

    public AddFavoriteCommandHandler(IUserRepository usuarioRepo, IDonghuaRepository donghuaRepo)
    {
        _usuarioRepo = usuarioRepo;
        _donghuaRepo = donghuaRepo;
    }

    public async Task<Unit> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepo.GetByIdAsync(request.UserId);
        var donghua = await _donghuaRepo.GetByIdAsync(request.DonghuaId);

        if (usuario == null || donghua == null)
        {
            throw new DomainValidationException( field: nameof(usuario) +  "ou" + nameof(donghua)  , message: "Usuario ou Donghua no encontrado");
        }

        usuario.AddFavorite(donghua);

        await _usuarioRepo.UpdateAsync(usuario);

        return Unit.Value;
    }
}