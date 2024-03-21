using AutoMapper;
using ChatApp.Server.Application.Profiles.Dtos;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Application.Profiles.Maps;

public sealed class ProfileDetailsDtoMap : Profile
{
    public ProfileDetailsDtoMap()
    {
        CreateMap<ProfileDetailsDto, User>()
            .ForMember(dest => dest.FirstName, opt =>
                opt.MapFrom(src => string.IsNullOrWhiteSpace(src.FirstName)
                    ? null
                    : src.FirstName))
            .ForMember(dest => dest.LastName, opt =>
                opt.MapFrom(src => string.IsNullOrWhiteSpace(src.LastName)
                    ? null
                    : src.LastName))
            .ForMember(dest => dest.Bio, opt =>
                opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Bio)
                    ? null
                    : src.Bio));

        CreateMap<User, ProfileDetailsDto>();
    }
}