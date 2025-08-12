using HrLink.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HrLink.Persistence.Extensions;

/// <summary>
/// Методы расширения слоя <see cref="Persistence"/>.
/// </summary>
public static class PersistenceExtension
{
    /// <summary>
    /// Добавление базы данных
    /// </summary>
    /// <param name="serviceCollection"><see cref="IServiceCollection"/>.</param>
    /// <param name="configuration"><see cref="IConfiguration"/>.</param>
    /// <returns>Измененный <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.AddNpgsql<ApplicationDbContext>(configuration.GetConnectionString("DatabaseConnectionString"),
            builder =>
            {
                builder.EnableRetryOnFailure(10);
            });
    }
}