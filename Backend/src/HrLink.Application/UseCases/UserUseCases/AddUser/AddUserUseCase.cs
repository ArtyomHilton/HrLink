using FluentValidation;
using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.DTOs;
using HrLink.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.UserUseCases.AddUser;

/// <see cref="IAddUserUseCase"/>
public class AddUserUseCase : IAddUserUseCase
{
    /// <inheritdoc cref="IApplicationDbContext"/>
    private readonly IApplicationDbContext _context;

    private readonly IValidator<AddUserCommand> _validator;

    public AddUserUseCase(IApplicationDbContext context, IValidator<AddUserCommand> validator)
    {
        _context = context;
        _validator = validator;
    }
    
    /// <inheritdoc />
    public async Task<Result<UserDetailDataResponse?>> Execute(AddUserCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Failure<UserDetailDataResponse?>(null,
                new ValidateError(validationResult.Errors[0].ErrorCode, validationResult.Errors[0].PropertyName));
        }
        
        if (await _context.Users.AnyAsync(x=> x.Email == command.Email, cancellationToken))
        {
            return Result.Failure<UserDetailDataResponse?>(null, new ValidateError("EmailExist", nameof(command.Email)));
        }

        command.RoleIds = await _context.Roles
            .Where(x=> command.RoleIds!.Contains(x.Id))
            .Select(x=> x.Id)
            .ToListAsync(cancellationToken);

        var entry = await _context.Users
            .AddAsync(command.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        
        var user = await _context.Users
            .Where(x=> x.Id == entry.Entity.Id)
            .Select(x =>
                new UserDetailDataResponse(
                    x.Id,
                    x.FirstName,
                    x.SecondName,
                    x.Patronymic,
                    x.DateOfBirthday,
                    x.Email,
                    x.Employee == null
                        ? null
                        : new EmployeeDetailDataResponse(
                            x.Employee.Id,
                            x.Employee.Position,
                            x.Employee.WorkEmail,
                            x.Employee.WorkPhoneNumber,
                            x.Employee.DateOfEmployment,
                            x.Employee.Interviews.Select(i => new InterviewShortDataResponse(
                                        i.Id,
                                        i.Vacancy.Position,
                                        i.InterviewDateTime,
                                        i.Status.StatusName))
                                    .ToList())))
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return Result.Success(user);
    }
}