namespace HrLink.Application.UseCases.UserUseCases.GetUserByIdUser;

/// <summary>
/// Запрос получения пользователя по идентификатору. 
/// </summary>
public class GetUserByIdQuery
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
}