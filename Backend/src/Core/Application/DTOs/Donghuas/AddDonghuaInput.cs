using DonghuaFlix.Backend.src.Core.Domain.Enum;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;

    public record AddDonghuaInput
    (
        string Title ,
        string? TitleEnglish  ,
        string? Description ,
        string Sinopse ,
        string Studio ,
        DateTime? ReleaseYear ,  
        Genre Genres , // Lista de nomes dos gêneros ,
        DonghuaType Type ,
        DonghuaStatus Status ,
        string? Banner ,
        string Image ,
        float Rating ,
        string? Trailer    
    
    );
//     // Propriedades necessárias para criar um Donghua