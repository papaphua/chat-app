using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupRepository : IRepository<Group>
{
    Task<List<Group>> GetByNameAsync(Guid userId, string search, bool includeAvatars = false);
    
    Task<List<Group>> GetAllByUserIdAsync(Guid userId, bool includeAvatars = false);
    
    Task<Group?> GetByIdAsync(Guid id);
}