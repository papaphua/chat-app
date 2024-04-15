using ChatApp.Server.Application.Shared.Dtos;

namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class GroupMemberDto
{
    public Guid Id { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string UserName { get; set; }
    
    public List<AvatarDto> Avatars { get; set; }
}