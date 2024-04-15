using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupMembershipRepository : IRepository<GroupMembership>
{
    Task<GroupMembership?> GetByGroupIdAndMemberIdAsync(Guid groupId, Guid memberId, bool includeGroup = false,
        bool includeRole = false);
};