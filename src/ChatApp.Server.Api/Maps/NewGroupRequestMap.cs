using AutoMapper;
using ChatApp.Server.Api.Core.Extensions;
using ChatApp.Server.Api.Requests;
using ChatApp.Server.Application.Groups.Dtos;

namespace ChatApp.Server.Api.Maps;

public class NewGroupRequestMap : Profile
{
    public NewGroupRequestMap()
    {
        CreateMap<NewGroupRequest, NewGroupDto>()
            .ForMember(dest => dest.Avatar, opt =>
                opt.MapFrom(src => src.File!.ToNewResourceDto()));
    }
}