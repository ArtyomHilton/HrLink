using HrLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrLink.Persistence.Configurations;

public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("Candidate");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired();
        builder.HasIndex(x => x.Id)
            .IsUnique();

        builder.Property(x => x.FirstName)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(x => x.SecondName)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(x => x.Patronymic)
            .HasMaxLength(50)
            .IsRequired(false);
        
        builder.Property(x => x.PhoneNumber)
            .IsRequired(false);
        
        builder.Property(x => x.Email)
            .IsRequired();

        builder.Ignore(x => x.FullName);
    }
}