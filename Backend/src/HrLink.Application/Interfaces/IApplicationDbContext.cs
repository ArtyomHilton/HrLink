using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Application.Interfaces;

/// <summary>
/// Контекст базы данных.
/// </summary>
public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<Candidate> Candidates { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<Interview> Interviews { get; set; }
    DbSet<Vacancy> Vacancies { get; set; }
    DbSet<VacancyStatus> VacancyStatuses { get; set; }
    DbSet<VacancyWorkFormat> VacancyWorkFormats  { get; set; }
    DbSet<WorkFormat> WorkFormats { get; set; }
    DbSet<Status> Statuses { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}