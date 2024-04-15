namespace ChatApp.Server.Application.Groups.Dtos;

public sealed class PermissionsDto
{
    public bool AllowSendTextMessages { get; set; }

    public bool AllowSendFiles { get; set; }

    public bool AllowReactions { get; set; }

    public bool AllowAddMembers { get; set; }

    public bool IsPublic { get; set; }
}