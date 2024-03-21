namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class GroupInfoDto 
{
    public string Name { get; set; } = default!;

    public string? Info { get; set; }
}