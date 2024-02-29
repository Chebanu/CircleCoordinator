using CircleCoordinator.Domain.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CircleCoordinator.Domain.Databases;

public class AppDbContext : DbContext
{
    public DbSet<Coordinator> Coordinators { get; set; }
    public DbSet<CircleSet> CircleSets { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> context) : base(context)
    {
    }
}