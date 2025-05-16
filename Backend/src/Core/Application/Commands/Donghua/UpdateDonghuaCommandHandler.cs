using MediatR;
using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using DonghuaFlix.Backend.src.Core.Application.Helpers;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;

public class UpdateDonghuaCommandHandler : IRequestHandler<UpdateDonghuaCommand, Unit>
{
    readonly  IDonghuaRepository _donghuaRepository;
    readonly IUserRepository _userRepository;
  

    public UpdateDonghuaCommandHandler( IUserRepository userRepository ,IDonghuaRepository donghuaRepository)
    {
        _userRepository = userRepository;
        _donghuaRepository = donghuaRepository;
    }

    public async Task<Unit> Handle(UpdateDonghuaCommand request, CancellationToken cancellationToken)
    {
        // Verifica se o usuário existe
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new DomainValidationException(field: "UserId", "Usuário não encontrado");

        // Verifica se o usuário tem permissão para atualizar
        if (!user.Role.Equals("Admin"))
            throw new BusinessRulesException(rulesName: "Só Admin", message: "Apenas administradores podem atualizar donghuas");


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
                

        return Unit.Value;
        
    }




}
