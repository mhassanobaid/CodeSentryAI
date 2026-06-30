using CodeSentryAI.Domain.Common;

namespace CodeSentryAI.Domain.Entities;

/// <summary>
/// Represents the AI analysis result.
/// </summary>
public class ReviewResult : BaseEntity
{
    private readonly List<ReviewIssue> _issues = [];

    private ReviewResult()
    {
        Summary = string.Empty;

        Review = null!;
    }

    public ReviewResult(
        Guid reviewId,
        int qualityScore,
        string summary,
        long processingDurationMs)
    {
        if (qualityScore < 0 || qualityScore > 100)
            throw new ArgumentOutOfRangeException(nameof(qualityScore));

        if (string.IsNullOrWhiteSpace(summary))
            throw new ArgumentException("Summary is required.");

        ReviewId = reviewId;
        QualityScore = qualityScore;
        Summary = summary.Trim();
        ProcessingDurationMs = processingDurationMs;
    }

    public Guid ReviewId { get; private set; }

    public int QualityScore { get; private set; }

    public string Summary { get; private set; }

    public long ProcessingDurationMs { get; private set; }

    public Review Review { get; private set; }

    public IReadOnlyCollection<ReviewIssue> Issues =>
        _issues.AsReadOnly();

    public void AddIssue(ReviewIssue issue)
    {
        ArgumentNullException.ThrowIfNull(issue);

        _issues.Add(issue);

        MarkAsUpdated();
    }
}
