using CodeSentryAI.Application.Interfaces.Persistence;
using CodeSentryAI.Domain.Entities;
using CodeSentryAI.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeSentryAI.Infrastructure.Persistence.Repositories;

public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default)
    {
        await _context.RefreshTokens.AddAsync(
            refreshToken,
            cancellationToken);
    }

    public Task UpdateAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default)
    {
        _context.RefreshTokens.Update(refreshToken);

        return Task.CompletedTask;
    }

    public async Task<RefreshToken?> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(
                x => x.Token == token && !x.IsDeleted,
                cancellationToken);
    }
}