using ChatApp.Server.Application.Directs.Dtos;
using ChatApp.Server.Application.Shared.Dtos;
using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Core.Abstractions.Results;

namespace ChatApp.Server.Application.Directs;

public interface IDirectService
{
    Task<Result<DirectDto>> GetDirectAsync(Guid userId, Guid directId);

    Task<Result<Guid>> CreateDirectAsync(Guid userId, Guid partnerId);

    Task<Result> RemoveDirectAsync(Guid userId, Guid directId);

    Task<Result> RemoveDirectForSelfAsync(Guid userId, Guid directId);

    Task<Result<MessageDto>> AddMessageAsync(Guid userId, Guid directId, NewMessageDto dto);

    Task<Result<Guid>> RemoveMessageAsync(Guid userId, Guid directId, Guid messageId);
    
    Task<Result> RemoveMessageForSelfAsync(Guid userId, Guid directId, Guid messageId);

    Task<Result<ReactionDto>> AddReactionAsync(Guid userId, Guid directId, Guid messageId, ReactionType type);

    Task<Result<Guid>> RemoveReactionAsync(Guid userId, Guid directId, Guid messageId, Guid reactionId);
}