using HrLink.BackgroundService.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace HrLink.BackgroundService.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundService(this IServiceCollection serviceCollection)
    {
        using var scope = serviceCollection.BuildServiceProvider()
            .CreateScope();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<QuartzOptions>>();
        
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

            configurator.AddJobs();
        });

        serviceCollection.AddQuartzHostedService(configure =>
        {
            configure.WaitForJobsToComplete = true;
            configure.AwaitApplicationStarted = true;
        });
        
        return serviceCollection;
    }

    private static IServiceCollectionQuartzConfigurator AddJobs(this IServiceCollectionQuartzConfigurator configurator)
    {
        configurator.AddJob<SendInterviewNotification>(SendInterviewNotification.JobKey);
        configurator.AddTrigger(options =>
        {
            options.WithIdentity(SendInterviewNotification.TriggerKey)
                .ForJob(SendInterviewNotification.JobKey)
                .WithCronSchedule("0 0 2 ? * MON-FRI");
        });

        return configurator;
    }
}