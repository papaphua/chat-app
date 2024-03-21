using AutoMapper;
using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Domain.Groups;

namespace ChatApp.Server.Application.Groups.Maps;

public sealed class GroupPermissionsDtoMap : Profile
{
    public GroupPermissionsDtoMap()
    {
        CreateMap<Group, GroupPermissionsDto>();

        CreateMap<GroupPermissionsDto, Group>();
    }
}