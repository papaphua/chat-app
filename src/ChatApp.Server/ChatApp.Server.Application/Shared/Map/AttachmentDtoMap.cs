﻿using AutoMapper;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Groups;

namespace ChatApp.Server.Application.Shared.Map;

public sealed class AttachmentDtoMap : Profile
{
    public AttachmentDtoMap()
    {
        CreateMap<DirectAttachment, AttachmentDto>();
        CreateMap<GroupAttachment, AttachmentDto>();
    }
}