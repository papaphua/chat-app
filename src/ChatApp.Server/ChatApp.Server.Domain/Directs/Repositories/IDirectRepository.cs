using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Directs.Repositories;

public interface IDirectRepository : IRepository<Direct>
{
    Task<List<Direct>> GetAllByUserIdAsync(Guid userId, bool includeMembers = false,
        bool includeMemberAvatars = false);
    
    Task<Direct?> GetByIdAsync(Guid id, bool includeMembers = false, bool includeMemberAvatars = false);

    Task<Direct?> GetByMemberIds(Guid memberId1, Guid memberId2, bool includeMembers = false);
}