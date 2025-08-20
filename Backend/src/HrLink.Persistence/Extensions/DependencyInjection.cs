using HrLink.Domain.Interfaces;
using HrLink.Domain.Interfaces.RoleRepositories;
using HrLink.Domain.Interfaces.UserRepositories;
using HrLink.Persistence.Context;
using HrLink.Persistence.Repositories;
using HrLink.Persistence.Repositories.RoleRepositories;
using HrLink.Persistence.Repositories.UserRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HrLink.Persistence.Extensions;

/// <summary>
/// Методы расширения слоя <see cref="Persistence"/>.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавление базы данных
    /// </summary>
    /// <param name="serviceCollection"><see cref="IServiceCollection"/>.</param>
    /// <param name="configuration"><see cref="IConfiguration"/>.</param>
    /// <returns>Измененный <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        return serviceCollection
            .AddNpgsql<ApplicationDbContext>(configuration.GetSection("ConnectionStrings")["DatabaseConnectionString"],
                builder => { builder.EnableRetryOnFailure(10); });
    }

    /// <summary>
    /// Регистрирует репозитории в DI. 
    /// </summary>
    /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
    /// <returns>Модифицированный <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddRepository(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICreateRoleRepository, CreateRoleRepository>();
        serviceCollection.AddScoped<IDeleteRoleRepository, DeleteRoleRepository>();
        serviceCollection.AddScoped<IGetAllRolesRepository, GetAllRolesRepository>();
        serviceCollection.AddScoped<IGetRoleByIdRepository, GetRoleByIdRepository>();
        serviceCollection.AddScoped<IUpdateRoleByIdRepository, UpdateRoleByIdRepository>();

        serviceCollection.AddScoped<IAddUserRepository, AddUserRepository>();
        serviceCollection.AddScoped<IAddRolesForUserRepository, AddRolesForUserRepository>();

        return serviceCollection;
    }
}