namespace CodeSentryAI.Application.Features.Authentication.DTOs;

/// <summary>
/// Authentication response returned after successful registration/login.
/// </summary>
public sealed class AuthResponseDto
{
    public Guid UserId { get; init; }

    public string FullName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string AccessToken { get; init; } = string.Empty;

    public string RefreshToken { get; init; } = string.Empty;
}
