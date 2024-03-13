using ChatApp.Server.Domain.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Infrastructure.Contacts.Configs;

public sealed class ContactAvatarConfig : IEntityTypeConfiguration<ContactAvatar>
{
    public void Configure(EntityTypeBuilder<ContactAvatar> builder)
    {
        builder.HasKey(avatar => new { avatar.ContactId, avatar.ResourceId });

        builder.HasOne(avatar => avatar.Resource)
            .WithOne()
            .HasForeignKey<ContactAvatar>(avatar => avatar.ResourceId);
    }
}