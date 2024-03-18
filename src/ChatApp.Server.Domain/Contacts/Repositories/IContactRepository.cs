using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Contacts.Repositories;

public interface IContactRepository : IRepository<Contact>
{
    Task<Contact?> GetByIdAsync(Guid id, bool includeAvatar = false);

    Task<Contact?> GetByOwnerIdAndPartnerId(Guid ownerId, Guid partnerId, bool includeAvatar = false);
}