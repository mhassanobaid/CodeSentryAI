using CodeSentryAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeSentryAI.Infrastructure.Persistence.Configurations;

public sealed class ReviewFileConfiguration : IEntityTypeConfiguration<ReviewFile>
{
    public void Configure(EntityTypeBuilder<ReviewFile> builder)
    {
        builder.ToTable("ReviewFiles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OriginalFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.StoredFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.StoragePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Extension)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(x => x.Review)
            .WithMany(x => x.Files)
            .HasForeignKey(x => x.ReviewId);
    }
}
