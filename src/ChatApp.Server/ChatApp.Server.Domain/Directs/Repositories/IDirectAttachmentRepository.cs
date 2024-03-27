using ChatApp.Server.Domain.Core.Abstractions;

namespace ChatApp.Server.Domain.Directs.Repositories;

public interface IDirectAttachmentRepository : IRepository<DirectAttachment>
{
    Task<List<DirectAttachment>> GetByMessageIdAsync(Guid messageId, bool includeResource = false);
}