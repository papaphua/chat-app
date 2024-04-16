using AutoMapper;
using ChatApp.Server.Application.Groups.Dtos;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Application.Groups.Maps;

public sealed class BanDtoMap : Profile
{
    public BanDtoMap()
    {
        CreateMap<User, BanDto>();
    }
}