using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
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

    public async Task<Result<User?>> Execute(Guid id, CancellationToken cancellationToken)
    {
        var user = await _cacheService.GetAsync<User?>($"user_{id}", cancellationToken);

        if (user is not null)
        {
            return Result.Success<User?>(user);
        }

        user = await _context.Users
            .Include(x => x.Employee)
            .ThenInclude(x => x.Interviews)
            .ThenInclude(x => x.Candidate)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDelete, cancellationToken);

        if (user is null)
        {
            return Result.Failure<User?>(user,
                new NotFoundError($"User with {nameof(id)}: {id} not found", nameof(id)));
        }

        await _cacheService.SetAsync<User>($"user_{id}", user, cancellationToken);

        return Result.Success<User?>(user);
    }
}