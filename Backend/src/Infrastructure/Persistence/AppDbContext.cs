using DonghuaFlix.Backend.src.Core.Domain.Entities;
using DonghuaFlix.Backend.src.Core.Domain.Enum;
using DonghuaFlix.Backend.src.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonghuaFlix.Backend.src.Infrastructure.Persistence;

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

        //Criar Dados


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
                hist.Property(h => h.DateVisualization).IsRequired();

                // Chave Primária Composta (assumindo uma entrada por usuário/episódio)
                hist.HasKey("UserId", "EpisodeId");
            });

            // Mapear outras propriedades do User (Nome, Sobrenome, etc.)
            u.Property(user => user.Name).HasMaxLength(100);
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
            d.Property(donghua => donghua.Sinopse).HasMaxLength(2000); // Ou tipo TEXT/NVARCHAR(MAX)
            d.Property(donghua => donghua.Image).HasMaxLength(500);

            d.Property(donghua => donghua.Status)
                .HasConversion<int>()
                .IsRequired();
             d.Property(donghua => donghua.Type)
                .HasConversion<int>()
                .IsRequired();
            // Para Genres, se for uma lista simples de Enums, pode usar OwnsMany ou uma tabela de junção
            // Exemplo simplificado (requer mais detalhes se for coleção):
            d.Property(donghua => donghua.Genres)
                .HasConversion<int>() // armazena o bitwise como valor inteiro
                .IsRequired();

            d.Property(donghua => donghua.ReleaseDate);
            d.Property(donghua => donghua.CreatedAt).IsRequired();
        });

        // --- Configuração de Episode (Raiz de Agregado) ---

        modelBuilder.Entity<Episode>(e =>
        {
            e.ToTable("Episodes");
            e.HasKey(ep => ep.Id);

            e.Property(ep => ep.DonghuaId).IsRequired();
            e.Property(ep => ep.Number).IsRequired();
            e.Property(ep => ep.Duration).IsRequired();
            e.Property(ep => ep.CreatedAt).IsRequired();

            e.HasIndex(ep => ep.DonghuaId);

            e.OwnsOne(ep => ep.Video, va =>
            {
                va.ToTable("VideoAssets");

                va.Property(v => v.CaminhoStorage)
                .IsRequired()
                .HasMaxLength(500);
                va.Property(v => v.DataUpload).IsRequired();

                va.OwnsMany(v => v.Manifests, m =>
                {
                    m.ToTable("VideoManifests");

                    m.Property<Guid>("Id"); // Propriedade sombra 'Id' para VideoManifest
                    // O EF Core cria automaticamente o 'VideoAssetId' como uma FK para o dono (VideoAsset)
                    m.HasKey("VideoAssetId", "Id"); // Chave composta com FK do VideoAsset + Id do Manifest

                    m.WithOwner()
                     .HasForeignKey("VideoAssetId"); // FK para VideoAsset

                    m.Property(manifest => manifest.Protocolo).IsRequired().HasMaxLength(10);
                    m.Property(manifest => manifest.CodecPrincipal).IsRequired().HasMaxLength(10);

                    m.OwnsMany(manifest => manifest.Qualities, q =>
                    {
                         q.ToTable("VideoQualityProfiles");

                        // 1. Define as propriedades sombra necessárias
                        q.Property<Guid>("VideoAssetId"); // <-- Esta propriedade sombra já existe na tabela pai (Manifest),
                                                         // mas precisa ser declarada na tabela filha para a PK/FK composta.
                        q.Property<Guid>("ManifestId");   // A FK para o Id do Manifest.
                        q.Property<Guid>("Id");           // A PK única para VideoQualityProfile.

                        // 2. A chave primária deve incluir TODAS as partes da chave do seu dono (VideoAssetId e ManifestId)
                        // mais a sua própria chave (Id).
                        q.HasKey("VideoAssetId", "ManifestId", "Id"); // <-- CORREÇÃO AQUI!

                        // 3. A chave estrangeira para o dono (VideoManifest) deve usar TODAS as partes da PK do dono.
                        q.WithOwner()
                         .HasForeignKey("VideoAssetId", "ManifestId"); // <-- CORREÇÃO AQUI!

                        q.Property(quality => quality.Quality).IsRequired().HasMaxLength(20);
                        q.Property(quality => quality.Path).IsRequired().HasMaxLength(500);
                        q.Property(quality => quality.Bitrate).IsRequired();
                        q.Property(quality => quality.Codec).IsRequired().HasMaxLength(20);
                    });
                });
            });
        });



        modelBuilder.Entity<Donghua>().HasData(
             new
    {
        Id = Guid.Parse("64d7f030-2fd4-4070-9753-1802fc4523ec"),
        Title = "The King's Avatar",
        Sinopse = "Um jogador profissional é forçado a sair do e-sport e começa de novo.",
        Studio = "G.CMay Animation",
        ReleaseDate = new DateTime(2017, 4, 7),
        Type = DonghuaType.Serie,
        Status = DonghuaStatus.Concluido,
        Image = "https://cdn.donghua.com/kings-avatar.jpg",
        Genres = Genre.Acao | Genre.Sports,
        CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc)
    },
    new
    {
        Id = Guid.Parse("f59fa054-16fc-4ad7-9ec7-26308bb9d774"),
        Title = "Mo Dao Zu Shi",
        Sinopse = "Cultivadores espirituais enfrentam antigos segredos e traições.",
        Studio = "Tencent Penguin Pictures",
        ReleaseDate = new DateTime(2018, 7, 9),
        Type = DonghuaType.Serie,
        Status = DonghuaStatus.Concluido,
        Image = "https://cdn.donghua.com/modaozushi.jpg",
        Genres = Genre.Acao | Genre.Sobrenatural,
        CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc)
    },
    new
    {
        Id = Guid.Parse("a88bc6c5-f848-4fd5-a1e8-bbe7aa5a7e41"),
        Title = "Heaven Official's Blessing",
        Sinopse = "Um deus exilado e um fantasma poderoso cruzam caminhos.",
        Studio = "Haoliners Animation",
        ReleaseDate = new DateTime(2020, 10, 31),
        Type = DonghuaType.Serie,
        Status = DonghuaStatus.EmAndamento,
        Image = "https://cdn.donghua.com/tgcf.jpg",
        Genres = Genre.Xianxia | Genre.Romance,
        CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc)
    },
    new
    {
        Id = Guid.Parse("d2a14412-b1e2-42c9-b7e0-4cd632df9dc8"),
        Title = "Scissor Seven",
        Sinopse = "Um barbeiro com amnésia atua como assassino fracassado.",
        Studio = "AHA Entertainment",
        ReleaseDate = new DateTime(2018, 4, 25),
        Type = DonghuaType.Serie,
        Status = DonghuaStatus.EmAndamento,
        Image = "https://cdn.donghua.com/scissor7.jpg",
        Genres = Genre.Comedia | Genre.Acao,
        CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc)
    },
    new
    {
        Id = Guid.Parse("c1270c8d-2e84-4f62-98ce-ef57c018e285"),
        Title = "White Cat Legend",
        Sinopse = "Um oficial imperial felino investiga conspirações.",
        Studio = "Nice Boat Animation",
        ReleaseDate = new DateTime(2020, 4, 10),
        Type = DonghuaType.Serie,
        Status = DonghuaStatus.EmAndamento,
        Image = "https://cdn.donghua.com/whitecat.jpg",
        Genres = Genre.Misterio | Genre.Acao,
        CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc)
    },
    new
    {
        Id = Guid.Parse("1de89ff8-1124-4ea5-83c6-bdcaa1d1e355"),
        Title = "Fog Hill of Five Elements",
        Sinopse = "Elementos e feras colidem em um mundo mitológico.",
        Studio = "Samsara Studio",
        ReleaseDate = new DateTime(2020, 7, 26),
        Type = DonghuaType.Special,
        Status = DonghuaStatus.Pausado,
        Image = "https://cdn.donghua.com/foghill.jpg",
        Genres = Genre.Acao | Genre.Fantasia,
        CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc)
    },
    new
    {
        Id = Guid.Parse("6b2c6cf0-d172-434f-878c-32f6a0842282"),
        Title = "The Daily Life of the Immortal King",
        Sinopse = "Um prodígio espiritual tenta levar uma vida normal.",
        Studio = "Haoliners Animation",
        ReleaseDate = new DateTime(2020, 1, 18),
        Type = DonghuaType.Serie,
        Status = DonghuaStatus.EmAndamento,
        Image = "https://cdn.donghua.com/immortalking.jpg",
        Genres = Genre.Comedia | Genre.Sobrenatural,
        CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc)
    },
    new
    {
        Id = Guid.Parse("3bdb6119-fbe4-4e3e-a4bb-62d06b6871cc"),
        Title = "Big Fish & Begonia",
        Sinopse = "A alma de uma jovem navega entre o mundo humano e o espiritual.",
        Studio = "B&T Studio",
        ReleaseDate = new DateTime(2016, 7, 8),
        Type = DonghuaType.Movie,
        Status = DonghuaStatus.Concluido,
        Image = "https://cdn.donghua.com/bigfish.jpg",
        Genres = Genre.Fantasia | Genre.Drama,
        CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc)
    },
    new
    {
        Id = Guid.Parse("d5e6fb84-82f1-4950-bbee-2677d059d61c"),
        Title = "Rakshasa Street",
        Sinopse = "Espíritos protetores defendem os vivos dos mortos.",
        Studio = "L2 Studio",
        ReleaseDate = new DateTime(2016, 4, 28),
        Type = DonghuaType.Serie,
        Status = DonghuaStatus.Concluido,
        Image = "https://cdn.donghua.com/rakshasa.jpg",
        Genres = Genre.Acao | Genre.Sobrenatural,
        CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc)
    },
    new
    {
        Id = Guid.Parse("ab9d49a3-2e61-4c28-9aa9-2d0c91357df6"),
        Title = "Spare Me, Great Lord!",
        Sinopse = "Um jovem órfão com poderes místicos luta para proteger sua irmã.",
        Studio = "Big Firebird",
        ReleaseDate = new DateTime(2021, 12, 3),
        Type = DonghuaType.Serie,
        Status = DonghuaStatus.EmAndamento,
        Image = "https://cdn.donghua.com/greatlord.jpg",
        Genres = Genre.Fantasia | Genre.Xuanhuan,
        CreatedAt = new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc)
    }
        );
    }

}
