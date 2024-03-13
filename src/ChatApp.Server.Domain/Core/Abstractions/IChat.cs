namespace ChatApp.Server.Domain.Core.Abstractions;

public interface IChat<TMembership, TMessage>
    where TMembership : IEntity
    where TMessage : IEntity
{
    ICollection<TMembership> Memberships { get; set; }
    
    ICollection<TMessage> Messages { get; set; }
}