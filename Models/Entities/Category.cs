namespace DonghuaFlix.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<Donghua> Donghuas { get; set; }
    }
}