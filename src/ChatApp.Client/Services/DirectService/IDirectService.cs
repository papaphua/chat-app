using ChatApp.Client.Core;
using ChatApp.Client.Dtos;

namespace ChatApp.Client.Services.DirectService;

public interface IDirectService
{
    Task<DirectDto> GetDirectAsync(Guid directId);

    Task<Guid> CreateDirectAsync(Guid partnerId);

    Task RemoveDirectAsync(Guid directId);

    Task RemoveDirectForSelfAsync(Guid directId);

    Task<MessageDto> AddMessageAsync(Guid directId, NewMessageDto dto);

    Task RemoveMessageAsync(Guid directId, Guid messageId);

    Task RemoveMessageForSelf(Guid directId, Guid messageId);

    Task AddReaction(Guid directId, Guid messageId, ReactionType type);

    Task RemoveReaction(Guid directId, Guid messageId, Guid reactionId);
}
