using AutoMapper;
using ChatApp.Server.Application.Core;
using ChatApp.Server.Application.Directs.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Application.Directs.Maps;

public sealed class DirectDtoMap : Profile
{
    public DirectDtoMap()
    {
        CreateMap<Direct, DirectDto>();

        CreateMap<User, DirectDto>()
            .ForMember(dest => dest.Id, opt =>
                opt.Ignore())
            .ForMember(dest => dest.UserId, opt =>
                opt.MapFrom(src => src.Id));

        CreateMap<Contact, DirectDto>()
            .ForMember(dest => dest.Id, opt =>
                opt.Ignore())
            .ForMember(dest => dest.Avatars, opt =>
                opt.MapFrom((src, _, destMember, context) =>
                {
                    List<PriorityAvatarDto> avatars = [];

                    if (destMember.Count > 0)
                        avatars.AddRange(destMember);

                    if (src.Avatar == null) return avatars;

                    var avatar = context.Mapper.Map<PriorityAvatarDto>(src.Avatar);
                    avatar.Priority = AvatarPriority.Contact;
                    avatars.Add(avatar);

                    return avatars;
                }));
    }
}