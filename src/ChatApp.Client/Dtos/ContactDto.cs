namespace ChatApp.Client.Dtos;

public sealed class ContactDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public PriorityAvatarDto? Avatar { get; set; }
}