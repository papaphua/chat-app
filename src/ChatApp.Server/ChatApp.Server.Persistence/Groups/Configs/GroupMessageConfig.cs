using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Groups.Configs;

public sealed class GroupMessageConfig : IEntityTypeConfiguration<GroupMessage>
{
    public void Configure(EntityTypeBuilder<GroupMessage> builder)
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