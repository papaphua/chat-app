namespace ChatApp.Server.Domain.Core.Groups;

public interface IGroupPrivacy
{
    bool IsPublic { get; set; }
    
    bool IsHidden { get; set; }
}