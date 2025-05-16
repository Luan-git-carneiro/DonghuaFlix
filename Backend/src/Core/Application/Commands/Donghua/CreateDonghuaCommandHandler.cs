using DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Application.Repositories;
using MediatR;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;  

namespace DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;

public class CreateDonghuaCommandHandler : IRequestHandler<CreateDonghuaCommand, Unit>
{
    private readonly IDonghuaRepository _donghuaRepository;
    private readonly IUserRepository _userRepository;

    public CreateDonghuaCommandHandler(IDonghuaRepository donghuaRepository, IUserRepository userRepository)
    {
        _donghuaRepository = donghuaRepository;
        _userRepository = userRepository;
    }

   public async Task<Unit> Handle(CreateDonghuaCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        
        if (user == null)
            throw new DomainValidationException("User", "Usuário não encontrado");
            
        if (!user.Role.Equals("Admin"))
            throw new BusinessRulesException( "Só Admin" , "Apenas administradores podem criar donghuas");



        // Verifica se o título já existe no repositório
        var existingDonghua = await _donghuaRepository.AnyAsync(
            d => EF.Functions.Collate(d.Title, "SQL_Latin1_General_CP1_CI_AS") == request.Title
        );

        if(!existingDonghua)
        {
            throw new DomainValidationException(field: nameof(request.Title), message: "Título já existe");
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
        
        // Toda validação acontece dentro do construtor do Donghua
        
        await _donghuaRepository.AddAsync(donghua);
        
        return Unit.Value;
    }
}