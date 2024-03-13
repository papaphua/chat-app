using ChatApp.Server.Domain.Users;

namespace ChatApp.Server.Domain.Core.Abstractions;

public interface IDeletion<TMessage>
    where TMessage : IEntity
{
    public Guid MessageId { get; set; }

    public TMessage Message { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }
}