using AutoMapper;
using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Application.Groups.Maps;

public sealed class GroupMemberDtoMap : Profile
{
    public GroupMemberDtoMap()
    {
        CreateMap<User, GroupMemberDto>();
    }
}