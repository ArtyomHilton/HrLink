using HrLink.Application.Interfaces;
using HrLink.Caching.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace HrLink.Caching.Extensions;

/// <summary>
/// Содержит методы расширения слоя <see cref="Caching"/>.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Конфигурирует и регистрирует сервис кэширования.
    /// </summary>
    /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    /// <returns>Измененный <see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddCaching(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetSection("ConnectionStrings")["RedisConnectionString"];
            options.InstanceName = "HrLink_";
        });
        
        serviceCollection.AddScoped<ICacheService, RedisCacheService>();

        return serviceCollection;
    }
}