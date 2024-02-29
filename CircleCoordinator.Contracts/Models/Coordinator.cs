namespace CircleCoordinator.Contracts.Models;

public class Coordinator
{
    public Guid Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
    public int Diameter { get; init; }
    public string Color { get; init; }
    public DateTimeOffset ModifiedAt { get; init; }
}