using CodeSentryAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeSentryAI.Infrastructure.Persistence.Configurations;

public sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("Reviews");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.ReviewType)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.QueuedAtUtc);

        builder.Property(x => x.StartedAtUtc);

        builder.Property(x => x.CompletedAtUtc);

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Files)
            .WithOne(x => x.Review)
            .HasForeignKey(x => x.ReviewId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Result)
            .WithOne(x => x.Review)
            .HasForeignKey<ReviewResult>(x => x.ReviewId);
    }
}