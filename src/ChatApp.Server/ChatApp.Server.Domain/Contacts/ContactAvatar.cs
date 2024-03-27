using ChatApp.Server.Domain.Resources;

namespace ChatApp.Server.Domain.Contacts;

public sealed class ContactAvatar(Guid contactId, Guid resourceId)
{
    public Guid ContactId { get; set; } = contactId;

    public Contact Contact { get; set; } = default!;

    public Guid ResourceId { get; set; } = resourceId;

    public Resource Resource { get; set; } = default!;
}