namespace DonghuaFlix.Models.Entities
{
    public class Comentario
    {
        public int CommentId { get; set; }
        public string Message { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;

        // Relacionamento com o Usuário que fez o comentário
        public int UserID { get; set; }
        public User user { get; set; }

        // Relacionamento com o Donghua (opcional, para comentários gerais)
        public int DonghuaID { get; set; }
        public Donghua donghua { get; set; }
        
        // Relacionamento com o Vídeo (episódio específico)
        public int EpisodeId { get; set; } // Chave estrangeira para o vídeo
        public Episode episode { get; set; } // Propriedade de navegação para o vídeo
    }

}