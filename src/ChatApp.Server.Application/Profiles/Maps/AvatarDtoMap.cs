using AutoMapper;
using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Application.Profiles.Maps;

public sealed class AvatarDtoMap : Profile
{
    public AvatarDtoMap()
    {
        CreateMap<UserAvatar, AvatarDto>()
            .ForMember(dest => dest.Timestamp, opt =>
                opt.MapFrom(src => src.Resource.Timestamp));
    }
}