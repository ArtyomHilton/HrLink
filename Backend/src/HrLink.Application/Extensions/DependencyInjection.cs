using HrLink.Application.UseCases.UserUseCases.AddRolesForUser;
using HrLink.Application.UseCases.UserUseCases.AddUser;
using HrLink.Application.UseCases.UserUseCases.GetUserByIdUser;
using HrLink.Application.UseCases.UserUseCases.GetUsers;
using Microsoft.Extensions.DependencyInjection;

namespace HrLink.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAddUserUseCase, AddUserUseCase>();
        serviceCollection.AddScoped<IAddRolesForUserUseCase, AddRolesForUserUseCase>();
        serviceCollection.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
        serviceCollection.AddScoped<IGetUsersUseCase, GetUsersUseCase>();
        
        return serviceCollection;
    }
}