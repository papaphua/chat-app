using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions.Chats;
using ChatApp.Server.Domain.Core.Abstractions.Groups;

namespace ChatApp.Server.Domain.Groups;

public sealed class GroupRole(Guid chatId, string name)
    : IEntity<Guid>, IRole<Group>, IGroupRoleRight
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ChatId { get; set; } = chatId;

    public Group Chat { get; set; } = default!;

    public string Name { get; set; } = name;
    
    public bool AllowChangeGroupInfo { get; set; } = true;
    
    public bool AllowDeleteMessages { get; set; } = true;
    
    public bool AllowBanUsers { get; set; } = true;
    
    public bool AllowInviteUsersViaLink { get; set; } = true;
    
    public bool AllowAddNewAdmins { get; set; }
}