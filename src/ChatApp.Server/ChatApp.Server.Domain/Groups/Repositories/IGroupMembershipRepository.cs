using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Shared;
using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupMembershipRepository : IRepository<GroupMembership>
{
    Task<PagedList<User>> GetPagedByGroupIdAsync(Guid groupId,
        MemberParameters parameters);
    
    Task<GroupMembership?> GetByGroupIdAndMemberIdAsync(Guid groupId, Guid memberId, bool includeGroup = false,
        bool includeGroupWithAvatars = false, bool includeRole = false);
};