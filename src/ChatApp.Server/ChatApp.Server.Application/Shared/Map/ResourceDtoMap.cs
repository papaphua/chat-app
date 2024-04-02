using AutoMapper;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Application.Shared.Map;

public sealed class ResourceDtoMap : Profile
{
    public ResourceDtoMap()
    {
        CreateMap<Resource, ResourceDto>()
            .ForMember(dest => dest.Bytes, opt =>
                opt.MapFrom(src => Convert.ToBase64String(src.Bytes)));
    }
}