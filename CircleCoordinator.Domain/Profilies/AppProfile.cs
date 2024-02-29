using AutoMapper;
using CircleCoordinator.Domain.Models.Database;

namespace CircleCoordinator.Domain.Profilies;

internal class AppProfile : Profile
{
    public AppProfile()
    {
        CreateMap<Coordinator, Contracts.Models.Coordinator>().ReverseMap();

        CreateMap<Contracts.Models.CircleSet, CircleSet>();
    }
}