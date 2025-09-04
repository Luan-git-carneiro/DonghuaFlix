using MediatR;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using AutoMapper;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;

public class UpdateDonghuaCommandHandler : IRequestHandler<UpdateDonghuaCommand, ApiResponse<DonghuaDto>>
{
    readonly  IDonghuaRepository _donghuaRepository;
    readonly IMapper  _mapper;
  

    public UpdateDonghuaCommandHandler( IDonghuaRepository donghuaRepository , IMapper mapper)
    {
        _donghuaRepository = donghuaRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<DonghuaDto>> Handle(UpdateDonghuaCommand request, CancellationToken cancellationToken)
    {

        var donghua = await _donghuaRepository.GetByIdAsync(request.DonghuaId.HasValue ? request.DonghuaId.Value : Guid.Empty);

        if(donghua == null)                     //validação
        {
            var errorResult = new ApiResponse<DonghuaDto>(
                sucess: false ,
                message: "Donghua com Id passado não encontrado" ,
                data: null ,
                errorCode: "NOT_FOUND" 
            );

            return errorResult ;
        }

        if (request.Title != null)
                donghua.UpdateTitle(request.Title);

        if (request.Sinopse != null)
                donghua.UpdateSinopse(request.Sinopse);

        if (request.Studio != null) 
                donghua.UpdateStudio(request.Studio);

        if (request.releaseDate != null)
        {
            donghua.UpdateReleaseDate(request.releaseDate.Value);
        }

        // Genres
        if(request.Genres != null)
        {
            Genre genre = (Genre)request.Genres ;   // Converte o valor para o tipo Genre e não Genre?
             
            donghua.UpdateGenres(genre); // Usa o método da entidade
        }

        if (request.Type != null)
            {
                DonghuaType type = (DonghuaType)request.Type; // Converte o valor para o tipo DonghuaType diferente de DonghuaType?
                
                donghua.UpdateType(type);
            }

        if (request.Status != null)
            {
                DonghuaStatus status = (DonghuaStatus)request.Status;  // Converte o valor para o tipo DonghuaStatus diferente de DonghuaStatus?
                
                donghua.UpdateStatus(status);
            }

        if (request.Image != null)
            donghua.UpdateImage(request.Image);

        await  _donghuaRepository.UpdateAsync(donghua); // Atualiza o donghua no repositório
                

        return new ApiResponse<DonghuaDto>(
            sucess: true ,
            message: "Donghua atualizado com sucesso" ,
            data: _mapper.Map<DonghuaDto>(donghua) ,
            errorCode: null 
        );
        
    }




}
