using ChatApp.Client.Core;
using ChatApp.Client.Core.Paging;
using ChatApp.Client.Dtos;

namespace ChatApp.Client.Services.DirectService;

public sealed class DirectService : IDirectService
{
    public async Task<PagedResponse<MessageDto>> GetAllMessagesAsync(Guid directId, MessageParameters parameters)
    {
        throw new NotImplementedException();
    }

    public async Task<DirectDto> GetDirectAsync(Guid directId)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateDirectAsync(Guid partnerId)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveDirectAsync(Guid directId)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveDirectForSelfAsync(Guid directId)
    {
        throw new NotImplementedException();
    }

    public async Task<MessageDto> AddMessageAsync(Guid directId, NewMessageDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveMessageAsync(Guid directId, Guid messageId)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveMessageForSelf(Guid directId, Guid messageId)
    {
        throw new NotImplementedException();
    }

    public async Task AddReaction(Guid directId, Guid messageId, ReactionType type)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveReaction(Guid directId, Guid messageId, Guid reactionId)
    {
        throw new NotImplementedException();
    }
}