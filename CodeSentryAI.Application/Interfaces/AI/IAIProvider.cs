namespace CodeSentryAI.Application.Interfaces.AI;

public interface IAIProvider
{
    Task<string> AnalyzeCodeAsync(
        string prompt,
        CancellationToken cancellationToken = default);
}
