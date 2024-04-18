namespace ChatApp.Server.Application.Home.Dtos;

public sealed class SearchPreviewDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public Guid? ResourceId { get; set; }
    
    public SearchType Type { get; set; }
}