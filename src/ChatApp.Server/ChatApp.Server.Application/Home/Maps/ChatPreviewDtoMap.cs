using AutoMapper;
using ChatApp.Server.Application.Home.Dtos;
using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Groups;

namespace ChatApp.Server.Application.Home.Maps;

public sealed class ChatPreviewDtoMap : Profile
{
    public ChatPreviewDtoMap()
    {
        CreateMap<Group, ChatPreviewDto>()
            .ForMember(dest => dest.Type, opt =>
                opt.MapFrom(src => ChatType.Group));

        CreateMap<Direct, ChatPreviewDto>()
            .ForMember(dest => dest.Type, opt =>
                opt.MapFrom(src => ChatType.Direct));
    }
}