//Logica de execução da query

using AutoMapper;
using DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua.DTOs;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries.ListDonghua;

public class ListDonghuasPagedQueryHandler : IRequestHandler<ListDonghuasPagedQuery , PagedDonghuaResultDto>
{
    private readonly IDonghuaRepository _donghuaRepository;
    private readonly IMapper _mapper;

    public ListDonghuasPagedQueryHandler(IDonghuaRepository donghuaRepository, IMapper mapper)
    {
        _donghuaRepository = donghuaRepository;
        _mapper = mapper;
    }

    public async Task<PagedDonghuaResultDto> Handle( ListDonghuasPagedQuery request , CancellationToken cancellationToken)
    {
        // Buscar dados paginados no repositorio
        var (donghuas, TotalItems) = await _donghuaRepository.GetPagedAsync(request.Page, request.PageSize, request.SearchTerm);

        //Mapeia para DTO
        var dto = new PagedDonghuaResultDto
        {
            Donghuas = _mapper.Map<List<DonghuaDto>>(donghuas),
            TotalItems = TotalItems,
            CurrentPage = request.Page,
            TotalPages = (int)Math.Ceiling((double)TotalItems / request.PageSize)
        };

        return dto;

    }
        
}