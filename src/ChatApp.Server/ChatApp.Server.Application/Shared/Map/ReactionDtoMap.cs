using AutoMapper;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Directs;

namespace ChatApp.Server.Application.Shared.Map;

public sealed class ReactionDtoMap : Profile
{
    public ReactionDtoMap()
    {
        CreateMap<DirectReaction, ReactionDto>();
    }
}