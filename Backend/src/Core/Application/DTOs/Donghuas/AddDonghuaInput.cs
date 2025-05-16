using DonghuaFlix.Backend.src.Core.Domain.Enum;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;

    public record AddDonghuaInput
    (
        string Title ,
        string Sinopse ,
        string Studio ,
        DateTime? ReleaseYear ,  // Note que usamos int em vez de DateTime
        Genre Genres , // Lista de nomes dos gêneros ,
        DonghuaType Type ,
        DonghuaStatus Status ,
        string? Image 
    
    
    );
//     // Propriedades necessárias para criar um Donghua