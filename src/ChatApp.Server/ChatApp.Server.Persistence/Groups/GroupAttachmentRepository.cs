using ChatApp.Server.Domain.Groups;
using ChatApp.Server.Domain.Groups.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Groups;

public sealed class GroupAttachmentRepository(ApplicationDbContext dbContext)
    : Repository<GroupAttachment>(dbContext), IGroupAttachmentRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<GroupAttachment>> GetByMessageIdAsync(Guid messageId, bool includeResource = false)
    {
        var query = _dbContext.Set<GroupAttachment>()
            .AsQueryable();

        if (includeResource)
            query = query.Include(attachment => attachment.Resource);

        return await query.Where(attachment => attachment.MessageId == messageId)
            .ToListAsync();
    }
}