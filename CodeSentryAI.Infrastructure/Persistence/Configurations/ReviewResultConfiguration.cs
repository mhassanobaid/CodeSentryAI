using CodeSentryAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeSentryAI.Infrastructure.Persistence.Configurations;

public sealed class ReviewResultConfiguration : IEntityTypeConfiguration<ReviewResult>
{
    public void Configure(EntityTypeBuilder<ReviewResult> builder)
    {
        builder.ToTable("ReviewResults");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.QualityScore)
            .IsRequired();

        builder.Property(x => x.Summary)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(x => x.ProcessingDurationMs)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(x => x.Review)
            .WithOne(x => x.Result)
            .HasForeignKey<ReviewResult>(x => x.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Issues)
            .WithOne(x => x.ReviewResult)
            .HasForeignKey(x => x.ReviewResultId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
