using AutoMapper;
using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Domain.Groups;

namespace ChatApp.Server.Application.Groups.Maps;

public sealed class GroupDtoMap : Profile
{
    public GroupDtoMap()
    {
        CreateMap<Group, GroupDto>();
    }
}