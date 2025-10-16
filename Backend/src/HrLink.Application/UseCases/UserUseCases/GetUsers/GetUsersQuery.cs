using HrLink.Application.UseCases.UserUseCases.GetUsers.Enums;

namespace HrLink.Application.UseCases.UserUseCases.GetUsers;

/// <summary>
/// Запрос для получения пользователей.
/// Содержит необходимые данные для получения пользователей.
/// </summary>
public class GetUsersQuery
{
    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int Page { get; private set; }
    
    /// <summary>
    /// Количество элементов на странице.
    /// </summary>
    public int ItemPerPage { get; private set; }
    
    /// <summary>
    /// Тип сортировки.
    /// </summary>
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