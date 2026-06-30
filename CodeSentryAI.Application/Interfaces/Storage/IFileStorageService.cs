namespace CodeSentryAI.Application.Interfaces.Storage;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(
        Stream stream,
        string fileName,
        CancellationToken cancellationToken = default);
}
