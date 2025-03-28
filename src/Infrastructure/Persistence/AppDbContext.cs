using DonghuaFlix.src.Core.Domain.Entities;
using DonghuaFlix.src.Core.Domain.Enum;
using DonghuaFlix.src.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DonghuaFlix.src.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Donghua> Donghuas { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<VideoAsset> Videos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //Configuração do Usuario e Fvoritos
        modelBuilder.Entity<User>(u =>
        {

            u.HasMany(u => u.Favorites)
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração para Histórico
            u.HasMany(u => u.Histories)
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);



            // Configuração do Email
            u.OwnsOne(u => u.Email, e =>
            {
                e.Property(e => e.Valor)
                    .HasColumnName("Email") // Nome da coluna no banco
                    .HasMaxLength(254)
                    .IsRequired();
            });

            // Configuração do Password
            u.OwnsOne(u => u.Password, p =>
            {
                p.Property(p => p.Value)
                    .HasColumnName("PasswordHash")
                    .HasMaxLength(60) // Tamanho fixo do hash BCrypt
                    .IsRequired();
            });

            // Configuração para UserRole (armazenar como string)
            u.Property(e => e.Role)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserRole)Enum.Parse(typeof(UserRole), v))
                .HasDefaultValue(UserRole.Visistante)
                .IsRequired();

            // Configuração para AccountStatus (armazenar como inteiro)
            u.Property(e => e.Status)
                .HasConversion<int>()
                .HasDefaultValue(AccountStatus.Active)
                .IsRequired();



        });


        modelBuilder.Entity<History>(h =>
        {
            // Como History herda de ValueObject, pode precisar de configuração especial
            h.HasNoKey(); // Se não tiver ID próprio

            // Configuração das chaves estrangeiras
            h.Property(x => x.UserId).IsRequired();
            h.Property(x => x.EpisodeId).IsRequired();

            // Se quiser criar um índice composto
            h.HasIndex(x => new { x.UserId, x.EpisodeId });
        });

        modelBuilder.Entity<Favorite>(h =>
        {
            // Como History herda de ValueObject, pode precisar de configuração especial
            h.HasNoKey(); // Se não tiver ID próprio

            // Configuração das chaves estrangeiras
            h.Property(x => x.UserId).IsRequired();
            h.Property(x => x.DonghuaId).IsRequired();

            // Se quiser criar um índice composto
            h.HasIndex(x => new { x.UserId, x.DonghuaId });
        });

        // Configuração do Donghua
        modelBuilder.Entity<Donghua>(d =>
        {
            d.ToTable("Donghuas");
            d.HasKey(d => d.Id);

            d.HasMany(d => d.Episodes)
                .WithOne()
                .HasForeignKey("DonghuaId")
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuração do Episódio
        modelBuilder.Entity<Episode>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Video)
                .WithOne()
                .HasForeignKey<VideoAsset>(v => v.Id);
        });

        // Configuração do Video
        modelBuilder.Entity<VideoAsset>(va =>
        {
            va.HasKey(v => v.Id);
            
            va.ToTable("VideoAssets");
            
            // Configuração dos Manifests (Value Objects)
            va.OwnsMany(v => v.Manifests, m =>
            {
                m.ToTable("VideoManifests");
                
                // Configuração das Qualidades (Value Objects dentro do Manifest)
                m.OwnsMany(m => m.Qualities, q =>
                {
                    q.ToTable("VideoQualityProfiles");
                    q.WithOwner().HasForeignKey("VideoManifestId");
                    q.HasKey("VideoManifestId", "Qualidade"); // Chave composta
                });
                
                m.Property(m => m.Protocolo).IsRequired().HasMaxLength(10);
                m.Property(m => m.CodecPrincipal).IsRequired().HasMaxLength(10);
            });
            
            va.Property(v => v.CaminhoStorage)
                .IsRequired()
                .HasMaxLength(500);
                
        });
    }

}
