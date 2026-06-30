using CodeSentryAI.Application.Interfaces.Persistence;
using CodeSentryAI.Domain.Entities;
using CodeSentryAI.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CodeSentryAI.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public Task UpdateAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        _context.Users.Update(user);

        return Task.CompletedTask;
    }

    public async Task<User?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id && !x.IsDeleted,
                cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(
                x => x.Email == email && !x.IsDeleted,
                cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AnyAsync(
                x => x.Email == email && !x.IsDeleted,
                cancellationToken);
    }
}