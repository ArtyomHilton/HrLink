using HrLink.Application.Interfaces;
using HrLink.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HrLink.Persistence.Extensions;

/// <summary>
/// Методы расширения слоя <see cref="Persistence"/>.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавление базы данных
    /// </summary>
    /// <param name="serviceCollection"><see cref="IServiceCollection"/>.</param>
    /// <param name="configuration"><see cref="IConfiguration"/>.</param>
    /// <returns>Измененный <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        return serviceCollection.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetSection("ConnectionStrings")["DatabaseConnectionString"],
                    builder => { builder.EnableRetryOnFailure(10); });
            });
    }
}