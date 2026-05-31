using ImposterSyndrome.Domain.Programme.Wendler531;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImposterSyndrome.Infrastructure.Persistence.Configuration;

public sealed class Wendler531ProgrammeConfiguration : AuditableConfiguration<Wendler531Programme>
{
    public override void PostConfigure(EntityTypeBuilder<Wendler531Programme> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(Wendler531Programme.NameMaxLength)
            .IsRequired();

        builder.ToTable("Wendler531Programmes");
    }
}
