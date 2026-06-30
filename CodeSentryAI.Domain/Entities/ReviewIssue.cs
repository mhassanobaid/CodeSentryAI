using CodeSentryAI.Domain.Common;
using CodeSentryAI.Domain.Enums;

namespace CodeSentryAI.Domain.Entities;

/// <summary>
/// Represents a single issue detected during AI code review.
/// </summary>
public class ReviewIssue : BaseEntity
{
    private ReviewIssue()
    {
        Title = string.Empty;
        Description = string.Empty;
        Suggestion = string.Empty;

        ReviewResult = null!;
    }

    public ReviewIssue(
        Guid reviewResultId,
        int lineNumber,
        IssueSeverity severity,
        IssueCategory category,
        string title,
        string description,
        string suggestion)
    {
        if (lineNumber < 1)
            throw new ArgumentOutOfRangeException(nameof(lineNumber));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.");

        ReviewResultId = reviewResultId;
        LineNumber = lineNumber;
        Severity = severity;
        Category = category;
        Title = title.Trim();
        Description = description.Trim();
        Suggestion = suggestion?.Trim() ?? string.Empty;
    }

    public Guid ReviewResultId { get; private set; }

    public int LineNumber { get; private set; }

    public IssueSeverity Severity { get; private set; }

    public IssueCategory Category { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string Suggestion { get; private set; }

    public ReviewResult ReviewResult { get; private set; }
}
