using HrLink.Application.Interfaces;
using HrLink.Email.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HrLink.Email;

public static class DependencyInjection
{
    public static IServiceCollection AddEmail(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEmailService, EmailService>();

        return serviceCollection;
    }
}