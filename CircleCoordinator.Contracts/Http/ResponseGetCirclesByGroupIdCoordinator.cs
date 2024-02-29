using CircleCoordinator.Contracts.Models;

namespace CircleCoordinator.Contracts.Http;

public class ResponseGetCirclesByGroupIdCoordinator
{
    public CircleSet Coordinators { get; init; }
    public bool Success { get; init; }
}