using DonghuaFlix.Backend.src.Core.Application.Helpers;

namespace DonghuaFlix.Backend.src.Core.Application.Interfaces;

public interface IApiResponse<T>
{
    /// <summary>
    /// suseco ou falha
    /// </summary>
    bool IsSucess { get; set; }

    /// <summary>
    /// Gets or sets the message of the response.
    /// </summary>
    string Message { get; set; }

    /// <summary>
    /// Gets or sets the data returned in the response.
    /// </summary>
    T? Data { get; set; }
    /// <summary>
    /// Gets or sets the error code, if any.
    /// /// </summary>
    string? ErrorCode { get; set; }
    /// <summary>
    /// lISTA DE LINKES
    /// /// </summary>
    List<Link>? Links { get; set; }
}