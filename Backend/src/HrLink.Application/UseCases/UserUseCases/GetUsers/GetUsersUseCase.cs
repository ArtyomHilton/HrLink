using HrLink.Application.Common.Results;
using HrLink.Application.Interfaces;
using HrLink.Application.UseCases.UserUseCases.GetUsers.Enums;
using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.UserUseCases.GetUsers;

public class GetUsersUseCase : IGetUsersUseCase
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheService _cacheService;


    public GetUsersUseCase(IApplicationDbContext context, ICacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<Result<List<User>?>> Execute(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _cacheService
            .GetAsync<List<User>?>(
                $"{nameof(User)}s_page{query.Page}_itemperpage{query.ItemPerPage}_sort{query.SortBy}",
                cancellationToken);

        if (users is not null)
        {
            return Result.Success<List<User>?>(users);
        }

        users = await _context.Users
            .Include(x => x.Employee)
            .Where(x => !x.IsDelete)
            .SortBy(query.SortBy)
            .Skip((query.Page - 1) * query.ItemPerPage)
            .Take(query.ItemPerPage)
            .ToListAsync(cancellationToken);

        if (users.Any())
        {
            await _cacheService
                .SetAsync<List<User>?>(
                    $"{nameof(User)}s_page{query.Page}_itemperpage{query.ItemPerPage}_sort{query.SortBy}",
                    users,
                    cancellationToken);
        }

        return Result.Success<List<User>?>(users);
    }
}