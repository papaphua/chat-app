using ChatApp.Server.Domain.Groups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApp.Server.Persistence.Groups.Configs;

public sealed class GroupDeletionConfig : IEntityTypeConfiguration<GroupDeletion>
{
    public void Configure(EntityTypeBuilder<GroupDeletion> builder)
    {
        builder.HasKey(deletion => new { deletion.MessageId, deletion.UserId });

        builder.HasOne(deletion => deletion.User)
            .WithMany()
            .HasForeignKey(deletion => deletion.UserId);
    }
}