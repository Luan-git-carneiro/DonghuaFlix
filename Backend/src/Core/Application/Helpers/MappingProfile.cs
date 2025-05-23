using AutoMapper;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Domain.Entities;

namespace DonghuaFlix.Backend.src.Core.Application.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Donghua, DonghuaDto>()
            .ForMember(dest => dest.DonghuaId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseDate));
        
    }
}