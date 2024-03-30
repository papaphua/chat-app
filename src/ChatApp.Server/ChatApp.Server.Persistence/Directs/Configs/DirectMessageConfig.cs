using ChatApp.Server.Domain.Directs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Directs.Configs;

public sealed class DirectMessageConfig : IEntityTypeConfiguration<DirectMessage>
{
    public void Configure(EntityTypeBuilder<DirectMessage> builder)
    {
        builder.HasOne(message => message.Sender)
            .WithMany()
            .HasForeignKey(message => message.SenderId);

        builder.HasMany(message => message.Attachments)
            .WithOne(attachment => attachment.Message)
            .HasForeignKey(attachment => attachment.MessageId);

        builder.HasMany(message => message.Reactions)
            .WithOne(reaction => reaction.Message)
            .HasForeignKey(reaction => reaction.MessageId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.HasMany(message => message.Deletions)
            .WithOne(deletion => deletion.Message)
            .HasForeignKey(deletion => deletion.MessageId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}