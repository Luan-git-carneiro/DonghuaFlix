using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;
using MediatR;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;

public class DeleteDonghuaCommandHandler : IRequestHandler<DeleteDonghuaCommand, Unit>
{
    private readonly IDonghuaRepository _donghuaRepository;
    // Injete serviços de autorização se necessário
    private readonly IUserRepository _userRepository;

    public DeleteDonghuaCommandHandler(IDonghuaRepository donghuaRepository , IUserRepository userRepository)
    { 
        _userRepository = userRepository;   
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
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new DomainValidationException(field: "USER_NOT_FOUND" ,"Usuário não encontrado.");
        }

        if(!user.Role.Equals("Admin"))
        {
            throw new BusinessRulesException(rulesName: "Só Admin" , message: "Apenas administradores podem deletar donghuas");
        }

        // 3. Executar a deleção
        await _donghuaRepository.DeleteAsync(donghua); // Ou talvez _donghuaRepository.DeleteByIdAsync(request.DonghuaId);
        // (Pode precisar de _unitOfWork.SaveChangesAsync() dependendo da sua implementação)

        // 4. Retornar sucesso (sem dados)
        return Unit.Value;
    }
}