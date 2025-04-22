using MediatR;
using DonghuaFlix.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.src.Core.Domain.Enum;
using DonghuaFlix.src.Core.Aplication.Repositories;
using DonghuaFlix.src.Core.Domain.Exceptions;
using DonghuaFlix.src.Core.Application.Helpers;

namespace DonghuaFlix.src.Core.Application.Commands.Donghua;

public class UpdateDonghuaCommandHandler : IRequestHandler<UpdateDonghuaCommand, Unit>
{
    readonly  IDonghuaRepository _donghuaRepository;
  

    public UpdateDonghuaCommandHandler( IDonghuaRepository donghuaRepository)
    {

        _donghuaRepository = donghuaRepository;
    }

    public async Task<Unit> Handle(UpdateDonghuaCommand request, CancellationToken cancellationToken)
    {


        if(request.DonghuaId == Guid.Empty)                     //validação
            throw new DomainValidationException(field: "DonghuaId", "DonghuaId não pode ser vazio");

        var donghua = await _donghuaRepository.GetByIdAsync(request.DonghuaId);         //carregar donghua  
        
        if (donghua == null)
            throw new DomainValidationException(field: "DonghuaId", "Donghua não encontrado");

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
             // Converter array de strings para enum Flags
            Genre newGenres = GenreConverter.ConvertStringsToGenreFlags(request.Genres);
            donghua.UpdateGenres(newGenres); // Usa o método da entidade
        }

        if (request.Type != null)
            {
                var type = Enum.Parse<DonghuaType>(request.Type, true);

                donghua.UpdateType(type);
            }

        if (request.Status != null)
            {
                var status = Enum.Parse<DonghuaStatus>(request.Status, true);

                donghua.UpdateStatus(status);
            }

        if (request.Image != null)
            donghua.UpdateImage(request.Image);

        await  _donghuaRepository.UpdateAsync(donghua); // Atualiza o donghua no repositório
                

        return Unit.Value;
        
    }




}
