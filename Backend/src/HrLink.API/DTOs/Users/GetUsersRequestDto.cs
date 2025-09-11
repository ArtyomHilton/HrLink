using HrLink.Application.UseCases.UserUseCases.GetUsers;

namespace HrLink.API.DTOs.Users;

public record GetUsersRequestDto
{
    public int Page { get; set; } = 1;
    public int ItemPerPage { get; set; } = 10;
    public string SortBy { get; set; } = "Default";

    public GetUsersQuery ToQuery() => 
        new GetUsersQuery(Page, ItemPerPage, SortBy);
}