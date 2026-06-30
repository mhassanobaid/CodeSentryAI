namespace CodeSentryAI.Domain.Common;

/// <summary>
/// Base class for all domain entities.
/// Provides common auditing and soft-delete functionality.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public Guid Id { get; protected set; } = Guid.NewGuid();

    /// <summary>
    /// UTC date/time when the entity was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; protected set; } = DateTime.UtcNow;

    /// <summary>
    /// UTC date/time when the entity was last modified.
    /// </summary>
    public DateTime? UpdatedAtUtc { get; protected set; }

    /// <summary>
    /// Indicates whether the entity has been soft deleted.
    /// </summary>
    public bool IsDeleted { get; protected set; }

    /// <summary>
    /// UTC date/time when the entity was soft deleted.
    /// </summary>
    public DateTime? DeletedAtUtc { get; protected set; }

    protected void MarkAsUpdated()
    {
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void SoftDelete()
    {
        if (IsDeleted)
            return;

        IsDeleted = true;
        DeletedAtUtc = DateTime.UtcNow;

        MarkAsUpdated();
    }
}
