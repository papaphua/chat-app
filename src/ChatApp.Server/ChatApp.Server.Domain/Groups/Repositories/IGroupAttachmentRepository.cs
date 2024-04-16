using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Groups.Repositories;

public interface IGroupAttachmentRepository : IRepository<GroupAttachment>
{
    Task<List<GroupAttachment>> GetByMessageIdAsync(Guid messageId, bool includeResource = false);
}