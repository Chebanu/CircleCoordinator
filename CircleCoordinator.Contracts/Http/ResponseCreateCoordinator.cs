using CircleCoordinator.Contracts.Models;

namespace CircleCoordinator.Contracts.Http;

public class ResponseCreateCoordinator
{
    public CircleSet Coordinator { get; init; }
    public bool Success { get; init; }
}