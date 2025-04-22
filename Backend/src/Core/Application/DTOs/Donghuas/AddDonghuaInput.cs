using DonghuaFlix.src.Core.Domain.Enum;

namespace DonghuaFlix.src.Core.Application.DTOs.Donghuas;

    public record AddDonghuaInput
    (
        string Title ,
        string Sinopse ,
        string Studio ,
        int ReleaseYear ,  // Note que usamos int em vez de DateTime
        List<string> Genres , // Lista de nomes dos gêneros ,
        string Type ,
        string Status ,
        string? Image 
    
    
    );
//     // Propriedades necessárias para criar um Donghua