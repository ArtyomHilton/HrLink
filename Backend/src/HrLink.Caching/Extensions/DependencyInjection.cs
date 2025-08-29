using HrLink.Application.Interfaces;
using HrLink.Caching.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace HrLink.Caching.Extensions;

public static class DependencyInjection
{
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