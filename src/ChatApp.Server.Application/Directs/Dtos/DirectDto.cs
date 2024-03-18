using ChatApp.Server.Application.Shared.Dtos;

namespace ChatApp.Server.Application.Directs.Dtos;

public sealed class DirectDto
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }

    public string Username { get; set; } = default!;
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Bio { get; set; }

    public List<PriorityAvatarDto> Avatars { get; set; } = default!;
}