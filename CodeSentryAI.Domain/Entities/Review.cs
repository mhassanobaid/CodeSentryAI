using CodeSentryAI.Domain.Common;
using CodeSentryAI.Domain.Enums;

namespace CodeSentryAI.Domain.Entities;

/// <summary>
/// Represents an AI code review request.
/// Aggregate Root.
/// </summary>
public class Review : BaseEntity
{
    private readonly List<ReviewFile> _files = [];

    private Review()
    {
        Title = string.Empty;
        User = null!;
    }

    public Review(
        string title,
        ReviewType reviewType,
        Guid userId)
    {
        SetTitle(title);

        ReviewType = reviewType;
        UserId = userId;

        Status = ReviewStatus.Pending;
        QueuedAtUtc = DateTime.UtcNow;
    }

    public string Title { get; private set; }

    public ReviewStatus Status { get; private set; }

    public ReviewType ReviewType { get; private set; }

    public Guid UserId { get; private set; }

    public DateTime QueuedAtUtc { get; private set; }

    public DateTime? StartedAtUtc { get; private set; }

    public DateTime? CompletedAtUtc { get; private set; }

    public User User { get; private set; }

    public IReadOnlyCollection<ReviewFile> Files =>
        _files.AsReadOnly();

    public ReviewResult? Result { get; private set; }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Review title is required.");

        Title = title.Trim();

        MarkAsUpdated();
    }

    public void AddFile(ReviewFile file)
    {
        ArgumentNullException.ThrowIfNull(file);

        _files.Add(file);

        MarkAsUpdated();
    }

    public void SetResult(ReviewResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        Result = result;

        MarkAsUpdated();
    }

    public void MarkAsProcessing()
    {
        if (Status != ReviewStatus.Pending)
            throw new InvalidOperationException("Only pending reviews can be processed.");

        Status = ReviewStatus.Processing;
        StartedAtUtc = DateTime.UtcNow;

        MarkAsUpdated();
    }

    public void MarkAsCompleted()
    {
        if (Status != ReviewStatus.Processing)
            throw new InvalidOperationException("Only processing reviews can be completed.");

        Status = ReviewStatus.Completed;
        CompletedAtUtc = DateTime.UtcNow;

        MarkAsUpdated();
    }

    public void MarkAsFailed()
    {
        Status = ReviewStatus.Failed;
        CompletedAtUtc = DateTime.UtcNow;

        MarkAsUpdated();
    }
}
