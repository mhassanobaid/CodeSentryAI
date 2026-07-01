using CodeSentryAI.Application.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CodeSentryAI.Infrastructure.Authentication.Passwords;

public sealed class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null!, password);
    }

    public bool VerifyPassword(
        string password,
        string passwordHash)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            null!,
            passwordHash,
            password);

        return result == PasswordVerificationResult.Success
            || result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}