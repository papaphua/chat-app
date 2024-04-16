using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupReactionRepository(ApplicationDbContext dbContext)
    : Repository<GroupReaction>(dbContext), IGroupReactionRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<GroupReaction?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<GroupReaction>()
            .FirstOrDefaultAsync(reaction => reaction.Id == id);
    }

    public async Task<List<GroupReaction>> GetAllByMessageIdAndUserIdAsync(Guid messageId, Guid userId)
    {
        return await _dbContext.Set<GroupReaction>()
            .Where(reaction => reaction.MessageId == messageId
                               && reaction.UserId == userId)
            .ToListAsync();
    }

    public async Task<GroupReaction?> GetByMessageIdAndUserIdAndTypeAsync(Guid messageId, Guid userId,
        ReactionType type)
    {
        return await _dbContext.Set<GroupReaction>()
            .FirstOrDefaultAsync(reaction => reaction.MessageId == messageId
                                             && reaction.UserId == userId
                                             && reaction.Type == type);
    }
}