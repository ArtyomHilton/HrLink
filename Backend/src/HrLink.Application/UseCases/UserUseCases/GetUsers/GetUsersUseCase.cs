using FluentValidation;
using HrLink.Application.Common.Results;
using HrLink.Application.Common.Results.Errors;
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

    private readonly IValidator<GetUsersQuery> _validator;


    public GetUsersUseCase(IApplicationDbContext context, ICacheService cacheService, IValidator<GetUsersQuery> validator)
    {
        _context = context;
        _cacheService = cacheService;
        _validator = validator;
    }

    /// <inheritdoc />
    public async Task<Result<List<UserShortDataResponse>>> Execute(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var validateResult = await _validator.ValidateAsync(query, cancellationToken);

        if (!validateResult.IsValid)
        {
            return Result.Failure<List<UserShortDataResponse>>(new ValidateError(validateResult.Errors[0].ErrorCode,
                validateResult.Errors[0].PropertyName));
        }
        
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
                    x.Employee != null))
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