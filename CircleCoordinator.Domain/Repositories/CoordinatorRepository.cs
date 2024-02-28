using CircleCoordinator.Domain.Databases;
using CircleCoordinator.Domain.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace CircleCoordinator.Domain.Repositories;

public interface ICoordinatorRepository
{
    Task<Coordinator> CreateCoordinates(Coordinator coordinator, CancellationToken cancellationToken = default);
    Task<Coordinator> GetCoordinates(Guid id, CancellationToken cancellationToken = default);
}

public class CoordinatorRepository : ICoordinatorRepository
{
    private readonly AppDbContext _dbContext;

    public CoordinatorRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Coordinator> CreateCoordinates(Coordinator coordinator, CancellationToken cancellationToken = default)
    {
        _ = await _dbContext.AddAsync(coordinator, cancellationToken);
        _ = await _dbContext.SaveChangesAsync(cancellationToken);

        return coordinator;
    }

    public Task<Coordinator> GetCoordinates(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Coordinators.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}