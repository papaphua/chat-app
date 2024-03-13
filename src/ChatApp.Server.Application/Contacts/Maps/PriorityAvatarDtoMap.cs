using AutoMapper;
using ChatApp.Server.Application.Contacts.Dtos;
using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Application.Contacts.Maps;

public sealed class PriorityAvatarDtoMap : Profile
{
    public PriorityAvatarDtoMap()
    {
        CreateMap<ContactAvatar, PriorityAvatarDto>()
            .ForMember(dest => dest.Timestamp, opt =>
                opt.MapFrom(src => src.Resource.Timestamp));
        CreateMap<UserAvatar, PriorityAvatarDto>()
            .ForMember(dest => dest.Timestamp, opt =>
                opt.MapFrom(src => src.Resource.Timestamp));
    }
}