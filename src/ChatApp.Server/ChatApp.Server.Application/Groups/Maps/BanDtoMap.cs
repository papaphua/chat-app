using AutoMapper;
using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Domain.Groups;

namespace ChatApp.Server.Application.Groups.Maps;

public sealed class BanDtoMap : Profile
{
    public BanDtoMap()
    {
        CreateMap<GroupBan, BanDto>()
            .ForAllMembers(opt => opt.MapFrom(
                src => src.User));
    }
}