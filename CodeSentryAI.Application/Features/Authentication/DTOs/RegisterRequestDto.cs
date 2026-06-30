namespace CodeSentryAI.Application.Features.Authentication.DTOs;

/// <summary>
/// Registration request from the client.
/// </summary>
public sealed class RegisterRequestDto
{
    public string FullName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}
