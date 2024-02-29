namespace CircleCoordinator.Contracts.Http;

public class ErrorResponse
{
    public IEnumerable<string> Errors { get; init; }
}