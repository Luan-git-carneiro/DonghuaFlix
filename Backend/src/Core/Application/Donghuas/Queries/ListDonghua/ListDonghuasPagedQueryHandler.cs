//Logica de execução da query

using AutoMapper;
using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua.DTOs;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.Entities;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua;

public class ListDonghuasPagedQueryHandler : IRequestHandler<ListDonghuasPagedQuery , ApiResponse<PagedResult<DonghuaDto>>>
{
    private readonly IDonghuaRepository _donghuaRepository;
    private readonly IMapper _mapper;

    public ListDonghuasPagedQueryHandler(IDonghuaRepository donghuaRepository, IMapper mapper)
    {
        _donghuaRepository = donghuaRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PagedResult<DonghuaDto>>> Handle( ListDonghuasPagedQuery request , CancellationToken cancellationToken)
    {
        // Buscar dados paginados no repositorio
        var (donghuas, TotalItems) = await _donghuaRepository.GetPagedAsync(request.Page, request.PageSize, request.SearchTerm);

        if(TotalItems == 0)
        {
                var responseErro = new ApiResponse<PagedResult<DonghuaDto>>(
                sucess: false,
                message: "Não encontrado nenhum registro" ,
                data: null,
                errorCode: "NOT_FOUND"
            );

            return responseErro;
        }

        //DECLARANDO A LISTA DE DONGHUAS  PARA TRANSFORMA EM DADO DTO
        List<DonghuaDto> donghuaDtos = new List<DonghuaDto>();

        foreach(Donghua donghua in donghuas) 
        {
            donghuaDtos.Add(_mapper.Map<DonghuaDto>(donghua));
        }

        var response = new ApiResponse<PagedResult<DonghuaDto>>(
            sucess: true,
            message: "Lista de Donghuas" ,
            data: new PagedResult<DonghuaDto>(
                items: donghuaDtos ,
                count: TotalItems ,
                pageNumber: request.Page,
                pageSize: request.PageSize
             ),
            errorCode: null
        );

        return response;

    }
        
}