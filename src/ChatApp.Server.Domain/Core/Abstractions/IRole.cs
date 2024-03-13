namespace ChatApp.Server.Domain.Core.Abstractions;

public interface IRole<TChat, TRoleRights>
    where TChat : IEntity
    where TRoleRights : IEntity
{
    Guid ChatId { get; set; }

    TChat Chat { get; set; }

    string Name { get; set; }
    
    ICollection<TRoleRights> Rights { get; set; }
}