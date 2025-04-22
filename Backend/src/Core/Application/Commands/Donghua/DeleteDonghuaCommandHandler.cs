using DonghuaFlix.src.Core.Aplication.Repositories;
using DonghuaFlix.src.Core.Application.Commands.Donghua;
using MediatR;

public class DeleteDonghuaCommandHandler : IRequestHandler<DeleteDonghuaCommand, Unit>
{
    private readonly IDonghuaRepository _donghuaRepository;
    // Injete serviços de autorização se necessário

    public DeleteDonghuaCommandHandler(IDonghuaRepository donghuaRepository)
    {
        _donghuaRepository = donghuaRepository;
    }

    public async Task<Unit> Handle(DeleteDonghuaCommand request, CancellationToken cancellationToken)
    {
        // 1. (Opcional mas bom) Verificar se o Donghua existe
        var donghua = await _donghuaRepository.GetByIdAsync(request.DonghuaId);
        if (donghua == null)
        {
            throw new KeyNotFoundException("Donghua não encontrado.");
        }

        // 2. Verificar permissão (MUITO IMPORTANTE para delete)
        // if (!await CanUserDelete(request.UserId, donghua))
        // {
        //     throw new UnauthorizedAccessException("Usuário não tem permissão para deletar este Donghua.");
        // }

        // 3. Executar a deleção
        await _donghuaRepository.DeleteAsync(donghua); // Ou talvez _donghuaRepository.DeleteByIdAsync(request.DonghuaId);
        // (Pode precisar de _unitOfWork.SaveChangesAsync() dependendo da sua implementação)

        // 4. Retornar sucesso (sem dados)
        return Unit.Value;
    }
}