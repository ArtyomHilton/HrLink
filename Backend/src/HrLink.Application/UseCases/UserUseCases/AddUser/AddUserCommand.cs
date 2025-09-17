using HrLink.Domain.Entities;

namespace HrLink.Application.UseCases.UserUseCases.AddUser;

/// <summary>
/// Команда добавления пользователя.
/// Содержит данные для добавления пользователя.
/// </summary>
public class AddUserCommand
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string SecondName { get; set; }
    
    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; }
    
    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTime DateOfBirthday { get; set; }
    
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Список идентификаторов ролей пользователей.
    /// </summary>
    public List<Guid>? RoleIds { get; set; }

    public AddUserCommand(string firstName, string secondName, string? patronymic, DateTime dateOfBirthday, string email, string password, List<Guid>? roleIds)
    {
        FirstName = firstName;
        SecondName = secondName;
        Patronymic = patronymic;
        DateOfBirthday = dateOfBirthday;
        Email = email;
        Password = password;
        RoleIds = roleIds;
    }
    
    /// <summary>
    /// Маппит команду в доменную сущность <see cref="User"/>.
    /// </summary>
    /// <returns>Доменную сущность <see cref="User"/>.</returns>
    public User ToEntity()
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