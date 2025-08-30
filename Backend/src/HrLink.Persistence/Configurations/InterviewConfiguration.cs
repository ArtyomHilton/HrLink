using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLink.Persistence.Configurations;

/// <summary>
/// Конфигурация <see cref="Interview"/>.
/// </summary>
public class InterviewConfiguration : IEntityTypeConfiguration<Interview>
{
    public void Configure(EntityTypeBuilder<Interview> builder)
    {
        builder.ToTable("Interview");
        
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
            .IsUnique();
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.CandidateId)
            .IsRequired();
        
        builder.Property(x=> x.EmployeeId)
            .IsRequired();

        builder.Property(x=> x.InterviewDateTime)
            .IsRequired();

        builder.HasOne(x => x.Candidate)
            .WithMany(x => x.Interviews)
            .HasForeignKey(x => x.CandidateId);

        builder.HasOne(x => x.Employee)
            .WithMany(x => x.Interviews)
            .HasForeignKey(x => x.EmployeeId);
    }
}