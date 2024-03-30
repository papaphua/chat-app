using ChatApp.Server.Domain.Directs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Directs.Configs;

public sealed class DirectAttachmentConfig : IEntityTypeConfiguration<DirectAttachment>
{
    public void Configure(EntityTypeBuilder<DirectAttachment> builder)
    {
        builder.HasKey(attachment => new { attachment.MessageId, attachment.ResourceId });

        builder.HasOne(attachment => attachment.Resource)
            .WithOne()
            .HasForeignKey<DirectAttachment>(attachment => attachment.ResourceId);
    }
}