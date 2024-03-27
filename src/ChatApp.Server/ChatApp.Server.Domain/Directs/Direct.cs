namespace ChatApp.Server.Domain.Directs;

public sealed class Direct
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public ICollection<DirectMembership> Memberships { get; set; } = default!;

    public ICollection<DirectMessage> Messages { get; set; } = default!;
}