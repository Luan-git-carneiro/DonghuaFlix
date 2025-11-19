using DonghuaFlix.Backend.src.Core.Domain.Abstractions;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using DonghuaFlix.Backend.src.Core.Domain.Enum.Genre;
using DonghuaFlix.Backend.src.Core.Domain.Exceptions;

namespace DonghuaFlix.Backend.src.Core.Domain.Entities;

public class Donghua : Entity
{
   public string Title { get; private set; }
    public string? TitleEnglish { get; private set; }
    public string? Description { get; private set; }
    public string Image { get; private set; }
    public string? Banner { get; private set; }
    public float? Rating { get; private set; } 
    public DateTime? ReleaseDate { get; private set; }
    public DonghuaStatus? Status { get; private set; }
    public string? Studio { get; private set; }
    public List<Genre>? Genres { get; private set; }
    public string Sinopse { get; private set; }
    public string? Trailer { get; private set; }
    public DonghuaType Type { get; private set; }

    public Donghua() {}
    public Donghua(string title, string sinopse, DonghuaType type, List<Genre>? genre, float rating , string image)
    {
        ParamDonghuaIsNullOrWhiteSpace(nameof(title), title);
        ParamDonghuaIsNullOrWhiteSpace(nameof(sinopse), sinopse);
        
        Title = title;
        Sinopse = sinopse;
        Type = type;
        Genres = genre;
        Rating = rating;
        Image = image;
    }

    public Donghua(string title, string sinopse, string studio, DateTime? releaseDate, 
                  DonghuaType type, DonghuaStatus status, string image, List<Genre>? genres, 
                  float? rating, string? titleEnglish = null, string? description = null, 
                  string? banner = null, string? trailer = null)
    {
        ParamDonghuaIsNullOrWhiteSpace(nameof(title), title);
        ParamDonghuaIsNullOrWhiteSpace(nameof(sinopse), sinopse);
        
        Title = title;
        Sinopse = sinopse;
        Studio = studio;
        ReleaseDate = releaseDate;
        Type = type;
        Status = status;
        Image = image;
        Genres = genres;
        Rating = rating;
        TitleEnglish = titleEnglish;
        Description = description;
        Banner = banner;
        Trailer = trailer;
    }

    public void ParamDonghuaIsNullOrWhiteSpace(string param, string value)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            throw new DomainValidationException(field: param, message: $"{param} do donghua é obrigatório.");
        }
        else if(value.Length < 4)
        {
            throw new DomainValidationException(field: param, message: $"{param} do donghua deve conter no mínimo 4 caracteres.");
        }
    }

    public void UpdateTitle(string title)
    {
        ParamDonghuaIsNullOrWhiteSpace(nameof(title), title);
        Title = title;
    }

    public void UpdateTitleEnglish(string? titleEnglish)
    {
        TitleEnglish = titleEnglish;
    }

    public void UpdateDescription(string? description)
    {
        Description = description;
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

    public void UpdateReleaseDate(DateTime? releaseDate)
    {
        ReleaseDate = releaseDate;
    }

    public void UpdateGenres(List<Genre>? genres)
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

    public void UpdateBanner(string? banner)
    {
        Banner = banner;
    }

    public void UpdateRating(float? rating)
    {
        Rating = rating;
    }

    public void UpdateTrailer(string? trailer)
    {
        Trailer = trailer;
    }

    public void UpdateDonghua(string title, string sinopse, string studio, DateTime? releaseDate, 
                             DonghuaType type, DonghuaStatus status, string image , List<Genre>? genres, 
                             float? rating, string? titleEnglish = null, string? description = null, 
                             string? banner = null, string? trailer = null)
    {
        UpdateTitle(title);
        UpdateSinopse(sinopse);
        UpdateStudio(studio);
        UpdateReleaseDate(releaseDate);
        UpdateGenres(genres);
        UpdateType(type);
        UpdateStatus(status);
        UpdateImage(image);
        UpdateRating(rating);
        UpdateTitleEnglish(titleEnglish);
        UpdateDescription(description);
        UpdateBanner(banner);
        UpdateTrailer(trailer);
    }

}
