namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class BanDto
{
    public Guid Id { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string UserName { get; set; }
}