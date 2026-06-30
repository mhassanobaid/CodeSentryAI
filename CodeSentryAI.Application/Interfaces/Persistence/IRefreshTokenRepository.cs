using CodeSentryAI.Domain.Entities;

namespace CodeSentryAI.Application.Interfaces.Persistence;

public interface IRefreshTokenRepository
{
    Task AddAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default);

    Task<RefreshToken?> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken = default);
}
