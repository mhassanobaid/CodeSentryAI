using CodeSentryAI.Domain.Common;

namespace CodeSentryAI.Domain.Entities;

/// <summary>
/// Represents a source code file uploaded for AI review.
/// </summary>
public class ReviewFile : BaseEntity
{
    private ReviewFile()
    {
        OriginalFileName = string.Empty;
        StoredFileName = string.Empty;
        StoragePath = string.Empty;
        Extension = string.Empty;

        Review = null!;
    }

    public ReviewFile(
        Guid reviewId,
        string originalFileName,
        string storedFileName,
        string storagePath,
        string extension,
        long fileSize)
    {
        if (string.IsNullOrWhiteSpace(originalFileName))
            throw new ArgumentException("Original file name is required.");

        if (string.IsNullOrWhiteSpace(storedFileName))
            throw new ArgumentException("Stored file name is required.");

        if (string.IsNullOrWhiteSpace(storagePath))
            throw new ArgumentException("Storage path is required.");

        if (fileSize <= 0)
            throw new ArgumentException("File size must be greater than zero.");

        ReviewId = reviewId;
        OriginalFileName = originalFileName.Trim();
        StoredFileName = storedFileName.Trim();
        StoragePath = storagePath.Trim();
        Extension = extension.Trim().ToLowerInvariant();
        FileSize = fileSize;
    }

    public Guid ReviewId { get; private set; }

    public string OriginalFileName { get; private set; }

    public string StoredFileName { get; private set; }

    public string StoragePath { get; private set; }

    public string Extension { get; private set; }

    public long FileSize { get; private set; }

    public Review Review { get; private set; }
}
