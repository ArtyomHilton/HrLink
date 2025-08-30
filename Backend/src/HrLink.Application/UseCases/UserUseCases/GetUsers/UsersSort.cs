using HrLink.Application.UseCases.UserUseCases.GetUsers.Enums;
using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.GetUsers;

/// <summary>
/// Сортировщих пользователей.
/// </summary>
public static class UsersSort
{
    /// <summary>
    /// Сортирует <paramref name="queryable"/> на основе <paramref name="sortBy"/>.
    /// </summary>
    /// <param name="queryable"><see cref="IQueryable"/></param>
    /// <param name="sortBy"><see cref="UsersSortBy"/></param>
    /// <returns>Отсортированную <paramref name="queryable"/>.</returns>
    public static IQueryable<User> SortBy(this IQueryable<User> queryable, UsersSortBy sortBy)
    {
        return sortBy switch
        {
            UsersSortBy.FullNameDescending => queryable
                .OrderByDescending(x => x.FirstName)
                .ThenByDescending(x=> x.SecondName)
                .ThenByDescending(x=> x.Patronymic),
            UsersSortBy.FullNameAscending => queryable
                .OrderBy(x => x.FirstName)
                .ThenBy(x=> x.SecondName)
                .ThenBy(x=> x.Patronymic),
            _ => queryable.OrderBy(x=> x.Id)
        };
    }
}