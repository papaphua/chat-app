using ChatApp.Server.Domain.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Contacts.Configs;

public sealed class ContactConfig : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasOne(contact => contact.Partner)
            .WithMany()
            .HasForeignKey(contact => contact.PartnerId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasOne(contact => contact.Avatar)
            .WithOne(avatar => avatar.Contact)
            .HasForeignKey<ContactAvatar>(avatar => avatar.ContactId);
    }
}