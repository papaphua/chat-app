namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class GroupInvitationDto
{
    public Guid CreatorId { get; set; }
    
    public string Link { get; set; }
    
    public DateTime Timestamp { get; set; }
}