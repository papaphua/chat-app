using ChatApp.Client.Core;
using ChatApp.Client.Core.Paging;
using ChatApp.Client.Dtos;
using Microsoft.AspNetCore.Components.Forms;

namespace ChatApp.Client.Services.DirectService;

public interface IDirectService
{
    Task<PagedResponse<MessageDto>> GetAllMessagesAsync(Guid directId, MessageParameters parameters);
     
    Task<DirectDto> GetDirectAsync(Guid directId);

    Task<Guid> CreateDirectAsync(Guid partnerId);

    Task RemoveDirectAsync(Guid directId);

    Task RemoveDirectForSelfAsync(Guid directId);

    Task<MessageDto> AddMessageAsync(Guid directId, string? text, List<IBrowserFile>? files = null);

    Task RemoveMessageAsync(Guid directId, Guid messageId);

    Task RemoveMessageForSelf(Guid directId, Guid messageId);

    Task AddReaction(Guid directId, Guid messageId, ReactionType type);

    Task RemoveReaction(Guid directId, Guid messageId, Guid reactionId);
}
