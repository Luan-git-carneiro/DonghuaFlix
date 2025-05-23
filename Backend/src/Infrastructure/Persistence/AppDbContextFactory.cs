using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DonghuaFlix.Backend.src.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=donghuaflix.db");
            // Ou use seu provedor de banco de dados apropriado, como SQL Server:
            // optionsBuilder.UseSqlServer("YourConnectionString");
            
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}