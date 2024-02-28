using System.ComponentModel.DataAnnotations.Schema;

namespace CircleCoordinator.Domain.Models.Database;

[Table("coordinator")]
public class Coordinator
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("createdAt")]
    public DateTimeOffset CreatedAt { get; set; }
    [Column("x")]
    public int X { get; set; }
    [Column("y")]
    public int Y { get; set; }
    [Column("diameter")]
    public int Diameter { get; set; }
    [Column("color")]
    public string Color { get; set; }
}