using HrLink.Application.Common.Results;
using HrLink.Application.DTOs;
using HrLink.Application.Interfaces;
using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.UseCases.UserUseCases.GetUsers;

/// <inheritdoc cref="IGetUsersUseCase"/>
public class GetUsersUseCase : IGetUsersUseCase
{
    /// <inheritdoc cref="IApplicationDbContext"/>
    private readonly IApplicationDbContext _context;

    /// <inheritdoc cref="ICacheService"/>
    private readonly ICacheService _cacheService;


    public GetUsersUseCase(IApplicationDbContext context, ICacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    /// <inheritdoc />
    public async Task<Result<List<UserShortDataResponse>>> Execute(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var cacheKey = $"{nameof(User)}s_page{query.Page}_itemperpage{query.ItemPerPage}_sort{query.SortBy}";
        
        var users = await _cacheService
            .GetAsync<List<UserShortDataResponse>?>(
                cacheKey,
                cancellationToken);

        if (users is not null)
        {
            return Result.Success(users);
        }

        users = await _context.Users
            .Where(x => !x.IsDelete)
            .SortBy(query.SortBy)
            .Skip((query.Page - 1) * query.ItemPerPage)
            .Take(query.ItemPerPage)
            .Select(x=> new UserShortDataResponse(
                    x.Id, x.FirstName, 
                    x.SecondName, 
                    x.Patronymic, 
                    x.Employee == null))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (users.Any())
        {
            await _cacheService
                .SetAsync(cacheKey, users, cancellationToken);
        }

        return Result.Success(users);
    }
}