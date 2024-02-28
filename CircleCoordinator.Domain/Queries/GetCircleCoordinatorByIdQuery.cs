using CircleCoordinator.Domain.Handlers;
using CircleCoordinator.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CircleCoordinator.Domain.Queries;

public class GetCircleCoordinatorByIdQuery : IRequest<GetCircleCoordinatorByIdResult>
{
    public Guid Id { get; init; }
}

public class GetCircleCoordinatorByIdResult
{
    public int X { get; init; }
    public int Y { get; init; }
    public int Diameter { get; init; }
    public string Color { get; init; }
    public string[] Errors { get; init; }
}

internal class GetCircleCoordinatorHandler : BaseRequestHandler<GetCircleCoordinatorByIdQuery, GetCircleCoordinatorByIdResult>
{
    private readonly ICoordinatorRepository _repository;

    public GetCircleCoordinatorHandler(ICoordinatorRepository repository,
        ILogger<BaseRequestHandler<GetCircleCoordinatorByIdQuery, GetCircleCoordinatorByIdResult>> logger) : base (logger)
    {
        _repository = repository;
    }
    protected override async Task<GetCircleCoordinatorByIdResult> HandleInternal(GetCircleCoordinatorByIdQuery request, CancellationToken cancellationToken)
    {
        return new GetCircleCoordinatorByIdResult();
    }
}