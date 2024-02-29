namespace CircleCoordinator.Contracts.Models;

public class CircleSet
{
    public Guid Id { get; init; }
    public List<Coordinator> Coordinators { get; init; }
}
