using HrLink.Application.UseCases.UserUseCases.AddRolesForUser;
using HrLink.Application.UseCases.UserUseCases.AddUser;
using HrLink.Application.UseCases.UserUseCases.GetUserByIdUser;
using HrLink.Application.UseCases.UserUseCases.GetUsers;
using Microsoft.Extensions.DependencyInjection;

namespace HrLink.Application.Extensions;

/// <summary>
/// Классы расширения для внедрения зависимостей слоя <see cref="Application"/>.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавление UseCases.
    /// </summary>
    /// <param name="serviceCollection"><see cref="IServiceCollection"/>.</param>
    /// <returns>Измененный <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAddUserUseCase, AddUserUseCase>();
        serviceCollection.AddScoped<IAddRolesForUserUseCase, AddRolesForUserUseCase>();
        serviceCollection.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
        serviceCollection.AddScoped<IGetUsersUseCase, GetUsersUseCase>();
        
        return serviceCollection;
    }
}