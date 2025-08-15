using HrLink.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HrLink.API.Extensions;

/// <summary>
/// Методы расширения для <see cref="WebApplication"/>.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Применяет миграции для БД.
    /// </summary>
    /// <param name="webApplication"><see cref="WebApplication"/></param>
    public static void ApplyMigrations(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (service.Database.GetPendingMigrations().Any())
        {
            service.Database.Migrate();
        }
    }
}