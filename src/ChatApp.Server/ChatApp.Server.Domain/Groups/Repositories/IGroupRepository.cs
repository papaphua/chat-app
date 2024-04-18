using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupRepository : IRepository<Group>
{
    Task<List<Group>> GetAllByUserIdAsync(Guid userId, bool includeAvatars = false);
    
    Task<Group?> GetByIdAsync(Guid id);
}