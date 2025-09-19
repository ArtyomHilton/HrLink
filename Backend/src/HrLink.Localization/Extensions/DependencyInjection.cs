using HrLink.Application.Interfaces;
using HrLink.Localization.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HrLink.Localization.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomLocalization(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ILocalizationService, LocalizationService>();
        
        return serviceCollection;
    }
}