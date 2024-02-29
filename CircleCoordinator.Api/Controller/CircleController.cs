using CircleCoordinator.Contracts.Http;
using CircleCoordinator.Domain.Commands;
using CircleCoordinator.Domain.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CircleCoordinator.Api.Controller;

[Route("circles")]
public class CircleController : ControllerBase
{
    private readonly IMediator _mediator;

    public CircleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetCircles(Guid id, CancellationToken cancellationToken)
    {
        GetCircleCoordinatorByIdQuery query = new GetCircleCoordinatorByIdQuery
        {
            Id = id
        };

        GetCirclesCoordinatorByIdResult result = await _mediator.Send(query, cancellationToken);

        return result.Success ? Ok(new ResponseGetCirclesByGroupIdCoordinator
        {
            Coordinators = result.Circles,
            Success = result.Success
        })
        :
        BadRequest(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateCircle([FromBody] RequestCreateCoordinator requestCoordinator, CancellationToken cancellationToken)
    {
        CreateCircleCoordinatorCommand command = new CreateCircleCoordinatorCommand
        {
            X = requestCoordinator.X,
            Y = requestCoordinator.Y
        };

        CreateCircleCoordinatorResult result = await _mediator.Send(command, cancellationToken);

        return Created($"circles/{result.CircleSet.Id}", new ResponseCreateCoordinator
        {
            Coordinator = result.CircleSet,
            Success = result.Success
        });
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateCircles([FromBody] RequestUpdateCirclesCoordinator requestCoordinator,
                                                    CancellationToken cancellationToken)
    {
        UpdateCirclesCoordinatorCommand command = new UpdateCirclesCoordinatorCommand
        {
            Id = requestCoordinator.Id,
            X = requestCoordinator.X,
            Y = requestCoordinator.Y
        };

        UpdateCirclesResult result = await _mediator.Send(command, cancellationToken);

        return result.Success ? Ok() : BadRequest(result);
    }
}