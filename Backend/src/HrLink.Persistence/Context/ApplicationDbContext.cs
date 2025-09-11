using HrLink.Application.Interfaces;
using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Persistence.Context;

/// <summary>
/// <see cref="DbContext"/>.
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Interview> Interviews { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<VacancyStatus> VacancyStatuses { get; set; }
    public DbSet<VacancyWorkFormat> VacancyWorkFormats { get; set; }
    public DbSet<WorkFormat> WorkFormats { get; set; }
}