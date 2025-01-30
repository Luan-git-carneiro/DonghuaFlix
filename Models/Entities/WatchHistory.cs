namespace DonghuaFlix.Models
{
    public class WatchHistory
    {
        public int HistoryID { get; set; }
        // Relacionamento com o Usuário 
        public int UserId { get; set; }
        public User user { get; set; }

        // Relacionamento com o Vídeo (episódio específico)
        public int EpisodeId { get; set; }
        public Episode episode { get; set; }

        // Relacionamento com o Donghua
        public List<Donghua> donghua { get; set; }
    }
}