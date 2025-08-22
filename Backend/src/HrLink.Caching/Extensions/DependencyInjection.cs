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
            options.InstanceName = "HrLink";
            var configOptions = ConfigurationOptions.Parse(configuration.GetSection("ConnectionStrings")["Redis"]!);
            configOptions.ConnectRetry = 5;
            configOptions.AllowAdmin = false;
            configOptions.ClientName = "HrLink_App";
        });

        serviceCollection.AddScoped<ICacheService,RedisCacheService>();
        
        return serviceCollection;
    }
}