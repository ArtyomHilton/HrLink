using Serilog;
using Serilog.Sinks.OpenSearch;

namespace HrLink.API.Extensions;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder AddLogger(this ILoggingBuilder builder, WebApplicationBuilder webApplicationBuilder)
    {
        var logger = new LoggerConfiguration()
            .Enrich.WithProperty("Application", "HrLink.API")
            .Enrich.WithProperty("Environment", webApplicationBuilder.Environment.EnvironmentName)
            .WriteTo.Logger(configuration => configuration
                .MinimumLevel.Information()
                .WriteTo.Console())
            .WriteTo.Logger(configuration => configuration
                .MinimumLevel.Error()
                .WriteTo.OpenSearch(
                    new OpenSearchSinkOptions(new Uri(
                        webApplicationBuilder.Configuration.GetConnectionString("OpenSearchConnectionString")!))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.OSv1,
                        IndexFormat = "hrlink-logs-{0:yyyy-MM-dd}",
                        ModifyConnectionSettings = x => x.BasicAuthentication
                        (webApplicationBuilder.Configuration.GetSection("OpenSearchAuth")["Login"],
                            webApplicationBuilder.Configuration.GetSection("OpenSearchAuth")["Password"])
                    })).CreateLogger();

        builder.ClearProviders().AddSerilog(logger);

        return builder;
    }
}