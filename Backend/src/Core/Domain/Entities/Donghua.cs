using DonghuaFlix.src.Core.Domain.Abstractions;
using DonghuaFlix.src.Core.Domain.Enum;
using DonghuaFlix.src.Core.Domain.Exceptions;

namespace DonghuaFlix.src.Core.Domain.Entities;

public class Donghua : Entity
{
    public  string Title { get; private set; }
    public string? Sinopse { get; private set; }
    public string? Studio { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public Genre Genres { get; private set; }
    public DonghuaType Type { get; private set; }
    public DonghuaStatus Status { get; private set; }
    public string? Image { get; private set; }
 
    public Donghua(string title, string sinopse, DonghuaType type, Genre genre)
    {
        ParamDonghuaIsNullOrWhiteSpace(nameof(title) , title);
        ParamDonghuaIsNullOrWhiteSpace( nameof(sinopse) , sinopse);
        
        Title = title;
        Sinopse = sinopse;
        Type = type;
        Genres = genre;

    }

    public Donghua( string title, string sinopse, string studio, int releaseDate, DonghuaType type, DonghuaStatus status, string image, Genre genres)
    {
        ParamDonghuaIsNullOrWhiteSpace( nameof(title) , title );
        ParamDonghuaIsNullOrWhiteSpace( nameof(sinopse) , sinopse );
        ValidateDate(releaseDate);
        
        Title = title;
        Sinopse = sinopse;
        Studio = studio;
        ReleaseDate = new DateTime(releaseDate, 1, 1);
        Type = type;
        Status = status;
        Image = image;
        Genres = genres;
        
    }

    public void ParamDonghuaIsNullOrWhiteSpace(string param , string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            throw new DomainValidationException( field: param , message: $"{param} do donghua é obrigatório." );
        }else if(value.Length < 4)
        {
            throw new DomainValidationException(field: param, message: $"{param} do donghua deve conter no mínimo 4 caracteres.");
        }
    }

    public void ValidateDate(int anoLancamento)
    {
        if(anoLancamento > DateTime.Now.Year)
        {
            throw new BusinessRulesException(rulesName: "ANO_NO_FUTURO", message: "Ano de lançamento do donghua não pode ser maior que o ano atual." );
        }
    }

    public void UpdateTitle(string title)
    {
        ParamDonghuaIsNullOrWhiteSpace(nameof(title), title);
        Title = title;
    }
    public void UpdateSinopse(string sinopse)
    {
        ParamDonghuaIsNullOrWhiteSpace(nameof(sinopse), sinopse);
        Sinopse = sinopse;
    }
    public void UpdateStudio(string studio)
    {
        ParamDonghuaIsNullOrWhiteSpace(nameof(studio), studio);
        Studio = studio;
    }
    public void UpdateReleaseDate(int releaseDate)
    {
        ValidateDate(releaseDate);
        ReleaseDate = new DateTime(releaseDate, 1, 1);
    }
    public void UpdateGenres(Genre genres)
    {
        Genres = genres;
    }
    public void UpdateType(DonghuaType type)
    {
        Type = type;
    }
    public void UpdateStatus(DonghuaStatus status)
    {
        Status = status;
    }
    public void UpdateImage(string image)
    {
        ParamDonghuaIsNullOrWhiteSpace(nameof(image), image);
        Image = image;
    }
    public void UpdateDonghua(string title, string sinopse, string studio, int releaseDate, DonghuaType type, DonghuaStatus status, string image, Genre genres)
    {
        UpdateTitle(title);
        UpdateSinopse(sinopse);
        UpdateStudio(studio);
        UpdateReleaseDate(releaseDate);
        UpdateGenres(genres);
        UpdateType(type);
        UpdateStatus(status);
        UpdateImage(image);
    }

}
