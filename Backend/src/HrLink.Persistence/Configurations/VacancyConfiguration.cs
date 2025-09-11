using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLink.Persistence.Configurations;

public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
{
    public void Configure(EntityTypeBuilder<Vacancy> builder)
    {
        builder.ToTable("Vacancy",
            t =>
            {
                t.HasCheckConstraint("ValidMinSalary", "\"MinSalary\" IS NULL OR \"MinSalary\" > 0");
                t.HasCheckConstraint("ValidMaxSalary", $"\"MaxSalary\" IS NULL OR \"MaxSalary\" <= {decimal.MaxValue}");
            });
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Position)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.MinSalary)
            .IsRequired(false);

        builder.Property(x => x.MaxSalary)
            .IsRequired(false);

        builder.Property(x => x.StatusId)
            .IsRequired();

        builder.HasOne(x => x.Status)
            .WithMany(x => x.Vacancies)
            .HasForeignKey(x=> x.StatusId)
            .IsRequired();
    }
}