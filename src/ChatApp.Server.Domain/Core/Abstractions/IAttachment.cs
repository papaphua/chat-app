using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Domain.Core.Abstractions;

public interface IAttachment<TMessage>
    where TMessage : IEntity
{
    Guid MessageId { get; set; }

    TMessage Message { get; set; }

    Guid ResourceId { get; set; }

    Resource Resource { get; set; }
}