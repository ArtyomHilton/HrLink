namespace HrLink.API.DTOs.Users;

public record GetUsersRequest
{
    public int Page { get; set; } = 1;
    public int ItemPerPage { get; set; } = 10;
    public string SortBy { get; set; } = "Default";
}