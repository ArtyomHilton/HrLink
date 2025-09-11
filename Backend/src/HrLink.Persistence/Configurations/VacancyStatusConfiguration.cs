using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLink.Persistence.Configurations;

public class VacancyStatusConfiguration : IEntityTypeConfiguration<VacancyStatus>
{
    public void Configure(EntityTypeBuilder<VacancyStatus> builder)
    {
        builder.ToTable("VacancyStatus");

        builder.HasKey(x => x.Id);

        builder.Property(x=> x.Name)
            .IsRequired();
        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}