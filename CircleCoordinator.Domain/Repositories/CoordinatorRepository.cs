using CircleCoordinator.Domain.Databases;
using CircleCoordinator.Domain.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CircleCoordinator.Domain.Repositories;

public interface ICoordinatorRepository
{
    Task<CircleSet> CreateCoordinates(CircleSet circle, CancellationToken cancellationToken = default);
    Task<CircleSet> GetCircleSet(Guid id, CancellationToken cancellationToken = default);
    Task<Coordinator> GetCoordinate(Guid id, CancellationToken cancellationToken = default);
    Task<Coordinator> UpdateCoordinates(Coordinator circleSet, CancellationToken cancellationToken = default);
}

public class CoordinatorRepository : ICoordinatorRepository
{
    private readonly AppDbContext _dbContext;

    public CoordinatorRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Coordinator> UpdateCoordinates(Coordinator circleSet, CancellationToken cancellationToken = default)
    {
        await _dbContext.Coordinators.AddAsync(circleSet, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return circleSet;
    }

    public async Task<CircleSet> CreateCoordinates(CircleSet circle, CancellationToken cancellationToken = default)
    {
        _ = await _dbContext.CircleSets.AddAsync(circle, cancellationToken);
        _ = await _dbContext.SaveChangesAsync(cancellationToken);

        return circle;
    }

    public async Task<CircleSet> GetCircleSet(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CircleSets.Include(x => x.Coordinators).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Coordinator> GetCoordinate(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Coordinators.FirstOrDefaultAsync(x => x.CircleSetId == id);
    }
}