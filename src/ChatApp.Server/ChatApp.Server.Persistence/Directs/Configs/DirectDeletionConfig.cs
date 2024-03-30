using ChatApp.Server.Domain.Directs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Directs.Configs;

public sealed class DirectDeletionConfig : IEntityTypeConfiguration<DirectDeletion>
{
    public void Configure(EntityTypeBuilder<DirectDeletion> builder)
    {
        builder.HasKey(deletion => new { deletion.MessageId, deletion.UserId });

        builder.HasOne(deletion => deletion.User)
            .WithMany()
            .HasForeignKey(deletion => deletion.UserId);
    }
}