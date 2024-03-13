namespace ChatApp.Server.Domain.Core.Abstractions.Chats;

public interface IRole<TChat>
    where TChat : IEntity
{
    Guid ChatId { get; set; }

    TChat Chat { get; set; }

    string Name { get; set; }
}