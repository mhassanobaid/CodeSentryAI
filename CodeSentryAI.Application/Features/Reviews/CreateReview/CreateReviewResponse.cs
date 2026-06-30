namespace CodeSentryAI.Application.Features.Reviews.CreateReview;

public sealed record CreateReviewResponse
(
    Guid ReviewId,
    string Status
);
