namespace HrLink.Domain.Entities;

/// <summary>
/// Пользователь.
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string SecondName { get; set; } = string.Empty;

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
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Хэш пароля.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Статус удаления.
    /// </summary>
    public bool IsDelete { get; set; }

    /// <summary>
    /// Идентификатор сотрудника.
    /// Внешний ключ к <see cref="Employee"/>.
    /// </summary>
    public Guid? EmployeeId { get; set; }

    /// <summary>
    /// Навигационное свойство к <see cref="Employee"/>.
    /// </summary>
    public Employee? Employee { get; set; }

    /// <summary>
    /// Навигационное свойство к <see cref="UserRole"/>.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}