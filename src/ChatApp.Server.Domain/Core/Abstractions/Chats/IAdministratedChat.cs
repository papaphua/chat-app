namespace ChatApp.Server.Domain.Core.Abstractions.Chats;

public interface IAdministratedChat<TMembership, TMessage, TRole, TAvatar>
    : IChat<TMembership, TMessage>
    where TMembership : IEntity
    where TMessage : IEntity
    where TRole : IEntity
    where TAvatar : IEntity
{
    ICollection<TAvatar> Avatars { get; set; }

    ICollection<TRole> Roles { get; set; }
}