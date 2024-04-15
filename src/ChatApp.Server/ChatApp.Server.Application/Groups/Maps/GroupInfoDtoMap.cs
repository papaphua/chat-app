using AutoMapper;
using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Domain.Groups;

namespace ChatApp.Server.Application.Groups.Maps;

public sealed class GroupInfoDtoMap : Profile
{
    public GroupInfoDtoMap()
    {
        CreateMap<Group, GroupInfoDto>();
    }
}