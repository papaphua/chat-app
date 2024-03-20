using AutoMapper;
using ChatApp.Server.Api.Core.Extensions;
using ChatApp.Server.Api.Requests;
using ChatApp.Server.Application.Shared.Dtos;

namespace ChatApp.Server.Api.Maps;

public sealed class NewMessageRequestMap : Profile
{
    public NewMessageRequestMap()
    {
        CreateMap<NewMessageRequest, NewMessageDto>()
            .ForMember(dest => dest.Attachments, opt =>
                opt.MapFrom(src => src.Files.ToNewResourceDtoList()));
    }
}