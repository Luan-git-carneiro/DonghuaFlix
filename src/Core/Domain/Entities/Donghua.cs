using DonghuaFlix.src.Core.Domain.Enum;
using DonghuaFlix.src.Core.Domain.Exceptions;

namespace DonghuaFlix.src.Core.Domain.Entities;

public class Donghua
{
    public Guid IdDonghua { get; private set; }
    public string Title { get; private set; }
    public string Sinopse { get; private set; }
    public string? Studio { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    public Genre Genres { get; private set; }
    public DonghuaType Type { get; private set; }
    public DonghuaStatus Status { get; private set; }
    public string? Image { get; private set; }


    public Donghua(string title, string sinopse, DonghuaType type, Genre genre)
    {
        ParamDonghuaIsNullOrWhiteSpace(title, "Título");
        ParamDonghuaIsNullOrWhiteSpace(sinopse, "Sinopse");
        
        Title = title;
        Sinopse = sinopse;
        Type = type;
        Genres = genre;
        IdDonghua = Guid.NewGuid();
    }

    public Donghua( string title, string sinopse, string studio, int releaseDate, DonghuaType type, DonghuaStatus status, string image, Genre genres)
    {
        ParamDonghuaIsNullOrWhiteSpace(title,"Título");
        ParamDonghuaIsNullOrWhiteSpace(sinopse, "Sinopse");
        ValidateDate(releaseDate);
        
        Title = title;
        Sinopse = sinopse;
        Studio = studio;
        ReleaseDate = new DateTime(releaseDate, 1, 1);
        Type = type;
        Status = status;
        Image = image;
        Genres = genres;
        IdDonghua = Guid.NewGuid();
    }

    public void ParamDonghuaIsNullOrWhiteSpace(string value,string param)
    {
        if(string.IsNullOrWhiteSpace(value))
        {
            throw new DonghuaValidationException($"{param} do donghua é obrigatório.");
        }else if(value.Length < 4)
        {
            throw new DonghuaValidationException($"{param} do donghua deve conter no mínimo 4 caracteres.");
        }
    }

    public void ValidateDate(int anoLancamento)
    {
        if(anoLancamento > DateTime.Now.Year)
        {
            throw new DonghuaValidationException("Ano de lançamento do donghua não pode ser maior que o ano atual.");
        }
    }
        
    

}
