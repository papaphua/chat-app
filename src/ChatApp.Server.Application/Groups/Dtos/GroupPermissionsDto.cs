using ChatApp.Server.Domain.Core.Groups;

namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class GroupPermissionsDto : IGroupPermissions
{
    public bool AllowSendTextMessages { get; set; }
    
    public bool AllowSendFiles { get; set; }
    
    public bool AllowReactions { get; set; }
    
    public bool AllowAddUsers { get; set; }
}