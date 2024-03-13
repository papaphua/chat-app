using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Domain.Contacts.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Infrastructure.Contacts;

public sealed class ContactRepository(ApplicationDbContext dbContext)
    : Repository<Contact>(dbContext), IContactRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Contact?> GetByIdAsync(Guid id, bool includeAvatar = false)
    {
        var query = _dbContext.Set<Contact>()
            .AsQueryable();

        if (includeAvatar)
            query = query.Include(contact => contact.Avatar)
                .ThenInclude(avatar => avatar!.Resource);

        return await query.FirstOrDefaultAsync(contact => contact.Id == id);
    }

    public async Task<Contact?> GetByOwnerIdAndPartnerId(Guid ownerId, Guid partnerId)
    {
        return await _dbContext.Set<Contact>()
            .FirstOrDefaultAsync(contact => contact.OwnerId == ownerId && contact.PartnerId == partnerId);
    }
}