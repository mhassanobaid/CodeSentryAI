using CodeSentryAI.Domain.Common;

namespace CodeSentryAI.Domain.Entities;

/// <summary>
/// Represents a refresh token used to renew JWT access tokens.
/// </summary>
public class RefreshToken : BaseEntity
{
    private RefreshToken()
    {
        Token = string.Empty;
        User = null!;
    }

    public RefreshToken(
        Guid userId,
        string token,
        DateTime expiresAtUtc)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Refresh token is required.");

        UserId = userId;
        Token = token;
        ExpiresAtUtc = expiresAtUtc;
    }

    public Guid UserId { get; private set; }

    public string Token { get; private set; }

    public DateTime ExpiresAtUtc { get; private set; }

    public DateTime? RevokedAtUtc { get; private set; }

    public User User { get; private set; }

    public bool IsExpired =>
        DateTime.UtcNow >= ExpiresAtUtc;

    public bool IsActive =>
        RevokedAtUtc is null && !IsExpired;

    public void Revoke()
    {
        if (RevokedAtUtc is not null)
            return;

        RevokedAtUtc = DateTime.UtcNow;

        MarkAsUpdated();
    }
}
