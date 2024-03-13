namespace ChatApp.Server.Domain.Core.Abstractions.Chats;

public interface IAdministratedMembership<TChat, TRole> : IMembership<TChat>
    where TChat : IEntity
    where TRole : IEntity
{
    Guid? RoleId { get; set; }

    TRole? Role { get; set; }
}