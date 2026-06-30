using CodeSentryAI.Domain.Entities;

namespace CodeSentryAI.Application.Interfaces.Persistence;

public interface IReviewRepository
{
    Task AddAsync(
        Review review,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Review review,
        CancellationToken cancellationToken = default);

    Task<Review?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Review>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Review>> GetPendingReviewsAsync(
        CancellationToken cancellationToken = default);
}
