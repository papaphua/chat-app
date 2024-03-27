using AutoMapper;
using ChatApp.Server.Application.Core;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Application.Shared.Map;

public sealed class PriorityAvatarDtoMap : Profile
{
    public PriorityAvatarDtoMap()
    {
        CreateMap<ContactAvatar, PriorityAvatarDto>()
            .ForMember(dest => dest.Priority, opt =>
                opt.MapFrom(src => AvatarPriority.Contact));

        CreateMap<UserAvatar, PriorityAvatarDto>()
            .ForMember(dest => dest.Priority, opt =>
                opt.MapFrom(src => AvatarPriority.User));
    }
}