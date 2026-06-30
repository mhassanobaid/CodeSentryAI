using MediatR;
using CodeSentryAI.Domain.Enums;

namespace CodeSentryAI.Application.Features.Reviews.CreateReview;

public sealed record CreateReviewCommand
(
    Guid UserId,
    string Title,
    ReviewType ReviewType
) : IRequest<CreateReviewResponse>;
