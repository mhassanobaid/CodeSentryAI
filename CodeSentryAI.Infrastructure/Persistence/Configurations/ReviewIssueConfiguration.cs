using CodeSentryAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeSentryAI.Infrastructure.Persistence.Configurations;

public sealed class ReviewIssueConfiguration : IEntityTypeConfiguration<ReviewIssue>
{
    public void Configure(EntityTypeBuilder<ReviewIssue> builder)
    {
        builder.ToTable("ReviewIssues");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.LineNumber);

        builder.Property(x => x.Severity)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Category)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(x => x.Suggestion)
            .HasMaxLength(4000);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(x => x.ReviewResult)
            .WithMany(x => x.Issues)
            .HasForeignKey(x => x.ReviewResultId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}