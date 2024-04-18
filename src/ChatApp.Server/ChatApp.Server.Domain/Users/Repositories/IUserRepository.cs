using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Users.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<List<User>> GetByUserNameAsync(Guid userId, string search, bool includeAvatars = false);
    
    Task<User?> GetByIdAsync(Guid id, bool includeAvatars = false);
}