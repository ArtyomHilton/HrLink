using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace HrLink.BackgroundService.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<BackgroundServiceOptions>>();
        
        serviceCollection.AddQuartz(configurator =>
        {
            configurator.UsePersistentStore(configure =>
            {
                configure.UsePostgres(cfg =>
                {
                    cfg.ConnectionString = options.Value.ConnectionString;
                    cfg.TablePrefix = options.Value.TablePrefix;
                });
                configure.UseSystemTextJsonSerializer();
                configure.UseProperties = true;
            });
            
            foreach (var valueProperty in options.Value.Properties)
            {
                configurator.Properties.Add(valueProperty.Key, valueProperty.Value);
            }
        });

        serviceCollection.AddQuartzHostedService(configure =>
        {
            configure.WaitForJobsToComplete = true;
            configure.AwaitApplicationStarted = true;
        });
        
        return serviceCollection;
    }
}