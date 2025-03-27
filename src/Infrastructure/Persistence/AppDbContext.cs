using DonghuaFlix.src.Core.Domain.Entities;
using DonghuaFlix.src.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

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

//         //Configuração do Usuario e Fvoritos
//         modelBuilder.Entity<User>(u =>
//         {
            
// u.OwnsMany(u => u.Favoritos, f =>
//         {
//             f.ToTable("UsuarioFavoritos");
//             f.WithOwner().HasForeignKey("UsuarioId");
//             f.HasKey("UsuarioId", nameof(Favorito.EpisodioId));
            
//             f.Property(f => f.EpisodioId).IsRequired();
//             f.Property(f => f.DataAdicao).IsRequired();
//         });

//         // Configuração para Histórico
//         u.OwnsMany(u => u.Historico, h =>
//         {
//             h.ToTable("UsuarioHistorico");
//             h.WithOwner().HasForeignKey("UsuarioId");
//             h.HasKey("UsuarioId", nameof(Historico.EpisodioId));
            
//             h.Property(h => h.EpisodioId).IsRequired();
//             h.Property(h => h.DataVisualizacao).IsRequired();
//             h.Property(h => h.Progresso)
//                 .HasConversion(
//                     v => v.Ticks,
//                     v => TimeSpan.FromTicks(v))
//                 .IsRequired();
//         });

//         });
//     }

    }
}