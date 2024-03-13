using AutoMapper;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Application.Shared.Map;

public sealed class NewResourceDtoMap : Profile
{
    public NewResourceDtoMap()
    {
        CreateMap<NewResourceDto, Resource>();
    }
}