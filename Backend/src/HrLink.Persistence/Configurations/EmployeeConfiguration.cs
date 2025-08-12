using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLink.Persistence.Configurations;

/// <summary>
/// Конфигурация сущности <see cref="Employee"/>.
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

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(x=> x.SecondName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Patronymic)
            .HasMaxLength(50);

        builder.Property(x => x.DateOfEmployment)
            .IsRequired();
    }
}