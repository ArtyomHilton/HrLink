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
    public required string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string SecondName { get; set; }
    
    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; }
    
    /// <summary>
    /// Дата рождения.
    /// </summary>
    public required DateTime DateOfBirthday { get; set; }
    
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Пароль.
    /// </summary>
    public required string Password { get; set; }
    
    /// <summary>
    /// Список идентификаторов ролей пользователей.
    /// </summary>
    public required List<Guid> RoleIds { get; set; }
    
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