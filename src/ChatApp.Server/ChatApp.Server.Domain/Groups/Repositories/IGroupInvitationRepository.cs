using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Shared;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupInvitationRepository : IRepository<GroupInvitation>
{
    Task<PagedList<GroupInvitation>> GetPagedByGroupIdAndUserIdAsync(Guid groupId, Guid userId,
        InvitationParameters parameters);
}