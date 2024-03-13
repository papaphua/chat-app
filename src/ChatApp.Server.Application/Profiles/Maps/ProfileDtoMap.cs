using AutoMapper;
using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Application.Profiles.Maps;

public sealed class ProfileDtoMap : Profile
{
    public ProfileDtoMap()
    {
        CreateMap<User, ProfileDto>();
    }
}