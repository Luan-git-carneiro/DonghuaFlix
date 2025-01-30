namespace DonghuaFlix.Models.Entities:

public class DonghuaCategoria
{
    public int DonghuaId { get; set; }
    public Donghua donghua { get; set; } = null!;

    public int CategoryId { get; set; }
    public Category category { get; set; } = null!;
}