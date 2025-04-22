using DonghuaFlix.src.Core.Domain.Entities;
using DonghuaFlix.src.Core.Aplication.Repositories;
using MediatR;
using DonghuaFlix.src.Core.Domain.Exceptions;
using DonghuaFlix.src.Core.Domain.Enum;
using DonghuaFlix.src.Core.Application.Helpers;

namespace DonghuaFlix.src.Core.Application.Commands.Donghua;

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


        //tratamento do enem genres
        Genre genresForEntity = GenreConverter.ConvertStringsToGenreFlags(request.Genres);
        // Criamos a entidade Donghua usando seu construtor
        var donghua = new Donghua(

            title: request.Title,
            sinopse: request.Sinopse,
            studio: request.Studio,
            releaseDate: request.ReleaseYear,
            type: request.Type,
            status: request.Status,
            image: request.Image,
            genres: genresForEntity
        );
        
        // Toda validação acontece dentro do construtor do Donghua
        
        await _donghuaRepository.AddAsync(donghua);
        
        return Unit.Value;
    }
}