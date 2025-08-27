using DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using MediatR;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using AutoMapper;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;

public class CreateDonghuaCommandHandler : IRequestHandler<CreateDonghuaCommand, ApiResponse<DonghuaDto>>
{
    private readonly IDonghuaRepository _donghuaRepository;
    private readonly IMapper _mapper ;

    public CreateDonghuaCommandHandler(IDonghuaRepository donghuaRepository , IMapper mapper)
    {
        _donghuaRepository = donghuaRepository;
        _mapper = mapper ;

    }

   public async Task<ApiResponse<DonghuaDto>> Handle(CreateDonghuaCommand request, CancellationToken cancellationToken)
    {

        // Verifica se o título já existe no repositório
        var existingDonghua = await _donghuaRepository.AnyAsync(
            d => EF.Functions.Collate(d.Title, "SQL_Latin1_General_CP1_CI_AS") == request.Title
        );

        if(!existingDonghua)
        {
            var responseError = new ApiResponse<DonghuaDto>(
                sucess: false,
                message: "Donghua ja existe no Sitema" ,
                data: null,
                errorCode: "409_CONFLITCT_DONGHUA_ALREADY_EXISTS"
            ); 

            return responseError ; 
        }

        // Criamos a entidade Donghua usando seu construtor
        var donghua = new Backend.src.Core.Domain.Entities.Donghua(

            title: request.Title,
            sinopse: request.Sinopse,
            studio: request.Studio,
            releaseDate:  request.ReleaseYear != null ? request.ReleaseYear.Value : DateTime.Now,
            type: request.Type,
            status: request.Status,
            image: string.IsNullOrEmpty(request.Image) ?  request.Image : null,
            genres: request.Genres
        );
        
        await _donghuaRepository.AddAsync(donghua);

        //Criar a resposta
        var response = new ApiResponse<DonghuaDto>(
            sucess: true ,
            message: "Donghua criado com sucesso" , 
            data: _mapper.Map<DonghuaDto>(donghua),
            errorCode: null 
        );
        
        return response;
    }
}