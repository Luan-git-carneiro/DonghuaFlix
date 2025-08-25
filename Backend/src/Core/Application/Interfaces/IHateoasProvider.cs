using DonghuaFlix.Backend.src.Core.Application.Helpers;

namespace DonghuaFlix.Backend.src.Core.Application.Interfaces;

public interface IHateoasProvider
{
    IEnumerable<Link> GenerateLinks (string controllerName, Guid id , object? additionalData );

}