using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrLink.Persistence.Context;

/// <summary>
/// <see cref="DbContext"/>.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Candidate> Candidates => Set<Candidate>();
}