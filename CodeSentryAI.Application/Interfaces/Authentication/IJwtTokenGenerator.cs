using CodeSentryAI.Domain.Entities;

namespace CodeSentryAI.Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);

    RefreshToken GenerateRefreshToken(Guid userId);
}
