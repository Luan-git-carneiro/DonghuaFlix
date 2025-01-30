namespace DonghuaFlix.Models.Entities
{
    public class Donghua
    {
        public int DonghuaId { get; set; }
        public string Title { get; set; }
        public string TitleOriginal { get; set; }
        public string Description { get; set; }
        public string studio { get; set; }
        public string coverImage { get; set; }
        public DateTime releaseDate { get; set; }
        public DonghuaStatus status { get; set; }
        public List<Genre> genres { get; set; }
        public List<Episode> episodes { get; set; }

    }
}