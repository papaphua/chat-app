using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Domain.Shared;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupMessageRepository(ApplicationDbContext dbContext)
    : Repository<GroupMessage>(dbContext), IGroupMessageRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<PagedList<GroupMessage>> GetPagedByGroupIdAndUserIdAsync(Guid groupId, Guid userId,
        MessageParameters parameters)
    {
        return await _dbContext.Set<GroupMessage>()
            .Include(message => message.Attachments)
            .Include(message => message.Reactions)
            .Where(message => message.GroupId == groupId
                              && message.Deletions.All(deletion => deletion.UserId != userId))
            .OrderByDescending(message => message.Timestamp)
            .ToPagedListAsync(parameters);
    }

    public async Task<GroupMessage?> GetByIdAsync(Guid id, bool includeDeletions = false)
    {
        var query = _dbContext.Set<GroupMessage>()
            .AsQueryable();

        if (includeDeletions)
            query = query.Include(message => message.Deletions);

        return await query.FirstOrDefaultAsync(message => message.Id == id);
    }
}