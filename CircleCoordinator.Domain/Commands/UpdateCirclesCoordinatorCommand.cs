using CircleCoordinator.Domain.Algorithm;
using CircleCoordinator.Domain.Handlers;
using CircleCoordinator.Domain.Helpers;
using CircleCoordinator.Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CircleCoordinator.Domain.Commands;

public class UpdateCirclesCoordinatorCommand : IRequest<UpdateCirclesResult>
{
    public Guid Id { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
}

public class UpdateCirclesResult
{
    public string[] Errors { get; init; }
    public bool Success { get; init; }
}

internal class UpdateCirclesCoordinatorCommandHandler :
    BaseRequestHandler<UpdateCirclesCoordinatorCommand, UpdateCirclesResult>
{
    private readonly ICoordinatorRepository _repository;
    private readonly ICircleDrawer _circleDrawer;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateCirclesCoordinatorCommandHandler(ICircleDrawer circleDrawer,
                                                IDateTimeProvider dateTimeProvider,
                                                ICoordinatorRepository repository,
                                                ILogger<BaseRequestHandler<
                                                    UpdateCirclesCoordinatorCommand, UpdateCirclesResult>> logger) : base(logger)
    {
        _repository = repository;
        _circleDrawer = circleDrawer;
        _dateTimeProvider = dateTimeProvider;
    }

    protected override async Task<UpdateCirclesResult> HandleInternal(UpdateCirclesCoordinatorCommand request,
                                                                    CancellationToken cancellationToken)
    {
        var getCircle = await _repository.GetCircleSet(request.Id, cancellationToken);

        if (getCircle == null)
        {
            return new UpdateCirclesResult
            {
                Success = false,
                Errors = ["Circle set doesn't exist"]
            };
        }

        var now = _dateTimeProvider.Now;

        var modifiedCircle = _circleDrawer.DrawCircle(request.X, request.Y);

        var coordinator = new Models.Database.Coordinator
        {
            Id = Guid.NewGuid(),
            X = modifiedCircle.X,
            Y = modifiedCircle.Y,
            Diameter = modifiedCircle.Diameter,
            Color = modifiedCircle.Color,
            ModifiedAt = now,
            CircleSetId= getCircle.Id            
        };

        await _repository.UpdateCoordinates(coordinator, cancellationToken);

        return new UpdateCirclesResult
        {
            Success = true,
        };
    }
}