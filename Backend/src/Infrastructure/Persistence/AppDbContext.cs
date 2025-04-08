using DonghuaFlix.src.Core.Domain.Entities;
using DonghuaFlix.src.Core.Domain.Enum;
using DonghuaFlix.src.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonghuaFlix.src.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
   // DbSets APENAS para Raízes de Agregado
    public DbSet<User> Users { get; set; }
    public DbSet<Donghua> Donghuas { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    // DbSet<VideoAsset> REMOVIDO

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // É uma boa prática chamar o método base
        base.OnModelCreating(modelBuilder);

        // --- Configuração de User (Raiz de Agregado) ---
        modelBuilder.Entity<User>(u =>
        {
            u.ToTable("Users"); // Nome explícito da tabela
            u.HasKey(user => user.Id);

            // Value Object Email (Owned)
            u.OwnsOne(user => user.Email, e =>
            {
                e.Property(email => email.Valor)
                    .HasColumnName("Email") // Nome da coluna no DB
                    .HasMaxLength(254)      // Tamanho padrão para emails
                    .IsRequired();
                // Garante que emails sejam únicos
                e.HasIndex(email => email.Valor).IsUnique();
            });

            // Value Object Password (Owned)
            u.OwnsOne(user => user.Password, p =>
            {
                p.Property(pw => pw.Value)
                    .HasColumnName("PasswordHash") // Nome da coluna no DB
                    .HasMaxLength(60) // Tamanho padrão do hash BCrypt
                    .IsRequired();
                 
             });

            // Conversão de Enums
            u.Property(user => user.Role)
                .HasConversion(
                    v => v.ToString(), // Salva como string
                    v => (UserRole)Enum.Parse(typeof(UserRole), v)) // Converte de volta para Enum
                .HasMaxLength(50) // Definir tamanho para a string no DB
                .HasDefaultValue(UserRole.Visistante) // Valor padrão
                .IsRequired();

            u.Property(user => user.Status)
                .HasConversion<int>() // Salva como inteiro
                .HasDefaultValue(AccountStatus.Active) // Valor padrão
                .IsRequired();

            // Coleção de Value Objects Favorites (Owned)
            u.OwnsMany(user => user.Favorites, fav =>
            {
                fav.ToTable("UserFavorites"); // Tabela para os favoritos
                fav.WithOwner().HasForeignKey("UserId"); // Chave estrangeira para User

                // Mapear propriedades do VO Favorite
                fav.Property(f => f.DonghuaId).IsRequired();
                fav.Property(f => f.DateCreat).IsRequired(); // Exemplo de propriedade adicional

                // Chave Primária Composta para a entrada na coleção de favoritos
                fav.HasKey("UserId", "DonghuaId");
            });

            // Coleção de Value Objects Histories (Owned)
            u.OwnsMany(user => user.Histories, hist =>
            {
                hist.ToTable("UserHistories"); // Tabela para o histórico
                hist.WithOwner().HasForeignKey("UserId"); // Chave estrangeira para User

                // Mapear propriedades do VO History
                hist.Property(h => h.EpisodeId).IsRequired();
                hist.Property(h => h.Progress).HasDefaultValue(TimeSpan.Zero); // Ex: Tempo assistido
                hist.Property(h => h.LastWatchedAt).IsRequired();

                // Chave Primária Composta (assumindo uma entrada por usuário/episódio)
                hist.HasKey("UserId", "EpisodeId");
            });

            // Mapear outras propriedades do User (Nome, Sobrenome, etc.)
            u.Property(user => user.FirstName).HasMaxLength(100);
            u.Property(user => user.LastName).HasMaxLength(100);
            u.Property(user => user.CreatedAt).IsRequired();

        });

        // --- Configuração de Donghua (Raiz de Agregado) ---
        modelBuilder.Entity<Donghua>(d =>
        {
            d.ToTable("Donghuas");
            d.HasKey(donghua => donghua.Id);

            // Relação com Episodes foi REMOVIDA daqui (Opção 2)
            // A relação é definida pela FK 'DonghuaId' na entidade Episode

            // Mapear propriedades do Donghua
            d.Property(donghua => donghua.Title).IsRequired().HasMaxLength(250);
            d.Property(donghua => donghua.Synopsis).HasMaxLength(2000); // Ou tipo TEXT/NVARCHAR(MAX)
            d.Property(donghua => donghua.CoverImageUrl).HasMaxLength(500);

            d.Property(donghua => donghua.Status)
                .HasConversion<int>()
                .IsRequired();
             d.Property(donghua => donghua.Type)
                .HasConversion<int>()
                .IsRequired();
            // Para Genres, se for uma lista simples de Enums, pode usar OwnsMany ou uma tabela de junção
            // Exemplo simplificado (requer mais detalhes se for coleção):
            // d.Property(donghua => donghua.Genres) ...

            d.Property(donghua => donghua.ReleaseYear);
            d.Property(donghua => donghua.CreatedAt).IsRequired();
        });

        // --- Configuração de Episode (Raiz de Agregado) ---
        modelBuilder.Entity<Episode>(e =>
        {
            e.ToTable("Episodes");
            e.HasKey(ep => ep.Id);

            // Chave Estrangeira para Donghua (define a relação)
            e.Property(ep => ep.DonghuaId).IsRequired();

            // Opcional: Configuração explícita da relação (geralmente não necessário)
            // e.HasOne<Donghua>().WithMany().HasForeignKey(ep => ep.DonghuaId);

            // Mapear propriedades do Episode
            e.Property(ep => ep.EpisodeNumber).IsRequired();
            e.Property(ep => ep.Title).HasMaxLength(250);
            e.Property(ep => ep.Synopsis).HasMaxLength(1000);
            e.Property(ep => ep.AirDate); // Pode ser DateOnly ou DateTime
            e.Property(ep => ep.CreatedAt).IsRequired();

            // Índice para buscar episódios por Donghua
            e.HasIndex(ep => ep.DonghuaId);

            // Entidade/VO VideoAsset (Owned) - Configuração movida para cá
            e.OwnsOne(ep => ep.Video, va => // 'Video' é o nome da propriedade na classe Episode
            {
                // Se quiser que os dados do VideoAsset fiquem em colunas na tabela Episodes:
                // Não use va.ToTable("VideoAssets");

                // Ou, se preferir uma tabela separada para os dados do VideoAsset (mais organizado):
                 va.ToTable("VideoAssets");

                 // Mapear propriedades simples do VideoAsset
                 va.Property(v => v.Duration); // Ex: TimeSpan
                 va.Property(v => v.CaminhoStorage)
                   .IsRequired()
                   .HasMaxLength(500); // Ou mais se necessário (URLs podem ser longas)
                 // Adicione outras propriedades do VideoAsset aqui (ex: ThumbnailUrl)
                 va.Property(v => v.ThumbnailUrl).HasMaxLength(500);

                 // Coleção de Value Objects Manifests (Owned, aninhado dentro de VideoAsset)
                 va.OwnsMany(v => v.Manifests, m =>
                 {
                     m.ToTable("VideoManifests");
                     // Chave estrangeira para VideoAsset (que pertence a Episode)
                     // O nome da FK depende de como EF Core nomeia a relação OwnsOne (Episode->Video).
                     // Pode ser "EpisodeId" ou um nome composto se VideoAsset tiver tabela própria com PK diferente.
                     // Tente sem especificar primeiro, ou ajuste se der erro.
                     m.WithOwner().HasForeignKey("EpisodeId"); // Ajuste o nome da FK se necessário!

                      // Mapear propriedades do VO VideoManifest
                      m.Property(manifest => manifest.Protocolo).IsRequired().HasMaxLength(10); // "HLS", "DASH"
                      m.Property(manifest => manifest.CodecPrincipal).IsRequired().HasMaxLength(10); // "H264", "HEVC"
                     // Adicione outras propriedades do Manifest

                     // Chave Primária Composta para o Manifest dentro da coleção
                     // Precisa da(s) chave(s) do dono (EpisodeId) + algo único do Manifest (Protocolo?)
                      m.HasKey("EpisodeId", "Protocolo"); // Assumindo Protocolo único por vídeo

                      // Coleção de Value Objects Qualities (Owned, aninhado dentro de Manifest)
                      m.OwnsMany(manifest => manifest.Qualities, q =>
                      {
                          q.ToTable("VideoQualityProfiles");
                         // Chave estrangeira para Manifest
                         // Precisa das chaves do dono (EpisodeId, Protocolo)
                         q.WithOwner().HasForeignKey("EpisodeId", "ManifestProtocolo"); // Ajuste os nomes das FKs!

                          // Mapear propriedades do VO VideoQualityProfile
                          q.Property(quality => quality.Qualidade).IsRequired().HasMaxLength(20); // "1080p", "720p_HDR"
                         q.Property(quality => quality.Url).IsRequired().HasMaxLength(500);
                         q.Property(quality => quality.Bitrate); // Ex: int (kbps)
                         q.Property(quality => quality.ResolutionWidth); // Ex: int
                         q.Property(quality => quality.ResolutionHeight); // Ex: int
                         // Adicione outras propriedades da Qualidade

                         // Chave Primária Composta para a Qualidade dentro da coleção
                         // Precisa das chaves dos donos (EpisodeId, ManifestProtocolo) + algo único da Qualidade (Qualidade?)
                         q.HasKey("EpisodeId", "ManifestProtocolo", "Qualidade");
                     });
                 });

                 // Nota: Se VideoAsset for mapeado para sua própria tabela com ToTable(),
                 // o EF Core usará a chave primária do Episode como chave primária/estrangeira para VideoAssets.
                 // Se você não usar ToTable(), as colunas do VideoAsset serão criadas na tabela Episodes.
            });
        });


        // --- Configurações para Entidades que foram REMOVIDAS ---
        // modelBuilder.Entity<History>... REMOVIDO
        // modelBuilder.Entity<Favorite>... REMOVIDO
        // modelBuilder.Entity<VideoAsset>... REMOVIDO (agora configurado via OwnsOne)
    }

}
