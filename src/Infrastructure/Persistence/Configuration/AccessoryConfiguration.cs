using ImposterSyndrome.Domain.Accessories;
using ImposterSyndrome.Domain.Heroes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImposterSyndrome.Infrastructure.Persistence.Configuration;

public class AccessoryConfiguration : AuditableConfiguration<Accessory>
{
    public override void PostConfigure(EntityTypeBuilder<Accessory> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(Accessory.NameMaxLength)
            .IsRequired();

        builder.Property(t => t.BarbellExercise)
            .HasMaxLength(25)
            .IsRequired();

    }
}