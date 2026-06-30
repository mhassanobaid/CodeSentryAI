using CodeSentryAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeSentryAI.Infrastructure.Persistence.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Review> Reviews => Set<Review>();

    public DbSet<ReviewFile> ReviewFiles => Set<ReviewFile>();

    public DbSet<ReviewResult> ReviewResults => Set<ReviewResult>();

    public DbSet<ReviewIssue> ReviewIssues => Set<ReviewIssue>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}