using MediatR;

namespace CodeSentryAI.Application.Features.Authentication.Login;

public sealed record LoginUserCommand
(
    string Email,
    string Password
) : IRequest<LoginUserResponse>;
