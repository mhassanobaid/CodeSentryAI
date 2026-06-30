using CodeSentryAI.Application.Interfaces.Persistence;
using CodeSentryAI.Domain.Entities;
using CodeSentryAI.Domain.Enums;
using CodeSentryAI.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeSentryAI.Infrastructure.Persistence.Repositories;

public sealed class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Review review,
        CancellationToken cancellationToken = default)
    {
        await _context.Reviews.AddAsync(review, cancellationToken);
    }

    public Task UpdateAsync(
        Review review,
        CancellationToken cancellationToken = default)
    {
        _context.Reviews.Update(review);

        return Task.CompletedTask;
    }

    public async Task<Review?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .Include(x => x.Files)
            .Include(x => x.Result)
                .ThenInclude(x => x!.Issues)
            .FirstOrDefaultAsync(
                x => x.Id == id && !x.IsDeleted,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Review>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .AsNoTracking()
            .Where(x => x.UserId == userId && !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Review>> GetPendingReviewsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .Where(x =>
                x.Status == ReviewStatus.Pending &&
                !x.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}