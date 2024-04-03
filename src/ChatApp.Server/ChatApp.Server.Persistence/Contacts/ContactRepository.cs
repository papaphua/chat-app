using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Domain.Contacts.Repositories;
using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Persistence.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Persistence.Contacts;

public sealed class ContactRepository(ApplicationDbContext dbContext)
    : Repository<Contact>(dbContext), IContactRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<PagedList<Contact>> GetPagedByOwnerId(Guid ownerId, ContactParameters parameters,
        bool includeAvatar = false, bool includeAvatarResource = false)
    {
        var query = _dbContext.Set<Contact>()
            .AsQueryable();

        if (includeAvatar)
            query = query.Include(contact => contact.Avatar);

        if (includeAvatarResource)
            query = query.Include(contact => contact.Avatar!.Resource);

        return await query.ToPagedListAsync(parameters);
    }

    public async Task<Contact?> GetByIdAsync(Guid id, bool includeAvatar = false, bool includeAvatarResource = false)
    {
        var query = _dbContext.Set<Contact>()
            .AsQueryable();

        if (includeAvatar)
            query = query.Include(contact => contact.Avatar);

        if (includeAvatarResource)
            query = query.Include(contact => contact.Avatar!.Resource);

        return await query.FirstOrDefaultAsync(contact => contact.Id == id);
    }

    public async Task<Contact?> GetByOwnerIdAndPartnerId(Guid ownerId, Guid partnerId, bool includeAvatar = false,
        bool includeAvatarResource = false)
    {
        var query = _dbContext.Set<Contact>()
            .AsQueryable();

        if (includeAvatar)
            query = query.Include(contact => contact.Avatar);

        if (includeAvatarResource)
            query = query.Include(contact => contact.Avatar!.Resource);

        return await query.FirstOrDefaultAsync(contact => contact.OwnerId == ownerId && contact.PartnerId == partnerId);
    }
}