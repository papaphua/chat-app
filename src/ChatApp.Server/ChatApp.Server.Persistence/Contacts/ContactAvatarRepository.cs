using ChatApp.Server.Domain.Contacts;
using ChatApp.Server.Domain.Contacts.Repositories;
using ChatApp.Server.Persistence.Core.Abstractions;

namespace ChatApp.Server.Persistence.Contacts;

public sealed class ContactAvatarRepository(ApplicationDbContext dbContext)
    : Repository<ContactAvatar>(dbContext), IContactAvatarRepository;