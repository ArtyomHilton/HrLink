using HrLink.Application.UseCases.UserUseCases.GetUsers.Enums;
using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.GetUsers;

public class GetUsersQuery
{
    public int Page { get; private set; }
    public int ItemPerPage { get; private set; }
    public UsersSortBy SortBy { get; private set; }

    public GetUsersQuery(int page, int itemPerPage, string sortBy)
    {
        Page = page;
        ItemPerPage = itemPerPage;
        SortBy = Enum.TryParse<UsersSortBy>(sortBy, true, out var usersSortBy)
            ? usersSortBy
            : UsersSortBy.Default;
    }
}