using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Paging;
using ChatApp.Server.Domain.Shared;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupRoleRepository : IRepository<GroupRole>
{
    Task<PagedList<GroupRole>> GetByGroupId(Guid groupId, RoleParameters parameters);
}