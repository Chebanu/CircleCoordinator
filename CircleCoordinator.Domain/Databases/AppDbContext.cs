using CircleCoordinator.Domain.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CircleCoordinator.Domain.Databases;

public class AppDbContext : DbContext
{
    public DbSet<Coordinator> Coordinators { get; init; }

    public AppDbContext(DbContextOptions<AppDbContext> context) : base(context)
    {
    }
}