using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Users.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByIdAsync(Guid id, bool includeAvatars = false);
}