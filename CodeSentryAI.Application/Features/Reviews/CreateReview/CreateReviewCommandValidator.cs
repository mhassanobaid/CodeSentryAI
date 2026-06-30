using FluentValidation;

namespace CodeSentryAI.Application.Features.Reviews.CreateReview;

public sealed class CreateReviewCommandValidator
    : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.ReviewType)
            .IsInEnum();
    }
}
