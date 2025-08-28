using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ImposterSyndrome.Domain.Common.Base;

namespace ImposterSyndrome.Infrastructure.Persistence.Configuration;

public abstract class AuditableConfiguration<T> : IEntityTypeConfiguration<T>
    where T : Auditable
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(Auditable.CreatedByMaxLength)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedBy)
            .HasMaxLength(Auditable.UpdatedByMaxLength);

        PostConfigure(builder);
    }

    public abstract void PostConfigure(EntityTypeBuilder<T> builder);
}