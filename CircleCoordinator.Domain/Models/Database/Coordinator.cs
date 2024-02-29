using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CircleCoordinator.Domain.Models.Database;

[Table("circleSet")]
public class CircleSet
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("coordinators")]
    public List<Coordinator> Coordinators { get; set; }
}

[Table("coordinator")]
public class Coordinator
{
    [Required]
    [Column("id")]
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Column("createdAt")]
    public DateTimeOffset CreatedAt { get; set; }

    [Required]
    [Column("x")]
    public int X { get; set; }

    [Required]
    [Column("y")]
    public int Y { get; set; }

    [Required]
    [Column("diameter")]
    public int Diameter { get; set; }

    [Required]
    [Column("color")]
    public string Color { get; set; }

    [Required]
    [Column("modifiedAt")]
    public DateTimeOffset ModifiedAt { get; set; }

    public virtual CircleSet CircleSet { get; set; }

    [Required]
    [ForeignKey(nameof(CircleSet))]
    public Guid CircleSetId { get; set; }
}