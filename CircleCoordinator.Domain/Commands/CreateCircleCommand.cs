using CircleCoordinator.Domain.Algorithm;
using CircleCoordinator.Domain.Handlers;
using CircleCoordinator.Domain.Helpers;
using CircleCoordinator.Domain.Models.Database;
using CircleCoordinator.Domain.Repositories;

using AutoMapper;
using MediatR;

using Microsoft.Extensions.Logging;

namespace CircleCoordinator.Domain.Commands;

public class CreateCircleCoordinatorCommand : IRequest<CreateCircleCoordinatorResult>
{
    public int X { get; init; }
    public int Y { get; init; }
}

public class CreateCircleCoordinatorResult
{
    public Contracts.Models.CircleSet CircleSet { get; init; }
    public bool Success { get; init; }
}

internal class CreateCircleCoordinatorCommandHandler :
    BaseRequestHandler<CreateCircleCoordinatorCommand, CreateCircleCoordinatorResult>
{
    private readonly ICoordinatorRepository _repository;
    private readonly ICircleDrawer _circleDrawer;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;

    public CreateCircleCoordinatorCommandHandler(ICoordinatorRepository repository,
                                            ICircleDrawer circleDrawer,
                                            IDateTimeProvider dateTimeProvider,
                                            ILogger<BaseRequestHandler
                                            <CreateCircleCoordinatorCommand, CreateCircleCoordinatorResult>> logger,
                                            IMapper mapper) : base(logger)
    {
        _repository = repository;
        _circleDrawer = circleDrawer;
        _dateTimeProvider = dateTimeProvider;
        _mapper = mapper;
    }
    protected override async Task<CreateCircleCoordinatorResult> HandleInternal(CreateCircleCoordinatorCommand request,
                                                                                CancellationToken cancellationToken)
    {
        var circleCoordinate = _circleDrawer.DrawCircle(request.X, request.Y);

        var now = _dateTimeProvider.Now;

        var circleId = Guid.NewGuid();

        var drawnCircle = new Coordinator
        {
            Id = Guid.NewGuid(),
            CreatedAt = now,
            X = request.X,
            Y = request.Y,
            Diameter = circleCoordinate.Diameter,
            Color = circleCoordinate.Color,
            ModifiedAt = now,
            CircleSetId = circleId
        };

        var circle = new CircleSet
        {
            Id = circleId,
            Coordinators = new List<Coordinator> { drawnCircle }
        };

        _ = await _repository.CreateCoordinates(circle);

        var coordinatorResult = new Coordinator
        {
            Id = drawnCircle.Id,
            CreatedAt = drawnCircle.CreatedAt,
            X = circleCoordinate.X,
            Y = circleCoordinate.Y,
            Diameter = circleCoordinate.Diameter,
            Color = circleCoordinate.Color,
            ModifiedAt = drawnCircle.ModifiedAt
        };

        return new CreateCircleCoordinatorResult
        {
            CircleSet = new Contracts.Models.CircleSet
            {
                Id = circle.Id,
                Coordinators = new List<Contracts.Models.Coordinator>
                {
                    _mapper.Map<Contracts.Models.Coordinator>(coordinatorResult)
                }
            },
            Success = true
        };
    }
}