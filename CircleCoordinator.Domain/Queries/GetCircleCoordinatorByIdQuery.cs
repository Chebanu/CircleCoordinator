using CircleCoordinator.Domain.Algorithm;
using CircleCoordinator.Domain.Handlers;
using CircleCoordinator.Domain.Repositories;

using MediatR;
using AutoMapper;

using Microsoft.Extensions.Logging;
using CircleCoordinator.Domain.Models.Algorithm;

namespace CircleCoordinator.Domain.Queries;

public class GetCircleCoordinatorByIdQuery : IRequest<GetCirclesCoordinatorByIdResult>
{
    public Guid Id { get; init; }
}

public class GetCirclesCoordinatorByIdResult
{
    public Contracts.Models.CircleSet Circles { get; init; }
    public bool Success { get; init; }
    public string[] Errors { get; init; }
}

internal class GetCircleCoordinatorHandler : BaseRequestHandler<GetCircleCoordinatorByIdQuery, GetCirclesCoordinatorByIdResult>
{
    private readonly ICoordinatorRepository _repository;
    private readonly ICircleDrawer _circleDrawer;
    private readonly IMapper _mapper;

    public GetCircleCoordinatorHandler(ICoordinatorRepository repository,
                                        ICircleDrawer circleDrawer,
                                        IMapper mapper,
                                        ILogger<BaseRequestHandler
                                            <GetCircleCoordinatorByIdQuery, GetCirclesCoordinatorByIdResult>> logger) : base(logger)
    {
        _repository = repository;
        _circleDrawer = circleDrawer;
        _mapper = mapper;
    }
    protected override async Task<GetCirclesCoordinatorByIdResult> HandleInternal(GetCircleCoordinatorByIdQuery request,
                                                                                    CancellationToken cancellationToken)
    {
        var getCircles = await _repository.GetCircleSet(request.Id);

        if (getCircles == null)
        {
            return new GetCirclesCoordinatorByIdResult
            {
                Success = false,
                Errors = ["CircleSet doesn't exist"]
            };
        }

        var circles = new List<Circle>();

        circles.AddRange(getCircles.Coordinators.Select(data => new Circle
        {
            X = data.X,
            Y = data.Y,
            Color = data.Color,
            Diameter = data.Diameter
        }));

        var modifiedCircles = new List<Circle>();

        modifiedCircles.AddRange(circles.Select(circle => _circleDrawer.DrawCircle(circle)));

        var dtoUpdatedCircles = new List<Contracts.Models.Coordinator>();

        dtoUpdatedCircles.AddRange(modifiedCircles.Select(coord => new Contracts.Models.Coordinator
        {
            Id = getCircles.Id,
            X = coord.X,
            Y = coord.Y,
            Diameter = coord.Diameter,
            Color = coord.Color
        }));

        var dtoCircleSet = _mapper.Map<Contracts.Models.CircleSet>(new Contracts.Models.CircleSet
        {
            Id = request.Id,
            Coordinators = dtoUpdatedCircles
        });

        return new GetCirclesCoordinatorByIdResult
        {
            Circles = dtoCircleSet,
            Success = true
        };
    }
}