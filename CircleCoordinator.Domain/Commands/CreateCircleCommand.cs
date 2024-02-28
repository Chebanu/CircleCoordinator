using CircleCoordinator.Domain.Algorithm;
using CircleCoordinator.Domain.Handlers;
using CircleCoordinator.Domain.Helpers;
using CircleCoordinator.Domain.Models.Database;
using CircleCoordinator.Domain.Repositories;

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
    public Guid Id { get; init; }
    public string[] Errors { get; init; }
    public bool Success { get; init; }
}

internal class CreateCircleCoordinatorCommandHandler :
    BaseRequestHandler<CreateCircleCoordinatorCommand, CreateCircleCoordinatorResult>
{
    private readonly ICoordinatorRepository _repository;
    private readonly ICircleDrawer _circleDrawer;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateCircleCoordinatorCommandHandler(ICoordinatorRepository repository, ICircleDrawer circleDrawer, IDateTimeProvider dateTimeProvider, ILogger<BaseRequestHandler<CreateCircleCoordinatorCommand, CreateCircleCoordinatorResult>> logger) : base(logger)
    {
        _repository = repository;
        _circleDrawer = circleDrawer;
        _dateTimeProvider = dateTimeProvider;
    }
    protected override async Task<CreateCircleCoordinatorResult> HandleInternal(CreateCircleCoordinatorCommand request, CancellationToken cancellationToken)
    {
        var circleCoordinate = _circleDrawer.DrawCircle(request.X, request.Y);

        var now = _dateTimeProvider.Now;

        var drawnCircle = new Coordinator
        {
            Id = Guid.NewGuid(),
            CreatedAt = now,
            X = request.X,
            Y = request.Y,
            Diameter = circleCoordinate.Diameter,
            Color = circleCoordinate.Color
        };

        _ = await _repository.CreateCoordinates(drawnCircle);

        return new CreateCircleCoordinatorResult
        {
            Id = drawnCircle.Id,
            Success = true
        };
    }
}