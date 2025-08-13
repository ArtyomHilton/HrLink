using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLink.Persistence.Configurations;

/// <summary>
/// Конфигурация для сущности <see cref="Role"/>.
/// </summary>
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
            .IsUnique();
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired();
    }
}