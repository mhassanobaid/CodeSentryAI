using CodeSentryAI.Domain.Common;

namespace CodeSentryAI.Domain.Entities;

/// <summary>
/// Represents an application user.
/// </summary>
public class User : BaseEntity
{
    private readonly List<Review> _reviews = [];
    private readonly List<RefreshToken> _refreshTokens = [];

    private User()
    {
        FullName = string.Empty;
        Email = string.Empty;
        PasswordHash = string.Empty;
    }

    public User(
        string fullName,
        string email,
        string passwordHash)
    {
        SetFullName(fullName);
        SetEmail(email);
        SetPasswordHash(passwordHash);
    }

    public string FullName { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    public void SetFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.");

        FullName = fullName.Trim();

        MarkAsUpdated();
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

        Email = email.Trim().ToLowerInvariant();

        MarkAsUpdated();
    }

    public void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required.");

        PasswordHash = passwordHash;

        MarkAsUpdated();
    }

    public void AddReview(Review review)
    {
        ArgumentNullException.ThrowIfNull(review);

        _reviews.Add(review);

        MarkAsUpdated();
    }

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);

        _refreshTokens.Add(refreshToken);

        MarkAsUpdated();
    }
}
