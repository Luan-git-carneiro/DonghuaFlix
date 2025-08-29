using DonghuaFlix.Backend.src.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DonghuaFlix.Backend.src.Core.Application.Helpers;

public class HateoasHelper :  IHateoasProvider
{
    private readonly IUrlHelper _urlHelper;
    public HateoasHelper(IUrlHelper urlHelper)
    {
        _urlHelper = urlHelper;
    }

    public IEnumerable<Link> GenerateLinks(string controllerName, Guid id , object? additionalData )
    {
        var links = new List<Link>();
       // Adiciona o link "self"
        var selfLink = _urlHelper.Link($"Get{controllerName}", new { id });
        if (!string.IsNullOrEmpty(selfLink))
        {
            links.Add(new Link(selfLink, "self", "GET"));
        }

        // Adiciona o link "update"
        var updateLink = _urlHelper.Link($"Update{controllerName}", new { id });
        if (!string.IsNullOrEmpty(updateLink))
        {
            links.Add(new Link(updateLink, "update", "PUT"));
        }

        // Adiciona o link "delete"
        var deleteLink = _urlHelper.Link($"Delete{controllerName}", new { id });
        if (!string.IsNullOrEmpty(deleteLink))
        {
            links.Add(new Link(deleteLink, "delete", "DELETE"));
        }

        // Adiciona links específicos para o controller "Donghua"
        if (controllerName.Equals("Donghua", StringComparison.OrdinalIgnoreCase))
        {
            var addToFavoritesLink = _urlHelper.Link("Favorite", new { donghuaId = id });
            if (!string.IsNullOrEmpty(addToFavoritesLink))
            {
                links.Add(new Link(addToFavoritesLink, "add-to-favorites", "POST"));
            }
        }
    
        return links;
    
    }
}