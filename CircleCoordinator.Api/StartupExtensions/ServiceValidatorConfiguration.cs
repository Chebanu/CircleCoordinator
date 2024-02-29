using CircleCoordinator.Api.Validator;
using CircleCoordinator.Contracts.Http;
using FluentValidation;

internal static class ServiceValidatorConfiguration
{
    public static IServiceCollection AddValidatorConfiguration(this IServiceCollection services)
    {
        return services.AddScoped<IValidator<RequestCreateCoordinator>, CircleCoordinatorRequestCreateValidator>()
                        .AddScoped<IValidator<RequestUpdateCirclesCoordinator>, CircleCoordinatorRequestUpdateValidator>();
    }
}