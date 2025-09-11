using HrLink.Application.UseCases.UserUseCases.AddUser;
using HrLink.Domain.Entities;

namespace HrLink.API.DTOs.Users;

public record AddUserRequestDto
{
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public string? Patronymic { get; set; }
    public required DateTime DateOfBirthday { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required List<Guid> RoleIds { get; set; }

    public AddUserCommand ToCommand()
    {
        var userId = Guid.NewGuid();

        return new AddUserCommand()
        {
            FirstName = this.FirstName,
            SecondName = this.SecondName,
            Patronymic = this.Patronymic,
            DateOfBirthday = this.DateOfBirthday,
            Email = this.Email,
            Password = this.Password, // TODO: Хэширование
            RoleIds = this.RoleIds
        };
    }
}