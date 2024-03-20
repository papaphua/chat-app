using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Directs.Repositories;

public interface IDirectReactionRepository : IRepository<DirectReaction>
{
    Task<DirectReaction?> GetByIdAsync(Guid id);
    
    Task<List<DirectReaction>> GetAllByMessageIdAndUserIdAsync(Guid messageId, Guid userId);

    Task<DirectReaction?> GetByMessageIdAndUserIdAndTypeAsync(Guid messageId, Guid userId, ReactionType type);
}