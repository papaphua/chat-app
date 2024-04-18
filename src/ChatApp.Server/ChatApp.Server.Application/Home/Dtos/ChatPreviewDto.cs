namespace ChatApp.Server.Application.Home.Dtos;

public sealed class ChatPreviewDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public Guid? ResourceId { get; set; }
    
    public ChatType Type { get; set; }
    
    public string LastMessage { get; set; }
    
    public DateTime? Timestamp { get; set; }
}