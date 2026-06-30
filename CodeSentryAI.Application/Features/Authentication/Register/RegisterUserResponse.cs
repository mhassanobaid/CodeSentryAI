namespace CodeSentryAI.Application.Features.Authentication.Register;

/// <summary>
/// Response returned after a successful user registration.
/// </summary>
public sealed record RegisterUserResponse
(
    Guid UserId,
    string FullName,
    string Email
);
