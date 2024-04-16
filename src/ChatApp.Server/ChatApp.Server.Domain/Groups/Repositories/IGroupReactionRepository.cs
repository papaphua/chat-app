using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupReactionRepository : IRepository<GroupReaction>
{
    Task<GroupReaction?> GetByIdAsync(Guid id);

    Task<List<GroupReaction>> GetAllByMessageIdAndUserIdAsync(Guid messageId, Guid userId);

    Task<GroupReaction?> GetByMessageIdAndUserIdAndTypeAsync(Guid messageId, Guid userId, ReactionType type);
}