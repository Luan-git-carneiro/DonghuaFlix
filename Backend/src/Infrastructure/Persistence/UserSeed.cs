

using DonghuaFlix.Backend.src.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DonghuaFlix.Backend.src.Infrastructure.Persistence;

public static class UserSeed
{
    /// <summary>
    /// Seed the admin user if it does not exist.
    /// </summary>
    /// <param name="context">The application database context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAdminUserAsync(AppDbContext context)
    {
        if(await context.Users.AnyAsync())
            return;

        var admin = new User(
            id: Guid.NewGuid(),
            Name: "Administrador",
            email: new Core.Domain.ValueObjects.Email("administradorLuan@donghua.com"),
            password: new Core.Domain.ValueObjects.Password("LuanAdmin123"),
            role: Core.Domain.Enum.UserRole.Admin,
            status: Core.Domain.Enum.AccountStatus.Active
        );

        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}