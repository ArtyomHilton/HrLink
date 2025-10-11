using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLink.Persistence.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.ToTable("Status");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.StatusName)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasIndex(x => x.StatusName)
            .IsUnique();

    }
}