using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
using HrLink.Application.DTOs;
using HrLink.Application.Interfaces;
using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.UserUseCases.GetUserByIdUser;

public class GetUserByIdUseCase : IGetUserByIdUseCase
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheService _cacheService;

    public GetUserByIdUseCase(IApplicationDbContext context, ICacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<Result<UserDetailDataResponse?>> Execute(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _cacheService.GetAsync<UserDetailDataResponse?>($"user_{query.Id}", cancellationToken);

        if (user is not null)
        {
            return Result.Success<UserDetailDataResponse?>(user);
        }

        user = await _context.Users
            .Where(x=> x.Id == query.Id && !x.IsDelete)
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
                            x.Employee.Interviews == null
                                ? new List<InterviewShortDataResponse>()
                                : x.Employee.Interviews.Select(i => new InterviewShortDataResponse(
                                        i.Id,
                                        i.Vacancy.Position,
                                        i.InterviewDateTime,
                                        i.Status.StatusName))
                                    .ToList())))
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            return Result.Failure(user, new NotFoundError<User>(nameof(query.Id),
                new Dictionary<string, object?>() { ["UserId"] = query.Id }));
        }

        await _cacheService.SetAsync($"user_{query.Id}", user, cancellationToken);

        return Result.Success<UserDetailDataResponse?>(user);
    }
}