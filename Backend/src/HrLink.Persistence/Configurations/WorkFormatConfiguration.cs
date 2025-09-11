using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLink.Persistence.Configurations;

public class WorkFormatConfiguration : IEntityTypeConfiguration<WorkFormat>
{
    public void Configure(EntityTypeBuilder<WorkFormat> builder)
    {
        builder.ToTable("WorkFormat");

        builder.HasKey(x => x.Id);
        
        builder.Property(x=> x.Name)
            .IsRequired();
        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}