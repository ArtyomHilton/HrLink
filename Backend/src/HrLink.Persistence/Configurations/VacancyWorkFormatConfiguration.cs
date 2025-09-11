using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLink.Persistence.Configurations;

public class VacancyWorkFormatConfiguration : IEntityTypeConfiguration<VacancyWorkFormat>
{
    public void Configure(EntityTypeBuilder<VacancyWorkFormat> builder)
    {
        builder.ToTable("VacancyWorkFormat");

        builder.HasKey(x => new { x.VacancyId, x.WorkFormatId });

        builder.HasOne(x => x.Vacancy)
            .WithMany(x => x.WorkFormats)
            .HasForeignKey(x => x.VacancyId)
            .IsRequired();

        builder.HasOne(x => x.WorkFormat)
            .WithMany(x => x.WorkFormats)
            .HasForeignKey(x => x.WorkFormatId)
            .IsRequired();
    }
}