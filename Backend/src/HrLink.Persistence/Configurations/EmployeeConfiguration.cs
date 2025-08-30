using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLink.Persistence.Configurations;

/// <summary>
/// Конфигурация <see cref="Employee"/>.
/// </summary>
public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employee");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
            .IsUnique();
        builder.Property(x => x.Id)
            .IsRequired();

        builder.HasIndex(x => x.WorkEmail)
            .IsUnique();
        builder.Property(x => x.WorkEmail)
            .IsRequired();
        
        builder.HasIndex(x => x.WorkPhoneNumber)
            .IsUnique();
        builder.Property(x => x.WorkPhoneNumber)
            .IsRequired(false);
        
        builder.Property(x => x.DateOfEmployment)
            .IsRequired();
    }
}