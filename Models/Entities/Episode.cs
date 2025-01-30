namespace DonghuaFlix.Models.Entities
{
    public class Episode
    {
        public int EpisodeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public number Duration { get; set; }
        public number season { get; set; }
        public numeber EpisodeNumber { get; set; }
        public string VideoUrl { get; set; }
        public int DonghuaId { get; set; }
        public Donghua Donghua { get; set; }
        public string urlThumbnail { get; set; }
    }
}