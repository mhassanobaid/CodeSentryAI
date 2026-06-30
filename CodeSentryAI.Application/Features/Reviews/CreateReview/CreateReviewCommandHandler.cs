using MediatR;
using CodeSentryAI.Application.Interfaces.Persistence;
using CodeSentryAI.Domain.Entities;

namespace CodeSentryAI.Application.Features.Reviews.CreateReview;

public sealed class CreateReviewCommandHandler
    : IRequestHandler<CreateReviewCommand, CreateReviewResponse>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReviewCommandHandler(
        IReviewRepository reviewRepository,
        IUnitOfWork unitOfWork)
    {
        _reviewRepository = reviewRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateReviewResponse> Handle(
        CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        var review = new Review(
            request.Title,
            request.ReviewType,
            request.UserId);

        await _reviewRepository.AddAsync(
            review,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new CreateReviewResponse(
            review.Id,
            review.Status.ToString());
    }
}
