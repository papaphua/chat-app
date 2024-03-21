using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupMembershipRepository : IRepository<GroupMembership>
{
    Task<GroupMembership?> GetByIdsAsync(Guid groupId, Guid memberId, bool includeRole = false);
}