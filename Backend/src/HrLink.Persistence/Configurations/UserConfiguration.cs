using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLink.Persistence.Configurations;

/// <summary>
/// Конфигурация <see cref="User"/>.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(x=> x.SecondName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Patronymic)
            .HasMaxLength(50);

        builder.Property(x => x.DateOfBirthday)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .IsRequired();
        
        builder.Property(x => x.IsDelete)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.EmployeeId)
            .IsRequired(false);
        builder.HasIndex(x => x.EmployeeId)
            .IsUnique();

        builder.HasOne(x => x.Employee)
            .WithOne(x => x.User)
            .HasForeignKey<User>(x => x.EmployeeId)
            .IsRequired(false);
    }
}