using MediatR;
using AutoMapper;
using DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Application.Repositories;

namespace DonghuaFlix.Backend.src.Core.Application.Donghuas.Queries;

public class GetDonghuaByIdQueryHandler : IRequestHandler<GetDonghuaByIdQuery, DonghuaDto>
{
    private readonly IDonghuaRepository _donghuaRepository;
    private readonly IMapper _mapper;

    public GetDonghuaByIdQueryHandler(IDonghuaRepository donghuaRepository, IMapper mapper)
    {
        _donghuaRepository = donghuaRepository;
        _mapper = mapper;
    }

    public async Task<DonghuaDto> Handle(GetDonghuaByIdQuery request, CancellationToken cancellationToken)
    {
        var donghua = await _donghuaRepository.GetByIdAsync(request.DonghuaId);

        if (donghua == null)
        {
            throw new KeyNotFoundException($"Donghua with ID {request.DonghuaId} not found.");
        }

        return _mapper.Map<DonghuaDto>(donghua);
    }
}