namespace CodeSentryAI.Application.Features.Authentication.Login;

public sealed record LoginUserResponse
(
    Guid UserId,
    string FullName,
    string Email,
    string AccessToken,
    string RefreshToken
);
