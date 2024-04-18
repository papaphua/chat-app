using AutoMapper;
using ChatApp.Server.Application.Home.Dtos;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Application.Home.Maps;

public sealed class SearchPreviewDtoMap : Profile
{
    public SearchPreviewDtoMap()
    {
        CreateMap<User, SearchPreviewDto>()
            .ForMember(dest => dest.Type, opt =>
                opt.MapFrom(src => SearchType.User));
        CreateMap<Group, SearchPreviewDto>()
            .ForMember(dest => dest.Type, opt =>
                opt.MapFrom(src => SearchType.Group));
    }
}