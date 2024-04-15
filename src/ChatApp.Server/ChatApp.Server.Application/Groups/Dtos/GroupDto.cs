using ChatApp.Server.Application.Shared.Dtos;

namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class GroupDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string? Info { get; set; }
    
    public Guid OwnerId { get; set; }

    public List<AvatarDto> Avatars { get; set; } = [];
}