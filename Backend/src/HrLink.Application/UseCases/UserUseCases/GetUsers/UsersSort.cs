using HrLink.Application.UseCases.UserUseCases.GetUsers.Enums;
using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.GetUsers;

public static class UsersSort
{
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