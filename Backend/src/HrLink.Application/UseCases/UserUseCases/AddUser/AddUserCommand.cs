using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.AddUser;

public class AddUserCommand
{
    public required string FirstName { get; set; }
    public required string SecondName { get; set; }
    public string? Patronymic { get; set; }
    public required DateTime DateOfBirthday { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required List<Guid> RoleIds { get; set; }
    
    public User ToModel()
    {
        var userId = Guid.NewGuid();

        return new User()
        {
            Id = userId,
            FirstName = this.FirstName,
            SecondName = this.SecondName,
            Patronymic = this.Patronymic,
            DateOfBirthday = this.DateOfBirthday,
            Email = this.Email,
            PasswordHash = this.Password, // TODO: Хэширование
            IsDelete = false,
            UserRoles = RoleIds.Select(x => new UserRole()
            {
                RoleId = x,
                UserId = userId
            }).ToList()
        };
    }
}