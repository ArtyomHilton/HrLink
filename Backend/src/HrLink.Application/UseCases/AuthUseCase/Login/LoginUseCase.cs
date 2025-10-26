using HrLink.Application.Common;
using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.DTOs;
using HrLink.Application.Interfaces;
using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.AuthUseCase.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IApplicationDbContext _context;

    public LoginUseCase(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<LoginDataResponse>> Execute(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(x => x.Email == command.Email)
            .Select(x=> new
            {
                Id = x.Id,
                FirstName = x.FirstName,
                SecondName = x.SecondName,
                Patronymic = x.Patronymic,
                DateOfBirthday = x.DateOfBirthday,
                Email = x.Email,
                PasswordHash = x.PasswordHash,
                Roles = x.UserRoles.Select(r=> new
                {
                    
                    r.Role.Id,
                    r.Role.Name
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginDataResponse>(new NotFoundError<User>(nameof(command.Email)));
        }

        if (!PasswordHasher.VerifyPassword(command.Password, user.PasswordHash))
        {
            return Result.Failure<LoginDataResponse>(new UnAuthorizedError());
        }

        return Result.Success(new LoginDataResponse(user.Id, user.FirstName, user.SecondName, user.Patronymic,
            user.DateOfBirthday, user.Email, user.Roles
                .Select(x => new RoleDataResponse(x.Id, x.Name))
                .ToList()));
    }
}