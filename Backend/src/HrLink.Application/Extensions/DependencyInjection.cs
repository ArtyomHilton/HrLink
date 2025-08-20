using HrLink.Application.UseCases.UserUseCases.AddUser;
using Microsoft.Extensions.DependencyInjection;

namespace HrLink.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAddUserUseCase, AddUserUseCase>();
        
        return serviceCollection;
    }
}