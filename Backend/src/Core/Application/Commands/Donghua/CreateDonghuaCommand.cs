using DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;
using DonghuaFlix.Backend.src.Core.Application.Helpers;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using MediatR;

namespace DonghuaFlix.Backend.src.Core.Application.Commands.Donghua;

public class CreateDonghuaCommand : IRequest<ApiResponse<DonghuaDto>>
{
    // Propriedades necessárias para criar um Donghua
    public string Title { get; set; }
    public string Sinopse { get; set; }
    public string Studio { get; set; }
    public DateTime? ReleaseYear { get; set; }  // Note que usamos int em vez de DateTime
    public Genre Genres { get; set; }  // Lista de nomes dos gêneros
    public  DonghuaType Type { get; set; }
    public DonghuaStatus  Status { get; set; }
    public string? Image { get; set; }
    public float? Rating { get; set; }

    public CreateDonghuaCommand( string title , string sinopse, string studio , DateTime? releaseYear , Genre genres , DonghuaType type, DonghuaStatus status , string? imaagem , float rating)
    {
        Title = title;
        Sinopse = sinopse;
        Studio = studio ;
        ReleaseYear = releaseYear;
        Genres = genres;
        Type = type;
        Status = status;
        Image = imaagem;
        Rating = rating;
    }
}