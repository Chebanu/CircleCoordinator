﻿using CircleCoordinator.Contracts.Http;
using CircleCoordinator.Domain.Constants;
using FluentValidation;

internal class CircleCoordinatorRequestCreateValidator : AbstractValidator<RequestCreateCoordinator>
{

    public CircleCoordinatorRequestCreateValidator()
    {
        RuleFor(x => x.X)
            .InclusiveBetween(-CanvasSize.CanvasWidth/2, CanvasSize.CanvasWidth / 2)
            .WithMessage($"The X coordinate must be between -{CanvasSize.CanvasWidth / 2} and {CanvasSize.CanvasWidth / 2}");

        RuleFor(x => x.Y)
            .InclusiveBetween(-CanvasSize.CanvasHeight / 2, CanvasSize.CanvasHeight /2)
            .WithMessage($"The Y coordinate must be between -{CanvasSize.CanvasHeight / 2} and {CanvasSize.CanvasHeight / 2}");
    }
}
