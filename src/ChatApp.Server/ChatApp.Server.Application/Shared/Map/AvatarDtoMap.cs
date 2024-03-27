using AutoMapper;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Application.Shared.Map;

public sealed class AvatarDtoMap : Profile
{
    public AvatarDtoMap()
    {
        CreateMap<UserAvatar, AvatarDto>();
        
        CreateMap<GroupAvatar, AvatarDto>();
    }
}