using MediatR;
using AutoMapper;
using DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Application.Helpers;

namespace DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.GetDonghua;

public class GetDonghuaByIdQueryHandler : IRequestHandler<GetDonghuaByIdQuery, ApiResponse<DonghuaDto>>
{
    private readonly IDonghuaRepository _donghuaRepository;
    private readonly IMapper _mapper;

    public GetDonghuaByIdQueryHandler(IDonghuaRepository donghuaRepository, IMapper mapper)
    {
        _donghuaRepository = donghuaRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<DonghuaDto>> Handle(GetDonghuaByIdQuery request, CancellationToken cancellationToken)
    {
        var donghua = await _donghuaRepository.GetByIdAsync(request.DonghuaId);

        if (donghua == null)
        {
            var response  =  new ApiResponse<DonghuaDto>(
                        sucess: false,
                        message: "Donghua não encontrado",
                        data: null,
                        errorCode: "NOT_FOUND"
                    );

            return response;
        }


        
        return new ApiResponse<DonghuaDto>(
                sucess: true ,
                message: "Donghua encontrado com sucesso",
                data: _mapper.Map<DonghuaDto>(donghua),
                errorCode: null
        );
    }
}