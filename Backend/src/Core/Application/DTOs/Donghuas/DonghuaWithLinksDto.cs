using DonghuaFlix.Backend.src.Core.Application.Helpers;

namespace DonghuaFlix.Backend.src.Core.Application.DTOs.Donghuas;

public class DonghuaWithLinksDto
{
    public DonghuaDto Donghua { get ; set;}
    public List<Link> Links { get; set;} = new List<Link>();
}