using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Domain.Contacts.Repositories;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Infrastructure.Contacts;

public sealed class ContactAvatarRepository(ApplicationDbContext dbContext)
    : Repository<ContactAvatar>(dbContext), IContactAvatarRepository
{
}