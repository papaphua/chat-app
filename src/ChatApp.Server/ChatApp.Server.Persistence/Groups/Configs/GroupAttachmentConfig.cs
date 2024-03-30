using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Infrastructure.Groups.Configs;

public sealed class GroupAttachmentConfig : IEntityTypeConfiguration<GroupAttachment>
{
    public void Configure(EntityTypeBuilder<GroupAttachment> builder)
    {
        builder.HasKey(attachment => new { attachment.MessageId, attachment.ResourceId });

        builder.HasOne(attachment => attachment.Resource)
            .WithOne()
            .HasForeignKey<GroupAttachment>(attachment => attachment.ResourceId);
    }
}