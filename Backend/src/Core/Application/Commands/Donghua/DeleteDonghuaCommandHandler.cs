using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;
using MediatR;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using AutoMapper;

public class DeleteDonghuaCommandHandler : IRequestHandler<DeleteDonghuaCommand, ApiResponse<DonghuaDto>>
{
    private readonly IDonghuaRepository _donghuaRepository;
    private readonly IMapper _mapper;

    public DeleteDonghuaCommandHandler(IDonghuaRepository donghuaRepository , IMapper mapper)
    { 
   
        _donghuaRepository = donghuaRepository;
        _mapper = mapper ;
    }

    public async Task<ApiResponse<DonghuaDto>> Handle(DeleteDonghuaCommand request, CancellationToken cancellationToken)
    {
        // 1. (Opcional mas bom) Verificar se o Donghua existe
        var donghua = await _donghuaRepository.GetByIdAsync(request.DonghuaId);

        if (donghua == null)
        {
            var errorResponse = new ApiResponse<DonghuaDto>(
                sucess:false ,
                message: "Donghua não encontrado" ,
                data: null ,
                errorCode: "NOT_FOUND"
            );

            return errorResponse ;
        }

        // 3. Executar a deleção
        await _donghuaRepository.DeleteAsync(donghua); // Ou talvez _donghuaRepository.DeleteByIdAsync(request.DonghuaId);
        // (Pode precisar de _unitOfWork.SaveChangesAsync() dependendo da sua implementação)
        
        var response = new ApiResponse<DonghuaDto>(
            sucess: true , 
            message: "Donghua removido com sucesso" ,
            data: _mapper.Map<DonghuaDto>(donghua) ,
            errorCode: null
        );

        // 4. Retornar sucesso (sem dados)
        return response ;
    }
}