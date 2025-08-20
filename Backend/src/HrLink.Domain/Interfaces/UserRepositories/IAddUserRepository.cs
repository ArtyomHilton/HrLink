using HrLink.Domain.Entities;

namespace HrLink.Domain.Interfaces.UserRepositories;

public interface IAddUserRepository
{
    Task<bool> EmailExistAsync(string email, CancellationToken cancellationToken);
    Task<User?> AddUserAsync(User user, CancellationToken cancellationToken);
}