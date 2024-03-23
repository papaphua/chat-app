using AutoMapper;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Presentation.Core.Extensions;
using ChatApp.Server.Presentation.Requests;

namespace ChatApp.Server.Presentation.Maps;

public sealed class NewMessageRequestMap : Profile
{
    public NewMessageRequestMap()
    {
        CreateMap<NewMessageRequest, NewMessageDto>()
            .ForMember(dest => dest.Attachments, opt =>
                opt.MapFrom(src => src.Files.ToNewResourceDtoList()));
    }
}