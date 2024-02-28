using CircleCoordinator.Domain.Algorithm;
using CircleCoordinator.Domain.Helpers;
using CircleCoordinator.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CircleCoordinator.Domain;

public static class DomainServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        return services.AddSingleton<IDateTimeProvider, DateTimeProvider>()
                        .AddScoped<ICircleDrawer, CircleDrawer>()
                        .AddScoped<ICoordinatorRepository, CoordinatorRepository>();
    }
}
