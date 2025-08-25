using DonghuaFlix.Backend.src.Core.Application.Interfaces;

namespace DonghuaFlix.Backend.src.Core.Application.Helpers;

public class ApiResponse<T> : IApiResponse<T>
{
    public bool IsSucess { get; set ; }
    public string Message { get; set; }
    public T? Data { get; set ;}
    public List<Link> Links { get; set;} = new List<Link>();
    public string? ErrorCode { get; set; } = null;


    public ApiResponse(bool sucess , string message , T? data , string? errorCode  )
    {
        IsSucess = sucess;
        Message = message;
        Data = data;
        ErrorCode = errorCode != null ? errorCode : null;
    }

    public ApiResponse(T data) : this(true, "Operação realizada com sucesso", data , null)
    {
    }

    public void AddLink(Link link)
    {
        Links.Add(link);
    }

    public void AddLinks(IEnumerable<Link> links)
    {
        Links.AddRange(links);
    }

}