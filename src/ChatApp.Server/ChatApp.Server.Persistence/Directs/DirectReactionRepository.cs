using ChatApp.Server.Domain.Core;
using ChatApp.Server.Domain.Directs;
using ChatApp.Server.Domain.Directs.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Directs;

public sealed class DirectReactionRepository(ApplicationDbContext dbContext)
    : Repository<DirectReaction>(dbContext), IDirectReactionRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<DirectReaction?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<DirectReaction>()
            .FirstOrDefaultAsync(reaction => reaction.Id == id);
    }

    public async Task<List<DirectReaction>> GetAllByMessageIdAndUserIdAsync(Guid messageId, Guid userId)
    {
        return await _dbContext.Set<DirectReaction>()
            .Where(reaction => reaction.MessageId == messageId
                               && reaction.UserId == userId)
            .ToListAsync();
    }

    public async Task<DirectReaction?> GetByMessageIdAndUserIdAndTypeAsync(Guid messageId, Guid userId,
        ReactionType type)
    {
        return await _dbContext.Set<DirectReaction>()
            .FirstOrDefaultAsync(reaction => reaction.MessageId == messageId
                                             && reaction.UserId == userId
                                             && reaction.Type == type);
    }
}